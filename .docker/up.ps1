docker compose -f (Join-Path $PSScriptRoot docker-compose.yml) up -d --build

docker exec dapperplayground-mssql-1 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "Change_Me" -i /usr/share/instnwnd.sql

docker exec dapperplayground-mysql-1 sh -c "mysql -uroot -pChange_Me < /usr/share/northwind.sql"
docker exec dapperplayground-mysql-1 sh -c "mysql -uroot -pChange_Me < /usr/share/northwind-data.sql"