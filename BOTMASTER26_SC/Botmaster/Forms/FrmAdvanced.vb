Public Class FrmAdvanced
    Private Sub BtnAddFamiliarAccount_Click(sender As Object, e As EventArgs) Handles BtnAddFamiliarAccount.Click
        FrmAddFamiliarAccount.ShowDialog()
        If FrmAddFamiliarAccount.AccountNumber <> "" Then
            LstFamiliarsNumbers.Items.Add(FrmAddFamiliarAccount.AccountNumber)
            SaveFriendContacts(LstFamiliarsNumbers)
        End If
    End Sub

    Private Sub BtnDeleteFamiliarAccount_Click(sender As Object, e As EventArgs) Handles BtnDeleteFamiliarAccount.Click
        If LstFamiliarsNumbers.Text = "" Then
            Exit Sub
        End If
        If MsgBox("Are you sure you want to delete this account?", vbQuestion + vbYesNo) = MsgBoxResult.Yes Then
            LstFamiliarsNumbers.Items.RemoveAt(LstFamiliarsNumbers.SelectedIndex)
            SaveFriendContacts(LstFamiliarsNumbers)
        End If
    End Sub

    Private Sub FrmAdvanced_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try : ThemeManager.ApplyTheme(Me) : Catch : End Try

        Me.BtnAddFamiliarAccount.Text = GetLangbyKey("FrmAdvanced.BtnAddFamiliarAccount")
        Me.BtnDeleteFamiliarAccount.Text = GetLangbyKey("FrmAdvanced.BtnDeleteFamiliarAccount")
        Me.BtnAddMessages.Text = GetLangbyKey("FrmAdvanced.BtnAddMessages")
        Me.BtnDeleteMessages.Text = GetLangbyKey("FrmAdvanced.BtnDeleteMessages")
        Me.Label2.Text = GetLangbyKey("FrmAdvanced.Label2")
        Me.Label10.Text = GetLangbyKey("FrmAdvanced.Label10")
        Me.Label3.Text = GetLangbyKey("FrmAdvanced.Label3")
        Me.Label1.Text = GetLangbyKey("FrmAdvanced.Label1")
        Me.Label7.Text = GetLangbyKey("FrmAdvanced.Label7")
        Me.Label8.Text = GetLangbyKey("FrmAdvanced.Label8")
        Me.Button5.Text = GetLangbyKey("FrmAdvanced.Button5")
        Me.CheckBox2.Text = GetLangbyKey("FrmAdvanced.CheckBox2")
        Me.GroupBox4.Text = GetLangbyKey("FrmAdvanced.GroupBox4")
        Me.Label12.Text = GetLangbyKey("FrmAdvanced.Label12")
        Me.Label14.Text = GetLangbyKey("FrmAdvanced.Label14")
        Me.Label11.Text = GetLangbyKey("FrmAdvanced.Label11")
        Me.WaitFrom2.Text = GetLangbyKey("FrmAdvanced.WaitFrom2")
        Me.GroupBox5.Text = GetLangbyKey("FrmAdvanced.GroupBox5")
        Me.TabPage1.Text = GetLangbyKey("FrmAdvanced.TabPage1")
        Me.TabPage2.Text = GetLangbyKey("FrmAdvanced.TabPage2")
        Me.TabPage3.Text = GetLangbyKey("FrmAdvanced.TabPage3")
        Me.CheckBox1.Text = GetLangbyKey("FrmAdvanced.CheckBox1")
        Me.Label13.Text = GetLangbyKey("FrmAdvanced.Label13")
        Me.Label4.Text = GetLangbyKey("FrmAdvanced.Label4")
        Me.Label5.Text = GetLangbyKey("FrmAdvanced.Label5")
        Me.Label15.Text = GetLangbyKey("FrmAdvanced.Label15")
        Me.Text = GetLangbyKey("FrmAdvanced")

        GetFriendsContacts(LstFamiliarsNumbers)
        GetMessages(LstMessages)

        GetSendingSettings()
        
        Try
            Dim newTab As New TabPage("Appearance & Startup")
            newTab.BackColor = System.Drawing.Color.White
            
            Dim grp As New GroupBox()
            grp.Text = "Application Appearance & Startup"
            grp.Location = New Point(26, 26)
            grp.Size = New Size(400, 130)

            Dim lblTheme As New Label()
            lblTheme.Text = "Theme:"
            lblTheme.Location = New Point(15, 30)
            lblTheme.AutoSize = True
            grp.Controls.Add(lblTheme)

            Dim cmbTheme As New ComboBox()
            cmbTheme.Name = "CmbTheme"
            cmbTheme.Items.Add("Cyberpunk")
            cmbTheme.Items.Add("Enterprise Dark")
            cmbTheme.Location = New Point(80, 27)
            cmbTheme.DropDownStyle = ComboBoxStyle.DropDownList
            cmbTheme.Text = GetSetting(Application.ProductName, "Settings", "AppTheme", "Cyberpunk")
            grp.Controls.Add(cmbTheme)

            Dim chkAutoRun As New CheckBox()
            chkAutoRun.Name = "ChkAutoRun"
            chkAutoRun.Text = "Run at Windows Startup"
            chkAutoRun.Location = New Point(15, 65)
            chkAutoRun.AutoSize = True
            chkAutoRun.Checked = CBool(GetSetting(Application.ProductName, "Settings", "AutoRun", "False"))
            grp.Controls.Add(chkAutoRun)

            Dim chkAutoHide As New CheckBox()
            chkAutoHide.Name = "ChkAutoHide"
            chkAutoHide.Text = "Start Minimized (System Tray)"
            chkAutoHide.Location = New Point(15, 95)
            chkAutoHide.AutoSize = True
            chkAutoHide.Checked = CBool(GetSetting(Application.ProductName, "Settings", "AutoStartMinimized", "False"))
            grp.Controls.Add(chkAutoHide)

            newTab.Controls.Add(grp)
            Me.TabControl1.TabPages.Add(newTab)
        Catch ex As Exception
        End Try
        GroupBox1.Enabled = CheckBox1.Checked
        GroupBox2.Enabled = CheckBox2.Checked




    End Sub
    Public Sub SaveFriendContacts(ByVal FriendLst As ListBox)
        Dim a As String
        Dim allFriends As String = ""
        For Each a In FriendLst.Items
            allFriends = allFriends & a & vbNewLine
        Next


        IO.File.WriteAllText(ClsSpecialDirectories.Getdata & "commonList.data", allFriends)

    End Sub
    Public Sub GetFriendsContacts(ByRef FriendLst As ListBox)

        If IO.File.Exists(ClsSpecialDirectories.Getdata & "commonList.data") Then
            FriendLst.Items.Clear()
            For Each num As String In IO.File.ReadAllLines(ClsSpecialDirectories.Getdata & "commonList.data")
                FriendLst.Items.Add(num)
            Next
        End If
    End Sub
    Public Sub SaveMessages(ByVal msgLst As ListBox)
        Dim allmessages As String = ""
        Dim a As String
        For Each a In msgLst.Items
            allmessages = allmessages & a & vbNewLine
        Next


        IO.File.WriteAllText(ClsSpecialDirectories.Getdata & "commonMessage.data", allmessages)
    End Sub
    Public Sub GetMessages(ByRef msgLst As ListBox)
        If IO.File.Exists(ClsSpecialDirectories.Getdata & "commonMessage.data") Then
            msgLst.Items.Clear()
            For Each msg As String In IO.File.ReadAllLines(ClsSpecialDirectories.Getdata & "commonMessage.data")
                msgLst.Items.Add(msg)
            Next
        End If
    End Sub

    Private Sub BtnAddMessages_Click(sender As Object, e As EventArgs) Handles BtnAddMessages.Click
        FrmAddMessage.ShowDialog()
        SaveMessages(LstMessages)
    End Sub

    Private Sub BtnDeleteMessages_Click(sender As Object, e As EventArgs) Handles BtnDeleteMessages.Click
        If LstMessages.Text = "" Then
            Exit Sub
        End If
        If MsgBox("Are you sure you want to delete this message?", vbQuestion + vbYesNo) = MsgBoxResult.Yes Then
            LstMessages.Items.RemoveAt(LstMessages.SelectedIndex)
            SaveMessages(LstMessages)
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
        GroupBox1.Enabled = CheckBox1.Checked
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        GroupBox2.Enabled = CheckBox2.Checked
    End Sub
    Public Sub SaveSendingSettings()
        Try
            Dim cmbTheme As ComboBox = DirectCast(Me.TabControl1.Controls.Find("CmbTheme", True).FirstOrDefault(), ComboBox)
            Dim chkAutoRun As CheckBox = DirectCast(Me.TabControl1.Controls.Find("ChkAutoRun", True).FirstOrDefault(), CheckBox)
            Dim chkAutoHide As CheckBox = DirectCast(Me.TabControl1.Controls.Find("ChkAutoHide", True).FirstOrDefault(), CheckBox)

            If cmbTheme IsNot Nothing Then
                SaveSetting(Application.ProductName, "Settings", "AppTheme", cmbTheme.Text)
            End If
            If chkAutoRun IsNot Nothing Then
                SaveSetting(Application.ProductName, "Settings", "AutoRun", chkAutoRun.Checked.ToString())
                Try
                    Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
                    If chkAutoRun.Checked Then
                        regKey.SetValue(Application.ProductName, Application.ExecutablePath)
                    Else
                        regKey.DeleteValue(Application.ProductName, False)
                    End If
                Catch
                End Try
            End If
            If chkAutoHide IsNot Nothing Then
                SaveSetting(Application.ProductName, "Settings", "AutoStartMinimized", chkAutoHide.Checked.ToString())
            End If
        Catch ex As Exception
        End Try

        SaveSetting(ApplicationTitle, "SendingConfig", "Speed", CmbSpeed.SelectedIndex)
        SaveSetting(ApplicationTitle, "SendingConfig", "DelayStart", WaitFrom.Value)
        SaveSetting(ApplicationTitle, "SendingConfig", "DelayEnd", WaitTo.Value)
        SaveSetting(ApplicationTitle, "SendingConfig", "ActivateDialog", CheckBox1.Checked.ToString)
        SaveSetting(ApplicationTitle, "SendingConfig", "DialogAfter", NumericUpDown3.Value)
        SaveSetting(ApplicationTitle, "SendingConfig", "DialogWait", NumericUpDown4.Value)
        SaveSetting(ApplicationTitle, "SendingConfig", "DialoCount", NumericUpDown6.Value)
        SaveSetting(ApplicationTitle, "SendingConfig", "ActivateSleep", CheckBox2.Checked.ToString)
        SaveSetting(ApplicationTitle, "SendingConfig", "SleepAfter", NumericUpDown1.Value)
        SaveSetting(ApplicationTitle, "SendingConfig", "SleepFor", NumericUpDown2.Value)

    End Sub
    Public Function GetSendingSettings() As ClsSendingConfig
        Dim _t As New ClsSendingConfig

        _t.Speed = Val(GetSetting(ApplicationTitle, "SendingConfig", "Speed", "3"))
        CmbSpeed.SelectedIndex = _t.Speed

        WaitFrom.Value = Val(GetSetting(ApplicationTitle, "SendingConfig", "DelayStart", "1"))
        WaitTo.Value = Val(GetSetting(ApplicationTitle, "SendingConfig", "DelayEnd", "2"))

        _t.ActivateDialog = CBool(GetSetting(ApplicationTitle, "SendingConfig", "ActivateDialog", "false"))
        CheckBox1.Checked = _t.ActivateDialog

        _t.DialogAfter = Val(GetSetting(ApplicationTitle, "SendingConfig", "DialogAfter", 5))

        NumericUpDown3.Value = _t.DialogAfter


        _t.DialogWait = Val(GetSetting(ApplicationTitle, "SendingConfig", "DialogWait", 1))
        NumericUpDown4.Value = _t.DialogWait

        _t.DialoCount = Val(GetSetting(ApplicationTitle, "SendingConfig", "DialoCount", 15))
        NumericUpDown6.Value = _t.DialoCount

        _t.ActivateSleep = CBool(GetSetting(ApplicationTitle, "SendingConfig", "ActivateSleep", "false"))
        CheckBox2.Checked = _t.ActivateSleep

        _t.SleepAfter = GetSetting(ApplicationTitle, "SendingConfig", "SleepAfter", 20)
        NumericUpDown1.Value = _t.SleepAfter

        _t.SleepFor = GetSetting(ApplicationTitle, "SendingConfig", "SleepFor", 5)
        NumericUpDown2.Value = _t.SleepFor

        _t.SendingMode = CBool(GetSetting(ApplicationTitle, "SendingConfig", "SendingMode", "true"))



        Return New ClsSendingConfig
    End Function

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        SaveSendingSettings()

        Me.Close()
    End Sub

    Private Sub FrmAdvanced_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub CheckBox1_CheckedChanged_1(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        GroupBox1.Enabled = CheckBox1.Checked
    End Sub
End Class
