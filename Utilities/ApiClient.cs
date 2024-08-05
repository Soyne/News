using Microsoft.Extensions.Options;
using NewsApiService.Interfaces;
using NewsApiService.Services.News.Queries;
using NewsApiService.Services.News.Dtos;
using RestSharp;

namespace NewsApiService.Utilities
{
	public class ApiClient : IApiClient
	{
		private readonly RestPollyClient _client;
		private readonly GeneralOptions generalOptions;

		public ApiClient(IOptions<GeneralOptions> options)
		{
			generalOptions = options.Value;
			_client = new RestPollyClient(new RestClientOptions(options.Value.ApiUrl));
		}

		public async Task<GetEverithingNewsResponse?> GetEverythingNewsAsync(GetEverithingNewsQuery requestDto, CancellationToken cancellationToken)
		{
			var url = "everything";
			var parameters = QueryHelper.AddEverythingNewsParameters(requestDto.Query, requestDto.From, requestDto.SortBy);
			var apiKey = $"&apiKey={generalOptions.ApiKey}";
			
			var request = new RestRequest(string.Concat(url, parameters, apiKey));

			var response = await _client.ExecuteAsync<GetEverithingNewsResponse>(request, cancellationToken);

			if (response.IsSuccessStatusCode)
			{
				return response.Data;
			}

			throw new InvalidOperationException(response.ErrorMessage);
		}
		
	}
}
