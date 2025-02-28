using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools
{
	// Token: 0x0200011A RID: 282
	[AttributeUsage(AttributeTargets.Class)]
	public class SupportedByAttribute : Attribute
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x000527D8 File Offset: 0x000509D8
		public SupportedByAttribute(string browser)
		{
			if (browser == null)
			{
				throw new ArgumentNullException("string");
			}
			this.Browser = browser;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0000491E File Offset: 0x00002B1E
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x00004926 File Offset: 0x00002B26
		public string Browser { get; set; }

		// Token: 0x04000536 RID: 1334
		[CompilerGenerated]
		private string a;
	}
}
