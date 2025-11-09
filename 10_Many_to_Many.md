- Create a `Portfolio` Model inside `Models` folder
- Add

```csharp
  public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
```

to the `AppUser` and `Stock` models.

- Add

```csharp
      public DbSet<Portfolio> Portfolios { get; set; }
      // Inside the `OnModelCreating` method after the base.OnModelCreating(builder); line.
      builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));

      builder.Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);

      builder.Entity<Portfolio>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.StockId);
```

to ApplicationDbContext.

- We can delete the `migrations` folder, as there might be warnings after the M-M relationship is created. Avoid this if you are in a team.

- Delete the Database and re-create it. Avoid this if you are in a team.

- Run `dotnet ef migrations add PortfolioManyToMany` to create the migration.

- Run `dotnet ef database update` to apply the migration.
