using System;
using HtmlAgilityPack;

namespace Agiso
{
	// Token: 0x020000D6 RID: 214
	public class HtmlAgilityPackHelper
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x000470A0 File Offset: 0x000452A0
		public static HtmlDocument GetDoc(string html)
		{
			HtmlDocument htmlDocument;
			if (string.IsNullOrEmpty(html))
			{
				htmlDocument = null;
			}
			else
			{
				HtmlDocument htmlDocument2 = new HtmlDocument();
				htmlDocument2.LoadHtml(html);
				htmlDocument = htmlDocument2;
			}
			return htmlDocument;
		}
	}
}
