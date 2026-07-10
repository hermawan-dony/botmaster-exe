<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSending
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSending))
        Me.ProgressBarSending = New System.Windows.Forms.ProgressBar()
        Me.ListViewNumbers = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderKind = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ImageListWa = New System.Windows.Forms.ImageList(Me.components)
        Me.ButtonStop = New System.Windows.Forms.Button()
        Me.ButtonPause = New System.Windows.Forms.Button()
        Me.LabelProgress = New System.Windows.Forms.Label()
        Me.ButtonViewLog = New System.Windows.Forms.Button()
        Me.PanelCountDown = New System.Windows.Forms.Panel()
        Me.LabelCountDown = New System.Windows.Forms.Label()
        Me.LabelCountDownHeader = New System.Windows.Forms.Label()
        Me.TimerCountDown = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExportSuccessfulToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerMultiChannelCounter = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.IsPausedLabel = New System.Windows.Forms.Label()
        Me.LabelIsResting = New System.Windows.Forms.Label()
        Me.Labelstatus = New System.Windows.Forms.Label()
        Me.PanelCountDown.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ProgressBarSending
        '
        Me.ProgressBarSending.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ProgressBarSending.Location = New System.Drawing.Point(7, 34)
        Me.ProgressBarSending.Name = "ProgressBarSending"
        Me.ProgressBarSending.Size = New System.Drawing.Size(682, 23)
        Me.ProgressBarSending.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBarSending.TabIndex = 0
        Me.ProgressBarSending.Value = 50
        '
        'ListViewNumbers
        '
        Me.ListViewNumbers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeaderKind, Me.ColumnHeader2, Me.ColumnHeader5, Me.ColumnHeader4, Me.ColumnHeader6})
        Me.ListViewNumbers.FullRowSelect = True
        Me.ListViewNumbers.GridLines = True
        Me.ListViewNumbers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ListViewNumbers.HideSelection = False
        Me.ListViewNumbers.Location = New System.Drawing.Point(6, 72)
        Me.ListViewNumbers.MultiSelect = False
        Me.ListViewNumbers.Name = "ListViewNumbers"
        Me.ListViewNumbers.Size = New System.Drawing.Size(683, 265)
        Me.ListViewNumbers.SmallImageList = Me.ImageListWa
        Me.ListViewNumbers.StateImageList = Me.ImageListWa
        Me.ListViewNumbers.TabIndex = 1
        Me.ListViewNumbers.UseCompatibleStateImageBehavior = False
        Me.ListViewNumbers.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "ID"
        Me.ColumnHeader3.Width = 150
        '
        'ColumnHeaderKind
        '
        Me.ColumnHeaderKind.Text = "Kind"
        Me.ColumnHeaderKind.Width = 80
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Type"
        Me.ColumnHeader2.Width = 72
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Sending Date"
        Me.ColumnHeader5.Width = 146
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Status"
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Message"
        Me.ColumnHeader6.Width = 186
        '
        'ImageListWa
        '
        Me.ImageListWa.ImageStream = CType(resources.GetObject("ImageListWa.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListWa.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListWa.Images.SetKeyName(0, "bwa_Sent.png")
        Me.ImageListWa.Images.SetKeyName(1, "bwa_not_Sent.png")
        Me.ImageListWa.Images.SetKeyName(2, "pending.png")
        '
        'ButtonStop
        '
        Me.ButtonStop.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ButtonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonStop.ForeColor = System.Drawing.Color.White
        Me.ButtonStop.Location = New System.Drawing.Point(587, 342)
        Me.ButtonStop.Name = "ButtonStop"
        Me.ButtonStop.Size = New System.Drawing.Size(102, 32)
        Me.ButtonStop.TabIndex = 2
        Me.ButtonStop.Text = "Close"
        Me.ButtonStop.UseVisualStyleBackColor = False
        '
        'ButtonPause
        '
        Me.ButtonPause.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ButtonPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonPause.ForeColor = System.Drawing.Color.White
        Me.ButtonPause.Location = New System.Drawing.Point(480, 342)
        Me.ButtonPause.Name = "ButtonPause"
        Me.ButtonPause.Size = New System.Drawing.Size(106, 32)
        Me.ButtonPause.TabIndex = 6
        Me.ButtonPause.Tag = "pause"
        Me.ButtonPause.Text = "Pause"
        Me.ButtonPause.UseVisualStyleBackColor = False
        '
        'LabelProgress
        '
        Me.LabelProgress.Location = New System.Drawing.Point(8, 7)
        Me.LabelProgress.Name = "LabelProgress"
        Me.LabelProgress.Size = New System.Drawing.Size(681, 22)
        Me.LabelProgress.TabIndex = 7
        Me.LabelProgress.Text = "Sending Process (0/0)"
        Me.LabelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ButtonViewLog
        '
        Me.ButtonViewLog.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ButtonViewLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonViewLog.ForeColor = System.Drawing.Color.White
        Me.ButtonViewLog.Location = New System.Drawing.Point(6, 342)
        Me.ButtonViewLog.Name = "ButtonViewLog"
        Me.ButtonViewLog.Size = New System.Drawing.Size(125, 32)
        Me.ButtonViewLog.TabIndex = 8
        Me.ButtonViewLog.Text = "Export"
        Me.ButtonViewLog.UseVisualStyleBackColor = False
        '
        'PanelCountDown
        '
        Me.PanelCountDown.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(11, Byte), Integer), CType(CType(23, Byte), Integer))
        Me.PanelCountDown.Controls.Add(Me.LabelCountDown)
        Me.PanelCountDown.Controls.Add(Me.LabelCountDownHeader)
        Me.PanelCountDown.Location = New System.Drawing.Point(151, 154)
        Me.PanelCountDown.Name = "PanelCountDown"
        Me.PanelCountDown.Padding = New System.Windows.Forms.Padding(6)
        Me.PanelCountDown.Size = New System.Drawing.Size(388, 118)
        Me.PanelCountDown.TabIndex = 10
        '
        'LabelCountDown
        '
        Me.LabelCountDown.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.LabelCountDown.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelCountDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCountDown.ForeColor = System.Drawing.Color.White
        Me.LabelCountDown.Location = New System.Drawing.Point(6, 34)
        Me.LabelCountDown.Name = "LabelCountDown"
        Me.LabelCountDown.Size = New System.Drawing.Size(376, 78)
        Me.LabelCountDown.TabIndex = 1
        Me.LabelCountDown.Text = "00:00:00"
        Me.LabelCountDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelCountDownHeader
        '
        Me.LabelCountDownHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.LabelCountDownHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelCountDownHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCountDownHeader.ForeColor = System.Drawing.Color.White
        Me.LabelCountDownHeader.Location = New System.Drawing.Point(6, 6)
        Me.LabelCountDownHeader.Name = "LabelCountDownHeader"
        Me.LabelCountDownHeader.Size = New System.Drawing.Size(376, 28)
        Me.LabelCountDownHeader.TabIndex = 0
        Me.LabelCountDownHeader.Text = "Your bulk will start after"
        Me.LabelCountDownHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TimerCountDown
        '
        Me.TimerCountDown.Enabled = True
        Me.TimerCountDown.Interval = 300
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportSuccessfulToolStripMenuItem, Me.ExportAllToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(167, 48)
        '
        'ExportSuccessfulToolStripMenuItem
        '
        Me.ExportSuccessfulToolStripMenuItem.Name = "ExportSuccessfulToolStripMenuItem"
        Me.ExportSuccessfulToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.ExportSuccessfulToolStripMenuItem.Text = "Export Successful"
        '
        'ExportAllToolStripMenuItem
        '
        Me.ExportAllToolStripMenuItem.Name = "ExportAllToolStripMenuItem"
        Me.ExportAllToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.ExportAllToolStripMenuItem.Text = "Export All"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Red
        Me.Panel1.Controls.Add(Me.LinkLabel1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(164, 162)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(360, 102)
        Me.Panel1.TabIndex = 13
        Me.Panel1.Visible = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(229, 78)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(122, 13)
        Me.LinkLabel1.TabIndex = 1
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Click Here to login again"
        Me.LinkLabel1.VisitedLinkColor = System.Drawing.Color.White
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(19, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(332, 51)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "OOPS... " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "System went went wrong and system logged out please " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Login again to co" &
    "ntinue your bulk "
        '
        'IsPausedLabel
        '
        Me.IsPausedLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.IsPausedLabel.Font = New System.Drawing.Font("Calibri", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IsPausedLabel.ForeColor = System.Drawing.Color.White
        Me.IsPausedLabel.Location = New System.Drawing.Point(5, 172)
        Me.IsPausedLabel.Name = "IsPausedLabel"
        Me.IsPausedLabel.Size = New System.Drawing.Size(686, 68)
        Me.IsPausedLabel.TabIndex = 14
        Me.IsPausedLabel.Text = "PAUSED"
        Me.IsPausedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IsPausedLabel.Visible = False
        '
        'LabelIsResting
        '
        Me.LabelIsResting.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.LabelIsResting.Font = New System.Drawing.Font("Calibri", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelIsResting.ForeColor = System.Drawing.Color.White
        Me.LabelIsResting.Location = New System.Drawing.Point(5, 170)
        Me.LabelIsResting.Name = "LabelIsResting"
        Me.LabelIsResting.Size = New System.Drawing.Size(686, 68)
        Me.LabelIsResting.TabIndex = 15
        Me.LabelIsResting.Text = "Resting"
        Me.LabelIsResting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LabelIsResting.Visible = False
        '
        'Labelstatus
        '
        Me.Labelstatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Labelstatus.Location = New System.Drawing.Point(0, 375)
        Me.Labelstatus.Name = "Labelstatus"
        Me.Labelstatus.Padding = New System.Windows.Forms.Padding(5)
        Me.Labelstatus.Size = New System.Drawing.Size(700, 25)
        Me.Labelstatus.TabIndex = 16
        '
        'FrmSending
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(700, 400)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Labelstatus)
        Me.Controls.Add(Me.LabelIsResting)
        Me.Controls.Add(Me.IsPausedLabel)
        Me.Controls.Add(Me.ProgressBarSending)
        Me.Controls.Add(Me.PanelCountDown)
        Me.Controls.Add(Me.ButtonViewLog)
        Me.Controls.Add(Me.LabelProgress)
        Me.Controls.Add(Me.ButtonPause)
        Me.Controls.Add(Me.ButtonStop)
        Me.Controls.Add(Me.ListViewNumbers)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmSending"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sending"
        Me.PanelCountDown.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ProgressBarSending As ProgressBar
    Friend WithEvents ListViewNumbers As ListView
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeaderKind As ColumnHeader
    Friend WithEvents ButtonStop As Button
    Friend WithEvents ButtonPause As Button
    Friend WithEvents LabelProgress As Label
    Friend WithEvents ButtonViewLog As Button
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ImageListWa As ImageList
    Friend WithEvents PanelCountDown As Panel
    Friend WithEvents LabelCountDown As Label
    Friend WithEvents LabelCountDownHeader As Label
    Friend WithEvents TimerCountDown As Timer
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ExportSuccessfulToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TimerMultiChannelCounter As Timer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents Label2 As Label
    Friend WithEvents IsPausedLabel As Label
    Friend WithEvents LabelIsResting As Label
    Friend WithEvents Labelstatus As Label
End Class
