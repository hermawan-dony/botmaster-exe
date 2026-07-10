Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.Net.WebRequestMethods
Imports System.Reflection
Imports System.Windows
Imports Newtonsoft.Json

Public Class FrmMain
    Dim _success As Integer
    Private isMouseDown As Boolean = False
    Private mouseOffset As Point
    Dim _resp As AccountSwticherDetails
    Public CmdAgrs As String = ""
    Public DefaultLang As String = "English"
    Private WithEvents AppNotifyIcon As NotifyIcon
    Public Shared LabelWAGW As Label


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If LstNumbers.SelectedItems.Count > 0 Then
            If MsgBox(GetLangbyKey("Messages.DELETE_NUMBER"), MsgBoxStyle.Question + vbYesNo, ApplicationTitle) = vbYes Then
                Dim li As ListViewItem
                For Each li In LstNumbers.SelectedItems
                    LstNumbers.Items.Remove(li)
                Next
            End If
        End If
        Statistics()

    End Sub
    Private Sub MnStrpImports_Opening(sender As Object, e As ComponentModel.CancelEventArgs) Handles MnStrpImports.Opening
        If LstNumbers.SelectedItems.Count > 0 Then
            DeleteToolStripMenuItem.Visible = True
            ToolStripMenuItem11.Visible = True
        Else
            DeleteToolStripMenuItem.Visible = False
            ToolStripMenuItem11.Visible = False
        End If
        If LstNumbers.Items.Count > 0 Then
            ToolStripMenuItem12.Visible = True
            ClearListToolStripMenuItem.Visible = True
        Else
            ToolStripMenuItem12.Visible = False
            ClearListToolStripMenuItem.Visible = False
        End If
        Statistics()
    End Sub
    Private Sub BtnSendingSub(sender As Object, e As EventArgs) Handles BtnSending.Click, ButtonSchedule.Click
        If IO.File.Exists(CurrentButtonFileName) Then
            ButtonsList = removeJascriptControlChars(IO.File.ReadAllText(CurrentButtonFileName))
        Else
            ButtonsList = ""
        End If

        If Not FrmBrowser.IsWAPILoggedIn Then
            MsgBox("Ensure that you are logged in into WhatsApp.", vbCritical, Application.ProductName)
            Exit Sub
        End If
        If LstNumbers.Items.Count = 0 Then
            MsgBox(GetLangbyKey("No_destination_to_send"), vbInformation, Application.ProductName)
            Exit Sub
        End If


        Dim DestinationsList As New List(Of DestinationModel)
        For Each DestinationListItem As ListViewItem In LstNumbers.Items

            Dim FullName As String = DestinationListItem.Text
            Dim FirstName As String
            Dim LastName As String


            If FullName <> "" And FullName <> "N/A" Then
                If FullName.Contains(" ") Then
                    FirstName = FullName.Split(" ")(0)
                    LastName = FullName.Split(" ")(UBound(FullName.Split(" ")))
                Else
                    FirstName = FullName
                    LastName = ""
                End If
            Else
                FullName = ""
                LastName = ""
                FirstName = ""
            End If

            DestinationsList.Add(New DestinationModel With {.WhatsAppID = DestinationListItem.Tag,
                                                            .Number = DestinationListItem.SubItems(1).Text,
                                                            .Fullname = FullName, .FirstName = FirstName,
                                                            .LastName = LastName, .Var1 = DestinationListItem.SubItems(2).Text,
                                                            .Var2 = DestinationListItem.SubItems(3).Text,
                                                            .Var3 = DestinationListItem.SubItems(4).Text,
                                                            .Var4 = DestinationListItem.SubItems(5).Text,
                                                            .Var5 = DestinationListItem.SubItems(6).Text})
        Next

        Dim MessageTab As Object
        Dim MessageText As Object
        Dim ContentToSend As Integer = 0
        Dim MessagesList As New List(Of String)
        For Each MessageTab In Me.TabMessages.Controls
            For Each MessageText In MessageTab.Controls
                If MessageText.text <> "" Then
                    MessagesList.Add(MessageText.text)
                End If
            Next
        Next

        Dim CanSend As Boolean = False
        For Each Message In MessagesList
            If Message <> "" Then
                CanSend = True
            End If
        Next

        Dim AttachmentList As New List(Of AttachmentModel)
        For Each AttachmentListItem As ListViewItem In LstMedia.Items
            AttachmentList.Add(New AttachmentModel With {.FileName = AttachmentListItem.Tag,
                                                         .Type = AttachmentListItem.SubItems(1).Text,
                                                         .Caption = AttachmentListItem.SubItems(2).Text})
        Next


        If sender.Name = "ButtonSchedule" Then
            If FrmSendingMode.ShowDialog = DialogResult.OK Then
                FrmSending.RefDate = FrmSendingMode.DatetoSend
            Else
                Exit Sub
            End If
        End If
        FrmSending.isButtons = CheckBox1.Checked
        If CheckBox1.Checked Then
            If IO.File.Exists(ClsSpecialDirectories.ButtonsFolder & "default.txt") Then
                FrmSending.ButtonObject = IO.File.ReadAllText(ClsSpecialDirectories.ButtonsFolder & "default.txt")
                '  MsgBox(FrmSending.ButtonObject)
                Try
                    FrmSending.Buttontext = JsonConvert.DeserializeObject(Of ButtonTemplate)(IO.File.ReadAllText(ClsSpecialDirectories.ButtonsFolder & "default.json")).Body
                Catch ex As Exception
                    FrmSending.Buttontext = "na"
                End Try

            End If

        End If
        FrmSending.BulkDestinations = DestinationsList
        FrmSending.BulkMessages = MessagesList
        FrmSending.BulkAttachments = AttachmentList
        FrmSendingType.ShowDialog()
        If FrmSendingType.Result = 0 Then
            Exit Sub
        End If
        '  FrmSending.isButtons = False
        FrmSending.DeliveryMode = FrmSendingType.Result
        FrmSending.Show()
    End Sub

    Private Sub DisableControls()
        NewBulkToolStripMenuItem.Enabled = False
        FF2.Enabled = False
        MM2.Enabled = False
        ClearNumbersListToolStripMenuItem.Enabled = False
        ClearMessageToolStripMenuItem.Enabled = False
        TxtMsg.Enabled = False
        MnStrpImports.Enabled = False
        BtnImgBrowse.Enabled = False
    End Sub
    Private Sub EnableControls()

        MnStrpImports.Enabled = True
        NewBulkToolStripMenuItem.Enabled = True
        FF2.Enabled = True
        MM2.Enabled = True
        ClearNumbersListToolStripMenuItem.Enabled = True
        ClearMessageToolStripMenuItem.Enabled = True
        TxtMsg.Enabled = True
        BtnImgBrowse.Enabled = True

    End Sub

    Private Sub SaveMessageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveMessageToolStripMenuItem.Click
        If TxtMsg.Text.Trim <> "" Then
            SaveFileDialog1.Filter = "*.txt|*.txt"
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                Dim sw As New IO.StreamWriter(SaveFileDialog1.FileName)
                sw.Write(TxtMsg.Text)
                sw.Close()
                sw.Dispose()
            End If
        End If
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        If MsgBox(GetLangbyKey("BWS_CLOSE_APPLICATION"), vbYesNo + vbQuestion, ApplicationTitle) = vbYes Then
            End
        End If

    End Sub
    Private Sub FrmMain_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = 1
        If MsgBox(GetLangbyKey("BWS_CLOSE_APPLICATION"), vbYesNo + vbQuestion, ApplicationTitle) = vbYes Then
            Try
                End
            Catch ex As Exception

            End Try

        End If
    End Sub
    Private Sub ClearLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearLogToolStripMenuItem.Click
        TxtLog.Text = ""
    End Sub
    Private Sub AddFiles()
        Dim Mediatype As Integer = 0

        Dim t As New OpenFileDialog
        t.Multiselect = True
        Select Case Mediatype
            Case 1
                t.Filter = "jpg,png,gif,mp4,m4v"
            Case 2
                t.Filter = "*.mp3;*.wav"
            Case 3
                t.Filter = "*.*|*.*"
        End Select
        t.Filter = "*.*|*.*"
        Dim li As ListViewItem
        Dim FileName As String
        If t.ShowDialog = DialogResult.OK Then
            For Each FileName In t.FileNames
                li = New ListViewItem
                li.Tag = FileName
                li.Text = GetFileName(FileName)
                li.SubItems.Add(GetMediaType(FileName))
                li.SubItems.Add("")
                Select Case GetMediaType(FileName)
                    Case "IMAGE"
                        li.ImageIndex = 1
                    Case "VIDEO"
                        li.ImageIndex = 2
                    Case Else
                        li.ImageIndex = 0
                End Select
                LstMedia.Items.Add(li)
            Next


        End If
    End Sub
    Private Function GetMediaType(ByVal FileName As String) As String
        Select Case UCase(GetExtension(FileName))
            Case "JPG"
                Return "IMAGE"
            Case "BMP"
                Return "IMAGE"
            Case "PNG"
                Return "IMAGE"
            Case "GIF"
                Return "IMAGE"
            Case "JPEG"
                Return "IMAGE"
            Case "MP4"
                Return "VIDEO"
            Case "M4V"
                Return "VIDEO"
            Case Else
                Return "FILE"

        End Select

    End Function

    Private Function GetFileName(ByVal FullPath As String) As String
        Dim a() As String = Split(FullPath, "\")
        Return a(UBound(a))
    End Function
    Private Function GetExtension(ByVal FileName As String) As String
        Try
            Dim a() As String = Split(FileName, ".")
            Return a(UBound(a))
        Catch ex As Exception
            Return ""
        End Try

    End Function
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TxtLog.Text = "" Then
            Exit Sub
        End If
        Dim sd As New SaveFileDialog
        sd.Filter = "*.txt|*.txt"
        If sd.ShowDialog = DialogResult.OK Then
            Try
                Dim sw As New IO.StreamWriter(sd.FileName)
                sw.Write(TxtLog.Text)
                sw.Close()
            Catch ex As Exception
                MsgBox(ex.Message, vbCritical, ApplicationTitle)
            End Try
        End If
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If MsgBox(GetLangbyKey("BWS_CLEAR_LOG"), vbQuestion + vbYesNo, ApplicationTitle) = vbYes Then
            TxtLog.Text = ""
        End If
    End Sub
    Private Sub ViewHelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewHelpToolStripMenuItem.Click
        Try
            If ShowBranding Then
                Process.Start(HelpLink)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        FrmAbout.ShowDialog()
    End Sub
    Private Sub BtnBold_Click(sender As Object, e As EventArgs) Handles BtnBold.Click
        On Error Resume Next

        Dim c As TextBox
        For Each c In TabMessages.SelectedTab.Controls

            Dim CurrentIndex As Integer = c.SelectionStart
            Dim CurrentLenght As Integer = c.SelectionLength
            c.Text = c.Text.Insert(CurrentIndex, "*")
            c.Text = c.Text.Insert(CurrentIndex + CurrentLenght + 1, "*")
        Next


    End Sub
    Private Sub BtnItalic_Click(sender As Object, e As EventArgs) Handles BtnItalic.Click
        On Error Resume Next
        Dim c As TextBox
        For Each c In TabMessages.SelectedTab.Controls
            Dim CurrentIndex As Integer = c.SelectionStart
            Dim CurrentLenght As Integer = c.SelectionLength
            c.Text = c.Text.Insert(CurrentIndex, "_")
            c.Text = c.Text.Insert(CurrentIndex + CurrentLenght + 1, "_")
        Next

    End Sub
    Private Sub BtnStrike_Click(sender As Object, e As EventArgs) Handles BtnStrike.Click
        On Error Resume Next
        Dim c As TextBox

        For Each c In TabMessages.SelectedTab.Controls

            Dim CurrentIndex As Integer = c.SelectionStart
            Dim CurrentLenght As Integer = c.SelectionLength
            c.Text = c.Text.Insert(CurrentIndex, "~")
            c.Text = c.Text.Insert(CurrentIndex + CurrentLenght + 1, "~")
        Next

    End Sub
    Public Sub FromFiles() Handles FF2.Click, FF1.Click, FF3.Click
        Try


            Dim Importer As New FileImports
            Dim _contact As WhatsAppContact
            If Importer.Showdialog = DialogResult.OK Then
                LstNumbers.Visible = False
                Dim ContactItem As ListViewItem
                For Each _contact In Importer.Contacts
                    ContactItem = New ListViewItem
                    ContactItem.Tag = _contact.WhatsAppID
                    ContactItem.Text = _contact.WhatsAppName
                    ContactItem.SubItems.Add(_contact.WhatsAppContact)
                    ContactItem.SubItems.Add(_contact.Var1)
                    ContactItem.SubItems.Add(_contact.Var2)
                    ContactItem.SubItems.Add(_contact.Var3)
                    ContactItem.SubItems.Add(_contact.Var4)
                    ContactItem.SubItems.Add(_contact.Var5)
                    LstNumbers.Items.Add(ContactItem)
                Next
                LstNumbers.Visible = True

            End If
            Statistics()
        Catch ex As Exception
            'MsgBox(ex.Message, vbCritical, Application.ProductName)
        End Try
    End Sub
    Public Sub ManualImports() Handles MM2.Click, MM1.Click, MM3.Click
        Dim Importer As New ManualImport
        Dim _contact As WhatsAppContact
        If Importer.Showdialog = DialogResult.OK Then
            LstNumbers.Visible = False
            Dim ContactItem As ListViewItem
            For Each _contact In Importer.Contacts
                ContactItem = New ListViewItem
                ContactItem.Tag = _contact.WhatsAppID
                ContactItem.Text = _contact.WhatsAppName
                ContactItem.SubItems.Add(_contact.WhatsAppContact)
                ContactItem.SubItems.Add("")
                ContactItem.SubItems.Add("")
                ContactItem.SubItems.Add("")
                ContactItem.SubItems.Add("")
                ContactItem.SubItems.Add("")
                LstNumbers.Items.Add(ContactItem)
            Next
            LstNumbers.Visible = True

        End If
        Statistics()
    End Sub
    Private Sub FrmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Application.Exit()
    End Sub
    Sub LoadGPTProfile()
        Dim profilePath As String = Path.Combine(Application.StartupPath, "defaultGtpProfile.gpt")

        If Not IO.File.Exists(profilePath) Then
            ' Initialize CurrentGPTSetting with default values if needed
            CurrentGPTSetting = New GTPModel() ' Adjust this according to your actual model class name
            Return
        End If

        Try
            Dim json As String = IO.File.ReadAllText(profilePath)
            CurrentGPTSetting = JsonConvert.DeserializeObject(Of GTPModel)(json)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Icon = GetAppIcon()
        PictureBox3.Image = GetAppIconImage()

        LoadChannels()
        LoadGPTProfile()

        sfts()
        
        Dim licKey As String = GetSetting(Application.ProductName, "license", "key", "")
        If licKey.ToLower().Contains("wasender") Then
            ApplicationTitle = "Botmaster ++WAGW"

            ' Initialize NotifyIcon for System Tray
            AppNotifyIcon = New NotifyIcon()
            AppNotifyIcon.Icon = Me.Icon
            AppNotifyIcon.Text = "Botmaster ++WAGW"
            AppNotifyIcon.Visible = True

            AddHandler AppNotifyIcon.DoubleClick, Sub(s, ev)
                                                      Me.Show()
                                                      Me.WindowState = FormWindowState.Normal
                                                      Me.ShowInTaskbar = True
                                                  End Sub

            Dim contextMenu As New ContextMenuStrip()
            contextMenu.Items.Add("Open Botmaster", Nothing, Sub(s, ev)
                                                                 Me.Show()
                                                                 Me.WindowState = FormWindowState.Normal
                                                                 Me.ShowInTaskbar = True
                                                             End Sub)
            contextMenu.Items.Add("Exit", Nothing, Sub(s, ev)
                                                       AppNotifyIcon.Visible = False
                                                       Application.Exit()
                                                   End Sub)
            AppNotifyIcon.ContextMenuStrip = contextMenu

            Try
                Dim runKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
                If GetSetting(Application.ProductName, "AutoRun", "Enabled", "0") = "1" Then
                    runKey.SetValue(Application.ProductName, Application.ExecutablePath)
                    
                    ' Auto Start Sync if DSN exists
                    Dim savedDSN As String = GetSetting(Application.ProductName, "ODBC", "SelectedDSN", "")
                    If savedDSN <> "" Then
                        FrmDatabaseSync.AutoStartSync(savedDSN)
                    End If

                    ' Auto Hide to Tray on boot/start if configured
                    If GetSetting(Application.ProductName, "AutoRun", "AutoHide", "0") = "1" Then
                        Me.WindowState = FormWindowState.Minimized
                        Me.ShowInTaskbar = False
                        Dim hideTimer As New Timer()
                        hideTimer.Interval = 100
                        AddHandler hideTimer.Tick, Sub()
                                                       hideTimer.Stop()
                                                       Me.Hide()
                                                   End Sub
                        hideTimer.Start()
                    End If
                Else
                    runKey.DeleteValue(Application.ProductName, False)
                End If
            Catch ex As Exception
            End Try
            
            ' Hide specific menu items for WAGW
            CheckForUpdateToolStripMenuItem.Visible = False
            AboutToolStripMenuItem.Visible = False
            ToolStripMenuItem10.Visible = False
            ToolStripMenuItem9.Visible = False
            ToolStripMenuItem8.Visible = False

            ' Initialize WAGW status label on main form header
            LabelWAGW = New Label()
            LabelWAGW.Text = "  WAGW Activated!!  "
            LabelWAGW.ForeColor = Color.LimeGreen
            LabelWAGW.BackColor = Color.Transparent
            LabelWAGW.Font = New Font("Calibri", 12.0F, FontStyle.Bold)
            LabelWAGW.AutoSize = True
            LabelWAGW.TextAlign = ContentAlignment.MiddleCenter
            LabelWAGW.Anchor = AnchorStyles.Top
            LabelWAGW.Location = New Point((Me.Panel3.Width - 180) \ 2, 4)
            AddHandler LabelWAGW.Paint, Sub(s, pe)
                                            Dim l As Label = CType(s, Label)
                                            pe.Graphics.DrawRectangle(New Pen(Color.LimeGreen, 2.0F), 0, 0, l.Width - 1, l.Height - 1)
                                        End Sub
            AddHandler Me.Panel3.Resize, Sub(s, ev)
                                             If LabelWAGW IsNot Nothing Then
                                                 LabelWAGW.Left = (Me.Panel3.Width - LabelWAGW.Width) \ 2
                                             End If
                                         End Sub
            LabelWAGW.Visible = False
            Me.Panel3.Controls.Add(LabelWAGW)

            Dim wagwMenuItem As New ToolStripMenuItem("++WAGW")
            AddHandler wagwMenuItem.Click, Sub(senderObj, eArgs)
                                               If FrmDatabaseSync.ActiveSyncInstance Is Nothing OrElse FrmDatabaseSync.ActiveSyncInstance.IsDisposed Then
                                                   FrmDatabaseSync.ActiveSyncInstance = New FrmDatabaseSync()
                                               End If
                                               FrmDatabaseSync.ActiveSyncInstance.Show()
                                               FrmDatabaseSync.ActiveSyncInstance.BringToFront()
                                           End Sub
            Me.ToolsToolStripMenuItem.DropDownItems.Add(wagwMenuItem)

            Dim wagwHelpItem As New ToolStripMenuItem("WAGW Tutorial")
            AddHandler wagwHelpItem.Click, Sub(senderObj, eArgs)
                                                Dim msg As String = "--- WAGW DATABASE AUTO-SYNC TUTORIAL ---" & vbCrLf & vbCrLf &
                                                                   "INDONESIA:" & vbCrLf &
                                                                   "1. Konfigurasi ODBC DSN terlebih dahulu di Windows (ODBC Data Source Administrator)." & vbCrLf &
                                                                   "2. Buka Tools > Database Auto-Sync (ODBC), pilih DSN dari dropdown, lalu klik 'Start Auto-Sync'." & vbCrLf &
                                                                   "3. Aplikasi akan otomatis membuat tabel 'outbox', 'inbox', dan 'sent' jika belum ada." & vbCrLf &
                                                                   "4. Kirim Pesan dengan insert SQL ke tabel 'outbox'. Setelah sukses terkirim, data akan dipindahkan ke tabel 'sent'." & vbCrLf &
                                                                   "   Contoh Query SQL yang bisa Anda jalankan:" & vbCrLf & vbCrLf &
                                                                   "   - Mengirim Pesan Teks Biasa:" & vbCrLf &
                                                                   "     INSERT INTO outbox (wa_no, wa_text) VALUES ('628512345678', 'Halo ini pesan tes')" & vbCrLf & vbCrLf &
                                                                   "   - Mengirim Pesan Media / Gambar (dengan Path File):" & vbCrLf &
                                                                   "     INSERT INTO outbox (wa_no, wa_text, wa_media) VALUES ('628512345678', 'Keterangan Gambar', 'C:\path\gambar.jpg')" & vbCrLf & vbCrLf &
                                                                   "   - Mengirim Pesan Tombol / Interactive (Format Spintax):" & vbCrLf &
                                                                   "     INSERT INTO outbox (wa_no, wa_text) VALUES ('628512345678', 'Pesan Utama|Tombol1|Tombol2')" & vbCrLf & vbCrLf &
                                                                   "--------------------------------------------------------" & vbCrLf & vbCrLf &
                                                                   "ENGLISH:" & vbCrLf &
                                                                   "1. Configure ODBC DSN in Windows (ODBC Data Source Administrator) first." & vbCrLf &
                                                                   "2. Open Tools > Database Auto-Sync (ODBC), select the DSN from the dropdown, then click 'Start Auto-Sync'." & vbCrLf &
                                                                   "3. The application will automatically create 'outbox', 'inbox', and 'sent' tables if they don't exist." & vbCrLf &
                                                                   "4. Send messages by executing an INSERT SQL to 'outbox'. Successfully sent messages are moved to 'sent'." & vbCrLf &
                                                                   "   Example SQL queries you can run:" & vbCrLf & vbCrLf &
                                                                   "   - Sending Plain Text Message:" & vbCrLf &
                                                                   "     INSERT INTO outbox (wa_no, wa_text) VALUES ('628512345678', 'Hello this is a test message')" & vbCrLf & vbCrLf &
                                                                   "   - Sending Media / Image Message (with File Path):" & vbCrLf &
                                                                   "     INSERT INTO outbox (wa_no, wa_text, wa_media) VALUES ('628512345678', 'Image Caption', 'C:\path\image.jpg')" & vbCrLf & vbCrLf &
                                                                   "   - Sending Button / Interactive Message (Spintax format):" & vbCrLf &
                                                                   "     INSERT INTO outbox (wa_no, wa_text) VALUES ('628512345678', 'Main Text|Button1|Button2')"

                                               ' Open dynamic RichTextBox Form window
                                               Dim frm As New Form()
                                               frm.Text = "WAGW Database Auto-Sync Tutorial"
                                               frm.Size = New Size(650, 550)
                                               frm.StartPosition = FormStartPosition.CenterParent
                                               frm.MinimizeBox = False
                                               frm.MaximizeBox = True
                                               frm.Icon = Me.Icon
                                               
                                               Dim rtb As New RichTextBox()
                                               rtb.Dock = DockStyle.Fill
                                               rtb.ReadOnly = True
                                               rtb.Font = New Font("Consolas", 10)
                                               rtb.Text = msg
                                               rtb.BackColor = Color.FromArgb(15, 17, 33)
                                               rtb.ForeColor = Color.White
                                               
                                               frm.Controls.Add(rtb)
                                               frm.ShowDialog()
                                           End Sub
            Me.HelpToolStripMenuItem.DropDownItems.Add(wagwHelpItem)
        Else
            AboutToolStripMenuItem.Visible = ShowAbout
            ViewHelpToolStripMenuItem.Visible = ShowHelp
        End If

        ProfileName = CmdAgrs


        DropShadow.ApplyShadows(Me)
        EnableChatGPTReplyToolStripMenuItem.Checked = CBool(GetSetting(Application.ProductName, "ChatGPT", "Enable", "0"))
        InitiateStart()
        Try
            For Each _langFile As String In IO.Directory.GetFiles(Application.StartupPath & "\languages\", "*.json")
                _langFile = _langFile.Replace(Application.StartupPath & "\languages\", "")
                _langFile = _langFile.Replace(".json", "")
                FrmLanguage.ComboBoxLanguage.Items.Add(_langFile)
            Next

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, Application.ProductName)
            End
        End Try


        Try
            FrmLanguage.ComboBoxLanguage.Text = GetSetting(Application.ProductName, "language", "languagename", DefaultLang)
            CurrentLanguage = GetSetting(Application.ProductName, "language", "languagename", DefaultLang)
        Catch ex As Exception
            CurrentLanguage = "English"
        End Try

        ApplyLanguage()
        FrmBrowser.Show()
        FrmBrowser.Hide()

        Dim a As Integer = Val(GetSetting("wapi", "api", "port", "0")) + 1
        SaveSetting("wapi", "api", "port", a)


        Label4.Text = ApplicationTitle & " v" & ApplicationVersion & " Build:" & IO.File.GetCreationTime(Assembly.GetExecutingAssembly().Location).ToString("yyyy.MM.dd")

        If ProfileName <> "" Then
            Label4.Text = Label4.Text & " Profile: " & ProfileName
        End If

        AboutToolStripMenuItem.Visible = ShowAbout AndAlso Not licKey.ToLower().Contains("wasender")

        ViewHelpToolStripMenuItem.Visible = ShowHelp



        BtnTopClose.Font = New Font("Marlett", 9.5F)
        BtnMinimize.Font = New Font("Marlett", 9.5F)
        BtnMax.Font = New Font("Marlett", 9.5F)


        Panel10.Width = 60
        ButtonSwitch.Width = 32
        ButtonSwitch.Margin = New Padding(0)
        ButtonSwitch.Padding = New Padding(0)
        ButtonSwitch.Font = New Font("Calibri", 7.0F, FontStyle.Bold)
        If GetSetting(Application.ProductName, "AutoReply", "Start", "1") = "1" Then
            ButtonSwitch.Dock = DockStyle.Right
            ButtonSwitch.BackColor = SwitchColor
            ButtonSwitch.Text = "ON"
            ButtonSwitch.ForeColor = Color.White
        Else
            ButtonSwitch.Dock = DockStyle.Left
            ButtonSwitch.BackColor = Color.FromArgb(237, 237, 237)
            ButtonSwitch.Text = "OFF"
            ButtonSwitch.ForeColor = Color.DarkGray
        End If

        CheckBox1.Checked = CBool(GetSetting(Application.ProductName, "Buttons", "IncludeButtons", "0"))

        Statistics()
        LoadAutoReplyRules()
        ApplyColor(Me)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        SaveSetting(Application.ProductName, "Buttons", "IncludeButtons", If(CheckBox1.Checked, "1", "0"))
    End Sub

    Private Sub Panel3_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel3.MouseDown, Label4.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            mouseOffset = New Point(-e.X, -e.Y)
            isMouseDown = True
        End If
    End Sub

    Private Sub Panel3_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel3.MouseMove, Label4.MouseMove
        If isMouseDown Then
            Dim mousePos As Point = Control.MousePosition
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Me.Location = mousePos
        End If
    End Sub
    Private Sub Panel3_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel3.MouseUp, Label4.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub
    Private Sub BtnMinimize_Click(sender As Object, e As EventArgs) Handles BtnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub BtnTopClose_Click(sender As Object, e As EventArgs) Handles BtnTopClose.Click
        Me.Close()
    End Sub
    Private Sub BtnMax_Click(sender As Object, e As EventArgs) Handles BtnMax.Click, Panel3.DoubleClick, Label4.DoubleClick
        If Me.WindowState = FormWindowState.Maximized Then
            BtnMax.Text = "1"
            Me.WindowState = FormWindowState.Normal
        Else
            BtnMax.Text = "2"
            Me.WindowState = FormWindowState.Maximized
        End If
    End Sub
    Private Sub ClearMessageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearMessageToolStripMenuItem.Click
        TxtMsg.Text = ""
    End Sub

    Private Sub ClearNumbersListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearNumbersListToolStripMenuItem.Click
        LstNumbers.Items.Clear()
        Statistics()
    End Sub

    Private Sub TxtLog_TextChanged(sender As Object, e As EventArgs) Handles TxtLog.TextChanged
        Dim a() As String = Split(TxtLog.Text, vbNewLine)
        If UBound(a) > 29 Then
            TxtLog.ScrollBars = ScrollBars.Vertical
        Else
            TxtLog.ScrollBars = ScrollBars.None
        End If

    End Sub

    Private Sub BtnAddMessage_Click(sender As Object, e As EventArgs) Handles BtnAddMessage.Click
        TabMessages.TabPages.Add("Message " & TabMessages.TabPages.Count + 1)
        Dim _txtmsg As New TextBox
        _txtmsg.Name = "txtMsg" & TabMessages.TabPages.Count
        _txtmsg.BackColor = Color.White
        _txtmsg.Multiline = True
        _txtmsg.ScrollBars = ScrollBars.Vertical
        _txtmsg.BorderStyle = BorderStyle.None
        _txtmsg.Dock = DockStyle.Fill
        _txtmsg.Margin = New Padding(3, 4, 3, 4)
        _txtmsg.Font = TxtMsg.Font

        TabMessages.TabPages(TabMessages.TabPages.Count - 1).Controls.Add(_txtmsg)
        TabMessages.TabPages(TabMessages.TabPages.Count - 1).Padding = New Padding(3, 3, 3, 3)
        TabMessages.SelectedTab = TabMessages.TabPages(TabMessages.TabPages.Count - 1)
    End Sub

    Private Sub BtnDeletMessage_Click(sender As Object, e As EventArgs) Handles BtnDeletMessage.Click
        If TabMessages.TabPages.Count > 1 Then
            TabMessages.TabPages.Remove(TabMessages.SelectedTab)
        End If
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click, NumberFilterToolStripMenuItem.Click
        If DemoMode Then : ShowDemoMessage() : End If
        FrmFilter.Show()
        Statistics()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        FrmReceived.Show()
    End Sub



    Private Sub BtnEmoji_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles BtnEmoji.LinkClicked
        Process.Start("https://www.emojicopy.com/")
    End Sub

    Private Sub TimerLcs_Tick(sender As Object, e As EventArgs) Handles TimerLcs.Tick
        TimerLcs.Enabled = False
    End Sub

    Private Sub SetCaptionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetCaptionToolStripMenuItem.Click
        If LstMedia.Items.Count > 0 Then

            If LstMedia.SelectedItems.Count > 0 Then
                If LstMedia.SelectedItems(0).Text <> "" Then
                    If LstMedia.SelectedItems(0).SubItems(1).Text = "Photo" Or LstMedia.SelectedItems(0).SubItems(1).Text = "Video" Then
                        Try
                            FrmSetCaption.TxtCaption.Text = LstMedia.SelectedItems(0).SubItems(2).Text
                        Catch ex As Exception
                            FrmSetCaption.TxtCaption.Text = ""
                        End Try

                        FrmSetCaption.ShowDialog()
                        If FrmSetCaption.dlgResult = DialogResult.OK Then

                            LstMedia.SelectedItems(0).SubItems(2).Text = FrmSetCaption.Caption
                        End If
                    End If
                End If
            End If
        End If
    End Sub



    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        If LstMedia.Items.Count > 0 Then

            If LstMedia.SelectedItems.Count > 0 Then
                If LstMedia.SelectedItems(0).Text <> "" Then
                    LstMedia.Items.Remove(LstMedia.SelectedItems(0))
                End If
            End If
        End If
    End Sub

    Private Sub OpenFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFileToolStripMenuItem.Click
        If LstMedia.Items.Count > 0 Then

            If LstMedia.SelectedItems.Count > 0 Then
                If LstMedia.SelectedItems(0).Text <> "" Then
                    Try
                        Process.Start(LstMedia.SelectedItems(0).Tag)
                    Catch ex As Exception
                        MsgBox(ex.Message, vbCritical, ApplicationTitle)
                    End Try

                End If
            End If
        End If
    End Sub


    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If CriticalError <> "" Then
            Dim _crErr As String = CriticalError
            CriticalError = ""
            MsgBox(_crErr, vbCritical, ApplicationTitle)

        End If
        'If LstMedia.Items.Count = 0 Then
        '    CheckBoxTurbo.Enabled = True
        'Else
        '    CheckBoxTurbo.Checked = False
        '    CheckBoxTurbo.Enabled = False
        'End If
    End Sub
    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        ContextMenuStrip1.Show()
    End Sub
    Private Sub Button19_MouseDown(sender As Object, e As MouseEventArgs) Handles Button19.MouseDown
        ContextMenuStripParams.Show(Button19, e.Location)
    End Sub

    Private Sub InsertSpintaxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertSpintaxToolStripMenuItem.Click
        If FrmSpintax.ShowDialog() = DialogResult.OK Then
            Dim c As TextBox
            For Each c In TabMessages.SelectedTab.Controls
                c.Text = c.Text.Insert(c.SelectionStart, FrmSpintax.spintax)
            Next
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)

    End Sub

    Private Sub InsertFullNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertFullNameToolStripMenuItem.Click
        Dim c As TextBox

        For Each c In TabMessages.SelectedTab.Controls
            c.Text = c.Text.Insert(c.SelectionStart, "[[fullname]]")

        Next
    End Sub

    Private Sub InsertFirstNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertFirstNameToolStripMenuItem.Click
        Dim c As TextBox

        For Each c In TabMessages.SelectedTab.Controls
            c.Text = c.Text.Insert(c.SelectionStart, "[[firstname]]")
        Next
    End Sub

    Private Sub InsertLastNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertLastNameToolStripMenuItem.Click

        For Each c In TabMessages.SelectedTab.Controls
            c.Text = c.Text.Insert(c.SelectionStart, "[[lastname]]")
        Next
    End Sub

    Private Sub InsertRandomTagToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertRandomTagToolStripMenuItem.Click
        For Each c In TabMessages.SelectedTab.Controls
            c.Text = c.Text.Insert(c.SelectionStart, "[[randomtag]]")
        Next
    End Sub

    Private Sub Variable1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Variable1ToolStripMenuItem.Click
        Dim c As TextBox
        For Each c In TabMessages.SelectedTab.Controls
            c.Text = c.Text.Insert(c.SelectionStart, "[[VAR1]]")
        Next
    End Sub

    Private Sub Variable2ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Variable2ToolStripMenuItem1.Click
        Dim c As TextBox
        For Each c In TabMessages.SelectedTab.Controls
            c.Text = c.Text.Insert(c.SelectionStart, "[[VAR2]]")
        Next

    End Sub
    Private Sub Variable3ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Variable3ToolStripMenuItem1.Click
        Dim c As TextBox
        For Each c In TabMessages.SelectedTab.Controls
            c.Text = c.Text.Insert(c.SelectionStart, "[[VAR3]]")
        Next

    End Sub
    Private Sub Variable4ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Variable4ToolStripMenuItem1.Click
        Dim c As TextBox
        For Each c In TabMessages.SelectedTab.Controls
            c.Text = c.Text.Insert(c.SelectionStart, "[[VAR4]]")
        Next

    End Sub
    Private Sub Variable5ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Variable5ToolStripMenuItem1.Click
        Dim c As TextBox
        For Each c In TabMessages.SelectedTab.Controls
            c.Text = c.Text.Insert(c.SelectionStart, "[[VAR5]]")
        Next

    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        FrmSentCampain.ShowDialog()
    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click, GroupGrabberToolStripMenuItem.Click
        If DemoMode Then : ShowDemoMessage() : End If
        FrmGroupGrabber.ShowDialog()
    End Sub

    Private Sub ButtonOpenWhatsApp_Click(sender As Object, e As EventArgs) Handles ButtonOpenWhatsApp.Click
        FrmBrowser.Show()
        FrmBrowser.Activate()
        FrmBrowser.WindowState = FormWindowState.Normal
    End Sub
    Private Async Sub ImportFromWhatsAppContactsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportFromWhatsAppContactsToolStripMenuItem.Click, WhatsAppContactsToolStripMenuItem.Click
        If Not FrmBrowser.IsWAPILoggedIn Then
            MsgBox(GetLangbyKey("WhatsApp_not_ready_please_wait_and_check"), vbInformation, Application.ProductName)
            Exit Sub
        End If
        Dim Contacts = Await FrmBrowser.GetAllContact()
        Dim li As ListViewItem
        If Not IsNothing(Contacts) Then
            For Each contact In Contacts.Where(Function(t) Not t.isSmb)
                If contact.id._serialized.ToLower.Contains("c.us") Then
                    li = New ListViewItem
                    li.Tag = contact.id._serialized
                    li.Text = If(contact.name = "", "N/A", contact.name)
                    li.SubItems.Add(contact.id.user)
                    li.SubItems.Add("")
                    li.SubItems.Add("")
                    li.SubItems.Add("")
                    li.SubItems.Add("")
                    li.SubItems.Add("")

                    LstNumbers.Items.Add(li)
                End If
            Next
        End If
        Statistics()
    End Sub
    Private Sub ButtonSwitch_Click(sender As Object, e As EventArgs) Handles ButtonSwitch.Click, Panel10.Click
        If ButtonSwitch.Dock = DockStyle.Left Then
            ButtonSwitch.Dock = DockStyle.Right
            ButtonSwitch.BackColor = SwitchColor
            ButtonSwitch.Text = "ON"
            ButtonSwitch.ForeColor = Color.White
            SaveSetting(Application.ProductName, "AutoReply", "Start", "1")
        Else
            ButtonSwitch.Dock = DockStyle.Left
            ButtonSwitch.BackColor = Color.FromArgb(237, 237, 237)
            ButtonSwitch.Text = "OFF"
            ButtonSwitch.ForeColor = Color.DarkGray
            SaveSetting(Application.ProductName, "AutoReply", "Start", "0")
        End If
    End Sub
    Private Sub BtnImgBrowse_MouseDown(sender As Object, e As MouseEventArgs) Handles BtnImgBrowse.MouseDown
        ContextMenuStripAttachFiles.Show(BtnImgBrowse, e.Location)
    End Sub
    Private Sub ImportMessageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportMessageToolStripMenuItem.Click
        Dim OpenDlg As New OpenFileDialog
        OpenDlg.Filter = "*.txt|*.txt"
        OpenDlg.Multiselect = False
        If OpenDlg.ShowDialog = DialogResult.OK Then
            If Not IsNothing(TxtMsg) Then
                TxtMsg.Text = IO.File.ReadAllText(OpenDlg.FileName)
            End If

        End If
        Statistics()
    End Sub

    Private Async Sub ImportFromWhatsAppGroupsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportFromWhatsAppGroupsToolStripMenuItem.Click, WhatsAppGroupsToolStripMenuItem.Click
        If Not FrmBrowser.IsWAPILoggedIn Then
            MsgBox(GetLangbyKey("WhatsApp_not_ready_please_wait_and_check"), vbInformation, Application.ProductName)
            Exit Sub
        End If
        Dim groups = Await FrmBrowser.GetAllGroups()
        Dim li As ListViewItem
        For Each group In groups

            li = New ListViewItem
            li.Tag = group.id
            li.Text = group.name
            li.SubItems.Add(group.id)
            li.SubItems.Add("")
            li.SubItems.Add("")
            li.SubItems.Add("")
            li.SubItems.Add("")
            li.SubItems.Add("")
            LstNumbers.Items.Add(li)
        Next
        Statistics()
    End Sub

    Private Async Sub ImportFromWhatsAppLIDContactsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportFromWhatsAppLIDContactsToolStripMenuItem.Click
        If Not FrmBrowser.IsWAPILoggedIn Then
            MsgBox(GetLangbyKey("WhatsApp_not_ready_please_wait_and_check"), vbInformation, Application.ProductName)
            Exit Sub
        End If
        Dim Contacts = Await FrmBrowser.GetAllContact()
        If Contacts Is Nothing Then
            MsgBox("Could not retrieve contacts.", vbInformation, Application.ProductName)
            Exit Sub
        End If
        Dim li As ListViewItem
        Dim added As Integer = 0
        For Each contact In Contacts.Where(Function(t) t.id._serialized.ToLowerInvariant().Contains("@lid"))
            li = New ListViewItem
            li.Tag = contact.id._serialized          ' e.g. 121775308415231@lid
            li.Text = If(Not String.IsNullOrWhiteSpace(contact.name), contact.name, If(Not String.IsNullOrWhiteSpace(contact.shortName), contact.shortName, "N/A"))
            li.SubItems.Add(contact.id._serialized)  ' WhatsApp ID column
            li.SubItems.Add("")
            li.SubItems.Add("")
            li.SubItems.Add("")
            li.SubItems.Add("")
            li.SubItems.Add("")
            LstNumbers.Items.Add(li)
            added += 1
        Next
        Statistics()
        MsgBox($"{added} LID contact(s) imported.", vbInformation, Application.ProductName)
    End Sub

    Private Sub ImportLIDManuallyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportLIDManuallyToolStripMenuItem.Click
        Dim Importer As New ManualImport With {.IsLIDMode = True}
        If Importer.Showdialog = DialogResult.OK Then
            LstNumbers.Visible = False
            For Each _contact In Importer.Contacts
                Dim li As New ListViewItem
                li.Tag = _contact.WhatsAppID   ' e.g. 121775308415231@lid
                li.Text = _contact.WhatsAppName
                li.SubItems.Add(_contact.WhatsAppID)
                li.SubItems.Add("")
                li.SubItems.Add("")
                li.SubItems.Add("")
                li.SubItems.Add("")
                li.SubItems.Add("")
                LstNumbers.Items.Add(li)
            Next
            LstNumbers.Visible = True
        End If
        Statistics()
    End Sub

    Public Sub Statistics()
        LabelTotalCount.Text = LstNumbers.Items.Count
        Dim li As ListViewItem
        Dim cc As Integer = 0
        Dim gc As Integer = 0
        For Each li In LstNumbers.Items
            If li.Tag.ToString.Contains("@c.us") Then
                cc = cc + 1
            Else
                gc = gc + 1
            End If
        Next
        LabelGroupsCount.Text = gc
        LabelContactsCount.Text = cc

    End Sub

    Private Sub PhotosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PhotosToolStripMenuItem.Click
        Dim OpenDlg As New OpenFileDialog()
        OpenDlg.Filter = "*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.ico|*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.ico"
        OpenDlg.Multiselect = True
        If OpenDlg.ShowDialog() = DialogResult.OK Then
            Dim fLi As ListViewItem
            For Each _file As String In OpenDlg.FileNames


                If GetFileSize(_file) > 1 Then
                    If MsgBox("The Image you are trying to add is larger than 1 MB ,and may it will cause error or system will not deliver it.do you want proceed ? ", vbYesNo + vbCritical, Application.ProductName) = vbNo Then
                        GoTo skip
                    End If
                End If


                fLi = New ListViewItem
                fLi.Tag = _file
                Dim a() As String = Split(_file, "\")
                fLi.Text = a(UBound(a))
                fLi.SubItems.Add("Photo")
                fLi.SubItems.Add("")
                fLi.ImageIndex = 1
                If IsMediaAdded(fLi) Then
                    If MsgBox(GetLangbyKey("The_file_that_you_want_to_add_its_already_available_do_you_want_add_it_anyway"), vbQuestion + vbYesNo, Application.ProductName) = vbYes Then
                        LstMedia.Items.Add(fLi)
                    End If
                Else
                    LstMedia.Items.Add(fLi)
                End If


skip:

            Next
        End If
    End Sub
    Private Function IsMediaAdded(ByVal li As ListViewItem) As Boolean
        For Each _li As ListViewItem In LstMedia.Items
            If _li.Tag = li.Tag Then
                Return True
                Exit Function
            End If

        Next
        Return False
    End Function
    Private Sub LstMedia_DoubleClick(sender As Object, e As EventArgs) Handles LstMedia.DoubleClick
        If LstMedia.Items.Count > 0 Then
            If LstMedia.SelectedItems.Count > 0 Then

                Select Case LstMedia.SelectedItems(0).SubItems(1).Text
                    Case "Catalog"
                        FrmCatalogBuilder.CatalogFileName = LstMedia.SelectedItems(0).Tag
                        FrmCatalogBuilder.ShowDialog()
                    Case ""
                    Case Else
                        Process.Start(LstMedia.SelectedItems(0).Tag)
                End Select
            End If
        End If
    End Sub

    Private Sub VideosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VideosToolStripMenuItem.Click
        Dim OpenDlg As New OpenFileDialog()
        OpenDlg.Filter = "*.mp4|*.mp4"
        OpenDlg.Multiselect = True
        If OpenDlg.ShowDialog() = DialogResult.OK Then
            Dim fLi As ListViewItem
            For Each _file As String In OpenDlg.FileNames

                If GetFileSize(_file) > 1 Then
                    If MsgBox("The Video you are trying to add is larger than 1 MB ,and may it will cause error or system will not deliver it.do you want proceed ? ", vbYesNo + vbCritical, Application.ProductName) = vbNo Then
                        GoTo skip
                    End If
                End If

                fLi = New ListViewItem
                fLi.Tag = _file
                Dim a() As String = Split(_file, "\")
                fLi.Text = a(UBound(a))
                fLi.SubItems.Add("Video")
                fLi.SubItems.Add("")
                fLi.ImageIndex = 2
                If IsMediaAdded(fLi) Then


                    If MsgBox(GetLangbyKey("The_file_that_you_want_to_add_its_already_available_do_you_want_add_it_anyway"), vbQuestion + vbYesNo, Application.ProductName) = vbYes Then
                        LstMedia.Items.Add(fLi)
                    End If
                Else
                    LstMedia.Items.Add(fLi)
                End If
skip:
            Next
        End If
    End Sub

    Private Sub DocumentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DocumentsToolStripMenuItem.Click
        Dim OpenDlg As New OpenFileDialog()
        OpenDlg.Filter = "*.txt;*.pdf|*.txt;*.pdf"
        OpenDlg.Multiselect = True
        If OpenDlg.ShowDialog() = DialogResult.OK Then
            Dim fLi As ListViewItem
            For Each _file As String In OpenDlg.FileNames

                If GetFileSize(_file) > 1 Then
                    If MsgBox("The file you are trying to add is larger than 1 MB ,and may it will cause error or system will not deliver it.do you want proceed ? ", vbYesNo + vbCritical, Application.ProductName) = vbNo Then
                        GoTo skip
                    End If
                End If


                fLi = New ListViewItem
                fLi.Tag = _file
                Dim a() As String = Split(_file, "\")
                fLi.Text = a(UBound(a))
                fLi.SubItems.Add("Document")
                fLi.SubItems.Add("")
                fLi.ImageIndex = 0
                If IsMediaAdded(fLi) Then

                    If MsgBox(GetLangbyKey("The_file_that_you_want_to_add_its_already_available_do_you_want_add_it_anyway"), vbQuestion + vbYesNo, Application.ProductName) = vbYes Then
                        LstMedia.Items.Add(fLi)
                    End If
                Else
                    LstMedia.Items.Add(fLi)
                End If
skip:
            Next
        End If
    End Sub
    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click, NewAutoReplyRuleToolStripMenuItem.Click
        FrmRules.Rule = Nothing
        FrmRules.TextBoxKeyword.Text = ""
        FrmRules.TextBoxMessage.Text = ""
        FrmRules.TextBoxRuleName.Text = ""
        FrmRules.ListViewAttachment.Items.Clear()
        FrmRules.CheckBoxEnable.Checked = False
        If FrmRules.ShowDialog() = DialogResult.OK Then
            Dim li As New ListViewItem
            li.Text = FrmRules.Rule.RuleName
            li.Tag = FrmRules.Rule
            li.SubItems.Add(FrmRules.Rule.RuleKeyword)
            li.SubItems.Add(FrmRules.Rule.RuleStatus)
            ListView1.Items.Add(li)
            SaveAutoReplyRules()
        End If
    End Sub


    Private Sub ListReceivedMessages_DoubleClick(sender As Object, e As EventArgs) Handles ListReceivedMessages.DoubleClick
        Try
            If ListReceivedMessages.SelectedItems.Count > 0 Then
                If ListReceivedMessages.SelectedItems(0).Text <> "" Then
                    Dim ReceivedFrm As New FrmReceivedMessage
                    ReceivedFrm.TextBoxReceivedDate.Text = ListReceivedMessages.SelectedItems(0).Text
                    ReceivedFrm.TextBoxSender.Text = ListReceivedMessages.SelectedItems(0).SubItems(1).Text
                    ReceivedFrm.TextBoxReceivedMessage.Text = ListReceivedMessages.SelectedItems(0).SubItems(2).Text
                    ReceivedFrm.WhatsAppID = ListReceivedMessages.SelectedItems(0).Tag
                    ReceivedFrm.Show()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, Application.ProductName)
        End Try
    End Sub

    Private Sub ButtonRules_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonRules.MouseDown
        RulesContextMenuStrip.Show(ButtonRules, e.Location)
    End Sub

    Private Sub ButtonUnderLine_Click(sender As Object, e As EventArgs)
        On Error Resume Next
        Dim c As TextBox
        For Each c In TabMessages.SelectedTab.Controls

            Dim CurrentIndex As Integer = c.SelectionStart
            Dim CurrentLenght As Integer = c.SelectionLength
            c.Text = c.Text.Insert(CurrentIndex, "*")

            c.Text = c.Text.Insert(CurrentIndex + CurrentLenght + 1, "*")
        Next
    End Sub

    Private Sub SetAutoReplyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetAutoReplyToolStripMenuItem.Click
        FrmAutoReply.ShowDialog()
    End Sub

    Private Sub AddAutoReplyRuleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddAutoReplyRuleToolStripMenuItem.Click
        FrmRules.Rule = Nothing
        FrmRules.TextBoxKeyword.Text = ""
        FrmRules.TextBoxMessage.Text = ""
        FrmRules.TextBoxRuleName.Text = ""
        FrmRules.ListViewAttachment.Items.Clear()
        FrmRules.CheckBoxEnable.Checked = False
        If FrmRules.ShowDialog() = DialogResult.OK Then
            Dim li As New ListViewItem
            li.Text = FrmRules.Rule.RuleName
            li.Tag = FrmRules.Rule
            li.SubItems.Add(FrmRules.Rule.Operand & " [" & FrmRules.Rule.RuleKeyword & "]")
            li.SubItems.Add(FrmRules.Rule.RuleStatus)
            ListView1.Items.Add(li)
            SaveAutoReplyRules()
        End If
    End Sub
    Public Sub SaveAutoReplyRules()
        Try
            Dim li As ListViewItem
            Dim lst As New List(Of ClsRuleModel)
            Dim _rule As ClsRuleModel
            For Each li In ListView1.Items
                _rule = li.Tag
                lst.Add(_rule)
            Next
            Dim _autoreplyJson = Newtonsoft.Json.JsonConvert.SerializeObject(lst)
            IO.File.WriteAllText(ClsSpecialDirectories.Getdata & "rules.json", _autoreplyJson)
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, Application.ProductName)
        End Try
    End Sub
    Public Sub LoadAutoReplyRules()
        Try

            If IO.File.Exists(ClsSpecialDirectories.Getdata & "rules.json") Then
                Dim li As ListViewItem
                Dim lst As IEnumerable(Of ClsRuleModel)
                lst = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of ClsRuleModel))(IO.File.ReadAllText(ClsSpecialDirectories.Getdata & "rules.json"))
                ListView1.Items.Clear()
                Dim _rule As ClsRuleModel

                For Each _rule In lst
                    li = New ListViewItem
                    li.Tag = _rule
                    li.Text = _rule.RuleName
                    If _rule.Operand = "" Then
                        _rule.Operand = "="
                    End If
                    li.SubItems.Add(_rule.Operand & " [" & _rule.RuleKeyword & "]")
                    li.SubItems.Add(_rule.RuleStatus)
                    ListView1.Items.Add(li)
                Next

            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, Application.ProductName)
        End Try
    End Sub
    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick, ModifyRuleToolStripMenuItem.Click
        If ListView1.SelectedItems.Count > 0 Then
            If ListView1.SelectedItems(0).Text <> "" Then
                FrmRules.TextBoxKeyword.Text = ""
                FrmRules.TextBoxMessage.Text = ""
                FrmRules.TextBoxRuleName.Text = ""
                FrmRules.ListViewAttachment.Items.Clear()
                FrmRules.CheckBoxEnable.Checked = False
                FrmRules.Rule = ListView1.SelectedItems(0).Tag

                If FrmRules.ShowDialog() = DialogResult.OK Then
                    Dim li As New ListViewItem
                    ListView1.SelectedItems(0).Text = FrmRules.Rule.RuleName
                    ListView1.SelectedItems(0).Tag = FrmRules.Rule
                    ListView1.SelectedItems(0).SubItems(1).Text = FrmRules.Rule.Operand & " [" & FrmRules.Rule.RuleKeyword & "]"
                    ListView1.SelectedItems(0).SubItems(2).Text = FrmRules.Rule.RuleStatus
                    SaveAutoReplyRules()
                End If
            End If
        End If
    End Sub

    Private Sub DeleteRuleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteRuleToolStripMenuItem.Click
        If ListView1.SelectedItems.Count > 0 Then
            If ListView1.SelectedItems(0).Text <> "" Then
                If MsgBox(GetLangbyKey("Are_you_Sure_you_want_to_delete_rule"), vbQuestion + vbYesNo, Application.ProductName) = vbYes Then
                    ListView1.SelectedItems(0).Remove()
                    SaveAutoReplyRules()
                End If
            End If
        End If
    End Sub

    Private Sub ResetAutoReplyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetAutoReplyToolStripMenuItem.Click
        IO.File.Delete(ClsSpecialDirectories.Getdata & "autoreplyusers.key")
        MsgBox(GetLangbyKey("Auto_reply_has_been_rested"), vbInformation, Application.ProductName)
    End Sub
    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click, SendingSettingsToolStripMenuItem.Click
        FrmAdvanced.ShowDialog()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click, SetAutoReplyToolStripMenuItem1.Click
        FrmAutoReply.ShowDialog()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click, NewBulkToolStripMenuItem.Click
        If MsgBox(GetLangbyKey("Are_you_sure_you_want_start_new_bulk"), vbQuestion + vbYesNo, Application.ProductName) = vbYes Then
            LstNumbers.Items.Clear()
            For Each t As TabPage In TabMessages.TabPages
                If t.Text <> "Message 1" Then
                    TabMessages.TabPages.Remove(t)
                End If

            Next
            TxtMsg.Text = ""
            LstMedia.Items.Clear()
            Statistics()
        End If
    End Sub

    Private Sub SentCampaignsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SentCampaignsToolStripMenuItem.Click
        FrmSentCampain.ShowDialog()
    End Sub

    Private Sub ReceivedMessageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReceivedMessageToolStripMenuItem.Click
        FrmReceived.Show()
    End Sub

    Private Sub LogsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogsToolStripMenuItem.Click
        FrmLog.Show()
    End Sub

    Private Sub LanguagesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LanguagesToolStripMenuItem.Click
        FrmLanguage.ShowDialog()
    End Sub


    Private Sub CheckForUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckForUpdateToolStripMenuItem.Click
        ' MsgBox(Val(Application.ProductVersion.Replace(".", "")))
        MsgBox("No update available", vbInformation, Application.ProductName)
    End Sub

    Private Sub MnStripMain_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MnStripMain.ItemClicked

    End Sub

    <Obsolete>
    Private Sub LicenseTimer_Tick(sender As Object, e As EventArgs) Handles LicenseTimer.Tick
        If Not Me.Visible Then
            Exit Sub
        End If

        LicenseTimer.Enabled = False

        If Not RequestLicenseAfter Then
            If LicenseMode Then
                CheckLicense()
                LabelRemaning.Visible = True
                LabelRemaning.Text = TotalDays & " Remaning days"
            End If
        Else
            If Val(GetSetting(Application.ProductName, "sfts", "dd", "")) >= ClsLicence.GetServerDate Then
                If LicenseMode Then
                    CheckLicense()
                    LabelRemaning.Visible = True
                    LabelRemaning.Text = TotalDays & " Remaning days"
                End If
            End If
        End If
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Try
            If ShowBranding Then
                Process.Start(HelpLink)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ViewLicenseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewLicenseToolStripMenuItem.Click
        If LicenseMode Then
            FrmLicense.ViewMode = 3
            FrmLicense.ShowDialog()
        Else
            MsgBox("license not required...", vbInformation, Application.ProductName)
        End If

    End Sub
    Private Sub LinkLabel2_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Try
            'Process.Start("http://wa.me/96171339282")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ChannelsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChannelsToolStripMenuItem.Click
        FrmAccounts.Show()
    End Sub

    Private Sub ClearAttachmentListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearAttachmentListToolStripMenuItem.Click
        If MsgBox(GetLangbyKey("Are you sure you want to clear attachment list"), vbQuestion + MsgBoxStyle.YesNo, Application.ProductName) = vbYes Then
            LstMedia.Items.Clear()
        End If
    End Sub
    Private Sub Button17_MouseDown(sender As Object, e As MouseEventArgs) Handles Button17.MouseDown
        ContextMenuStripImports.Show()
        ContextMenuStripImports.Left = System.Windows.Forms.Control.MousePosition.X
        ContextMenuStripImports.Top = System.Windows.Forms.Control.MousePosition.Y
    End Sub

    Private Sub ExportContactsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportContactsToolStripMenuItem.Click
        If LstNumbers.Items.Count > 0 Then
            SaveFileDialog1.Filter = "*.txt|*.txt"
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                Dim li As ListViewItem
                Dim t As String = ""
                For Each li In LstNumbers.Items

                    t = t & li.Text & vbTab & li.SubItems(1).Text & vbNewLine

                Next
                Dim sw As New IO.StreamWriter(SaveFileDialog1.FileName)
                sw.Write(t)
                sw.Close()
                sw.Dispose()
            End If


        End If
        Statistics()
    End Sub

    Private Sub ClearListToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ClearListToolStripMenuItem2.Click, ClearListToolStripMenuItem.Click
        If LstNumbers.Items.Count = 0 Then
            Exit Sub
        End If
        If MsgBox(GetLangbyKey("Messages.CLEAR_LIST"), MsgBoxStyle.Question + MsgBoxStyle.YesNo, ApplicationTitle) = vbYes Then
            LstNumbers.Items.Clear()
        End If

        Statistics()
    End Sub

    Private Sub InsertCountryCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertCountryCodeToolStripMenuItem.Click


        If FrmInput.ShowDialog = DialogResult.OK Then
            Dim li As ListViewItem
            For Each li In LstNumbers.Items
                li.SubItems(1).Text = FrmInput.CountryCode & li.SubItems(1).Text
                li.Tag = FrmInput.CountryCode & li.Tag
            Next
        End If
    End Sub
    Private Sub Button18_MouseDown(sender As Object, e As MouseEventArgs) Handles Button18.MouseDown
        ContextMenuReceivedMsg.Show(sender, e.Location)
    End Sub

    Private Sub ExportMessagesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportMessagesToolStripMenuItem.Click
        If ListReceivedMessages.Items.Count = 0 Then
            Exit Sub
        End If

        Dim dlg As New SaveFileDialog
        dlg.Filter = "*.txt|*.txt"
        If dlg.ShowDialog = DialogResult.OK Then
            Dim result As String = ""
            Dim li As ListViewItem
            For Each li In ListReceivedMessages.Items
                result = result & li.SubItems(0).Text & vbTab & li.SubItems(1).Text & vbTab & li.SubItems(2).Text & vbNewLine
            Next
            IO.File.WriteAllText(dlg.FileName, result)
        End If

    End Sub

    Private Sub ClearMessageToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ClearMessageToolStripMenuItem1.Click
        ListReceivedMessages.Items.Clear()
    End Sub
    Private Sub LstNumbers_KeyUp(sender As Object, e As KeyEventArgs) Handles LstMedia.KeyUp, LstNumbers.KeyUp, ListReceivedMessages.KeyUp
        If e.KeyCode = Keys.Delete Then
            For Each li As ListViewItem In sender.SelectedItems
                li.Remove()
            Next
        End If
    End Sub

    Private WithEvents StatusChecker As New Forms.Timer With {.Interval = 1000, .Enabled = True}
    Private Sub StatusChecker_OnTick() Handles StatusChecker.Tick
        Task.Run(Sub()

                     Dim PingCount As Integer = 0
                     For i = 1 To 6
                         Try
                             If My.Computer.Network.Ping("172.217.18.142") Then PingCount += 1
                         Catch
                         End Try

                     Next
                     Status.IsInternetConnected = PingCount > 3


                 End Sub)
        LblIsConnected.Text = If(Status.IsInternetConnected, "Connected", "Disconnected")
    End Sub

    Private Sub PDFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PDFToolStripMenuItem.Click
        Dim OpenDlg As New OpenFileDialog()
        OpenDlg.Filter = "*.pdf|*.pdf"
        OpenDlg.Multiselect = True
        If OpenDlg.ShowDialog() = DialogResult.OK Then
            Dim fLi As ListViewItem
            For Each _file As String In OpenDlg.FileNames


                If GetFileSize(_file) > 1 Then
                    If MsgBox("The file you are trying to add is larger than 1 MB ,and may it will cause error or system will not deliver it.do you want proceed ? ", vbYesNo + vbCritical, Application.ProductName) = vbNo Then
                        GoTo skip
                    End If
                End If


                fLi = New ListViewItem
                fLi.Tag = _file
                Dim a() As String = Split(_file, "\")
                fLi.Text = a(UBound(a))
                fLi.SubItems.Add("pdf")
                fLi.SubItems.Add("")
                fLi.ImageIndex = 2
                If IsMediaAdded(fLi) Then

                    If GetFileSize(_file) > 1 Then
                        If MsgBox("The file you are trying to add is larger than 1 MB ,
                                      and may it will cause error or system will not deliver it.
                                      do you want proceed ? ", vbYesNo + vbCritical, Application.ProductName) = vbNo Then
                            Exit Sub
                        End If

                    End If

                    If MsgBox(GetLangbyKey("The_file_that_you_want_to_add_its_already_available_do_you_want_add_it_anyway"), vbQuestion + vbYesNo, Application.ProductName) = vbYes Then
                        LstMedia.Items.Add(fLi)
                    End If
                Else
                    LstMedia.Items.Add(fLi)
                End If

skip:
            Next
        End If
    End Sub

    Private Sub CreateNewProfileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateNewProfileToolStripMenuItem.Click
        FrmProfile.ShowDialog()
    End Sub

    Public Function GetFileSize(ByVal FileName As String) As Double
        Dim myFile As New FileInfo(FileName)
        Return ((myFile.Length) / 1024) / 1024
    End Function
    Private Sub AudioMessageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AudioMessageToolStripMenuItem.Click
        Dim OpenDlg As New OpenFileDialog()
        OpenDlg.Filter = "*.ogg|*.ogg"
        OpenDlg.Multiselect = True
        If OpenDlg.ShowDialog() = DialogResult.OK Then
            Dim fLi As ListViewItem
            For Each _file As String In OpenDlg.FileNames

                If GetFileSize(_file) > 1 Then
                    If MsgBox("The Video you are trying to add is larger than 1 MB ,and may it will cause error or system will not deliver it.do you want proceed ? ", vbYesNo + vbCritical, Application.ProductName) = vbNo Then
                        GoTo skip
                    End If
                End If

                fLi = New ListViewItem
                fLi.Tag = _file
                Dim a() As String = Split(_file, "\")
                fLi.Text = a(UBound(a))
                fLi.SubItems.Add("Audio")
                fLi.SubItems.Add("")
                fLi.ImageIndex = 2
                If IsMediaAdded(fLi) Then


                    If MsgBox(GetLangbyKey("The_file_that_you_want_to_add_its_already_available_do_you_want_add_it_anyway"), vbQuestion + vbYesNo, Application.ProductName) = vbYes Then
                        LstMedia.Items.Add(fLi)
                    End If
                Else
                    LstMedia.Items.Add(fLi)
                End If
skip:
            Next
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs)
        FrmIntances.Show()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs)
        CurrentCatalog = ""
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)
        FrmCatalogBuilder.ShowDialog()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs)
        Dim dg As New OpenFileDialog
        dg.Filter = "*.catalog|*.catalog"
        If dg.ShowDialog = DialogResult.OK Then
            Try
                CurrentCatalog = IO.File.ReadAllText(dg.FileName)
            Catch ex As Exception
                MsgBox(ex.Message, vbCritical, Application.ProductName)
            End Try

        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs)
        Dim dg As New OpenFileDialog
        dg.Filter = "*.buttons|*.buttons"
        If dg.ShowDialog = DialogResult.OK Then
            Try
                CurrentButtonFileName = dg.FileName
            Catch ex As Exception
                MsgBox(ex.Message, vbCritical, Application.ProductName)
            End Try

        End If
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs)
        If CurrentButtonFileName <> "" Then
            FrmInteactiveButtonsBuilder.OpenButtonsFile(CurrentButtonFileName)
        End If
        FrmInteactiveButtonsBuilder.ShowDialog()
        CurrentButtonFileName = FrmInteactiveButtonsBuilder.buttonFileName
    End Sub
    Private Sub Button12_Click(sender As Object, e As EventArgs)
        CurrentButtonFileName = ""
        FrmInteactiveButtonsBuilder.ListView1.Items.Clear()
    End Sub
    Private Sub ToolStripMenuItem26_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem26.Click
        FrmEngager.Show()
    End Sub
    'center form on the middle of screen 
    Private Sub FrmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Maximized Then
            BtnMax.Text = "2"
        Else
            BtnMax.Text = "1"
        End If
        Dim x As Integer = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Dim y As Integer = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
        Me.Location = New Point(x, y)
    End Sub

    Private Sub ResetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetToolStripMenuItem.Click
        KillProcess("msedgewebview2")
    End Sub
    ' create function to kill process 
    Public Sub KillProcess(ByVal ProcessName As String)
        Dim p() As Process = Process.GetProcessesByName(ProcessName)
        For Each pr As Process In p
            pr.Kill()
        Next
        Application.DoEvents()
        System.Threading.Thread.Sleep(1000)
        Application.DoEvents()
        DeleteDirectory(CurrentWaProfile)
        MsgBox("Reset done, application will close", vbInformation, Application.ProductName)
        End
    End Sub
    ' create function to delete directory 
    Public Sub DeleteDirectory(ByVal target_dir As String)
        Dim fileEntries As String() = Directory.GetFiles(target_dir)
        Dim fileName As String
        For Each fileName In fileEntries
            IO.File.Delete(fileName)
        Next
        Dim subdirectoryEntries As String() = Directory.GetDirectories(target_dir)
        Dim subdirectory As String
        For Each subdirectory In subdirectoryEntries
            DeleteDirectory(subdirectory)
        Next
        Directory.Delete(target_dir, False)
    End Sub

    Private Sub TxtMsg_TextChanged(sender As Object, e As EventArgs) Handles TxtMsg.TextChanged

    End Sub

    Private Sub CatalogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CatalogToolStripMenuItem.Click
        ReturnedCatalogFileName = ""
        FrmCatalogBuilder.ShowDialog()

        If ReturnedCatalogFileName <> "" Then
            Dim fLi As New ListViewItem
            fLi.Tag = ReturnedCatalogFileName
            fLi.Text = Path.GetFileName(ReturnedCatalogFileName)
            fLi.SubItems.Add("Catalog")
            fLi.SubItems.Add("")
            fLi.ImageIndex = 3
            If IsMediaAdded(fLi) Then
                If MsgBox(GetLangbyKey("The_file_that_you_want_to_add_its_already_available_do_you_want_add_it_anyway"), vbQuestion + vbYesNo, Application.ProductName) = vbYes Then
                    LstMedia.Items.Add(fLi)
                End If
            Else
                LstMedia.Items.Add(fLi)
            End If
        End If
    End Sub

    Private Sub ImportSavedCatalogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportSavedCatalogToolStripMenuItem.Click
        Dim dg As New OpenFileDialog
        dg.Filter = "*.catalog|*.catalog"
        If dg.ShowDialog = DialogResult.OK Then

            Dim fLi As New ListViewItem
            fLi.Tag = dg.FileName
            fLi.Text = Path.GetFileName(dg.FileName)
            fLi.SubItems.Add("Catalog")
            fLi.SubItems.Add("")
            fLi.ImageIndex = 3
            If IsMediaAdded(fLi) Then
                If MsgBox(GetLangbyKey("The_file_that_you_want_to_add_its_already_available_do_you_want_add_it_anyway"), vbQuestion + vbYesNo, Application.ProductName) = vbYes Then
                    LstMedia.Items.Add(fLi)
                End If
            Else
                LstMedia.Items.Add(fLi)
            End If
        End If
    End Sub

    Private Sub MissedCallListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MissedCallListToolStripMenuItem.Click
        For Each li As ListViewItem In LstNumbers.Items
            FrmCalls.DestinationsList.Add(li.Tag)
        Next
        FrmCalls.Show()
    End Sub

    Private Sub EnableChatGPTReplyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnableChatGPTReplyToolStripMenuItem.Click
        EnableChatGPTReplyToolStripMenuItem.Checked = Not EnableChatGPTReplyToolStripMenuItem.Checked
        SaveSetting(Application.ProductName, "ChatGPT", "Enable", If(EnableChatGPTReplyToolStripMenuItem.Checked, "1", "0"))
    End Sub

    Private Sub ChatGPTSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChatGPTSettingsToolStripMenuItem.Click
        FrmGPT.ShowDialog()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Try
            FrmMainButtons.FileName = "default"
            FrmMainButtons.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, ApplicationTitle)
        End Try
    End Sub

    Private Sub Panel10_Paint(sender As Object, e As PaintEventArgs) Handles Panel10.Paint

    End Sub

    Private Sub Panel14_Paint(sender As Object, e As PaintEventArgs) Handles Panel14.Paint

    End Sub
End Class