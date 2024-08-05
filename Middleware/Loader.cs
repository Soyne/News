using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace NewsApiService.Middleware
{
	public static class Loader
	{
		public static IServiceCollection AddCQRSCorePipelines(this IServiceCollection services, params Type[] excludedBehaviours)
		{
			var pipelines = new[] {
			 typeof(UnhandledExceptionThrowBehaviour<,>),
			 typeof(UnhandledExceptionBehaviour<,>),
		 }.Where(z => !excludedBehaviours?.Contains(z) ?? true);

			foreach (var pipeline in pipelines)
			{
				if (!services.Any(x => x.ImplementationType == pipeline))
				{
					services.AddTransient(typeof(IPipelineBehavior<,>), pipeline);
				}
			}

			return services;
		}

	}
}
