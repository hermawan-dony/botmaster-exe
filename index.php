<?php
    ini_set('max_execution_time', 600);
    include "datadb.php";

    // Ambil respon dengan sanitasi sederhana
    $respon = filter_input(INPUT_GET, 'respon', FILTER_SANITIZE_SPECIAL_CHARS);

    if (!empty($respon)) {
        // json_encode untuk mencegah injection di dalam alert()
        echo '<script>alert(' . json_encode($respon) . ');</script>';
    }
?>
<!DOCTYPE html>
<html lang="id">
<head>
    <meta charset="UTF-8">
    <title>Business Whatsapp Sender | BotMaster</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, viewport-fit=cover">

    <meta property="og:description"
          content="Kirim promosi ke RIBUAN nomor WhatsApp hanya dengan sekali klik. Tanpa kirim manual satu per satu.">
    <meta property="og:url" content="business whatsapp sender|botmaster">
    <meta name="description"
          content="Business WhatsApp Sender (BotMaster) membantu kirim promosi ke ribuan nomor WhatsApp hanya dengan sekali klik. Tanpa manual satu per satu, dilengkapi fitur unggulan untuk mempermudah pekerjaan Anda.">

    <link rel="shortcut icon" href="index_files/botmaster.ico" type="image/x-icon">
    
    <!-- Google Fonts: Plus Jakarta Sans -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet">
    
    <!-- Font Awesome untuk ikon -->
    <link rel="stylesheet"
          href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css"
          integrity="sha512-DTOQO9RWCH3ppGqcWaEA1BIZOC6xxalwEsw9c2QQeAIftl+Vegovlnee1c9QX4TctnWMn13TZye+giMm8e2LwA=="
          crossorigin="anonymous" referrerpolicy="no-referrer">

    <style>
        :root {
            --primary: #f59e0b;
            --primary-hover: #d97706;
            --primary-soft: #fef3c7;
            --accent: #8b5cf6;
            --accent-glow: rgba(139, 92, 246, 0.15);
            --bg: #090d16;
            --bg-card: rgba(17, 24, 39, 0.7);
            --border-color: rgba(255, 255, 255, 0.08);
            --text-main: #f3f4f6;
            --text-muted: #9ca3af;
            --success: #10b981;
            --danger: #f43f5e;
            --font-family: 'Plus Jakarta Sans', system-ui, -apple-system, sans-serif;
        }

        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        html {
            scroll-behavior: smooth;
        }

        body {
            font-family: var(--font-family);
            background: 
                radial-gradient(circle at 10% 20%, rgba(37, 99, 235, 0.15) 0%, transparent 40%),
                radial-gradient(circle at 90% 80%, rgba(139, 92, 246, 0.15) 0%, transparent 40%),
                var(--bg);
            color: var(--text-main);
            min-height: 100vh;
            line-height: 1.6;
            -webkit-font-smoothing: antialiased;
        }

        .page-wrap {
            max-width: 1200px;
            margin: 0 auto;
            padding: 3rem 1.5rem 5rem;
        }

        .hero {
            display: grid;
            grid-template-columns: minmax(0, 1.4fr) minmax(0, 1fr);
            gap: 3rem;
            align-items: start;
        }

        @media (max-width: 992px) {
            .hero {
                grid-template-columns: minmax(0, 1fr);
                gap: 2.5rem;
            }
        }

        /* Badge */
        .badge {
            display: inline-flex;
            align-items: center;
            gap: 0.5rem;
            padding: 0.4rem 1rem;
            border-radius: 999px;
            background: rgba(245, 158, 11, 0.1);
            border: 1px solid rgba(245, 158, 11, 0.2);
            font-size: 0.75rem;
            font-weight: 700;
            letter-spacing: 0.05em;
            text-transform: uppercase;
            color: var(--primary);
            margin-bottom: 1.5rem;
        }

        /* Hero Typography */
        h1.hero-title {
            font-size: clamp(2.2rem, 3.5vw, 3.2rem);
            font-weight: 800;
            line-height: 1.2;
            letter-spacing: -0.02em;
            margin-bottom: 1.25rem;
            color: #ffffff;
        }

        h1.hero-title span {
            background: linear-gradient(135deg, #f59e0b, #fbbf24);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        .hero-subtitle {
            font-size: 1.1rem;
            color: var(--text-muted);
            margin-bottom: 2rem;
            max-width: 38rem;
        }

        .hero-subtitle strong {
            color: #ffffff;
            font-weight: 600;
        }

        /* Cards Style */
        .card {
            background: var(--bg-card);
            backdrop-filter: blur(12px);
            -webkit-backdrop-filter: blur(12px);
            border-radius: 1.5rem;
            border: 1px solid var(--border-color);
            padding: 2rem;
            box-shadow: 0 20px 40px rgba(0, 0, 0, 0.3);
        }

        /* Features Section */
        .feature-card h2 {
            font-size: 1.4rem;
            font-weight: 700;
            margin-bottom: 0.5rem;
            color: #ffffff;
        }

        .feature-card h2 span {
            color: var(--primary);
        }

        .feature-lead {
            font-size: 0.95rem;
            color: var(--text-muted);
            margin-bottom: 1.5rem;
        }

        .feature-list {
            list-style: none;
            display: grid;
            gap: 0.85rem;
        }

        .feature-list li {
            display: flex;
            align-items: flex-start;
            gap: 0.75rem;
            font-size: 0.95rem;
            color: #d1d5db;
        }

        .feature-list li i {
            color: var(--success);
            font-size: 1.15rem;
            margin-top: 0.1rem;
            flex-shrink: 0;
        }

        .feature-list li span.highlight {
            background: rgba(245, 158, 11, 0.15);
            border-radius: 0.375rem;
            padding: 0.1rem 0.4rem;
            color: var(--primary);
            font-weight: 600;
        }

        /* Promo Strip */
        .promo-strip {
            margin-top: 1.75rem;
            padding: 1rem 1.25rem;
            border-radius: 1rem;
            background: linear-gradient(135deg, rgba(59, 130, 246, 0.1), rgba(139, 92, 246, 0.1));
            border: 1px solid rgba(139, 92, 246, 0.2);
            font-size: 0.9rem;
        }

        .promo-strip strong {
            color: #ffffff;
        }

        .promo-strip span.em {
            color: var(--primary);
            font-weight: 800;
        }

        /* Sticky Price Card */
        .price-card {
            background: radial-gradient(circle at top right, rgba(245, 158, 11, 0.08), transparent 70%), var(--bg-card);
            border: 1px solid rgba(245, 158, 11, 0.25);
            position: sticky;
            top: 2rem;
        }

        .price-badge {
            display: inline-block;
            background: var(--primary);
            color: #000000;
            font-size: 0.75rem;
            font-weight: 800;
            text-transform: uppercase;
            padding: 0.25rem 0.75rem;
            border-radius: 999px;
            margin-bottom: 0.75rem;
        }

        .price-val {
            font-size: 2.5rem;
            font-weight: 800;
            color: #ffffff;
            margin-bottom: 0.25rem;
            letter-spacing: -0.03em;
        }

        .price-val span {
            color: var(--primary);
        }

        .price-note {
            font-size: 0.9rem;
            color: var(--text-muted);
            margin-bottom: 1.5rem;
        }

        .price-note strong {
            color: var(--success);
            font-weight: 600;
        }

        /* Dynamic Updates Panel */
        .update-panel {
            background: rgba(255, 255, 255, 0.03);
            border: 1px solid rgba(255, 255, 255, 0.05);
            border-radius: 1rem;
            padding: 1.25rem;
            margin-bottom: 1.5rem;
            display: none; /* Diload dinamis via JS */
        }

        .update-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            font-size: 0.8rem;
            color: var(--text-muted);
            margin-bottom: 0.75rem;
            border-bottom: 1px solid rgba(255, 255, 255, 0.06);
            padding-bottom: 0.5rem;
        }

        .update-header span.v-badge {
            background: var(--accent);
            color: #ffffff;
            font-weight: 700;
            padding: 0.15rem 0.5rem;
            border-radius: 0.25rem;
        }

        .update-list {
            list-style: none;
            display: grid;
            gap: 0.5rem;
        }

        .update-list li {
            font-size: 0.85rem;
            color: #d1d5db;
            display: flex;
            gap: 0.5rem;
            align-items: flex-start;
        }

        .update-list li::before {
            content: "•";
            color: var(--accent);
            font-weight: bold;
        }

        /* Buttons Style */
        .btn-row {
            display: grid;
            gap: 0.75rem;
        }

        .btn {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            gap: 0.6rem;
            padding: 0.85rem 1.5rem;
            border-radius: 0.8rem;
            border: none;
            font-size: 0.95rem;
            font-weight: 700;
            cursor: pointer;
            text-decoration: none;
            transition: all 0.2s cubic-bezier(0.4, 0, 0.2, 1);
            text-align: center;
        }

        .btn-primary {
            background: linear-gradient(135deg, #f59e0b, #d97706);
            color: #000000;
            box-shadow: 0 10px 20px rgba(245, 158, 11, 0.2);
        }

        .btn-primary:hover {
            transform: translateY(-2px);
            box-shadow: 0 14px 28px rgba(245, 158, 11, 0.35);
        }

        .btn-outline {
            background: transparent;
            color: #ffffff;
            border: 1px solid rgba(255, 255, 255, 0.15);
        }

        .btn-outline:hover {
            background: rgba(255, 255, 255, 0.05);
            border-color: rgba(255, 255, 255, 0.3);
            transform: translateY(-2px);
        }

        .btn-secondary {
            background: rgba(139, 92, 246, 0.1);
            color: var(--primary-soft);
            border: 1px dashed rgba(245, 158, 11, 0.4);
        }

        .btn-secondary:hover {
            background: rgba(139, 92, 246, 0.2);
            transform: translateY(-2px);
        }

        .btn-whatsapp {
            background: #25d366;
            color: #ffffff;
            font-weight: 700;
            border: none;
            box-shadow: 0 10px 20px rgba(37, 211, 102, 0.15);
        }

        .btn-whatsapp:hover {
            background: #22c35e;
            transform: translateY(-2px);
            box-shadow: 0 14px 28px rgba(37, 211, 102, 0.3);
        }

        .btn-telegram {
            background: #0088cc;
            color: #ffffff;
            font-weight: 700;
            border: none;
            box-shadow: 0 10px 20px rgba(0, 136, 204, 0.15);
        }

        .btn-telegram:hover {
            background: #0077b5;
            transform: translateY(-2px);
            box-shadow: 0 14px 28px rgba(0, 136, 204, 0.3);
        }

        .trust-text {
            margin-top: 1.25rem;
            font-size: 0.8rem;
            color: var(--text-muted);
            text-align: center;
        }

        .trust-text i {
            color: var(--success);
            margin-right: 0.35rem;
        }

        /* Demo Videos Section */
        .section-title {
            margin: 5rem 0 2rem;
            text-align: center;
        }

        .section-title h3 {
            font-size: 1.8rem;
            font-weight: 800;
            color: #ffffff;
            margin-bottom: 0.5rem;
        }

        .section-title p {
            font-size: 0.95rem;
            color: var(--text-muted);
        }

        .video-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(320px, 1fr));
            gap: 2rem;
        }

        .video-card {
            background: var(--bg-card);
            border-radius: 1.25rem;
            padding: 1.25rem;
            border: 1px solid var(--border-color);
            box-shadow: 0 15px 30px rgba(0, 0, 0, 0.2);
            transition: border-color 0.3s;
        }

        .video-card:hover {
            border-color: rgba(139, 92, 246, 0.3);
        }

        .video-card h4 {
            font-size: 1.05rem;
            font-weight: 700;
            color: #ffffff;
            margin-bottom: 0.75rem;
        }

        .video-card iframe {
            width: 100%;
            aspect-ratio: 16 / 9;
            border-radius: 0.75rem;
            border: none;
        }

        .contact-box {
            margin-top: 1.5rem;
        }

        /* Footer */
        .footer {
            margin-top: 5rem;
            padding-top: 2rem;
            border-top: 1px solid rgba(255, 255, 255, 0.05);
            font-size: 0.85rem;
            color: var(--text-muted);
            text-align: center;
        }

        .footer a {
            color: var(--primary);
            text-decoration: none;
            font-weight: 600;
        }

        .footer a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
