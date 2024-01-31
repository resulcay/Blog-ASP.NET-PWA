@echo off
:: BatchGotAdmin
:-------------------------------------
REM  --> Check for permissions
>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"

REM --> If error flag set, we do not have admin.
if '%errorlevel%' NEQ '0' (
    echo Requesting administrative privileges...
    goto UACPrompt
) else ( goto gotAdmin )

:UACPrompt
    echo Set UAC = CreateObject^("Shell.Application"^) > "%temp%\getadmin.vbs"
    echo UAC.ShellExecute "%~s0", "", "", "runas", 1 >> "%temp%\getadmin.vbs"
    "%temp%\getadmin.vbs"
    exit /B

:gotAdmin
    if exist "%temp%\getadmin.vbs" (del "%temp%\getadmin.vbs")
    echo Administrative privileges acquired.
    :: Continue with your script below this line

:: Change directory to the specified path
cd C:\Windows\Microsoft.NET\Framework\v4.0.30319

:: Run InstallUtil.exe with the provided service path
InstallUtil.exe C:\CustomWindowsServices\BlogReportService\ReporterWindowsService.exe

:: Pause to keep the command prompt open (optional)
pause