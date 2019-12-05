@echo off
setlocal EnableDelayedExpansion
REM color 08

set CURL=%~dp0utils\curl.exe
set UNZIP=%~dp0utils\7za.exe
set TMPDIR=%~dp0_temp
set ODIR=%~dp0_dstdir
set STUB=%~dp0stubs

:: ---------------------------------------------------------------------------------------------------------------------
set "ver_composer=1.9.1"
set "ver_httpd=2.4.41"
set "ver_nginx=1.17.6"
set "ver_openssl=1.1.1e"
set "ver_php72=7.2.25"
set "ver_php73=7.3.12"
set "ver_php74=7.4.0"
set "ver_imagick=7.0.7-11"
set "ver_php_imagick=3.4.3"
set "ver_php_phalcon=3.4.5"
set "ver_phpredis=5.1.1"
set "ver_xdebug=2.8.0"
set "ver_mkcert=1.4.1"
set "ver_adminer=4.7.5"
set "ver_mailhog=1.0.0"
set "ver_mhsendmail=0.2.0"

:: Download link
set "url_php72=https://windows.php.net/downloads/releases/php-%ver_php72%-Win32-VC15-x64.zip"
set "url_php73=https://windows.php.net/downloads/releases/php-%ver_php73%-Win32-VC15-x64.zip"
set "url_php74=https://windows.php.net/downloads/releases/php-%ver_php74%-Win32-vc15-x64.zip"

set "url_phalcon_php72=https://github.com/phalcon/cphalcon/releases/download/v%ver_php_phalcon%/phalcon_x64_vc15_php7.3_%ver_php_phalcon%-4250.zip"
set "url_phalcon_php73=https://github.com/phalcon/cphalcon/releases/download/v%ver_php_phalcon%/phalcon_x64_vc15_php7.3_%ver_php_phalcon%-4250.zip"

set "url_imagick_php72=https://windows.php.net/downloads/pecl/snaps/imagick/%ver_php_imagick%/php_imagick-%ver_php_imagick%-7.2-ts-vc15-x64.zip"
set "url_imagick_php73=https://windows.php.net/downloads/pecl/snaps/imagick/%ver_php_imagick%/php_imagick-%ver_php_imagick%-7.3-ts-vc15-x64.zip"
set "url_imagick_php74=https://windows.php.net/downloads/pecl/snaps/imagick/%ver_php_imagick%/php_imagick-%ver_php_imagick%-7.4-ts-vc15-x64.zip"

set "url_phpredis_php72=https://windows.php.net/downloads/pecl/releases/redis/%ver_phpredis%/php_redis-%ver_phpredis%-7.2-ts-vc15-x64.zip"
set "url_phpredis_php73=https://windows.php.net/downloads/pecl/releases/redis/%ver_phpredis%/php_redis-%ver_phpredis%-7.3-ts-vc15-x64.zip"
set "url_phpredis_php74=https://windows.php.net/downloads/pecl/releases/redis/%ver_phpredis%/php_redis-%ver_phpredis%-7.4-ts-vc15-x64.zip"

set "url_xdebug_php72=https://xdebug.org/files/php_xdebug-%ver_xdebug%beta2-7.2-vc15-x86_64.dll"
set "url_xdebug_php73=https://xdebug.org/files/php_xdebug-%ver_xdebug%-7.3-vc15-x86_64.dll"
set "url_xdebug_php74=https://xdebug.org/files/php_xdebug-%ver_xdebug%-7.4-vc15-x86_64.dll"

:: ---------------------------------------------------------------------------------------------------------------------
:menu
for /F "tokens=1,2 delims=#" %%a in ('"prompt #$H#$E# & echo on & for %%b in (1) do rem"') do (set "DEL=%%a")
<nul set /p=""
call :PainText 02 "=====================================================" && echo. &
call :PainText 02 "=  1 - Build setup files       s - Install MSBuild   " && echo. &
call :PainText 02 "=  2 - Compile Varlet app      c - Clean packages    " && echo. &
call :PainText 02 "=  3 - Compile installer       x - Exit              " && echo. &
call :PainText 02 "====================================================="
goto :choice

:PainText
<nul set /p "=%DEL%" > "%~2"
findstr /v /a:%1 /R "+" "%~2" nul
del "%~2" > nul
goto :eof

