Imports System.Data.Odbc

Public Class FrmDatabaseSync
    Private IsSyncing As Boolean = False
    Private DSN As String = ""

    Private Sub FrmDatabaseSync_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try : ThemeManager.ApplyTheme(Me) : Catch : End Try
        Text = "Database Auto-Sync"
        TextBoxDSN.Text = "DSN=BotmasterDB;"
        TimerSync.Interval = 5000
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Close()
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If IsSyncing Then
            TimerSync.Stop()
            IsSyncing = False
            BtnOK.Text = "Start Auto-Sync"
            LogMsg("Auto-Sync stopped.")
        Else
            Try
                DSN = TextBoxDSN.Text
                Dim conn As New OdbcConnection(DSN)
                conn.Open()
                
                Try
                    Dim cmd As New OdbcCommand("CREATE TABLE outbox (id INT, wa_no VARCHAR(50), wa_text VARCHAR(2000), wa_media VARCHAR(255), status VARCHAR(20))", conn)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                End Try
                Try
                    Dim cmd As New OdbcCommand("CREATE TABLE inbox (id INT, wa_no VARCHAR(50), wa_text VARCHAR(2000), receive_time VARCHAR(50))", conn)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                End Try
                
                conn.Close()

                TimerSync.Start()
                IsSyncing = True
                BtnOK.Text = "Stop Auto-Sync"
                LogMsg("Auto-Sync started. Connected to DSN.")
            Catch ex As Exception
                MsgBox("Connection failed: " & ex.Message, vbCritical)
            End Try
        End If
    End Sub

    Private Sub TimerSync_Tick(sender As Object, e As EventArgs) Handles TimerSync.Tick
        TimerSync.Stop()
        Try
            Dim conn As New OdbcConnection(DSN)
            conn.Open()
            Dim cmd As New OdbcCommand("SELECT id, wa_no, wa_text, wa_media FROM outbox WHERE status='pending'", conn)
            Dim reader As OdbcDataReader = cmd.ExecuteReader()
            
            Dim updates As New List(Of String)

            While reader.Read()
                Dim id As String = reader("id").ToString()
                Dim wa_no As String = reader("wa_no").ToString()
                Dim wa_text As String = reader("wa_text").ToString()
                Dim wa_media As String = reader("wa_media").ToString()
                
                LogMsg("Sending to " & wa_no)
                If FrmBrowser.IsWAPILoggedIn Then
                    Try
                        If wa_media <> "" AndAlso System.IO.File.Exists(wa_media) Then
                            ' Ada file/gambar
                            Dim base64 As String = "data:image/jpeg;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(wa_media))
                            Dim filename As String = System.IO.Path.GetFileName(wa_media)
                            FrmBrowser.SendRegularFile(wa_no, base64, filename, wa_text, False)
                        Else
                            ' Hanya teks
                            Dim script As String = $"botmaster.sendMessageto('{wa_no}','{wa_text}',false)"
                            FrmBrowser.WebView2.ExecuteScriptAsync(script)
                        End If
                    Catch ex As Exception
                        LogMsg("Failed to send: " & ex.Message)
                    End Try
                End If
                updates.Add(id)
            End While
            conn.Close()

            If updates.Count > 0 Then
                conn.Open()
                For Each id In updates
                    Dim cmdUpd As New OdbcCommand("UPDATE outbox SET status='sent' WHERE id=" & id, conn)
                    cmdUpd.ExecuteNonQuery()
                Next
                conn.Close()
            End If
        Catch ex As Exception
        End Try
        
        If IsSyncing Then
            TimerSync.Start()
        End If
    End Sub

    Public Sub RecordInbox(wa_no As String, wa_text As String)
        If IsSyncing Then
            Try
                Dim conn As New OdbcConnection(DSN)
                conn.Open()
                Dim cmd As New OdbcCommand("INSERT INTO inbox (wa_no, wa_text, receive_time) VALUES (?, ?, ?)", conn)
                cmd.Parameters.AddWithValue("?", wa_no)
                cmd.Parameters.AddWithValue("?", wa_text)
                cmd.Parameters.AddWithValue("?", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.ExecuteNonQuery()
                conn.Close()
                
                Invoke(Sub()
                    LogMsg("Received from " & wa_no)
                End Sub)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub LogMsg(msg As String)
        ListBoxLog.Items.Add(DateTime.Now.ToString("HH:mm:ss") & " - " & msg)
        If ListBoxLog.Items.Count > 100 Then
            ListBoxLog.Items.RemoveAt(0)
        End If
        ListBoxLog.TopIndex = ListBoxLog.Items.Count - 1
    End Sub
End Class
