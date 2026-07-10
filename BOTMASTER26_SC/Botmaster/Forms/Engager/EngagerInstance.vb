Imports System.Net
Imports Microsoft.Web
Imports Microsoft.Web.WebView2
Imports Microsoft.Web.WebView2.Core

Public Class EngagerInstance
    Inherits WebView2.WinForms.WebView2

#Region "Properties"
    Public Property AccountName As String
    Private Property IsInitialized As Boolean = False
    Private Property IsInitializing As Boolean = False
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Initializes the WebView2 instance asynchronously with proper error handling
    ''' </summary>
    Public Async Function InitializeAsync() As Task(Of Boolean)
        If IsInitialized OrElse IsInitializing Then
            Debug.WriteLine($"EngagerInstance [{AccountName}]: Already initialized or initializing")
            Return IsInitialized
        End If

        IsInitializing = True
        AccountName = Me.Name

        Try
            Dim profilePath As String = ClsSpecialDirectories.GetProfilesEngager() & AccountName
            Debug.WriteLine($"EngagerInstance [{AccountName}]: Creating WebView2 environment at: {profilePath}")

            ' Create environment asynchronously
            Dim webView2Environment = Await CoreWebView2Environment.CreateAsync(Nothing, profilePath)

            ' Ensure CoreWebView2 is initialized
            Await Me.EnsureCoreWebView2Async(webView2Environment)

            ' Set source URL
            Me.Source = New Uri("https://web.whatsapp.com")

            IsInitialized = True
            Debug.WriteLine($"EngagerInstance [{AccountName}]: Initialization successful")
            Return True

        Catch ex As Exception
            Debug.WriteLine($"EngagerInstance [{AccountName}]: Initialization failed - {ex.Message}")
            IsInitialized = False
            Return False

        Finally
            IsInitializing = False
        End Try
    End Function

    ''' <summary>
    ''' Legacy synchronous initialization method - calls async version
    ''' Deprecated: Use InitializeAsync() instead to avoid blocking
    ''' </summary>
    <Obsolete("Use InitializeAsync() instead to avoid UI blocking")>
    Public Sub Int()
        ' Fire and forget - non-blocking initialization
        ' The initialization will complete asynchronously
        Dim initTask = InitializeAsync()
    End Sub
#End Region

#Region "Event Handlers"
    Private Async Sub WhatsAppIntance_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles Me.NavigationCompleted
        Try
            If Not e.IsSuccess Then
                Debug.WriteLine($"EngagerInstance [{AccountName}]: Navigation failed with status: {e.WebErrorStatus}")
                Return
            End If

            If Not IsWebView2Ready() Then
                Debug.WriteLine($"EngagerInstance [{AccountName}]: WebView2 not ready for navigation completed")
                Return
            End If

            Debug.WriteLine($"EngagerInstance [{AccountName}]: Navigation completed, waiting for WhatsApp to load")

            ' Wait for WhatsApp elements to load with timeout
            Dim isLoaded = Await WaitForWhatsAppLoadAsync(TimeSpan.FromSeconds(30))

            If Not isLoaded Then
                Debug.WriteLine($"EngagerInstance [{AccountName}]: WhatsApp elements did not load in time")
                Return
            End If

            ' Additional delay to ensure UI is fully rendered
            Await Task.Delay(6000)

            ' Inject WAPI script
            Await InjectWAPIScriptAsync()

        Catch ex As Exception
            Debug.WriteLine($"EngagerInstance [{AccountName}]: NavigationCompleted error - {ex.Message}")
        End Try
    End Sub
#End Region

#Region "WhatsApp Operations"
    ''' <summary>
    ''' Waits for WhatsApp UI elements to load
    ''' </summary>
    Private Async Function WaitForWhatsAppLoadAsync(timeout As TimeSpan) As Task(Of Boolean)
        Dim startTime = DateTime.Now
        Dim checkInterval = 100 ' milliseconds

        While (DateTime.Now - startTime) < timeout
            Try
                If Not IsWebView2Ready() Then
                    Return False
                End If

                Dim result = Await Me.ExecuteScriptAsync("document.getElementsByClassName('copyable-text').length")
                Dim elementCount = Val(result)

                If elementCount > 0 Then
                    Debug.WriteLine($"EngagerInstance [{AccountName}]: WhatsApp loaded ({elementCount} elements found)")
                    Return True
                End If

            Catch ex As Exception
                Debug.WriteLine($"EngagerInstance [{AccountName}]: Error checking WhatsApp load - {ex.Message}")
            End Try

            Await Task.Delay(checkInterval)
        End While

        Return False
    End Function

    ''' <summary>
    ''' Injects the WAPI script into the page
    ''' </summary>
    Private Async Function InjectWAPIScriptAsync() As Task(Of Boolean)
        Try
            If Not IsWebView2Ready() Then
                Debug.WriteLine($"EngagerInstance [{AccountName}]: Cannot inject WAPI - WebView2 not ready")
                Return False
            End If

            Dim WAPI As String = WAPIScript
            WAPI = WAPI.Replace("(10452:?)", "")

            Await Me.ExecuteScriptAsync(WAPI)
            Debug.WriteLine($"EngagerInstance [{AccountName}]: WAPI script injected successfully")
            Return True

        Catch ex As Exception
            Debug.WriteLine($"EngagerInstance [{AccountName}]: WAPI injection failed - {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Sends a message to a WhatsApp number
    ''' </summary>
    Public Async Function SendMessageAsync(ByVal Number As String, ByVal Message As String) As Task(Of Boolean)
        Try
            If Not IsWebView2Ready() Then
                Debug.WriteLine($"EngagerInstance [{AccountName}]: Cannot send message - WebView2 not ready")
                Return False
            End If

            If String.IsNullOrWhiteSpace(Number) Then
                Debug.WriteLine($"EngagerInstance [{AccountName}]: Cannot send message - Number is empty")
                Return False
            End If

            Dim script = $"botmaster.sendMessageto('{Number}@c.us','{SafeJavaScript(Message)}',false)"
            Await Me.ExecuteScriptAsync(script)
            Debug.WriteLine($"EngagerInstance [{AccountName}]: Message sent to {Number}")
            Return True

        Catch ex As Exception
            Debug.WriteLine($"EngagerInstance [{AccountName}]: SendMessage failed - {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Legacy synchronous send message method
    ''' Deprecated: Use SendMessageAsync() instead to avoid blocking
    ''' </summary>
    <Obsolete("Use SendMessageAsync() instead to avoid UI blocking")>
    Public Sub SendMessage(ByVal Number As String, ByVal Message As String)
        ' Fire and forget - non-blocking send
        Dim sendTask = SendMessageAsync(Number, Message)
    End Sub

    ''' <summary>
    ''' Checks if the user is logged into WhatsApp
    ''' </summary>
    Public Async Function IsLoggedIn() As Task(Of Boolean)
        Try
            If Not IsWebView2Ready() Then
                Return False
            End If

            Dim result = Await Me.ExecuteScriptAsync("WAPI.isLoggedIn()")
            Return Boolean.Parse(result)

        Catch ex As Exception
            Debug.WriteLine($"EngagerInstance [{AccountName}]: IsLoggedIn check failed - {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Gets the current user's WhatsApp ID
    ''' </summary>
    Public Async Function GetMe() As Task(Of String)
        Try
            If Not IsWebView2Ready() Then
                Return ""
            End If

            Dim result = Await Me.ExecuteScriptAsync("WPP.conn.getMyUserId().user")
            Return result?.Trim(""""c) ' Remove quotes from JSON result

        Catch ex As Exception
            Debug.WriteLine($"EngagerInstance [{AccountName}]: GetMe failed - {ex.Message}")
            Return ""
        End Try
    End Function
#End Region

#Region "Helper Methods"
    ''' <summary>
    ''' Checks if WebView2 is ready for script execution
    ''' </summary>
    Private Function IsWebView2Ready() As Boolean
        Return IsInitialized AndAlso
               Me.CoreWebView2 IsNot Nothing AndAlso
               Not Me.IsDisposed
    End Function
#End Region

#Region "Disposal"
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing Then
                Debug.WriteLine($"EngagerInstance [{AccountName}]: Disposing")
                IsInitialized = False
                IsInitializing = False
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
#End Region

End Class
