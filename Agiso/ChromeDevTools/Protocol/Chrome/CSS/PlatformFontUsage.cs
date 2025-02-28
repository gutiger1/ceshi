using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x0200060C RID: 1548
	[SupportedBy("Chrome")]
	public class PlatformFontUsage
	{
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x0000D138 File Offset: 0x0000B338
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x0000D140 File Offset: 0x0000B340
		public string FamilyName { get; set; }

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0000D149 File Offset: 0x0000B349
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x0000D151 File Offset: 0x0000B351
		public double GlyphCount { get; set; }

		// Token: 0x04001030 RID: 4144
		[CompilerGenerated]
		private string a;

		// Token: 0x04001031 RID: 4145
		[CompilerGenerated]
		private double b;
	}
}
