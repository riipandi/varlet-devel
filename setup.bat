@echo off

set CURL=%~dp0utils\curl.exe
set UNZIP=%~dp0utils\7za.exe
set TMPDIR=%~dp0_temp
set ODIR=%~dp0_output
set STUB=%~dp0stubs

:: Packages version
set "ver_composer=1.9.1"
set "ver_httpd=2.4.41"
set "ver_nginx=1.17.6"
set "ver_openssl=1.1.1e"
set "ver_php72=7.2.25"
set "ver_php73=7.3.12"
set "ver_php74=7.4.0"
set "ver_imagick=7.0.7-11"
set "ver_php_imagick=3.4.3"
set "ver_phpredis=5.1.1"
set "ver_xdebug=2.8.0"
set "ver_mkcert=1.4.1"
set "ver_adminer=4.7.5"

:: Download link
set "url_php72=https://windows.php.net/downloads/releases/php-%ver_php72%-Win32-VC15-x64.zip"
set "url_php73=https://windows.php.net/downloads/releases/php-%ver_php73%-Win32-VC15-x64.zip"
set "url_php74=https://windows.php.net/downloads/releases/php-%ver_php74%-Win32-vc15-x64.zip"

set "url_imagick_php72=https://windows.php.net/downloads/pecl/snaps/imagick/%ver_php_imagick%/php_imagick-%ver_php_imagick%-7.2-ts-vc15-x64.zip"
set "url_imagick_php73=https://windows.php.net/downloads/pecl/snaps/imagick/%ver_php_imagick%/php_imagick-%ver_php_imagick%-7.3-ts-vc15-x64.zip"
set "url_imagick_php74=https://windows.php.net/downloads/pecl/snaps/imagick/%ver_php_imagick%/php_imagick-%ver_php_imagick%-7.4-ts-vc15-x64.zip"
set "url_imagick=http://windows.php.net/downloads/pecl/deps/ImageMagick-%ver_imagick%-vc15-x64.zip"

set "url_phpredis_php72=https://windows.php.net/downloads/pecl/releases/redis/%ver_phpredis%/php_redis-%ver_phpredis%-7.2-ts-vc15-x64.zip"
set "url_phpredis_php73=https://windows.php.net/downloads/pecl/releases/redis/%ver_phpredis%/php_redis-%ver_phpredis%-7.3-ts-vc15-x64.zip"
set "url_phpredis_php74=https://windows.php.net/downloads/pecl/releases/redis/%ver_phpredis%/php_redis-%ver_phpredis%-7.4-ts-vc15-x64.zip"

set "url_xdebug_php72=https://xdebug.org/files/php_xdebug-%ver_xdebug%beta2-7.2-vc15-x86_64.dll"
set "url_xdebug_php73=https://xdebug.org/files/php_xdebug-%ver_xdebug%-7.3-vc15-x86_64.dll"
set "url_xdebug_php74=https://xdebug.org/files/php_xdebug-%ver_xdebug%-7.4-vc15-x86_64.dll"

:: Main components
:: -----------------------------------------------------------------------------------------------

if not exist "%ODIR%" mkdir "%ODIR%" 2> NUL
if not exist "%TMPDIR%" mkdir "%TMPDIR%" 2> NUL
if not exist "%ODIR%\utils" mkdir "%ODIR%\utils" 2> NUL

:: VCRedist 2012 + 2015-2019
set "URL_VCREDIST_1519=https://aka.ms/vs/16/release/VC_redist.x64.exe"
set "URL_VCREDIST_2012=https://download.microsoft.com/download/1/6/B/16B06F60-3B20-4FF2-B699-5E9B7962F9AE/VSU_4/vcredist_x64.exe"
if not exist "%TMPDIR%\vcredis\" (
  echo. && echo Downloading Visual C++ Redistributable ...
  if not exist "%TMPDIR%\vcredis" mkdir "%TMPDIR%\vcredis" 2> NUL
  %CURL% -L# %URL_VCREDIST_2012% -o "%TMPDIR%\vcredis\vcredis2012x64.exe"
  %CURL% -L# %URL_VCREDIST_1519% -o "%TMPDIR%\vcredis\vcredis1519x64.exe"
)

