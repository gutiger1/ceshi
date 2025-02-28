using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x02000643 RID: 1603
	[SupportedBy("Chrome")]
	[CommandResponse("Canvas.startCapturing")]
	public class StartCapturingCommandResponse
	{
		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x0000D864 File Offset: 0x0000BA64
		// (set) Token: 0x06001E5C RID: 7772 RVA: 0x0000D86C File Offset: 0x0000BA6C
		public string TraceLogId { get; set; }

		// Token: 0x040010A1 RID: 4257
		[CompilerGenerated]
		private string a;
	}
}
