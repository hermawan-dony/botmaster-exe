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

## 3. Tambahkan Menu & Tombol di FrmMain
Buka `FrmMain.vb` (atau suruh AI mengerjakannya), lalu tambahkan kode ini di baris paling bawah sebelum `End Class`:
```vb
    Private Sub DatabaseAutoSyncToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseAutoSyncToolStripMenuItem.Click
        Dim frm As New FrmDatabaseSync()
        frm.ShowDialog()
    End Sub

    Private Sub WAGWHelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WAGWHelpToolStripMenuItem.Click
        Dim msg As String = "--- PANDUAN PENGGUNAAN DATABASE AUTO-SYNC WAGW ---" & vbCrLf & vbCrLf &
                            "INDONESIA:" & vbCrLf &
                            "1. Buka menu Tools > Database Auto-Sync (ODBC)." & vbCrLf &
                            "2. Masukkan nama DSN dari database Anda." & vbCrLf &
                            "3. Klik Start Auto-Sync." & vbCrLf &
                            "4. Masukkan data pengiriman (nomor WA, pesan, path lampiran) ke tabel 'outbox'." & vbCrLf &
                            "5. Pesan yang masuk ke WA Anda akan terekam otomatis ke tabel 'inbox'." & vbCrLf & vbCrLf &
                            "ENGLISH:" & vbCrLf &
                            "1. Open Tools > Database Auto-Sync (ODBC) menu." & vbCrLf &
                            "2. Enter your database DSN name." & vbCrLf &
                            "3. Click Start Auto-Sync." & vbCrLf &
                            "4. Insert your message data (WA number, text, media path) into the 'outbox' table." & vbCrLf &
                            "5. Incoming WA messages will be automatically recorded into the 'inbox' table."

        MsgBox(msg, MsgBoxStyle.Information, "Botmaster-WAGW Help Guide")
    End Sub
```
*(Jangan lupa daftarkan item menu `DatabaseAutoSyncToolStripMenuItem` dan `WAGWHelpToolStripMenuItem` di `FrmMain.Designer.vb` agar tombolnya muncul).*

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
