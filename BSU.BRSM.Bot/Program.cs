var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddMvcCore();
builder.Services
    .AddRazorPages()
    .AddNewtonsoftJson()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

app
    .UseStatusCodePagesWithReExecute("/{0}") // ��������� ���������� ��������� ������ ���, ����� ������ ����� ����� �������� ������ ������� ��� �������
    .UseDeveloperExceptionPage()
    //.UseExceptionHandler("/Error")
    .UseStaticFiles()
    .UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapGet("/test", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Bot}/{action=Index}");

app.Run();
