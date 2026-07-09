<?php
    ini_set('max_execution_time', 600);
    include "datadb.php";

    // Ambil respon dengan sanitasi sederhana
    $respon = filter_input(INPUT_GET, 'respon', FILTER_SANITIZE_SPECIAL_CHARS);

    if (!empty($respon)) {
        echo '<script>alert(' . json_encode($respon) . ');</script>';
    }

    $lang = isset($_GET['lang']) && $_GET['lang'] === 'en' ? 'en' : 'id';
    
    $t = [
        'id' => [
            'meta_title' => 'BotMaster | Business WhatsApp Sender Terbaik',
            'meta_desc' => 'Tingkatkan omzet bisnis Anda dengan BotMaster. Software pengirim pesan WhatsApp massal otomatis, cepat, aman, dan tanpa biaya bulanan.',
            'client_area' => 'Client Area',
            'badge_status' => 'Sistem Stabil & Siap Digunakan',
            'hero_title_1' => 'Kirim Promosi ke ',
            'hero_title_span' => 'Ribuan Kontak',
            'hero_title_2' => ' Tanpa Ribet',
            'hero_desc' => 'Tingkatkan konversi penjualan Anda dengan sistem broadcast WhatsApp otomatis. Tanpa biaya langganan bulanan, lisensi sekali bayar untuk selamanya.',
            'feat_1' => 'Multi Device Support',
            'feat_2' => 'Unlimited Pesan',
            'feat_3' => 'Grab Kontak Grup',
            'feat_4' => 'Import/Export Mudah',
            'feat_5' => 'Pesan Personal (Nama, dll)',
            'feat_6' => 'Bebas Biaya Bulanan',
            'price_title' => 'Lisensi Lifetime',
            'price_unit' => '/ 1 PC',
            'btn_download' => 'Download Update Terbaru',
            'btn_gen_license' => 'Generate Lisensi Aktif',
            'btn_order' => 'Order Lisensi Baru',
            'guarantee' => '100% Bebas Update Fee Selamanya',
            'chat_telegram' => 'Chat Support via Telegram',
            'chat_whatsapp' => 'Chat Support via WhatsApp',
            'stat_1' => 'Tingkat Keterkiriman',
            'stat_2' => 'Lebih Cepat dari Manual',
            'stat_3' => 'Lisensi Tanpa Batas Waktu',
            'why_title' => 'Mengapa Memilih BotMaster?',
            'why_desc' => 'Fitur lengkap yang dirancang khusus untuk mempermudah operasional dan meningkatkan omset bisnis Anda.',
            'ben_1_title' => 'Efisiensi Waktu & Tenaga',
            'ben_1_desc' => 'Otomatisasi pengiriman promosi dan follow-up. Tim admin Anda tidak perlu lagi copy-paste pesan satu per satu, sehingga bisa fokus melayani pembeli.',
            'ben_2_title' => 'Meningkatkan Penjualan',
            'ben_2_desc' => 'Jangkau kembali database pelanggan lama Anda dengan penawaran menarik secara instan. Follow-up konsisten adalah kunci meningkatkan konversi.',
            'ben_3_title' => 'Auto Reply Bot',
            'ben_3_desc' => 'Balas pesan pelanggan secara otomatis berdasarkan kata kunci tertentu. Bisa dikombinasikan dengan tombol interaktif dan katalog produk.',
            'ben_4_title' => 'Pesan Tepat Sasaran',
            'ben_4_desc' => 'Fitur personalisasi pesan memungkinkan Anda memanggil nama pelanggan atau menyebutkan detail invoice, membuat pesan terasa lebih intim dan tidak seperti robot.',
            'ben_5_title' => 'Filter Nomor Aktif',
            'ben_5_desc' => 'Bersihkan database Anda. Sistem akan memverifikasi nomor WhatsApp yang aktif dan tidak aktif sebelum mengirim, menjaga efisiensi kampanye Anda.',
            'ben_6_title' => 'Hemat Biaya Operasional',
            'ben_6_desc' => 'Cukup bayar lisensi sekali untuk penggunaan selamanya di PC Anda. Jauh lebih terjangkau dibandingkan layanan WhatsApp berbayar bulanan.',
            'vid_title' => 'Lihat Cara Kerjanya',
            'vid_desc' => 'Tonton demo singkat bagaimana BotMaster mempermudah rutinitas marketing Anda setiap harinya.',
            'vid_1_title' => 'Fitur Broadcast Masal',
            'vid_2_title' => 'Fitur Filter Nomor Aktif',
            'footer' => 'BotMaster - Solusi WhatsApp Marketing Otomatis.',
            'lang_switch_id' => 'Indonesia',
            'lang_switch_en' => 'English'
        ],
        'en' => [
            'meta_title' => 'BotMaster | Best Business WhatsApp Sender',
            'meta_desc' => 'Increase your business revenue with BotMaster. Fast, safe, and automated bulk WhatsApp sender without monthly subscription fees.',
            'client_area' => 'Client Area',
            'badge_status' => 'System Stable & Ready',
            'hero_title_1' => 'Send Promotions to ',
            'hero_title_span' => 'Thousands of Contacts',
            'hero_title_2' => ' Effortlessly',
            'hero_desc' => 'Boost your sales conversion with our automated WhatsApp broadcast system. No monthly fees, one-time payment for a lifetime license.',
            'feat_1' => 'Multi Device Support',
            'feat_2' => 'Unlimited Messages',
            'feat_3' => 'Grab Group Contacts',
            'feat_4' => 'Easy Import/Export',
            'feat_5' => 'Personalized Messages',
            'feat_6' => 'No Monthly Fees',
            'price_title' => 'Lifetime License',
            'price_unit' => '/ 1 PC',
            'btn_download' => 'Download Latest Update',
            'btn_gen_license' => 'Generate Active License',
            'btn_order' => 'Order New License',
            'guarantee' => '100% Free Lifetime Updates',
            'chat_telegram' => 'Chat Support via Telegram',
            'chat_whatsapp' => 'Chat Support via WhatsApp',
            'stat_1' => 'Delivery Rate',
            'stat_2' => 'Faster than Manual',
            'stat_3' => 'Lifetime License',
            'why_title' => 'Why Choose BotMaster?',
            'why_desc' => 'Comprehensive features specifically designed to simplify your operations and boost your business revenue.',
            'ben_1_title' => 'Time & Effort Efficiency',
            'ben_1_desc' => 'Automate promotions and follow-ups. Your admin team no longer needs to copy-paste messages one by one.',
            'ben_2_title' => 'Increase Sales',
            'ben_2_desc' => 'Instantly reach out to your old customer database with attractive offers. Consistent follow-ups are key to conversions.',
            'ben_3_title' => 'Auto Reply Bot',
            'ben_3_desc' => 'Automatically reply to customer messages based on specific keywords. Can be combined with interactive buttons and catalogs.',
            'ben_4_title' => 'Targeted Messaging',
            'ben_4_desc' => 'Personalization features allow you to call customers by name or mention invoice details, making messages feel intimate.',
            'ben_5_title' => 'Active Number Filter',
            'ben_5_desc' => 'Clean your database. The system verifies active and inactive WhatsApp numbers before sending, maintaining campaign efficiency.',
            'ben_6_title' => 'Save Operational Costs',
            'ben_6_desc' => 'Pay the license once for lifetime use on your PC. Much more affordable compared to monthly paid WhatsApp services.',
            'vid_title' => 'See How It Works',
            'vid_desc' => 'Watch a short demo of how BotMaster simplifies your daily marketing routine.',
            'vid_1_title' => 'Mass Broadcast Feature',
            'vid_2_title' => 'Active Number Filter Feature',
            'footer' => 'BotMaster - Automated WhatsApp Marketing Solution.',
            'lang_switch_id' => 'Indonesia',
            'lang_switch_en' => 'English'
        ]
    ];
    $text = $t[$lang];
