Imports System.ComponentModel
Imports Newtonsoft
Imports Newtonsoft.Json
Public Class FrmAccounts
    Private Sub FrmAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        FrmAddAccount.ShowDialog()
    End Sub

    Public Sub PopulateChannels()
        For Each wa In WaWebviewChannels
            For Each tb As TabPage In TabControl1.TabPages
                If tb.Name = wa.AccountName Then

                End If
            Next
        Next
    End Sub

    Public Sub AddBrowsertoTab(ByRef wa As WaChannel)
        Dim waTab As New TabPage
        waTab.Name = wa.AccountName
        waTab.Text = waTab.Name
        wa.WA.Dock = DockStyle.Fill
        wa.WA.Visible = True
        waTab.Controls.Add(wa.WA)
        TabControl1.TabPages.Add(waTab)
        TabControl1.SelectTab(waTab)
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Dim selectedWA As WaChannel = Nothing
        If TabControl1.TabPages.Count > 0 Then
            If MsgBox($"Are you sure you want to delete {TabControl1.SelectedTab} ?", vbQuestion + vbYesNo, Application.ProductName) = vbYes Then
                For Each wa As WaChannel In WaWebviewChannels
                    If wa.AccountName = TabControl1.SelectedTab.Name Then
                        selectedWA = wa
                    End If
                Next
                WaWebviewChannels.Remove(selectedWA)
                selectedWA.WA.Dispose()
                TabControl1.TabPages.Remove(TabControl1.SelectedTab)
                SaveChannels()
            End If
        End If
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmAccounts_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        Me.Hide()
    End Sub
End Class
