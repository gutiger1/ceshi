using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x020005F4 RID: 1524
	[SupportedBy("Chrome")]
	public class CSSStyle
	{
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x0000CE3B File Offset: 0x0000B03B
		// (set) Token: 0x06001CDC RID: 7388 RVA: 0x0000CE43 File Offset: 0x0000B043
		public string StyleSheetId { get; set; }

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x0000CE4C File Offset: 0x0000B04C
		// (set) Token: 0x06001CDE RID: 7390 RVA: 0x0000CE54 File Offset: 0x0000B054
		public CSSProperty[] CssProperties { get; set; }

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x0000CE5D File Offset: 0x0000B05D
		// (set) Token: 0x06001CE0 RID: 7392 RVA: 0x0000CE65 File Offset: 0x0000B065
		public ShorthandEntry[] ShorthandEntries { get; set; }

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x0000CE6E File Offset: 0x0000B06E
		// (set) Token: 0x06001CE2 RID: 7394 RVA: 0x0000CE76 File Offset: 0x0000B076
		public string CssText { get; set; }

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x0000CE7F File Offset: 0x0000B07F
		// (set) Token: 0x06001CE4 RID: 7396 RVA: 0x0000CE87 File Offset: 0x0000B087
		public SourceRange Range { get; set; }

		// Token: 0x04001003 RID: 4099
		[CompilerGenerated]
		private string a;

		// Token: 0x04001004 RID: 4100
		[CompilerGenerated]
		private CSSProperty[] b;

		// Token: 0x04001005 RID: 4101
		[CompilerGenerated]
		private ShorthandEntry[] c;

		// Token: 0x04001006 RID: 4102
		[CompilerGenerated]
		private string d;

		// Token: 0x04001007 RID: 4103
		[CompilerGenerated]
		private SourceRange e;
	}
}
