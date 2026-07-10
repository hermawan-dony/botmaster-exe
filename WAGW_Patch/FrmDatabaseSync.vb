Imports System.Data.Odbc

Public Class FrmDatabaseSync
    Private IsSyncing As Boolean = False
    Private DSN As String = ""

    Public Shared ActiveSyncInstance As FrmDatabaseSync

    Private Structure OutboxItem
        Dim Id As String
        Dim Mode As String
        Dim Destination As String
        Dim Message As String
        Dim Media As String
        Dim File As String
        Dim Time As String
    End Structure

    Public Shared Sub AutoStartSync(ByVal savedDSN As String)
        If ActiveSyncInstance Is Nothing Then
            ActiveSyncInstance = New FrmDatabaseSync()
            Dim forceInit As IntPtr = ActiveSyncInstance.Handle
        End If
        ActiveSyncInstance.TextBoxDSN.Text = savedDSN
        ActiveSyncInstance.StartSyncProcess()
    End Sub

    Public Sub StartSyncProcess()
        Try
            DSN = TextBoxDSN.Text
            SaveSetting(Application.ProductName, "ODBC", "SelectedDSN", DSN)
            Dim conn As New OdbcConnection(DSN)
            conn.Open()
            
            VerifyAndCreateTables(conn)
            
            conn.Close()

            TimerSync.Start()
            IsSyncing = True
            BtnOK.Text = "Stop Auto-Sync"
            LabelActivated.Visible = True
            If FrmMain.LabelWAGW IsNot Nothing Then FrmMain.LabelWAGW.Visible = True
            LogMsg("Auto-Sync started automatically.")
        Catch ex As Exception
            LogMsg("AutoStart Connection failed: " & ex.Message)
        End Try
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Me.Hide()
        Else
            MyBase.OnFormClosing(e)
        End If
    End Sub

    Private Sub FrmDatabaseSync_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ActiveSyncInstance = Me
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

            Dim savedDSN As String = GetSetting(Application.ProductName, "ODBC", "SelectedDSN", "")
            If savedDSN <> "" Then
                If Not TextBoxDSN.Items.Contains(savedDSN) Then
                    TextBoxDSN.Items.Add(savedDSN)
                End If
                TextBoxDSN.SelectedItem = savedDSN
            Else
                If TextBoxDSN.Items.Count > 0 Then
                    TextBoxDSN.SelectedIndex = 0
                Else
                    TextBoxDSN.Items.Add("DSN=BotmasterDB;")
                    TextBoxDSN.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception
            Dim savedDSN As String = GetSetting(Application.ProductName, "ODBC", "SelectedDSN", "")
            If savedDSN <> "" Then
                TextBoxDSN.Items.Clear()
                TextBoxDSN.Items.Add(savedDSN)
                TextBoxDSN.SelectedIndex = 0
            Else
                TextBoxDSN.Items.Add("DSN=BotmasterDB;")
                TextBoxDSN.SelectedIndex = 0
            End If
        End Try
    End Sub

    Private Sub VerifyAndCreateTables(conn As OdbcConnection)
        Dim outboxExists As Boolean = False
        Dim outboxValid As Boolean = False
        
        Try
            Dim cmdTest As New OdbcCommand("SELECT 1 FROM outbox WHERE 1=0", conn)
            cmdTest.ExecuteNonQuery()
            outboxExists = True
        Catch ex As Exception
        End Try
        
        If outboxExists Then
            Try
                Dim cmdTestCol As New OdbcCommand("SELECT destination_number, message_text, media_path, status FROM outbox WHERE 1=0", conn)
                cmdTestCol.ExecuteNonQuery()
                outboxValid = True
            Catch ex As Exception
            End Try
            
            If Not outboxValid Then
                Dim result As MsgBoxResult = MsgBox("Struktur kolom tabel 'outbox' tidak sesuai. Hapus dan buat ulang tabel?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "WAGW Database Sync")
                If result = MsgBoxResult.Yes Then
                    Try
                        Dim cmdDrop As New OdbcCommand("DROP TABLE outbox", conn)
                        cmdDrop.ExecuteNonQuery()
                    Catch ex As Exception
                    End Try
                    CreateOutboxTable(conn)
                End If
            End If
        Else
            CreateOutboxTable(conn)
        End If

        Dim inboxExists As Boolean = False
        Dim inboxValid As Boolean = False
        Try
            Dim cmdTest As New OdbcCommand("SELECT 1 FROM inbox WHERE 1=0", conn)
            cmdTest.ExecuteNonQuery()
            inboxExists = True
        Catch ex As Exception
        End Try
        
        If inboxExists Then
            Try
                Dim cmdTestCol As New OdbcCommand("SELECT destination_number, message_text, receive_time FROM inbox WHERE 1=0", conn)
                cmdTestCol.ExecuteNonQuery()
                inboxValid = True
            Catch ex As Exception
            End Try
            
            If Not inboxValid Then
                Dim result As MsgBoxResult = MsgBox("Struktur kolom tabel 'inbox' tidak sesuai. Hapus dan buat ulang tabel?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "WAGW Database Sync")
                If result = MsgBoxResult.Yes Then
                    Try
                        Dim cmdDrop As New OdbcCommand("DROP TABLE inbox", conn)
                        cmdDrop.ExecuteNonQuery()
                    Catch ex As Exception
                    End Try
                    CreateInboxTable(conn)
                End If
            End If
        Else
            CreateInboxTable(conn)
        End If

        ' Sent table verification
        Dim sentExists As Boolean = False
        Try
            Dim cmdTest As New OdbcCommand("SELECT 1 FROM sent WHERE 1=0", conn)
            cmdTest.ExecuteNonQuery()
            sentExists = True
        Catch ex As Exception
        End Try
        
        If Not sentExists Then
            CreateSentTable(conn)
        End If
    End Sub

    Private Sub CreateOutboxTable(conn As OdbcConnection)
        Try
            Dim cmd As New OdbcCommand("CREATE TABLE outbox (id INT AUTO_INCREMENT PRIMARY KEY, destination_number VARCHAR(50), message_text VARCHAR(2000), media_path VARCHAR(255), status VARCHAR(20) DEFAULT 'pending')", conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Try
                Dim cmdFallback As New OdbcCommand("CREATE TABLE outbox (id INT IDENTITY(1,1) PRIMARY KEY, destination_number VARCHAR(50), message_text VARCHAR(2000), media_path VARCHAR(255), status VARCHAR(20) DEFAULT 'pending')", conn)
                cmdFallback.ExecuteNonQuery()
            Catch ex2 As Exception
            End Try
        End Try
    End Sub

    Private Sub CreateInboxTable(conn As OdbcConnection)
        Try
            Dim cmd As New OdbcCommand("CREATE TABLE inbox (id INT AUTO_INCREMENT PRIMARY KEY, destination_number VARCHAR(50), message_text VARCHAR(2000), receive_time VARCHAR(50))", conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Try
                Dim cmdFallback As New OdbcCommand("CREATE TABLE inbox (id INT IDENTITY(1,1) PRIMARY KEY, destination_number VARCHAR(50), message_text VARCHAR(2000), receive_time VARCHAR(50))", conn)
                cmdFallback.ExecuteNonQuery()
            Catch ex2 As Exception
            End Try
        End Try
    End Sub

    Private Sub CreateSentTable(conn As OdbcConnection)
        Try
            Dim cmd As New OdbcCommand("CREATE TABLE sent (id INT AUTO_INCREMENT PRIMARY KEY, destination_number VARCHAR(50), message_text VARCHAR(2000), media_path VARCHAR(255), sent_time VARCHAR(50))", conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Try
                Dim cmdFallback As New OdbcCommand("CREATE TABLE sent (id INT IDENTITY(1,1) PRIMARY KEY, destination_number VARCHAR(50), message_text VARCHAR(2000), media_path VARCHAR(255), sent_time VARCHAR(50))", conn)
                cmdFallback.ExecuteNonQuery()
            Catch ex2 As Exception
            End Try
        End Try
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Hide()
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If IsSyncing Then
            TimerSync.Stop()
            IsSyncing = False
            BtnOK.Text = "Start Auto-Sync"
            LabelActivated.Visible = False
            If FrmMain.LabelWAGW IsNot Nothing Then FrmMain.LabelWAGW.Visible = False
            LogMsg("Auto-Sync stopped.")
        Else
            Try
                DSN = TextBoxDSN.Text
                SaveSetting(Application.ProductName, "ODBC", "SelectedDSN", DSN)
                Dim conn As New OdbcConnection(DSN)
                conn.Open()
                
                VerifyAndCreateTables(conn)
                
                conn.Close()

                TimerSync.Start()
                IsSyncing = True
                BtnOK.Text = "Stop Auto-Sync"
                LabelActivated.Visible = True
                If FrmMain.LabelWAGW IsNot Nothing Then FrmMain.LabelWAGW.Visible = True
                LogMsg("Auto-Sync started. Connected to DSN.")
            Catch ex As Exception
                MsgBox("Connection failed: " & ex.Message, vbCritical)
            End Try
        End If
    End Sub

    Private Sub BtnTestSQL_Click(sender As Object, e As EventArgs) Handles BtnTestSQL.Click
        Try
            Dim testConn As New OdbcConnection(TextBoxDSN.Text)
            testConn.Open()
            Dim cmd As New OdbcCommand(TextBoxSQL.Text, testConn)
            Dim rows As Integer = cmd.ExecuteNonQuery()
            testConn.Close()
            MsgBox("SQL Query executed successfully. Rows affected: " & rows, MsgBoxStyle.Information, "SQL Test Success")
            LogMsg("SQL Test executed successfully.")
        Catch ex As Exception
            MsgBox("SQL Error: " & ex.Message, MsgBoxStyle.Critical, "SQL Test Failed")
        End Try
    End Sub

    Private Sub TimerSync_Tick(sender As Object, e As EventArgs) Handles TimerSync.Tick
        TimerSync.Stop()
        System.Threading.Tasks.Task.Run(Sub()
            Try
                Dim conn As New OdbcConnection(DSN)
                conn.Open()
                Dim cmd As New OdbcCommand("SELECT id, wa_mode, wa_no, wa_text, wa_media, wa_file, wa_time FROM outbox", conn)
                Dim reader As OdbcDataReader = cmd.ExecuteReader()
                
                Dim processed As New List(Of OutboxItem)

                While reader.Read()
                    Dim item As New OutboxItem()
                    item.Id = reader("id").ToString()
                    item.Mode = reader("wa_mode").ToString()
                    item.Destination = reader("wa_no").ToString()
                    item.Message = reader("wa_text").ToString()
                    item.Media = reader("wa_media").ToString()
                    item.File = reader("wa_file").ToString()
                    item.Time = reader("wa_time").ToString()
                    
                    Invoke(Sub() LogMsg("Sending to " & item.Destination))
                    If FrmBrowser.IsWAPILoggedIn Then
                        Try
                            Dim activeMedia As String = ""
                            If Not String.IsNullOrEmpty(item.Media) AndAlso System.IO.File.Exists(item.Media) Then
                                activeMedia = item.Media
                            ElseIf Not String.IsNullOrEmpty(item.File) AndAlso System.IO.File.Exists(item.File) Then
                                activeMedia = item.File
                            End If

                            If activeMedia <> "" Then
                                ' File / media message
                                Dim base64 As String = "data:image/jpeg;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(activeMedia))
                                Dim filename As String = System.IO.Path.GetFileName(activeMedia)
                                Invoke(Sub() FrmBrowser.SendRegularFile(item.Destination, base64, filename, item.Message, False))
                            Else
                                ' Text message
                                Invoke(Sub()
                                    Dim script As String = $"botmaster.sendMessageto('{item.Destination}','{item.Message}',false)"
                                    FrmBrowser.WebView2.ExecuteScriptAsync(script)
                                End Sub)
                            End If
                        Catch ex As Exception
                            Invoke(Sub() LogMsg("Failed to send: " & ex.Message))
                        End Try
                    End If
                    processed.Add(item)
                End While
                conn.Close()

                If processed.Count > 0 Then
                    conn.Open()
                    For Each item In processed
                        ' Log into sent table
                        Try
                            Dim cmdSent As New OdbcCommand("INSERT INTO sent (id, wa_mode, wa_no, wa_text, wa_media, wa_file, wa_time, status) VALUES (?, ?, ?, ?, ?, ?, ?, ?)", conn)
                            Dim parsedId As Integer = 0
                            Integer.TryParse(item.Id, parsedId)
                            Dim parsedMode As Integer = 0
                            Integer.TryParse(item.Mode, parsedMode)
                            
                            cmdSent.Parameters.AddWithValue("?", parsedId)
                            cmdSent.Parameters.AddWithValue("?", parsedMode)
                            cmdSent.Parameters.AddWithValue("?", item.Destination)
                            cmdSent.Parameters.AddWithValue("?", item.Message)
                            cmdSent.Parameters.AddWithValue("?", item.Media)
                            cmdSent.Parameters.AddWithValue("?", item.File)
                            
                            Dim sentTimeStr As String = item.Time
                            If String.IsNullOrEmpty(sentTimeStr) Then
                                sentTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                            End If
                            cmdSent.Parameters.AddWithValue("?", sentTimeStr)
                            cmdSent.Parameters.AddWithValue("?", "sent")
                            cmdSent.ExecuteNonQuery()
                        Catch ex As Exception
                        End Try

                        ' Delete from outbox
                        Dim deleted As Boolean = False
                        If Not String.IsNullOrEmpty(item.Id) AndAlso item.Id <> "0" Then
                            Try
                                Dim cmdDel As New OdbcCommand("DELETE FROM outbox WHERE id=" & item.Id, conn)
                                cmdDel.ExecuteNonQuery()
                                deleted = True
                            Catch ex As Exception
                            End Try
                        End If
                        
                        ' Fallback to delete using text/destination if ID fails or is not populated
                        If Not deleted Then
                            Try
                                Dim cmdDelFallback As New OdbcCommand("DELETE FROM outbox WHERE wa_no=? AND wa_text=?", conn)
                                cmdDelFallback.Parameters.AddWithValue("?", item.Destination)
                                cmdDelFallback.Parameters.AddWithValue("?", item.Message)
                                cmdDelFallback.ExecuteNonQuery()
                            Catch ex As Exception
                            End Try
                        End If
                    Next
                    conn.Close()
                End If
            Catch ex As Exception
            End Try
            
            Invoke(Sub()
                If IsSyncing Then
                    TimerSync.Start()
                End If
            End Sub)
        End Sub)
    End Sub

    Public Sub RecordInbox(destination_number As String, message_text As String)
        If IsSyncing Then
            Try
                Dim conn As New OdbcConnection(DSN)
                conn.Open()
                Dim cmd As New OdbcCommand("INSERT INTO inbox (wa_no, wa_text, wa_time, status) VALUES (?, ?, ?, ?)", conn)
                cmd.Parameters.AddWithValue("?", destination_number)
                cmd.Parameters.AddWithValue("?", message_text)
                cmd.Parameters.AddWithValue("?", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.Parameters.AddWithValue("?", "received")
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
