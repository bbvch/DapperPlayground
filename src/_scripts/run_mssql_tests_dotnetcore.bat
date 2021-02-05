@ECHO off
SET runnerDir=%~dp0..\packages\xunit.runner.console.2.4.1\tools\netcoreapp2.0
SET binDir=%~dp0..\DapperPlayground.MsSql.DotNetCoreTest\bin\Debug

ECHO ------
ECHO For this script to work the assemblies need to be compiled in:
ECHO %binDir%
ECHO ------

%runnerDir%\xunit.console.exe %binDir%\DapperPlayground.MsSql.DotNetCoreTest.dll -parallel none -diagnostics