using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x0200062D RID: 1581
	[Command("Canvas.captureFrame")]
	[SupportedBy("Chrome")]
	public class CaptureFrameCommand
	{
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x0000D644 File Offset: 0x0000B844
		// (set) Token: 0x06001E06 RID: 7686 RVA: 0x0000D64C File Offset: 0x0000B84C
		public string FrameId { get; set; }

		// Token: 0x04001081 RID: 4225
		[CompilerGenerated]
		private string a;
	}
}
