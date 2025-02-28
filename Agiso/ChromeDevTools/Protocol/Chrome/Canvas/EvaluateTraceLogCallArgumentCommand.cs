using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x02000636 RID: 1590
	[SupportedBy("Chrome")]
	[Command("Canvas.evaluateTraceLogCallArgument")]
	public class EvaluateTraceLogCallArgumentCommand
	{
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x0000D688 File Offset: 0x0000B888
		// (set) Token: 0x06001E17 RID: 7703 RVA: 0x0000D690 File Offset: 0x0000B890
		public string TraceLogId { get; set; }

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x0000D699 File Offset: 0x0000B899
		// (set) Token: 0x06001E19 RID: 7705 RVA: 0x0000D6A1 File Offset: 0x0000B8A1
		public long CallIndex { get; set; }

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x0000D6AA File Offset: 0x0000B8AA
		// (set) Token: 0x06001E1B RID: 7707 RVA: 0x0000D6B2 File Offset: 0x0000B8B2
		public long ArgumentIndex { get; set; }

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x0000D6BB File Offset: 0x0000B8BB
		// (set) Token: 0x06001E1D RID: 7709 RVA: 0x0000D6C3 File Offset: 0x0000B8C3
		public string ObjectGroup { get; set; }

		// Token: 0x04001085 RID: 4229
		[CompilerGenerated]
		private string a;

		// Token: 0x04001086 RID: 4230
		[CompilerGenerated]
		private long b;

		// Token: 0x04001087 RID: 4231
		[CompilerGenerated]
		private long c;

		// Token: 0x04001088 RID: 4232
		[CompilerGenerated]
		private string d;
	}
}
