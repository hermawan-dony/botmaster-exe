Public Class FrmAddEngagerInstance
    Private action As Integer
    Public AccountName As String
    Private Sub FrmAddIntance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        action = 0
        TextBox1.Text = ""
    End Sub
    Public Function IsValidName(ByVal Name As String) As Boolean
        Dim ValidChars As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_"
        Dim Result As Boolean = True
        For Each NameChar In Name
            If Not ValidChars.Contains(NameChar) Then
                Result = False
            End If
        Next
        Return Result
    End Function
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        action = 0
        Me.Close()
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If Not IsValidName(TextBox1.Text) Then
            MsgBox("Account Name contains invalid characters 'Name should be only alphanumeric'", vbCritical, Application.ProductName)
            TextBox1.SelectAll()
            TextBox1.Focus()
            Exit Sub
        End If

        If IsAccountExist(TextBox1.Text) Then
            MsgBox("Account Name already Exist, Please choose another one", vbCritical, Application.ProductName)
            TextBox1.SelectAll()
            TextBox1.Focus()
            Exit Sub
        End If

        action = 1
        Me.Close()
    End Sub
    Private Sub FrmAddIntance_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If action = 0 Then
            AccountName = ""
        Else
            AccountName = TextBox1.Text
        End If
    End Sub
End Class