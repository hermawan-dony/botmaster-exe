Imports System.IO
Imports Newtonsoft.Json

Public Class FrmGPT
    Private MyGTPModel As New GTPModel
    Sub New()
        InitializeComponent()
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim promptForm As New FrmGPTPrompt()
        If promptForm.ShowDialog() = DialogResult.OK Then
            Dim newPrompt As New GPTPrompt() With {
                .Question = promptForm.TxtQuestion.Text,
                .Answer = promptForm.TxtAnswer.Text
            }
            Me.MyGTPModel.Prompts.Add(newPrompt)
            LoadPromptList()
        End If
    End Sub
    Sub LoadPromptList()
        LstPrompt.Items.Clear()
        If MyGTPModel IsNot Nothing AndAlso MyGTPModel.Prompts IsNot Nothing Then
            For Each prompt As GPTPrompt In MyGTPModel.Prompts
                Dim item As New ListViewItem(prompt.Question)
                item.Tag = prompt
                item.SubItems.Add(prompt.Answer)
                LstPrompt.Items.Add(item)
            Next
        End If
    End Sub
    Private Sub BtnModify_Click(sender As Object, e As EventArgs) Handles BtnModify.Click
        If LstPrompt.SelectedItems.Count > 0 Then
            Dim selectedIndex As Integer = LstPrompt.SelectedIndices(0) ' Get selected index
            Dim promptForm As New FrmGPTPrompt()
            promptForm.TxtQuestion.Text = Me.MyGTPModel.Prompts(selectedIndex).Question
            promptForm.TxtAnswer.Text = Me.MyGTPModel.Prompts(selectedIndex).Answer
            If promptForm.ShowDialog() = DialogResult.OK Then
                With Me.MyGTPModel.Prompts(selectedIndex)
                    .Question = promptForm.TxtQuestion.Text
                    .Answer = promptForm.TxtAnswer.Text
                End With
                LoadPromptList()
            End If
        End If
    End Sub
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If LstPrompt.SelectedItems.Count > 0 Then
            Dim selectedIndex As Integer = LstPrompt.SelectedIndices(0)
            Me.MyGTPModel.Prompts.RemoveAt(selectedIndex)
            LoadPromptList()
        End If
    End Sub
    Private Sub SaveGPTProfileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveGPTProfileToolStripMenuItem.Click
        Using sfd As New SaveFileDialog()
            sfd.Filter = "GPT Files (*.gpt)|*.gpt"
            If sfd.ShowDialog() = DialogResult.OK Then
                Dim json As String = JsonConvert.SerializeObject(Me.MyGTPModel)
                System.IO.File.WriteAllText(sfd.FileName, json)
            End If
        End Using
    End Sub
    Private Sub LoadGPTProfileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadGPTProfileToolStripMenuItem.Click
        Using ofd As New OpenFileDialog()
            ofd.Filter = "GPT Files (*.gpt)|*.gpt"
            If ofd.ShowDialog() = DialogResult.OK Then
                Dim json As String = System.IO.File.ReadAllText(ofd.FileName)
                Me.MyGTPModel = JsonConvert.DeserializeObject(Of GTPModel)(json)
                TxtPaiKey.Text = MyGTPModel.APIKey
                TxtSystemMessage.Text = MyGTPModel.SystemMessage
                CmbLanguage.Text = MyGTPModel.Language
                LstPrompt.Items.Clear()

                LoadPromptList()
            End If
        End Using
    End Sub
    Sub setGpt()
        MyGTPModel.APIKey = TxtPaiKey.Text
        MyGTPModel.SystemMessage = TxtSystemMessage.Text
        MyGTPModel.Language = CmbLanguage.Text
        MyGTPModel.Prompts.Clear()

        For Each promptItem As ListViewItem In LstPrompt.Items
            Dim prompt As New GPTPrompt() With {
                    .Question = promptItem.Text,
                    .Answer = promptItem.SubItems(1).Text
                }
            MyGTPModel.Prompts.Add(prompt)
        Next
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Dim defaultFilePath As String = Path.Combine(Application.StartupPath, "defaultGtpProfile.gpt")
        setGpt()

        Try
            CurrentGPTSetting = Me.MyGTPModel
            Dim json As String = JsonConvert.SerializeObject(Me.MyGTPModel)
            System.IO.File.WriteAllText(defaultFilePath, json)
            MessageBox.Show("Profile saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

            MessageBox.Show($"Failed to save profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Try
    End Sub
    Private Sub PopulateLanguagesComboBox()
        Dim languages As String() = New String() {
        "English", "Chinese (Simplified)", "Spanish", "Hindi", "Arabic",
        "Bengali", "Portuguese", "Russian", "Japanese", "French",
        "Hebrew", "Indonesian"
    }
        For Each language As String In languages
            CmbLanguage.Items.Add(language)
        Next
        CmbLanguage.SelectedIndex = 0
    End Sub

    Private Sub FrmGPT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try : ThemeManager.ApplyTheme(Me) : Catch : End Try
        PopulateLanguagesComboBox()
        MyGTPModel = CurrentGPTSetting
        If Not IsNothing(CurrentGPTSetting) Then
            TxtPaiKey.Text = CurrentGPTSetting.APIKey
            CmbLanguage.Text = CurrentGPTSetting.Language
            TxtSystemMessage.Text = CurrentGPTSetting.SystemMessage
            LoadPromptList()
        End If
    End Sub
End Class
