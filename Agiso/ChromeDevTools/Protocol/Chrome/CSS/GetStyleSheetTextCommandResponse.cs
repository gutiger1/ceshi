using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000607 RID: 1543
	[CommandResponse("CSS.getStyleSheetText")]
	[SupportedBy("Chrome")]
	public class GetStyleSheetTextCommandResponse
	{
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x0000D08E File Offset: 0x0000B28E
		// (set) Token: 0x06001D35 RID: 7477 RVA: 0x0000D096 File Offset: 0x0000B296
		public string Text { get; set; }

		// Token: 0x04001026 RID: 4134
		[CompilerGenerated]
		private string a;
	}
}
