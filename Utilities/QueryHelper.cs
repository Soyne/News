using System.Text;

namespace NewsApiService.Utilities
{
	public static class QueryHelper
	{
		public static string AddEverythingNewsParameters(string query, string from, string sortBy)
		{
			var sb = new StringBuilder("?");

			if (!string.IsNullOrEmpty(query))
			{
				sb.Append("&q=" + query);
			}

			if (!string.IsNullOrEmpty(from))
			{
				sb.Append("&from=" + from);

			}
			if (!string.IsNullOrEmpty(sortBy))
			{
				sb.Append("&sortBy=" + sortBy);
			}

			return sb.ToString();
		}
	}
}
