using MediatR;
using Microsoft.Extensions.Logging;
using NewsApiService.Interfaces;
using NewsApiService.Services.News.Queries;
using System.Text.Json;

namespace NewsApiService.Services
{
	public class NewsProccesorTask : INewsProcessorTask
	{
		private readonly IMediator _mediator;
		private readonly ILogger<NewsProccesorTask> logger;

		public NewsProccesorTask(IMediator mediator, ILoggerFactory loggerFactory)
		{
			_mediator = mediator;
			logger = loggerFactory.CreateLogger<NewsProccesorTask>();
		}

		public async Task Execute()
		{
			var command = new GetEverithingNewsQuery() { Query = "Nvidia", From = "2024-08-03", SortBy = "Popularity" };

			var response = await _mediator.Send(command);

			logger.LogInformation(JsonSerializer.Serialize(response,
				new JsonSerializerOptions { WriteIndented = true }));
		}
	}
}
