echo off

cls
color 1f
echo Checking for Administrator elevation.

openfiles>nul 2>&1

if %errorlevel% EQU 0 goto isadmin

COLOR 4f
echo.    You are not running as Administrator.
echo.    This tool cannot do it's job without elevation.
echo.
echo.    You need run this tool as Administrator.
echo.

echo.Press any key to continue . . .
pause
exit

:isadmin
if exist c:\windows\assembly\GAC_MSIL\office\14.0.0.0__71e9bce111e9429c\OFFICE.DLL set officever=14
if exist c:\windows\assembly\GAC_MSIL\office\15.0.0.0__71e9bce111e9429c\OFFICE.DLL set officever=15
if exist c:\windows\assembly\GAC_MSIL\office\16.0.0.0__71e9bce111e9429c\OFFICE.DLL set officever=16

md c:\windows\assembly\GAC_MSIL\office\12.0.0.0__71e9bce111e9429c
xcopy c:\windows\assembly\GAC_MSIL\office\%officever%.0.0.0__71e9bce111e9429c c:\windows\assembly\GAC_MSIL\office\12.0.0.0__71e9bce111e9429c /s/y

pause
