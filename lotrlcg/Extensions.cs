namespace lotrlcg
{
	public static class Extensions
	{
		// string extension
		public static string Truncate(this string s, int length, string truncPost = "")
		{
			return (string.IsNullOrEmpty(s) || s.Length <= length) ? s : s.Substring(0, length - truncPost.Length) + truncPost;
		}
	}
}
