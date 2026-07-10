Public Class FrmProfile
    Private Sub FrmProfile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If AllowedString(TextBox1.Text) Then
            CreateShortCut(Application.ExecutablePath, TextBox1.Text, TextBox1.Text.ToLower)
            Me.Close()
        Else
            MsgBox("Only letters and numbers are accepted , special characters not allowed.", vbCritical, Application.ProductName)
            TextBox1.Focus()
        End If
    End Sub

    Public Function AllowedString(ByVal Text As String) As Boolean
        Dim str As String = "abcdefjhijklmnopqrstuvwxyzABCDEFJHIJKLMNOPQRSTUVWXYZ0123456789_"
        Dim result As Boolean = True
        For Each tChr In Text
            If Not str.Contains(tChr) Then
                result = False
                Exit For
            End If
        Next
        Return result
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Function CreateShortCut(ByVal TargetName As String, ByVal ShortCutName As String, ByVal arg As String) As Boolean
        Dim oShell As Object
        Dim oLink As Object
        Try

            oShell = CreateObject("WScript.Shell")
            oLink = oShell.CreateShortcut(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & ShortCutName & ".lnk")

            oLink.TargetPath = TargetName
            oLink.Arguments = arg
            oLink.WindowStyle = 1
            oLink.Save()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub FrmProfile_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Dispose()
    End Sub
End Class