Imports System.Data.Odbc

Public Class FrmDatabaseSync
    Private IsSyncing As Boolean = False
    Private DSN As String = ""

    Private Sub FrmDatabaseSync_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GetAppIcon()
        Catch ex As Exception
        End Try
        Text = "Database Auto-Sync"
        LoadODBCSources()
        TimerSync.Interval = 5000
    End Sub

    Private Sub LoadODBCSources()
        TextBoxDSN.Items.Clear()
        Try
            Dim userDSNKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\ODBC\ODBC.INI\ODBC Data Sources")
            If userDSNKey IsNot Nothing Then
                For Each valName As String In userDSNKey.GetValueNames()
                    TextBoxDSN.Items.Add("DSN=" & valName & ";")
                Next
            End If
            
            Dim sysDSNKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\ODBC\ODBC.INI\ODBC Data Sources")
            If sysDSNKey IsNot Nothing Then
                For Each valName As String In sysDSNKey.GetValueNames()
                    Dim dsnStr As String = "DSN=" & valName & ";"
                    If Not TextBoxDSN.Items.Contains(dsnStr) Then
                        TextBoxDSN.Items.Add(dsnStr)
                    End If
                Next
            End If
            
            Dim sysDSNKey32 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\ODBC\ODBC.INI\ODBC Data Sources")
            If sysDSNKey32 IsNot Nothing Then
                For Each valName As String In sysDSNKey32.GetValueNames()
                    Dim dsnStr As String = "DSN=" & valName & ";"
                    If Not TextBoxDSN.Items.Contains(dsnStr) Then
                        TextBoxDSN.Items.Add(dsnStr)
                    End If
                Next
            End If

            If TextBoxDSN.Items.Count > 0 Then
                TextBoxDSN.SelectedIndex = 0
            Else
                TextBoxDSN.Items.Add("DSN=BotmasterDB;")
                TextBoxDSN.SelectedIndex = 0
            End If
        Catch ex As Exception
            TextBoxDSN.Items.Add("DSN=BotmasterDB;")
            TextBoxDSN.SelectedIndex = 0
        End Try
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
                    Dim cmd As New OdbcCommand("CREATE TABLE outbox (id INT, destination_number VARCHAR(50), message_text VARCHAR(2000), media_path VARCHAR(255), status VARCHAR(20) DEFAULT 'pending')", conn)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                End Try
                Try
                    Dim cmd As New OdbcCommand("CREATE TABLE inbox (id INT, destination_number VARCHAR(50), message_text VARCHAR(2000), receive_time VARCHAR(50))", conn)
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
            Dim cmd As New OdbcCommand("SELECT id, destination_number, message_text, media_path FROM outbox WHERE status='pending' OR status IS NULL OR status=''", conn)
            Dim reader As OdbcDataReader = cmd.ExecuteReader()
            
            Dim updates As New List(Of String)

            While reader.Read()
                Dim id As String = reader("id").ToString()
                Dim destination_number As String = reader("destination_number").ToString()
                Dim message_text As String = reader("message_text").ToString()
                Dim media_path As String = reader("media_path").ToString()
                
                LogMsg("Sending to " & destination_number)
                If FrmBrowser.IsWAPILoggedIn Then
                    Try
                        If media_path <> "" AndAlso System.IO.File.Exists(media_path) Then
                            ' Ada file/gambar
                            Dim base64 As String = "data:image/jpeg;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(media_path))
                            Dim filename As String = System.IO.Path.GetFileName(media_path)
                            FrmBrowser.SendRegularFile(destination_number, base64, filename, message_text, False)
                        Else
                            ' Hanya teks
                            Dim script As String = $"botmaster.sendMessageto('{destination_number}','{message_text}',false)"
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

    Public Sub RecordInbox(destination_number As String, message_text As String)
        If IsSyncing Then
            Try
                Dim conn As New OdbcConnection(DSN)
                conn.Open()
                Dim cmd As New OdbcCommand("INSERT INTO inbox (destination_number, message_text, receive_time) VALUES (?, ?, ?)", conn)
                cmd.Parameters.AddWithValue("?", destination_number)
                cmd.Parameters.AddWithValue("?", message_text)
                cmd.Parameters.AddWithValue("?", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.ExecuteNonQuery()
                conn.Close()
                
                Invoke(Sub()
                    LogMsg("Received from " & destination_number)
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
