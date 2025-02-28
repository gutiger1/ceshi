using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x0200060E RID: 1550
	[SupportedBy("Chrome")]
	public class RuleMatch
	{
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x0000D17C File Offset: 0x0000B37C
		// (set) Token: 0x06001D58 RID: 7512 RVA: 0x0000D184 File Offset: 0x0000B384
		public CSSRule Rule { get; set; }

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x0000D18D File Offset: 0x0000B38D
		// (set) Token: 0x06001D5A RID: 7514 RVA: 0x0000D195 File Offset: 0x0000B395
		public long[] MatchingSelectors { get; set; }

		// Token: 0x04001034 RID: 4148
		[CompilerGenerated]
		private CSSRule a;

		// Token: 0x04001035 RID: 4149
		[CompilerGenerated]
		private long[] b;
	}
}
