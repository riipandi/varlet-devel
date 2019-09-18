@echo off

set UNZIP=%~dp0..\_buildtools\7za.exe
set CURL=%~dp0..\_buildtools\curl.exe
set ODIR=%~dp0..\packages

:: Packages version
set "vPHP73=7.3.9"
set "vPHP72=7.2.22"
set "vPHP56=5.6.40"
set "vCOMPOSER=1.9.0"
set "vIMAGICK=3.4.3"
set "vPHPREDIS=5.0.2"

:: Download link
set "VCREDIST_1519=https://aka.ms/vs/16/release/VC_redist.x64.exe"
set "VCREDIST_2012=http://download.microsoft.com/download/1/6/B/16B06F60-3B20-4FF2-B699-5E9B7962F9AE/VSU_4/vcredist_x64.exe"

set "URL_PHP73=https://windows.php.net/downloads/releases/php-%vPHP73%-nts-Win32-VC15-x64.zip"
set "URL_PHP72=https://windows.php.net/downloads/releases/php-%vPHP72%-nts-Win32-VC15-x64.zip"
set "URL_PHP56=https://windows.php.net/downloads/releases/archives/php-%vPHP56%-nts-Win32-VC11-x64.zip"
set "URL_COMPOSER=https://getcomposer.org/download/%vCOMPOSER%/composer.phar"

set "URL_IMAGICK73=http://windows.php.net/downloads/pecl/snaps/imagick/%vIMAGICK%/php_imagick-%vIMAGICK%-7.3-nts-vc15-x64.zip"
set "URL_IMAGICK72=http://windows.php.net/downloads/pecl/snaps/imagick/%vIMAGICK%/php_imagick-%vIMAGICK%-7.2-nts-vc15-x64.zip"
set "URL_IMAGICK56=http://windows.php.net/downloads/pecl/releases/imagick/%vIMAGICK%/php_imagick-%vIMAGICK%-5.6-nts-vc11-x64.zip"

set "URL_REDISPHP73=https://windows.php.net/downloads/pecl/releases/redis/%vPHPREDIS%/php_redis-%vPHPREDIS%-7.3-nts-vc15-x64.zip"
set "URL_REDISPHP72=https://windows.php.net/downloads/pecl/releases/redis/%vPHPREDIS%/php_redis-%vPHPREDIS%-7.2-nts-vc15-x64.zip"

:: Main components
:: -----------------------------------------------------------------------------------------------

if not exist "%ODIR%" mkdir "%ODIR%" 2> NUL

:: VCRedist 2012 + 2015-2019
if not exist "%ODIR%\vcredis\" (
    echo. && echo Downloading Visual C++ Redistributable ...
    if not exist "%ODIR%\vcredis" mkdir "%ODIR%\vcredis" 2> NUL
    %CURL% -L# %VCREDIST_2012% -o "%ODIR%\vcredis\vcredis2012x64.exe"
    %CURL% -L# %VCREDIST_1519% -o "%ODIR%\vcredis\vcredis1519x64.exe"
)

:: PHP v7.3
if not exist "%TMP%\php-%vPHP73%.zip" (
    echo. && echo Downloading PHP v%vPHP73% ...
    %CURL% -L# %URL_PHP73% -o "%TMP%\php-%vPHP73%.zip"
    %CURL% -L# %URL_IMAGICK73% -o "%TMP%\imagick-%vIMAGICK%-php73.zip"
    %CURL% -L# %URL_REDISPHP73% -o "%TMP%\php73_redis.zip"
)
if exist "%TMP%\php-%vPHP73%.zip" (
    echo. && echo Extracting PHP v%vPHP73% ...
    if exist "%ODIR%\php73" RD /S /Q "%ODIR%\php73"
    %UNZIP% x "%TMP%\php-%vPHP73%.zip" -o"%ODIR%\php73" -y > nul

    %UNZIP% x "%TMP%\imagick-%vIMAGICK%-php73.zip" -o"%ODIR%\php73" -y > nul
    copy /Y "%ODIR%\php73\php_imagick.dll" "%ODIR%\php73\ext\php_imagick.dll" > nul
    del /F "%ODIR%\php73\php_imagick.dll"

    %UNZIP% x "%TMP%\php73_redis.zip" -o"%TMP%" -y > nul
    copy /Y "%TMP%\php_redis.dll" "%ODIR%\php73\ext\php_redis.dll" > nul
    del /F "%TMP%\php_redis.dll"
)

