Register (User/Admin registeration)

- Create an AccountController.cs
- Create RegistorDto.cs
- Update ApplicationDBContext.cs with OnModelCreating
- Run the migrations `dotnet ef migrations add SeedRole`. We can assign any name for `SeedRole`.
- Run `dotnet ef database update`
