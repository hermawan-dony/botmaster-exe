<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmFilter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFilter))
        Me.TimerVerification = New System.Windows.Forms.Timer(Me.components)
        Me.ListViewNumbers = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.NumericUpDownDelay = New System.Windows.Forms.NumericUpDown()
        Me.ButtonImportFromFile = New System.Windows.Forms.Button()
        Me.ButtonManualImport = New System.Windows.Forms.Button()
        Me.ButtonNumberGenerator = New System.Windows.Forms.Button()
        Me.ButtonExport = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ButtonStop = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ProgressBarRequest = New System.Windows.Forms.ProgressBar()
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar()
        Me.LabelRequestProgress = New System.Windows.Forms.Label()
        Me.LabelVerificationProgress = New System.Windows.Forms.Label()
        Me.ButtonClear = New System.Windows.Forms.Button()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.ButtonImporttosender = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.LBLStatus = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RegularToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BusinessToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.NumericUpDownDelay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TimerVerification
        '
        Me.TimerVerification.Enabled = True
        Me.TimerVerification.Interval = 1000
        '
        'ListViewNumbers
        '
        Me.ListViewNumbers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader4, Me.ColumnHeader3})
        Me.ListViewNumbers.FullRowSelect = True
        Me.ListViewNumbers.GridLines = True
        Me.ListViewNumbers.HideSelection = False
        Me.ListViewNumbers.Location = New System.Drawing.Point(8, 78)
        Me.ListViewNumbers.Name = "ListViewNumbers"
        Me.ListViewNumbers.Size = New System.Drawing.Size(584, 290)
        Me.ListViewNumbers.TabIndex = 0
        Me.ListViewNumbers.UseCompatibleStateImageBehavior = False
        Me.ListViewNumbers.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Name"
        Me.ColumnHeader1.Width = 174
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Number"
        Me.ColumnHeader2.Width = 140
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Status"
        Me.ColumnHeader4.Width = 76
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Is Business"
        Me.ColumnHeader3.Width = 108
        '
        'NumericUpDownDelay
        '
        Me.NumericUpDownDelay.Location = New System.Drawing.Point(9, 16)
        Me.NumericUpDownDelay.Name = "NumericUpDownDelay"
        Me.NumericUpDownDelay.Size = New System.Drawing.Size(47, 20)
        Me.NumericUpDownDelay.TabIndex = 0
        '
        'ButtonImportFromFile
        '
        Me.ButtonImportFromFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ButtonImportFromFile.FlatAppearance.BorderSize = 0
        Me.ButtonImportFromFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonImportFromFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonImportFromFile.ForeColor = System.Drawing.Color.White
        Me.ButtonImportFromFile.Location = New System.Drawing.Point(7, 35)
        Me.ButtonImportFromFile.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.ButtonImportFromFile.Name = "ButtonImportFromFile"
        Me.ButtonImportFromFile.Padding = New System.Windows.Forms.Padding(2)
        Me.ButtonImportFromFile.Size = New System.Drawing.Size(60, 30)
        Me.ButtonImportFromFile.TabIndex = 106
        Me.ButtonImportFromFile.Text = "Files"
        Me.ButtonImportFromFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonImportFromFile.UseVisualStyleBackColor = False
        '
        'ButtonManualImport
        '
        Me.ButtonManualImport.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ButtonManualImport.FlatAppearance.BorderSize = 0
        Me.ButtonManualImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonManualImport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonManualImport.ForeColor = System.Drawing.Color.White
        Me.ButtonManualImport.Location = New System.Drawing.Point(68, 35)
        Me.ButtonManualImport.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.ButtonManualImport.Name = "ButtonManualImport"
        Me.ButtonManualImport.Padding = New System.Windows.Forms.Padding(2)
        Me.ButtonManualImport.Size = New System.Drawing.Size(57, 30)
        Me.ButtonManualImport.TabIndex = 107
        Me.ButtonManualImport.Text = "Manual"
        Me.ButtonManualImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonManualImport.UseVisualStyleBackColor = False
        '
        'ButtonNumberGenerator
        '
        Me.ButtonNumberGenerator.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ButtonNumberGenerator.FlatAppearance.BorderSize = 0
        Me.ButtonNumberGenerator.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonNumberGenerator.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonNumberGenerator.ForeColor = System.Drawing.Color.White
        Me.ButtonNumberGenerator.Location = New System.Drawing.Point(126, 35)
        Me.ButtonNumberGenerator.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.ButtonNumberGenerator.Name = "ButtonNumberGenerator"
        Me.ButtonNumberGenerator.Padding = New System.Windows.Forms.Padding(2)
        Me.ButtonNumberGenerator.Size = New System.Drawing.Size(123, 30)
        Me.ButtonNumberGenerator.TabIndex = 108
        Me.ButtonNumberGenerator.Text = "Numbers Generator"
        Me.ButtonNumberGenerator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonNumberGenerator.UseVisualStyleBackColor = False
        '
        'ButtonExport
        '
        Me.ButtonExport.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ButtonExport.FlatAppearance.BorderSize = 0
        Me.ButtonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonExport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonExport.ForeColor = System.Drawing.Color.White
        Me.ButtonExport.Location = New System.Drawing.Point(518, 35)
        Me.ButtonExport.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.ButtonExport.Name = "ButtonExport"
        Me.ButtonExport.Padding = New System.Windows.Forms.Padding(2)
        Me.ButtonExport.Size = New System.Drawing.Size(72, 30)
        Me.ButtonExport.TabIndex = 115
        Me.ButtonExport.Text = "Export"
        Me.ButtonExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonExport.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(368, 489)
        Me.Button1.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Padding = New System.Windows.Forms.Padding(2)
        Me.Button1.Size = New System.Drawing.Size(124, 32)
        Me.Button1.TabIndex = 121
        Me.Button1.Text = "Start Checking"
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = False
        '
        'ButtonStop
        '
        Me.ButtonStop.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ButtonStop.Enabled = False
        Me.ButtonStop.FlatAppearance.BorderSize = 0
        Me.ButtonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonStop.ForeColor = System.Drawing.Color.White
        Me.ButtonStop.Location = New System.Drawing.Point(240, 489)
        Me.ButtonStop.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.ButtonStop.Name = "ButtonStop"
        Me.ButtonStop.Padding = New System.Windows.Forms.Padding(2)
        Me.ButtonStop.Size = New System.Drawing.Size(124, 32)
        Me.ButtonStop.TabIndex = 122
        Me.ButtonStop.Text = "Stop "
        Me.ButtonStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonStop.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.NumericUpDownDelay)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 481)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(104, 42)
        Me.GroupBox1.TabIndex = 123
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Delay"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(62, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Sec"
        '
        'ProgressBarRequest
        '
        Me.ProgressBarRequest.Location = New System.Drawing.Point(8, 414)
        Me.ProgressBarRequest.Name = "ProgressBarRequest"
        Me.ProgressBarRequest.Size = New System.Drawing.Size(580, 13)
        Me.ProgressBarRequest.TabIndex = 131
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Location = New System.Drawing.Point(8, 455)
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(577, 13)
        Me.ProgressBar2.TabIndex = 132
        '
        'LabelRequestProgress
        '
        Me.LabelRequestProgress.AutoSize = True
        Me.LabelRequestProgress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelRequestProgress.Location = New System.Drawing.Point(8, 396)
        Me.LabelRequestProgress.Name = "LabelRequestProgress"
        Me.LabelRequestProgress.Size = New System.Drawing.Size(91, 13)
        Me.LabelRequestProgress.TabIndex = 133
        Me.LabelRequestProgress.Text = "Request Progress"
        '
        'LabelVerificationProgress
        '
        Me.LabelVerificationProgress.AutoSize = True
        Me.LabelVerificationProgress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelVerificationProgress.Location = New System.Drawing.Point(8, 438)
        Me.LabelVerificationProgress.Name = "LabelVerificationProgress"
        Me.LabelVerificationProgress.Size = New System.Drawing.Size(103, 13)
        Me.LabelVerificationProgress.TabIndex = 134
        Me.LabelVerificationProgress.Text = "Verification Progress"
        '
        'ButtonClear
        '
        Me.ButtonClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ButtonClear.FlatAppearance.BorderSize = 0
        Me.ButtonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClear.ForeColor = System.Drawing.Color.White
        Me.ButtonClear.Location = New System.Drawing.Point(358, 35)
        Me.ButtonClear.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.ButtonClear.Name = "ButtonClear"
        Me.ButtonClear.Padding = New System.Windows.Forms.Padding(2)
        Me.ButtonClear.Size = New System.Drawing.Size(66, 30)
        Me.ButtonClear.TabIndex = 135
        Me.ButtonClear.Text = "Clear"
        Me.ButtonClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonClear.UseVisualStyleBackColor = False
        '
        'ButtonClose
        '
        Me.ButtonClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ButtonClose.FlatAppearance.BorderSize = 0
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClose.ForeColor = System.Drawing.Color.White
        Me.ButtonClose.Location = New System.Drawing.Point(495, 489)
        Me.ButtonClose.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Padding = New System.Windows.Forms.Padding(2)
        Me.ButtonClose.Size = New System.Drawing.Size(90, 32)
        Me.ButtonClose.TabIndex = 136
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonClose.UseVisualStyleBackColor = False
        '
        'ButtonImporttosender
        '
        Me.ButtonImporttosender.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ButtonImporttosender.FlatAppearance.BorderSize = 0
        Me.ButtonImporttosender.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonImporttosender.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonImporttosender.ForeColor = System.Drawing.Color.White
        Me.ButtonImporttosender.Location = New System.Drawing.Point(425, 35)
        Me.ButtonImporttosender.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.ButtonImporttosender.Name = "ButtonImporttosender"
        Me.ButtonImporttosender.Padding = New System.Windows.Forms.Padding(2)
        Me.ButtonImporttosender.Size = New System.Drawing.Size(92, 30)
        Me.ButtonImporttosender.TabIndex = 138
        Me.ButtonImporttosender.Text = "Import to Sender"
        Me.ButtonImporttosender.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonImporttosender.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(254, 35)
        Me.Button2.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Padding = New System.Windows.Forms.Padding(2)
        Me.Button2.Size = New System.Drawing.Size(103, 30)
        Me.Button2.TabIndex = 139
        Me.Button2.Text = "Insert Country Code"
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button2.UseVisualStyleBackColor = False
        '
        'LBLStatus
        '
        Me.LBLStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLStatus.ForeColor = System.Drawing.Color.Black
        Me.LBLStatus.Location = New System.Drawing.Point(6, 373)
        Me.LBLStatus.Name = "LBLStatus"
        Me.LBLStatus.Size = New System.Drawing.Size(485, 15)
        Me.LBLStatus.TabIndex = 140
        Me.LBLStatus.Text = "Total:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 141
        Me.Label2.Text = "Imports"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AllToolStripMenuItem, Me.ToolStripMenuItem1, Me.RegularToolStripMenuItem, Me.BusinessToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(120, 76)
        '
        'AllToolStripMenuItem
        '
        Me.AllToolStripMenuItem.Name = "AllToolStripMenuItem"
        Me.AllToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.AllToolStripMenuItem.Text = "All"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(116, 6)
        '
        'RegularToolStripMenuItem
        '
        Me.RegularToolStripMenuItem.Name = "RegularToolStripMenuItem"
        Me.RegularToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.RegularToolStripMenuItem.Text = "Regular"
        '
        'BusinessToolStripMenuItem
        '
        Me.BusinessToolStripMenuItem.Name = "BusinessToolStripMenuItem"
        Me.BusinessToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.BusinessToolStripMenuItem.Text = "Business"
        '
        'FrmFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(600, 529)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ButtonImportFromFile)
        Me.Controls.Add(Me.LBLStatus)
        Me.Controls.Add(Me.ButtonManualImport)
        Me.Controls.Add(Me.ButtonNumberGenerator)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ButtonImporttosender)
        Me.Controls.Add(Me.ButtonClear)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.LabelVerificationProgress)
        Me.Controls.Add(Me.LabelRequestProgress)
        Me.Controls.Add(Me.ProgressBar2)
        Me.Controls.Add(Me.ProgressBarRequest)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ButtonStop)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ButtonExport)
        Me.Controls.Add(Me.ListViewNumbers)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WhatsApp Numbers Filter"
        CType(Me.NumericUpDownDelay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TimerVerification As Timer
    Friend WithEvents ListViewNumbers As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents NumericUpDownDelay As NumericUpDown
    Friend WithEvents ButtonImportFromFile As Button
    Friend WithEvents ButtonManualImport As Button
    Friend WithEvents ButtonNumberGenerator As Button
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ButtonExport As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents ButtonStop As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ProgressBarRequest As ProgressBar
    Friend WithEvents ProgressBar2 As ProgressBar
    Friend WithEvents LabelRequestProgress As Label
    Friend WithEvents LabelVerificationProgress As Label
    Friend WithEvents ButtonClear As Button
    Friend WithEvents ButtonClose As Button
    Friend WithEvents ButtonImporttosender As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents LBLStatus As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents AllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents RegularToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BusinessToolStripMenuItem As ToolStripMenuItem
End Class
