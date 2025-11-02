- Create an interface ITokenService.
- Create a Service Folder inside api.
- Create TokenService class inside Service folder.
- Create a NewUserDto class inside DTOs folder.
- Go to AccountController Add
  `private readonly ITokenService _tokenService;`

  Update

  ```csharp
    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
    {
      _userManager = userManager;
      _tokenService = tokenService;
    }
  ```

  Update `return Ok("User Created");` to

  ```csharp
  return Ok(new NewUserDto
  {
      Username = appUser.UserName,
      Email = appUser.Email,
      Token = _tokenService.CreateToken(appUser)
  });
  ```

- Update the `SigningKey` in the `appsettings.json`, so that it's long enough for the `_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));`. This error will be thrown otherwise `the key size must be greater than: '512' bits, key has 'actual_size_will_be_displayed_here' bits."`
