# Botmaster-WAGW v2.6.1 Executable Release

Repositori ini berisi file executable (binary bundle) untuk **Botmaster-WAGW v2.6.1** yang telah dikompilasi menggunakan Visual Studio 2022. Pada versi ini, Botmaster telah bertransformasi dengan UI berdesain Dark Theme premium dan dilengkapi dengan kemampuan layaknya mesin Gateway WAGW.

## Isi Bundle (`botmaster26.zip`)
File bundle ZIP berisi program utama beserta seluruh library dependensi yang dibutuhkan:
* **Botmaster-WAGW.exe** (Aplikasi Utama / Core Engine)
* **Languages/** (Dukungan multibahasa termasuk Bahasa Indonesia)
* **runtimes/** (Library runtime pendukung)
* Dependensi sistem lainnya seperti WebView2, Newtonsoft.Json, dan library API HTTP.

## Fitur Utama
* **NEW: WAGW ODBC Auto-Sync**: Sistem gerbang (*gateway*) otomatis yang membaca tabel `outbox` dan menulis ke tabel `inbox` via koneksi ODBC DSN tanpa membuat UI *freeze*, mendukung pengiriman Gambar/Dokumen (via kolom `wa_media`).
* **NEW: WAGW Dark Theme UI**: Antarmuka mode gelap premium (Warna Navy Blue & Aksen Pink).
* **WhatsApp Bulk Sender**: Pengiriman pesan massal.
* **Group Grabber**: Pengambil data kontak grup WhatsApp.
* **Contact Filter**: Penyaring nomor WhatsApp aktif.
* **Auto Reply**: Balas pesan otomatis dengan dukungan integrasi **ChatGPT / OpenAI**.
* **LID Contacts Import**: Dukungan impor kontak berformat `@lid`.

## Lisensi & Validasi
* Sistem verifikasi lisensi pada rilis ini telah diarahkan untuk memvalidasi lisensi melalui server **`wasender.biz`**.

## Cara Penggunaan
1. Unduh file **[botmaster26.zip](botmaster26.zip)** dari repositori ini.
2. Ekstrak file zip tersebut ke folder komputer Anda.
3. Jalankan **`Botmaster.exe`** untuk masuk ke aplikasi utama.
