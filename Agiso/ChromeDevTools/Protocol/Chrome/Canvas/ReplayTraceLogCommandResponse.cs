using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x0200063F RID: 1599
	[SupportedBy("Chrome")]
	[CommandResponse("Canvas.replayTraceLog")]
	public class ReplayTraceLogCommandResponse
	{
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06001E3F RID: 7743 RVA: 0x0000D798 File Offset: 0x0000B998
		// (set) Token: 0x06001E40 RID: 7744 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		public ResourceState ResourceState { get; set; }

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x0000D7A9 File Offset: 0x0000B9A9
		// (set) Token: 0x06001E42 RID: 7746 RVA: 0x0000D7B1 File Offset: 0x0000B9B1
		public double ReplayTime { get; set; }

		// Token: 0x04001095 RID: 4245
		[CompilerGenerated]
		private ResourceState a;

		// Token: 0x04001096 RID: 4246
		[CompilerGenerated]
		private double b;
	}
}
