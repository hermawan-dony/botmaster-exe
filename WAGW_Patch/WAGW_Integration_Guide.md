# Panduan Integrasi Ulang WAGW (ODBC Auto-Sync)

Jika Anda mendapatkan *update* Botmaster versi terbaru (polos) dan ingin memasukkan kembali fitur WAGW ini, ikuti 4 langkah mudah berikut:

## 1. Salin File Form
Salin ketiga file dari folder `WAGW_Patch` ini:
- `FrmDatabaseSync.vb`
- `FrmDatabaseSync.Designer.vb`
- `FrmDatabaseSync.resx`

Tempelkan ke dalam folder proyek Botmaster Anda di:
`BOTMASTER26_SC\Botmaster\Forms\`

## 2. Daftarkan Form ke Project (Botmaster.vbproj)
Buka file `BOTMASTER26_SC\Botmaster\Botmaster.vbproj` menggunakan teks editor (misal Notepad). 
Cari bagian `<ItemGroup>` yang berisi `<Compile Include="Forms\...`. 
Tambahkan baris berikut di sana:
```xml
    <Compile Include="Forms\FrmDatabaseSync.Designer.vb">
      <DependentUpon>FrmDatabaseSync.vb</DependentUpon>
    </Compile>
    <Compile Include="Forms\FrmDatabaseSync.vb">
      <SubType>Form</SubType>
    </Compile>
```
Lalu cari bagian `<EmbeddedResource` dan tambahkan:
```xml
    <EmbeddedResource Include="Forms\FrmDatabaseSync.resx">
      <DependentUpon>FrmDatabaseSync.vb</DependentUpon>
    </EmbeddedResource>
```

## 3. Tambahkan Logika Menu Dinamis di FrmMain_Load
Buka `FrmMain.vb` lalu cari fungsi `FrmMain_Load`. Tambahkan kode berikut untuk mengaktifkan WAGW secara dinamis hanya jika lisensi mengandung "wasender":
```vb
        Dim licKey As String = GetSetting(Application.ProductName, "license", "key", "")
        If licKey.ToLower().Contains("wasender") Then
            Me.Text = "Botmaster ++WAGW"
            Me.MaximizeBox = True
            Me.FormBorderStyle = FormBorderStyle.Sizable
            
            Try
                Dim runKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
                If GetSetting(Application.ProductName, "AutoRun", "Enabled", "0") = "1" Then
                    runKey.SetValue(Application.ProductName, Application.ExecutablePath)
                Else
                    runKey.DeleteValue(Application.ProductName, False)
                End If
            Catch ex As Exception
            End Try
            
            Dim wagwMenuItem As New ToolStripMenuItem("Database Auto-Sync (ODBC)")
            AddHandler wagwMenuItem.Click, Sub(senderObj, eArgs)
                                               Dim frm As New FrmDatabaseSync()
                                               frm.ShowDialog()
                                           End Sub

            Dim wagwHelpMenuItem As New ToolStripMenuItem("WAGW Integration Help")
            AddHandler wagwHelpMenuItem.Click, Sub(senderObj, eArgs)
                                                   MsgBox("--- PANDUAN PENGGUNAAN DATABASE AUTO-SYNC WAGW ---", MsgBoxStyle.Information, "Botmaster-WAGW Help Guide")
                                               End Sub

            ToolsToolStripMenuItem.DropDownItems.Add(wagwMenuItem)
            HelpToolStripMenuItem.DropDownItems.Add(wagwHelpMenuItem)
        End If
```
*(Jangan lupa juga suntikkan `chkAutoRun` CheckBox dinamis ke dalam `FrmAdvanced_Load` untuk memunculkan pengaturan UI AutoRun)*

## 4. Hook Pesan Masuk (Inbox)
Buka file `BOTMASTER26_SC\Botmaster\Forms\FrmBrowser.vb`, cari *Sub* bernama `ReceivedMessage(ByVal json As String)`.
Di dalam fungsi tersebut, di bawah kode yang mem- *parsing* nomor pengirim (`Dim remoteNumber As String = ...`), sisipkan baris berikut:
```vb
            ' Hook for Database Sync Inbox
            Try
                FrmDatabaseSync.RecordInbox(remoteNumber, body)
            Catch ex As Exception
            End Try
```
Jangan lupa ganti *modifier* fungsi `SendVoiceNote` dan `SendRegularFile` di file yang sama dari `Private Sub` menjadi `Public Sub`.

Kompilasi ulang dengan Visual Studio 2022. Selesai!


## July 2026 Updates
- **SQL Schema**: Columns updated to `destination_number`, `message_text`, `media_path`. Added `status DEFAULT 'pending'`.

- **Blind Mode**: Hardcoded parameter alse in otmaster.sendMessageto for seamless stealth sending.

- **UI Features**: Maximize button explicitly enabled in FrmMain. AutoRun logic embedded into Settings. ThemeManager extended to handle ListView.
