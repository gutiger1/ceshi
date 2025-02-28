using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000605 RID: 1541
	[SupportedBy("Chrome")]
	[CommandResponse("CSS.getPlatformFontsForNode")]
	public class GetPlatformFontsForNodeCommandResponse
	{
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001D2C RID: 7468 RVA: 0x0000D05B File Offset: 0x0000B25B
		// (set) Token: 0x06001D2D RID: 7469 RVA: 0x0000D063 File Offset: 0x0000B263
		public string CssFamilyName { get; set; }

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x0000D06C File Offset: 0x0000B26C
		// (set) Token: 0x06001D2F RID: 7471 RVA: 0x0000D074 File Offset: 0x0000B274
		public PlatformFontUsage[] Fonts { get; set; }

		// Token: 0x04001023 RID: 4131
		[CompilerGenerated]
		private string a;

		// Token: 0x04001024 RID: 4132
		[CompilerGenerated]
		private PlatformFontUsage[] b;
	}
}
