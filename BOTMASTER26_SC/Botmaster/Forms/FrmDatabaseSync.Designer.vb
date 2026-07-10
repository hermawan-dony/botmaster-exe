<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDatabaseSync
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LabelDSN = New System.Windows.Forms.Label()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.TextBoxDSN = New System.Windows.Forms.ComboBox()
        Me.ListBoxLog = New System.Windows.Forms.ListBox()
        Me.TimerSync = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'LabelDSN
        '
        Me.LabelDSN.AutoSize = True
        Me.LabelDSN.Location = New System.Drawing.Point(17, 12)
        Me.LabelDSN.Name = "LabelDSN"
        Me.LabelDSN.Size = New System.Drawing.Size(117, 13)
        Me.LabelDSN.TabIndex = 7
        Me.LabelDSN.Text = "ODBC DSN Connection"
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(224, 350)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(89, 32)
        Me.BtnCancel.TabIndex = 6
        Me.BtnCancel.Text = "Close"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(319, 350)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(107, 32)
        Me.BtnOK.TabIndex = 5
        Me.BtnOK.Text = "Start Auto-Sync"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'TextBoxDSN
        '
        Me.TextBoxDSN.Location = New System.Drawing.Point(20, 28)
        Me.TextBoxDSN.Name = "TextBoxDSN"
        Me.TextBoxDSN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TextBoxDSN.Size = New System.Drawing.Size(406, 21)
        Me.TextBoxDSN.TabIndex = 4
        Me.TextBoxDSN.Text = "DSN=MyDatabase;"
        '
        'ListBoxLog
        '
        Me.ListBoxLog.FormattingEnabled = True
        Me.ListBoxLog.Location = New System.Drawing.Point(20, 95)
        Me.ListBoxLog.Name = "ListBoxLog"
        Me.ListBoxLog.Size = New System.Drawing.Size(406, 121)
        Me.ListBoxLog.TabIndex = 8
        '
        'LabelActivated
        '
        Me.LabelActivated = New System.Windows.Forms.Label()
        Me.LabelActivated.AutoSize = True
        Me.LabelActivated.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelActivated.ForeColor = System.Drawing.Color.LimeGreen
        Me.LabelActivated.Location = New System.Drawing.Point(20, 65)
        Me.LabelActivated.Name = "LabelActivated"
        Me.LabelActivated.Size = New System.Drawing.Size(133, 16)
        Me.LabelActivated.Text = "WAGW Activated!!"
        Me.LabelActivated.Visible = False
        '
        'LabelSQL
        '
        Me.LabelSQL = New System.Windows.Forms.Label()
        Me.LabelSQL.AutoSize = True
        Me.LabelSQL.Location = New System.Drawing.Point(20, 230)
        Me.LabelSQL.Name = "LabelSQL"
        Me.LabelSQL.Size = New System.Drawing.Size(155, 13)
        Me.LabelSQL.Text = "SQL Test Script (Insert Outbox):"
        '
        'TextBoxSQL
        '
        Me.TextBoxSQL = New System.Windows.Forms.TextBox()
        Me.TextBoxSQL.Location = New System.Drawing.Point(20, 250)
        Me.TextBoxSQL.Multiline = True
        Me.TextBoxSQL.Name = "TextBoxSQL"
        Me.TextBoxSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxSQL.Size = New System.Drawing.Size(406, 80)
        Me.TextBoxSQL.TabIndex = 9
        Me.TextBoxSQL.Text = "INSERT INTO outbox (wa_no, wa_text) VALUES ('628512345678', 'Test WAGW message')"
        '
        'BtnTestSQL
        '
        Me.BtnTestSQL = New System.Windows.Forms.Button()
        Me.BtnTestSQL.Location = New System.Drawing.Point(20, 350)
        Me.BtnTestSQL.Name = "BtnTestSQL"
        Me.BtnTestSQL.Size = New System.Drawing.Size(120, 32)
        Me.BtnTestSQL.TabIndex = 10
        Me.BtnTestSQL.Text = "Run SQL Test"
        Me.BtnTestSQL.UseVisualStyleBackColor = True
        '
        'FrmDatabaseSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(447, 400)
        Me.Controls.Add(Me.LabelActivated)
        Me.Controls.Add(Me.LabelSQL)
        Me.Controls.Add(Me.TextBoxSQL)
        Me.Controls.Add(Me.BtnTestSQL)
        Me.Controls.Add(Me.ListBoxLog)
        Me.Controls.Add(Me.LabelDSN)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.TextBoxDSN)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDatabaseSync"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Database Auto-Sync"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelDSN As Label
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnOK As Button
    Friend WithEvents TextBoxDSN As ComboBox
    Friend WithEvents ListBoxLog As ListBox
    Friend WithEvents TimerSync As Timer
    Friend WithEvents LabelActivated As Label
    Friend WithEvents LabelSQL As Label
    Friend WithEvents TextBoxSQL As TextBox
    Friend WithEvents BtnTestSQL As Button
End Class

