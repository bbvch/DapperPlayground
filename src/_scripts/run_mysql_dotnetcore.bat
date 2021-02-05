@ECHO off
SET binDir=%~dp0..\DapperPlayground.MsSql.DotNetCoreTest\bin\Debug\netcoreapp3.1

ECHO ------
ECHO For this script to work the assemblies need to be compiled in:
ECHO %binDir%
ECHO ------

dotnet %binDir%\DapperPlayground.MySql.DotNetCoreTest.dll