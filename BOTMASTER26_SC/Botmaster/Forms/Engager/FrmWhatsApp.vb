Public Class FrmWhatsApp
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnAddAccount.Click
        AddWhatsAppAccount()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not IsNothing(TabControl1.SelectedTab) Then
            RemoveInstance(TabControl1.SelectedTab.Text)
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        ApplyColor(Me)
    End Sub
End Class