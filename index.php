<?php
    ini_set('max_execution_time', 600);
    include "datadb.php";

    // Ambil respon dengan sanitasi sederhana
    $respon = filter_input(INPUT_GET, 'respon', FILTER_SANITIZE_SPECIAL_CHARS);

    if (!empty($respon)) {
        echo '<script>alert(' . json_encode($respon) . ');</script>';
    }

    $lang = isset($_GET['lang']) && $_GET['lang'] === 'en' ? 'en' : 'id';
    
    $domain = isset($_SERVER['HTTP_HOST']) ? strtolower($_SERVER['HTTP_HOST']) : '';
    $isWAGW = (strpos($domain, 'wasender.biz') !== false);
    
    $t = [
        'id' => [
            'meta_title' => 'BotMaster-WAGW | Aplikasi Broadcast & Gateway WhatsApp Super Cepat',
            'meta_desc' => 'Rahasia ribuan pebisnis! BotMaster-WAGW adalah software pengirim pesan WhatsApp & mesin Gateway (ODBC Auto-Sync) otomatis, cepat, aman, tanpa biaya bulanan.',
            'client_area' => 'Client Area',
            'badge_status' => 'WAGW Engine Stable & Ready',
            'hero_title_1' => 'Otomatisasi Bisnis & Kirim Pesan ke ',
            'hero_title_span' => 'Ribuan Kontak',
            'hero_title_2' => ' Secara Autopilot 24/7!',
            'hero_desc' => 'Tingkatkan omzet & konversi Anda dengan sistem auto-broadcast dan inovasi terbaru WAGW ODBC Database Auto-Sync (Tarik otomatis dari MySQL/SQL Server). 100% Tanpa langganan bulanan!',
            'feat_1' => 'WAGW ODBC Auto-Sync (NEW!)',
            'feat_2' => 'Premium UI Dark Theme',
            'feat_3' => 'Grab Kontak Grup Instan',
            'feat_4' => 'Kirim Unlimited Pesan',
            'feat_5' => 'Personalisasi Sapaan (Nama)',
            'feat_6' => '100% Bebas Biaya Bulanan',
            'price_title' => 'Lisensi Tahunan',
            'price_unit' => '/ 1 PC / Tahun',
            'btn_download' => 'Download Botmaster-WAGW TERBARU!! v',
            'btn_gen_license' => 'Generate Lisensi Aktif',
            'btn_order' => 'Cek Data Lisensi Kamu',
            'guarantee' => 'Bebas Biaya Update Selama Masa Aktif',
            'chat_telegram' => 'Chat Support via Telegram',
            'chat_whatsapp' => 'Chat Support via WhatsApp',
            'stat_1' => 'Tingkat Keterbacaan (Open Rate)',
            'stat_2' => 'Lebih Cepat & Efisien',
            'stat_3' => 'Tanpa Potongan Biaya Bulanan',
            'why_title' => 'Ubah WhatsApp Anda Menjadi Mesin Pencetak Uang',
            'why_desc' => 'Fakta: 80% pembeli online di Indonesia lebih suka bertransaksi via WhatsApp. Ini adalah tool marketing WAJIB bagi Pemilik Bisnis, Dropshipper, dan Affiliate untuk meraup profit maksimal.',
            'ben_1_title' => '🚀 WAGW ODBC Database Auto-Sync (NEW!)',
            'ben_1_desc' => 'Ubah Botmaster menjadi mesin Gateway 24/7 (Multi-Database Ready)! Sistem berjalan stealth di latar belakang dan membaca tabel outbox Anda via MySQL, SQL Server, atau ODBC. Cukup INSERT SQL, otomatis kirim Teks & Rich Media (Gambar/File)!',
            'ben_2_title' => '💎 Premium Dark Theme UI',
            'ben_2_desc' => 'Mata tidak cepat lelah! Botmaster kini hadir dengan wajah baru Mode Gelap (Navy Blue & Pink) yang sangat elegan dan profesional untuk operasional jangka panjang.',
            'ben_3_title' => '🤖 CS Robot Pintar (Auto Reply)',
            'ben_3_desc' => 'Toko Anda kini buka 24/7. Balas pesan pelanggan seketika berdasarkan kata kunci. Tidak ada lagi calon pembeli yang kabur karena respons lambat.',
            'ben_4_title' => '🎯 Pesan Personal (Anti-Kaku)',
            'ben_4_desc' => 'Sapa pelanggan dengan namanya! Fitur personalisasi ekstrim kami membuat pesan terasa sangat intim, eksklusif, dan jauh dari kesan robot.',
            'ben_5_title' => '🛡️ Filter Nomor & Anti-Banned',
            'ben_5_desc' => 'Jangan buang kuota pesan ke nomor mati! Sistem cerdas kami memverifikasi nomor WA secara live sebelum mengirim, menjaga efektivitas kampanye Anda.',
            'ben_6_title' => '✂️ Pangkas Biaya Promosi Hingga 90%',
            'ben_6_desc' => 'Cukup bayar satu kali untuk lisensi tahunan super murah. Jauh lebih hemat dibandingkan biaya iklan medsos atau sewa platform WA berbayar bulanan.',
            'vid_title' => 'Lihat Kehebatannya Langsung',
            'vid_desc' => 'Tonton demo singkat bagaimana Botmaster-WAGW mengambil alih rutinitas marketing yang membosankan menjadi mesin otomatis.',
            'vid_1_title' => 'Fitur Broadcast Masal',
            'vid_2_title' => 'Fitur Filter Nomor Aktif',
            'footer' => 'Botmaster-WAGW - Solusi WhatsApp Marketing Otomatis Terbaik.',
            'lang_switch_id' => 'Indonesia',
            'lang_switch_en' => 'English'
        ],
        'en' => [
            'meta_title' => 'BotMaster-WAGW | The Ultimate Fast & Safe WhatsApp Broadcaster',
            'meta_desc' => 'The secret weapon of thousands of businesses! BotMaster-WAGW is an automated WhatsApp sender & Gateway (ODBC Auto-Sync) with ZERO monthly fees.',
            'client_area' => 'Client Area',
            'badge_status' => 'WAGW Engine Stable & Ready',
            'hero_title_1' => 'Automate Business & Broadcast to ',
            'hero_title_span' => 'Thousands of Contacts',
            'hero_title_2' => ' 24/7 on Autopilot!',
            'hero_desc' => 'Skyrocket your conversion rates today with the most powerful WhatsApp auto-broadcast system featuring the all-new WAGW ODBC Database Auto-Sync (Connect via MySQL/SQL Server). 100% No monthly subscriptions!',
            'feat_1' => 'WAGW ODBC Auto-Sync (NEW!)',
            'feat_2' => 'Premium UI Dark Theme',
            'feat_3' => 'Instant Group Contact Grabber',
            'feat_4' => 'Send Unlimited Messages',
            'feat_5' => 'Personalized Greetings (Name)',
            'feat_6' => '100% Zero Monthly Fees',
            'price_title' => 'Yearly License',
            'price_unit' => '/ 1 PC / Year',
            'btn_download' => 'Download LATEST Botmaster-WAGW!! v',
            'btn_gen_license' => 'Generate Active License',
            'btn_order' => 'Check Your License Data',
            'guarantee' => 'Free Updates During Active Period',
            'chat_telegram' => 'Chat Support via Telegram',
            'chat_whatsapp' => 'Chat Support via WhatsApp',
            'stat_1' => 'High Open & Read Rate',
            'stat_2' => 'Faster & Highly Efficient',
            'stat_3' => 'Zero Hidden Monthly Fees',
            'why_title' => 'Turn Your WhatsApp Into an ATM',
            'why_desc' => 'Fact: 80% of online buyers prefer transacting via WhatsApp. This is a MUST-HAVE marketing tool for Business Owners, Dropshippers, and Affiliates to maximize profit.',
            'ben_1_title' => '🚀 WAGW ODBC Database Auto-Sync (NEW!)',
            'ben_1_desc' => 'Turn Botmaster into a 24/7 Headless Gateway machine! Multi-Database Ready (MySQL, SQL Server, ODBC). Just INSERT into the Outbox table, the system will natively dispatch Text and Rich Media (Images, PDFs) seamlessly!',
            'ben_2_title' => '💎 Premium Dark Theme UI',
            'ben_2_desc' => 'Say goodbye to eye strain! Botmaster now comes with a brand-new, highly elegant Dark Mode (Navy Blue & Pink) designed for professional, long-term operations.',
            'ben_3_title' => '🤖 Smart CS Robot (Auto Reply)',
            'ben_3_desc' => 'Your store is now open 24/7. Reply to customer messages instantly based on keywords. No more losing buyers due to slow responses.',
            'ben_4_title' => '🎯 Hyper-Personalized Messaging',
            'ben_4_desc' => 'Greet customers by their names! Our extreme personalization feature makes messages feel highly intimate, exclusive, and human.',
            'ben_5_title' => '🛡️ Smart Filter & Anti-Banned',
            'ben_5_desc' => 'Don\'t waste messages on dead numbers! Our smart engine live-verifies WA numbers before sending, keeping your campaigns effective and safe.',
            'ben_6_title' => '✂️ Slash Marketing Costs by 90%',
            'ben_6_desc' => 'Pay only once for an incredibly affordable yearly license. Much cheaper than social media ads or expensive monthly WA platforms.',
            'vid_title' => 'See the Magic In Action',
            'vid_desc' => 'Watch a short demo of how Botmaster-WAGW takes over your boring marketing routines and turns them into an automated machine.',
            'vid_1_title' => 'Mass Broadcast Feature',
            'vid_2_title' => 'Active Number Filter Feature',
            'footer' => 'Botmaster-WAGW - The Ultimate Automated WhatsApp Marketing Solution.',
            'lang_switch_id' => 'Indonesia',
            'lang_switch_en' => 'English'
        ]
    ];

    if (!$isWAGW) {
        $t['id']['meta_title'] = 'BotMaster | Aplikasi Broadcast WhatsApp Super Cepat';
        $t['id']['meta_desc'] = 'Rahasia ribuan pebisnis! BotMaster adalah software pengirim pesan WhatsApp otomatis, cepat, aman, tanpa biaya bulanan.';
        $t['id']['badge_status'] = 'BotMaster Stable & Ready';
        $t['id']['hero_desc'] = 'Tingkatkan omzet & konversi Anda dengan sistem auto-broadcast. 100% Tanpa langganan bulanan!';
        $t['id']['feat_1'] = 'Kirim Pesan Tanpa Batas';
        $t['id']['btn_download'] = 'Download Botmaster TERBARU!! v';
        $t['id']['ben_1_title'] = '🚀 Kirim Unlimited Pesan';
        $t['id']['ben_1_desc'] = 'Kirim pesan ke ribuan kontak Anda tanpa batas. Bebas biaya bulanan!';
        $t['id']['vid_desc'] = 'Tonton demo singkat bagaimana Botmaster mengambil alih rutinitas marketing yang membosankan menjadi mesin otomatis.';
        $t['id']['footer'] = 'Botmaster - Solusi WhatsApp Marketing Otomatis Terbaik.';

        $t['en']['meta_title'] = 'BotMaster | The Ultimate Fast & Safe WhatsApp Broadcaster';
        $t['en']['meta_desc'] = 'The secret weapon of thousands of businesses! BotMaster is an automated WhatsApp sender with ZERO monthly fees.';
        $t['en']['badge_status'] = 'BotMaster Stable & Ready';
        $t['en']['hero_desc'] = 'Skyrocket your conversion rates today with the most powerful WhatsApp auto-broadcast system. 100% No monthly subscriptions!';
        $t['en']['feat_1'] = 'Send Unlimited Messages';
        $t['en']['btn_download'] = 'Download LATEST Botmaster!! v';
        $t['en']['ben_1_title'] = '🚀 Send Unlimited Messages';
        $t['en']['ben_1_desc'] = 'Broadcast to thousands of contacts with zero monthly limits!';
        $t['en']['vid_desc'] = 'Watch a short demo of how Botmaster takes over your boring marketing routines and turns them into an automated machine.';
        $t['en']['footer'] = 'Botmaster - The Ultimate Automated WhatsApp Marketing Solution.';
    }
    $text = $t[$lang];
