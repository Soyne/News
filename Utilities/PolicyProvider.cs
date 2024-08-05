using Polly;
using Polly.Retry;
using RestSharp;
using System.Net;

namespace NewsApiService.Utilities
{
	public class PolicyProvider
	{
		public static AsyncRetryPolicy<RestResponse<TResult>> RetryPolicyAsync<TResult>(RestRequest request)
		{
			var retry = Policy
				.Handle<Exception>()
				.OrResult<RestResponse<TResult>>(r => r.StatusCode != HttpStatusCode.OK)
				.WaitAndRetryAsync(
					5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
					(result, timespan, retryNo, context) => StaticLogger.LogError($"Retry request {result?.Result.ResponseUri?.ToString() ?? request.Resource } attempt {retryNo}: {result?.Result?.ErrorException?.Message}")); 
				
			return retry;
		}
	}
}
