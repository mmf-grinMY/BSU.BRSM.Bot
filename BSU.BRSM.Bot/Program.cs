var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvcCore();
builder.Services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Bot}/{action=Index}/{id?}");

app.Run();
