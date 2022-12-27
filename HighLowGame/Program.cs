using HighLowGame.Hubs;
using HighLowGameMaster;
using HighLowGameMaster.Extensions.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddGameMaster(builder.Configuration);
builder.Services.AddTransient<GameMasterFactory>();
builder.Services.Configure<GameMasterSettings>(
    builder.Configuration.GetSection(nameof(GameMasterSettings.GameMaster)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<GameHub>("/gameHub");

app.Run();
