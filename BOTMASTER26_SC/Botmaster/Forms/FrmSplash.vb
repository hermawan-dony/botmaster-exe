Imports System.Net
Imports System.Reflection
Imports Microsoft.Web.WebView2.Core

Public Class FrmSplash

    Private MainHandler As Boolean
    Private WithEvents WAPILoader As New WebClient
    Public CmdArgs As String = ""
    Public Sub SetHandler(ByVal value As Boolean)
        MainHandler = value
    End Sub
    Public Sub SetSplashImage(ByVal SplashImage As Image)
        Me.BackgroundImage = SplashImage
        Me.BackgroundImageLayout = ImageLayout.Stretch
    End Sub
    Public Sub SetAboutImage(ByVal SplashImage As Image)
        FrmAbout.BackgroundImage = SplashImage
        FrmAbout.BackgroundImageLayout = ImageLayout.Stretch
    End Sub
    Public Sub SetApplicationTitle(ByVal title As String)
        ApplicationTitle = title
    End Sub
    Public Sub SetWebsite(ByVal Url As String)
        WebsiteURL = Url
        WebsiteName = Url.Replace("https://", "").Replace("http://", "").Replace("www.", "")
    End Sub
    Public Sub SetSupportEmail(ByVal email As String)
        SupportEmail = email
    End Sub
    Public Sub SetSupportPhone(ByVal phone As String)
        SupportPhone = phone
    End Sub
    Public Sub SetHelpLink(ByVal thelplink As String)
        HelpLink = thelplink
    End Sub
    Public Sub SetSupportURL(ByVal supporturl As String)
        supporturl = supporturl
    End Sub
    Public Sub SetLicenseMode(ByVal licmode As Boolean)
        LicenseMode = licmode
    End Sub
    Public Sub SetPrimaryColor(ByVal clr As Color)
        ColorPrimary = clr
    End Sub
    Public Sub SetSecondaryColor(ByVal clr As Color)
        ColorSecondary = clr
    End Sub
    Public Sub SetThirdColor(ByVal clr As Color)
        ColorThird = clr
    End Sub
    Public Sub cmdargument(ByVal args As String)
        CmdArgs = args
    End Sub
    Public Sub SetLicenseURL(ByVal URL As String)
        ClsLicence.LicenseURL = URL
    End Sub
    Public Sub SetDefaultLanguage(ByVal Lang As String)
        FrmMain.DefaultLang = Lang
    End Sub


    Private SplashStopwatch As New Stopwatch()

    Private Sub FrmSplash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SplashStopwatch.Start()
        FrmLauncher.SetTheme()

        DropShadow.ApplyShadows(Me)

        Panel3.Width = 0
        Panel3.Left = 1
        Panel3.Top = 1
        Panel3.Height = Panel2.Height - 2
        
        Dim licKey As String = GetSetting(Application.ProductName, "license", "key", "")
        If licKey.ToLower().Contains("wasender") Then
            Try
                If IO.File.Exists(Application.StartupPath & "\bot++.png") Then
                    Me.BackgroundImage = Image.FromFile(Application.StartupPath & "\bot++.png")
                End If
            Catch ex As Exception
            End Try
            LabelStatus.Text = "Loading WAGW API... (WAGW ENGINE ACTIVATED)"
        Else
            LabelStatus.Text = "Loading API..."
        End If

        LabelVersion.Text = "v:" & Application.ProductVersion
        LabelBuildDate.Text = "Build:" & IO.File.GetCreationTime(Assembly.GetExecutingAssembly().Location).ToString("yyyy.MM.dd")
        Application.DoEvents()
        WAPILoader.DownloadStringAsync(New Uri("https://auth.botmaster.us/api/v7/wapi?key=bmsk_live_X1QOkDiIb9FwgbfWC2DGf5T4O58HRrdU"))

        Try
            WebView21.InitializeLifetimeService()
            Dim webView2Environment = CoreWebView2Environment.CreateAsync(Nothing, ClsSpecialDirectories.GetGetDefaultProfile).Result
            WebView21.EnsureCoreWebView2Async(webView2Environment)
            WebView21.Source = New Uri("https://www.whatsapp.com/")
        Catch ex As Exception
            If MsgBox("Unable to find Microsoft Webview Runtime. click yes if you want install it.", vbCritical + vbYesNo) = vbYes Then
                Process.Start("https://go.microsoft.com/fwlink/p/?LinkId=2124703")
            End If
            End
        End Try



    End Sub

    Private Sub FrmSplash_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        DropShadow.ApplyShadows(Me)

        Dim fecha As Date = IO.File.GetCreationTime(Assembly.GetExecutingAssembly().Location)
        Application.DoEvents()



    End Sub


    Private Sub WAPILoader_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles WAPILoader.DownloadProgressChanged
        Panel3.BeginInvoke(Sub()
                               Panel3.Width = ((e.BytesReceived * Panel2.Width) \ e.TotalBytesToReceive)

                           End Sub)
    End Sub

    Private Async Sub WAPILoader_DownloadStringCompleted(sender As Object, e As DownloadStringCompletedEventArgs) Handles WAPILoader.DownloadStringCompleted

        Try
            WAPIScript = e.Result
        Catch ex As Exception
            MsgBox("Unable to connect to server...", vbCritical, Application.ProductName)
            End
        End Try

        SplashStopwatch.Stop()
        Dim elapsed As Long = SplashStopwatch.ElapsedMilliseconds
        If elapsed < 3500 Then
            Await Task.Delay(CInt(3500 - elapsed))
        End If

        Me.Hide()
        FrmMain.CmdAgrs = CmdArgs
        FrmMain.Show()
        FrmMain.Focus()
        FrmMain.Activate()
    End Sub

    Private Sub LabelStatus_Click(sender As Object, e As EventArgs) Handles LabelStatus.Click

    End Sub
End Class