:: PHP v7.2
if not exist "%TMP%\php-%vPHP72%.zip" (
    echo. && echo Downloading PHP v%vPHP72% ...
    %CURL% -L# %URL_PHP72% -o "%TMP%\php-%vPHP72%.zip"
    %CURL% -L# %URL_IMAGICK72% -o "%TMP%\imagick-%vIMAGICK%-php72.zip"
    %CURL% -L# %URL_REDISPHP72% -o "%TMP%\php72_redis.zip"
)
if exist "%TMP%\php-%vPHP72%.zip" (
    echo. && echo Extracting PHP v%vPHP72% ...
    if exist "%ODIR%\php72" RD /S /Q "%ODIR%\php72"
    %UNZIP% x "%TMP%\php-%vPHP72%.zip" -o"%ODIR%\php72" -y > nul

    %UNZIP% x "%TMP%\imagick-%vIMAGICK%-php72.zip" -o"%ODIR%\php72" -y > nul
    copy /Y "%ODIR%\php72\php_imagick.dll" "%ODIR%\php72\ext\php_imagick.dll" > nul
    del /F "%ODIR%\php72\php_imagick.dll"

    %UNZIP% x "%TMP%\php72_redis.zip" -o"%TMP%" -y > nul
    copy /Y "%TMP%\php_redis.dll" "%ODIR%\php72\ext\php_redis.dll" > nul
    del /F "%TMP%\php_redis.dll"
)

:: PHP v5.6
if not exist "%TMP%\php-%vPHP56%.zip" (
    echo. && echo Downloading PHP v%vPHP56% ...
    %CURL% -L# %URL_PHP56% -o "%TMP%\php-%vPHP56%.zip"
    %CURL% -L# %URL_IMAGICK56% -o "%TMP%\imagick-%vIMAGICK%-php56.zip"
)
if exist "%TMP%\php-%vPHP56%.zip" (
    echo. && echo Extracting PHP v%vPHP56% ...
    if exist "%ODIR%\php56" RD /S /Q "%ODIR%\php56"
    %UNZIP% x "%TMP%\php-%vPHP56%.zip" -o"%ODIR%\php56" -y > nul
    %UNZIP% x "%TMP%\imagick-%vIMAGICK%-php56.zip" -o"%ODIR%\php56" -y > nul
    copy /Y "%ODIR%\php56\php_imagick.dll" "%ODIR%\php56\ext\php_imagick.dll" > nul
    del /F "%ODIR%\php56\php_imagick.dll"
)

:: Composer
if not exist "%ODIR%\composer\" (
    echo. && echo Downloading Composer v%vCOMPOSER% ...
    if not exist "%ODIR%\composer" mkdir "%ODIR%\composer" 2> NUL
    %CURL% -L# %URL_COMPOSER% -o "%ODIR%\composer\composer.phar"
)

:: Extra components
:: -----------------------------------------------------------------------------------------------

:: ionCube Loader VC15
echo. && echo Download or extracting ionCube Loader VC15 ...
if not exist "%TMP%\ioncube-vc15.zip" (
    %CURL% -L# "https://downloads.ioncube.com/loader_downloads/ioncube_loaders_win_nonts_vc15_x86-64.zip" -o "%TMP%\ioncube-vc15.zip"
)
if exist "%TMP%\ioncube-vc15.zip" ( %UNZIP% x "%TMP%\ioncube-vc15.zip" -o"%ODIR%" -y > nul )

:: ionCube Loader VC11
echo. && echo Download or extracting ionCube Loader VC11 ...
if not exist "%TMP%\ioncube-vc11.zip" (
    %CURL% -L#      "https://downloads.ioncube.com/loader_downloads/ioncube_loaders_win_nonts_vc11_x86-64.zip" -o "%TMP%\ioncube-vc11.zip"
)
if exist "%TMP%\ioncube-vc11.zip" ( %UNZIP% x "%TMP%\ioncube-vc11.zip" -o"%ODIR%" -y > nul )

:: Cleanup unused files
echo. && echo Cleanup unused files ...
forfiles /p "%ODIR%" /s /m *.pdb /d -1 /c "cmd /c del /F @file"

:: Done!
:: -----------------------------------------------------------------------------------------------

echo. && echo All files already downloaded!
pause
