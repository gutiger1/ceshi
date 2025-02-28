using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x0200061A RID: 1562
	[SupportedBy("Chrome")]
	public class SourceRange
	{
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x0000D2F2 File Offset: 0x0000B4F2
		// (set) Token: 0x06001D90 RID: 7568 RVA: 0x0000D2FA File Offset: 0x0000B4FA
		public long StartLine { get; set; }

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x0000D303 File Offset: 0x0000B503
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x0000D30B File Offset: 0x0000B50B
		public long StartColumn { get; set; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x0000D314 File Offset: 0x0000B514
		// (set) Token: 0x06001D94 RID: 7572 RVA: 0x0000D31C File Offset: 0x0000B51C
		public long EndLine { get; set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0000D325 File Offset: 0x0000B525
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x0000D32D File Offset: 0x0000B52D
		public long EndColumn { get; set; }

		// Token: 0x0400104A RID: 4170
		[CompilerGenerated]
		private long a;

		// Token: 0x0400104B RID: 4171
		[CompilerGenerated]
		private long b;

		// Token: 0x0400104C RID: 4172
		[CompilerGenerated]
		private long c;

		// Token: 0x0400104D RID: 4173
		[CompilerGenerated]
		private long d;
	}
}
