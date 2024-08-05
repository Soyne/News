using MediatR;
using NewsApiService.Services.News.Dtos;

namespace NewsApiService.Services.News.Queries
{
	public class GetEverithingNewsQuery : IRequest<GetEverithingNewsResponse>
	{
		public string Query { get; set; }
		public string From { get; set; }
		public string SortBy { get; set; }
	}

}
