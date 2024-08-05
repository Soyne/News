using Microsoft.Extensions.Logging;

public static class StaticLogger
{
	private static readonly ILoggerFactory LoggerFactory;
	public static readonly ILogger Instance;

	static StaticLogger()
	{
		LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
		{
			builder.AddConsole();
			builder.SetMinimumLevel(LogLevel.Information);
		});

		Instance = LoggerFactory.CreateLogger("StaticLogger");
	}

	public static void LogError(string message)
	{
		Instance.LogError(message);
	}
}
