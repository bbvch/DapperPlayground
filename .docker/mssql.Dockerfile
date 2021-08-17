FROM mcr.microsoft.com/mssql/server:2019-latest

COPY ../db/northwind/mssql/instnwnd.sql /usr/share/instnwnd.sql