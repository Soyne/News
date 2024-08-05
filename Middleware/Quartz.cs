using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsApiService.Jobs;
using Quartz;

namespace NewsApiService.Middleware
{
	public static class Quartz
	{
		public static IServiceCollection AddQartz(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddQuartz(q =>
			{
				var jobKey = new JobKey("NewsApiJob");
				var jobInterval = configuration.GetValue<int>("General:NewsApiJobSchedule");

				q.AddJob<ProcessNewsJob>(opts => opts.WithIdentity(jobKey));


				q.AddTrigger(opts => opts
					.ForJob(jobKey)
					.WithIdentity("NewsApiTrigger")
					.StartNow()
					.WithSimpleSchedule(x => x.WithIntervalInSeconds(jobInterval).RepeatForever()));
			});

			services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

			return services;
		}
	}
}
