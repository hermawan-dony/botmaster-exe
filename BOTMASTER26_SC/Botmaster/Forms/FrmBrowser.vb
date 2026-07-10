Imports System.ComponentModel
Imports System.Threading
Imports System.Web.UI
Imports System.Windows
Imports Microsoft.Web.WebView2.Core
Imports Newtonsoft.Json
Imports OpenAI_API.Models

Public Class FrmBrowser

#Region "Fields"

    Public LoginTag As String
    Public IsWhatsAppLoggedIn As Boolean
    Private WAPI As String
    Public IsLoggedIn As Boolean = False
    Public IsBeta As Boolean
    Public IsPaused As Boolean
    Private WebViewIsBusy As Boolean
    Public CurrentProfile As String
    Public ReceivingCounter As Integer

    ' Message logging
    Private allReceivedMessage As String = String.Empty

    ' Bulk sending fields
    Private _Destinations As List(Of DestinationModel)
    Private _Messages As List(Of String)
    Private _attachments As List(Of AttachmentModel)
    Private _SafeMode As Boolean
    Private _buttons As String
    Private _withCatalog As Boolean
    Private ButtonText As String
    Private IsBtn As Boolean
    Private _sendingListView As ListView   ' captured on UI thread at bulk start

    ' Timers
    Private WithEvents LoginTimer As New Forms.Timer With {.Interval = 5000, .Enabled = False}
    Private WithEvents RequestTimer As New Forms.Timer With {.Interval = 3000, .Enabled = True}

    ' File locks
    Private ReadOnly autoReplyLock As New Object()
    Private ReadOnly messageKeyLock As New Object()

    ' Constants
    Private Const MAX_MESSAGE_BODY_LENGTH As Integer = 500
    Private Const SCRIPT_INJECTION_DELAY_MS As Integer = 8000
    Private Const CHAT_OPEN_DELAY_MS As Integer = 700
    Private Const ATTACHMENT_DELAY_MS As Integer = 600

#End Region

