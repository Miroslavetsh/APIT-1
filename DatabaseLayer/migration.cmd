@echo off

if defined %1 ( set migration_name=%1 ) else ( echo Migration name not defined && goto end: )
if defined %2 ( set "opt=%2" ) else ( set opt=[]  )

dotnet ef migrations add %migration_name% %opt%
dotnet ef database update

:end
