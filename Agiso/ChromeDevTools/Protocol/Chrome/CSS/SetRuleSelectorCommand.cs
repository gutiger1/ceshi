using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000615 RID: 1557
	[SupportedBy("Chrome")]
	[Command("CSS.setRuleSelector")]
	public class SetRuleSelectorCommand
	{
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x0000D26A File Offset: 0x0000B46A
		// (set) Token: 0x06001D7B RID: 7547 RVA: 0x0000D272 File Offset: 0x0000B472
		public string StyleSheetId { get; set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0000D27B File Offset: 0x0000B47B
		// (set) Token: 0x06001D7D RID: 7549 RVA: 0x0000D283 File Offset: 0x0000B483
		public SourceRange Range { get; set; }

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0000D28C File Offset: 0x0000B48C
		// (set) Token: 0x06001D7F RID: 7551 RVA: 0x0000D294 File Offset: 0x0000B494
		public string Selector { get; set; }

		// Token: 0x04001042 RID: 4162
		[CompilerGenerated]
		private string a;

		// Token: 0x04001043 RID: 4163
		[CompilerGenerated]
		private SourceRange b;

		// Token: 0x04001044 RID: 4164
		[CompilerGenerated]
		private string c;
	}
}
