using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x02000644 RID: 1604
	[SupportedBy("Chrome")]
	[Command("Canvas.stopCapturing")]
	public class StopCapturingCommand
	{
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x0000D875 File Offset: 0x0000BA75
		// (set) Token: 0x06001E5F RID: 7775 RVA: 0x0000D87D File Offset: 0x0000BA7D
		public string TraceLogId { get; set; }

		// Token: 0x040010A2 RID: 4258
		[CompilerGenerated]
		private string a;
	}
}
