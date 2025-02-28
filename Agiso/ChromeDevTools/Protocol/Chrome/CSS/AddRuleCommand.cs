using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x020005EC RID: 1516
	[Command("CSS.addRule")]
	[SupportedBy("Chrome")]
	public class AddRuleCommand
	{
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x0000CC70 File Offset: 0x0000AE70
		// (set) Token: 0x06001C9E RID: 7326 RVA: 0x0000CC78 File Offset: 0x0000AE78
		public string StyleSheetId { get; set; }

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x0000CC81 File Offset: 0x0000AE81
		// (set) Token: 0x06001CA0 RID: 7328 RVA: 0x0000CC89 File Offset: 0x0000AE89
		public string RuleText { get; set; }

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x0000CC92 File Offset: 0x0000AE92
		// (set) Token: 0x06001CA2 RID: 7330 RVA: 0x0000CC9A File Offset: 0x0000AE9A
		public SourceRange Location { get; set; }

		// Token: 0x04000FE8 RID: 4072
		[CompilerGenerated]
		private string a;

		// Token: 0x04000FE9 RID: 4073
		[CompilerGenerated]
		private string b;

		// Token: 0x04000FEA RID: 4074
		[CompilerGenerated]
		private SourceRange c;
	}
}
