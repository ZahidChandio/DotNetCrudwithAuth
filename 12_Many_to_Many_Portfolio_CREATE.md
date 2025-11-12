- Add `AddPortfolio` method to `PortfolioController`.
- Add 
```csharp
  Task<Stock?> GetBySymbolAsync(string symbol);
```
to the IStockRepository.
- Add `GetBySymbolAsync` method to `StockRepository`.
- Add 
```csharp 
  Task<Portfolio> CreateAsync(Portfolio portfolio);
```
to the IPortfolioRepository.
- Add `CreateAsync` method to `PortfolioRepository`.