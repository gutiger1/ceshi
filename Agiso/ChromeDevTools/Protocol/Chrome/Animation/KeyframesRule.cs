using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Animation
{
	// Token: 0x02000667 RID: 1639
	[SupportedBy("Chrome")]
	public class KeyframesRule
	{
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x0000DD0A File Offset: 0x0000BF0A
		// (set) Token: 0x06001F0C RID: 7948 RVA: 0x0000DD12 File Offset: 0x0000BF12
		public string Name { get; set; }

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x0000DD1B File Offset: 0x0000BF1B
		// (set) Token: 0x06001F0E RID: 7950 RVA: 0x0000DD23 File Offset: 0x0000BF23
		public KeyframeStyle[] Keyframes { get; set; }

		// Token: 0x040010E7 RID: 4327
		[CompilerGenerated]
		private string a;

		// Token: 0x040010E8 RID: 4328
		[CompilerGenerated]
		private KeyframeStyle[] b;
	}
}
