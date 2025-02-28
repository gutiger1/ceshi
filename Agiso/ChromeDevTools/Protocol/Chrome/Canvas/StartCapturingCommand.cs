using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x02000642 RID: 1602
	[Command("Canvas.startCapturing")]
	[SupportedBy("Chrome")]
	public class StartCapturingCommand
	{
		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x0000D853 File Offset: 0x0000BA53
		// (set) Token: 0x06001E59 RID: 7769 RVA: 0x0000D85B File Offset: 0x0000BA5B
		public string FrameId { get; set; }

		// Token: 0x040010A0 RID: 4256
		[CompilerGenerated]
		private string a;
	}
}
