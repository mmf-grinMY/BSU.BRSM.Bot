using BSU.BRSM.Bot.App;
using BSU.BRSM.Bot.Data.EF;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRazorPages()
    .AddNewtonsoftJson()
    .AddRazorRuntimeCompilation();

builder.Services
    .AddDistributedMemoryCache()
    .AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(20);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    })
    .AddEfRepositories(builder.Configuration.GetConnectionString("BotDBString"))
    .AddSingleton<BrsmBot>();

var app = builder.Build();

app
    .UseStatusCodePagesWithReExecute("/{0}") // повторное выполнение конвейера всякий раз, когда статус кодом будет являться ошибка сервера или клиента
    .UseDeveloperExceptionPage()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthorization()
    .UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
