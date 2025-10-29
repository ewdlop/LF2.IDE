using LF2IDE.BlazorServer.Components;
using LF2IDE.BlazorServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add LF2 IDE services
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<ILF2DataService, LF2DataService>();
builder.Services.AddScoped<EditorStateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