:choice
echo. && set /P c="What do you want to do?: "
if /I "%c%" EQU "1" goto :build_setup
if /I "%c%" EQU "2" goto :compile_app
if /I "%c%" EQU "3" goto :compile_inno
if /I "%c%" EQU "s" goto :install_msbuild
if /I "%c%" EQU "c" goto :clean_packages
if /I "%c%" EQU "x" goto :quit
goto :choice

:: ---------------------------------------------------------------------------------------------------------------------
:build_setup
if not exist "%ODIR%" mkdir "%ODIR%" 2> NUL
if not exist "%TMPDIR%" mkdir "%TMPDIR%" 2> NUL
if not exist "%ODIR%\utils" mkdir "%ODIR%\utils" 2> NUL

:: PHP v7.3
if not exist "%TMPDIR%\php-%ver_php73%.zip" (
  echo. && echo ^> Downloading PHP v%ver_php73% ...
  %CURL% -L# %url_php73% -o "%TMPDIR%\php-%ver_php73%.zip"
  %CURL% -L# %url_xdebug_php73% -o "%TMPDIR%\php73_xdebug.dll"
  %CURL% -L# %url_phpredis_php73% -o "%TMPDIR%\php73_redis.zip"
  %CURL% -L# %url_imagick_php73% -o "%TMPDIR%\imagick-%ver_php_imagick%-php73.zip"
  %CURL% -L# %url_phalcon_php73% -o "%TMPDIR%\phalcon-%ver_php73%-php73.zip"
)
if exist "%TMPDIR%\php-%ver_php73%.zip" (
  echo. && echo ^> Extracting PHP v%ver_php73% ...
  if exist "%ODIR%\php\php-7.3-ts" RD /S /Q "%ODIR%\php\php-7.3-ts"
  %UNZIP% x "%TMPDIR%\php-%ver_php73%.zip" -o"%ODIR%\php\php-7.3-ts" -y > nul
  copy /Y "%TMPDIR%\php73_xdebug.dll" "%ODIR%\php\php-7.3-ts\ext\php_xdebug.dll" > nul

  %UNZIP% x "%TMPDIR%\phalcon-%ver_php73%-php73.zip" -o"%TMPDIR%\phalcon73" -y > nul
  copy /Y "%TMPDIR%\phalcon73\php_phalcon.dll" "%ODIR%\php\php-7.3-ts\ext\php_phalcon.dll" > nul

  %UNZIP% x "%TMPDIR%\php73_redis.zip" -o"%TMPDIR%" -y > nul
  copy /Y "%TMPDIR%\php_redis.dll" "%ODIR%\php\php-7.3-ts\ext\php_redis.dll" > nul
  del /F "%TMPDIR%\php_redis.dll"

  %UNZIP% x "%TMPDIR%\imagick-%ver_php_imagick%-php73.zip" -o"%ODIR%\php\php-7.3-ts" -y > nul
  copy /Y "%ODIR%\php\php-7.3-ts\php_imagick.dll" "%ODIR%\php\php-7.3-ts\ext\php_imagick.dll" > nul
  del /F "%ODIR%\php\php-7.3-ts\php_imagick.dll"
)

:: PHP v7.2
if not exist "%TMPDIR%\php-%ver_php72%.zip" (
  echo. && echo Downloading PHP v%ver_php72% ...
  %CURL% -L# %url_php72% -o "%TMPDIR%\php-%ver_php72%.zip"
  %CURL% -L# %url_xdebug_php72% -o "%TMPDIR%\php72_xdebug.dll"
  %CURL% -L# %url_phpredis_php72% -o "%TMPDIR%\php72_redis.zip"
  %CURL% -L# %url_imagick_php72% -o "%TMPDIR%\imagick-%ver_php_imagick%-php72.zip"
  %CURL% -L# %url_phalcon_php72% -o "%TMPDIR%\phalcon-%ver_php72%-php72.zip"
)
if exist "%TMPDIR%\php-%ver_php72%.zip" (
  echo. && echo ^> Extracting PHP v%ver_php72% ...
  if exist "%ODIR%\php\php-7.2-ts" RD /S /Q "%ODIR%\php\php-7.2-ts"
  %UNZIP% x "%TMPDIR%\php-%ver_php72%.zip" -o"%ODIR%\php\php-7.2-ts" -y > nul
  copy /Y "%TMPDIR%\php72_xdebug.dll" "%ODIR%\php\php-7.2-ts\ext\php_xdebug.dll" > nul

  %UNZIP% x "%TMPDIR%\phalcon-%ver_php72%-php72.zip" -o"%TMPDIR%\phalcon72" -y > nul
  copy /Y "%TMPDIR%\phalcon72\php_phalcon.dll" "%ODIR%\php\php-7.2-ts\ext\php_phalcon.dll" > nul

  %UNZIP% x "%TMPDIR%\php72_redis.zip" -o"%TMPDIR%" -y > nul
  copy /Y "%TMPDIR%\php_redis.dll" "%ODIR%\php\php-7.2-ts\ext\php_redis.dll" > nul
  del /F "%TMPDIR%\php_redis.dll"

  %UNZIP% x "%TMPDIR%\imagick-%ver_php_imagick%-php72.zip" -o"%ODIR%\php\php-7.2-ts" -y > nul
  copy /Y "%ODIR%\php\php-7.2-ts\php_imagick.dll" "%ODIR%\php\php-7.2-ts\ext\php_imagick.dll" > nul
  del /F "%ODIR%\php\php-7.2-ts\php_imagick.dll"
)

