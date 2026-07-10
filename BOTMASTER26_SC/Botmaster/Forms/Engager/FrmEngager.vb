Public Class FrmEngager
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AddForm(FrmWhatsApp)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AddForm(FrmDashboard)
    End Sub
    Private Sub FrmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        For Each _tab As TabPage In FrmWhatsApp.TabControl1.TabPages
            _tab.Controls.Clear()
        Next
        SaveInstances()
    End Sub
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadInstances()
        AddForm(FrmDashboard)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        AddForm(FrmMessages)
    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint
        ApplyColor(Me)
    End Sub
End Class