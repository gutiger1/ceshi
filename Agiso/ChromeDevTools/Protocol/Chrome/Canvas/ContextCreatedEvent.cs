using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x0200062F RID: 1583
	[Event("Canvas.contextCreated")]
	[SupportedBy("Chrome")]
	public class ContextCreatedEvent
	{
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x0000D666 File Offset: 0x0000B866
		// (set) Token: 0x06001E0C RID: 7692 RVA: 0x0000D66E File Offset: 0x0000B86E
		public string FrameId { get; set; }

		// Token: 0x04001083 RID: 4227
		[CompilerGenerated]
		private string a;
	}
}
