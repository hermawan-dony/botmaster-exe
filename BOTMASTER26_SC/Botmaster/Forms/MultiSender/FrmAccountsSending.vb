Public Class FrmAccountsSending
    Public AccountsList As New List(Of WAWebview2)
    Public Messages As List(Of String)
    Public BulkDestinations As List(Of DestinationModel)
    Public BulkAttachments As New List(Of AttachmentModel)
    Private Sub FrmAccountsSending_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class