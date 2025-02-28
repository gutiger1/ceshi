using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x0200063E RID: 1598
	[Command("Canvas.replayTraceLog")]
	[SupportedBy("Chrome")]
	public class ReplayTraceLogCommand
	{
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x0000D776 File Offset: 0x0000B976
		// (set) Token: 0x06001E3B RID: 7739 RVA: 0x0000D77E File Offset: 0x0000B97E
		public string TraceLogId { get; set; }

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x0000D787 File Offset: 0x0000B987
		// (set) Token: 0x06001E3D RID: 7741 RVA: 0x0000D78F File Offset: 0x0000B98F
		public long StepNo { get; set; }

		// Token: 0x04001093 RID: 4243
		[CompilerGenerated]
		private string a;

		// Token: 0x04001094 RID: 4244
		[CompilerGenerated]
		private long b;
	}
}
