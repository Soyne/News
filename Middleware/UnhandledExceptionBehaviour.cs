using MediatR;
using Microsoft.Extensions.Logging;

namespace NewsApiService.Middleware
{
	public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	 where TRequest : notnull, IRequest<TResponse>
	{
		private readonly ILogger<TRequest> _logger;

		public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
		{
			_logger = logger;
		}

		public async Task<TResponse> Handle(TRequest request,  RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			TResponse response = default;
			try
			{
				response = await next();
			}
			catch (Exception ex)
			{
				ex.Data["RequestName"] = typeof(TRequest).Name;
				ex.Data["TResponse"] = typeof(TResponse).Name;
				ex.Data["Request"] = request;
				ex.Data["ResponseException"] = ex.InnerException != null ? ex.InnerException.ToString() : ex.Message ?? string.Empty;
				_logger.LogCritical(ex, ex.Message);
				//throw ex;
			}

			return response;
		}
	}
}
