Public Class FrmMessages
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dg As New OpenFileDialog
        dg.Filter = "*.txt|*.txt"
        If dg.ShowDialog() = DialogResult.OK Then
            If dg.FileName <> "" Then
                For Each t In IO.File.ReadAllLines(dg.FileName)
                    If t.Trim <> "" Then
                        ListBox1.Items.Add(t)
                    End If
                Next
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ListBox1.Items.Clear()
    End Sub
End Class
