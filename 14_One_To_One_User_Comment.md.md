- Add
```cs
  public string AppUserId { get; set; }
  public AppUser AppUser { get; set; }
```
to Comment model.

- Run `dotnet ef migrations add CommentOneToOne` to add migration.
- Run `dotnet ef database update` to update database.