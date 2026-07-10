Module ModuleConfig

    Public ServerURL As String = "http://businesswhatsappsender.mediaplus.me/api/v1/GetExKey.ashx" 'you can host getlic on your server 


    Public WebsiteURL As String = "https://getbotmaster.com/"
    Public WebsiteName As String = "GetBotMaster.Me"
    Public HelpLink As String = "https://wa.me/6285175420807"
    Public SupportURL As String = "https://wa.me/6285175420807"
    Public SupportPhone As String = "6285175420807"
    Public SupportEmail As String = "support@getbotmaster.com"

    Public ColorPrimary As Color = Color.FromArgb(15, 17, 33)
    Public ColorSecondary As Color = Color.FromArgb(10, 11, 23)
    Public ColorThird As Color = Color.FromArgb(25, 27, 48)


    ''' <summary>
    ''' Make license mode True if you are reseller user 
    ''' </summary>Ca

    Public Property LicenseMode As Boolean = True

    Public IsDemoMode As Boolean = False
    Public ShowBranding As Boolean = True
    Public ApplicationTitle As String = Application.ProductName
    Public CompanyName As String = "Mediaplus"
    Public RequestLicenseAfter As Boolean = False

    Public ApplicationVersion As String = Application.ProductVersion
    Public ShowAbout As Boolean = True
    Public ShowLanguageOption As Boolean = True
    Public ShowThemesOption As Boolean = True
    Public ShowHelp As Boolean = True
    Public ShowUpdateLicense As Boolean = True

    Public DemoMessage As String = $"{vbNewLine}{vbNewLine}----------------------{vbNewLine}{ApplicationTitle} Demo version {vbNewLine}developer:{CompanyName}{vbNewLine}For more info{vbNewLine}E-mail:{SupportEmail}{vbNewLine}or{vbNewLine}WhatsApp:https://wa.me/{SupportPhone}"
    Public CurrentWaProfile As String
    Public Sub sfts()
        If GetSetting(Application.ProductName, "sfts", "dd", "") = "" Then
            SaveSetting(Application.ProductName, "sfts", "dd", Now.ToString("yyyyMMdd"))
        End If
    End Sub
    Public SwitchColor As Color

    Public Sub ApplyColor(ByRef mainclrl As Control)

        'Exit Sub

        Dim Color1 As Color = ColorPrimary
        Dim Color2 As Color = ColorSecondary
        Dim Color3 As Color = ColorThird
        Dim Color4 As Color = Color.FromArgb(35, 37, 65)

        SwitchColor = ColorPrimary


        Dim RefColor1 As Color = Color.FromArgb(15, 17, 33)
        Dim RefColor2 As Color = Color.FromArgb(10, 11, 23)
        Dim RefColor3 As Color = Color.FromArgb(25, 27, 48)
        Dim RefColor4 As Color = Color.FromArgb(35, 37, 65)

        For Each ctrl As Control In mainclrl.Controls
            If Not IsNothing(ctrl.BackColor) Then
                If ctrl.BackColor = RefColor1 Then
                    ctrl.BackColor = Color1
                End If
                If ctrl.BackColor = RefColor2 Then
                    ctrl.BackColor = Color2
                End If
                If ctrl.BackColor = RefColor3 Then
                    ctrl.BackColor = Color3
                End If
                If ctrl.ForeColor = RefColor4 Then
                    ctrl.ForeColor = Color4
                End If
                ApplyColor(ctrl)
            End If
        Next
    End Sub

    Public Function GetAppIconImage() As Image
        ' Get the icon associated with the running EXE
        Dim exePath As String = Application.ExecutablePath
        Dim appIcon As Icon = Icon.ExtractAssociatedIcon(exePath)

        If appIcon IsNot Nothing Then
            ' Convert Icon to Bitmap (Image)
            Return appIcon.ToBitmap()
        Else
            Return Nothing
        End If
    End Function
    Public Function GetAppIcon() As Icon

        Dim exePath As String = Application.ExecutablePath
        Dim appIcon As Icon = Icon.ExtractAssociatedIcon(exePath)

        Return appIcon
    End Function



End Module
