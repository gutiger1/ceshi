using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x02000647 RID: 1607
	[SupportedBy("Chrome")]
	[Event("Canvas.traceLogsRemoved")]
	public class TraceLogsRemovedEvent
	{
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		// (set) Token: 0x06001E70 RID: 7792 RVA: 0x0000D8F4 File Offset: 0x0000BAF4
		public string FrameId { get; set; }

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x0000D8FD File Offset: 0x0000BAFD
		// (set) Token: 0x06001E72 RID: 7794 RVA: 0x0000D905 File Offset: 0x0000BB05
		public string TraceLogId { get; set; }

		// Token: 0x040010A9 RID: 4265
		[CompilerGenerated]
		private string a;

		// Token: 0x040010AA RID: 4266
		[CompilerGenerated]
		private string b;
	}
}
