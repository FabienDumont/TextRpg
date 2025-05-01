using TextRpg.Application;
using TextRpg.Blazor.Components;
using TextRpg.Blazor.Stores;
using TextRpg.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddInfrastructure("Data/database.db");
builder.Services.AddApplication();
builder.Services.AddScoped<GameSaveStore>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

  var canConnect = false;
  try
  {
    canConnect = await db.Database.CanConnectAsync();
  }
  catch (Exception ex)
  {
    Console.WriteLine($"[DB] Connection failed: {ex.Message}");
  }

  if (canConnect)
  {
    try
    {
      await db.InitializeDataAsync();
      Console.WriteLine("[DB] Database initialized.");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"[DB] Initialization failed: {ex.Message}");
    }
  }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
