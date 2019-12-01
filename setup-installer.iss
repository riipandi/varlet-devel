; by Aris Ripandi - 2019

#define BasePath      ""
#define AppVersion    "2.0"
#define AppName       "Varlet Core"
#define AppPublisher  "Aris Ripandi"
#define AppWebsite    "https://arisio.us"
#define AppGithubUrl  "https://github.com/riipandi/varlet-core"
#define SetupFileName "varlet-core-2.0-x64"

[Setup]
AppName                    = {#AppName}
AppVersion                 = {#AppVersion}
AppPublisher               = {#AppPublisher}
AppPublisherURL            = {#AppWebsite}
AppSupportURL              = {#AppWebsite}
AppUpdatesURL              = {#AppWebsite}
DefaultGroupName           = {#AppName}
OutputBaseFilename         = {#SetupFileName}
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

OutputDir             = {#BasePath}_output
SetupIconFile         = "{#BasePath}include\setup-icon.ico"
LicenseFile           = "{#BasePath}include\varlet-license.txt"
WizardImageFile       = "{#BasePath}include\setup-img-side.bmp"
WizardSmallImageFile  = "{#BasePath}include\setup-img-top.bmp"
DefaultDirName        = {sd}\Varlet\core
UninstallFilesDir     = {app}
Uninstallable         = yes
CreateUninstallRegKey = yes
DirExistsWarning      = yes
AlwaysRestart         = no

[Registry]
Root: HKLM; Subkey: "Software\{#AppPublisher}"; Flags: uninsdeletekeyifempty;
Root: HKLM; Subkey: "Software\{#AppPublisher}\{#AppName}"; Flags: uninsdeletekey;
Root: HKLM; Subkey: "Software\{#AppPublisher}\{#AppName}"; ValueType: string; ValueName: "InstallPath"; ValueData: "{app}";
Root: HKLM; Subkey: "Software\{#AppPublisher}\{#AppName}"; ValueType: string; ValueName: "AppVersion"; ValueData: "{#AppVersion}";

[Tasks]
Name: task_install_vcredis; Description: "Install Visual C++ Redistributable"; Flags: unchecked;
Name: task_autorun_service; Description: "Run services when Windows starts"; Flags: unchecked;
Name: task_add_path_envars; Description: "Add PATH environment variables";

[Files]
; Main project files ----------------------------------------------------------------------------------
Source: {#BasePath}include\varlet-license.txt; DestDir: {app}\docs; Flags: ignoreversion
Source: {#BasePath}stubs\php\php.ini; DestDir: "{app}\php\php-7.2-ts"; Flags: ignoreversion
Source: {#BasePath}stubs\php\php.ini; DestDir: "{app}\php\php-7.3-ts"; Flags: ignoreversion
; Source: {#BasePath}stubs\php\phpfpmservice.xml; DestDir: {app}; Flags: ignoreversion
; Source: {#BasePath}stubs\nginx\nginxservice.xml; DestDir: {app}; Flags: ignoreversion
; Source: {#BasePath}_output\phpfpmservice.exe; DestDir: {app}; Flags: ignoreversion
; Source: {#BasePath}_output\nginxservice.exe; DestDir: {app}; Flags: ignoreversion
; Source: {#BasePath}stubs\openssl.cnf; DestDir: {app}\openssl; Flags: ignoreversion
; CLI apps for varlet ---------------------------------------------------------------------------------
; Source: {#BasePath}_output\cli\varlet.runtimeconfig.json; DestDir: {app}\cli; Flags: ignoreversion recursesubdirs
; Source: {#BasePath}_output\cli\*.dll; DestDir: {app}\cli; Flags: ignoreversion recursesubdirs
; Source: {#BasePath}_output\cli\*.exe; DestDir: {app}\cli; Flags: ignoreversion recursesubdirs
; Essential files and directories ---------------------------------------------------------------------
Source: "{#BasePath}_output\php\php-7.2-ts\*"; DestDir: "{app}\php\php-7.2-ts"; Flags: ignoreversion recursesubdirs
Source: "{#BasePath}_output\php\php-7.3-ts\*"; DestDir: "{app}\php\php-7.3-ts"; Flags: ignoreversion recursesubdirs
Source: {#BasePath}_output\composer\*; DestDir: {app}\composer; Flags: ignoreversion recursesubdirs
Source: {#BasePath}_output\httpd\*; DestDir: {app}\httpd; Flags: ignoreversion recursesubdirs
Source: {#BasePath}stubs\htdocs\*; DestDir: {app}\htdocs; Flags: ignoreversion recursesubdirs
; Source: {#BasePath}_output\nginx\*; DestDir: {app}\nginx; Flags: ignoreversion recursesubdirs
; Dependencies and libraries -------------------------------------------------------------------------
Source: {#BasePath}_temp\vcredis\*; DestDir: {tmp}; Flags: ignoreversion deleteafterinstall

[Icons]
Name: "{group}\Uninstall {#AppName}"; Filename: "{uninstallexe}"

[UninstallDelete]
Type: filesandordirs; Name: {app}

[Run]
; Install external packages --------------------------------------------------------------------------
Filename: "msiexec.exe"; Parameters: "/i ""{tmp}\vcredis2012x64.exe"" /quiet /norestart"; Flags: waituntilterminated; Tasks: task_install_vcredis
Filename: "msiexec.exe"; Parameters: "/i ""{tmp}\vcredis1519x64.exe"" /quiet /norestart"; Flags: waituntilterminated; Tasks: task_install_vcredis
; Filename: "{app}\varlet.exe"; BeforeInstall: CreatePathEnvironment

[Dirs]
Name: {app}\tmp; Flags: uninsalwaysuninstall
Name: {app}\httpd; Flags: uninsalwaysuninstall

; ----------------------------------------------------------------------------------------------------
; Programmatic section -------------------------------------------------------------------------------
; ----------------------------------------------------------------------------------------------------
#include 'include\setup-helpers.iss'

[Code]
var
  BaseDir : String;
  Str : String;

procedure InitializeWizard;
begin
  CustomLicensePage;
  //CreateFooterText(#169 + ' 2019 - {#AppPublisher}');
  CreateFooterText('{#AppGithubUrl}');
end;

procedure ConfigureApplication;
begin
  BaseDir := ExpandConstant('{app}\php\');

  // PHP 7.2
  FileReplaceString(BaseDir + 'php-7.2-ts\php.ini', '<<INSTALL_DIR>>', PathWithSlashes(ExpandConstant('{app}')));
  FileReplaceString(BaseDir + 'php-7.2-ts\php.ini', '<<PHP_BASEDIR>>', PathWithSlashes(BaseDir + 'php-7.2-ts'));

  // PHP 7.3
  FileReplaceString(BaseDir + 'php-7.3-ts\php.ini', '<<INSTALL_DIR>>', PathWithSlashes(ExpandConstant('{app}')));
  FileReplaceString(BaseDir + 'php-7.3-ts\php.ini', '<<PHP_BASEDIR>>', PathWithSlashes(BaseDir + 'php-7.3-ts'));

  // Create composer.bat
  Str := '@echo off' + #13#10#13#10 + '"'+BaseDir+'php-7.3-ts\php.exe" "'+ExpandConstant('{app}\composer\composer.phar')+'" %*';
  SaveStringToFile(BaseDir + '\composer\composer.bat', Str, False);

  // Nginx
  // FileReplaceString(BaseDir + '\nginx\conf\vhost\default.conf', '<<INSTALL_DIR>>', PathWithSlashes(ExpandConstant('{app}')));

  // PHP & Nginx Services
  // FileReplaceString(BaseDir + '\phpfpmservice.xml', '<<INSTALL_DIR>>', ExpandConstant('{app}'));
  // FileReplaceString(BaseDir + '\nginxservice.xml', '<<INSTALL_DIR>>', ExpandConstant('{app}'));

  // Autorun services
  // if WizardIsTaskSelected('task_autorun_service') then begin
  //   FileReplaceString(BaseDir + '\phpfpmservice.xml', 'Manual', 'Automatic');
  //   FileReplaceString(BaseDir + '\nginxservice.xml', 'Manual', 'Automatic');
  // end;
end;

procedure CreatePathEnvironment();
begin
  EnvAddPath(ExpandConstant('{app}\utils'));
  EnvAddPath(ExpandConstant('{app}\composer'));
  EnvAddPath(ExpandConstant('{app}\httpd\bin'));
  EnvAddPath(ExpandConstant('{app}\php\php-7.3-ts'));
  EnvAddPath(ExpandConstant('{userappdata}\Composer\vendor\bin'));
  // OpenSSL configuration file
  // EnvAddPath(ExpandConstant('{app}\openssl\bin'));
  // CreateEnvironmentVariable('OPENSSL_CONF', ExpandConstant('{app}\openssl\openssl.cnf'));
end;

procedure RemovePathEnvironment;
begin
  EnvRemovePath(ExpandConstant('{app}\utils'));
  EnvRemovePath(ExpandConstant('{app}\composer'));
  EnvRemovePath(ExpandConstant('{app}\httpd\bin'));
  EnvRemovePath(ExpandConstant('{app}\php\php-7.3-ts'));
  EnvRemovePath(ExpandConstant('{userappdata}\Composer\vendor\bin'));
  // RemoveEnvironmentVariable('OPENSSL_CONF');
end;

procedure CurPageChanged(CurPageID: Integer);
begin
  if CurPageID = wpLicense then begin
    WizardForm.NextButton.Caption := '&I agree';
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  BaseDir := ExpandConstant('{app}');
  if CurStep = ssPostInstall then begin

    WizardForm.StatusLabel.Caption := 'Setting up application configuration ...';
    ConfigureApplication;

    if WizardIsTaskSelected('task_add_path_envars') then begin
      WizardForm.StatusLabel.Caption := 'Adding PATH environment variables ...';
      CreatePathEnvironment;
    end;

    // WizardForm.StatusLabel.Caption := 'Installing PHP-FPM services ...';
    // if Exec(ExpandConstant('{app}\phpfpmservice.exe'), 'install', '', SW_HIDE, ewWaitUntilTerminated, ResultCode) then begin
    //   Exec(ExpandConstant('net.exe'), 'start VarletPHP', '', SW_HIDE, ewWaitUntilTerminated, ResultCode)
    // end else begin
    //   MsgBox('Failed to install PHP-FPM service', mbInformation, MB_OK);
    // end;

    // WizardForm.StatusLabel.Caption := 'Installing Nginx services ...';
    // if Exec(ExpandConstant('{app}\nginxservice.exe'), 'install', '', SW_HIDE, ewWaitUntilTerminated, ResultCode) then begin
    //   Exec(ExpandConstant('net.exe'), 'start VarletNginx', '', SW_HIDE, ewWaitUntilTerminated, ResultCode)
    // end else begin
    //   MsgBox('Failed to install PHP-FPM service', mbInformation, MB_OK);
    // end;
  end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  case CurUninstallStep of
    usUninstall:
      begin
        RemovePathEnvironment;
        // KillService('VarletNginx');
        // KillService('VarletPHP');
      end;
    usPostUninstall:
      begin
        // MsgBox(ExpandConstant('{#AppName}') + ' uninstalled, but some files are not removed!', mbInformation, MB_OK);
      end;
  end;
end;
