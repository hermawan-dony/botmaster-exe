<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBrowser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBrowser))
        Me.TimerEvent = New System.Windows.Forms.Timer(Me.components)
        Me.TimerInitiateWAPI = New System.Windows.Forms.Timer(Me.components)
        Me.TimerIsWhatsAppLoggedIn = New System.Windows.Forms.Timer(Me.components)
        Me.TimerReceive = New System.Windows.Forms.Timer(Me.components)
        Me.WebView2 = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button16 = New System.Windows.Forms.Button()
        CType(Me.WebView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TimerEvent
        '
        Me.TimerEvent.Enabled = True
        Me.TimerEvent.Interval = 1000
        '
        'TimerInitiateWAPI
        '
        Me.TimerInitiateWAPI.Enabled = True
        Me.TimerInitiateWAPI.Interval = 1000
        '
        'TimerIsWhatsAppLoggedIn
        '
        Me.TimerIsWhatsAppLoggedIn.Enabled = True
        Me.TimerIsWhatsAppLoggedIn.Interval = 1000
        '
        'TimerReceive
        '
        Me.TimerReceive.Interval = 1000
        '
        'WebView2
        '
        Me.WebView2.CreationProperties = Nothing
        Me.WebView2.DefaultBackgroundColor = System.Drawing.Color.White
        Me.WebView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebView2.Location = New System.Drawing.Point(0, 34)
        Me.WebView2.Name = "WebView2"
        Me.WebView2.Size = New System.Drawing.Size(1049, 576)
        Me.WebView2.TabIndex = 0
        Me.WebView2.ZoomFactor = 1.0R
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.Button16)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1049, 34)
        Me.Panel1.TabIndex = 1
        '
        'Button16
        '
        Me.Button16.BackColor = System.Drawing.SystemColors.Control
        Me.Button16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button16.FlatAppearance.BorderSize = 0
        Me.Button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button16.ForeColor = System.Drawing.Color.Black
        Me.Button16.Location = New System.Drawing.Point(958, 0)
        Me.Button16.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Button16.Name = "Button16"
        Me.Button16.Padding = New System.Windows.Forms.Padding(2)
        Me.Button16.Size = New System.Drawing.Size(91, 34)
        Me.Button16.TabIndex = 108
        Me.Button16.Text = "Refresh"
        Me.Button16.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button16.UseVisualStyleBackColor = False
        '
        'FrmBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1049, 610)
        Me.Controls.Add(Me.WebView2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmBrowser"
        Me.Text = "FrmBrowser"
        CType(Me.WebView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TimerEvent As Timer
    Friend WithEvents TimerInitiateWAPI As Timer
    Friend WithEvents TimerIsWhatsAppLoggedIn As Timer
    Friend WithEvents TimerReceive As Timer
    Friend WithEvents WebView2 As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button16 As Button
End Class
