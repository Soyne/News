using RestSharp;

namespace NewsApiService.Utilities
{
	public class RestPollyClient
	{
		private RestClient _internalClient;

		public RestPollyClient(RestClientOptions clientOptions) => this._internalClient = new RestClient(clientOptions);

		public async Task<RestResponse<TResult>> ExecuteAsync<TResult>(RestRequest request, CancellationToken cancellationToken = default) =>
		 await PolicyProvider.RetryPolicyAsync<TResult>(request)
			 .ExecuteAsync(async () => await this._internalClient.ExecuteAsync<TResult>(request, cancellationToken)).ConfigureAwait(false);
	}
}
