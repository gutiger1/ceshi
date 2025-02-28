using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000613 RID: 1555
	[Command("CSS.setPropertyText")]
	[SupportedBy("Chrome")]
	public class SetPropertyTextCommand
	{
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0000D226 File Offset: 0x0000B426
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x0000D22E File Offset: 0x0000B42E
		public string StyleSheetId { get; set; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x0000D237 File Offset: 0x0000B437
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x0000D23F File Offset: 0x0000B43F
		public SourceRange Range { get; set; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x0000D248 File Offset: 0x0000B448
		// (set) Token: 0x06001D75 RID: 7541 RVA: 0x0000D250 File Offset: 0x0000B450
		public string Text { get; set; }

		// Token: 0x0400103E RID: 4158
		[CompilerGenerated]
		private string a;

		// Token: 0x0400103F RID: 4159
		[CompilerGenerated]
		private SourceRange b;

		// Token: 0x04001040 RID: 4160
		[CompilerGenerated]
		private string c;
	}
}
