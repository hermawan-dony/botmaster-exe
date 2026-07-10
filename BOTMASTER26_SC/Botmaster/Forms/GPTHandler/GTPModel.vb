Public Class GTPModel
    Public Property APIKey As String
    Public Property Language As String
    Public Property SystemMessage As String

    Public Property Prompts As New List(Of GPTPrompt)

End Class
Public Class GPTPrompt
    Public Property Question As String
    Public Property Answer As String
End Class