#Region "Initialization & WebView Setup"

    Private Async Sub FrmBrowser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Await InitializeWebView2Async()
        Catch ex As Exception
            HandleWebViewInitializationError(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Initializes WebView2 control with appropriate profile and navigates to WhatsApp Web
    ''' </summary>
    Private Async Function InitializeWebView2Async() As Task
        ' Check WebView2 runtime
        Try
            WebView2.InitializeLifetimeService()
        Catch ex As Exception
            If Not PromptWebView2Installation() Then
                End
                Return
            End If
        End Try

        WebViewIsBusy = False

        ' Create WebView2 environment with profile
        Dim webView2Environment As CoreWebView2Environment = Nothing
        Try
            Dim profilePath As String = If(String.IsNullOrEmpty(ProfileName),
                                           ClsSpecialDirectories.GetGetDefaultProfile,
                                           ClsSpecialDirectories.GetProfiles & ProfileName)

            CurrentWaProfile = profilePath
            webView2Environment = Await CoreWebView2Environment.CreateAsync(Nothing, profilePath)
        Catch ex As Exception
            Debug.WriteLine($"Error creating WebView2 environment: {ex.Message}")
        End Try

        ' Initialize WebView2
        Await WebView2.EnsureCoreWebView2Async(webView2Environment)
        WebView2.Source = New Uri("https://web.whatsapp.com")

        ' Load and prepare WAPI script
        WAPI = GetWAPI()
        If WAPI IsNot Nothing Then
            WAPI = WAPI.Replace("(10452:?)", "")
        Else
            WAPI = String.Empty
        End If

        ' Start login monitoring
        IsLoggedIn = False
        LoginTimer.Enabled = True
    End Function

    Private Function PromptWebView2Installation() As Boolean
        Dim result = MsgBox($"You have to install Edge Runtime to start the application.{vbCrLf}Do you want to install it?",
                           vbCritical + vbYesNo, Application.ProductName)

        If result = vbYes Then
            Try
                Process.Start("https://developer.microsoft.com/en-us/microsoft-edge/webview2/")
            Catch ex As Exception
                Debug.WriteLine($"Error opening browser: {ex.Message}")
            End Try
        End If

        Return False
    End Function

    Private Sub HandleWebViewInitializationError(ex As Exception)
        Debug.WriteLine($"WebView initialization error: {ex.Message}")
        MsgBox("Failed to initialize WhatsApp Web. Please restart the application.", vbCritical, Application.ProductName)
    End Sub

    Private Function GetWAPI() As String
        Try
            Return WAPIScript
        Catch ex As Exception
            Debug.WriteLine($"Error loading WAPI script: {ex.Message}")
            Return Nothing
        End Try
    End Function

    Private Async Sub WebView2_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles WebView2.NavigationCompleted
        Try


            Do
                Debug.WriteLine("WAITING TO INJECT SCRIPT")
                Await Task.Delay(1000)
                ' Check if page loaded
                Dim elementCount = Val(Await WebView2.ExecuteScriptAsync("document.getElementsByTagName('button').length"))
                If elementCount > 4 Then
                    Exit Do
                End If
            Loop

            ' Additional delay for stability
            Await Task.Delay(SCRIPT_INJECTION_DELAY_MS)

            ' Inject WAPI script
            If Not String.IsNullOrEmpty(WAPI) Then
                Await WebView2.ExecuteScriptAsync(WAPI)
                Debug.WriteLine("WAPI script injected successfully")

                ' Verify WAPI loaded
                Await Task.Delay(1000)
                Dim wapiLoaded = Await WebView2.ExecuteScriptAsync("typeof WAPI !== 'undefined';")
                Debug.WriteLine($"WAPI loaded: {wapiLoaded}")
            Else
                Debug.WriteLine("WAPI script is empty or null")
            End If
        Catch ex As Exception
            Debug.WriteLine($"Error in navigation completed: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Login & Authentication"

    Private Async Sub LoginTimer_tick() Handles LoginTimer.Tick
        Try
            ' Check if WAPI is loaded first
            Dim wapiCheckResult As String = Await WebView2.ExecuteScriptAsync("typeof WAPI !== 'undefined' && WAPI.isLoggedIn ? WAPI.isLoggedIn() : false;")

            ' Handle the result safely
            Dim _WAPILoginResult As Boolean = False
            If Not String.IsNullOrEmpty(wapiCheckResult) AndAlso wapiCheckResult.ToLower <> "null" Then
                Boolean.TryParse(wapiCheckResult.ToLower, _WAPILoginResult)
            End If

            IsLoggedIn = _WAPILoginResult
            Status.IsWAPIloggedin = IsLoggedIn

            UpdateLoginStatusUI(IsLoggedIn)

            ' Only update account info if logged in
            If IsLoggedIn Then
                FrmMain.Label3.Text = $"Account:{(Await GetMe()).Replace("""", "")}"
            End If
        Catch ex As Exception
            Debug.WriteLine($"Error checking login status: {ex.Message}")
        End Try
    End Sub

    Private Sub UpdateLoginStatusUI(loggedIn As Boolean)
        If InvokeRequired Then
            BeginInvoke(Sub() UpdateLoginStatusUI(loggedIn))
            Return
        End If

        FrmMain.LblIsLoggedIn.Text = If(loggedIn, "Logged In", "Not Ready")
        FrmMain.LabelLoginStatus.BackColor = If(loggedIn, Color.Green, Color.Red)
        FrmMain.LabelLoginStatus.Text = If(loggedIn, "Ready", "Not Ready")

        Debug.WriteLine($"IsLoggedIn:{loggedIn}")
    End Sub

    Public Async Function GetMe() As Task(Of String)
        Try
            Return Await WebView2.ExecuteScriptAsync("WPP.conn.getMyUserId().user")
        Catch ex As Exception
            Debug.WriteLine($"Error getting user ID: {ex.Message}")
            Return "N/A"
        End Try
    End Function

    Public Function IsWAPILoggedIn() As Boolean
        Return IsLoggedIn
    End Function

#End Region

#Region "Message Receiving & Processing"

    Private Async Sub MessageRequester() Handles RequestTimer.Tick
        If FrmMain.ButtonSwitch.Dock = DockStyle.Left Then
            Exit Sub
        End If

        Try
            Await WebView2.ExecuteScriptAsync("WAPI.getAllChatsWithNewMsg(e=>{ var requestMessage = {}; requestMessage.inputMessage=e; window.chrome.webview.postMessage(requestMessage)})")
        Catch ex As Exception
            Debug.WriteLine($"Error requesting messages: {ex.Message}")
        End Try
    End Sub

    Private Async Sub WebView2_WebMessageReceived(sender As Object, e As CoreWebView2WebMessageReceivedEventArgs) Handles WebView2.WebMessageReceived
        Debug.WriteLine(e.WebMessageAsJson)

        Try
            Dim jsonMessage As String = e.WebMessageAsJson.ToString()

            Select Case True
                Case jsonMessage.Contains("isBeta:")
                    HandleBetaStatus(jsonMessage)

                Case jsonMessage.Contains("checkedResult")
                    HandleAccountCheckResult(jsonMessage)

                Case jsonMessage.Contains("BOTMASTER_RECEIVED_MESSAGE")
                    ReceivedMessage(jsonMessage)

                Case jsonMessage.Contains("GroupsContacts123")
                    Await HandleGroupContactsFetch(jsonMessage)

                Case jsonMessage.Contains("inputMessage")
                    Await HandleInputMessages(jsonMessage)

                Case jsonMessage.Contains("sentStatus")
                    HandleSentStatus(jsonMessage)
            End Select
        Catch ex As Exception
            Debug.WriteLine($"Error processing web message: {ex.Message}")
        End Try
    End Sub

    Private Sub HandleBetaStatus(jsonMessage As String)
        IsBeta = jsonMessage.ToLower.Contains("true")
        Status.IsBeta = IsBeta
    End Sub

    Private Sub HandleAccountCheckResult(jsonMessage As String)
        Try
            Dim NumResponse As CheckNumResponseModel = JsonConvert.DeserializeObject(Of CheckNumResponseModel)(jsonMessage)
            UpdateFilterList(NumResponse)
        Catch ex As Exception
            Debug.WriteLine($"Error handling account check result: {ex.Message}")
        End Try
    End Sub

    'Private Async Function HandleGroupContactsFetch(jsonMessage As String) As Task
    '    Try
    '        Debug.WriteLine("HandleGroupContactsFetch called")
    '        Debug.WriteLine($"ForceStopFetching at start: {FrmGroupGrabber.ForceStopFetching}")

    '        Dim Participants = JsonConvert.DeserializeObject(Of FetchedGroups)(jsonMessage)
    '        If Participants Is Nothing OrElse Participants.GroupsContacts123 Is Nothing Then
    '            Debug.WriteLine("No participants found in response")
    '            Return
    '        End If

    '        Debug.WriteLine($"Found {Participants.GroupsContacts123.Count} participants to fetch")

    '        ' Update UI on the main thread
    '        Try
    '            FrmGroupGrabber.BeginInvoke(Sub()
    '                                            FrmGroupGrabber.Button1.Enabled = False
    '                                            FrmGroupGrabber.Button2.Enabled = True
    '                                            FrmGroupGrabber.ProgressBar1.Maximum = Participants.GroupsContacts123.Count
    '                                            FrmGroupGrabber.ProgressBar1.Value = 0
    '                                            FrmGroupGrabber.Panel2.Visible = True
    '                                            FrmGroupGrabber.Label1.Text = "Fetching contacts..."
    '                                            Debug.WriteLine("UI Updated: Panel2 visible, ProgressBar set")
    '                                        End Sub)
    '        Catch ex As Exception
    '            Debug.WriteLine($"Could not update UI (form may be closing): {ex.Message}")
    '            Return ' Exit if form is closing
    '        End Try

    '        Dim contactIndex As Integer = 0
    '        For Each contact In Participants.GroupsContacts123
    '            contactIndex += 1

    '            ' Check stop flag BEFORE processing each contact
    '            Debug.WriteLine($"Checking ForceStopFetching before contact {contactIndex}: {FrmGroupGrabber.ForceStopFetching}")

    '            If FrmGroupGrabber.ForceStopFetching Then
    '                Debug.WriteLine($">>> STOP DETECTED at contact {contactIndex}/{Participants.GroupsContacts123.Count} <<<")
    '                FrmGroupGrabber.ForceStopFetching = False ' Reset flag
    '                Exit For
    '            End If

    '            Debug.WriteLine($"Fetching contact {contactIndex}/{Participants.GroupsContacts123.Count}: {contact.id}")

    '            Dim number = Await GetJidFromLid(contact.id)

    '            If number <> "" Then
    '                Dim li As New ListViewItem With {
    '                    .Tag = number,
    '                    .Text = number
    '                }
    '                li.SubItems.Add(contact.id)

    '                ' Thread-safe UI update
    '                Try
    '                    FrmGroupGrabber.BeginInvoke(Sub()
    '                                                    FrmGroupGrabber.ListView2.Items.Insert(0, li)
    '                                                    FrmGroupGrabber.Label1.Text = $"Fetched:{FrmGroupGrabber.ListView2.Items.Count} / {FrmGroupGrabber.ProgressBar1.Maximum}"
    '                                                    If FrmGroupGrabber.ProgressBar1.Value < FrmGroupGrabber.ProgressBar1.Maximum Then
    '                                                        FrmGroupGrabber.ProgressBar1.Value += 1
    '                                                    End If
    '                                                End Sub)

    '                    Debug.WriteLine($"Contact {contactIndex} added to ListView: {number}")
    '                Catch ex As Exception
    '                    Debug.WriteLine($"Could not update ListView (form may be closing): {ex.Message}")
    '                    Exit For ' Exit loop if form is closing
    '                End Try
    '            Else
    '                Debug.WriteLine($"Contact {contactIndex} returned empty number")
    '            End If

    '            ' Check flag again after fetching
    '            Debug.WriteLine($"Checking ForceStopFetching after contact {contactIndex}: {FrmGroupGrabber.ForceStopFetching}")

    '            If FrmGroupGrabber.ForceStopFetching Then
    '                Debug.WriteLine($">>> STOP DETECTED after fetching contact {contactIndex} <<<")
    '                FrmGroupGrabber.ForceStopFetching = False
    '                Exit For
    '            End If
    '        Next

    '        Debug.WriteLine("HandleGroupContactsFetch loop completed, resetting UI")

    '        ' Clean up UI on the main thread
    '        Try
    '            FrmGroupGrabber.BeginInvoke(Sub()
    '                                            FrmGroupGrabber.Button1.Enabled = True
    '                                            FrmGroupGrabber.Button2.Enabled = False
    '                                            FrmGroupGrabber.Panel2.Visible = False
    '                                            Debug.WriteLine("UI Reset: Buttons enabled, Panel2 hidden")
    '                                        End Sub)
    '        Catch ex As Exception
    '            Debug.WriteLine($"Could not reset UI (form may be closed): {ex.Message}")
    '        End Try

    '    Catch ex As Exception
    '        Debug.WriteLine($"ERROR in HandleGroupContactsFetch: {ex.Message}")
    '        Debug.WriteLine($"Stack trace: {ex.StackTrace}")

    '        ' Ensure UI is reset even on error
    '        Try
    '            FrmGroupGrabber.BeginInvoke(Sub()
    '                                            FrmGroupGrabber.Button1.Enabled = True
    '                                            FrmGroupGrabber.Button2.Enabled = False
    '                                            FrmGroupGrabber.Panel2.Visible = False
    '                                        End Sub)
    '        Catch invEx As Exception
    '            Debug.WriteLine($"Error resetting UI in exception handler: {invEx.Message}")
    '        End Try
    '    End Try
    'End Function
    Private Async Function HandleGroupContactsFetch(jsonMessage As String) As Task
        Try
            ' Check stop flag at the very start
            If FrmGroupGrabber.ForceStopFetching Then
                Debug.WriteLine(">>> STOP FLAG ALREADY SET - Skipping this group <<<")
                Return
            End If

            Debug.WriteLine("HandleGroupContactsFetch called")

            Dim Participants = JsonConvert.DeserializeObject(Of FetchedGroups)(jsonMessage)
            If Participants Is Nothing OrElse Participants.GroupsContacts123 Is Nothing Then
                Debug.WriteLine("No participants found in response")
                Return
            End If

            Debug.WriteLine($"Found {Participants.GroupsContacts123.Count} participants to fetch")

            ' Update UI on the main thread
            Try
                FrmGroupGrabber.BeginInvoke(Sub()
                                                FrmGroupGrabber.Button1.Enabled = False
                                                FrmGroupGrabber.Button2.Enabled = True
                                                FrmGroupGrabber.ProgressBar1.Maximum = Participants.GroupsContacts123.Count
                                                FrmGroupGrabber.ProgressBar1.Value = 0
                                                FrmGroupGrabber.Panel2.Visible = True
                                                FrmGroupGrabber.Label1.Text = "Fetching contacts..."
                                            End Sub)
            Catch ex As Exception
                Debug.WriteLine($"Could not update UI (form may be closing): {ex.Message}")
                Return
            End Try

            Dim contactIndex As Integer = 0
            For Each contact In Participants.GroupsContacts123
                contactIndex += 1

                ' Check stop flag BEFORE processing each contact
                If FrmGroupGrabber.ForceStopFetching Then
                    Debug.WriteLine($">>> STOP DETECTED at contact {contactIndex}/{Participants.GroupsContacts123.Count} <<<")
                    ' ✅ DON'T RESET THE FLAG HERE - let Button2 or form closing handle it
                    Exit For
                End If

                Debug.WriteLine($"Fetching contact {contactIndex}/{Participants.GroupsContacts123.Count}: {contact.id}")

                Dim number = Await GetJidFromLid(contact.id)

                If number <> "" Then
                    Dim li As New ListViewItem With {
                    .Tag = number,
                    .Text = number
                }
                    li.SubItems.Add(contact.id)

                    Try
                        FrmGroupGrabber.BeginInvoke(Sub()
                                                        FrmGroupGrabber.ListView2.Items.Insert(0, li)
                                                        FrmGroupGrabber.Label1.Text = $"Fetched:{FrmGroupGrabber.ListView2.Items.Count} / {FrmGroupGrabber.ProgressBar1.Maximum}"
                                                        If FrmGroupGrabber.ProgressBar1.Value < FrmGroupGrabber.ProgressBar1.Maximum Then
                                                            FrmGroupGrabber.ProgressBar1.Value += 1
                                                        End If
                                                    End Sub)
                    Catch ex As Exception
                        Debug.WriteLine($"Could not update ListView (form may be closing): {ex.Message}")
                        Exit For
                    End Try
                End If

                ' Check flag again after fetching
                If FrmGroupGrabber.ForceStopFetching Then
                    Debug.WriteLine($">>> STOP DETECTED after fetching contact {contactIndex} <<<")
                    Exit For
                End If
            Next

            Debug.WriteLine("HandleGroupContactsFetch loop completed, resetting UI")

            ' Clean up UI
            Try
                FrmGroupGrabber.BeginInvoke(Sub()
                                                FrmGroupGrabber.Button1.Enabled = True
                                                FrmGroupGrabber.Button2.Enabled = False
                                                FrmGroupGrabber.Panel2.Visible = False
                                            End Sub)
            Catch ex As Exception
                Debug.WriteLine($"Could not reset UI (form may be closed): {ex.Message}")
            End Try

        Catch ex As Exception
            Debug.WriteLine($"ERROR in HandleGroupContactsFetch: {ex.Message}")

            Try
                FrmGroupGrabber.BeginInvoke(Sub()
                                                FrmGroupGrabber.Button1.Enabled = True
                                                FrmGroupGrabber.Button2.Enabled = False
                                                FrmGroupGrabber.Panel2.Visible = False
                                            End Sub)
            Catch invEx As Exception
                Debug.WriteLine($"Error resetting UI in exception handler: {invEx.Message}")
            End Try
        End Try
    End Function
    Private Async Function HandleInputMessages(jsonMessage As String) As Task
        Try
            Dim RequestIDs = JsonConvert.DeserializeObject(Of ReceivedMessage2)(jsonMessage)
            If RequestIDs Is Nothing Then Return

            For Each RequestID In RequestIDs.inputMessage
                If RequestID IsNot Nothing AndAlso RequestID.lastReceivedKey IsNot Nothing Then
                    If Not IsReceivedKeyExsist(RequestID.lastReceivedKey._serialized) Then
                        AddMessageKey(RequestID.lastReceivedKey._serialized)
                        Console.WriteLine(RequestID.lastReceivedKey._serialized)
                        Await WebView2.ExecuteScriptAsync("WAPI.getMessageById('" & RequestID.lastReceivedKey._serialized & "').then(e=>{e.actionType='ReceivedMessage'; window.chrome.webview.postMessage(e)})")
                    End If
                End If
            Next
        Catch ex As Exception
            Debug.WriteLine($"Error handling input messages: {ex.Message}")
        End Try
    End Function

    Private Sub HandleSentStatus(jsonMessage As String)
        Try
            Dim ReceivedStatus = JsonConvert.DeserializeObject(Of MessageSentStatusModel)(jsonMessage)
            If ReceivedStatus Is Nothing Then Return

            Dim li As New ListViewItem With {.Tag = ReceivedStatus.id}

            Dim idLower As String = ReceivedStatus.id.ToLower()
            Dim id As String = idLower.Replace("@c.us", "").Replace("@g.us", "").Replace("@lid", "")
            li.Text = id
            li.SubItems.Add("📨 Message")   ' Kind column
            li.SubItems.Add(If(idLower.Contains("@c.us") OrElse idLower.Contains("@lid"), "Contact", "Group"))
            li.SubItems.Add(Now.ToString("yyyy-MM-dd HH:mm:ss"))
            li.SubItems.Add(If(ReceivedStatus.sentStatus, "Sent", "Failed"))
            li.SubItems.Add(ReceivedStatus.message)
            li.ImageIndex = If(ReceivedStatus.sentStatus, 0, 1)
            li.ForeColor = If(ReceivedStatus.sentStatus, Color.Green, Color.Red)

            If ReceivedStatus.sentStatus Then
                FrmSending.SuccessMessages += 1
            Else
                FrmSending.FailedMessages += 1
            End If

            AddListViewItemThreadSafe(_sendingListView, li)
        Catch ex As Exception
            Debug.WriteLine($"Error handling sent status: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Logs a file send entry directly to FrmSending's ListView (blue row)
    ''' </summary>
    Private Sub LogFileSentToUI(destination As String, fileName As String)
        Try
            Dim idLower As String = destination.ToLower()
            Dim id As String = idLower.Replace("@c.us", "").Replace("@g.us", "").Replace("@lid", "")

            Dim li As New ListViewItem With {.Tag = destination, .Text = id}
            li.SubItems.Add("📎 File")      ' Kind column
            li.SubItems.Add(If(idLower.Contains("@c.us") OrElse idLower.Contains("@lid"), "Contact", "Group"))
            li.SubItems.Add(Now.ToString("yyyy-MM-dd HH:mm:ss"))
            li.SubItems.Add("Sent")
            li.SubItems.Add(fileName)
            li.ImageIndex = 0
            li.ForeColor = Color.DodgerBlue

            AddListViewItemThreadSafe(_sendingListView, li)
        Catch ex As Exception
            Debug.WriteLine($"Error logging file send: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Processes incoming WhatsApp messages with ChatGPT, auto-reply, and rule-based responses
    ''' </summary>
    Public Async Sub ReceivedMessage(ByVal json As String)
        Try
            ' 1) Deserialize incoming JSON into object model
            Dim root As RootReceivedObj = JsonConvert.DeserializeObject(Of RootReceivedObj)(json)
            If root Is Nothing OrElse root.command Is Nothing Then Exit Sub

            Dim msg = root.command
            Dim msgId = msg.id

            ' 2) Basic filtering: ignore invalid / outgoing / non-user messages
            If msgId Is Nothing OrElse msgId.fromMe Then Exit Sub

            Dim remoteJid As String = msgId.remote
            If String.IsNullOrWhiteSpace(remoteJid) Then Exit Sub
            Dim remoteJidLower As String = remoteJid.ToLowerInvariant()
            If Not (remoteJidLower.Contains("@c.us") OrElse remoteJidLower.Contains("@lid")) Then Exit Sub

            Dim body As String = msg.body
            If String.IsNullOrWhiteSpace(body) OrElse body.Length >= MAX_MESSAGE_BODY_LENGTH Then Exit Sub

            ' 3) Log message (with memory management)
            LogReceivedMessage(json)

            Dim remoteNumber As String = remoteJid.ToLowerInvariant().Replace("@c.us", "").Replace("@lid", "")

            ' Hook for Database Sync Inbox
            Try
                FrmDatabaseSync.RecordInbox(remoteNumber, body)
            Catch ex As Exception
            End Try

            ' 4) Add message to ListView controls in UI (thread-safe)
            AddMessageToUI(remoteJid, remoteNumber, body)

            ' 5) Try ChatGPT auto-reply (if enabled)
            Dim chatGPTHandled As Boolean = False
            Try
                If FrmMain.EnableChatGPTReplyToolStripMenuItem.Checked Then
                    chatGPTHandled = Await ProcessChatGPTReply(remoteJid, body)
                End If
            Catch ex As Exception
                Debug.WriteLine($"ChatGPT processing error: {ex.Message}")
                ' Continue to fallback methods instead of exiting
            End Try

            ' If ChatGPT handled it successfully, stop here
            If chatGPTHandled Then Exit Sub

            ' 6) Auto reply from autoreply.json
            Dim autoReplyHandled As Boolean = ProcessAutoReply(remoteJid)

            ' 7) Apply rule-based replies from rules.json (only if auto-reply didn't handle it)
            If Not autoReplyHandled Then
                ProcessRuleBasedReplies(remoteJid, body)
            End If

        Catch ex As Exception
            Debug.WriteLine($"Error in ReceivedMessage: {ex.Message}")
        End Try
    End Sub

    Private Sub LogReceivedMessage(json As String)
        ' Prevent unbounded memory growth - limit log size
        If allReceivedMessage.Length > 1000000 Then ' 1MB limit
            allReceivedMessage = allReceivedMessage.Substring(allReceivedMessage.Length \ 2)
        End If
        allReceivedMessage &= json & vbNewLine & vbNewLine
    End Sub

    Private Sub AddMessageToUI(remoteJid As String, remoteNumber As String, body As String)
        Dim li As New ListViewItem With {
            .Tag = remoteJid,
            .Text = Now.ToString()
        }
        li.SubItems.Add(remoteNumber)
        li.SubItems.Add(body)

        Dim li2 As New ListViewItem With {
            .Tag = remoteJid,
            .Text = Now.ToString()
        }
        li2.SubItems.Add(remoteNumber)
        li2.SubItems.Add(body)

        AddListViewItemThreadSafe(FrmMain.ListReceivedMessages, li)
        AddListViewItemThreadSafe(FrmReceived.ListView1, li2)
    End Sub

    Private Async Function ProcessChatGPTReply(remoteJid As String, body As String) As Task(Of Boolean)
        GPTAPI = New OpenAI_API.OpenAIAPI(CurrentGPTSetting.APIKey)

        Dim chat = GPTAPI.Chat.CreateConversation()
        chat.Model = Model.GPT4_Turbo

        Dim languagePart As String =
            If(Not String.IsNullOrWhiteSpace(CurrentGPTSetting.Language),
               " in " & CurrentGPTSetting.Language & " Language ",
               String.Empty)

        chat.AppendSystemMessage(CurrentGPTSetting.SystemMessage &
                                 " give a good short answers " & languagePart)

        If CurrentGPTSetting.Prompts IsNot Nothing Then
            For Each prompt In CurrentGPTSetting.Prompts
                chat.AppendUserInput(prompt.Question)
                chat.AppendExampleChatbotOutput(prompt.Answer)
            Next
        End If

        chat.AppendUserInput(body)

        Dim response As String = Await chat.GetResponseFromChatbotAsync()
        SendReplyMessage(remoteJid, response, Nothing)

        Return True ' Indicate successful handling
    End Function

    Private Function ProcessAutoReply(remoteJid As String) As Boolean
        Try
            Dim autoReplyPath As String = IO.Path.Combine(ClsSpecialDirectories.Getdata, "autoreply.json")
            If Not IO.File.Exists(autoReplyPath) Then Return False

            Dim autoJson = IO.File.ReadAllText(autoReplyPath)
            Dim autoReply As ClsAutoReplyMessage = JsonConvert.DeserializeObject(Of ClsAutoReplyMessage)(autoJson)

            If autoReply IsNot Nothing AndAlso Not IsAutoReplied(remoteJid) Then
                AddAutoReplyAccount(remoteJid)
                SendReplyMessage(remoteJid, autoReply.Message, autoReply.Attachment, autoReply.ButtonFile)

                If autoReply.isButton Then
                    SendButton(remoteJid, autoReply.ButtonText, autoReply.ButtonObject)
                End If

                Return True
            End If
        Catch ex As Exception
            Debug.WriteLine($"Error processing auto-reply: {ex.Message}")
        End Try

        Return False
    End Function

    Private Sub ProcessRuleBasedReplies(remoteJid As String, body As String)
        Try
            Dim rulesPath As String = IO.Path.Combine(ClsSpecialDirectories.Getdata, "rules.json")
            If Not IO.File.Exists(rulesPath) Then Return

            Dim rulesJson = IO.File.ReadAllText(rulesPath)
            Dim rules As List(Of ClsRuleModel) = JsonConvert.DeserializeObject(Of List(Of ClsRuleModel))(rulesJson)
            If rules Is Nothing Then Return

            Dim bodyLower = body.Trim().ToLowerInvariant()

            For Each rule In rules
                ' Skip disabled rules
                If Not rule.RuleStatus Then Continue For

                Dim keywordLower = rule.RuleKeyword.Trim().ToLowerInvariant()
                Dim isMatch As Boolean = False

                Select Case rule.Operand
                    Case "="
                        isMatch = (rule.RuleKeyword = body.Trim())
                    Case "Like"
                        isMatch = (keywordLower = bodyLower)
                    Case "Start with"
                        isMatch = bodyLower.StartsWith(keywordLower)
                    Case "End with"
                        isMatch = bodyLower.EndsWith(keywordLower)
                    Case "Contains"
                        isMatch = bodyLower.Contains(keywordLower)
                End Select

                If isMatch Then
                    SendReplyMessage(remoteJid, rule.RuleMessage, rule.Attachment, rule.ButtonsFile)
                End If
            Next
        Catch ex As Exception
            Debug.WriteLine($"Error processing rule-based replies: {ex.Message}")
        End Try
    End Sub

    Private Sub AddListViewItemThreadSafe(listView As ListView, item As ListViewItem)
        If listView Is Nothing OrElse listView.IsDisposed Then Return
        Try
            If listView.InvokeRequired Then
                listView.BeginInvoke(New Action(Sub()
                    If Not listView.IsDisposed Then
                        listView.Items.Insert(0, item)
                    End If
                End Sub))
            Else
                listView.Items.Insert(0, item)
            End If
        Catch ex As Exception
            Debug.WriteLine($"AddListViewItemThreadSafe error: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Message Sending"

    Public Sub SendMessage(ByVal WhatsAppAccount As String, ByVal Message As String, ByVal IsSafe As Boolean, Optional Buttons As String = "")
        If String.IsNullOrWhiteSpace(Message) Then Return   ' never send empty/whitespace messages

        If IsDemoMode Then
            Message = Message & vbNewLine & DemoMessage
        End If

        Message = SafeJavaScript(Message)

        Try
            WebView2.BeginInvoke(Sub()
                                     WebView2.ExecuteScriptAsync($"botmaster.sendMessageto('{WhatsAppAccount}','{Message}',{IsSafe.ToString.ToLower})")
                                 End Sub)
        Catch ex As Exception
            Debug.WriteLine($"Error sending message: {ex.Message}")
        End Try
    End Sub

    Public Sub SendButton(ByVal WhatsAppAccount As String, ByVal Message As String, ByVal buttons As String)
        Try
            WebView2.BeginInvoke(Sub()
                                     WebView2.ExecuteScriptAsync("WPP.chat.sendTextMessage('" & WhatsAppAccount & "', '" & Message & "'," & buttons & ");")
                                 End Sub)
        Catch ex As Exception
            Debug.WriteLine($"Error sending button: {ex.Message}")
        End Try
    End Sub

    Public Sub SendReplyMessage(ByVal ID As String, ByVal Message As String, ByVal Attachment As List(Of ClsAttachment), Optional Buttonfile As String = "")
        Try
            If IsDemoMode AndAlso Not String.IsNullOrWhiteSpace(Message) Then
                Message = Message & vbNewLine & DemoMessage
            End If

            ' Only send text message if there is actual content
            If Not String.IsNullOrWhiteSpace(Message) Then
                SendMessage(ID, Message, False)
            End If

            If Attachment IsNot Nothing Then
                For Each attach As ClsAttachment In Attachment
                    Application.DoEvents()

                    Dim Caption As String = attach.Caption
                    If IsDemoMode Then
                        Caption = Caption & vbNewLine & DemoMessage
                    End If

                    Caption = SafeJavaScript(Caption)

                    If attach.MediaType = "Catalog" Then
                        SendCatalog(ID, attach.File)
                    Else
                        SendFile(ID, attach.File, attach.Caption, False)
                    End If

                    System.Threading.Thread.Sleep(ATTACHMENT_DELAY_MS)
                Next
            End If
        Catch ex As Exception
            Debug.WriteLine($"Error sending reply message: {ex.Message}")
        End Try
    End Sub

    Public Sub SendFile(ByVal WhatsAppAccount As String, ByVal File As String, ByVal Caption As String, ByVal IsSafe As Boolean)
        If IsDemoMode Then
            Caption = Caption & vbNewLine & DemoMessage
        End If

        Try
            Dim Base64converter As New ClsBase64
            Dim Base64File As String = Base64converter.ConvertFileToBase64(File)
            Dim FileName As String = IO.Path.GetFileName(File)

            If FileName.ToLower.EndsWith(".ogg") Then
                SendVoiceNote(WhatsAppAccount, Base64File, FileName, Caption, IsSafe)
            Else
                SendRegularFile(WhatsAppAccount, Base64File, FileName, Caption, IsSafe)
            End If
        Catch ex As Exception
            Debug.WriteLine($"Error sending file: {ex.Message}")
        End Try
    End Sub

    Public Sub SendVoiceNote(WhatsAppAccount As String, Base64File As String, FileName As String, Caption As String, IsSafe As Boolean)
        Dim js As String = "function sendPTT(base64,id,isSafe){
                                if (isSafe==true){
                                     WAPI.getchatId(id).then(e=>{ 
                                        console.log(e);
                                        if(e){
                                               WPP.chat.sendFileMessage(
                                                id,
                                                base64,
                                                {
                                                    type: 'audio',
                                                    isPtt: true 
                                                }
                                              );

                                        }else{
                                            console.log('error');
                                            
                                        }
                                    })

                                }else{
                                    WPP.chat.sendFileMessage(
                                                id,
                                                base64,
                                                {
                                                    type: 'audio',
                                                    isPtt: true 
                                                }
                                              );
                                }
                            };  sendPTT('{base64}','{id}');"

        js = js.Replace("{base64}", Base64File)
        js = js.Replace("{id}", WhatsAppAccount)
        js = js.Replace("{caption}", SafeJavaScript(Caption))
        js = js.Replace("{filename}", FileName)
        js = js.Replace("{isSafe}", IsSafe.ToString.ToLower)

        WebView2.BeginInvoke(Sub()
                                 WebView2.ExecuteScriptAsync(js)
                             End Sub)
    End Sub

    Public Sub SendRegularFile(WhatsAppAccount As String, Base64File As String, FileName As String, Caption As String, IsSafe As Boolean)
        Dim js As String = "function sendFile(base64,id,filename,caption,isSafe){
                                if (isSafe==true){
                                     WAPI.getchatId(id).then(e=>{ 
                                        console.log(e);
                                        if(e){
                                             WPP.chat.sendFileMessage(
                                                    id,
                                                    base64,
                                                    {
                                                      type: getFileType(filename), 
                                                      caption: caption,
                                                      filename: filename,
                                                    }
                                                  );

                                        }else{
                                            console.log('error');
                                            
                                        }
                                    })

                                }else{
                                      WPP.chat.sendFileMessage(
                                                    id,
                                                    base64,
                                                    {
                                                      type: getFileType(filename), 
                                                      caption: caption,
                                                      filename: filename,
                                                    }
                                                  );
                                }
                            };  sendFile('{base64}','{id}','{filename}','{caption}','{isSafe}');"

        js = js.Replace("{base64}", Base64File)
        js = js.Replace("{id}", WhatsAppAccount)
        js = js.Replace("{caption}", SafeJavaScript(Caption))
        js = js.Replace("{filename}", FileName)
        js = js.Replace("{isSafe}", IsSafe.ToString.ToLower)

        WebView2.BeginInvoke(Sub()
                                 WebView2.ExecuteScriptAsync(js)
                             End Sub)
    End Sub

    Public Sub SendCatalog(ByVal Number As String, ByVal CatalogFileName As String)
        Try
            If String.IsNullOrEmpty(CatalogFileName) Then Return

            Dim JSONList = IO.File.ReadAllText(CatalogFileName)
            Debug.WriteLine($"WPP.chat.sendListMessage('{Number}',JSON.parse('{JSONList}'));")

            WebView2.BeginInvoke(Sub()
                                     WebView2.ExecuteScriptAsync($"WPP.chat.sendListMessage('{Number}',JSON.parse('{JSONList}'));")
                                 End Sub)
        Catch ex As Exception
            Debug.WriteLine($"Error sending catalog: {ex.Message}")
        End Try
    End Sub

    Public Sub OfferCall(ByVal Number As String)
        Task.Run(Sub()
                     Try
                         WebView2.BeginInvoke(Sub()
                                                  WebView2.ExecuteScriptAsync($"WPP.call.offer('{Number}');")
                                              End Sub)
                     Catch ex As Exception
                         Debug.WriteLine($"Error offering call: {ex.Message}")
                     End Try
                 End Sub)
    End Sub

#End Region

#Region "Bulk Sending Campaign"

    Public Sub StartBulk(ByVal Destinations As List(Of DestinationModel),
                         ByVal Messages As List(Of String),
                         ByVal Attachments As List(Of AttachmentModel),
                         ByVal SafeMode As Boolean,
                         Optional IsButtons As Boolean = False,
                         Optional Buttons As String = "",
                         Optional WithCatalog As Boolean = False,
                         Optional Catalog As String = "",
                         Optional ButtonBody As String = "")

        _Destinations = Destinations
        _Messages = Messages
        _SafeMode = SafeMode
        _attachments = Attachments
        ButtonText = ButtonBody
        _withCatalog = WithCatalog
        IsBtn = IsButtons
        _buttons = If(IsButtons, Buttons, String.Empty)

        ' Capture the ListView reference here on the UI thread (safe cross-thread hand-off)
        _sendingListView = FrmSending.ListViewNumbers

        Task.Run(Sub() doStartBulk())
    End Sub

    Private Sub doStartBulk()
        Try
            Dim MessageToSend As String
            Dim SendingCounter As Integer = 0

            Status.IsCampainStatred = True
            Status.IsCampaignEnd = False
            Dim StartDate As String = Now.ToString("dd-MM-yyyy HH:mm:ss")
            Status.StatusString = "Campaign Started"

            For Each Destination In _Destinations
                ' Check for force stop
                If Status.ForceClose Then
                    Status.ForceClose = False
                    Exit Sub
                End If

                ' Wait for login
                Do
                    Application.DoEvents()
                Loop Until IsWAPILoggedIn()

                ' Handle pause
                Do While Status.IsCampainPaused
                    Application.DoEvents()
                    Status.StatusString = "Campaign is paused"
                Loop

                ' Select random message
                Randomize()
                MessageToSend = If(_Messages.Count > 0,
                                  _Messages(Int(Rnd() * _Messages.Count)),
                                  String.Empty)

                ' Apply variables
                MessageToSend = ApplyVariables(MessageToSend, Destination.Fullname,
                                               Destination.FirstName, Destination.LastName,
                                               Destination.Var1, Destination.Var2, Destination.Var3,
                                               Destination.Var4, Destination.Var5)

                Status.StatusString = $"Sending message to:{Destination.WhatsAppID}"

                ' Send message (only if there is actual text to send)
                If Not String.IsNullOrWhiteSpace(MessageToSend) Then
                    SendMessage(Destination.WhatsAppID, MessageToSend, _SafeMode, _buttons)

                    If IsBtn Then
                        SendButton(Destination.WhatsAppID, ButtonText, _buttons)
                    End If

                    System.Threading.Thread.Sleep(1000)
                End If

                ' Send attachments
                For Each attachment In _attachments
                    Dim caption As String = ApplyVariables(attachment.Caption, Destination.Fullname,
                                                          Destination.FirstName, Destination.LastName,
                                                          Destination.Var1, Destination.Var2, Destination.Var3,
                                                          Destination.Var4, Destination.Var5)

                    If attachment.Type = "Catalog" Then
                        SendCatalog(Destination.WhatsAppID, attachment.FileName)
                    Else
                        SendFile(Destination.WhatsAppID, attachment.FileName, caption, _SafeMode)
                    End If

                    ' Log file send to FrmSending
                    LogFileSentToUI(Destination.WhatsAppID, IO.Path.GetFileName(attachment.FileName))

                    System.Threading.Thread.Sleep(1000)
                Next

                ' Update progress
                Status.TotalProgress = SendingCounter
                Status.TotalCount = _Destinations.Count



                ' Apply delay
                Thread.Sleep(GetDelay())


                ' Engagement/Dialog sending
                ProcessEngagementDialog(SendingCounter)

                ' Resting period
                ProcessRestingPeriod(SendingCounter)

                Status.StatusString = ""
                SendingCounter += 1
            Next

            ' Campaign complete
            Dim EndDate As String = Now.ToString("dd-MM-yyyy HH:mm:ss")
            Status.IsCampainStatred = False
            Status.IsCampaignEnd = True
            Status.EndCampMessage = $"Your Campaign has been done start at:{StartDate} and finish at:{EndDate}"

        Catch ex As Exception
            Debug.WriteLine($"Error in bulk sending: {ex.Message}")
            Status.IsCampainStatred = False
        End Try
    End Sub

    Private Sub ProcessEngagementDialog(SendingCounter As Integer)
        Try
            If Not CBool(GetSetting(ApplicationTitle, "SendingConfig", "ActivateDialog", "false")) Then Return

            Dim dialogAfter As Integer = Val(GetSetting(ApplicationTitle, "SendingConfig", "DialogAfter", "5"))
            If dialogAfter = 0 Then Return ' Prevent division by zero

            If SendingCounter Mod (dialogAfter + 1) = 0 AndAlso SendingCounter > 0 Then
                Status.StatusString = "Engagement start (Familiar sending)"

                Dim commonListPath = ClsSpecialDirectories.Getdata & "commonList.data"
                Dim commonMsgPath = ClsSpecialDirectories.Getdata & "commonMessage.data"

                If Not IO.File.Exists(commonListPath) OrElse Not IO.File.Exists(commonMsgPath) Then Return

                Dim FamiliarDestination As String() = IO.File.ReadAllLines(commonListPath)
                Dim dialogCount As Integer = Val(GetSetting(ApplicationTitle, "SendingConfig", "DialoCount", "15"))
                Dim FamiliarLimits As Integer = Math.Min(FamiliarDestination.Count, dialogCount)

                Dim FamiliarMessages As String() = IO.File.ReadAllLines(commonMsgPath)

                For i = 0 To FamiliarLimits - 1
                    Randomize()
                    SendReplyMessage(FamiliarDestination(i) & "@c.us",
                                    FamiliarMessages(Int(Rnd() * FamiliarMessages.Count)),
                                    Nothing)
                    Thread.Sleep(Val(GetSetting(ApplicationTitle, "SendingConfig", "DialogWait", "1")) * 1000)
                Next
            End If
        Catch ex As Exception
            Debug.WriteLine($"Error in engagement dialog: {ex.Message}")
        End Try
    End Sub

    Private Sub ProcessRestingPeriod(SendingCounter As Integer)
        Try
            If Not CBool(GetSetting(ApplicationTitle, "SendingConfig", "ActivateSleep", "false")) Then Return

            Dim sleepAfter As Integer = Val(GetSetting(ApplicationTitle, "SendingConfig", "SleepAfter", "20"))
            If sleepAfter = 0 Then Return ' Prevent division by zero

            If SendingCounter Mod sleepAfter = 0 AndAlso SendingCounter > 0 Then
                Dim sleepDuration = Val(GetSetting(ApplicationTitle, "SendingConfig", "SleepFor", "5"))

                Status.IsResting = True
                Status.StatusString = $"Campaign is resting for {sleepDuration} seconds"
                BulkIsResting = True

                Thread.Sleep(sleepDuration * 1000)

                BulkIsResting = False
                Status.IsResting = False
            End If
        Catch ex As Exception
            Debug.WriteLine($"Error in resting period: {ex.Message}")
        End Try
    End Sub

    Public Sub StopBulk()
        Status.ForceClose = True
    End Sub

#End Region

#Region "Variable & Text Processing"

    Public Function ApplyVariables(ByVal VariableText As String,
                                   Optional fullName As String = "",
                                   Optional FirstName As String = "",
                                   Optional LastName As String = "",
                                   Optional Var1 As String = "",
                                   Optional Var2 As String = "",
                                   Optional Var3 As String = "",
                                   Optional Var4 As String = "",
                                   Optional Var5 As String = "") As String

        VariableText = VariableText.Replace("[[fullname]]", fullName)
        VariableText = VariableText.Replace("[[firstname]]", FirstName)
        VariableText = VariableText.Replace("[[lastname]]", LastName)
        VariableText = VariableText.Replace("[[VAR1]]", Var1)
        VariableText = VariableText.Replace("[[VAR2]]", Var2)
        VariableText = VariableText.Replace("[[VAR3]]", Var3)
        VariableText = VariableText.Replace("[[VAR4]]", Var4)
        VariableText = VariableText.Replace("[[VAR5]]", Var5)
        VariableText = VariableText.Replace("[[randomtag]]", "#" & (Int(Rnd() * 1000000) + 1000000))

        VariableText = ApplySpinText(VariableText)

        Return VariableText
    End Function

    Private Function ApplySpinText(ByVal Text As String) As String
        Dim _text As String = Text
        Dim SpinTextDictionary As New List(Of DictionaryEntry)

        _text = _text.Replace("{{", "||{{")
        _text = _text.Replace("}}", "}}||")

        Dim SpintextArr() As String = Split(_text, "||")

        For Each Spintext As String In SpintextArr
            If Spintext.Trim.StartsWith("{{") AndAlso Spintext.Trim.EndsWith("}}") Then
                Dim cSpin As String = Spintext.Replace("{{", "").Replace("}}", "")
                Dim rWords() As String = cSpin.Split("|"c)

                If rWords.Length > 0 Then
                    Randomize()
                    Dim selector As Integer = 0

                    ' Multiple random iterations for better randomization
                    For i As Integer = 0 To 30
                        selector = Int(Rnd() * rWords.Length)
                    Next

                    Dim dicEntry As New DictionaryEntry(Spintext, rWords(selector))
                    SpinTextDictionary.Add(dicEntry)
                End If
            End If
        Next

        Dim Result As String = Text
        For Each dicEntry In SpinTextDictionary
            Result = Result.Replace(dicEntry.Key, dicEntry.Value)
        Next

        Return Result
    End Function

    Private Function GetDelay() As Integer
        Dim Num1 As Integer = Val(GetSetting(ApplicationTitle, "SendingConfig", "DelayStart", "0"))
        Dim Num2 As Integer = Val(GetSetting(ApplicationTitle, "SendingConfig", "DelayEnd", "2"))

        Randomize()

        If Num2 > 0 Then
            Return (Num1 + (Int(Rnd() * Num2))) * 1000
        Else
            Return 100
        End If
    End Function

#End Region

#Region "WhatsApp API Queries"

    Public Async Function CheckWhatsAppAccount(ByVal id As String) As Task
        Try
            Await WebView2.ExecuteScriptAsync($"WPP.contact.queryExists('{id}').then(e=>{{ var checkResult={{id : '{id}' , checkedResult : e }};  window.chrome.webview.postMessage(checkResult)}})")
        Catch ex As Exception
            Debug.WriteLine($"Error checking WhatsApp account: {ex.Message}")
        End Try
    End Function

    Public Async Sub getGroupParticipantIDs(ByVal id As String)
        Try
            Await WebView2.ExecuteScriptAsync("WPP.group.getParticipants('" & id & "').then(e=>{var GroupsContact = {GroupsContacts123:e}; window.chrome.webview.postMessage(GroupsContact) })")
        Catch ex As Exception
            Debug.WriteLine($"Error getting group participants: {ex.Message}")
        End Try
    End Sub

    Public Async Function GetJidFromLid(ByVal lid As String) As Task(Of String)
        Try
            Await WebView2.ExecuteScriptAsync($"WPP.chat.openChatBottom('{lid}');")
            Await Task.Delay(CHAT_OPEN_DELAY_MS)

            Dim Number = Await WebView2.ExecuteScriptAsync(My.Resources.GetPhoneNumber)
            If Number.ToString.Contains("null") Then Return String.Empty

            Return Number.Replace("""", "")
        Catch ex As Exception
            Debug.WriteLine($"Error getting JID from LID: {ex.Message}")
            Return String.Empty
        End Try
    End Function

    Public Async Function GetAllContact() As Task(Of List(Of whatsAppContactModel))
        Try
            Dim ContactResult = Await WebView2.ExecuteScriptAsync("WPP.whatsapp.ContactStore.toJSON();")
            Return JsonConvert.DeserializeObject(Of List(Of whatsAppContactModel))(ContactResult)
        Catch ex As Exception
            Debug.WriteLine($"Error getting all contacts: {ex.Message}")
            Return Nothing
        End Try
    End Function

    Public Async Function GetAllGroups() As Task(Of List(Of WhatsAppGroupModel))
        Try
            Dim groupResult = Await WebView2.ExecuteScriptAsync("var t=[];for(let c of WAPI.getAllGroups()){t.push({id:c.id._serialized,name:c.name})};t;")
            Return JsonConvert.DeserializeObject(Of List(Of WhatsAppGroupModel))(groupResult)
        Catch ex As Exception
            Debug.WriteLine($"Error getting all groups: {ex.Message}")
            Return Nothing
        End Try
    End Function

    Public Async Function GetMobileNumbers() As Task(Of List(Of String))
        Dim js As String = "
                (() => {
                  const spans = Array.from(document.querySelectorAll('span[title]'));
                  const results = new Set();
                  const phoneRegex = /\+?\s*(?:\(?\d{1,4}\)?[\s-]*){2,}\d{2,}/g;
                  function normalizePhone(s){ const hasPlus=/^\s*\+/.test(s); const digits=s.replace(/[^\d]/g,''); return (hasPlus?'+':'')+digits; }
                  function isValidPhone(s){
                    const plusCount=(s.match(/\+/g)||[]).length; if(plusCount>1) return false;
                    const openPar=(s.match(/\(/g)||[]).length, closePar=(s.match(/\)/g)||[]).length; if(openPar!==closePar) return false;
                    const digitCount=(s.match(/\d/g)||[]).length; if(digitCount<7 || digitCount>15) return false;
                    return true;
                  }
                  for (const el of spans) {
                    const t = el.getAttribute('title') || '';
                    if (!/\d/.test(t)) continue;
                    const matches = t.match(phoneRegex) || [];
                    for (let m of matches) {
                      m = m.trim();
                      if (!isValidPhone(m)) continue;
                      results.add(normalizePhone(m));
                    }
                  }
                  return Array.from(results);
                })();"

        Try
            Dim raw As String = Await WebView2.ExecuteScriptAsync(js)
            Dim phones As List(Of String) = Nothing

            Try
                phones = JsonConvert.DeserializeObject(Of List(Of String))(raw)
            Catch
                Dim inner As String = JsonConvert.DeserializeObject(Of String)(raw)
                phones = JsonConvert.DeserializeObject(Of List(Of String))(inner)
            End Try

            Return If(phones, New List(Of String))
        Catch ex As Exception
            Debug.WriteLine($"Error getting mobile numbers: {ex.Message}")
            Return New List(Of String)
        End Try
    End Function

    Public Async Sub OpenGroup(ByVal ID As String)
        Try
            Await WebView2.ExecuteScriptAsync($"WPP.chat.openChatBottom('{ID}');")
        Catch ex As Exception
            Debug.WriteLine($"Error opening group: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Filter List Updates"

    Private Sub UpdateFilterList(ByVal Result As CheckNumResponseModel)
        Try
            Debug.WriteLine("Checking " & Result.id)

            For Each li As ListViewItem In FrmFilter.ListViewNumbers.Items
                If li.SubItems(2).Text.Contains("Requested") Then
                    FrmFilter.SetStatus()

                    If FrmFilter.ProgressBar2.Value < FrmFilter.ProgressBar2.Maximum Then
                        FrmFilter.ProgressBar2.Value += 1
                    End If

                    If li.Tag.ToString.ToLower = Result.id Then
                        If Result.checkedResult IsNot Nothing Then
                            li.SubItems(2).Text = "OK"
                            li.ForeColor = Color.Green
                            li.SubItems(3).Text = If(Result.checkedResult.biz, "Business", "Regular")
                        Else
                            li.SubItems(2).Text = "Not Found"
                            li.ForeColor = Color.Red
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            Debug.WriteLine($"Error updating filter list: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "File Operations - Thread-Safe"

    Public Sub AddAutoReplyAccount(ByVal Account As String)
        SyncLock autoReplyLock
            Try
                Using sw As New IO.StreamWriter(ClsSpecialDirectories.Getdata & "autoreplyusers.key", True)
                    sw.WriteLine(Account)
                End Using
            Catch ex As Exception
                Debug.WriteLine($"Error adding auto-reply account: {ex.Message}")
            End Try
        End SyncLock
    End Sub

    Public Function IsAutoReplied(ByVal Account As String) As Boolean
        SyncLock autoReplyLock
            Try
                Dim filePath = ClsSpecialDirectories.Getdata & "autoreplyusers.key"
                If IO.File.Exists(filePath) Then
                    Return IO.File.ReadAllText(filePath).Contains(Account)
                End If
            Catch ex As Exception
                Debug.WriteLine($"Error checking auto-reply status: {ex.Message}")
            End Try
        End SyncLock

        Return False
    End Function

    Public Function IsReceivedKeyExsist(ByVal key As String) As Boolean
        SyncLock messageKeyLock
            Try
                Dim filePath = ClsSpecialDirectories.Getdata & "messageskeys.key"
                If IO.File.Exists(filePath) Then
                    Return IO.File.ReadAllText(filePath).Contains(key)
                End If
            Catch ex As Exception
                Debug.WriteLine($"Error checking message key: {ex.Message}")
            End Try
        End SyncLock

        Return False
    End Function

    Public Sub AddMessageKey(ByVal key As String)
        SyncLock messageKeyLock
            Try
                IO.File.AppendAllText(ClsSpecialDirectories.Getdata & "messageskeys.key", key & vbNewLine)
            Catch ex As Exception
                Debug.WriteLine($"Error adding message key: {ex.Message}")
            End Try
        End SyncLock
    End Sub

#End Region

#Region "Utility Methods"

    Public Sub DeleteFilesAndFolder(ByVal Folder As String)
        Try
            ' Delete all files in the folder
            For Each file As String In IO.Directory.GetFiles(Folder)
                Try
                    IO.File.Delete(file)
                Catch ex As Exception
                    Debug.WriteLine($"Error deleting file {file}: {ex.Message}")
                End Try
            Next

            ' Recursively delete all subfolders
            For Each sfolder As String In IO.Directory.GetDirectories(Folder)
                Try
                    DeleteFilesAndFolder(sfolder)
                Catch ex As Exception
                    Debug.WriteLine($"Error deleting folder {sfolder}: {ex.Message}")
                End Try
            Next

            ' Delete the folder itself
            Try
                IO.Directory.Delete(Folder)
            Catch ex As Exception
                Debug.WriteLine($"Error deleting folder {Folder}: {ex.Message}")
            End Try
        Catch ex As Exception
            Debug.WriteLine($"Error processing folder {Folder}: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Form Events & Stubs"

    Private Sub FrmBrowser_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Me.Hide()
        End If
    End Sub

    Private Sub WebView2_Click(sender As Object, e As EventArgs) Handles WebView2.Click
        ' Event stub - no implementation needed
    End Sub

    ' Stub methods for future implementation
    Public Sub SendPool()
        ' TODO: Implement pool sending
    End Sub

    Public Sub SendVcard()
        ' TODO: Implement vCard sending
    End Sub

    Public Sub SendLocation()
        ' TODO: Implement location sending
    End Sub

#End Region

End Class

