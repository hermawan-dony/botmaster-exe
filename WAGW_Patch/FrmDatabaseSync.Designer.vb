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
        Me.TextBoxDSN = New System.Windows.Forms.TextBox()
        Me.ListBoxLog = New System.Windows.Forms.ListBox()
        Me.TimerSync = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'LabelDSN
        '
        Me.LabelDSN.AutoSize = True
        Me.LabelDSN.Location = New System.Drawing.Point(17, 18)
        Me.LabelDSN.Name = "LabelDSN"
        Me.LabelDSN.Size = New System.Drawing.Size(117, 13)
        Me.LabelDSN.TabIndex = 7
        Me.LabelDSN.Text = "ODBC DSN Connection"
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(224, 218)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(89, 32)
        Me.BtnCancel.TabIndex = 6
        Me.BtnCancel.Text = "Close"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(319, 218)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(107, 32)
        Me.BtnOK.TabIndex = 5
        Me.BtnOK.Text = "Start Auto-Sync"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'TextBoxDSN
        '
        Me.TextBoxDSN.Location = New System.Drawing.Point(20, 34)
        Me.TextBoxDSN.Name = "TextBoxDSN"
        Me.TextBoxDSN.Size = New System.Drawing.Size(406, 20)
        Me.TextBoxDSN.TabIndex = 4
        Me.TextBoxDSN.Text = "DSN=MyDatabase;"
        '
        'ListBoxLog
        '
        Me.ListBoxLog.FormattingEnabled = True
        Me.ListBoxLog.Location = New System.Drawing.Point(20, 71)
        Me.ListBoxLog.Name = "ListBoxLog"
        Me.ListBoxLog.Size = New System.Drawing.Size(406, 134)
        Me.ListBoxLog.TabIndex = 8
        '
        'FrmDatabaseSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(447, 262)
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
    Friend WithEvents TextBoxDSN As TextBox
    Friend WithEvents ListBoxLog As ListBox
    Friend WithEvents TimerSync As Timer
End Class
