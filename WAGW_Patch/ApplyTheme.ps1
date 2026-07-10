$files = Get-ChildItem -Path "BOTMASTER26_SC\Botmaster" -Filter "*.Designer.vb" -Recurse
foreach ($file in $files) {
    $content = Get-Content $file.FullName
    $content = $content -replace "CType\(CType\(99, Byte\), Integer\), CType\(CType\(46, Byte\), Integer\), CType\(CType\(161, Byte\), Integer\)", "CType(CType(10, Byte), Integer), CType(CType(11, Byte), Integer), CType(CType(23, Byte), Integer)"
    $content = $content -replace "CType\(CType\(133, Byte\), Integer\), CType\(CType\(76, Byte\), Integer\), CType\(CType\(199, Byte\), Integer\)", "CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(48, Byte), Integer)"
    $content = $content -replace "CType\(CType\(79, Byte\), Integer\), CType\(CType\(31, Byte\), Integer\), CType\(CType\(133, Byte\), Integer\)", "CType(CType(15, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(33, Byte), Integer)"
    $content = $content -replace "CType\(CType\(30, Byte\), Integer\), CType\(CType\(190, Byte\), Integer\), CType\(CType\(165, Byte\), Integer\)", "CType(CType(217, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(211, Byte), Integer)"
    Set-Content -Path $file.FullName -Value $content
}

$moduleConfigFile = "BOTMASTER26_SC\Botmaster\ModuleConfig.vb"
if (Test-Path $moduleConfigFile) {
    $content = Get-Content $moduleConfigFile
    $content = $content -replace "79, 31, 133", "15, 17, 33"
    $content = $content -replace "99, 46, 161", "10, 11, 23"
    $content = $content -replace "133, 76, 199", "25, 27, 48"
    $content = $content -replace "177, 121, 241", "35, 37, 65"
    $content = $content -replace "30, 190, 165", "0, 230, 230"
    Set-Content -Path $moduleConfigFile -Value $content
}

Write-Output "Theme Colors Replaced with Dark Blue and Pink WAGW Theme"