:: Apache HTTP Server
if not exist "%TMPDIR%\httpd-%ver_httpd%.zip" (
  echo. && echo Downloading Apache HTTP Server v%ver_httpd% ...
  %CURL% -L# "https://home.apache.org/~steffenal/VC15/binaries/httpd-%ver_httpd%-win64-VC15.zip" -o "%TMPDIR%\httpd-%ver_httpd%.zip"
)
if exist "%TMPDIR%\httpd-%ver_httpd%.zip" (
  echo. && echo ^> Extracting Apache HTTP Server v%ver_httpd% ...
  if exist "%ODIR%\httpd" RD /S /Q "%ODIR%\httpd"
  if exist "%TMPDIR%\Apache24" RD /S /Q "%TMPDIR%\Apache24"
  %UNZIP% x "%TMPDIR%\httpd-%ver_httpd%.zip" -o"%TMPDIR%" -y > nul
  xcopy %TMPDIR%\Apache24 %ODIR%\httpd /E /I /Y > nul
  RD /S /Q "%ODIR%\httpd\bin\iconv"
  RD /S /Q "%ODIR%\httpd\conf"
  RD /S /Q "%ODIR%\httpd\error"
  RD /S /Q "%ODIR%\httpd\htdocs"
  RD /S /Q "%ODIR%\httpd\icons"
  RD /S /Q "%ODIR%\httpd\include"
  RD /S /Q "%ODIR%\httpd\logs"
  RD /S /Q "%ODIR%\httpd\lib"
  RD /S /Q "%ODIR%\httpd\manual"
  xcopy %STUB%\httpd\conf %ODIR%\httpd\conf /E /I /Y > nul
)

:: ImageMagick
if not exist "%TMPDIR%\imagick-%ver_imagick%.zip" (
  echo. && echo Downloading ImageMagick v%ver_imagick% ...
  %CURL% -L# "http://windows.php.net/downloads/pecl/deps/ImageMagick-%ver_imagick%-vc15-x64.zip" -o "%TMPDIR%\imagick-%ver_imagick%.zip"
)
if exist "%TMPDIR%\imagick-%ver_imagick%.zip" (
  echo. && echo ^> Extracting ImageMagick v%ver_imagick% ...
  if exist "%ODIR%\imagick" RD /S /Q "%ODIR%\imagick"
  %UNZIP% x "%TMPDIR%\imagick-%ver_imagick%.zip" -o"%ODIR%\imagick" -y > nul
)

:: Composer
if not exist "%TMPDIR%\composer.phar" (
  echo. && echo Downloading Composer v%ver_composer% ...
  %CURL% -L# "https://getcomposer.org/download/%ver_composer%/composer.phar" -o "%TMPDIR%\composer.phar"
)
if exist "%TMPDIR%\composer.phar" ( copy /Y "%TMPDIR%\composer.phar" "%ODIR%\utils\composer.phar" > nul )

:: ionCube Loader VC15
if not exist "%TMPDIR%\ioncube-vc15.zip" (
  echo. && echo Downloading ionCube loader ...
  %CURL% -L# "https://downloads.ioncube.com/loader_downloads/ioncube_loaders_win_vc15_x86-64.zip" -o "%TMPDIR%\ioncube-vc15.zip"
)
if exist "%TMPDIR%\ioncube-vc15.zip" (
  echo. && echo ^> Extracting ionCube loader ...
  if exist "%TMPDIR%\ioncube" RD /S /Q "%TMPDIR%\ioncube"
  %UNZIP% x "%TMPDIR%\ioncube-vc15.zip" -o"%TMPDIR%" -y > nul
  copy /Y "%TMPDIR%\ioncube\ioncube_loader_win_7.2.dll" "%ODIR%\php\php-7.2-ts\ext\php_ioncube.dll" > nul
  copy /Y "%TMPDIR%\ioncube\ioncube_loader_win_7.3.dll" "%ODIR%\php\php-7.3-ts\ext\php_ioncube.dll" > nul
)

