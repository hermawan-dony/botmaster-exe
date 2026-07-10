Public Class FrmSetCaption
    Private Sub BtnEmoji_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)

    End Sub
    Public Caption As String
    Public dlgResult As DialogResult
    Private Sub BtnBold_Click(sender As Object, e As EventArgs) Handles BtnBold.Click
        Dim CurrentIndex As Integer = TxtCaption.SelectionStart
        Dim CurrentLenght As Integer = TxtCaption.SelectionLength
        TxtCaption.Text = TxtCaption.Text.Insert(CurrentIndex, "*")
        TxtCaption.Text = TxtCaption.Text.Insert(CurrentIndex + CurrentLenght + 1, "*")
    End Sub
    Private Sub BtnItalic_Click(sender As Object, e As EventArgs) Handles BtnItalic.Click
        Dim CurrentIndex As Integer = TxtCaption.SelectionStart
        Dim CurrentLenght As Integer = TxtCaption.SelectionLength
        TxtCaption.Text = TxtCaption.Text.Insert(CurrentIndex, "_")
        TxtCaption.Text = TxtCaption.Text.Insert(CurrentIndex + CurrentLenght + 1, "_")
    End Sub
    Private Sub BtnStrike_Click(sender As Object, e As EventArgs) Handles BtnStrike.Click
        Dim CurrentIndex As Integer = TxtCaption.SelectionStart
        Dim CurrentLenght As Integer = TxtCaption.SelectionLength
        TxtCaption.Text = TxtCaption.Text.Insert(CurrentIndex, "~")
        TxtCaption.Text = TxtCaption.Text.Insert(CurrentIndex + CurrentLenght + 1, "~")
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Caption = TxtCaption.Text
        dlgResult = DialogResult.OK
        Me.Close()

    End Sub
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        dlgResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub FrmSetCaption_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub LinkName_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkName.LinkClicked
        TxtCaption.Text = TxtCaption.Text.Insert(TxtCaption.SelectionStart, "[[fullname]]")

    End Sub

    Private Sub LinkFirstName_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkFirstName.LinkClicked
        TxtCaption.Text = TxtCaption.Text.Insert(TxtCaption.SelectionStart, "[[firstname]]")

    End Sub

    Private Sub LinkLastName_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLastName.LinkClicked
        TxtCaption.Text = TxtCaption.Text.Insert(TxtCaption.SelectionStart, "[[lastname]]")
    End Sub

    Private Sub FrmSetCaption_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Process.Start("https://www.emojicopy.com/")
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        ContextMenuStripParams.Show(sender, Button19.Width, Button19.Height)
    End Sub
    Private Sub InsertFullNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertFullNameToolStripMenuItem.Click

        TxtCaption.Text = TxtCaption.Text.Insert(TxtCaption.SelectionStart, "[[fullname]]")
    End Sub

    Private Sub InsertFirstNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertFirstNameToolStripMenuItem.Click
        TxtCaption.Text = TxtCaption.Text.Insert(TxtCaption.SelectionStart, "[[firstname]]")

    End Sub

    Private Sub InsertLastNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertLastNameToolStripMenuItem.Click
        TxtCaption.Text = TxtCaption.Text.Insert(TxtCaption.SelectionStart, "[[lastname]]")

    End Sub

    Private Sub InsertRandomTagToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertRandomTagToolStripMenuItem.Click
        TxtCaption.Text = TxtCaption.Text.Insert(TxtCaption.SelectionStart, "[[randomtag]]")

    End Sub

    Private Sub Variable1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Variable1ToolStripMenuItem.Click
        TxtCaption.Text = TxtCaption.Text.Insert(TxtCaption.SelectionStart, "[[VAR1]]")
    End Sub

    Private Sub Variable2ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Variable2ToolStripMenuItem1.Click
        TxtCaption.Text = TxtCaption.Text.Insert(TxtCaption.SelectionStart, "[[VAR2]]")

    End Sub
    Private Sub Variable3ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Variable3ToolStripMenuItem1.Click
        TxtCaption.Text = TxtCaption.Text.Insert(TxtCaption.SelectionStart, "[[VAR3]]")

    End Sub
    Private Sub Variable4ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Variable4ToolStripMenuItem1.Click
        TxtCaption.Text = TxtCaption.Text.Insert(TxtCaption.SelectionStart, "[[VAR4]]")

    End Sub
    Private Sub Variable5ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Variable5ToolStripMenuItem1.Click
        TxtCaption.Text = TxtCaption.Text.Insert(TxtCaption.SelectionStart, "[[VAR5]]")

    End Sub

End Class