?>
<!DOCTYPE html>
<html lang="<?= $lang ?>">
<head>
    <meta charset="UTF-8">
    <title><?= $text['meta_title'] ?></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, viewport-fit=cover">
    <meta name="description" content="<?= $text['meta_desc'] ?>">

    <link rel="shortcut icon" href="index_files/botmaster.ico" type="image/x-icon">
    
    <!-- Google Fonts -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@300;400;500;600;700;800&family=Space+Grotesk:wght@700&display=swap" rel="stylesheet">
    
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" integrity="sha512-DTOQO9RWCH3ppGqcWaEA1BIZOC6xxalwEsw9c2QQeAIftl+Vegovlnee1c9QX4TctnWMn13TZye+giMm8e2LwA==" crossorigin="anonymous" referrerpolicy="no-referrer">

    <style>
        :root {
            --primary: #25D366; /* WhatsApp Green */
            --primary-hover: #128C7E;
            --accent: #F59E0B; /* Gold for premium feel */
            --bg-main: #0B1120;
            --bg-card: rgba(30, 41, 59, 0.7);
            --bg-card-hover: rgba(30, 41, 59, 0.9);
            --text-light: #F8FAFC;
            --text-muted: #94A3B8;
            --border: rgba(255, 255, 255, 0.08);
            --font-main: 'Plus Jakarta Sans', sans-serif;
            --font-display: 'Space Grotesk', sans-serif;
        }

        * { margin: 0; padding: 0; box-sizing: border-box; }
        html { scroll-behavior: smooth; }
        
        body {
            font-family: var(--font-main);
            background-color: var(--bg-main);
            color: var(--text-light);
            line-height: 1.6;
            overflow-x: hidden;
            background-image: 
                radial-gradient(circle at 15% 50%, rgba(37, 211, 102, 0.08) 0%, transparent 50%),
                radial-gradient(circle at 85% 30%, rgba(245, 158, 11, 0.05) 0%, transparent 50%);
            background-size: 200% 200%;
            background-attachment: fixed;
            animation: gradientMove 15s ease infinite alternate;
        }

        @keyframes gradientMove {
            0% { background-position: 0% 50%; }
            50% { background-position: 100% 50%; }
            100% { background-position: 0% 50%; }
        }

        /* Animations */
        @keyframes fadeUp {
            from { opacity: 0; transform: translateY(30px); }
            to { opacity: 1; transform: translateY(0); }
        }
        
        .animate-up { animation: fadeUp 0.8s cubic-bezier(0.16, 1, 0.3, 1) forwards; opacity: 0; }
        .delay-1 { animation-delay: 0.2s; }
        .delay-2 { animation-delay: 0.4s; }

        .container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 0 1.5rem;
        }

        /* Navbar & Language Switcher */
        .navbar {
            padding: 1.5rem 0;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
        .logo {
            font-family: var(--font-display);
            font-size: 1.5rem;
            color: white;
            text-decoration: none;
            display: flex;
            align-items: center;
            gap: 0.5rem;
        }
        .logo i { color: var(--primary); }

        .nav-actions {
            display: flex;
            gap: 1rem;
            align-items: center;
        }

        .lang-switch {
            display: flex;
            gap: 0.5rem;
            background: rgba(255, 255, 255, 0.05);
            padding: 0.25rem;
            border-radius: 8px;
            border: 1px solid var(--border);
        }
        .lang-switch a {
            padding: 0.4rem 0.8rem;
            border-radius: 6px;
            text-decoration: none;
            color: var(--text-muted);
            font-size: 0.85rem;
            font-weight: 600;
            transition: all 0.3s;
        }
        .lang-switch a.active {
            background: rgba(255, 255, 255, 0.1);
            color: white;
        }
        .lang-switch a:hover:not(.active) {
            color: white;
        }

        /* Hero Section */
        .hero {
            padding: 4rem 0 6rem;
            display: grid;
            grid-template-columns: 1.2fr 1fr;
            gap: 4rem;
            align-items: center;
        }
        @media (max-width: 992px) {
            .hero { grid-template-columns: 1fr; gap: 3rem; text-align: center; }
            .nav-actions { flex-direction: column-reverse; }
        }

        .badge-update {
            display: inline-flex;
            align-items: center;
            gap: 0.5rem;
            background: rgba(37, 211, 102, 0.1);
            border: 1px solid rgba(37, 211, 102, 0.3);
            color: var(--primary);
            padding: 0.4rem 1rem;
            border-radius: 50px;
            font-size: 0.85rem;
            font-weight: 600;
            margin-bottom: 1.5rem;
            box-shadow: 0 0 20px rgba(37, 211, 102, 0.2);
        }
        
        .hero h1 {
            font-family: var(--font-display);
            font-size: clamp(2.5rem, 4vw, 4rem);
            line-height: 1.1;
            margin-bottom: 1.5rem;
        }
        .hero h1 span { color: var(--primary); position: relative; }
        .hero h1 span::after {
            content: ''; position: absolute; left: 0; bottom: 0; width: 100%; height: 30%; background: rgba(37, 211, 102, 0.2); z-index: -1;
        }
        
        .hero p {
            font-size: 1.1rem;
            color: var(--text-muted);
            margin-bottom: 2.5rem;
            max-width: 90%;
        }
        @media (max-width: 992px) { .hero p { margin: 0 auto 2.5rem; } }

        .feature-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 1rem;
        }
        
        .feat-item {
            display: flex;
            align-items: center;
            gap: 0.75rem;
            font-size: 0.95rem;
            background: rgba(255,255,255,0.02);
            padding: 1rem;
            border-radius: 12px;
            border: 1px solid var(--border);
            transition: all 0.3s cubic-bezier(0.16, 1, 0.3, 1);
            box-shadow: inset 0 0 0 1px transparent;
        }
        .feat-item:hover {
            border-color: var(--primary);
            transform: translateY(-4px) scale(1.02);
            background: rgba(255,255,255,0.05);
            box-shadow: 0 10px 20px rgba(0,0,0,0.2), inset 0 0 0 1px rgba(37, 211, 102, 0.3);
        }
        .feat-item i { color: var(--accent); font-size: 1.2rem; }

        /* Pricing Card (Floating & Sticky) */
        .pricing-wrapper {
            position: relative;
        }
        .pricing-card {
            background: var(--bg-card);
            backdrop-filter: blur(24px);
            -webkit-backdrop-filter: blur(24px);
            border-radius: 24px;
            padding: 2.5rem;
            border: 1px solid var(--border);
            box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.5), 0 0 0 1px rgba(255,255,255,0.1) inset;
            position: sticky;
            top: 2rem;
            transition: transform 0.4s cubic-bezier(0.16, 1, 0.3, 1);
        }
        .pricing-card:hover {
            transform: translateY(-5px);
            background: var(--bg-card-hover);
            border-color: rgba(255,255,255,0.15);
        }
        .pricing-card::before {
            content: ''; position: absolute; inset: -1px;
            border-radius: 25px; padding: 1px;
            background: linear-gradient(135deg, var(--primary), transparent, var(--accent));
            -webkit-mask: linear-gradient(#fff 0 0) content-box, linear-gradient(#fff 0 0);
            -webkit-mask-composite: xor; mask-composite: exclude;
            pointer-events: none;
            opacity: 0.6;
            transition: opacity 0.3s;
        }
        .pricing-card:hover::before { opacity: 1; }

        .price-title {
            color: var(--accent); font-size: 0.9rem; font-weight: 700; text-transform: uppercase; letter-spacing: 1.5px; margin-bottom: 0.5rem;
        }
        .price-amount {
            font-family: var(--font-display); font-size: 3rem; font-weight: 700; margin-bottom: 1rem;
            background: linear-gradient(to right, #fff, #cbd5e1);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }
        .price-amount span { font-size: 1rem; color: var(--text-muted); font-weight: 400; font-family: var(--font-main); -webkit-text-fill-color: var(--text-muted); }
        
        /* Dynamic Update Panel */
        .update-box {
            background: rgba(0,0,0,0.4); border-radius: 12px; padding: 1rem; margin-bottom: 1.5rem; border: 1px solid rgba(255,255,255,0.05);
            box-shadow: inset 0 2px 10px rgba(0,0,0,0.2);
        }
        .update-box-header { display: flex; justify-content: space-between; align-items: center; margin-bottom: 0.5rem; border-bottom: 1px solid rgba(255,255,255,0.05); padding-bottom: 0.5rem;}
        .version-tag { background: var(--primary); color: #000; padding: 2px 8px; border-radius: 4px; font-weight: 700; font-size: 0.8rem; }
        .date-tag { font-size: 0.8rem; color: var(--text-muted); }
        .changelog-list { list-style: none; font-size: 0.85rem; color: #cbd5e1; }
        .changelog-list li { position: relative; padding-left: 1rem; margin-bottom: 0.25rem; }
        .changelog-list li::before { content: '→'; position: absolute; left: 0; color: var(--accent); }

        .btn-group { display: flex; flex-direction: column; gap: 1rem; margin-bottom: 1.5rem; }
        
        .btn {
            display: inline-flex; justify-content: center; align-items: center; gap: 0.5rem;
            padding: 1rem 1.5rem; border-radius: 12px; font-weight: 600; font-size: 1rem;
            text-decoration: none; transition: all 0.3s cubic-bezier(0.16, 1, 0.3, 1); border: none; cursor: pointer;
            position: relative; overflow: hidden;
        }
        .btn-main {
            background: var(--primary); color: #000; box-shadow: 0 10px 25px rgba(37, 211, 102, 0.3);
        }
        .btn-main::after {
            content: ''; position: absolute; top: 0; left: -100%; width: 50%; height: 100%;
            background: linear-gradient(to right, transparent, rgba(255,255,255,0.4), transparent);
            transform: skewX(-20deg); transition: 0.5s;
        }
        .btn-main:hover::after { left: 150%; }
        .btn-main:hover { transform: translateY(-3px); background: var(--primary-hover); box-shadow: 0 15px 35px rgba(37, 211, 102, 0.5); }
        
        .btn-outline {
            background: rgba(255,255,255,0.02); color: white; border: 1px solid var(--border);
        }
        .btn-outline:hover { background: rgba(255,255,255,0.08); border-color: rgba(255,255,255,0.3); transform: translateY(-2px); box-shadow: 0 5px 15px rgba(0,0,0,0.3); }

        .btn-secondary {
            background: rgba(245, 158, 11, 0.1); color: var(--accent); border: 1px solid rgba(245, 158, 11, 0.2);
        }
        .btn-secondary:hover { background: rgba(245, 158, 11, 0.2); transform: translateY(-2px); }

        .guarantee { text-align: center; font-size: 0.85rem; color: var(--text-muted); display: flex; align-items: center; justify-content: center; gap: 0.5rem;}
        .guarantee i { color: var(--accent); font-size: 1.1rem; }

        /* Social Proof / Stats */
        .stats-section { padding: 4rem 0; text-align: center; position: relative; }
        .stats-section::before { content: ''; position: absolute; top: 0; left: 10%; right: 10%; height: 1px; background: linear-gradient(90deg, transparent, var(--border), transparent); }
        .stats-grid { display: grid; grid-template-columns: repeat(3, 1fr); gap: 2rem; max-width: 800px; margin: 0 auto; }
        .stat-item h4 { font-family: var(--font-display); font-size: 3rem; color: var(--primary); margin-bottom: 0.5rem; text-shadow: 0 0 20px rgba(37, 211, 102, 0.3); }
        .stat-item p { color: var(--text-muted); font-weight: 500; font-size: 1.1rem; }
        @media (max-width: 768px) { .stats-grid { grid-template-columns: 1fr; } }

        /* Benefits Section */
        .benefits-section { padding: 6rem 0; background: rgba(0,0,0,0.2); border-top: 1px solid rgba(255,255,255,0.02); }
        .benefits-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(320px, 1fr)); gap: 2rem; margin-top: 4rem; }
        .benefit-card {
            background: rgba(255,255,255,0.015); border: 1px solid var(--border);
            padding: 2.5rem; border-radius: 20px; transition: all 0.4s cubic-bezier(0.16, 1, 0.3, 1);
            position: relative; overflow: hidden;
        }
        .benefit-card::before {
            content: ''; position: absolute; top: 0; left: 0; width: 100%; height: 100%;
            background: radial-gradient(circle at top right, rgba(245, 158, 11, 0.1), transparent 60%);
            opacity: 0; transition: 0.4s; z-index: 0;
        }
        .benefit-card > * { position: relative; z-index: 1; }
        .benefit-card:hover { transform: translateY(-8px); background: rgba(255,255,255,0.03); border-color: rgba(245, 158, 11, 0.3); box-shadow: 0 20px 40px rgba(0,0,0,0.4); }
        .benefit-card:hover::before { opacity: 1; }
        .benefit-icon { width: 60px; height: 60px; background: rgba(245, 158, 11, 0.1); color: var(--accent); border-radius: 14px; display: flex; align-items: center; justify-content: center; font-size: 1.8rem; margin-bottom: 1.5rem; border: 1px solid rgba(245, 158, 11, 0.2); }
        .benefit-card h3 { font-size: 1.3rem; font-family: var(--font-display); margin-bottom: 1rem; color: #fff; }
        .benefit-card p { font-size: 1rem; color: var(--text-muted); line-height: 1.7; }

        /* Videos Section */
        .video-section { padding: 6rem 0; border-top: 1px solid var(--border); position: relative; }
        .section-title { text-align: center; margin-bottom: 4rem; }
        .section-title h2 { font-family: var(--font-display); font-size: 2.5rem; margin-bottom: 1rem; background: linear-gradient(to right, #fff, #94A3B8); -webkit-background-clip: text; -webkit-text-fill-color: transparent; }
        .section-title p { color: var(--text-muted); max-width: 650px; margin: 0 auto; font-size: 1.1rem; }
        
        .video-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(320px, 1fr)); gap: 3rem; }
        .video-wrapper {
            background: rgba(255,255,255,0.02); padding: 1.5rem; border-radius: 20px; border: 1px solid var(--border);
            transition: transform 0.4s, box-shadow 0.4s;
        }
        .video-wrapper:hover { transform: translateY(-8px) scale(1.01); box-shadow: 0 20px 50px rgba(0,0,0,0.5); border-color: rgba(255,255,255,0.15); }
        .video-wrapper iframe { width: 100%; aspect-ratio: 16/9; border-radius: 12px; border: none; box-shadow: 0 10px 30px rgba(0,0,0,0.3); }
        .video-wrapper h3 { margin-top: 1.5rem; font-size: 1.2rem; text-align: center; color: #fff; }

        /* Footer */
        footer { padding: 3rem 0; text-align: center; border-top: 1px solid var(--border); color: var(--text-muted); font-size: 0.95rem; background: rgba(0,0,0,0.3); }
        footer a { color: var(--primary); text-decoration: none; font-weight: 600; }

        /* FOMO Toast */
        .fomo-toast {
            position: fixed;
            bottom: 30px;
            left: 30px;
            background: rgba(11, 17, 32, 0.85);
            backdrop-filter: blur(16px);
            -webkit-backdrop-filter: blur(16px);
            border: 1px solid rgba(255,255,255,0.1);
            padding: 1rem;
            border-radius: 12px;
            display: flex;
            align-items: center;
            gap: 1rem;
            box-shadow: 0 10px 30px rgba(0,0,0,0.5);
            z-index: 999;
            transform: translateY(100px);
            opacity: 0;
            transition: all 0.5s cubic-bezier(0.16, 1, 0.3, 1);
            pointer-events: none;
        }
        .fomo-toast.show {
            transform: translateY(0);
            opacity: 1;
        }
        .fomo-icon {
            background: rgba(37, 211, 102, 0.15);
            color: var(--primary);
            width: 40px;
            height: 40px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 1.2rem;
        }
        .fomo-text p {
            margin: 0;
            font-size: 0.9rem;
            color: #fff;
        }
        .fomo-text p span {
            font-weight: 700;
            color: var(--accent);
        }
        .fomo-text small {
            font-size: 0.75rem;
            color: var(--text-muted);
        }

        /* Mobile Sticky CTA */
        .mobile-cta { display: none; position: fixed; bottom: 0; left: 0; width: 100%; background: rgba(11, 17, 32, 0.9); backdrop-filter: blur(10px); -webkit-backdrop-filter: blur(10px); border-top: 1px solid rgba(255,255,255,0.1); padding: 1rem; z-index: 100; text-align: center; box-shadow: 0 -10px 20px rgba(0,0,0,0.5); }
        .mobile-cta .btn { width: 100%; padding: 1.2rem; font-size: 1.1rem; }
        @media (max-width: 768px) {
            .mobile-cta { display: block; }
            body { padding-bottom: 80px; }
        }
    </style>
</head>
<body>

    <nav class="navbar container">
        <a href="#" class="logo">
            <i class="fa-solid fa-robot"></i> BotMaster
        </a>
        <div class="nav-actions">
            <div class="lang-switch">
                <a href="?lang=id" class="<?= $lang === 'id' ? 'active' : '' ?>">ID</a>
                <a href="?lang=en" class="<?= $lang === 'en' ? 'active' : '' ?>">EN</a>
            </div>
        </div>
    </nav>

    <main class="container hero">
        <div class="hero-content animate-up">
            <div class="badge-update" id="badge-update-status">
                <i class="fa-solid fa-circle-check"></i> <?= $text['badge_status'] ?>
            </div>
            <h1><?= $text['hero_title_1'] ?><span><?= $text['hero_title_span'] ?></span><?= $text['hero_title_2'] ?></h1>
            <p><?= $text['hero_desc'] ?></p>

            <div class="feature-grid">
                <div class="feat-item"><i class="fa-solid fa-mobile-screen"></i> <?= $text['feat_1'] ?></div>
                <div class="feat-item"><i class="fa-solid fa-infinity"></i> <?= $text['feat_2'] ?></div>
                <div class="feat-item"><i class="fa-solid fa-users"></i> <?= $text['feat_3'] ?></div>
                <div class="feat-item"><i class="fa-solid fa-file-import"></i> <?= $text['feat_4'] ?></div>
                <div class="feat-item"><i class="fa-solid fa-user-tag"></i> <?= $text['feat_5'] ?></div>
                <div class="feat-item"><i class="fa-solid fa-coins"></i> <?= $text['feat_6'] ?></div>
            </div>
        </div>

        <div class="pricing-wrapper animate-up delay-1">
            <div class="pricing-card">
                <div class="price-title"><?= $text['price_title'] ?></div>
                <!-- Menampilkan harga selalu dalam IDR sesuai permintaan user -->
                <div class="price-amount">Rp <?= htmlspecialchars($harga1 ?? '150.000', ENT_QUOTES, 'UTF-8'); ?> <span><?= $text['price_unit'] ?></span></div>
                
                <div class="update-box" id="update-box" style="display: none;">
                    <div class="update-box-header">
                        <span class="version-tag" id="web-version">v--</span>
                        <span class="date-tag" id="web-release-date">--</span>
                    </div>
                    <ul class="changelog-list" id="web-changelog">
                        <!-- Dimuat dari version.json Github -->
                    </ul>
                </div>

                <div class="btn-group">
                    <a href="https://github.com/hermawan-dony/botmaster-exe/raw/main/botmaster26.zip" id="btn-download" class="btn btn-main">
                        <i class="fa-solid fa-cloud-arrow-down"></i> <?= $text['btn_download'] ?>
                    </a>
                    <a href="./request" class="btn btn-secondary" target="_blank">
                        <i class="fa-solid fa-key"></i> <?= $text['btn_gen_license'] ?>
                    </a>
                    <a href="./order" class="btn btn-outline" target="_blank">
                        <i class="fa-solid fa-cart-shopping"></i> <?= $text['btn_order'] ?>
                    </a>
                </div>

                <div class="guarantee">
                    <i class="fa-solid fa-shield-halved"></i> <?= $text['guarantee'] ?>
                </div>

                <?php if (!empty($pembayaran)) : ?>
                    <div style="margin-top: 1.5rem; text-align: center;">
                        <?php if (!empty($url_reseller) && $url_reseller === "http://app.wasender.biz") : ?>
                            <a href="https://t.me/wawn1782" target="_blank" style="color: #38bdf8; text-decoration: none; font-size: 0.95rem; font-weight: 600; display: inline-flex; align-items: center; gap: 0.4rem; transition: color 0.2s;">
                                <i class="fa-brands fa-telegram" style="font-size: 1.2rem;"></i> <?= $text['chat_telegram'] ?>
                            </a>
                        <?php else : ?>
                            <?php
                            $wa_clean = isset($wa_reseller) ? preg_replace('/[^0-9]/', '', $wa_reseller) : '';
                            $tpl_text = rawurlencode("Hallo kak, saya mau konsultasi soal Aplikasi Botmaster.");
                            ?>
                            <a href="https://api.whatsapp.com/send?phone=<?= $wa_clean; ?>&text=<?= $tpl_text; ?>" target="_blank" style="color: var(--primary); text-decoration: none; font-size: 0.95rem; font-weight: 600; display: inline-flex; align-items: center; gap: 0.4rem; transition: color 0.2s;">
                                <i class="fa-brands fa-whatsapp" style="font-size: 1.2rem;"></i> <?= $text['chat_whatsapp'] ?>
                            </a>
                        <?php endif; ?>
                    </div>
                <?php endif; ?>

            </div>
        </div>
    </main>

    <section class="stats-section animate-up delay-2">
        <div class="container stats-grid">
            <div class="stat-item">
                <h4>99%</h4>
                <p><?= $text['stat_1'] ?></p>
            </div>
            <div class="stat-item">
                <h4>10x</h4>
                <p><?= $text['stat_2'] ?></p>
            </div>
            <div class="stat-item">
                <h4>∞</h4>
                <p><?= $text['stat_3'] ?></p>
            </div>
        </div>
    </section>

    <section class="benefits-section">
        <div class="container">
            <div class="section-title">
                <h2><?= $text['why_title'] ?></h2>
                <p><?= $text['why_desc'] ?></p>
            </div>
            <div class="benefits-grid">
                <div class="benefit-card">
                    <div class="benefit-icon"><i class="fa-solid fa-clock"></i></div>
                    <h3><?= $text['ben_1_title'] ?></h3>
                    <p><?= $text['ben_1_desc'] ?></p>
                </div>
                <div class="benefit-card">
                    <div class="benefit-icon"><i class="fa-solid fa-chart-line"></i></div>
                    <h3><?= $text['ben_2_title'] ?></h3>
                    <p><?= $text['ben_2_desc'] ?></p>
                </div>
                <div class="benefit-card">
                    <div class="benefit-icon"><i class="fa-solid fa-robot"></i></div>
                    <h3><?= $text['ben_3_title'] ?></h3>
                    <p><?= $text['ben_3_desc'] ?></p>
                </div>
                <div class="benefit-card">
                    <div class="benefit-icon"><i class="fa-solid fa-bullseye"></i></div>
                    <h3><?= $text['ben_4_title'] ?></h3>
                    <p><?= $text['ben_4_desc'] ?></p>
                </div>
                <div class="benefit-card">
                    <div class="benefit-icon"><i class="fa-solid fa-filter"></i></div>
                    <h3><?= $text['ben_5_title'] ?></h3>
                    <p><?= $text['ben_5_desc'] ?></p>
                </div>
                <div class="benefit-card">
                    <div class="benefit-icon"><i class="fa-solid fa-wallet"></i></div>
                    <h3><?= $text['ben_6_title'] ?></h3>
                    <p><?= $text['ben_6_desc'] ?></p>
                </div>
            </div>
        </div>
    </section>

    <section class="video-section">
        <div class="container">
            <div class="section-title">
                <h2><?= $text['vid_title'] ?></h2>
                <p><?= $text['vid_desc'] ?></p>
            </div>
            <div class="video-grid">
                <div class="video-wrapper">
                    <iframe src="https://www.youtube.com/embed/UCxjHLwWO4k?list=PL-GUChjQHCJfX7w730Q4Pd6p6lUrcySTZ" title="Demo Broadcast" allowfullscreen></iframe>
                    <h3><?= $text['vid_1_title'] ?></h3>
                </div>
                <div class="video-wrapper">
                    <iframe src="https://www.youtube.com/embed/fKThXedMshY?list=PL-GUChjQHCJfX7w730Q4Pd6p6lUrcySTZ" title="Demo Filter Nomor" allowfullscreen></iframe>
                    <h3><?= $text['vid_2_title'] ?></h3>
                </div>
            </div>
        </div>
    </section>

    <footer class="container">
        <p>&copy; <?= date('Y'); ?> <?= $text['footer'] ?></p>
    </footer>

    <!-- Mobile Sticky CTA -->
    <div class="mobile-cta">
        <a href="https://github.com/hermawan-dony/botmaster-exe/raw/main/botmaster26.zip" class="btn btn-main">
            <i class="fa-solid fa-cloud-arrow-down"></i> <?= $text['btn_download'] ?>
        </a>
    </div>

    <!-- FOMO Toast Element -->
    <div class="fomo-toast" id="fomo-toast">
        <div class="fomo-icon"><i class="fa-solid fa-check"></i></div>
        <div class="fomo-text">
            <p><span id="fomo-name">Seseorang</span> dari <span id="fomo-city">Jakarta</span></p>
            <p style="font-size: 0.8rem; color: #cbd5e1;"><?= $lang === 'en' ? 'Just bought Lifetime License' : 'Baru saja membeli Lisensi Lifetime' ?></p>
            <small id="fomo-time">2 menit lalu</small>
        </div>
    </div>

    <!-- Script Dynamic Update Github -->
    <script>
        document.addEventListener("DOMContentLoaded", function() {
            const metadataUrl = "https://raw.githubusercontent.com/hermawan-dony/botmaster-exe/main/version.json?t=" + new Date().getTime(); // Prevent cache
            
            fetch(metadataUrl)
                .then(response => response.json())
                .then(data => {
                    // Update main download button
                    const btnDownload = document.getElementById("btn-download");
                    if (btnDownload) {
                        btnDownload.href = data.download_url;
                        btnDownload.innerHTML = `<i class="fa-solid fa-cloud-arrow-down"></i> <?= $lang === 'en' ? 'Download v' : 'Download v' ?>${data.version}`;
                    }

                    // Update mobile sticky CTA download link
                    const mobileBtns = document.querySelectorAll(".mobile-cta .btn-main");
                    mobileBtns.forEach(btn => {
                        btn.href = data.download_url;
                    });

                    document.getElementById("web-version").textContent = `v${data.version}`;
                    document.getElementById("web-release-date").textContent = data.release_date;
                    
                    const changelogList = document.getElementById("web-changelog");
                    if (data.changelog && data.changelog.length > 0) {
                        changelogList.innerHTML = "";
                        data.changelog.forEach(item => {
                            const li = document.createElement("li");
                            li.textContent = item;
                            changelogList.appendChild(li);
                        });
                        document.getElementById("update-box").style.display = "block";
                        document.getElementById("badge-update-status").innerHTML = `<i class="fa-solid fa-rocket"></i> <?= $lang === 'en' ? 'Version' : 'Versi' ?> ${data.version} <?= $lang === 'en' ? 'Available!' : 'Tersedia!' ?>`;
                    }
                })
                .catch(err => console.error("Gagal load metadata github:", err));
        });

        // FOMO Notification Script
        document.addEventListener("DOMContentLoaded", function() {
            const fomoToast = document.getElementById("fomo-toast");
            const fomoName = document.getElementById("fomo-name");
            const fomoCity = document.getElementById("fomo-city");
            const fomoTime = document.getElementById("fomo-time");

            const names = ["Budi", "Andi", "Siti", "Ahmad", "Reza", "Dian", "Agus", "Hendra", "Rina", "Joko"];
            const cities = ["Jakarta", "Surabaya", "Bandung", "Medan", "Semarang", "Makassar", "Bali", "Yogyakarta", "Palembang", "Balikpapan"];
            
            function showFomo() {
                // Randomize data
                const rName = names[Math.floor(Math.random() * names.length)];
                const rCity = cities[Math.floor(Math.random() * cities.length)];
                const rTime = Math.floor(Math.random() * 10) + 1; // 1 to 10 minutes ago
                
                fomoName.textContent = rName;
                fomoCity.textContent = rCity;
                fomoTime.textContent = rTime + (<?= $lang === 'en' ? "' min ago'" : "' menit lalu'" ?>);

                // Show toast
                fomoToast.classList.add("show");

                // Hide after 5 seconds
                setTimeout(() => {
                    fomoToast.classList.remove("show");
                }, 5000);
            }

            // Initial delay before first popup
            setTimeout(() => {
                showFomo();
                // Then repeat randomly between 15 to 30 seconds
                setInterval(() => {
                    showFomo();
                }, Math.floor(Math.random() * 15000) + 15000);
            }, 8000);
        });
    </script>
</body>
</html>
