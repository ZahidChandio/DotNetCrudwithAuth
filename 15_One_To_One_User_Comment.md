- Add
```cs
  private readonly UserManager<AppUser> _userManager;
  // Add this in th CommentController constructor
  _userManager = userManager;
  // Add this in th CommentController Create method
  var username = User.GetUsername();
  var appUser = await _userManager.FindByNameAsync(username);
  commentModel.AppUserId = appUser.Id;
  // Add this in the CommentRepository (GetComments method)
  .Include(a => a.AppUser).

```
to Comment controller.

- `ThenInclude` to handle nested user inside stock (stock.comment.user).

- Add `.ThenInclude(a=>a.AppUser)` (GetStocks method) to StockRepository.
        
- Add `public string CreatedBy { get; set; } = string.Empty;` to CommentDto.

- Add `CreatedBy = commentModel.AppUser.UserName` to CommentMapper.

- Add `.ToList()` to `var stockDto = stocks.Select(s => s.ToStockDto())` inside StockController 