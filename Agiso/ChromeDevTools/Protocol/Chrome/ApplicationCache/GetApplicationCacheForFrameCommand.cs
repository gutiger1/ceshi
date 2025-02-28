using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.ApplicationCache
{
	// Token: 0x02000656 RID: 1622
	[Command("ApplicationCache.getApplicationCacheForFrame")]
	[SupportedBy("Chrome")]
	public class GetApplicationCacheForFrameCommand
	{
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x0000DAD9 File Offset: 0x0000BCD9
		// (set) Token: 0x06001EB9 RID: 7865 RVA: 0x0000DAE1 File Offset: 0x0000BCE1
		public string FrameId { get; set; }

		// Token: 0x040010C6 RID: 4294
		[CompilerGenerated]
		private string a;
	}
}
