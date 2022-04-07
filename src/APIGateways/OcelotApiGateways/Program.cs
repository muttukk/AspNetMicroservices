using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot().AddCacheManager(settings=>settings.WithDictionaryHandle());

ConfigurationManager configurationManager = builder.Configuration;

builder.Host.ConfigureAppConfiguration((hostingConext, config) =>
{
    config.AddJsonFile($"ocelot.{hostingConext.HostingEnvironment.EnvironmentName}.json", true, true);
});

//builder.Host.ConfigureLogging((hostingConext, logging) =>
//{
//    logging.AddConfiguration(hostingConext.Configuration.GetSection("Logging"));
//    logging.AddConsole();
//    logging.AddDebug();
//});

var app = builder.Build();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
});
app.UseOcelot();
app.Run();
