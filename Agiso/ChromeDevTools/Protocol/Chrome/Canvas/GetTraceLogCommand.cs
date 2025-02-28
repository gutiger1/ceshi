using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x0200063A RID: 1594
	[SupportedBy("Chrome")]
	[Command("Canvas.getTraceLog")]
	public class GetTraceLogCommand
	{
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x0000D721 File Offset: 0x0000B921
		// (set) Token: 0x06001E2D RID: 7725 RVA: 0x0000D729 File Offset: 0x0000B929
		public string TraceLogId { get; set; }

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x0000D732 File Offset: 0x0000B932
		// (set) Token: 0x06001E2F RID: 7727 RVA: 0x0000D73A File Offset: 0x0000B93A
		public long StartOffset { get; set; }

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x0000D743 File Offset: 0x0000B943
		// (set) Token: 0x06001E31 RID: 7729 RVA: 0x0000D74B File Offset: 0x0000B94B
		public long MaxLength { get; set; }

		// Token: 0x0400108E RID: 4238
		[CompilerGenerated]
		private string a;

		// Token: 0x0400108F RID: 4239
		[CompilerGenerated]
		private long b;

		// Token: 0x04001090 RID: 4240
		[CompilerGenerated]
		private long c;
	}
}
