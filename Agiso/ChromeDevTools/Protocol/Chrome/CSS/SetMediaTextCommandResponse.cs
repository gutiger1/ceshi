using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000612 RID: 1554
	[SupportedBy("Chrome")]
	[CommandResponse("CSS.setMediaText")]
	public class SetMediaTextCommandResponse
	{
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0000D215 File Offset: 0x0000B415
		// (set) Token: 0x06001D6E RID: 7534 RVA: 0x0000D21D File Offset: 0x0000B41D
		public CSSMedia Media { get; set; }

		// Token: 0x0400103D RID: 4157
		[CompilerGenerated]
		private CSSMedia a;
	}
}
