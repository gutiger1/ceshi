using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000617 RID: 1559
	[Command("CSS.setStyleSheetText")]
	[SupportedBy("Chrome")]
	public class SetStyleSheetTextCommand
	{
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x0000D2AE File Offset: 0x0000B4AE
		// (set) Token: 0x06001D85 RID: 7557 RVA: 0x0000D2B6 File Offset: 0x0000B4B6
		public string StyleSheetId { get; set; }

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x0000D2BF File Offset: 0x0000B4BF
		// (set) Token: 0x06001D87 RID: 7559 RVA: 0x0000D2C7 File Offset: 0x0000B4C7
		public string Text { get; set; }

		// Token: 0x04001046 RID: 4166
		[CompilerGenerated]
		private string a;

		// Token: 0x04001047 RID: 4167
		[CompilerGenerated]
		private string b;
	}
}
