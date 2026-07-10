Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms
Imports Newtonsoft.Json
Public Class WAWebview2
    Inherits WebView2
    Public Sub Int(ByVal AccountName As String)

        Dim webView2Environment
        Try
            webView2Environment = CoreWebView2Environment.CreateAsync(Nothing, ClsSpecialDirectories.GetAccountFolder & AccountName).Result
            Me.EnsureCoreWebView2Async(webView2Environment)
            Me.Source = New Uri("https://web.whatsapp.com")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub WhatsAppIntance_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles Me.NavigationCompleted
        Try
            Me.ExecuteScriptAsync(WAPIScript)
        Catch ex As Exception

        End Try

    End Sub
    Public Async Sub SendMessage(ByVal Number As String, ByVal Message As String)
        Try
            Await Me.ExecuteScriptAsync($"WAPI.sendMessage('{Number}@c.us','{Message}')")
        Catch ex As Exception

        End Try
    End Sub
    Public Async Function IsLoggedIn() As Task(Of Boolean)
        Try
            Return Await Me.ExecuteScriptAsync("WAPI.isLoggedIn()")
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Async Function GetMe() As Task(Of String)
        Try
            Return Await Me.ExecuteScriptAsync("Store.MaybeMeUser.getMaybeMeUser().user")
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Class
