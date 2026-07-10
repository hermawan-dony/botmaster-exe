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
    Public ThemeActive As String = "Dark"
    Public SwitchColor As Color

    Public Sub InitializeTheme()
        Dim licKey As String = GetSetting(Application.ProductName, "license", "key", "")
        Dim defaultThemeVal As String = "Purple"
        If licKey.ToLower().Contains("wasender") Then
            defaultThemeVal = "Dark"
        End If
        ThemeActive = GetSetting(Application.ProductName, "Theme", "Active", defaultThemeVal)

        If ThemeActive = "Purple" Then
            ColorPrimary = Color.FromArgb(51, 15, 84)
            ColorSecondary = Color.FromArgb(73, 24, 118)
            ColorThird = Color.FromArgb(99, 39, 158)
        ElseIf ThemeActive = "Light" Then ' Sleek Gray
            ColorPrimary = Color.FromArgb(32, 33, 36)
            ColorSecondary = Color.FromArgb(43, 45, 49)
            ColorThird = Color.FromArgb(58, 60, 65)
        ElseIf ThemeActive = "Blue" Then ' Ocean Blue
            ColorPrimary = Color.FromArgb(11, 19, 43)
            ColorSecondary = Color.FromArgb(28, 37, 65)
            ColorThird = Color.FromArgb(43, 58, 92)
        ElseIf ThemeActive = "Pink" Then ' Soft Pink
            ColorPrimary = Color.FromArgb(46, 16, 29)
            ColorSecondary = Color.FromArgb(70, 24, 45)
            ColorThird = Color.FromArgb(98, 35, 64)
        ElseIf ThemeActive = "Red" Then ' Vibrant Red
            ColorPrimary = Color.FromArgb(46, 11, 11)
            ColorSecondary = Color.FromArgb(71, 16, 16)
            ColorThird = Color.FromArgb(99, 24, 24)
        Else ' Dark (WAGW Default)
            ColorPrimary = Color.FromArgb(15, 17, 33)
            ColorSecondary = Color.FromArgb(10, 11, 23)
            ColorThird = Color.FromArgb(25, 27, 48)
        End If
    End Sub

    Public Sub ApplyColor(ByRef mainclrl As Control)

        Dim Color1 As Color = ColorPrimary
        Dim Color2 As Color = ColorSecondary
        Dim Color3 As Color = ColorThird
        Dim Color4 As Color = Color.FromArgb(35, 37, 65)

        SwitchColor = ColorPrimary

        Dim RefColor1 As Color = Color.FromArgb(15, 17, 33)
        Dim RefColor2 As Color = Color.FromArgb(10, 11, 23)
        Dim RefColor3 As Color = Color.FromArgb(25, 27, 48)
        Dim RefColor4 As Color = Color.FromArgb(35, 37, 65)

        ' Apply theme background color directly to forms and tab pages
        If TypeOf mainclrl Is Form Then
            mainclrl.BackColor = ColorPrimary
        End If

        For Each ctrl As Control In mainclrl.Controls
            If Not IsNothing(ctrl.BackColor) Then
                ' Map default dark backgrounds to active theme colors
                If ctrl.BackColor = RefColor1 OrElse ctrl.BackColor = SystemColors.Control Then
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

                ' Specific theme handling for tab pages
                If TypeOf ctrl Is TabPage Then
                    ctrl.BackColor = ColorPrimary
                End If

                ' Force high contrast white text and custom backgrounds for all themes
                If TypeOf ctrl Is Label OrElse TypeOf ctrl Is CheckBox OrElse TypeOf ctrl Is RadioButton OrElse TypeOf ctrl Is GroupBox OrElse TypeOf ctrl Is Button OrElse TypeOf ctrl Is MenuStrip OrElse TypeOf ctrl Is ToolStrip OrElse TypeOf ctrl Is StatusStrip OrElse TypeOf ctrl Is TabControl Then
                    ctrl.ForeColor = Color.White
                    If TypeOf ctrl Is MenuStrip Then
                        Try
                            Dim menu As MenuStrip = CType(ctrl, MenuStrip)
                            menu.Renderer = New ThemeToolStripRenderer()
                        Catch
                        End Try
                    End If
                ElseIf TypeOf ctrl Is TextBox Then
                    Dim txt As TextBox = CType(ctrl, TextBox)
                    If txt.ReadOnly Then
                        txt.BackColor = ColorThird
                        txt.ForeColor = Color.White
                    Else
                        txt.BackColor = Color.White
                        txt.ForeColor = Color.Black
                    End If
                ElseIf TypeOf ctrl Is ComboBox OrElse TypeOf ctrl Is ListBox OrElse TypeOf ctrl Is DataGridView Then
                    ctrl.BackColor = ColorThird
                    ctrl.ForeColor = Color.White
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

Public Class ThemeToolStripRenderer
    Inherits ToolStripProfessionalRenderer

    Protected Overrides Sub OnRenderMenuItemBackground(e As ToolStripItemRenderEventArgs)
        Dim rect As New Rectangle(Point.Empty, e.Item.Size)
        If e.Item.Selected Then
            Using brush As New SolidBrush(Color.FromArgb(80, 80, 80))
                e.Graphics.FillRectangle(brush, rect)
            End Using
        Else
            Using brush As New SolidBrush(e.ToolStrip.BackColor)
                e.Graphics.FillRectangle(brush, rect)
            End Using
        End If
    End Sub
End Class