?>
<!DOCTYPE html>
<html lang="<?= $lang ?>" class="scroll-smooth">
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
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css">

    <!-- Tailwind CSS -->
    <script src="https://cdn.tailwindcss.com"></script>
    <script>
        tailwind.config = {
            theme: {
                extend: {
                    fontFamily: {
                        sans: ['Plus Jakarta Sans', 'sans-serif'],
                        display: ['Space Grotesk', 'sans-serif'],
                    },
                    colors: {
                        brand: {
                            purple: '#7C3AED',
                            purpleHover: '#6D28D9',
                            dark: '#0B1120',
                            card: 'rgba(30, 41, 59, 0.7)'
                        }
                    },
                    animation: {
                        'blob': 'blob 15s infinite alternate',
                        'float': 'float 6s ease-in-out infinite',
                        'pulse-glow': 'pulseGlow 2s cubic-bezier(0.4, 0, 0.6, 1) infinite',
                    },
                    keyframes: {
                        blob: {
                            '0%': { transform: 'translate(0px, 0px) scale(1)' },
                            '33%': { transform: 'translate(30px, -50px) scale(1.1)' },
                            '66%': { transform: 'translate(-20px, 20px) scale(0.9)' },
                            '100%': { transform: 'translate(0px, 0px) scale(1)' },
                        },
                        float: {
                            '0%, 100%': { transform: 'translateY(0)' },
                            '50%': { transform: 'translateY(-10px)' },
                        },
                        pulseGlow: {
                            '0%, 100%': { opacity: 1 },
                            '50%': { opacity: .5 },
                        }
                    }
                }
            }
        }
    </script>

    <style>
        @keyframes marquee {
            0% { transform: translateX(0%); }
            100% { transform: translateX(-100%); }
        }
        @keyframes marquee2 {
            0% { transform: translateX(0%); }
            100% { transform: translateX(-100%); }
        }
        .animate-marquee { animation: marquee 25s linear infinite; }
        .animate-marquee2 { animation: marquee2 25s linear infinite; }
        .background-animate {
            background-size: 200% 200%;
            animation: bgShimmer 3s linear infinite;
        }
        @keyframes bgShimmer {
            0% { background-position: 200% 0; }
            100% { background-position: -200% 0; }
        }
        .glass-card {
            background: rgba(30, 41, 59, 0.7);
            backdrop-filter: blur(12px);
            -webkit-backdrop-filter: blur(12px);
            border: 1px solid rgba(255, 255, 255, 0.08);
        }
        
        .fomo-toast {
            transform: translateY(150%);
            opacity: 0;
            transition: all 0.5s cubic-bezier(0.16, 1, 0.3, 1);
            pointer-events: none;
        }
        .fomo-toast.show {
            transform: translateY(0);
            opacity: 1;
        }
    </style>
