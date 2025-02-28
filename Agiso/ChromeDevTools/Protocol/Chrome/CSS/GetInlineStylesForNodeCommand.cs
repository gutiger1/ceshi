using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x020005FE RID: 1534
	[SupportedBy("Chrome")]
	[Command("CSS.getInlineStylesForNode")]
	public class GetInlineStylesForNodeCommand
	{
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0000CFA0 File Offset: 0x0000B1A0
		// (set) Token: 0x06001D10 RID: 7440 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
		public long NodeId { get; set; }

		// Token: 0x04001018 RID: 4120
		[CompilerGenerated]
		private long a;
	}
}
