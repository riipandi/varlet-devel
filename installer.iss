; by Aris Ripandi - 2019

#define BasePath      ""
#define AppName       "Varlet"
#define AppSlug       "varlet"
#define AppWebsite    "varlet.dev"
#define AppPublisher  "Aris Ripandi"
#define GetAppVersion GetFileVersion('_dstdir\VarletUi.exe')

[Setup]
AppName                    = {#AppName}
AppVersion                 = {#GetAppVersion}
AppPublisher               = {#AppPublisher}
AppPublisherURL            = {#AppWebsite}
AppSupportURL              = {#AppWebsite}
AppUpdatesURL              = {#AppWebsite}
DefaultGroupName           = {#AppName}
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

OutputDir             = {#BasePath}_temp
OutputBaseFilename    = {#AppSlug}-{#GetAppVersion}-x64
SetupIconFile         = "{#BasePath}include\setup-icon.ico"
LicenseFile           = "{#BasePath}include\varlet-license.txt"
WizardImageFile       = "{#BasePath}include\setup-img-side.bmp"
WizardSmallImageFile  = "{#BasePath}include\setup-img-top.bmp"
;DefaultDirName        = {sd}\Varlet
DefaultDirName        = {code:GetDefaultDir}
UninstallFilesDir     = {app}
Uninstallable         = yes
CreateUninstallRegKey = yes
DirExistsWarning      = yes
AlwaysRestart         = no

[Registry]
Root: HKLM; Subkey: "Software\{#AppPublisher}"; Flags: uninsdeletekeyifempty;
Root: HKLM; Subkey: "Software\{#AppPublisher}\{#AppName}"; Flags: uninsdeletekey;
Root: HKLM; Subkey: "Software\{#AppPublisher}\{#AppName}"; ValueType: string; ValueName: "InstallPath"; ValueData: "{app}";
Root: HKLM; Subkey: "Software\{#AppPublisher}\{#AppName}"; ValueType: string; ValueName: "AppVersion"; ValueData: "{#GetAppVersion}";

[Tasks]
Name: task_add_path_envars; Description: "Add PATH environment variables";
Name: task_autorun_service; Description: "Run services when Windows starts"; Flags: unchecked
Name: task_install_mailhog; Description: "Install Mailhog SMTP Testing"; Flags: unchecked

[Files]
; ----------------------------------------------------------------------------------------------------------------------
Source: "{#BasePath}credits.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#BasePath}include\varlet-license.txt"; DestDir: "{app}"; DestName: "license.txt"; Flags: ignoreversion
Source: "{#BasePath}source\_release\VarletUi.exe"; DestDir: "{app}"; Flags: ignoreversion
; ----------------------------------------------------------------------------------------------------------------------
Source: "{#BasePath}_dstdir\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
Source: "{#BasePath}stubs\config\php.ini"; DestDir: "{app}\php\php-7.2-ts"; Flags: ignoreversion
Source: "{#BasePath}stubs\config\php.ini"; DestDir: "{app}\php\php-7.3-ts"; Flags: ignoreversion
Source: "{#BasePath}stubs\htdocs\*"; DestDir: "{app}\htdocs"; Flags: ignoreversion recursesubdirs
Source: "{#BasePath}stubs\opt\*"; DestDir: "{app}\opt"; Flags: ignoreversion recursesubdirs
; ----------------------------------------------------------------------------------------------------------------------
Source: "{#BasePath}_temp\vcredis\*"; DestDir: {tmp}; Flags: ignoreversion deleteafterinstall

[Icons]
Name: "{group}\VarletUi"; Filename: "{app}\VarletUi.exe"
Name: "{commondesktop}\VarletUi"; Filename: "{app}\VarletUi.exe"
Name: "{group}\Uninstall {#AppName}"; Filename: "{uninstallexe}"

[Run]
Filename: "{tmp}\vcredis2012x64.exe"; Parameters: "/install /passive /norestart"; Description: "Installing VCRedist 2012"; Flags: waituntilterminated; Check: VCRedist2012NotInstalled
Filename: "{tmp}\vcredis1519x64.exe"; Parameters: "/install /passive /norestart"; Description: "Installing VCRedist 2015"; Flags: waituntilterminated; Check: VCRedist2015NotInstalled
;Filename: "{app}\VarletUi.exe"; Description: "Run Varlet Controller"; Flags: postinstall shellexec skipifsilent ; BeforeInstall: StartAppServices

[Dirs]
Name: {app}\tmp; Flags: uninsalwaysuninstall
Name: {app}\httpd; Flags: uninsalwaysuninstall
Name: {app}\httpd\conf\certs; Flags: uninsalwaysuninstall

[UninstallDelete]
Type: filesandordirs; Name: {app}

; ----------------------------------------------------------------------------------------------------
; Programmatic section -------------------------------------------------------------------------------
; ----------------------------------------------------------------------------------------------------
#include 'include\setup-helpers.iss'

[Code]
const AppFolder = 'Varlet';

function GetDefaultDir(Param: string): string;
begin
  Result := GetAppRegistry('InstallPath');
  if not RegKeyExists(HKLM, 'Software\{#AppPublisher}\{#AppName}') then
    Result := ExpandConstant('{sd}\') + AppFolder;
end;

function ShouldSkipPage(PageID: Integer): Boolean;
begin
  Result := (PageID = wpSelectDir) and DirExists(GetAppRegistry('InstallPath'));
  if not RegKeyExists(HKLM, 'Software\{#AppPublisher}\{#AppName}') then
    Result := (PageID = wpSelectDir) and DirExists(ExpandConstant('{sd}\') + AppFolder);
end;

function InitializeSetup: Boolean;
begin
  Result := True;
  if RegKeyExists(HKLM, 'Software\{#AppPublisher}\{#AppName}') then begin
    InstallPath := GetAppRegistry('InstallPath');
    Str := 'Previous installation detected at: '+InstallPath+''#13#13'This process will update current installation.'#13'Do you want to start the process?';
    Result := MsgBox(Str, mbConfirmation, MB_YESNO) = idYes;
    if Result = False then begin
      MsgBox('Installation cancelled!', mbInformation, MB_OK);
      Abort;
    end else begin
      if IsAppRunning('VarletUi.exe') then TaskKillByPid('VarletUi.exe');
      if IsAppRunning('varlet.exe') then TaskKillByPid('varlet.exe');
      if IsServiceRunning('VarletHttpd') then KillService('VarletHttpd');
      if IsServiceRunning('VarletMailhog') then KillService('VarletMailhog');
    end;
  end;
end;

procedure InitializeWizard;
begin
  CustomLicensePage;
  // CreateFooterText(#169 + ' 2019 - {#AppPublisher}');
  CreateFooterText('{#AppWebsite}');
end;

function VCRedist2012NotInstalled: Boolean;
begin
  Result := not RegKeyExists(HKEY_LOCAL_MACHINE, 'SOFTWARE\WOW6432Node\Microsoft\VisualStudio\12.0');
end;

function VCRedist2015NotInstalled: Boolean;
begin
  Result := not RegKeyExists(HKEY_LOCAL_MACHINE, 'SOFTWARE\WOW6432Node\Microsoft\VisualStudio\14.0');
end;

procedure ConfigureApplication;
var CertDir : String;
begin
  BaseDir := ExpandConstant('{app}');
  CertDir := BaseDir + '\httpd\conf\certs';

  // httpd with ssl
  Str := '-key-file ' + CertDir + '\localhost.key -cert-file ' + CertDir + '\localhost.pem localhost';
  Exec(BaseDir + '\utils\mkcert.exe', Str, '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
  Exec(BaseDir + '\utils\mkcert.exe', '-install', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
  FileReplaceString(BaseDir + '\httpd\conf\httpd.conf', '<<INSTALL_DIR>>', PathWithSlashes(ExpandConstant('{app}')));

  // PHP 7.2
  FileReplaceString(BaseDir + '\php\php-7.2-ts\php.ini', '<<INSTALL_DIR>>', PathWithSlashes(ExpandConstant('{app}')));
  FileReplaceString(BaseDir + '\php\php-7.2-ts\php.ini', '<<PHP_BASEDIR>>', PathWithSlashes(BaseDir + '\php\php-7.2-ts'));

  // PHP 7.3
  FileReplaceString(BaseDir + '\php\php-7.3-ts\php.ini', '<<INSTALL_DIR>>', PathWithSlashes(ExpandConstant('{app}')));
  FileReplaceString(BaseDir + '\php\php-7.3-ts\php.ini', '<<PHP_BASEDIR>>', PathWithSlashes(BaseDir + '\php\php-7.3-ts'));

  // Create composer.bat
  Str := '@echo off' + #13#10#13#10 + '"'+BaseDir+'\php\php-7.3-ts\php.exe" "'+ExpandConstant('{app}\utils\composer.phar')+'" %*';
  SaveStringToFile(BaseDir + '\utils\composer.bat', Str, False);
end;

procedure CreatePathEnvironment();
begin
  EnvAddPath(ExpandConstant('{app}\utils'));
  EnvAddPath(ExpandConstant('{app}\httpd\bin'));
  EnvAddPath(ExpandConstant('{app}\imagick\bin'));
  EnvAddPath(ExpandConstant('{app}\php\php-7.3-ts'));
  EnvAddPath(ExpandConstant('{userappdata}\Composer\vendor\bin'));
  CreateEnvironmentVariable('OPENSSL_CONF', ExpandConstant('{app}\httpd\conf\openssl.cnf'));
end;

procedure RemovePathEnvironment;
begin
  EnvRemovePath(ExpandConstant('{app}\utils'));
  EnvRemovePath(ExpandConstant('{app}\httpd\bin'));
  EnvRemovePath(ExpandConstant('{app}\imagick\bin'));
  EnvRemovePath(ExpandConstant('{app}\php\php-7.3-ts'));
  EnvRemovePath(ExpandConstant('{userappdata}\Composer\vendor\bin'));
  RemoveEnvironmentVariable('OPENSSL_CONF');
end;

procedure StartAppServices;
begin
  Exec(ExpandConstant('net.exe'), 'start VarletMailhog', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
  Exec(ExpandConstant('net.exe'), 'start VarletHttpd', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
  ShellExec('open', 'http://localhost/', '', '', SW_SHOW, ewNoWait, ResultCode);
end;

procedure CurPageChanged(CurPageID: Integer);
begin
  if CurPageID = wpLicense then begin
    WizardForm.NextButton.Caption := '&I agree';
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
var HttpdBin : String;
begin
  BaseDir := ExpandConstant('{app}');
  HttpdBin := BaseDir + '\httpd\bin\httpd.exe';

  if CurStep = ssPostInstall then begin

    WizardForm.StatusLabel.Caption := 'Setting up application configuration ...';
    ConfigureApplication;

    if WizardIsTaskSelected('task_add_path_envars') then begin
      WizardForm.StatusLabel.Caption := 'Adding PATH environment variables ...';
      CreatePathEnvironment;
    end;

    // Apache Web Server
    WizardForm.StatusLabel.Caption := 'Installing Apache Web Server ...';
    Str := '-k install -n "VarletHttpd" -f '+BaseDir+'"\httpd\conf\httpd.conf"';
    Exec(HttpdBin, Str, '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
    if WizardIsTaskSelected('task_autorun_service') then begin
      Exec(ExpandConstant('sc.exe'), 'config VarletHttpd start=auto', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
      Exec(ExpandConstant('net.exe'), 'start VarletHttpd', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
    end else begin
      Exec(ExpandConstant('sc.exe'), 'config VarletHttpd start=demand', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
      Exec(ExpandConstant('net.exe'), 'stop VarletHttpd', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
    end;

    // Mailhog service
    if WizardIsTaskSelected('task_install_mailhog') then begin
      WizardForm.StatusLabel.Caption := 'Installing Mailhog services ...';
      FileReplaceString(BaseDir + '\mailhog\mailhogservice.xml', '<<INSTALL_DIR>>', ExpandConstant('{app}'));
      if WizardIsTaskSelected('task_autorun_service') then begin
        FileReplaceString(BaseDir + '\mailhog\mailhogservice.xml', '<<SERVICE_MODE>>', 'Automatic');
      end else begin
        FileReplaceString(BaseDir + '\mailhog\mailhogservice.xml', '<<SERVICE_MODE>>', 'Manual');
      end;
      Exec(BaseDir + '\mailhog\mailhogservice.exe', 'install', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
      Exec(ExpandConstant('net.exe'), 'stop VarletMailhog', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
    end;
  end;

  if (CurStep=ssDone) then begin
    StartAppServices;
    ShellExec('open', ExpandConstant('{app}\VarletUi.exe'), '', '', SW_SHOW, ewNoWait, ResultCode);
  end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  case CurUninstallStep of
    usUninstall:
      begin
        RemovePathEnvironment;
        KillService('VarletHttpd');
        KillService('VarletMailhog');
        TaskKillByPid('VarletUi.exe');
        TaskKillByPid('varlet.exe');
      end;
    usPostUninstall:
      begin
        // MsgBox(ExpandConstant('{#AppName}') + ' uninstalled, but some files are not removed!', mbInformation, MB_OK);
      end;
  end;
end;
