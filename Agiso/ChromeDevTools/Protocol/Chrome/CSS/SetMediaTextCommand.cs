using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000611 RID: 1553
	[Command("CSS.setMediaText")]
	[SupportedBy("Chrome")]
	public class SetMediaTextCommand
	{
		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x0000D1E2 File Offset: 0x0000B3E2
		// (set) Token: 0x06001D67 RID: 7527 RVA: 0x0000D1EA File Offset: 0x0000B3EA
		public string StyleSheetId { get; set; }

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0000D1F3 File Offset: 0x0000B3F3
		// (set) Token: 0x06001D69 RID: 7529 RVA: 0x0000D1FB File Offset: 0x0000B3FB
		public SourceRange Range { get; set; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x0000D204 File Offset: 0x0000B404
		// (set) Token: 0x06001D6B RID: 7531 RVA: 0x0000D20C File Offset: 0x0000B40C
		public string Text { get; set; }

		// Token: 0x0400103A RID: 4154
		[CompilerGenerated]
		private string a;

		// Token: 0x0400103B RID: 4155
		[CompilerGenerated]
		private SourceRange b;

		// Token: 0x0400103C RID: 4156
		[CompilerGenerated]
		private string c;
	}
}
