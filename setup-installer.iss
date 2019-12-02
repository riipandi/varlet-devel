; by Aris Ripandi - 2019

#define BasePath      ""
#define AppVersion    "2.1"
#define AppName       "Varlet Core"
#define AppPublisher  "Aris Ripandi"
#define AppWebsite    "https://arisio.us"
#define AppGithubUrl  "https://github.com/riipandi/varlet-core"
#define SetupFileName "varlet-core-2.1-x64"

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
Name: task_install_vcredis; Description: "Install Visual C++ Redistributable"; Flags: unchecked
Name: task_autorun_service; Description: "Run services when Windows starts"; Flags: unchecked
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
Source: {#BasePath}_output\utils\*; DestDir: {app}\utils; Flags: ignoreversion recursesubdirs
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
Filename: "http://localhost/"; Description: "Open localhost page"; Flags: postinstall shellexec skipifsilent unchecked; BeforeInstall: StartHttpdService

[Dirs]
Name: {app}\tmp; Flags: uninsalwaysuninstall
Name: {app}\httpd; Flags: uninsalwaysuninstall
Name: {app}\httpd\conf\certs; Flags: uninsalwaysuninstall

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
var CertDir : String;
begin
  BaseDir := ExpandConstant('{app}');
  CertDir := BaseDir + '\httpd\conf\certs';

  // httpd + mkcert
  Str := '-key-file ' + CertDir + '\localhost.key -cert-file ' + CertDir + '\localhost.pem localhost';
  Exec(BaseDir + '\httpd\bin\mkcert.exe', Str, '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
  Exec(BaseDir + '\httpd\bin\mkcert.exe', '-install', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
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
  EnvAddPath(ExpandConstant('{app}\php\php-7.3-ts'));
  EnvAddPath(ExpandConstant('{userappdata}\Composer\vendor\bin'));
  CreateEnvironmentVariable('OPENSSL_CONF', ExpandConstant('{app}\httpd\conf\openssl.cnf'));
end;

procedure RemovePathEnvironment;
begin
  EnvRemovePath(ExpandConstant('{app}\utils'));
  EnvRemovePath(ExpandConstant('{app}\httpd\bin'));
  EnvRemovePath(ExpandConstant('{app}\php\php-7.3-ts'));
  EnvRemovePath(ExpandConstant('{userappdata}\Composer\vendor\bin'));
  RemoveEnvironmentVariable('OPENSSL_CONF');
end;

procedure StartHttpdService;
begin
  Exec(ExpandConstant('net.exe'), 'start VarletHttpd', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
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

    // httpd services
    WizardForm.StatusLabel.Caption := 'Installing HTTPd services ...';
    Str := '-k install -n "VarletHttpd" -f '+BaseDir+'"\httpd\conf\httpd.conf"';
    Exec(HttpdBin, Str, '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
    if WizardIsTaskSelected('task_autorun_service') then begin
      Exec(ExpandConstant('sc.exe'), 'config VarletHttpd start=auto', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
      Exec(ExpandConstant('net.exe'), 'start VarletHttpd', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
    end else begin
      Exec(ExpandConstant('sc.exe'), 'config VarletHttpd start=demand', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
      Exec(ExpandConstant('net.exe'), 'stop VarletHttpd', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
    end;
  end;

  if (CurStep=ssDone) then
  begin
    // do somethind
  end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  case CurUninstallStep of
    usUninstall:
      begin
        RemovePathEnvironment;
        KillService('VarletHttpd');
      end;
    usPostUninstall:
      begin
        // MsgBox(ExpandConstant('{#AppName}') + ' uninstalled, but some files are not removed!', mbInformation, MB_OK);
      end;
  end;
end;
