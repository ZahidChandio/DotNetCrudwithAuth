- Open nuget gallery extension and install `Identity.core (by microsoft)`, `Microsoft.AspNetCore.Identity.EntityFrameworkCore (by ASP.NET)`, and `Microsoft.AspNetCore.Authentication.JwtBearer` extensions

- Go to Models -> Add AppUser.cs class

- Update the "ApplicationDBContext" base class from "DbContext" to "IdentityDbContext<AppUser>" inside the "data/ApplicationDBContext".

- Add Identity Service

  - Go to program.cs and add
    `builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
  options.Password.RequireDigit = true;
  options.Password.RequireLowercase = true;
  options.Password.RequireLowercase = true;
  options.Password.RequireUppercase = true;
  options.Password.RequireNonAlphanumeric = true;
  options.Password.RequiredLength = 12;  
}).AddEntityFrameworkStores<ApplicationDBContext>();`

- Add Authentication Service & Schemes

  - Go to program.cs and add
    `builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = BearerTokenDefaults.AuthenticationScheme;
    }).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
    ValidateIssuer = true,
    ValidIssuer = builder.Configuration["JWT:Issuer"],
    ValidateAudience = true,
    ValidAudience = builder.Configuration["JWT:Audience"],
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]))
    };
    });

- Sdd `"JWT": { "Issuer": "http://localhost:5246", "Audience": "http://localhost:5246", "SignInKey": "add_very_secure_string"}` to `appsettings.json`

- Add `app.UseAuthentication();` and `app.UseAuthorization();` to `program.cs` under the `app.UseHttpsRedirection();`

- Run the migration commands
  - dotnet ef migrations add identity
  - dotnet ef database update