:: OpenSSL
REM if not exist "%TMPDIR%\openssl-%ver_openssl%.zip" (
REM   echo. && echo Downloading OpenSSL v%ver_openssl% ...
REM   %CURL% -L# "https://mirror.firedaemon.com/OpenSSL/openssl-%ver_openssl%-dev.zip" -o "%TMPDIR%\openssl-%ver_openssl%.zip"
REM )
REM if exist "%TMPDIR%\openssl-%ver_openssl%.zip" (
REM   echo. && echo Extracting OpenSSL v%ver_openssl% ...
REM   if exist "%ODIR%\openssl" RD /S /Q "%ODIR%\openssl"
REM   if exist "%TMPDIR%\openssl-1.1" RD /S /Q "%TMPDIR%\openssl-1.1"
REM   %UNZIP% x "%TMPDIR%\openssl-%ver_openssl%.zip" -o"%TMPDIR%" -y > nul
REM   xcopy %TMPDIR%\openssl-1.1\x64 %ODIR%\openssl /E /I /Y > nul
REM )

:: Apache HTTPd
if not exist "%TMPDIR%\httpd-%ver_httpd%.zip" (
  echo. && echo Downloading Apache HTTPd v%ver_httpd% ...
  %CURL% -L# "https://home.apache.org/~steffenal/VC15/binaries/httpd-%ver_httpd%-win64-VC15.zip" -o "%TMPDIR%\httpd-%ver_httpd%.zip"
)
if exist "%TMPDIR%\httpd-%ver_httpd%.zip" (
  echo. && echo Extracting Apache HTTPd v%ver_httpd% ...
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

:: mkcert
if not exist "%TMPDIR%\mkcert.exe" (
  echo. && echo Downloading mkcert v%ver_mkcert% ...
  %CURL% -L# "https://github.com/FiloSottile/mkcert/releases/download/v%ver_mkcert%/mkcert-v%ver_mkcert%-windows-amd64.exe" -o "%TMPDIR%\mkcert.exe"
)
if exist "%TMPDIR%\mkcert.exe" ( copy /Y "%TMPDIR%\mkcert.exe" "%ODIR%\utils\mkcert.exe" > nul )

:: Winsw PHP + Nginx
REM if exist "%ODIR%\phpfpmservice.exe" ( del /F "%ODIR%\phpfpmservice.exe" )
REM if not exist "%ODIR%\phpfpmservice.exe" ( copy /Y "%~dp0utils\winsw.exe" "%ODIR%\phpfpmservice.exe" > nul )
REM if exist "%ODIR%\nginxservice.exe" ( del /F "%ODIR%\nginxservice.exe" )
REM if not exist "%ODIR%\nginxservice.exe" ( copy /Y "%~dp0utils\winsw.exe" "%ODIR%\nginxservice.exe" > nul )

:: Nginx
REM if not exist "%TMPDIR%\nginx-%ver_nginx%.zip" (
REM   echo. && echo Downloading Nginx v%ver_nginx% ...
REM   %CURL% -L# "http://nginx.org/download/nginx-%ver_nginx%.zip" -o "%TMPDIR%\nginx-%ver_nginx%.zip"
REM )
REM if exist "%TMPDIR%\nginx-%ver_nginx%.zip" (
REM   echo. && echo Extracting NGINX v%ver_nginx% ...
REM   if exist "%ODIR%\nginx" RD /S /Q "%ODIR%\nginx"
REM   %UNZIP% x "%TMPDIR%\nginx-%ver_nginx%.zip" -o"%ODIR%" -y > nul
REM   ren "%ODIR%\nginx-%ver_nginx%" nginx
REM   RD /S /Q "%ODIR%\nginx\conf"
REM   RD /S /Q "%ODIR%\nginx\contrib"
REM   RD /S /Q "%ODIR%\nginx\html"
REM   RD /S /Q "%ODIR%\nginx\logs"
REM   xcopy %STUB%\nginx\conf %ODIR%\nginx\conf /E /I /Y > nul
REM   xcopy %STUB%\nginx\html %ODIR%\nginx\html /E /I /Y > nul
REM )

:: PHP v7.4
REM if not exist "%TMPDIR%\php-%ver_php74%.zip" (
REM   echo. && echo Downloading PHP v%ver_php74% ...
REM   %CURL% -L# %url_php74% -o "%TMPDIR%\php-%ver_php74%.zip"
REM   %CURL% -L# %url_xdebug_php74% -o "%TMPDIR%\php74_xdebug.dll"
REM   %CURL% -L# %url_imagick_php74% -o "%TMPDIR%\imagick-%ver_php_imagick%-php74.zip"
REM   %CURL% -L# %url_phpredis_php74% -o "%TMPDIR%\php74_redis.zip"
REM )
REM if exist "%TMPDIR%\php-%ver_php74%.zip" (
REM   echo. && echo Extracting PHP v%ver_php74% ...
REM   if exist "%ODIR%\php\php-7.4-ts" RD /S /Q "%ODIR%\php\php-7.4-ts"
REM   %UNZIP% x "%TMPDIR%\php-%ver_php74%.zip" -o"%ODIR%\php\php-7.4-ts" -y > nul
REM   copy /Y "%TMPDIR%\php74_xdebug.dll" "%ODIR%\php\php-7.4-ts\ext\php_xdebug.dll" > nul

REM   %UNZIP% x "%TMPDIR%\imagick-%ver_php_imagick%-php74.zip" -o"%ODIR%\php\php-7.4-ts" -y > nul
REM   copy /Y "%ODIR%\php\php-7.4-ts\php_imagick.dll" "%ODIR%\php\php-7.4-ts\ext\php_imagick.dll" > nul
REM   del /F "%ODIR%\php\php-7.4-ts\php_imagick.dll"

REM   %UNZIP% x "%TMPDIR%\php74_redis.zip" -o"%TMPDIR%" -y > nul
REM   copy /Y "%TMPDIR%\php_redis.dll" "%ODIR%\php\php-7.4-ts\ext\php_redis.dll" > nul
REM   del /F "%TMPDIR%\php_redis.dll"
REM )

:: PHP v7.3
if not exist "%TMPDIR%\php-%ver_php73%.zip" (
  echo. && echo Downloading PHP v%ver_php73% ...
  %CURL% -L# %url_php73% -o "%TMPDIR%\php-%ver_php73%.zip"
  %CURL% -L# %url_xdebug_php73% -o "%TMPDIR%\php73_xdebug.dll"
  %CURL% -L# %url_phpredis_php73% -o "%TMPDIR%\php73_redis.zip"
  %CURL% -L# %url_imagick_php73% -o "%TMPDIR%\imagick-%ver_php_imagick%-php73.zip"
)
if exist "%TMPDIR%\php-%ver_php73%.zip" (
  echo. && echo Extracting PHP v%ver_php73% ...
  if exist "%ODIR%\php\php-7.3-ts" RD /S /Q "%ODIR%\php\php-7.3-ts"
  %UNZIP% x "%TMPDIR%\php-%ver_php73%.zip" -o"%ODIR%\php\php-7.3-ts" -y > nul
  copy /Y "%TMPDIR%\php73_xdebug.dll" "%ODIR%\php\php-7.3-ts\ext\php_xdebug.dll" > nul

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
)
if exist "%TMPDIR%\php-%ver_php72%.zip" (
  echo. && echo Extracting PHP v%ver_php72% ...
  if exist "%ODIR%\php\php-7.2-ts" RD /S /Q "%ODIR%\php\php-7.2-ts"
  %UNZIP% x "%TMPDIR%\php-%ver_php72%.zip" -o"%ODIR%\php\php-7.2-ts" -y > nul
  copy /Y "%TMPDIR%\php72_xdebug.dll" "%ODIR%\php\php-7.2-ts\ext\php_xdebug.dll" > nul

  %UNZIP% x "%TMPDIR%\php72_redis.zip" -o"%TMPDIR%" -y > nul
  copy /Y "%TMPDIR%\php_redis.dll" "%ODIR%\php\php-7.2-ts\ext\php_redis.dll" > nul
  del /F "%TMPDIR%\php_redis.dll"

  %UNZIP% x "%TMPDIR%\imagick-%ver_php_imagick%-php72.zip" -o"%ODIR%\php\php-7.2-ts" -y > nul
  copy /Y "%ODIR%\php\php-7.2-ts\php_imagick.dll" "%ODIR%\php\php-7.2-ts\ext\php_imagick.dll" > nul
  del /F "%ODIR%\php\php-7.2-ts\php_imagick.dll"
)

:: ImageMagick
if not exist "%TMPDIR%\imagick-%ver_imagick%.zip" (
  echo. && echo Downloading ImageMagick v%ver_imagick% ...
  %CURL% -L# %url_imagick% -o "%TMPDIR%\imagick-%ver_imagick%.zip"
)
if exist "%TMPDIR%\imagick-%ver_imagick%.zip" (
  echo. && echo Extracting ImageMagick v%ver_imagick% ...
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
  echo. && echo Extracting ionCube loader ...
  if exist "%TMPDIR%\ioncube" RD /S /Q "%TMPDIR%\ioncube"
  %UNZIP% x "%TMPDIR%\ioncube-vc15.zip" -o"%TMPDIR%" -y > nul
  copy /Y "%TMPDIR%\ioncube\ioncube_loader_win_7.2.dll" "%ODIR%\php\php-7.2-ts\ext\php_ioncube.dll" > nul
  copy /Y "%TMPDIR%\ioncube\ioncube_loader_win_7.3.dll" "%ODIR%\php\php-7.3-ts\ext\php_ioncube.dll" > nul
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

:: "%WINDIR%\Microsoft.NET\FrameWork64\v4.0.30319\MSBuild.exe"
:: "%programfiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\amd64\MSBuild.exe"
if not exist "%~dp0source\packages" (
  echo. && echo Installing Nuget packages ...
  "%~dp0utils\nuget.exe" install "%~dp0source\VarletCli\packages.config" -OutputDirectory "%~dp0source\packages" > nul
  "%~dp0utils\nuget.exe" install "%~dp0source\VarletUi\packages.config" -OutputDirectory "%~dp0source\packages" > nul
)
echo. && echo Compiling Varlet App ... && echo.
"%programfiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" -nologo "%~dp0source\Varlet.sln" /p:Configuration=Release /p:Platform=x64 /verbosity:minimal
copy /Y "%~dp0source\_build\x64\Release\varlet.exe" "%ODIR%\utils\varlet.exe" > nul
copy /Y "%~dp0source\_build\x64\Release\VarletUi.exe" "%ODIR%\VarletUi.exe" > nul

echo. && echo Include extra utilities ...
copy /Y "%~dp0utils\7za.dll" "%ODIR%\utils\7za.dll" > nul
copy /Y "%~dp0utils\7za.exe" "%ODIR%\utils\7za.exe" > nul
copy /Y "%~dp0utils\7zxa.dll" "%ODIR%\utils\7zxa.dll" > nul
copy /Y "%~dp0utils\curl.exel" "%ODIR%\utils\curl.exe" > nul
copy /Y "%~dp0utils\curl-ca-bundle.crtll" "%ODIR%\utils\curl-ca-bundle.crt" > nul
copy /Y "%~dp0utils\libcurl-x64.dll" "%ODIR%\utils\libcurl-x64.dll" > nul

:: Cleanup unused files
echo. && echo Cleanup unused files ...
forfiles /p "%ODIR%" /s /m *.pdb /d -1 /c "cmd /c del /F @file"

:: ---------------------------------------------------------------------------------------------------------------------

:choice
set /P c="Do you want to compile installer script? [y/N] : "
if /I "%c%" EQU "Y" goto :compile_inno
if /I "%c%" EQU "N" goto :quit
goto :choice

:compile_inno
echo. && echo Compiling installer files ...
"%programfiles(x86)%\Inno Setup 6\ISCC.exe" /Qp "%~dp0installer.iss"
echo. && echo Setup file has been created! && echo.
pause
exit

:quit
echo. && echo All files already downloaded! && echo.
pause