:: VCRedist 2012 + 2015-2019
set "URL_VCREDIST_1519=https://aka.ms/vs/16/release/VC_redist.x64.exe"
set "URL_VCREDIST_2012=https://download.microsoft.com/download/1/6/B/16B06F60-3B20-4FF2-B699-5E9B7962F9AE/VSU_4/vcredist_x64.exe"
if not exist "%TMPDIR%\vcredis\" (
  echo. && echo Downloading Visual C++ Redistributable ...
  if not exist "%TMPDIR%\vcredis" mkdir "%TMPDIR%\vcredis" 2> NUL
  %CURL% -L# %URL_VCREDIST_2012% -o "%TMPDIR%\vcredis\vcredis2012x64.exe"
  %CURL% -L# %URL_VCREDIST_1519% -o "%TMPDIR%\vcredis\vcredis1519x64.exe"
)

:: Mailhog + mhsendmail
if not exist "%TMPDIR%\mailhog.exe" (
  echo. && echo Downloading Mailhog v%ver_mailhog% ...
  %CURL% -L# "https://github.com/mailhog/MailHog/releases/download/v%ver_mailhog%/MailHog_windows_amd64.exe" -o "%TMPDIR%\mailhog.exe"
  %CURL% -L# "https://github.com/mailhog/mhsendmail/releases/download/v%ver_mhsendmail%/mhsendmail_windows_amd64.exe" -o "%TMPDIR%\mhsendmail.exe"
)
if exist "%TMPDIR%\mailhog.exe" (
  echo. && echo ^> Extracting Mailhog v%ver_mailhog% ...
  if not exist "%ODIR%\mailhog" mkdir "%ODIR%\mailhog" 2> NUL
  copy /Y "%TMPDIR%\mailhog.exe" "%ODIR%\mailhog\mailhog.exe" > nul
  copy /Y "%TMPDIR%\mhsendmail.exe" "%ODIR%\mailhog\mhsendmail.exe" > nul
  copy /Y "%STUB%\config\mailhogservice.xml" "%ODIR%\mailhog\mailhogservice.xml" > nul
  copy /Y "%~dp0utils\winsw.exe" "%ODIR%\mailhog\mailhogservice.exe" > nul
)

:: Adminer
if not exist "%ODIR%\opt\adminer" (
  mkdir "%ODIR%\opt\adminer" 2> NUL
  echo. && echo Downloading Adminer v%ver_adminer% ...
  copy /Y "%STUB%\opt\adminer\index.php" "%ODIR%\opt\adminer\index.php" > nul
  %CURL% -Ls "https://github.com/vrana/adminer/releases/download/v%ver_adminer%/adminer-%ver_adminer%-en.php" -o "%ODIR%\opt\adminer\adminer.php"
  %CURL% -Ls "https://raw.githubusercontent.com/vrana/adminer/master/designs/rmsoft/adminer.css" -o "%ODIR%\opt\adminer\adminer.css"
  if not exist "%ODIR%\opt\adminer\plugins" mkdir "%ODIR%\opt\adminer\plugins" 2> NUL
  %CURL% -Ls "https://raw.github.com/vrana/adminer/master/plugins/plugin.php" -o "%ODIR%\opt\adminer\plugin.php"
  %CURL% -Ls "https://raw.github.com/vrana/adminer/master/plugins/foreign-system.php" -o "%ODIR%\opt\adminer\plugins\foreign-system.php"
  %CURL% -Ls "https://raw.github.com/vrana/adminer/master/plugins/login-servers.php" -o "%ODIR%\opt\adminer\plugins\login-servers.php"
  %CURL% -Ls "https://raw.github.com/vrana/adminer/master/plugins/database-hide.php" -o "%ODIR%\opt\adminer\plugins\database-hide.php"
  %CURL% -Ls "https://raw.github.com/vrana/adminer/master/plugins/edit-foreign.php" -o "%ODIR%\opt\adminer\plugins\edit-foreign.php"
  %CURL% -Ls "https://raw.github.com/vrana/adminer/master/plugins/dump-zip.php" -o "%ODIR%\opt\adminer\plugins\dump-zip.php"
)

