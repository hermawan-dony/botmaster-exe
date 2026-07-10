Public Class FrmSendingMode
    Public DatetoSend As DateTime
    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click


        DatetoSend = DateTimePicker1.Value

        Me.DialogResult = DialogResult.OK

    End Sub
    Private Sub FrmSendingMode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try : ThemeManager.ApplyTheme(Me) : Catch : End Try

    End Sub

    Private Sub FrmSendingMode_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '   Me.Dispose()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel

    End Sub
End Class
