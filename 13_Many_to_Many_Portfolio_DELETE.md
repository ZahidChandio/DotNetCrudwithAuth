Steps:
- Get the user (identity).
- Get the portfolio (Portfolio repo).
- Filter
- Delete

--------
- Add 
```cs
Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol);
```
to IPortfolioRepository.
- Add `DeletePortfolio` method to `PortfolioRepository`.
- Add `DeletePortfolio` method to `PortfolioController`.