</head>
<body class="bg-brand-dark text-slate-100 font-sans antialiased overflow-x-hidden selection:bg-fuchsia-600 selection:text-white relative">

    <!-- Background Animated Blobs -->
    <div class="fixed inset-0 z-[-1] overflow-hidden pointer-events-none">
        <div class="absolute top-[-10%] left-[-10%] w-[40%] h-[40%] rounded-full bg-violet-600/20 blur-[100px] animate-blob"></div>
        <div class="absolute top-[20%] right-[-10%] w-[30%] h-[50%] rounded-full bg-cyan-600/10 blur-[100px] animate-blob" style="animation-delay: 2s;"></div>
        <div class="absolute bottom-[-10%] left-[20%] w-[50%] h-[50%] rounded-full bg-blue-600/10 blur-[120px] animate-blob" style="animation-delay: 4s;"></div>
    </div>

    <!-- Grid Overlay -->
    <div class="fixed inset-0 z-0 pointer-events-none opacity-[0.03]" style="background-image: linear-gradient(to right, #ffffff 1px, transparent 1px), linear-gradient(to bottom, #ffffff 1px, transparent 1px); background-size: 24px 24px;"></div>

    <!-- Navbar -->
    <nav class="fixed w-full z-50 transition-all duration-300 bg-slate-900/80 backdrop-blur-md border-b border-white/5">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div class="flex items-center justify-between h-20">
                <div class="flex-shrink-0 flex items-center gap-3">
                    <img src="https://app.wasender.biz/index_files/botmaster.ico" alt="BotMaster Logo" class="w-10 h-10 rounded-xl shadow-lg shadow-violet-500/30">
                    <span class="font-display font-bold text-2xl tracking-tight text-white">Botmaster<?= $isWAGW ? '<span class="text-fuchsia-500"> ++WAGW</span>' : '' ?></span>
                </div>
                <div class="flex items-center gap-4">
                    <div class="flex bg-slate-800/80 rounded-lg p-1 border border-white/10">
                        <a href="?lang=id" class="px-3 py-1 rounded-md text-sm font-medium transition-colors <?= $lang === 'id' ? 'bg-cyan-400 text-slate-900 shadow-md font-bold' : 'text-slate-400 hover:text-white' ?>">ID</a>
                        <a href="?lang=en" class="px-3 py-1 rounded-md text-sm font-medium transition-colors <?= $lang === 'en' ? 'bg-cyan-400 text-slate-900 shadow-md font-bold' : 'text-slate-400 hover:text-white' ?>">EN</a>
                    </div>
                </div>
            </div>
        </div>
    </nav>

    <!-- Hero Section -->
    <main class="pt-32 pb-16 sm:pt-40 sm:pb-24 lg:pb-32 overflow-hidden">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 relative">
            <div class="lg:grid lg:grid-cols-12 lg:gap-16 items-center">
                
                <!-- Left Content -->
                <div class="lg:col-span-6 text-center lg:text-left mb-16 lg:mb-0 relative z-10">
                    <div class="inline-flex items-center gap-2 px-4 py-2 rounded-full bg-violet-500/10 border border-violet-500/20 text-violet-300 font-medium text-sm mb-8 animate-float">
                        <span class="flex w-2 h-2 rounded-full bg-green-400 animate-pulse-glow"></span>
                        <?= $text['badge_status'] ?>
                    </div>
                    
                    <h1 class="font-display text-5xl sm:text-6xl lg:text-7xl font-bold tracking-tight mb-6 leading-[1.1]">
                        <?= $text['hero_title_1'] ?>
                        <span class="block text-transparent bg-clip-text bg-gradient-to-r from-violet-400 via-cyan-400 to-cyan-200 pb-2">
                            <?= $text['hero_title_span'] ?>
                        </span>
                        <?= $text['hero_title_2'] ?>
                    </h1>
                    
                    <p class="text-lg sm:text-xl text-slate-400 mb-10 max-w-2xl mx-auto lg:mx-0 leading-relaxed">
                        <?= $text['hero_desc'] ?>
                    </p>

                    <div class="grid grid-cols-2 gap-4 max-w-xl mx-auto lg:mx-0">
                        <div class="flex items-center gap-3 p-4 rounded-xl glass-card hover:bg-slate-800/80 transition-colors">
                            <i class="fa-solid fa-mobile-screen-button text-violet-400 text-xl"></i>
                            <span class="text-sm font-medium text-slate-200"><?= $text['feat_1'] ?></span>
                        </div>
                        <div class="flex items-center gap-3 p-4 rounded-xl glass-card hover:bg-slate-800/80 transition-colors">
                            <i class="fa-solid fa-infinity text-fuchsia-400 text-xl"></i>
                            <span class="text-sm font-medium text-slate-200"><?= $text['feat_2'] ?></span>
                        </div>
                        <div class="flex items-center gap-3 p-4 rounded-xl glass-card hover:bg-slate-800/80 transition-colors">
                            <i class="fa-solid fa-users text-blue-400 text-xl"></i>
                            <span class="text-sm font-medium text-slate-200"><?= $text['feat_3'] ?></span>
                        </div>
                        <div class="flex items-center gap-3 p-4 rounded-xl glass-card hover:bg-slate-800/80 transition-colors">
                            <i class="fa-solid fa-file-import text-pink-400 text-xl"></i>
                            <span class="text-sm font-medium text-slate-200"><?= $text['feat_4'] ?></span>
                        </div>
                        <div class="flex items-center gap-3 p-4 rounded-xl glass-card hover:bg-slate-800/80 transition-colors">
                            <i class="fa-solid fa-user-tag text-amber-400 text-xl"></i>
                            <span class="text-sm font-medium text-slate-200"><?= $text['feat_5'] ?></span>
                        </div>
                        <div class="flex items-center gap-3 p-4 rounded-xl glass-card hover:bg-slate-800/80 transition-colors">
                            <i class="fa-solid fa-coins text-emerald-400 text-xl"></i>
                            <span class="text-sm font-medium text-slate-200"><?= $text['feat_6'] ?></span>
                        </div>
                    </div>
                </div>

                <!-- Right Content: Pricing Card -->
                <div class="lg:col-span-6 lg:ml-auto w-full max-w-lg mx-auto relative z-10">
                    <div class="absolute -inset-1 bg-gradient-to-r from-violet-500 to-fuchsia-500 rounded-2xl blur opacity-30 animate-pulse-glow"></div>
                    <!-- Shimmer Wrapper -->
                    <div class="relative p-[1px] rounded-2xl bg-gradient-to-r from-transparent via-violet-500/50 to-transparent background-animate">
                    <div class="relative glass-card rounded-2xl p-8 sm:p-10 border-t border-l border-white/20 h-full w-full">
                        <div class="text-sm font-bold tracking-widest text-fuchsia-400 uppercase mb-4"><?= $text['price_title'] ?></div>
                        
                        <?php if ($isWAGW): ?>
                            <div class="mb-8">
                                <div class="flex items-baseline gap-2">
                                    <span class="text-3xl font-display font-bold text-white tracking-tight">Mulai dari Rp 10.000</span>
                                    <span class="text-slate-400 font-medium">/ bulan</span>
                                </div>
                                <div class="text-slate-400 font-medium mt-1">atau Rp 99.000 / tahun</div>
                            </div>
                        <?php else: ?>
                            <div class="flex items-baseline gap-2 mb-8">
                                <?php
                                    $display_price = '0';
                                    $show_rp = true;
                                    if (isset($harga1)) {
                                        $clean_price = trim($harga1);
                                        if (!is_numeric(str_replace(['.', ','], '', $clean_price))) {
                                            $display_price = trim(str_ireplace(['Rp.', 'Rp'], '', $clean_price));
                                            $show_rp = false;
                                        } else {
                                            $numeric_val = (float)str_replace(['.', ','], '', $clean_price);
                                            $display_price = number_format($numeric_val, 0, ',', '.');
                                            $show_rp = true;
                                        }
                                    }
                                ?>
                                <span class="text-5xl font-display font-bold text-white tracking-tight"><?= ($show_rp ? 'Rp ' : '') . htmlspecialchars($display_price, ENT_QUOTES, 'UTF-8') ?></span>
                                <span class="text-slate-400 font-medium"><?= $text['price_unit'] ?></span>
                            </div>
                        <?php endif; ?>

                        <!-- Update Box -->
                        <div id="update-box" class="hidden bg-slate-900/60 rounded-xl p-5 mb-8 border border-white/5">
                            <div class="flex justify-between items-center mb-4 border-b border-white/5 pb-3">
                                <span id="web-version" class="bg-gradient-to-r from-violet-600 to-fuchsia-600 text-white text-xs font-bold px-3 py-1 rounded-md shadow-lg shadow-violet-500/20">v--</span>
                                <span id="web-release-date" class="text-xs text-slate-400 font-medium">--</span>
                            </div>
                            <ul id="web-changelog" class="text-sm text-slate-300 space-y-2">
                                <!-- Changelog items injected via JS -->
                            </ul>
                        </div>

                        <div class="space-y-4">
                            <a href="https://github.com/hermawan-dony/botmaster-exe/raw/main/botmaster26.zip" id="btn-download" class="group relative flex items-center justify-center gap-3 w-full bg-gradient-to-r from-violet-600 to-fuchsia-600 hover:from-violet-500 hover:to-fuchsia-500 text-white font-bold text-lg py-4 px-8 rounded-xl shadow-lg shadow-violet-500/30 transition-all hover:-translate-y-1 overflow-hidden">
                                <div class="absolute inset-0 bg-white/20 translate-x-[-100%] group-hover:translate-x-[100%] transition-transform duration-700 skew-x-[-20deg]"></div>
                                <i class="fa-solid fa-cloud-arrow-down"></i> 
                                <span><?= $text['btn_download'] ?>26</span>
                            </a>
                            
                            <a href="./request" class="flex items-center justify-center gap-3 w-full bg-slate-800 hover:bg-slate-700 text-amber-400 font-semibold text-lg py-4 px-8 rounded-xl border border-white/5 transition-colors shadow-inner">
                                <i class="fa-solid fa-key"></i> <?= $text['btn_gen_license'] ?>
                            </a>
                            
                            <a href="./order" class="flex items-center justify-center gap-3 w-full bg-slate-800 hover:bg-slate-700 text-slate-200 font-semibold text-lg py-4 px-8 rounded-xl border border-white/5 transition-colors">
                                <i class="fa-solid fa-database"></i> <?= $text['btn_order'] ?>
                            </a>
                        </div>

                        <div class="mt-8 pt-6 border-t border-white/10 text-center space-y-3">
                            <div class="flex items-center justify-center gap-2 text-sm text-slate-300">
                                <i class="fa-solid fa-shield-halved text-amber-400"></i>
                                <?= $text['guarantee'] ?>
                            </div>
                            <div class="flex items-center justify-center gap-4">
                                <?php if (!empty($pembayaran)) : ?>
                                    <?php if (!empty($url_reseller) && $url_reseller === "http://app.wasender.biz") : ?>
                                        <a href="https://t.me/wawn1782" target="_blank" class="text-sm text-sky-400 hover:text-sky-300 flex items-center gap-2 transition-colors">
                                            <i class="fa-brands fa-telegram"></i> <?= $text['chat_telegram'] ?>
                                        </a>
                                    <?php else : ?>
                                        <?php
                                        $wa_clean = isset($wa_reseller) ? preg_replace('/[^0-9]/', '', $wa_reseller) : '';
                                        $tpl_text = rawurlencode("Hallo kak, saya mau konsultasi soal Aplikasi Botmaster.");
                                        ?>
                                        <a href="https://api.whatsapp.com/send?phone=<?= $wa_clean; ?>&text=<?= $tpl_text; ?>" target="_blank" class="text-sm text-emerald-400 hover:text-emerald-300 flex items-center gap-2 transition-colors">
                                            <i class="fa-brands fa-whatsapp"></i> <?= $text['chat_whatsapp'] ?>
                                        </a>
                                    <?php endif; ?>
                                <?php endif; ?>
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
            </div>
        </div>
    </main>

    <!-- Social Proof Marquee -->
    <section class="py-10 border-y border-white/5 bg-slate-900/50 relative z-10 overflow-hidden">
        <div class="text-center mb-6">
            <span class="text-sm font-bold tracking-widest text-slate-500 uppercase">Dipercaya oleh 5.000+ Pebisnis & UMKM di Indonesia</span>
        </div>
        <div class="relative flex overflow-x-hidden">
            <div class="animate-marquee whitespace-nowrap flex items-center gap-16 opacity-40">
                <span class="text-xl font-display font-bold"><i class="fa-solid fa-building"></i> TechCorp</span>
                <span class="text-xl font-display font-bold"><i class="fa-solid fa-shop"></i> Storeedia</span>
                <span class="text-xl font-display font-bold"><i class="fa-solid fa-store"></i> UMKM Maju</span>
                <span class="text-xl font-display font-bold"><i class="fa-solid fa-industry"></i> IndoGrosir</span>
                <span class="text-xl font-display font-bold"><i class="fa-solid fa-cart-shopping"></i> RetailPro</span>
                <span class="text-xl font-display font-bold"><i class="fa-solid fa-basket-shopping"></i> PasarOnline</span>
            </div>
            <div class="absolute top-0 animate-marquee2 whitespace-nowrap flex items-center gap-16 opacity-40" style="left: 100%;">
                <span class="text-xl font-display font-bold"><i class="fa-solid fa-building"></i> TechCorp</span>
                <span class="text-xl font-display font-bold"><i class="fa-solid fa-shop"></i> Storeedia</span>
                <span class="text-xl font-display font-bold"><i class="fa-solid fa-store"></i> UMKM Maju</span>
                <span class="text-xl font-display font-bold"><i class="fa-solid fa-industry"></i> IndoGrosir</span>
                <span class="text-xl font-display font-bold"><i class="fa-solid fa-cart-shopping"></i> RetailPro</span>
                <span class="text-xl font-display font-bold"><i class="fa-solid fa-basket-shopping"></i> PasarOnline</span>
            </div>
        </div>
    </section>

    <!-- Content Sections -->
    <section class="border-y border-white/5 bg-slate-900/50 relative z-10">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-16">
            <div class="grid grid-cols-1 md:grid-cols-3 gap-8 text-center divide-y md:divide-y-0 md:divide-x divide-white/5">
                <div class="p-4">
                    <h4 class="font-display text-4xl sm:text-5xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-cyan-400 to-cyan-200 mb-2 drop-shadow-sm">99%</h4>
                    <p class="text-slate-400 font-medium"><?= $text['stat_1'] ?></p>
                </div>
                <div class="p-4 pt-8 md:pt-4">
                    <h4 class="font-display text-4xl sm:text-5xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-cyan-400 to-cyan-200 mb-2 drop-shadow-sm">10x</h4>
                    <p class="text-slate-400 font-medium"><?= $text['stat_2'] ?></p>
                </div>
                <div class="p-4 pt-8 md:pt-4">
                    <h4 class="font-display text-4xl sm:text-5xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-cyan-400 to-cyan-200 mb-2 drop-shadow-sm">∞</h4>
                    <p class="text-slate-400 font-medium"><?= $text['stat_3'] ?></p>
                </div>
            </div>
        </div>
    </section>

    <!-- Features / Benefits Section -->
    <section class="py-24 relative z-10">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div class="text-center max-w-3xl mx-auto mb-16">
                <h2 class="font-display text-3xl sm:text-4xl font-bold text-white mb-4"><?= $text['why_title'] ?></h2>
                <p class="text-lg text-slate-400"><?= $text['why_desc'] ?></p>
            </div>
            
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                <!-- Benefit 1 -->
                <div class="glass-card rounded-2xl p-8 hover:-translate-y-2 transition-all duration-300 group hover:shadow-[0_0_30px_rgba(139,92,246,0.3)] hover:border-violet-500/50">
                    <div class="w-12 h-12 rounded-xl bg-violet-500/10 border border-violet-500/20 flex items-center justify-center mb-6 group-hover:scale-110 transition-transform">
                        <i class="fa-solid fa-bolt text-2xl text-violet-400"></i>
                    </div>
                    <h3 class="text-xl font-bold text-white mb-3"><?= $text['ben_1_title'] ?></h3>
                    <p class="text-slate-400 text-sm leading-relaxed"><?= $text['ben_1_desc'] ?></p>
                </div>
                <!-- Benefit 2 -->
                <div class="glass-card rounded-2xl p-8 hover:-translate-y-2 transition-all duration-300 group hover:shadow-[0_0_30px_rgba(139,92,246,0.3)] hover:border-violet-500/50">
                    <div class="w-12 h-12 rounded-xl bg-fuchsia-600/10 border border-cyan-500/20 flex items-center justify-center mb-6 group-hover:scale-110 transition-transform">
                        <i class="fa-solid fa-chart-line text-2xl text-fuchsia-400"></i>
                    </div>
                    <h3 class="text-xl font-bold text-white mb-3"><?= $text['ben_2_title'] ?></h3>
                    <p class="text-slate-400 text-sm leading-relaxed"><?= $text['ben_2_desc'] ?></p>
                </div>
                <!-- Benefit 3 -->
                <div class="glass-card rounded-2xl p-8 hover:-translate-y-2 transition-all duration-300 group hover:shadow-[0_0_30px_rgba(139,92,246,0.3)] hover:border-violet-500/50">
                    <div class="w-12 h-12 rounded-xl bg-blue-500/10 border border-blue-500/20 flex items-center justify-center mb-6 group-hover:scale-110 transition-transform">
                        <i class="fa-solid fa-robot text-2xl text-blue-400"></i>
                    </div>
                    <h3 class="text-xl font-bold text-white mb-3"><?= $text['ben_3_title'] ?></h3>
                    <p class="text-slate-400 text-sm leading-relaxed"><?= $text['ben_3_desc'] ?></p>
                </div>
                <!-- Benefit 4 -->
                <div class="glass-card rounded-2xl p-8 hover:-translate-y-2 transition-all duration-300 group hover:shadow-[0_0_30px_rgba(139,92,246,0.3)] hover:border-violet-500/50">
                    <div class="w-12 h-12 rounded-xl bg-pink-500/10 border border-pink-500/20 flex items-center justify-center mb-6 group-hover:scale-110 transition-transform">
                        <i class="fa-solid fa-bullseye text-2xl text-pink-400"></i>
                    </div>
                    <h3 class="text-xl font-bold text-white mb-3"><?= $text['ben_4_title'] ?></h3>
                    <p class="text-slate-400 text-sm leading-relaxed"><?= $text['ben_4_desc'] ?></p>
                </div>
                <!-- Benefit 5 -->
                <div class="glass-card rounded-2xl p-8 hover:-translate-y-2 transition-all duration-300 group hover:shadow-[0_0_30px_rgba(139,92,246,0.3)] hover:border-violet-500/50">
                    <div class="w-12 h-12 rounded-xl bg-amber-500/10 border border-amber-500/20 flex items-center justify-center mb-6 group-hover:scale-110 transition-transform">
                        <i class="fa-solid fa-filter text-2xl text-amber-400"></i>
                    </div>
                    <h3 class="text-xl font-bold text-white mb-3"><?= $text['ben_5_title'] ?></h3>
                    <p class="text-slate-400 text-sm leading-relaxed"><?= $text['ben_5_desc'] ?></p>
                </div>
                <!-- Benefit 6 -->
                <div class="glass-card rounded-2xl p-8 hover:-translate-y-2 transition-all duration-300 group hover:shadow-[0_0_30px_rgba(139,92,246,0.3)] hover:border-violet-500/50">
                    <div class="w-12 h-12 rounded-xl bg-emerald-500/10 border border-emerald-500/20 flex items-center justify-center mb-6 group-hover:scale-110 transition-transform">
                        <i class="fa-solid fa-money-bill-wave text-2xl text-emerald-400"></i>
                    </div>
                    <h3 class="text-xl font-bold text-white mb-3"><?= $text['ben_6_title'] ?></h3>
                    <p class="text-slate-400 text-sm leading-relaxed"><?= $text['ben_6_desc'] ?></p>
                </div>
            </div>
        </div>
    </section>

    <!-- Video Section -->
    <section class="py-24 relative z-10 border-t border-white/5 bg-slate-900/30">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div class="text-center max-w-3xl mx-auto mb-16">
                <h2 class="font-display text-3xl sm:text-4xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-cyan-400 to-cyan-200 mb-4"><?= $text['vid_title'] ?></h2>
                <p class="text-lg text-slate-400"><?= $text['vid_desc'] ?></p>
            </div>
            <div class="grid grid-cols-1 md:grid-cols-2 gap-10 max-w-5xl mx-auto">
                <div class="glass-card p-4 rounded-2xl group hover:-translate-y-2 transition-transform duration-300 border-white/10 shadow-2xl">
                    <div class="relative w-full aspect-video rounded-xl overflow-hidden border border-white/5">
                        <iframe class="w-full h-full" src="https://www.youtube.com/embed/fKThXedMshY?list=PL-GUChjQHCJfX7w730Q4Pd6p6lUrcySTZ" title="Demo Broadcast" allowfullscreen></iframe>
                    </div>
                    <h3 class="text-center font-bold text-lg text-white mt-4"><?= $text['vid_1_title'] ?></h3>
                </div>
                <div class="glass-card p-4 rounded-2xl group hover:-translate-y-2 transition-transform duration-300 border-white/10 shadow-2xl">
                    <div class="relative w-full aspect-video rounded-xl overflow-hidden border border-white/5">
                        <iframe class="w-full h-full" src="https://www.youtube.com/embed/fKThXedMshY?list=PL-GUChjQHCJfX7w730Q4Pd6p6lUrcySTZ" title="Demo Filter Nomor" allowfullscreen></iframe>
                    </div>
                    <h3 class="text-center font-bold text-lg text-white mt-4"><?= $text['vid_2_title'] ?></h3>
                </div>
            </div>
        </div>
    </section>

    <!-- Footer -->
    <footer class="border-t border-white/5 bg-slate-900/80 mt-12 py-8 relative z-10">
        <div class="max-w-7xl mx-auto px-4 text-center text-slate-500 text-sm">
            <p>&copy; <?= date('Y'); ?> <?= $text['footer'] ?></p>
        </div>
    </footer>

    <!-- Mobile Sticky CTA -->
    <div class="lg:hidden fixed bottom-0 left-0 w-full glass-card border-t border-white/10 p-4 z-40 shadow-[0_-10px_20px_rgba(0,0,0,0.5)]">
        <a href="https://github.com/hermawan-dony/botmaster-exe/raw/main/botmaster26.zip" class="mobile-btn-download flex items-center justify-center gap-2 w-full bg-gradient-to-r from-violet-600 to-fuchsia-600 text-white font-bold py-3 rounded-xl shadow-lg shadow-violet-500/30">
            <i class="fa-solid fa-cloud-arrow-down"></i> <?= $text['btn_download'] ?>26
        </a>
    </div>

    <!-- FOMO Toast Element -->
    <div class="fomo-toast fixed bottom-24 lg:bottom-8 left-4 lg:left-8 glass-card p-4 rounded-2xl flex items-center gap-4 z-50 shadow-2xl shadow-black max-w-sm" id="fomo-toast">
        <div class="w-10 h-10 shrink-0 rounded-full bg-emerald-500/20 flex items-center justify-center">
            <i class="fa-solid fa-check text-emerald-400"></i>
        </div>
        <div>
            <p class="text-sm text-slate-200 m-0 leading-tight">
                <span id="fomo-name" class="font-bold text-fuchsia-400">Seseorang</span> dari <span id="fomo-city" class="font-bold text-white">Jakarta</span>
            </p>
            <p class="text-[0.75rem] text-slate-400 mt-1 mb-0"><?= $lang === 'en' ? 'Just bought Yearly License' : 'Baru saja membeli Lisensi Tahunan' ?></p>
            <p class="text-[0.65rem] text-slate-500 mt-1" id="fomo-time">2 menit lalu</p>
        </div>
    </div>

    <!-- Script Dynamic Update Github & FOMO -->
    <script>
        document.addEventListener("DOMContentLoaded", function() {
            // Github Fetcher
            const metadataUrl = "https://raw.githubusercontent.com/hermawan-dony/botmaster-exe/main/version.json?t=" + new Date().getTime();
            
            fetch(metadataUrl)
                .then(response => response.json())
                .then(data => {
                    const btnDownload = document.getElementById("btn-download");
                    if (btnDownload) {
                        btnDownload.href = data.download_url;
                        btnDownload.innerHTML = `<div class="absolute inset-0 bg-white/20 translate-x-[-100%] group-hover:translate-x-[100%] transition-transform duration-700 skew-x-[-20deg]"></div><i class="fa-solid fa-cloud-arrow-down"></i> <span><?= $text['btn_download'] ?>${data.version}</span>`;
                    }

                    const mobileBtns = document.querySelectorAll(".mobile-btn-download");
                    mobileBtns.forEach(btn => {
                        btn.href = data.download_url;
                        btn.innerHTML = `<i class="fa-solid fa-cloud-arrow-down"></i> <?= $text['btn_download'] ?>${data.version}`;
                    });

                    document.getElementById("web-version").textContent = `v${data.version}`;
                    document.getElementById("web-release-date").textContent = data.release_date;
                    
                    const changelogList = document.getElementById("web-changelog");
                    if (data.changelog && data.changelog.length > 0) {
                        changelogList.innerHTML = "";
                        data.changelog.forEach(item => {
                            const li = document.createElement("li");
                            li.className = "relative pl-4 before:content-['→'] before:absolute before:left-0 before:text-fuchsia-500 before:font-bold";
                            li.textContent = item;
                            changelogList.appendChild(li);
                        });
                        const ub = document.getElementById("update-box");
                        ub.classList.remove("hidden");
                        ub.style.display = "block";
                    }
                })
                .catch(err => console.error("Gagal load metadata github:", err));

            // FOMO Notification Script
            const fomoToast = document.getElementById("fomo-toast");
            const fomoName = document.getElementById("fomo-name");
            const fomoCity = document.getElementById("fomo-city");
            const fomoTime = document.getElementById("fomo-time");

            const names = ["Budi", "Andi", "Siti", "Ahmad", "Reza", "Dian", "Agus", "Hendra", "Rina", "Joko"];
            const cities = ["Jakarta", "Surabaya", "Bandung", "Medan", "Semarang", "Makassar", "Bali", "Yogyakarta", "Palembang", "Balikpapan"];
            
            function showFomo() {
                const rName = names[Math.floor(Math.random() * names.length)];
                const rCity = cities[Math.floor(Math.random() * cities.length)];
                const rTime = Math.floor(Math.random() * 10) + 1;
                
                fomoName.textContent = rName;
                fomoCity.textContent = rCity;
                fomoTime.textContent = rTime + (<?= $lang === 'en' ? "' min ago'" : "' menit lalu'" ?>);

                fomoToast.classList.add("show");

                setTimeout(() => {
                    fomoToast.classList.remove("show");
                }, 5000);
            }

            setTimeout(() => {
                showFomo();
                setInterval(() => {
                    showFomo();
                }, Math.floor(Math.random() * 15000) + 15000);
            }, 8000);
        });
    </script>
</body>
</html>
