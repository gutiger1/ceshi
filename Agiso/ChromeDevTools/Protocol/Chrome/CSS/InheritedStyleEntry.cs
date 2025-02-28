using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000608 RID: 1544
	[SupportedBy("Chrome")]
	public class InheritedStyleEntry
	{
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0000D09F File Offset: 0x0000B29F
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x0000D0A7 File Offset: 0x0000B2A7
		public CSSStyle InlineStyle { get; set; }

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0000D0B0 File Offset: 0x0000B2B0
		// (set) Token: 0x06001D3A RID: 7482 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		public RuleMatch[] MatchedCSSRules { get; set; }

		// Token: 0x04001027 RID: 4135
		[CompilerGenerated]
		private CSSStyle a;

		// Token: 0x04001028 RID: 4136
		[CompilerGenerated]
		private RuleMatch[] b;
	}
}
