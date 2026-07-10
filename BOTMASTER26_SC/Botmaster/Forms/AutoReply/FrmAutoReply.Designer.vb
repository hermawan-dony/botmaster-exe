<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAutoReply
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
        Me.ButtonEmoji = New System.Windows.Forms.Button()
        Me.BtnStrike = New System.Windows.Forms.Button()
        Me.BtnItalic = New System.Windows.Forms.Button()
        Me.BtnBold = New System.Windows.Forms.Button()
        Me.GroupBoxDivider = New System.Windows.Forms.GroupBox()
        Me.ButtonClear = New System.Windows.Forms.Button()
        Me.ButtonDelete = New System.Windows.Forms.Button()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.LabelAttachment = New System.Windows.Forms.Label()
        Me.LabelMessage = New System.Windows.Forms.Label()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ListViewAttachment = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuMediaType = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SetCaptionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripSeparator()
        Me.OpenFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextBoxMessage = New System.Windows.Forms.TextBox()
        Me.ContextMenuStripAttachFiles = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PhotosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DocumentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VideoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AudioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CreateNewCatalogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportSavedCatalogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel4 = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ContextMenuMediaType.SuspendLayout()
        Me.ContextMenuStripAttachFiles.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonEmoji
        '
        Me.ButtonEmoji.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.ButtonEmoji.FlatAppearance.BorderSize = 0
        Me.ButtonEmoji.Font = New System.Drawing.Font("Wingdings", 12.0!)
        Me.ButtonEmoji.ForeColor = System.Drawing.Color.Black
        Me.ButtonEmoji.Location = New System.Drawing.Point(341, 121)
        Me.ButtonEmoji.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonEmoji.Name = "ButtonEmoji"
        Me.ButtonEmoji.Size = New System.Drawing.Size(29, 25)
        Me.ButtonEmoji.TabIndex = 128
        Me.ButtonEmoji.Text = "J"
        Me.ButtonEmoji.UseVisualStyleBackColor = False
        '
        'BtnStrike
        '
        Me.BtnStrike.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnStrike.FlatAppearance.BorderSize = 0
        Me.BtnStrike.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnStrike.ForeColor = System.Drawing.Color.Black
        Me.BtnStrike.Location = New System.Drawing.Point(430, 121)
        Me.BtnStrike.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnStrike.Name = "BtnStrike"
        Me.BtnStrike.Size = New System.Drawing.Size(29, 25)
        Me.BtnStrike.TabIndex = 126
        Me.BtnStrike.Text = "S "
        Me.BtnStrike.UseVisualStyleBackColor = False
        '
        'BtnItalic
        '
        Me.BtnItalic.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnItalic.FlatAppearance.BorderSize = 0
        Me.BtnItalic.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnItalic.ForeColor = System.Drawing.Color.Black
        Me.BtnItalic.Location = New System.Drawing.Point(400, 121)
        Me.BtnItalic.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnItalic.Name = "BtnItalic"
        Me.BtnItalic.Size = New System.Drawing.Size(29, 25)
        Me.BtnItalic.TabIndex = 125
        Me.BtnItalic.Text = "I"
        Me.BtnItalic.UseVisualStyleBackColor = False
        '
        'BtnBold
        '
        Me.BtnBold.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(122, Byte), Integer))
        Me.BtnBold.FlatAppearance.BorderSize = 0
        Me.BtnBold.Font = New System.Drawing.Font("Arial Black", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBold.ForeColor = System.Drawing.Color.Black
        Me.BtnBold.Location = New System.Drawing.Point(370, 121)
        Me.BtnBold.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnBold.Name = "BtnBold"
        Me.BtnBold.Size = New System.Drawing.Size(29, 25)
        Me.BtnBold.TabIndex = 124
        Me.BtnBold.Text = "B"
        Me.BtnBold.UseVisualStyleBackColor = False
        '
        'GroupBoxDivider
        '
        Me.GroupBoxDivider.Location = New System.Drawing.Point(-15, 334)
        Me.GroupBoxDivider.Name = "GroupBoxDivider"
        Me.GroupBoxDivider.Size = New System.Drawing.Size(521, 7)
        Me.GroupBoxDivider.TabIndex = 123
        Me.GroupBoxDivider.TabStop = False
        '
        'ButtonClear
        '
        Me.ButtonClear.Location = New System.Drawing.Point(175, 299)
        Me.ButtonClear.Name = "ButtonClear"
        Me.ButtonClear.Size = New System.Drawing.Size(80, 26)
        Me.ButtonClear.TabIndex = 122
        Me.ButtonClear.Text = "Clear"
        Me.ButtonClear.UseVisualStyleBackColor = True
        '
        'ButtonDelete
        '
        Me.ButtonDelete.Location = New System.Drawing.Point(92, 299)
        Me.ButtonDelete.Name = "ButtonDelete"
        Me.ButtonDelete.Size = New System.Drawing.Size(80, 26)
        Me.ButtonDelete.TabIndex = 121
        Me.ButtonDelete.Text = "Delete"
        Me.ButtonDelete.UseVisualStyleBackColor = True
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Location = New System.Drawing.Point(9, 299)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(80, 26)
        Me.ButtonAdd.TabIndex = 119
        Me.ButtonAdd.Text = "Add"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'LabelAttachment
        '
        Me.LabelAttachment.AutoSize = True
        Me.LabelAttachment.Location = New System.Drawing.Point(9, 153)
        Me.LabelAttachment.Name = "LabelAttachment"
        Me.LabelAttachment.Size = New System.Drawing.Size(64, 13)
        Me.LabelAttachment.TabIndex = 118
        Me.LabelAttachment.Text = "Attachment "
        '
        'LabelMessage
        '
        Me.LabelMessage.AutoSize = True
        Me.LabelMessage.Location = New System.Drawing.Point(9, 6)
        Me.LabelMessage.Name = "LabelMessage"
        Me.LabelMessage.Size = New System.Drawing.Size(50, 13)
        Me.LabelMessage.TabIndex = 117
        Me.LabelMessage.Text = "Message"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(306, 353)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 25)
        Me.ButtonCancel.TabIndex = 114
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(384, 353)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 25)
        Me.ButtonOK.TabIndex = 113
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ListViewAttachment
        '
        Me.ListViewAttachment.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.ListViewAttachment.ContextMenuStrip = Me.ContextMenuMediaType
        Me.ListViewAttachment.FullRowSelect = True
        Me.ListViewAttachment.HideSelection = False
        Me.ListViewAttachment.Location = New System.Drawing.Point(9, 171)
        Me.ListViewAttachment.Name = "ListViewAttachment"
        Me.ListViewAttachment.Size = New System.Drawing.Size(450, 124)
        Me.ListViewAttachment.TabIndex = 112
        Me.ListViewAttachment.UseCompatibleStateImageBehavior = False
        Me.ListViewAttachment.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "File Name"
        Me.ColumnHeader1.Width = 226
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Type"
        Me.ColumnHeader2.Width = 84
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Caption"
        Me.ColumnHeader3.Width = 140
        '
        'ContextMenuMediaType
        '
        Me.ContextMenuMediaType.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetCaptionToolStripMenuItem, Me.ToolStripMenuItem2, Me.DeleteToolStripMenuItem1, Me.ToolStripMenuItem6, Me.OpenFileToolStripMenuItem})
        Me.ContextMenuMediaType.Name = "ContextMenuMediaType"
        Me.ContextMenuMediaType.Size = New System.Drawing.Size(136, 82)
        '
        'SetCaptionToolStripMenuItem
        '
        Me.SetCaptionToolStripMenuItem.Name = "SetCaptionToolStripMenuItem"
        Me.SetCaptionToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.SetCaptionToolStripMenuItem.Text = "Set Caption"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(132, 6)
        '
        'DeleteToolStripMenuItem1
        '
        Me.DeleteToolStripMenuItem1.Name = "DeleteToolStripMenuItem1"
        Me.DeleteToolStripMenuItem1.Size = New System.Drawing.Size(135, 22)
        Me.DeleteToolStripMenuItem1.Text = "Delete File"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(132, 6)
        '
        'OpenFileToolStripMenuItem
        '
        Me.OpenFileToolStripMenuItem.Name = "OpenFileToolStripMenuItem"
        Me.OpenFileToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.OpenFileToolStripMenuItem.Text = "Open File"
        '
        'TextBoxMessage
        '
        Me.TextBoxMessage.Location = New System.Drawing.Point(9, 22)
        Me.TextBoxMessage.Multiline = True
        Me.TextBoxMessage.Name = "TextBoxMessage"
        Me.TextBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxMessage.Size = New System.Drawing.Size(450, 95)
        Me.TextBoxMessage.TabIndex = 110
        '
        'ContextMenuStripAttachFiles
        '
        Me.ContextMenuStripAttachFiles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PhotosToolStripMenuItem, Me.DocumentsToolStripMenuItem, Me.VideoToolStripMenuItem, Me.AudioToolStripMenuItem, Me.ToolStripMenuItem1, Me.CreateNewCatalogToolStripMenuItem, Me.ImportSavedCatalogToolStripMenuItem})
        Me.ContextMenuStripAttachFiles.Name = "ContextMenuStripAttachFiles"
        Me.ContextMenuStripAttachFiles.Size = New System.Drawing.Size(189, 142)
        '
        'PhotosToolStripMenuItem
        '
        Me.PhotosToolStripMenuItem.Name = "PhotosToolStripMenuItem"
        Me.PhotosToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.PhotosToolStripMenuItem.Text = "Photos"
        '
        'DocumentsToolStripMenuItem
        '
        Me.DocumentsToolStripMenuItem.Name = "DocumentsToolStripMenuItem"
        Me.DocumentsToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.DocumentsToolStripMenuItem.Text = "Documents"
        '
        'VideoToolStripMenuItem
        '
        Me.VideoToolStripMenuItem.Name = "VideoToolStripMenuItem"
        Me.VideoToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.VideoToolStripMenuItem.Text = "Video"
        '
        'AudioToolStripMenuItem
        '
        Me.AudioToolStripMenuItem.Name = "AudioToolStripMenuItem"
        Me.AudioToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.AudioToolStripMenuItem.Text = "Audio Message"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(185, 6)
        '
        'CreateNewCatalogToolStripMenuItem
        '
        Me.CreateNewCatalogToolStripMenuItem.Name = "CreateNewCatalogToolStripMenuItem"
        Me.CreateNewCatalogToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.CreateNewCatalogToolStripMenuItem.Text = "Create New Catalog"
        '
        'ImportSavedCatalogToolStripMenuItem
        '
        Me.ImportSavedCatalogToolStripMenuItem.Name = "ImportSavedCatalogToolStripMenuItem"
        Me.ImportSavedCatalogToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ImportSavedCatalogToolStripMenuItem.Text = "Import Saved Catalog"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(493, 350)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(350, 20)
        Me.TextBox1.TabIndex = 129
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(837, 350)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(61, 20)
        Me.Button1.TabIndex = 130
        Me.Button1.Text = "Browse"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(890, 350)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(61, 20)
        Me.Button2.TabIndex = 131
        Me.Button2.Text = "Clear"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(490, 334)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 132
        Me.Label1.Text = "Catalog File"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(493, 373)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(77, 13)
        Me.LinkLabel1.TabIndex = 133
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Create Catalog"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(574, 373)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(64, 13)
        Me.LinkLabel2.TabIndex = 134
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Edit Catalog"
        '
        'LinkLabel3
        '
        Me.LinkLabel3.AutoSize = True
        Me.LinkLabel3.Location = New System.Drawing.Point(574, 441)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(64, 13)
        Me.LinkLabel3.TabIndex = 140
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "Edit Buttons"
        '
        'LinkLabel4
        '
        Me.LinkLabel4.AutoSize = True
        Me.LinkLabel4.Location = New System.Drawing.Point(493, 441)
        Me.LinkLabel4.Name = "LinkLabel4"
        Me.LinkLabel4.Size = New System.Drawing.Size(77, 13)
        Me.LinkLabel4.TabIndex = 139
        Me.LinkLabel4.TabStop = True
        Me.LinkLabel4.Text = "Create Buttons"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(490, 402)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 138
        Me.Label2.Text = "Buttons File"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(890, 418)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(61, 20)
        Me.Button3.TabIndex = 137
        Me.Button3.Text = "Clear"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(837, 418)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(61, 20)
        Me.Button4.TabIndex = 136
        Me.Button4.Text = "Browse"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(493, 418)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(350, 20)
        Me.TextBox2.TabIndex = 135
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(9, 124)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(100, 17)
        Me.CheckBox1.TabIndex = 141
        Me.CheckBox1.Text = "Include Buttons"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(115, 127)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 142
        Me.Label3.Text = "Config Buttons "
        '
        'FrmAutoReply
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(468, 388)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.LinkLabel3)
        Me.Controls.Add(Me.LinkLabel4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ButtonEmoji)
        Me.Controls.Add(Me.BtnStrike)
        Me.Controls.Add(Me.BtnItalic)
        Me.Controls.Add(Me.BtnBold)
        Me.Controls.Add(Me.GroupBoxDivider)
        Me.Controls.Add(Me.ButtonClear)
        Me.Controls.Add(Me.ButtonDelete)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.LabelAttachment)
        Me.Controls.Add(Me.LabelMessage)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ListViewAttachment)
        Me.Controls.Add(Me.TextBoxMessage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAutoReply"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AutoReply Message"
        Me.ContextMenuMediaType.ResumeLayout(False)
        Me.ContextMenuStripAttachFiles.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonEmoji As Button
    Friend WithEvents BtnStrike As Button
    Friend WithEvents BtnItalic As Button
    Friend WithEvents BtnBold As Button
    Friend WithEvents GroupBoxDivider As GroupBox
    Friend WithEvents ButtonClear As Button
    Friend WithEvents ButtonDelete As Button
    Friend WithEvents ButtonAdd As Button
    Friend WithEvents LabelAttachment As Label
    Friend WithEvents LabelMessage As Label
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ListViewAttachment As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents TextBoxMessage As TextBox
    Friend WithEvents ContextMenuStripAttachFiles As ContextMenuStrip
    Friend WithEvents PhotosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DocumentsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuMediaType As ContextMenuStrip
    Friend WithEvents SetCaptionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents DeleteToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As ToolStripSeparator
    Friend WithEvents OpenFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents LinkLabel2 As LinkLabel
    Friend WithEvents LinkLabel3 As LinkLabel
    Friend WithEvents LinkLabel4 As LinkLabel
    Friend WithEvents Label2 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents VideoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AudioToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents CreateNewCatalogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportSavedCatalogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label3 As Label
End Class
