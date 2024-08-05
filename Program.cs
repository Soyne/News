using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NewsApiService.Interfaces;
using NewsApiService.Middleware;
using NewsApiService.Services;
using NewsApiService.Utilities;

public class Program
{
	static async Task Main(string[] args)
	{
		Console.WriteLine(Directory.GetCurrentDirectory());

		var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

		var configuration = builder.Build();

		if (configuration == null)
			throw new NullReferenceException(nameof(configuration));

		var host = Host.CreateDefaultBuilder(args)
			  .ConfigureServices((context, services) =>
			  {
				  services.Configure<GeneralOptions>(configuration.GetRequiredSection("General"));

				  services.AddCQRSCorePipelines();
				  services.AddScoped<INewsProcessorTask, NewsProccesorTask>();
				  services.AddScoped<IApiClient, ApiClient>();

				  services.AddLogging(config =>
				  {
					  config.ClearProviders();
					  config.AddConsole();
					  config.SetMinimumLevel(LogLevel.Information);
				  });

				  services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
				  services.AddQartz(configuration);
			  })
			  .Build();

		await host.RunAsync();
	}
}