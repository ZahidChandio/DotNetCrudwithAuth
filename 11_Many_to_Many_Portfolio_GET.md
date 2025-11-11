- Create a `PortfolioController` inside `Controllers` folder.
- Create a `Extensions`.
- Add `ClaimsExtensions.cs` inside `Extensions` folder.
- Create an `IPortfolioRepository` `interface` inside `Interfaces` folder.
- Create a `PortfolioRepository` `class` inside `Repositories` folder.
- Add 
```csharp
  builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
```
to the `Program.cs` file.