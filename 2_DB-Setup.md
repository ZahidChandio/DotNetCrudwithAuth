- Create "Data" folder inside api

<!-- SQL Server Setup -->

- Open the terminal and pull sql image (for mac we need to use docker Resource: https://builtin.com/software-engineering-perspectives/sql-server-management-studio-mac)
  `sudo docker pull mcr.microsoft.com/mssql/server:2022-latest`

- Run this command to run the SQL server using pulled docker image
  `docker run -d --name sql_server_test -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=dockerStrongPwd123' -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest`

- Install https://open-vsx.org/extension/cweijan/dbclient-jdbc and https://open-vsx.org/extension/cweijan/vscode-mysql-client2 cursor/vs-code extensions for sql server connection

- Update appsettings.json with the DB connection string

  ```
    "ConnectionStrings": {
      "DefaultConnection": "Data Source=localhost;Initial Catalog=finshark;User Id=sa;Password=test@123;Integrated Security=True;TrustServerCertificate=true;Trusted_Connection=false"
    },
  ```

- Add DB context to Program.cs (Add this before builder.Build() command)

  ```
    builder.Services.AddDbContext<ApplicationDBContext>( options => {
      options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
  ```

- Run `dotnet tool install --global dotnet-ef --version 9.0.0`

- Run `dotnet ef migrations add Init`
- Run `dotnet ef database update`
