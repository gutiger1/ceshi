using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000601 RID: 1537
	[CommandResponse("CSS.getMatchedStylesForNode")]
	[SupportedBy("Chrome")]
	public class GetMatchedStylesForNodeCommandResponse
	{
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x0000D006 File Offset: 0x0000B206
		// (set) Token: 0x06001D1F RID: 7455 RVA: 0x0000D00E File Offset: 0x0000B20E
		public RuleMatch[] MatchedCSSRules { get; set; }

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x0000D017 File Offset: 0x0000B217
		// (set) Token: 0x06001D21 RID: 7457 RVA: 0x0000D01F File Offset: 0x0000B21F
		public PseudoIdMatches[] PseudoElements { get; set; }

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x0000D028 File Offset: 0x0000B228
		// (set) Token: 0x06001D23 RID: 7459 RVA: 0x0000D030 File Offset: 0x0000B230
		public InheritedStyleEntry[] Inherited { get; set; }

		// Token: 0x0400101E RID: 4126
		[CompilerGenerated]
		private RuleMatch[] a;

		// Token: 0x0400101F RID: 4127
		[CompilerGenerated]
		private PseudoIdMatches[] b;

		// Token: 0x04001020 RID: 4128
		[CompilerGenerated]
		private InheritedStyleEntry[] c;
	}
}
