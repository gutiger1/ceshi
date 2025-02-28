using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x020005F3 RID: 1523
	[SupportedBy("Chrome")]
	public class CSSRule
	{
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0000CDE6 File Offset: 0x0000AFE6
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x0000CDEE File Offset: 0x0000AFEE
		public string StyleSheetId { get; set; }

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x0000CDF7 File Offset: 0x0000AFF7
		// (set) Token: 0x06001CD3 RID: 7379 RVA: 0x0000CDFF File Offset: 0x0000AFFF
		public SelectorList SelectorList { get; set; }

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x0000CE08 File Offset: 0x0000B008
		// (set) Token: 0x06001CD5 RID: 7381 RVA: 0x0000CE10 File Offset: 0x0000B010
		public StyleSheetOrigin Origin { get; set; }

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x0000CE19 File Offset: 0x0000B019
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x0000CE21 File Offset: 0x0000B021
		public CSSStyle Style { get; set; }

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x0000CE2A File Offset: 0x0000B02A
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x0000CE32 File Offset: 0x0000B032
		public CSSMedia[] Media { get; set; }

		// Token: 0x04000FFE RID: 4094
		[CompilerGenerated]
		private string a;

		// Token: 0x04000FFF RID: 4095
		[CompilerGenerated]
		private SelectorList b;

		// Token: 0x04001000 RID: 4096
		[CompilerGenerated]
		private StyleSheetOrigin c;

		// Token: 0x04001001 RID: 4097
		[CompilerGenerated]
		private CSSStyle d;

		// Token: 0x04001002 RID: 4098
		[CompilerGenerated]
		private CSSMedia[] e;
	}
}
