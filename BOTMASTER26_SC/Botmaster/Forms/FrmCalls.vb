Public Class FrmCalls
    Public DestinationsList As New List(Of String)
    Dim IsPaused As Boolean = False
    Dim StartCall As Task
    Dim SendingIndex As Integer = 0
    Private Sub FrmCalls_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Maximum = DestinationsList.Count
        ProgressBar1.Value = 0
        Label1.Text = $"Proccessing Calls 0/{ProgressBar1.Maximum}"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Interval = NumericUpDown1.Value * 1000
        Timer1.Enabled = True
        Button1.Enabled = False
        Button4.Enabled = True
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Timer1.Enabled = False
        Button1.Enabled = True
        Button4.Enabled = False
    End Sub
    Private Sub FrmCalls_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Interval = NumericUpDown1.Value * 1000
        If SendingIndex < DestinationsList.Count Then
            FrmBrowser.OfferCall(DestinationsList(SendingIndex))
            ProgressBar1.Value += 1
            Label1.Text = $"Proccessing Calls {ProgressBar1.Value}/{ProgressBar1.Maximum}"
            TextBox1.Text &= "Called offered to:" & DestinationsList(SendingIndex) & vbNewLine
            SendingIndex += 1
        Else
            Timer1.Enabled = False
            Button1.Enabled = True
            Button4.Enabled = False
            MsgBox("All Calls Sent Successfully", MsgBoxStyle.Information, "Calls")
            ProgressBar1.Maximum = DestinationsList.Count
            ProgressBar1.Value = 0
            Label1.Text = $"Proccessing Calls 0/{ProgressBar1.Maximum}"
            TextBox1.Text = ""
        End If

    End Sub
End Class