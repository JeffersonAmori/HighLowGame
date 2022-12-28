using HighLowGame.Hubs;
using HighLowGameMaster;
using HighLowGameMaster.Extensions.AspNetCore;
using LoggerAdapter.Extensions;
using RandomnessService;
using RandomnessService.Providers;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

try
{
    Log.Information("Starting web application.");

    // Add services to the container.
    builder.Services.AddRazorPages();
	builder.Services.AddSignalR();
	builder.Services.AddGameMaster(builder.Configuration);
	builder.Services.AddTransient<GameMasterFactory>();
	builder.Services.Configure<GameMasterSettings>(
		builder.Configuration.GetSection(nameof(GameMasterSettings.GameMaster)));
	builder.Services.AddSingleton<IRandomnessService>(
		new NeverRepeatRandomnessService(new PeanutButterProvider()));

    builder.Host.UseGenericLogAdapter();
    builder.Host.UseSerilog();

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
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}
