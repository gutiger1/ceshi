using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000606 RID: 1542
	[SupportedBy("Chrome")]
	[Command("CSS.getStyleSheetText")]
	public class GetStyleSheetTextCommand
	{
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0000D07D File Offset: 0x0000B27D
		// (set) Token: 0x06001D32 RID: 7474 RVA: 0x0000D085 File Offset: 0x0000B285
		public string StyleSheetId { get; set; }

		// Token: 0x04001025 RID: 4133
		[CompilerGenerated]
		private string a;
	}
}
