using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x0200061E RID: 1566
	[Event("CSS.styleSheetRemoved")]
	[SupportedBy("Chrome")]
	public class StyleSheetRemovedEvent
	{
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0000D358 File Offset: 0x0000B558
		// (set) Token: 0x06001D9F RID: 7583 RVA: 0x0000D360 File Offset: 0x0000B560
		public string StyleSheetId { get; set; }

		// Token: 0x04001055 RID: 4181
		[CompilerGenerated]
		private string a;
	}
}