<div class="page-wrap">

    <header class="hero">
        <div>
            <div class="badge"> 
                <i class="fa-solid fa-bolt"></i>
                BUSINESS WA SENDER - BOTMASTER
            </div>

            <h1 class="hero-title">
                Kirim promosi ke <span>ribuan kontak WhatsApp</span> hanya dengan sekali klik.
            </h1>

            <p class="hero-subtitle">
                BotMaster adalah <strong>Business WhatsApp Sender</strong> yang membantu tim marketing & admin
                mengirim pesan massal secara <strong>cepat, rapi, dan tetap terasa personal</strong> - 
                tanpa harus kirim satu per satu.
            </p>

            <div class="card feature-card">
                <h2><span>Fitur utama</span> untuk memudahkan pekerjaan Anda</h2>
                <p class="feature-lead">
                    Semua yang dibutuhkan untuk campaign WhatsApp yang serius, tanpa biaya bulanan yang
                    menyakitkan kantong.
                </p>
                <ul class="feature-list">
                    <li>
                        <i class="fa-solid fa-circle-check"></i>
                        <span><span class="highlight">Support WhatsApp Multi Device</span> - tetap jalan di akun WA terbaru.</span>
                    </li>
                    <li>
                        <i class="fa-solid fa-circle-check"></i>
                        <span>Unlimited pengiriman pesan (selama mengikuti batas aman penggunaan WA).</span>
                    </li>
                    <li>
                        <i class="fa-solid fa-circle-check"></i>
                        <span>Bisa digunakan beberapa akun WhatsApp sekaligus dalam waktu bersamaan.</span>
                    </li>
                    <li>
                        <i class="fa-solid fa-circle-check"></i>
                        <span>Kirim pesan <strong>lebih personal</strong> sesuai data: nama, nomor invoice, nominal, dst.</span>
                    </li>
                    <li>
                        <i class="fa-solid fa-circle-check"></i>
                        <span>Grab / sedot kontak dari grup-grup WhatsApp dengan cepat.</span>
                    </li>
                    <li>
                        <i class="fa-solid fa-circle-check"></i>
                        <span>Export / import data kontak WhatsApp dengan mudah.</span>
                    </li>
                    <li>
                        <i class="fa-solid fa-circle-check"></i>
                        <span><strong style="color: var(--primary-soft);">Tanpa biaya bulanan.</strong> Tidak ada biaya tambahan saat ada update.</span>
                    </li>
                </ul>

                <div class="promo-strip">
                    <strong>Trial dulu, baru yakin:</strong><br>
                    <span class="em">GRATIS TRIAL 1 HARI</span> - pastikan cocok, jalan di sistem Anda, dan sesuai kebutuhan.
                    Kalau sudah <strong>"klik"</strong>, baru lanjut ke lisensi berbayar.
                </div>
            </div>
        </div>

        <aside>
            <div class="card price-card">
                <div class="price-card-inner">
                    <span class="price-badge">Harga Promo Spesial</span>
                    
                    <div class="price-val">
                        Rp <span><?= htmlspecialchars($harga1 ?? '', ENT_QUOTES, 'UTF-8'); ?></span>
                    </div>
                    
                    <p class="price-note"> 
                        <strong>Masa Aktif 1 Tahun</strong> untuk 1 PC.
                    </p>

                    <!-- Panel Informasi Update Otomatis via Github -->
                    <div id="update-info-panel" class="update-panel">
                        <div class="update-header">
                            <span>Update Terbaru: <span class="v-badge" id="web-version">v--</span></span>
                            <span id="web-release-date">--</span>
                        </div>
                        <p style="font-size: 0.8rem; font-weight: 700; margin-bottom: 0.4rem; color: #ffffff;">Fitur Baru & Pembaruan:</p>
                        <ul class="update-list" id="web-changelog">
                            <!-- JS akan mengisi otomatis -->
                        </ul>
                    </div>

                    <div class="btn-row">
                        <a href="https://github.com/hermawan-dony/botmaster-exe/raw/main/botmaster26.zip"
                           id="btn-download"
                           class="btn btn-primary">
                            <i class="fa-solid fa-download"></i>
                            Download BotMaster Versi --
                        </a>

                        <a href="./request"
                           class="btn btn-outline" target="_blank" rel="noopener">
                            <i class="fa-solid fa-key"></i>
                            Generate Lisensi Lifetime
                        </a>

                        <a href="./order"
                           class="btn btn-secondary" target="_blank" rel="noopener">
                            <i class="fa-solid fa-receipt"></i>
                            Lihat Lisensi / Order Anda
                        </a>
                    </div>

                    <p class="trust-text">
                        <i class="fa-solid fa-shield-halved"></i>
                        Lisensi tetap aktif meskipun ada update versi - tanpa biaya tambahan.
                    </p>
                </div>
            </div>

            <?php if (!empty($pembayaran)) : ?>
                <div class="contact-box">
                    <?php if (!empty($url_reseller) && $url_reseller === "http://app.wasender.biz") : ?>
                        <a href="https://t.me/wawn1782"
                           class="btn btn-telegram"
                           style="width: 100%;" target="_blank" rel="noopener">
                            <i class="fa-brands fa-telegram" style="font-size: 1.15rem;"></i>
                            Butuh konsultasi? Chat via Telegram
                        </a>
                    <?php else : ?>
                        <?php
                        $wa_clean = isset($wa_reseller) ? preg_replace('/[^0-9]/', '', $wa_reseller) : '';
                        $tpl_text = rawurlencode(
                            "Hallo kak, saya mau konsultasi soal Aplikasi Botmaster / Business WA Sender ya.."
                        );
                        ?>
                        <a href="https://api.whatsapp.com/send?phone=<?= $wa_clean; ?>&text=<?= $tpl_text; ?>"
                           class="btn btn-whatsapp"
                           style="width: 100%;" target="_blank" rel="noopener">
                            <i class="fa-brands fa-whatsapp" style="font-size: 1.25rem;"></i>
                            Butuh konsultasi? Hubungi via WhatsApp
                        </a>
                    <?php endif; ?>
                </div>
            <?php endif; ?>
        </aside>
    </header>

    <section>
        <div class="section-title">
            <h3>Video Demo BotMaster / WA Sender</h3>
            <p>Lihat langsung cara kerja BotMaster sebelum Anda memutuskan untuk menggunakan di bisnis.</p>
        </div>

        <div class="video-grid">
            <div class="video-card">
                <h4>Overview & Pengiriman Broadcast</h4>
                <iframe src="https://www.youtube.com/embed/UCxjHLwWO4k?list=PL-GUChjQHCJfX7w730Q4Pd6p6lUrcySTZ"
                        title="Demo BotMaster - Overview"
                        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                        referrerpolicy="strict-origin-when-cross-origin"
                        allowfullscreen></iframe>
            </div>
            <div class="video-card">
                <h4>Filter & Cek Nomor Aktif</h4>
                <iframe src="https://www.youtube.com/embed/fKThXedMshY?list=PL-GUChjQHCJfX7w730Q4Pd6p6lUrcySTZ"
                        title="Demo BotMaster - Filter Number"
                        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                        referrerpolicy="strict-origin-when-cross-origin"
                        allowfullscreen></iframe>
            </div>
        </div>
    </section>

    <footer class="footer">
        <div>
            &copy; <?= date('Y'); ?> BotMaster - Business WhatsApp Sender.
            Dibuat untuk membantu Anda mengelola promosi & reminder pelanggan dengan lebih efisien.
        </div>
        <div style="margin-top:.5rem;">
            <span style="opacity:.75;">Perlu bantuan?</span>
            <a href="#top">Hubungi admin</a>.
        </div>
    </footer>
