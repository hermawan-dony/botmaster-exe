Public Class FrmIntances
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AddWAInstance()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not IsNothing(TabControl1.SelectedTab) Then
            RemoveWAInstance(TabControl1.SelectedTab.Text)
        End If

    End Sub
End Class