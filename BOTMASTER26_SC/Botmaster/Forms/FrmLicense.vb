Public Class FrmLicense
    Public ViewMode As Integer
    Private Sub FrmLicense_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If ViewMode <> 3 Then
            End
            Me.Dispose()
        Else
            Me.Hide()
        End If
    End Sub

    <Obsolete>
    Private Sub FrmLicense_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyColor(Me)
        TextBox1.Text = Encrypt(getMacAddress)
        On Error Resume Next
        LicneseTextBox.Text = GetSetting(Application.ProductName, "license", "key", "")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Clipboard.SetText(TextBox1.Text)
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles LicneseTextBox.TextChanged
        Try
            'Label12.Visible = False
            'Dim _license As LicenseModel = Newtonsoft.Json.JsonConvert.DeserializeObject(Of LicenseModel)(Decrypt(TextBox2.Text))
            'LicName.Text = _license.ClientName
            'LicEmail.Text = _license.ClientEmail
            'LicCreateDate.Text = _license.CreateDate
            'LicExpiryDate.Text = _license.ExpiryDate
            'LicFilter.Text = _license.AllowFliter.ToString
            'LicSending.Text = _license.AllowSending.ToString
            'LicAutoReply.Text = _license.AllowBot.ToString


        Catch ex As Exception
            If LicneseTextBox.Text <> "" Then
                Label12.Visible = True
            Else
                Label12.Visible = False
            End If
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        On Error Resume Next
        Application.Exit()
        Application.ExitThread()
    End Sub

    <Obsolete>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        On Error Resume Next
        If LicneseTextBox.Text = "" Then
            MsgBox("Please enter your licnese", vbCritical, Application.ProductName)
            Exit Sub
        End If
        Dim srvdate As Long = Val(GetServerDate())
        Dim result As ActivationCodeResponse
        result = ClsLicence.ValidateLicense(LicneseTextBox.Text)
        If Not IsNothing(result) Then
            If result.IsExsist Then
                If Not IsNothing(result.Response) Then
                    If result.Response.RequestKey = Encrypt(getMacAddress) Then
                        If srvdate <= Val(result.Response.ExpiryDate) Then
                            If result.Response.Status = 1 Then

                                ExpriyDate = result.Response.ExpiryDate

                                TotalDays = ClsLicence.GetRemianingDays(result.Response.ExpiryDate, srvdate)

                                AllowSending = result.Response.AllowSending
                                AllowAutoReply = result.Response.AllowBot
                                allowFilter = result.Response.AllowFilter


                                SaveSetting(Application.ProductName, "license", "key", LicneseTextBox.Text)
                                MsgBox("Your license is successfuly activated", MsgBoxStyle.Information, Application.ProductName)
                                FrmMain.LabelRemaning.Text = TotalDays & " Remaning days"
                                Me.Hide()
                            Else
                                MsgBox("License disabled by vendor.", vbCritical, Application.ProductName)
                            End If

                        Else
                            MsgBox("License already expired", vbCritical, Application.ProductName)
                        End If
                    Else
                        MsgBox("License not related to this machine", vbCritical, Application.ProductName)
                    End If
                End If
            Else
                MsgBox("Unable to validate license check vendor", vbCritical, Application.ProductName)
            End If
        Else
            MsgBox("Unable to validate license check vendor", vbCritical, Application.ProductName)
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Try
            Process.Start("http://wa.me/" & SupportPhone)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim dlq As New OpenFileDialog
        dlq.Filter = "*.lic|*.lic"
        If dlq.ShowDialog = DialogResult.OK Then
            Try
                LicneseTextBox.Text = IO.File.ReadAllText(dlq.FileName)
            Catch ex As Exception
                MsgBox(ex.Message, vbCritical, Application.ProductName)
            End Try

        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Try
            Process.Start("mailto:" & SupportEmail)
        Catch ex As Exception

        End Try

    End Sub

    <Obsolete>
    Private Sub ValidateButton_Click(sender As Object, e As EventArgs) Handles ValidateButton.Click
        On Error Resume Next
        LicName.Text = ""
        LicEmail.Text = ""
        LicCreateDate.Text = ""
        LicExpiryDate.Text = ""
        LicSending.Text = ""
        LicAutoReply.Text = ""
        LicFilter.Text = ""

        Dim result As ActivationCodeResponse
        result = ClsLicence.ValidateLicense(LicneseTextBox.Text)

        If Not IsNothing(result) Then
            If result.IsExsist Then
                If Not IsNothing(result.Response) Then
                    If result.Response.RequestKey = Encrypt(getMacAddress) Then
                        If Val(GetServerDate()) <= Val(result.Response.ExpiryDate) Then
                            If result.Response.Status = 1 Then
                                LicName.Text = result.Response.Name
                                LicEmail.Text = result.Response.Email
                                LicCreateDate.Text = result.Response.Mobile
                                LicExpiryDate.Text = ClsLicence.ResolveDate(result.Response.ExpiryDate)
                                LicSending.Text = result.Response.AllowSending
                                LicAutoReply.Text = result.Response.AllowBot
                                LicFilter.Text = result.Response.AllowFilter
                            Else
                                MsgBox(result.ErrorDescription, vbCritical, "BOTMASTER WASENDER - LICENSE DISABLED")
                            End If

                        Else
                            MsgBox(result.ErrorDescription, vbCritical, "BOTMASTER WASENDER - LICENSE EXPIRED")
                        End If
                    Else
                        MsgBox("License not related to this machine.", vbCritical, "BOTMASTER WASENDER")
                    End If
                End If
            Else
                MsgBox("Unable to validate license check vendor.", vbCritical, "BOTMASTER WASENDER")
            End If
        Else
            MsgBox("Unable to validate license check vendor.", vbCritical, "BOTMASTER WASENDER")
        End If

    End Sub
End Class