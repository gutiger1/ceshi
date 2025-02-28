using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x0200060D RID: 1549
	[SupportedBy("Chrome")]
	public class PseudoIdMatches
	{
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x0000D15A File Offset: 0x0000B35A
		// (set) Token: 0x06001D53 RID: 7507 RVA: 0x0000D162 File Offset: 0x0000B362
		public long PseudoId { get; set; }

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x0000D16B File Offset: 0x0000B36B
		// (set) Token: 0x06001D55 RID: 7509 RVA: 0x0000D173 File Offset: 0x0000B373
		public RuleMatch[] Matches { get; set; }

		// Token: 0x04001032 RID: 4146
		[CompilerGenerated]
		private long a;

		// Token: 0x04001033 RID: 4147
		[CompilerGenerated]
		private RuleMatch[] b;
	}
}
