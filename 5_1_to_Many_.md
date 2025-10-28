- Install `Newtonsoft.json` and `Microsoft.AspNetCore.Mvc.NewtonsoftJson` using the Nuget gallery extension

- Add NewtonSoft code:
  `  builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});`
