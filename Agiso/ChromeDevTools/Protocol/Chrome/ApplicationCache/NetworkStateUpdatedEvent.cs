using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.ApplicationCache
{
	// Token: 0x0200065C RID: 1628
	[SupportedBy("Chrome")]
	[Event("ApplicationCache.networkStateUpdated")]
	public class NetworkStateUpdatedEvent
	{
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x0000DB2E File Offset: 0x0000BD2E
		// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x0000DB36 File Offset: 0x0000BD36
		public bool IsNowOnline { get; set; }

		// Token: 0x040010CB RID: 4299
		[CompilerGenerated]
		private bool a;
	}
}
