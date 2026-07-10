Imports System.ComponentModel

Public Class FrmSending
    Public DeliveryMode As Integer
    Private SafeMode As Boolean
    Private RightEnd As Boolean = False
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonStop.Click
        Me.Close()
    End Sub

    Public SendingCounter As Integer
    Public RefDate As DateTime

    Public BulkDestinations As List(Of DestinationModel)
    Public BulkAttachments As New List(Of AttachmentModel)
    Public BulkMessages As New List(Of String)
    Public BulkName As String
    Public SendingProfiles As New List(Of String)
    Dim BulkSendingDate As DateTime
    Dim bulkTotal As Integer
    Public SuccessMessages As Integer
    Public FailedMessages As Integer
    Public isButtons As Boolean
    Public ButtonObject As String
    Public Buttontext As String


    Public Event OnBulkStart As EventHandler
    Public Event OnBulkEnd As EventHandler
    Public Event OnMessageSent As EventHandler
    Public Event OnSendingProgress As EventHandler
    Public Event OnstartResting As EventHandler
    Public Event OnStopResting As EventHandler
    Public Event OnBulkPause As EventHandler
    Public Event OnBulkUnPause As EventHandler
    Public Event OnBulkStoped As EventHandler

    Public WithEvents EventsManager As New Timer With {.Interval = 1, .Enabled = True}
    Public WithEvents EventsOnSendManager As New Timer With {.Interval = 1000, .Enabled = False}

    Sub sender_events() Handles EventsManager.Tick
        If Status.IsCampainPaused Then
            If Not IsPausedLabel.Visible Then
                IsPausedLabel.Visible = True
            End If
        Else
            If IsPausedLabel.Visible Then
                IsPausedLabel.Visible = False
            End If
        End If

        If Status.IsResting Then
            If Not LabelIsResting.Visible Then
                LabelIsResting.Visible = True
            End If
        Else
            If LabelIsResting.Visible Then
                LabelIsResting.Visible = False
            End If
        End If
        If Status.IsCampaignEnd Then
            Status.IsCampaignEnd = False
            MsgBox("Campaign has been done!", vbInformation, Application.ProductName)
        End If

        If Not Status.IsWAPIloggedin Then
            If Not Panel1.Visible Then
                Panel1.Visible = True
            End If
        Else
            If Panel1.Visible Then
                Panel1.Visible = False
            End If
        End If
            Labelstatus.Text = Status.StatusString
    End Sub
    Private Sub EventsOnSendManager_Tick(sender As Object, e As EventArgs) Handles EventsOnSendManager.Tick
        ProgressBarSending.Value = Status.TotalProgress + 1
        LabelProgress.Text = $"Sending Process ({Status.TotalProgress + 1}/{BulkDestinations.Count})"
    End Sub


    Private Sub FrmSending_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try : ThemeManager.ApplyTheme(Me) : Catch : End Try
        ApplyColor(Me)


        ProgressBarSending.Value = 0
        EventsOnSendManager.Enabled = True

        Status.IsCampainPaused = False
        Status.TotalProgress = 0
        DropShadow.ApplyShadows(Me)

        FrmMain.BtnSending.Enabled = False
        FrmMain.ButtonSchedule.Enabled = False
        ProgressBarSending.Maximum = BulkDestinations.Count
        LabelProgress.Text = $"Sending Process (0/{BulkDestinations.Count})"
        Status.ForceClose = False

        If DeliveryMode = 1 Then
            Me.Text = "Sending Bulk - Safe Mode"
            SafeMode = True

        ElseIf DeliveryMode = 2 Then
            Me.Text = "Sending Bulk - Blind Mode"
            SafeMode = False

        Else
            Me.Text = "Sending Bulk - Multi-Channel Mode"
        End If

        If RefDate > Now Then
            PanelCountDown.Visible = True
            TimerCountDown.Enabled = True
            ButtonPause.Enabled = False
            ButtonViewLog.Enabled = False
            Exit Sub
        Else
            PanelCountDown.Visible = False
            TimerCountDown.Enabled = False
            startbulk()
        End If
    End Sub


    Private Sub startbulk()
        RightEnd = False
        ListViewNumbers.Items.Clear()
        MessagesSentList.Clear()
        SuccessMessages = 0
        FailedMessages = 0

        PanelCountDown.Visible = False
        TimerCountDown.Enabled = False

        PanelCountDown.Visible = False
        TimerCountDown.Enabled = False
        ButtonPause.Enabled = True
        ButtonViewLog.Enabled = True

        BulkSendingDate = Now

        BulkIsMessageSent = False
        If DeliveryMode <> 3 Then
            FrmBrowser.StartBulk(BulkDestinations, BulkMessages, BulkAttachments, SafeMode, isButtons, ButtonObject, False, "", Buttontext)
        End If
    End Sub

    Private Function GetDelay() As Integer
        Dim Num1 As Integer = Val(GetSetting(ApplicationTitle, "SendingConfig", "DelayStart", "0"))
        Dim Num2 As Integer = Val(GetSetting(ApplicationTitle, "SendingConfig", "DelayEnd", "2"))

        Randomize()
        Dim a As Integer = 10
        If Num2 > 0 Then
            a = (Num1 + (Int(Rnd() * Num2))) * 1000
        Else
            a = 100
        End If
        If a = 0 Then
            a = 100
        End If
        Return a
    End Function
    Private demosendCounter As Integer
    Private refeshcounter As Long
    Private Sub ButtonPause_Click(sender As Object, e As EventArgs) Handles ButtonPause.Click
        If ButtonPause.Tag = "pause" Then
            Status.IsCampainPaused = True
            ButtonPause.Text = "Resume"
            ButtonPause.Tag = "resume"
        Else
            Status.IsCampainPaused = False
            ButtonPause.Text = "Pause"
            ButtonPause.Tag = "pause"
        End If
    End Sub
    Sub Countdown()
        Dim Date1 As DateTime = Now
        Dim Date2 As DateTime = RefDate
        Dim t As Long = DateDiff(DateInterval.Second, Date1, Date2)
        Dim dayCount As Integer = DateDiff(DateInterval.Day, Date1, Date2)
        Dim iSpan As TimeSpan = TimeSpan.FromSeconds(t)

        LabelCountDown.Text = (iSpan.Hours + (dayCount * 24)).ToString.PadLeft(2, "0"c) & ":" &
                        iSpan.Minutes.ToString.PadLeft(2, "0"c) & ":" &
                        iSpan.Seconds.ToString.PadLeft(2, "0"c)
        If t = 0 Then

            startbulk()
        End If
    End Sub
    Private Sub TimerCountDown_Tick(sender As Object, e As EventArgs) Handles TimerCountDown.Tick
        Countdown()
    End Sub

    Public Sub CreateReport(ByVal SendingDate As DateTime,
                            ByVal attachment As List(Of AttachmentModel),
                            ByVal destination As ListView)
        Try



            Dim ReportName As String = SendingDate.ToString("yyyy-MM-dd HH[mm[ss") & "_" & destination.Items.Count & ".html"

            Dim report As String = My.Resources.Report

            report = report.Replace("<!--date-->", SendingDate.ToString("yyyy-MM-dd HH:mm:ss"))
            report = report.Replace("<!--total-->", destination.Items.Count)
            report = report.Replace("<!--success-->", SuccessMessages)
            report = report.Replace("<!--failed-->", FailedMessages)

            Dim AttachmentHTML As String = ""
            AttachmentHTML = AttachmentHTML & "<tr>" & vbNewLine
            AttachmentHTML = AttachmentHTML & "<td style = 'width: 164px' >{0}</td>" & vbNewLine
            AttachmentHTML = AttachmentHTML & "<td style='width: 89px'>{1}</td>" & vbNewLine
            AttachmentHTML = AttachmentHTML & "<td style = 'width: 71px' >{2}</td>" & vbNewLine
            AttachmentHTML = AttachmentHTML & "</tr>" & vbNewLine

            Dim _attachment As String = ""
            For Each _attach As AttachmentModel In attachment
                _attachment = _attachment & String.Format(AttachmentHTML, _attach.FileName, _attach.Type, _attach.Caption)
            Next

            report = report.Replace("<!-- Attachment -->", _attachment)

            Dim recordHtml As String = ""
            Dim status As String = ""
            For Each li As ListViewItem In destination.Items
                recordHtml = recordHtml & "<tr><td Class='{{ICON}}' style='width: 15px'></td>" & vbNewLine
                recordHtml = recordHtml & "<td style ='width: 35px'><span _locid='OverviewSolutionSpan'>{{DESTINATION}}</span></td>" & vbNewLine
                recordHtml = recordHtml & "<td style ='width: 207px'><span _locid='OverviewSolutionSpan'>{{TYPE}}</span></td>" & vbNewLine
                recordHtml = recordHtml & "<td style='width: 207px'><span _locid='OverviewSolutionSpan'>{{DATE}}</span></td>" & vbNewLine
                recordHtml = recordHtml & "<td style ='width: 207px'><span _locid='OverviewSolutionSpan'>{{STATUS}}</span></td>" & vbNewLine
                recordHtml = recordHtml & "<td style='width: 207px'><span _locid='OverviewSolutionSpan'>{{MESSAGE}}</span></td>" & vbNewLine

                If li.ImageIndex = 0 Then
                    status = "IconSuccessEncoded"
                Else
                    status = "IconErrorEncoded"
                End If

                recordHtml = recordHtml.Replace("{{ICON}}", status)
                recordHtml = recordHtml.Replace("{{DESTINATION}}", li.SubItems(0).Text)
                recordHtml = recordHtml.Replace("{{TYPE}}", li.SubItems(1).Text)
                recordHtml = recordHtml.Replace("{{DATE}}", li.SubItems(2).Text)
                recordHtml = recordHtml.Replace("{{STATUS}}", li.SubItems(3).Text)
                recordHtml = recordHtml.Replace("{{MESSAGE}}", li.SubItems(4).Text)


            Next
            report = report.Replace("<!--messages-->", recordHtml)

            IO.File.WriteAllText(ClsSpecialDirectories.GetReport & ReportName.Replace(":", ""), report)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ButtonViewLog_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonViewLog.MouseDown
        ContextMenuStrip1.Show(ButtonViewLog, e.Location)
    End Sub

    Public Sub Export(ByVal ExportType As Integer)
        Dim saveDlg As New SaveFileDialog
        saveDlg.Filter = "*.csv|*.csv"
        Dim result As String = ""
        If saveDlg.ShowDialog = DialogResult.OK Then
            For Each li As ListViewItem In ListViewNumbers.Items
                If ExportType = 0 Then
                    result = result & li.Text & vbNewLine
                Else
                    If li.ImageIndex = 0 Then
                        result = result & li.Text & vbNewLine
                    End If
                End If
            Next
            IO.File.WriteAllText(saveDlg.FileName, result)
        End If
    End Sub
    Private Sub ExportAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportAllToolStripMenuItem.Click
        Export(0)
    End Sub
    Private Sub ExportSuccessfulToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportSuccessfulToolStripMenuItem.Click
        Export(1)
    End Sub
    Private Sub FrmSending_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        EventsOnSendManager.Enabled = False
        Status.ForceClose = True
        FrmMain.BtnSending.Enabled = True
        FrmMain.ButtonSchedule.Enabled = True

        If Not RightEnd Then
            CreateReport(BulkSendingDate, BulkAttachments, ListViewNumbers)
        End If
        Me.Dispose()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        FrmBrowser.Show()
    End Sub
    Private Sub FrmSending_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Status.IsCampainStatred Then
            If MsgBox("Are you sure you want close?", vbYesNo + vbQuestion, Application.ProductName) = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub Labelstatus_Click(sender As Object, e As EventArgs) Handles Labelstatus.Click

    End Sub
End Class


