using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x0200062E RID: 1582
	[SupportedBy("Chrome")]
	[CommandResponse("Canvas.captureFrame")]
	public class CaptureFrameCommandResponse
	{
		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x0000D655 File Offset: 0x0000B855
		// (set) Token: 0x06001E09 RID: 7689 RVA: 0x0000D65D File Offset: 0x0000B85D
		public string TraceLogId { get; set; }

		// Token: 0x04001082 RID: 4226
		[CompilerGenerated]
		private string a;
	}
}
