using NewsApiService.Services;
using Quartz;
using Microsoft.Extensions.Logging;
using NewsApiService.Interfaces;

namespace NewsApiService.Jobs
{
	public class ProcessNewsJob(ILoggerFactory loggerFactory, INewsProcessorTask newsProcessorTask) : IJob
	{
		private readonly ILogger<ProcessNewsJob> _logger = loggerFactory.CreateLogger<ProcessNewsJob>();
		private readonly INewsProcessorTask _newsProcessorTask = newsProcessorTask;

		public async Task Execute(IJobExecutionContext context)
		{
			_logger.LogInformation($"{nameof(NewsProccesorTask)} Start");

			await _newsProcessorTask.Execute();

			_logger.LogInformation($"{nameof(NewsProccesorTask)} End");
		}
	}
}
