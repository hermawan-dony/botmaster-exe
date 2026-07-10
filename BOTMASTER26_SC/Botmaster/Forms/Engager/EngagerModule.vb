Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Security.Cryptography
Imports System.Text

Module EngagerModule

    Public EWhatsAppIntances As New List(Of EngagerInstance)
    Public EWhatsIntance As EngagerInstance
    Public Sub AddForm(ByVal frm As Form)
        FrmEngager.Panel4.Controls.Clear()
        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Visible = True
        frm.BackColor = Color.White
        FrmEngager.Panel4.Controls.Add(frm)
    End Sub
    Public Async Sub AddWhatsAppAccount()
        FrmAddEngagerInstance.ShowDialog()
        Dim ProfName As String = FrmAddEngagerInstance.AccountName

        If ProfName <> "" Then
            EWhatsIntance = New EngagerInstance
            EWhatsIntance.Name = ProfName
            Await EWhatsIntance.InitializeAsync()
            EWhatsAppIntances.Add(EWhatsIntance)
            PopulateAccounts()
        End If
    End Sub


    Public Function IsAccountExist(ByVal AccountName As String) As Boolean
        Dim Result As Boolean = False
        For Each instance As EngagerInstance In EWhatsAppIntances
            If instance.Name.ToLower = AccountName.ToLower Then
                Result = True
            End If
        Next
        Return Result
    End Function
    Public Sub PopulateAccounts()
        For Each instance As EngagerInstance In EWhatsAppIntances
            If Not IsTabExist(instance.Name) Then
                Dim waTab As New TabPage
                waTab.Text = instance.Name
                waTab.Name = instance.Name
                waTab.Controls.Add(instance)
                instance.Visible = True
                instance.Dock = DockStyle.Fill
                FrmWhatsApp.TabControl1.TabPages.Add(waTab)
                FrmWhatsApp.TabControl1.SelectTab(waTab)
            End If
        Next
    End Sub
    Public Function IsTabExist(ByVal TabName As String) As Boolean
        Dim Result As Boolean = False
        For Each _tab As TabPage In FrmWhatsApp.TabControl1.TabPages
            If _tab.Name = TabName Then
                Result = True
            End If
        Next
        Return Result
    End Function
    Public Sub RemoveInstance(ByVal InstanceName As String)
        Dim foundInstance As EngagerInstance = Nothing
        For Each instance As EngagerInstance In EWhatsAppIntances
            If instance.Name = InstanceName Then
                foundInstance = instance
                Exit For
            End If
        Next
        EWhatsAppIntances.Remove(foundInstance)
        foundInstance.Dispose()
        Dim FoundTab As TabPage = Nothing
        For Each tab As TabPage In FrmWhatsApp.TabControl1.TabPages
            If tab.Name = InstanceName Then
                FoundTab = tab
            End If
        Next
        FrmWhatsApp.TabControl1.TabPages.Remove(FoundTab)
        SaveInstances()
    End Sub
    Public Sub SaveInstances()
        Dim Result As String = ""
        For Each instance As EngagerInstance In EWhatsAppIntances
            Result = Result & instance.Name & vbNewLine
        Next
        SaveSetting(Application.ProductName, "SavedInstances", "names", Result)


    End Sub
    Public Async Sub LoadInstances()
        Dim RawInstances As String = GetSetting(Application.ProductName, "SavedInstances", "names", "")
        For Each InstanceName In RawInstances.Split(vbNewLine)
            If InstanceName.Length > 1 Then
                EWhatsIntance = New EngagerInstance
                EWhatsIntance.Name = InstanceName.Trim
                Await EWhatsIntance.InitializeAsync()
                EWhatsAppIntances.Add(EWhatsIntance)

            End If
        Next
        PopulateAccounts()
    End Sub
    Public Async Sub Send()
        Try
            ' Get logged-in instances
            Dim _WhatsAppIntances As New List(Of EngagerInstance)
            For Each instance As EngagerInstance In EWhatsAppIntances
                If Await instance.IsLoggedIn Then
                    _WhatsAppIntances.Add(instance)
                End If
            Next

            ' Validate we have enough instances
            If _WhatsAppIntances.Count < 2 Then
                Debug.WriteLine("Send: Need at least 2 logged-in instances to send messages")
                Return
            End If

            ' Validate we have messages
            If FrmMessages.ListBox1.Items.Count = 0 Then
                Debug.WriteLine("Send: No messages available to send")
                Return
            End If

            ' Select two different random instances (non-blocking)
            Dim rnd As New Random()
            Dim x As Integer = rnd.Next(0, _WhatsAppIntances.Count)
            Dim y As Integer

            ' Ensure y is different from x
            Do
                y = rnd.Next(0, _WhatsAppIntances.Count)
            Loop While y = x

            ' Select random message
            Dim message As String = FrmMessages.ListBox1.Items(rnd.Next(0, FrmMessages.ListBox1.Items.Count)).ToString()
            message = message.Replace("\", "")
            message = message.Replace("'", "")

            ' Get sender and receiver numbers
            Dim senderNumber As String = Replace(Await _WhatsAppIntances(x).GetMe, """", "")
            Dim receiverNumber As String = Replace(Await _WhatsAppIntances(y).GetMe, """", "")

            ' Add to ListView
            Dim li As New ListViewItem
            li.Text = Now.ToString()
            li.SubItems.Add(senderNumber)
            li.SubItems.Add(receiverNumber)
            li.SubItems.Add(message)
            FrmDashboard.ListView1.Items.Insert(0, li)

            ' Send message using async method
            Await _WhatsAppIntances(x).SendMessageAsync(receiverNumber, message)
            Debug.WriteLine($"Send: Message sent from {senderNumber} to {receiverNumber}")

        Catch ex As Exception
            Debug.WriteLine($"Send: Error - {ex.Message}")
        End Try
    End Sub

End Module
