Imports System.Text.RegularExpressions

Public Class FrmAddAccount
    Private Sub FrmAddAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try : ThemeManager.ApplyTheme(Me) : Catch : End Try

    End Sub
    ' create a function to accept only alphanumeric
    Function IsAlphaNumeric(ByVal strToCheck As String) As Boolean
        Dim strPattern As String
        strPattern = "^[a-zA-Z0-9]*$"
        If Regex.IsMatch(strToCheck, strPattern) Then
            IsAlphaNumeric = True
        Else
            IsAlphaNumeric = False
        End If
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not IsAlphaNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If String.IsNullOrEmpty(TextBox1.Text) Then
            TextBox1.Focus()
            Exit Sub
        End If
        If IsChannelExist(TextBox1.Text) Then
            MsgBox("Channel Name already exist please choose another name", vbCritical, Application.ProductName)
            TextBox1.SelectAll()
            TextBox1.Focus()
            Exit Sub
        End If

        Dim NewWa As New WaChannel
        NewWa.AccountName = TextBox1.Text
        NewWa.WA = New WAWebview2
        NewWa.WA.Int(NewWa.AccountName)
        WaWebviewChannels.Add(NewWa)
        FrmAccounts.AddBrowsertoTab(NewWa)
        SaveChannels()
        Me.Close()
    End Sub

    Private Sub FrmAddAccount_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Dispose()
    End Sub
End Class
