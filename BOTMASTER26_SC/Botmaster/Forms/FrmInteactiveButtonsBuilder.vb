Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class FrmInteactiveButtonsBuilder
    Public buttonFileName As String
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ListView1.SelectedItems.Count > 0 Then
            If Not IsNothing(ListView1.SelectedItems(0)) Then
                ListView1.Items.Remove(ListView1.SelectedItems(0))
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListView1.Items.Count = 0 Then
            MsgBox("You should add buttons", vbCritical, Application.ProductName)
            Exit Sub
        End If
        Dim t As New List(Of Object)

        For Each li As ListViewItem In ListView1.Items
            t.Add(li.Tag)
        Next
        Dim ButtonsPath As String = ClsSpecialDirectories.ButtonsFolder & GenerateFileName()
        File.WriteAllText(ButtonsPath, Newtonsoft.Json.JsonConvert.SerializeObject(t))

        buttonFileName = ButtonsPath
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim t As New List(Of Object)

        For Each li As ListViewItem In ListView1.Items
            t.Add(li.Tag)
        Next

        Dim dlg As New SaveFileDialog
        dlg.Filter = "*.buttons|*.buttons"
        If dlg.ShowDialog = DialogResult.OK Then
            File.WriteAllText(dlg.FileName, Newtonsoft.Json.JsonConvert.SerializeObject(t))
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim dlg As New OpenFileDialog
        dlg.Filter = "*.buttons|*.buttons"
        Dim ObjJson As String = ""
        If dlg.ShowDialog = DialogResult.OK Then
            ObjJson = removeJascriptControlChars(File.ReadAllText(dlg.FileName))
            Dim Buttons As List(Of Object) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of Object))(ObjJson)
            ListView1.Items.Clear()

            For Each button As JObject In Buttons
                Dim li As New ListViewItem

                li.Tag = removeJascriptControlChars(button.ToString)
                li.Text = ButtonType(button.ToString)
                li.SubItems.Add(button("text"))
                li.SubItems.Add(button(ButtonType(button.ToString)))
                ListView1.Items.Add(li)
            Next
        End If

    End Sub




    Public Sub OpenButtonsFile(ByVal FileName As String)

        Dim ObjJson As String = ""
        ObjJson = removeJascriptControlChars(File.ReadAllText(FileName))
        Dim Buttons As List(Of Object) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of Object))(ObjJson)
        ListView1.Items.Clear()


        For Each button As JObject In Buttons
            Dim li As New ListViewItem
            li.Tag = removeJascriptControlChars(button.ToString)
            li.Text = ButtonType(button.ToString)
            li.SubItems.Add(button("text"))
            li.SubItems.Add(button(ButtonType(button.ToString)))
            ListView1.Items.Add(li)
        Next


    End Sub

    Function ButtonType(ByVal t As String) As String
        Select Case True
            Case t.Contains("reply")
                Return "reply"
            Case t.Contains("url")
                Return "url"
            Case t.Contains("phoneNumber")
                Return "phoneNumber"
            Case Else
                Return "reply"
        End Select
    End Function
    Function GenerateFileName() As String
        Return $"buttons_{Now.ToString("yyyyMMddHHmmss")}.buttons"
    End Function

    Private Sub FrmInteactiveButtonsBuilder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try : ThemeManager.ApplyTheme(Me) : Catch : End Try

    End Sub
End Class
