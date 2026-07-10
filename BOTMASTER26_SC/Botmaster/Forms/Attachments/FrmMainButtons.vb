Imports Newtonsoft.Json

Public Class FrmMainButtons
    Private Reply As String = "{id: '{{VALUE}}',text: '{{TEXT}}'}"
    Private Code As String = "{code: '{{VALUE}}',text: '{{TEXT}}'}"
    Private Url As String = "{url: '{{VALUE}}',text: '{{TEXT}}'}"
    Private PhoneNumber As String = "{phoneNumber: '{{VALUE}}',text: '{{TEXT}}'}"
    Private Template As String = "{
                                      useInteractiveMessage: true, 
                                      buttons: [
                                             {{BUTTONS}}
                                      ],
                                      title: '{{TITLE}}', 
                                      footer: '{{FOOTER}}'
                                    }"
    Public FileName As String
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        BuildTemplate()
        Me.Close()
    End Sub
    Private Sub BuildTemplate()
        If TextBox2.Text = "" Then
            MsgBox("Please enter a title", vbCritical, Application.ProductName)
            Exit Sub
        End If
        If TextBox4.Text = "" Then
            MsgBox("Please enter a footer", vbCritical, Application.ProductName)
            Exit Sub
        End If

        If RadioButton1.Checked Then
            If Reply1.Text = "" And Reply2.Text = "" And Reply3.Text = "" Then
                MsgBox("Please enter at least one reply", vbCritical, Application.ProductName)
                Exit Sub
            End If
        Else
            If Action1.Text = "" And Action2.Text = "" And Action3.Text = "" Then
                MsgBox("Please enter at least one action", vbCritical, Application.ProductName)
                Exit Sub
            End If
        End If

        Template = Template.Replace("{{TITLE}}", TextBox2.Text)
        Template = Template.Replace("{{FOOTER}}", TextBox4.Text)
        Dim TempButton1, TempButton2, TempButton3 As String
        Dim TempButtons As String = ""
        Dim Key As String = ""
        If RadioButton1.Checked Then
            If Reply1.Text <> "" Then
                Select Case ComboReply1.Text
                    Case "Reply"
                        TempButton1 = Reply
                        TempButton1 = TempButton1.Replace("{{VALUE}}", Reply1.Text)
                        TempButton1 = TempButton1.Replace("{{TEXT}}", Reply1.Text)
                        TempButtons = TempButtons & TempButton1 & ","
                    Case "Code"
                        TempButton1 = Code
                        TempButton1 = TempButton1.Replace("{{VALUE}}", Reply1.Text)
                        TempButton1 = TempButton1.Replace("{{TEXT}}", Reply1.Text)
                        TempButtons = TempButtons & TempButton1 & ","
                End Select
            End If


            If Reply2.Text <> "" Then
                Select Case ComboReply2.Text
                    Case "Reply"
                        TempButton2 = Reply
                        TempButton2 = TempButton2.Replace("{{VALUE}}", Reply2.Text)
                        TempButton2 = TempButton2.Replace("{{TEXT}}", Reply2.Text)
                        TempButtons = TempButtons & TempButton2 & ","
                    Case "Code"
                        TempButton2 = Code
                        TempButton2 = TempButton2.Replace("{{VALUE}}", Reply2.Text)
                        TempButton2 = TempButton2.Replace("{{TEXT}}", Reply2.Text)
                        TempButtons = TempButtons & TempButton2 & ","
                End Select

            End If
            If Reply3.Text <> "" Then
                Select Case ComboReply3.Text
                    Case "Reply"
                        TempButton3 = Reply
                        TempButton3 = TempButton3.Replace("{{VALUE}}", Reply3.Text)
                        TempButton3 = TempButton3.Replace("{{TEXT}}", Reply3.Text)
                        TempButtons = TempButtons & TempButton3 & ","
                    Case "Code"
                        TempButton3 = Code
                        TempButton3 = TempButton3.Replace("{{VALUE}}", Reply3.Text)
                        TempButton3 = TempButton3.Replace("{{TEXT}}", Reply3.Text)
                        TempButtons = TempButtons & TempButton3 & ","
                End Select

            End If
        Else
            If Action1.Text <> "" Then
                Select Case comboaction1.Text
                    Case "URL"
                        TempButton1 = Url
                        TempButton1 = TempButton1.Replace("{{VALUE}}", Action1.Text)
                        TempButton1 = TempButton1.Replace("{{TEXT}}", TxtLabel1.Text)
                        TempButtons = TempButtons & TempButton1 & ","
                    Case "Phone Number"
                        TempButton1 = PhoneNumber
                        TempButton1 = TempButton1.Replace("{{VALUE}}", Action1.Text)
                        TempButton1 = TempButton1.Replace("{{TEXT}}", TxtLabel1.Text)
                        TempButtons = TempButtons & TempButton1 & ","
                End Select
            End If
            If Action2.Text <> "" Then
                Select Case comboaction2.Text
                    Case "URL"
                        TempButton2 = Url
                        TempButton2 = TempButton2.Replace("{{VALUE}}", Action2.Text)
                        TempButton2 = TempButton2.Replace("{{TEXT}}", TxtLabel2.Text)
                        TempButtons = TempButtons & TempButton2 & ","
                    Case "Phone Number"
                        TempButton2 = PhoneNumber
                        TempButton2 = TempButton2.Replace("{{VALUE}}", Action2.Text)
                        TempButton2 = TempButton2.Replace("{{TEXT}}", TxtLabel2.Text)
                        TempButtons = TempButtons & TempButton2 & ","
                End Select
            End If
            If Action3.Text <> "" Then
                Select Case comboaction3.Text
                    Case "URL"
                        TempButton3 = Url
                        TempButton3 = TempButton3.Replace("{{VALUE}}", Action3.Text)
                        TempButton3 = TempButton3.Replace("{{TEXT}}", TxtLabel3.Text)
                        TempButtons = TempButtons & TempButton3 & ","
                    Case "Phone Number"
                        TempButton3 = PhoneNumber
                        TempButton3 = TempButton3.Replace("{{VALUE}}", Action3.Text)
                        TempButton3 = TempButton3.Replace("{{TEXT}}", TxtLabel3.Text)
                        TempButtons = TempButtons & TempButton3 & ","
                End Select
            End If
        End If
        'TempButtons = TempButtons.Substring(0, TempButtons.Length - 1)
        TempButtons = TempButtons.TrimEnd(",")
        Template = Template.Replace("{{BUTTONS}}", TempButtons)

        IO.File.WriteAllText(ClsSpecialDirectories.ButtonsFolder() & FileName & ".txt", Template)
        Dim btnsTemplate As New ButtonTemplate
        btnsTemplate.Title = TextBox2.Text
        btnsTemplate.Footer = TextBox4.Text
        btnsTemplate.tButtons = New List(Of tButton)
        btnsTemplate.IsReply = RadioButton1.Checked
        btnsTemplate.Body = TextBox1.Text
        If RadioButton1.Checked Then
            If Reply1.Text <> "" Then
                Dim btn As New tButton
                btn.ButtonType = ComboReply1.Text
                btn.Value = Reply1.Text
                btn.Text = Reply1.Text
                btnsTemplate.tButtons.Add(btn)
            End If
            If Reply2.Text <> "" Then
                Dim btn As New tButton
                btn.ButtonType = ComboReply2.Text
                btn.Value = Reply2.Text
                btn.Text = Reply2.Text
                btnsTemplate.tButtons.Add(btn)
            End If
            If Reply3.Text <> "" Then
                Dim btn As New tButton
                btn.ButtonType = ComboReply3.Text
                btn.Value = Reply3.Text
                btn.Text = Reply3.Text
                btnsTemplate.tButtons.Add(btn)
            End If
        Else
            If Action1.Text <> "" Then
                Dim btn As New tButton
                btn.ButtonType = comboaction1.Text
                btn.Value = Action1.Text
                btn.Text = TxtLabel1.Text
                btnsTemplate.tButtons.Add(btn)
            End If
            If Action2.Text <> "" Then
                Dim btn As New tButton
                btn.ButtonType = comboaction2.Text
                btn.Value = Action2.Text
                btn.Text = TxtLabel2.Text
                btnsTemplate.tButtons.Add(btn)
            End If
            If Action3.Text <> "" Then
                Dim btn As New tButton
                btn.ButtonType = comboaction3.Text
                btn.Value = Action3.Text
                btn.Text = TxtLabel3.Text
                btnsTemplate.tButtons.Add(btn)
            End If
        End If
        IO.File.WriteAllText(ClsSpecialDirectories.ButtonsFolder() & FileName & ".json", JsonConvert.SerializeObject(btnsTemplate))

    End Sub


    Private Sub FrmMainButtons_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try : ThemeManager.ApplyTheme(Me) : Catch : End Try
        ComboReply1.SelectedIndex = 0
        ComboReply2.SelectedIndex = 0
        ComboReply3.SelectedIndex = 0
        comboaction1.SelectedIndex = 0
        comboaction2.SelectedIndex = 0
        comboaction3.SelectedIndex = 0


        If IO.File.Exists(ClsSpecialDirectories.ButtonsFolder() & FileName & ".json") Then
            Dim t As ButtonTemplate = JsonConvert.DeserializeObject(Of ButtonTemplate)(IO.File.ReadAllText(ClsSpecialDirectories.ButtonsFolder() & FileName & ".json"))
            If Not IsNothing(t) Then
                TextBox2.Text = t.Title
                TextBox4.Text = t.Footer
                TextBox1.Text = t.Body
                RadioButton1.Checked = t.IsReply
                RadioButton2.Checked = Not t.IsReply
                If t.IsReply Then
                    For Each b As tButton In t.tButtons
                        Select Case t.tButtons.IndexOf(b)
                            Case 0
                                Reply1.Text = b.Value
                                ComboReply1.Text = b.ButtonType
                            Case 1
                                Reply2.Text = b.Value
                                ComboReply2.Text = b.ButtonType
                            Case 2
                                Reply3.Text = b.Value
                                ComboReply3.Text = b.ButtonType
                        End Select
                    Next
                Else
                    For Each b As tButton In t.tButtons
                        Select Case t.tButtons.IndexOf(b)
                            Case 0
                                Action1.Text = b.Value
                                comboaction1.Text = b.ButtonType
                                TxtLabel1.Text = b.Text
                            Case 1
                                Action2.Text = b.Value
                                comboaction2.Text = b.ButtonType
                                TxtLabel2.Text = b.Text
                            Case 2
                                Action3.Text = b.Value
                                comboaction3.Text = b.ButtonType
                                TxtLabel3.Text = b.Text
                        End Select
                    Next
                End If
            End If

        End If
        If Not RadioButton1.Checked Then
            Reply1.Text = ""
            Reply2.Text = ""
            Reply3.Text = ""
        Else
            Action1.Text = ""
            Action2.Text = ""
            Action3.Text = ""
            TxtLabel1.Text = ""
            TxtLabel2.Text = ""
            TxtLabel3.Text = ""

        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Panel1.Enabled = RadioButton1.Checked
        Panel2.Enabled = RadioButton2.Checked
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Panel1.Enabled = RadioButton1.Checked
        Panel2.Enabled = RadioButton2.Checked
    End Sub
End Class
Public Class ButtonTemplate
    Public Property Title As String
    Public Property Footer As String
    Public Property tButtons As List(Of tButton)
    Public Property Body As String
    Public Property IsReply As Boolean
End Class
Public Class tButton
    Public Property ButtonType As String
    Public Property Value As String
    Public Property Text As String
End Class
