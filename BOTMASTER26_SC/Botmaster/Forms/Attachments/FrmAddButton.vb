Public Class FrmAddButton
    Private Sub FrmAddButton_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try : ThemeManager.ApplyTheme(Me) : Catch : End Try
        ComboBox1.Text = "Reply"
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.Text
            Case "Reply"
                TextBox2.Text = ""
                TextBox2.Enabled = False
                Label3.Text = "Action:"
            Case "URL"
                TextBox2.Enabled = True
                Label3.Text = "URL:"
            Case "Phone"
                TextBox2.Enabled = True
                Label3.Text = "Phone:"
        End Select
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Please enter button text", vbCritical, Application.ProductName)
            TextBox1.Focus()
            Exit Sub
        End If
        If ComboBox1.Text = "URL" Then
            If TextBox2.Text = "" Then
                MsgBox("You have to enter URL", vbCritical, Application.ProductName)
                TextBox2.Focus()
                Exit Sub
            End If
            Dim tester As String = TextBox2.Text.ToLower
            tester = tester.Replace("https://", "http://")
            If Not tester.StartsWith("http://") Then
                MsgBox("Please enter a valid url start with http:// or https://", vbCritical, Application.ProductName)
                TextBox2.SelectAll()
                TextBox2.Focus()
                Exit Sub
            End If
        End If

        If ComboBox1.Text = "Phone" Then
            If TextBox2.Text = "" Then
                MsgBox("You have to enter Phone Number", vbCritical, Application.ProductName)
                TextBox2.Focus()
                Exit Sub
            End If
        End If

        If FrmInteactiveButtonsBuilder.ListView1.Items.Count > 5 Then
            MsgBox("You cannot add more than 5 buttons", vbCritical, Application.ProductName)
            Exit Sub
        End If
        Dim button As Object = Nothing
        Select Case ComboBox1.Text
            Case "Reply"
                button = New ReplyButton With {.id = FrmInteactiveButtonsBuilder.ListView1.Items.Count + 1, .text = TextBox1.Text, .reply = ""}
            Case "URL"
                button = New UrlButton With {.id = FrmInteactiveButtonsBuilder.ListView1.Items.Count + 1, .text = TextBox1.Text, .url = TextBox2.Text}
            Case "Phone"
                button = New PhoneButton With {.id = FrmInteactiveButtonsBuilder.ListView1.Items.Count + 1, .text = TextBox1.Text, .phoneNumber = TextBox2.Text}
        End Select

        Dim li As New ListViewItem
        li.Tag = button
        li.Text = ComboBox1.Text
        li.SubItems.Add(TextBox1.Text)
        li.SubItems.Add(TextBox2.Text)
        FrmInteactiveButtonsBuilder.ListView1.Items.Add(li)
        Me.Close()
    End Sub
End Class
Public Class UrlButton
    Public id As String
    Public url As String
    Public text As String
End Class
Public Class PhoneButton
    Public id As String
    Public phoneNumber As String
    Public text As String
End Class
Public Class ReplyButton
    Public id As String
    Public reply As String
    Public text As String
End Class
