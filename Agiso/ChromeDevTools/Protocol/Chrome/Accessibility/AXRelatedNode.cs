using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Accessibility
{
	// Token: 0x02000675 RID: 1653
	[SupportedBy("Chrome")]
	public class AXRelatedNode
	{
		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x0000DEA2 File Offset: 0x0000C0A2
		// (set) Token: 0x06001F47 RID: 8007 RVA: 0x0000DEAA File Offset: 0x0000C0AA
		public string Idref { get; set; }

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x0000DEB3 File Offset: 0x0000C0B3
		// (set) Token: 0x06001F49 RID: 8009 RVA: 0x0000DEBB File Offset: 0x0000C0BB
		public long BackendNodeId { get; set; }

		// Token: 0x0400110E RID: 4366
		[CompilerGenerated]
		private string a;

		// Token: 0x0400110F RID: 4367
		[CompilerGenerated]
		private long b;
	}
}
