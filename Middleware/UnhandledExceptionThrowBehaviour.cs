using MediatR;
using Microsoft.Extensions.Logging;

namespace NewsApiService.Middleware
{
	public class UnhandledExceptionThrowBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	  where TRequest : notnull, IRequest<TResponse>
	{
		private readonly ILogger<TRequest> _logger;

		public UnhandledExceptionThrowBehaviour(ILogger<TRequest> logger)
		{
			_logger = logger;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			try
			{
				return await next();
			}
			catch (Exception ex)
			{
				ex.Data["RequestName"] = typeof(TRequest).Name;
				ex.Data["TResponse"] = typeof(TResponse).Name;
				ex.Data["Request"] = request;
				ex.Data["ResponseException"] = ex.InnerException != null ? ex.InnerException.ToString() : ex.Message ?? string.Empty;
				_logger.LogError(ex, ex.Message);
				throw ex;
			}
		}
	}
}
