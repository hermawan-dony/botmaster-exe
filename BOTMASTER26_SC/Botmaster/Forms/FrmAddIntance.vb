Public Class FrmAddIntance
    Public InstanceName As String
    Private Sub FrmAddIntance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = ""
    End Sub
    Private Function IsValidName(ByVal Name As String) As Boolean
        Dim InvalidChars As String = "_abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim result As Boolean = True
        For Each NameChars As Char In Name
            If Not InvalidChars.Contains(NameChars) Then
                result = False
            End If
        Next
        Return result
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If IsValidName(TextBox1.Text) Then
            InstanceName = TextBox1.Text
            Me.Close()
        Else
            MsgBox("Name contains invalid chars name should be alphanumeric only.", vbCritical, Application.ProductName)
            TextBox1.SelectAll()
            TextBox1.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        InstanceName = ""
        Me.Close()
    End Sub
End Class