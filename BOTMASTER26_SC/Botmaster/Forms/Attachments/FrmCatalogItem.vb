Public Class FrmCatalogItem
    Public ItemName As String
    Public ItemDescription As String
    Public action As Integer
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Enter Item name", vbCritical, Application.ProductName)
            TextBox1.Focus()
            Exit Sub
        End If
        ItemName = TextBox1.Text
        ItemDescription = TextBox2.Text
        Me.Close()
    End Sub

    Private Sub FrmCatalogItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If action = 1 Then
            ItemName = ""
            ItemDescription = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
        Else
            TextBox1.Text = ItemName
            TextBox2.Text = ItemDescription
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ItemName = ""
        ItemDescription = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        Me.Close()
    End Sub
End Class