Public Class FrmGroupGrabber
    ' ✅ Make this Shared so it's accessible even when form is closing
    Public Shared ForceStopFetching As Boolean = False

    Private Async Sub BtnFetchGroups_Click(sender As Object, e As EventArgs) Handles BtnFetchGroups.Click
        Dim Groups = Await FrmBrowser.GetAllGroups()

        ListView1.Items.Clear()

        Dim Groupli As ListViewItem
        If Not IsNothing(Groups) Then
            For Each Group In Groups
                Try
                    Groupli = New ListViewItem
                    Groupli.Tag = Group.id
                    Groupli.Text = Group.name
                    Groupli.SubItems.Add(Group.id)
                    ListView1.Items.Add(Groupli)
                Catch ex As Exception
                    Debug.WriteLine($"Error adding group: {ex.Message}")
                End Try
            Next
        End If
        GetCount()
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        ListView2.Items.Clear()
        GetCount()
    End Sub

    Private Sub BtnSelectAll_Click(sender As Object, e As EventArgs)
        For Each li As ListViewItem In ListView1.Items
            li.Checked = True
        Next
    End Sub

    Private Sub BtnDeselectAll_Click(sender As Object, e As EventArgs)
        For Each li As ListViewItem In ListView1.Items
            li.Checked = False
        Next
    End Sub

    Private Sub BtnClearGroups_Click(sender As Object, e As EventArgs) Handles BtnClearGroups.Click
        ListView1.Items.Clear()
        GetCount()
    End Sub

    Private Sub BtnDeleteGroups_Click(sender As Object, e As EventArgs) Handles BtnDeleteGroups.Click
        For Each li As ListViewItem In ListView1.Items
            If li.Checked Then
                li.Remove()
            End If
        Next
        GetCount()
    End Sub

    Private Sub BtnExportGroups_MouseDown(sender As Object, e As MouseEventArgs) Handles BtnExportGroups.MouseDown
        ContextMenuStrip1.Show(sender, e.Location)
    End Sub

    Private Sub AllGroupsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllGroupsToolStripMenuItem.Click
        ExportGroups(0)
    End Sub

    Public Sub ExportGroups(ByVal ExportType As Integer)
        Dim dlg As New SaveFileDialog With {.Filter = "Text Files|*.txt"}

        If dlg.ShowDialog = DialogResult.OK Then
            Dim result As String = ""

            For Each li As ListViewItem In ListView1.Items
                If ExportType = 0 Then
                    result &= li.Text & "," & li.Tag & vbNewLine
                Else
                    If li.Checked Then
                        result &= li.Text & "," & li.Tag & vbNewLine
                    End If
                End If
            Next

            Try
                IO.File.WriteAllText(dlg.FileName, result)
            Catch ex As Exception
                MessageBox.Show($"Error saving file: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub CheckedGroupsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckedGroupsToolStripMenuItem.Click
        ExportGroups(1)
    End Sub

    Private Sub FrmGroupGrabber_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListView1.Items.Clear()
        GetCount()
        ForceStopFetching = False ' Reset flag when form loads
    End Sub

    Private Sub GetCount()
        ToolStripStatusLabel1.Text = "Groups Count: " & ListView1.Items.Count
        ToolStripStatusLabel2.Text = "Contacts Count: " & ListView2.Items.Count
    End Sub

    Private Sub BtnAddtoList_Click(sender As Object, e As EventArgs) Handles BtnAddtoList.Click
        For Each t As ListViewItem In ListView2.Items
            Dim li As New ListViewItem With {
                .Tag = t.Tag.ToString.Replace("+", "") & "@c.us",
                .Text = "N/A"
            }
            li.SubItems.Add(t.Tag.ToString.Replace("+", ""))
            li.SubItems.Add("")
            li.SubItems.Add("")
            li.SubItems.Add("")
            li.SubItems.Add("")
            li.SubItems.Add("")
            FrmMain.LstNumbers.Items.Add(li)
        Next
        FrmMain.Statistics()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        ' Event handler stub
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Reset the flag before starting
        ForceStopFetching = False

        ' ✅ Capture checked items on UI thread BEFORE starting background task
        Dim checkedGroups As New List(Of String)
        For Each item As ListViewItem In ListView1.CheckedItems
            checkedGroups.Add(item.Tag.ToString())
            If ForceStopFetching Then
                Debug.WriteLine("⛔ Fetching stopped by user in Button1 loop")
                Button1.Enabled = True
                Button2.Enabled = False
                Exit For
            End If
        Next

        ' Check if any groups are selected
        If checkedGroups.Count = 0 Then
            MessageBox.Show("Please select at least one group.", "No Groups Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Disable fetch button, enable stop button
        Button1.Enabled = False
        Button2.Enabled = True

        Debug.WriteLine($"🚀 Starting fetch for {checkedGroups.Count} groups")

        ' Process groups sequentially (not in background task)
        ' This is necessary because responses come asynchronously via WebMessage
        For Each groupTag As String In checkedGroups
            ' Check the flag before processing each group
            If ForceStopFetching Then
                Debug.WriteLine("⛔ Fetching stopped by user in Button1 loop")
                Button1.Enabled = True
                Button2.Enabled = False
                Exit For
            End If

            Debug.WriteLine($"📤 Requesting participants for group: {groupTag}")
            ' Request group participants (response will come via WebMessage)
            FrmBrowser.getGroupParticipantIDs(groupTag)

            ' Wait a bit for the request to be sent
            Application.DoEvents()
            System.Threading.Thread.Sleep(1000) ' 1 second between group requests
        Next

        ' Note: Buttons will be re-enabled by HandleGroupContactsFetch when all contacts are fetched
        ' Or immediately if stopped
        If ForceStopFetching Then
            Button1.Enabled = True
            Button2.Enabled = False
            Debug.WriteLine("✋ Button1 loop ended due to ForceStop")
        Else
            Debug.WriteLine("✅ All group participant requests sent")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Debug.WriteLine("🛑 ============ Button2 (Stop) clicked ============")
        Debug.WriteLine($"📍 ForceStopFetching BEFORE: {ForceStopFetching}")
        ForceStopFetching = True
        Task.Run(Async Function()
                     Await Task.Delay(3000) ' Wait 3 seconds
                     ForceStopFetching = False
                     Debug.WriteLine("🔄 ForceStopFetching reset to False after delay")
                 End Function)

        Debug.WriteLine($"📍 ForceStopFetching AFTER: {ForceStopFetching}")
        Debug.WriteLine("⛔ Force stop requested - FLAG SET TO TRUE")
        Debug.WriteLine("🛑 ============================================")
    End Sub

    Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click
        Dim dlg As New SaveFileDialog With {
            .Filter = "Text Files|*.txt|CSV Files|*.csv",
            .DefaultExt = "txt"
        }

        If dlg.ShowDialog = DialogResult.OK Then
            Dim result As String = "Number,Lid" & vbNewLine

            For Each li As ListViewItem In ListView2.Items
                result &= li.Text & "," & li.SubItems(1).Text & vbNewLine
            Next

            Try
                IO.File.WriteAllText(dlg.FileName, result)
                MessageBox.Show("Export completed successfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"Error saving file: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Handles form closing event - sets flag to stop any ongoing operations
    ''' </summary>
    Private Sub FrmGroupGrabber_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Set the flag to stop fetching when form is closing
        Debug.WriteLine("🚪 ============ Form closing - Setting ForceStop ============")
        Debug.WriteLine($"📍 ForceStopFetching BEFORE: {ForceStopFetching}")

        ForceStopFetching = True

        Debug.WriteLine($"📍 ForceStopFetching AFTER: {ForceStopFetching}")
        Debug.WriteLine("⛔ Form closing - ForceStopFetching set to True")

        ' Give a moment for the flag to be checked
        Application.DoEvents()
        System.Threading.Thread.Sleep(100)

        Debug.WriteLine("🚪 ============================================")

        ' Optional: You can cancel the close and just hide if needed
        ' e.Cancel = True
        ' Me.Hide()
    End Sub

    ''' <summary>
    ''' Handles form closed event - cleanup
    ''' </summary>
    Private Sub FrmGroupGrabber_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        ForceStopFetching = True
        Debug.WriteLine("🚪 Form closed - ForceStopFetching set to True (FormClosed event)")
    End Sub

    Private Sub FrmGroupGrabber_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        ' Event stub
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        For Each li As ListViewItem In ListView1.Items
            li.Checked = True
        Next
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        For Each li As ListViewItem In ListView1.Items
            li.Checked = False
        Next

    End Sub
End Class

Public Class GroupParticipant
    Public Property _serialized As String
    Public Property server As String
    Public Property user As String
End Class