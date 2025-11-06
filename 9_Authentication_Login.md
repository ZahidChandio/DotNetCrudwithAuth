- Create `LoginDto.cs` class in `Dtos` folder
- Add `Login` function in `AuthController.cs` class
- import in `Program.cs` file

```cs
using Microsoft.OpenApi.Models;
```

- Add below code in `Program.cs` after the `AddSwaggerGen` line, so that Swagger UI can have JWT built-in to it.

```cs
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
```

- Try adding `[Authorize]` to StockController.cs or CommentController.cs to see the difference.
- A lock icon will appear in the top right corner of the Swagger UI. Click on it and add user `Token` value.
