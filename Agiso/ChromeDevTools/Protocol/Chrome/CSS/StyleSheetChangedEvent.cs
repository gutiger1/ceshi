using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x0200061C RID: 1564
	[SupportedBy("Chrome")]
	[Event("CSS.styleSheetChanged")]
	public class StyleSheetChangedEvent
	{
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x0000D347 File Offset: 0x0000B547
		// (set) Token: 0x06001D9C RID: 7580 RVA: 0x0000D34F File Offset: 0x0000B54F
		public string StyleSheetId { get; set; }

		// Token: 0x0400104F RID: 4175
		[CompilerGenerated]
		private string a;
	}
}
