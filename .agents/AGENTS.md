# Botmaster Project Rules

Whenever updating or modifying this project, please follow these rules:

1. **License Domain**: Ensure the license validation URL uses `wasender.biz`. The exact URL is:
   `https://panel.wasender.biz/api/ceklisensi.php`
2. **Empty Contact Info**: Always clear out all developer contact information (WhatsApp, Email, Website support) by setting them to empty strings `""` in both launcher configuration files:
   - `Launcher\FrmLauncher.vb`
   - `Botmaster\Customization\FrmLauncher.vb`
3. **License Form Errors**: In `FrmLicense.vb`, ensure `ValidateButton_Click` uses the dynamic error messages from the server response (`result.ErrorDescription`). The title for `MsgBox` must be set as:
   - `"BOTMASTER WASENDER"` (for general checks/errors)
   - `"BOTMASTER WASENDER - LICENSE DISABLED"` (when license is disabled)
   - `"BOTMASTER WASENDER - LICENSE EXPIRED"` (when license is expired)
4. **Compilation**: Always compile/build the project using Visual Studio 2022 (MSBuild 17.x).
5. **Bundling**: After all updates are made, build the project and bundle the release outputs into a single ZIP file named `botmaster26.zip` in the root of the workspace. Inform the user of the complete absolute PATH location once completed.
6. **Version Metadata**: Whenever compiling a new release, always update the `version.json` metadata file in the root of the workspace (specifying the version, current release date, and changelog/new features). **Important**: The changelog should ONLY contain user-facing feature updates. Do NOT include internal technical changes (e.g., license domain redirects, cleaning developer info, or error message adjustments) in the changelog. Push both `botmaster26.zip` and `version.json` to the GitHub repository so the website updates automatically.
7. **Landing Page Deployment**: The `index.php` file is deployed manually to the user's hosting. Do not push `index.php` to the GitHub repository when making changes to it.
8. **WAGW Feature Integration**: If the user receives a vanilla Botmaster upgrade, you must re-apply the WAGW (ODBC Auto-Sync) feature. The backup files and instructions for this integration are stored in the `WAGW_Patch` folder in the root directory. Follow `WAGW_Integration_Guide.md` to re-inject the `FrmDatabaseSync` form, update the `Botmaster.vbproj`, modify the Help menu, and hook into `FrmBrowser.vb` AutoReply listener. Additionally, run the `WAGW_Patch\ApplyTheme.ps1` script to re-apply the WAGW Dark Theme (Dark Blue/Pink) to all Designer forms and `ModuleConfig.vb`.
9. **License Migration Mechanism**: When changing the application's name (e.g., to `Botmaster-WAGW`), the Windows Registry path changes, which normally resets the hardware HWID/Request Key. We have built an automatic fallback in `WhatsappBulkSenderModule.vb` (`InitiateStart` and `getMacAddress`) that scans `Software\VB and VBA Program Settings` for legacy Botmaster keys and either auto-migrates them or displays a sleek dark-themed UI for the user to select which key to migrate. Maintain this fallback logic in future upgrades to prevent licensing issues.
