; by Aris Ripandi - 2019

#define BasePath      "..\"

#define AppVersion    "1.0"
#define AppName       "Varlet Core"
#define AppPublisher  "Aris Ripandi"
#define AppWebsite    "https://arisio.us"

[Setup]
AppName                    = {#AppName}
AppVersion                 = {#AppVersion}
AppPublisher               = {#AppPublisher}
AppPublisherURL            = {#AppWebsite}
AppSupportURL              = {#AppWebsite}
AppUpdatesURL              = {#AppWebsite}
DefaultGroupName           = {#AppName}
OutputBaseFilename         = varlet-core-{#AppVersion}
AppCopyright               = Copyright (c) {#AppPublisher}
ArchitecturesAllowed            = x64
ArchitecturesInstallIn64BitMode = x64
Compression                = lzma2/max
SolidCompression           = yes
DisableStartupPrompt       = yes
DisableWelcomePage         = no
DisableDirPage             = no
DisableProgramGroupPage    = yes
DisableReadyPage           = no
DisableFinishedPage        = no
AppendDefaultDirName       = yes
AlwaysShowComponentsList   = no
FlatComponentsList         = yes

SetupIconFile         = "setup-icon.ico"
LicenseFile           = "setup-license.txt"
WizardImageFile       = "setup-img-side.bmp"
WizardSmallImageFile  = "setup-img-top.bmp"
DefaultDirName        = {sd}\Varlet\core
UninstallFilesDir     = {app}
Uninstallable         = yes
CreateUninstallRegKey = yes
DirExistsWarning      = yes
AlwaysRestart         = no
OutputDir             = {#BasePath}output

[Registry]
Root: HKLM; Subkey: "Software\{#AppPublisher}"; Flags: uninsdeletekeyifempty;
Root: HKLM; Subkey: "Software\{#AppPublisher}\{#AppName}"; Flags: uninsdeletekey;
Root: HKLM; Subkey: "Software\{#AppPublisher}\{#AppName}"; ValueType: string; ValueName: "InstallPath"; ValueData: "{app}";
Root: HKLM; Subkey: "Software\{#AppPublisher}\{#AppName}"; ValueType: string; ValueName: "AppVersion"; ValueData: "{#AppVersion}";

[Files]
; Main project files ----------------------------------------------------------------------------------
Source: {#BasePath}license.txt; DestDir: {app}; Flags: ignoreversion
Source: {#BasePath}stubs\switch-php.bat; DestDir: {app}; Flags: ignoreversion
Source: {#BasePath}stubs\php7.ini; DestDir: {app}; Flags: ignoreversion
Source: {#BasePath}stubs\php5.ini; DestDir: {app}; Flags: ignoreversion
; Essential files and directories ---------------------------------------------------------------------
Source: {#BasePath}packages\php56\*; DestDir: {app}\php56; Flags: ignoreversion recursesubdirs
Source: {#BasePath}packages\php72\*; DestDir: {app}\php72; Flags: ignoreversion recursesubdirs
Source: {#BasePath}packages\php73\*; DestDir: {app}\php73; Flags: ignoreversion recursesubdirs
Source: {#BasePath}packages\ioncube\*; DestDir: {app}\ioncube; Flags: ignoreversion recursesubdirs
Source: {#BasePath}packages\composer\*; DestDir: {app}\composer; Flags: ignoreversion recursesubdirs
; Dependencies and libraries -------------------------------------------------------------------------
Source: {#BasePath}packages\vcredis\vcredis2012x64.exe; DestDir: {tmp}; Flags: ignoreversion deleteafterinstall
Source: {#BasePath}packages\vcredis\vcredis1519x64.exe; DestDir: {tmp}; Flags: ignoreversion deleteafterinstall

[Icons]
Name: "{group}\Uninstall {#AppName}"; Filename: "{uninstallexe}"

[UninstallDelete]
Type: filesandordirs; Name: {app}

[Run]
; Install external packages --------------------------------------------------------------------------
Filename: "msiexec.exe"; Parameters: "/i ""{tmp}\vcredis2012x64.exe"" /quiet /norestart"; Flags: waituntilterminated;
Filename: "msiexec.exe"; Parameters: "/i ""{tmp}\vcredis1519x64.exe"" /quiet /norestart"; Flags: waituntilterminated;
