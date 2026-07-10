Public Class FrmSendingType
    Public Result As Integer
    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    <Obsolete>
    Private Sub FrmSendingType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try : ThemeManager.ApplyTheme(Me) : Catch : End Try
        ApplyColor(Me)
        If LicenseMode Then
            CheckLicense()
            If Not AllowSending Then
                MsgBox("Not allowed to use this option , please check your vendor.", vbInformation, ApplicationTitle)
                Me.Close()
                Exit Sub
            End If
        End If
        LoadList2()
        RadioButtonSafeMode.Checked = True
        Result = 0
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        If RadioButtonSafeMode.Checked Then
            Result = 1
        ElseIf RadioButtonBlindMode.Checked Then
            Result = 2
        Else
            Result = 3
        End If
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        ListView1.Enabled = RadioButton1.Checked
        LinkLabel1.Enabled = RadioButton1.Checked
        LinkLabel2.Enabled = RadioButton1.Checked
        LinkLabel3.Enabled = RadioButton1.Checked
        LinkLabel4.Enabled = RadioButton1.Checked
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        For Each li As ListViewItem In ListView1.Items : li.Checked = True : Next
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        For Each li As ListViewItem In ListView1.Items : li.Checked = False : Next
    End Sub
    Public Async Sub LoadList2()
        ListView1.Items.Clear()
        For Each t As WaChannel In WaWebviewChannels
            Dim li As New ListViewItem
            li.Tag = t.WA
            li.Text = t.AccountName
            li.SubItems.Add(Replace(Await t.WA.GetMe, """", ""))
            li.SubItems.Add(If(Await t.WA.IsLoggedIn, "Logged In", "Not Active"))
            ListView1.Items.Add(li)
        Next

    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        LoadList2()
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        FrmAccounts.Show()
    End Sub
End Class
