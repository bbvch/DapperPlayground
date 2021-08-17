FROM mysql:latest

COPY ../db/northwind/mysql/northwind.sql /usr/share/northwind.sql
COPY ../db/northwind/mysql/northwind-data.sql /usr/share/northwind-data.sql