</div>

<!-- Fetch Metadata dari Github secara Dinamis -->
<script>
    document.addEventListener("DOMContentLoaded", function() {
        const metadataUrl = "https://raw.githubusercontent.com/hermawan-dony/botmaster-exe/main/version.json";
        
        fetch(metadataUrl)
            .then(response => {
                if (!response.ok) {
                    throw new Error("Gagal memuat metadata versi");
                }
                return response.json();
            })
            .then(data => {
                // 1. Update text tombol & link download
                const btnDownload = document.getElementById("btn-download");
                if (btnDownload) {
                    btnDownload.href = data.download_url;
                    btnDownload.innerHTML = `<i class="fa-solid fa-download"></i> Download BotMaster Versi ${data.version} (Terbaru)`;
                }

                // 2. Update Versi & Tanggal Rilis di Panel
                const webVersion = document.getElementById("web-version");
                const webReleaseDate = document.getElementById("web-release-date");
                if (webVersion) webVersion.textContent = `v${data.version}`;
                if (webReleaseDate) webReleaseDate.textContent = data.release_date;

                // 3. Update Changelog
                const webChangelog = document.getElementById("web-changelog");
                if (webChangelog && data.changelog) {
                    webChangelog.innerHTML = ""; // Kosongkan placeholder
                    data.changelog.forEach(item => {
                        const li = document.createElement("li");
                        li.textContent = item;
                        webChangelog.appendChild(li);
                    });
                }

                // 4. Tampilkan panel update
                const updatePanel = document.getElementById("update-info-panel");
                if (updatePanel) {
                    updatePanel.style.display = "block";
                }
            })
            .catch(error => {
                console.error("Error fetching updates info:", error);
                // Fallback jika github bermasalah
                const btnDownload = document.getElementById("btn-download");
                if (btnDownload) {
                    btnDownload.innerHTML = `<i class="fa-solid fa-download"></i> Download BotMaster (Terbaru)`;
                }
            });
    });
</script>
</body>
</html>
