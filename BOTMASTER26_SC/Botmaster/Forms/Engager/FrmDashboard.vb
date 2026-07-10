Imports System.Threading
Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Newtonsoft
Imports Newtonsoft.Json
Public Class FrmDashboard
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Button4.Enabled = True
        Button5.Enabled = True
        Timer1.Enabled = False
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button4.Enabled = False
        Button5.Enabled = True
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Randomize()
        Timer1.Interval = (Int(Rnd() * NumericUpDown2.Value) + NumericUpDown1.Value) * 1000
        Send()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ListView1.Items.Clear()
    End Sub

    Private Sub FrmDashboardvb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try : ThemeManager.ApplyTheme(Me) : Catch : End Try
        ApplyColor(Me)
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
End Class
Public Class IntanceDetails
    Public Property name As String
    Public Property isloggedin As String
    Public Property Number As String
End Class