:: mkcert
if not exist "%TMPDIR%\mkcert.exe" (
  echo. && echo Downloading mkcert v%ver_mkcert% ...
  %CURL% -L# "https://github.com/FiloSottile/mkcert/releases/download/v%ver_mkcert%/mkcert-v%ver_mkcert%-windows-amd64.exe" -o "%TMPDIR%\mkcert.exe"
)
if exist "%TMPDIR%\mkcert.exe" ( copy /Y "%TMPDIR%\mkcert.exe" "%ODIR%\utils\mkcert.exe" > nul )

echo. && echo ^> Include extra utilities ...
copy /Y "%~dp0utils\7za.dll" "%ODIR%\utils\7za.dll" > nul
copy /Y "%~dp0utils\7za.exe" "%ODIR%\utils\7za.exe" > nul
copy /Y "%~dp0utils\7zxa.dll" "%ODIR%\utils\7zxa.dll" > nul
copy /Y "%~dp0utils\curl.exel" "%ODIR%\utils\curl.exe" > nul
copy /Y "%~dp0utils\curl-ca-bundle.crtll" "%ODIR%\utils\curl-ca-bundle.crt" > nul
copy /Y "%~dp0utils\libcurl-x64.dll" "%ODIR%\utils\libcurl-x64.dll" > nul

:: Cleanup unused files
echo. && echo ^> Cleanup unused files ...
forfiles /p "%ODIR%" /s /m *.pdb /d -1 /c "cmd /c del /F @file"
goto :menu

:: ---------------------------------------------------------------------------------------------------------------------
:compile_app
echo. && echo ^> Compiling Varlet App ... && echo.
if not exist "%~dp0source\packages" (
  echo. && echo Installing Nuget packages ... && echo.
  "%~dp0utils\nuget.exe" install "%~dp0source\VarletCli\packages.config" -OutputDirectory "%~dp0source\packages" > nul
  "%~dp0utils\nuget.exe" install "%~dp0source\VarletUi\packages.config" -OutputDirectory "%~dp0source\packages" > nul
)

if exist "%programfiles%\JetBrains\JetBrains Rider 2019.2.3\tools\MSBuild\Current\Bin\MSBuild.exe" (
  "%programfiles%\JetBrains\JetBrains Rider 2019.2.3\tools\MSBuild\Current\Bin\MSBuild.exe" "%~dp0source\Varlet.sln" /p:Configuration=Release /verbosity:minimal -nologo
) else (
  "%HOMEDRIVE%\SDK\JetMSBuild\MSBuild\15.0\Bin\MSBuild.exe" "%~dp0source\Varlet.sln" /p:Configuration=Release /verbosity:minimal -nologo
)
copy /Y "%~dp0source\_release\varlet.exe" "%ODIR%\utils\varlet.exe" > nul
copy /Y "%~dp0source\_release\VarletUi.exe" "%ODIR%\VarletUi.exe" > nul
echo. && goto :menu

:: ---------------------------------------------------------------------------------------------------------------------
:compile_inno
echo. && echo ^> Compiling installer files ...
"%programfiles(x86)%\Inno Setup 6\ISCC.exe" /Qp "%~dp0installer.iss"
echo. && echo. && echo Setup file has been created!
echo. && goto :menu

:: ---------------------------------------------------------------------------------------------------------------------
:install_msbuild
echo. && echo ^> Installing JetBrains MSBuild ...
if not exist "%TMP%\JetMSBuild.zip" ( %CURL% -L# "https://jb.gg/msbuild" -o "%TMP%\JetMSBuild.zip" )
if exist "%TMP%\JetMSBuild.zip" ( %UNZIP% x "%TMP%\JetMSBuild.zip" -o"%HOMEDRIVE%\SDK\JetMSBuild" -y > nul )
echo. && echo ^> JetBrains MSBuild has been installed!
echo. && goto :menu

:: ---------------------------------------------------------------------------------------------------------------------
:clean_packages
echo. && echo ^> Removing old packages ...
if exist "%TMPDIR%" RD /S /Q "%TMPDIR%"
if exist "%ODIR%" RD /S /Q "%ODIR%"
echo. && goto :menu

:: ---------------------------------------------------------------------------------------------------------------------
:quit
echo. && echo ^> Done, good bye. && echo.
