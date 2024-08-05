namespace NewsApiService.Services.News.Dtos
{
    public class GetEverithingNewsResponse
	{
		public string Status { get; set; }
		public int TotalResults { get; set; }
		public IEnumerable<ArticleDto> Articles { get; set; }
	}
}
