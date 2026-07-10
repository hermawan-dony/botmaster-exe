Imports System.Data.Odbc

Public Class FrmDatabaseSync
    Public IsSyncing As Boolean = False
    Public DSN As String = ""

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

    Private Function GetDSNName(dsnConnStr As String) As String
        Try
            If String.IsNullOrEmpty(dsnConnStr) Then Return ""
            Dim parts = dsnConnStr.Split(";"c)
            For Each part In parts
                If part.Trim().ToUpper().StartsWith("DSN=") Then
                    Return part.Trim().Substring(4)
                End If
            Next
        Catch
        End Try
        Return "ODBC"
    End Function

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
            Try
                Dim countCmd As New OdbcCommand("SELECT COUNT(*) FROM outbox", conn)
                Dim initialCount As Integer = Convert.ToInt32(countCmd.ExecuteScalar())
                UpdateWAGWLabel(initialCount)
            Catch
                UpdateWAGWLabel(0)
            End Try
            RefreshGrids()
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
        Dim accountNum As String = "628512345678"
        Try
            If FrmMain.Label3 IsNot Nothing AndAlso FrmMain.Label3.Text.StartsWith("Account:") Then
                Dim num As String = FrmMain.Label3.Text.Substring(8).Trim()
                If num <> "N/A" AndAlso num <> "" Then
                    accountNum = num
                End If
            End If
        Catch
        End Try
        TextBoxSQL.Text = $"INSERT INTO outbox (wa_no, wa_text) VALUES ('{accountNum}', 'Test WAGW message')"
        
        LoadODBCSources()
        TimerSync.Interval = 5000
        Me.Text = "++WAGW"
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
                Dim cmdTestCol As New OdbcCommand("SELECT wa_no, wa_text, wa_media FROM outbox WHERE 1=0", conn)
                Using reader = cmdTestCol.ExecuteReader()
                End Using
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
                Dim cmdTestCol As New OdbcCommand("SELECT wa_no, wa_text, wa_time FROM inbox WHERE 1=0", conn)
                Using reader = cmdTestCol.ExecuteReader()
                End Using
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

        Dim sentExists As Boolean = False
        Dim sentValid As Boolean = False
        Try
            Dim cmdTest As New OdbcCommand("SELECT 1 FROM sent WHERE 1=0", conn)
            cmdTest.ExecuteNonQuery()
            sentExists = True
        Catch ex As Exception
        End Try
        
        If sentExists Then
            Try
                Dim cmdTestCol As New OdbcCommand("SELECT wa_no, wa_text, wa_media FROM sent WHERE 1=0", conn)
                Using reader = cmdTestCol.ExecuteReader()
                End Using
                sentValid = True
            Catch ex As Exception
            End Try
            
            If Not sentValid Then
                Dim result As MsgBoxResult = MsgBox("Struktur kolom tabel 'sent' tidak sesuai. Hapus dan buat ulang tabel?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "WAGW Database Sync")
                If result = MsgBoxResult.Yes Then
                    Try
                        Dim cmdDrop As New OdbcCommand("DROP TABLE sent", conn)
                        cmdDrop.ExecuteNonQuery()
                    Catch ex As Exception
                    End Try
                    CreateSentTable(conn)
                End If
            End If
        Else
            CreateSentTable(conn)
        End If
    End Sub

    Private Sub CreateOutboxTable(conn As OdbcConnection)
        Try
            Dim cmd As New OdbcCommand("CREATE TABLE outbox (id INTEGER PRIMARY KEY AUTOINCREMENT, wa_mode INTEGER, wa_no TEXT NOT NULL, wa_text TEXT NOT NULL, wa_media TEXT, wa_file TEXT, wa_time DATETIME DEFAULT CURRENT_TIMESTAMP)", conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Try
                Dim cmdFallback As New OdbcCommand("CREATE TABLE outbox (id INT AUTO_INCREMENT PRIMARY KEY, wa_mode INT, wa_no VARCHAR(50) NOT NULL, wa_text VARCHAR(2000) NOT NULL, wa_media VARCHAR(255), wa_file VARCHAR(255), wa_time VARCHAR(50))", conn)
                cmdFallback.ExecuteNonQuery()
            Catch ex2 As Exception
                Try
                    Dim cmdFallback2 As New OdbcCommand("CREATE TABLE outbox (id INT IDENTITY(1,1) PRIMARY KEY, wa_mode INT, wa_no VARCHAR(50) NOT NULL, wa_text VARCHAR(2000) NOT NULL, wa_media VARCHAR(255), wa_file VARCHAR(255), wa_time VARCHAR(50))", conn)
                    cmdFallback2.ExecuteNonQuery()
                Catch ex3 As Exception
                End Try
            End Try
        End Try
    End Sub

    Private Sub CreateInboxTable(conn As OdbcConnection)
        Try
            Dim cmd As New OdbcCommand("CREATE TABLE inbox (id INTEGER PRIMARY KEY AUTOINCREMENT, wa_no TEXT NOT NULL, sub_no TEXT, wa_text TEXT NOT NULL, wa_time DATETIME NOT NULL, status TEXT)", conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Try
                Dim cmdFallback As New OdbcCommand("CREATE TABLE inbox (id INT AUTO_INCREMENT PRIMARY KEY, wa_no VARCHAR(50) NOT NULL, sub_no VARCHAR(50), wa_text VARCHAR(2000) NOT NULL, wa_time VARCHAR(50), status VARCHAR(20))", conn)
                cmdFallback.ExecuteNonQuery()
            Catch ex2 As Exception
                Try
                    Dim cmdFallback2 As New OdbcCommand("CREATE TABLE inbox (id INT IDENTITY(1,1) PRIMARY KEY, wa_no VARCHAR(50) NOT NULL, sub_no VARCHAR(50), wa_text VARCHAR(2000) NOT NULL, wa_time VARCHAR(50), status VARCHAR(20))", conn)
                    cmdFallback2.ExecuteNonQuery()
                Catch ex3 As Exception
                End Try
            End Try
        End Try
    End Sub

    Private Sub CreateSentTable(conn As OdbcConnection)
        Try
            Dim cmd As New OdbcCommand("CREATE TABLE sent (id INTEGER, wa_mode INTEGER, wa_no TEXT NOT NULL, wa_text TEXT NOT NULL, wa_media TEXT, wa_file TEXT, wa_time DATETIME DEFAULT CURRENT_TIMESTAMP, status TEXT, PRIMARY KEY (id, wa_time))", conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Try
                Dim cmdFallback As New OdbcCommand("CREATE TABLE sent (id INT, wa_mode INT, wa_no VARCHAR(50) NOT NULL, wa_text VARCHAR(2000) NOT NULL, wa_media VARCHAR(255), wa_file VARCHAR(255), wa_time VARCHAR(50), status VARCHAR(20), PRIMARY KEY (id, wa_time))", conn)
                cmdFallback.ExecuteNonQuery()
            Catch ex2 As Exception
                Try
                    Dim cmdFallback3 As New OdbcCommand("CREATE TABLE sent (id INT, wa_mode INT, wa_no VARCHAR(50) NOT NULL, wa_text VARCHAR(2000) NOT NULL, wa_media VARCHAR(255), wa_file VARCHAR(255), wa_time VARCHAR(50), status VARCHAR(20))", conn)
                    cmdFallback3.ExecuteNonQuery()
                Catch ex3 As Exception
                End Try
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
                Try
                    Dim countCmd As New OdbcCommand("SELECT COUNT(*) FROM outbox", conn)
                    Dim initialCount As Integer = Convert.ToInt32(countCmd.ExecuteScalar())
                    UpdateWAGWLabel(initialCount)
                Catch
                    UpdateWAGWLabel(0)
                End Try
                RefreshGrids()
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
            Dim currentCount As Integer = 0
            Try
                Dim countCmd As New OdbcCommand("SELECT COUNT(*) FROM outbox", testConn)
                currentCount = Convert.ToInt32(countCmd.ExecuteScalar())
            Catch
            End Try
            UpdateWAGWLabel(currentCount)

            testConn.Close()
            MsgBox("SQL Query executed successfully. Rows affected: " & rows, MsgBoxStyle.Information, "SQL Test Success")
            RefreshGrids()
            LogMsg("SQL Test executed successfully.")
        Catch ex As Exception
            MsgBox("SQL Error: " & ex.Message, MsgBoxStyle.Critical, "SQL Test Failed")
        End Try
    End Sub

    Private Sub TimerSync_Tick(sender As Object, e As EventArgs) Handles TimerSync.Tick
        Dim isBrowserLoggedIn As Boolean = False
        Try
            isBrowserLoggedIn = FrmBrowser.IsWAPILoggedIn()
        Catch
        End Try

        TimerSync.Stop()
        System.Threading.Tasks.Task.Run(Sub()
            Try
                Dim conn As New OdbcConnection(DSN)
                conn.Open()
                
                Dim currentCount As Integer = 0
                Try
                    Dim countCmd As New OdbcCommand("SELECT COUNT(*) FROM outbox", conn)
                    currentCount = Convert.ToInt32(countCmd.ExecuteScalar())
                Catch
                End Try
                UpdateWAGWLabel(currentCount)

                Dim cmd As New OdbcCommand("SELECT id, wa_mode, wa_no, wa_text, wa_media, wa_file, wa_time FROM outbox", conn)
                Dim reader As OdbcDataReader = cmd.ExecuteReader()
                
                Dim processed As New List(Of OutboxItem)

                While reader.Read()
                    Dim item As New OutboxItem()
                    item.Id = If(IsDBNull(reader("id")), "", reader("id").ToString())
                    item.Mode = If(IsDBNull(reader("wa_mode")), "", reader("wa_mode").ToString())
                    item.Destination = If(IsDBNull(reader("wa_no")), "", reader("wa_no").ToString())
                    item.Message = If(IsDBNull(reader("wa_text")), "", reader("wa_text").ToString())
                    item.Media = If(IsDBNull(reader("wa_media")), "", reader("wa_media").ToString())
                    item.File = If(IsDBNull(reader("wa_file")), "", reader("wa_file").ToString())
                    item.Time = If(IsDBNull(reader("wa_time")), "", reader("wa_time").ToString())
                    
                    Dim sentSuccessfully As Boolean = False
                    
                    If isBrowserLoggedIn Then
                        WasLoginWarningLogged = False
                        If ActiveSyncInstance IsNot Nothing AndAlso ActiveSyncInstance.IsHandleCreated Then
                            ActiveSyncInstance.BeginInvoke(Sub() ActiveSyncInstance.LogMsg("Sending to " & item.Destination))
                        End If
                        Try
                            Dim formattedNumber As String = item.Destination
                            If Not formattedNumber.Contains("@") Then
                                formattedNumber = formattedNumber & "@c.us"
                            End If

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
                                If ActiveSyncInstance IsNot Nothing AndAlso ActiveSyncInstance.IsHandleCreated Then
                                    ActiveSyncInstance.BeginInvoke(Sub() FrmBrowser.SendRegularFile(formattedNumber, base64, filename, item.Message, False))
                                End If
                                sentSuccessfully = True
                            Else
                                ' Text message
                                If ActiveSyncInstance IsNot Nothing AndAlso ActiveSyncInstance.IsHandleCreated Then
                                    ActiveSyncInstance.BeginInvoke(Sub()
                                        Dim script As String = $"botmaster.sendMessageto('{formattedNumber}','{SafeJavaScript(item.Message)}',false)"
                                        FrmBrowser.WebView2.ExecuteScriptAsync(script)
                                    End Sub)
                                End If
                                sentSuccessfully = True
                            End If
                        Catch ex As Exception
                            If ActiveSyncInstance IsNot Nothing AndAlso ActiveSyncInstance.IsHandleCreated Then
                                ActiveSyncInstance.BeginInvoke(Sub() ActiveSyncInstance.LogMsg("Failed to send: " & ex.Message))
                            End If
                        End Try
                    Else
                        If Not WasLoginWarningLogged Then
                            If ActiveSyncInstance IsNot Nothing AndAlso ActiveSyncInstance.IsHandleCreated Then
                                ActiveSyncInstance.BeginInvoke(Sub() ActiveSyncInstance.LogMsg("WA Web is not connected/ready. Please click 'Open WhatsApp' and log in."))
                            End If
                            WasLoginWarningLogged = True
                        End If
                    End If
                    
                    If sentSuccessfully Then
                        processed.Add(item)
                    End If
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
                If ActiveSyncInstance IsNot Nothing AndAlso ActiveSyncInstance.IsHandleCreated Then
                    ActiveSyncInstance.BeginInvoke(Sub() ActiveSyncInstance.LogMsg("Sync error: " & ex.Message))
                End If
            End Try
            
            If ActiveSyncInstance IsNot Nothing AndAlso Not ActiveSyncInstance.IsDisposed Then
                If ActiveSyncInstance.IsHandleCreated Then
                    ActiveSyncInstance.BeginInvoke(Sub()
                        If ActiveSyncInstance.IsSyncing Then
                            If ActiveSyncInstance.Visible Then
                                ActiveSyncInstance.RefreshGrids()
                            End If
                            ActiveSyncInstance.TimerSync.Start()
                        End If
                    End Sub)
                Else
                    Try
                        If ActiveSyncInstance.IsSyncing Then
                            ActiveSyncInstance.TimerSync.Start()
                        End If
                    Catch
                    End Try
                End If
            End If
        End Sub)
    End Sub

    Public Shared Sub RecordInbox(destination_number As String, message_text As String)
        If ActiveSyncInstance IsNot Nothing AndAlso ActiveSyncInstance.IsSyncing Then
            Try
                Dim conn As New OdbcConnection(ActiveSyncInstance.DSN)
                conn.Open()
                Dim cmd As New OdbcCommand("INSERT INTO inbox (wa_no, wa_text, wa_time, status) VALUES (?, ?, ?, ?)", conn)
                cmd.Parameters.AddWithValue("?", destination_number)
                cmd.Parameters.AddWithValue("?", message_text)
                cmd.Parameters.AddWithValue("?", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.Parameters.AddWithValue("?", "received")
                cmd.ExecuteNonQuery()
                conn.Close()
                
                If ActiveSyncInstance.IsHandleCreated Then
                    ActiveSyncInstance.BeginInvoke(Sub()
                        ActiveSyncInstance.LogMsg("Received from " & destination_number)
                    End Sub)
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Shared WasLoginWarningLogged As Boolean = False

    Public Sub RefreshGrids()
        If Not IsSyncing Then Return
        
        System.Threading.Tasks.Task.Run(Sub()
            Try
                Dim conn As New OdbcConnection(DSN)
                conn.Open()
                
                Dim dtOutbox As New DataTable()
                Using cmd As New OdbcCommand("SELECT * FROM outbox", conn)
                    Using adapter As New OdbcDataAdapter(cmd)
                        adapter.Fill(dtOutbox)
                    End Using
                End Using
                
                Dim dtSent As New DataTable()
                Using cmd As New OdbcCommand("SELECT * FROM sent", conn)
                    Using adapter As New OdbcDataAdapter(cmd)
                        adapter.Fill(dtSent)
                    End Using
                End Using
                
                Dim dtInbox As New DataTable()
                Using cmd As New OdbcCommand("SELECT * FROM inbox", conn)
                    Using adapter As New OdbcDataAdapter(cmd)
                        adapter.Fill(dtInbox)
                    End Using
                End Using
                
                conn.Close()
                
                If Me.IsHandleCreated AndAlso Not Me.IsDisposed Then
                    Me.BeginInvoke(Sub()
                        BindGridSafely(dgvOutbox, dtOutbox, "id DESC")
                        BindGridSafely(dgvSent, dtSent, "id DESC")
                        BindGridSafely(dgvInbox, dtInbox, "id DESC")
                    End Sub)
                End If
            Catch ex As Exception
            End Try
        End Sub)
    End Sub

    Private Sub BindGridSafely(dgv As DataGridView, dt As DataTable, sortExpr As String)
        Try
            If dt.Rows.Count = 0 Then
                dgv.DataSource = dt
                Return
            End If
            Dim dv As New DataView(dt)
            If dt.Columns.Contains(sortExpr.Split(" "c)(0)) Then
                dv.Sort = sortExpr
            End If
            Dim sortedDt As DataTable = dv.ToTable()
            If sortedDt.Rows.Count > 10 Then
                Dim newDt As DataTable = sortedDt.Clone()
                For i As Integer = 0 To 9
                    newDt.ImportRow(sortedDt.Rows(i))
                Next
                dgv.DataSource = newDt
            Else
                dgv.DataSource = sortedDt
            End If
        Catch
            dgv.DataSource = dt
        End Try
    End Sub

    Public Shared Sub UpdateWAGWLabel(outboxCount As Integer)
        If FrmMain.LabelWAGW IsNot Nothing Then
            Dim dsnName As String = ""
            If ActiveSyncInstance IsNot Nothing Then
                dsnName = ActiveSyncInstance.GetDSNName(ActiveSyncInstance.DSN)
            End If
            Dim textToShow As String = $"  ++WAGW Active [ {dsnName} ] ({outboxCount})  "
            Dim updateUI As Action = Sub()
                FrmMain.LabelWAGW.Text = textToShow
                FrmMain.LabelWAGW.Visible = True
                If FrmMain.LabelWAGW.Parent IsNot Nothing Then
                    FrmMain.LabelWAGW.Left = (FrmMain.LabelWAGW.Parent.Width - FrmMain.LabelWAGW.Width) \ 2
                End If
            End Sub
            
            If FrmMain.LabelWAGW.InvokeRequired Then
                FrmMain.LabelWAGW.BeginInvoke(updateUI)
            Else
                updateUI()
            End If
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
