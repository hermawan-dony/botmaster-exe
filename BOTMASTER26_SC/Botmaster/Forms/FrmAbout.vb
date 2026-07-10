Imports System.Reflection

Public Class FrmAbout
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)

    End Sub

    Private Sub FrmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ApplyColor(Me)
        LinkLabel1.Text = WebsiteName
        If Not ShowBranding Then
            LinkLabel1.Visible = False
        End If
        DropShadow.ApplyShadows(Me)
        LabelVersion.Text = "v:" & Application.ProductVersion
        LabelBuildDate.Text = "Build:" & IO.File.GetCreationTime(Assembly.GetExecutingAssembly().Location).ToString("yyyy.MM.dd")
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs)
        Process.Start("https://www.facebook.com/mediaplusme")
    End Sub
    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Process.Start($"https://web.whatsapp.com/send?phone={SupportPhone}")
    End Sub
    Private Sub LinkLabel1_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start(WebsiteURL)
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Process.Start($"mailto:{SupportEmail}")
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs)
        Process.Start("https://www.youtube.com/channel/UCYVg9u9eLx2gQ80OlwoJKYw")
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs)
        Process.Start("https://twitter.com/khaldoun_baz")
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs)
        Process.Start("https://www.instagram.com/mediaplus.me/")
    End Sub
End Class