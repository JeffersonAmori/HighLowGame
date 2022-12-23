using HighLowGame.Hubs;
using HighLowGameMaster;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IGameMaster>(x =>
{
    var config = x.GetRequiredService<IConfiguration>();
    var minValue = config.GetValue<int>("GameMaster:MinimumValue");
    var maxValue = config.GetValue<int>("GameMaster:MaximumValue");

    return new GameMaster(minValue, maxValue);
});

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
