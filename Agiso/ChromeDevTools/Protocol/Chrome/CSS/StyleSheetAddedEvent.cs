using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x0200061B RID: 1563
	[Event("CSS.styleSheetAdded")]
	[SupportedBy("Chrome")]
	public class StyleSheetAddedEvent
	{
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x0000D336 File Offset: 0x0000B536
		// (set) Token: 0x06001D99 RID: 7577 RVA: 0x0000D33E File Offset: 0x0000B53E
		public GClass5 Header { get; set; }

		// Token: 0x0400104E RID: 4174
		[CompilerGenerated]
		private GClass5 a;
	}
}
