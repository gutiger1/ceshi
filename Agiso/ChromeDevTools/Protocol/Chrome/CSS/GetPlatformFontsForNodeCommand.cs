using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000604 RID: 1540
	[SupportedBy("Chrome")]
	[Command("CSS.getPlatformFontsForNode")]
	public class GetPlatformFontsForNodeCommand
	{
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x0000D04A File Offset: 0x0000B24A
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x0000D052 File Offset: 0x0000B252
		public long NodeId { get; set; }

		// Token: 0x04001022 RID: 4130
		[CompilerGenerated]
		private long a;
	}
}
