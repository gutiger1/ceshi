using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x020005FD RID: 1533
	[CommandResponse("CSS.getComputedStyleForNode")]
	[SupportedBy("Chrome")]
	public class GetComputedStyleForNodeCommandResponse
	{
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x0000CF8F File Offset: 0x0000B18F
		// (set) Token: 0x06001D0D RID: 7437 RVA: 0x0000CF97 File Offset: 0x0000B197
		public CSSComputedStyleProperty[] ComputedStyle { get; set; }

		// Token: 0x04001017 RID: 4119
		[CompilerGenerated]
		private CSSComputedStyleProperty[] a;
	}
}
