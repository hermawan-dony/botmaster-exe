Imports System.Drawing
Imports System.Windows.Forms

Public Module ThemeManager
    Public Sub ApplyTheme(frm As Form)
        Try
            Dim theme As String = GetSetting(Application.ProductName, "Settings", "AppTheme", "Cyberpunk")
            
            Dim bMain As Color
            Dim bContainer As Color
            Dim accent As Color
            Dim foreCol As Color = Color.White
            
            If theme = "Enterprise Dark" Then
                bMain = Color.FromArgb(18, 18, 18)
                bContainer = Color.FromArgb(30, 30, 30)
                accent = Color.FromArgb(0, 122, 204)
            Else ' Cyberpunk
                bMain = Color.FromArgb(10, 11, 23)
                bContainer = Color.FromArgb(15, 17, 33)
                accent = Color.FromArgb(0, 230, 230)
            End If
            
            frm.BackColor = bMain
            frm.ForeColor = foreCol
            
            ApplyToControls(frm.Controls, bMain, bContainer, accent, foreCol)
        Catch ex As Exception
        End Try
    End Sub
    
    Private Sub ApplyToControls(controls As Control.ControlCollection, bMain As Color, bContainer As Color, accent As Color, foreCol As Color)
        For Each c As Control In controls
            If TypeOf c Is Panel OrElse TypeOf c Is GroupBox OrElse TypeOf c Is TabPage Then
                c.BackColor = bMain
                c.ForeColor = foreCol
            End If
            
            If TypeOf c Is TextBox OrElse TypeOf c Is ComboBox OrElse TypeOf c Is ListBox OrElse TypeOf c Is RichTextBox Then
                c.BackColor = bContainer
                c.ForeColor = foreCol
                Try
                    ' Remove border for modern look if flat
                    DirectCast(c, TextBoxBase).BorderStyle = BorderStyle.FixedSingle
                Catch
                End Try
            End If
            
            If TypeOf c Is Button Then
                Dim btn As Button = DirectCast(c, Button)
                If btn.FlatStyle = FlatStyle.Flat OrElse btn.FlatStyle = FlatStyle.Popup Then
                    btn.BackColor = accent
                    btn.ForeColor = If(accent.R + accent.G + accent.B > 382, Color.Black, Color.White)
                    btn.FlatAppearance.BorderSize = 0
                End If
            End If
            
            If TypeOf c Is Label OrElse TypeOf c Is CheckBox OrElse TypeOf c Is RadioButton Then
                c.BackColor = Color.Transparent
                c.ForeColor = foreCol
            End If
            
            If TypeOf c Is DataGridView Then
                Dim dgv As DataGridView = DirectCast(c, DataGridView)
                dgv.BackgroundColor = bContainer
                dgv.GridColor = bMain
                dgv.DefaultCellStyle.BackColor = bContainer
                dgv.DefaultCellStyle.ForeColor = foreCol
                dgv.DefaultCellStyle.SelectionBackColor = accent
                dgv.DefaultCellStyle.SelectionForeColor = Color.White
                dgv.ColumnHeadersDefaultCellStyle.BackColor = bMain
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = accent
                dgv.EnableHeadersVisualStyles = False
            End If
            
            If c.HasChildren Then
                ApplyToControls(c.Controls, bMain, bContainer, accent, foreCol)
            End If
        Next
    End Sub
End Module
