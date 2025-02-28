using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x020005FC RID: 1532
	[Command("CSS.getComputedStyleForNode")]
	[SupportedBy("Chrome")]
	public class GetComputedStyleForNodeCommand
	{
		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x0000CF7E File Offset: 0x0000B17E
		// (set) Token: 0x06001D0A RID: 7434 RVA: 0x0000CF86 File Offset: 0x0000B186
		public long NodeId { get; set; }

		// Token: 0x04001016 RID: 4118
		[CompilerGenerated]
		private long a;
	}
}
