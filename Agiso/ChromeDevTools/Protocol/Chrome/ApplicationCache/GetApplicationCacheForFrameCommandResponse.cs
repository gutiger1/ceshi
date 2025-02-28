using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.ApplicationCache
{
	// Token: 0x02000657 RID: 1623
	[CommandResponse("ApplicationCache.getApplicationCacheForFrame")]
	[SupportedBy("Chrome")]
	public class GetApplicationCacheForFrameCommandResponse
	{
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x0000DAEA File Offset: 0x0000BCEA
		// (set) Token: 0x06001EBC RID: 7868 RVA: 0x0000DAF2 File Offset: 0x0000BCF2
		public ApplicationCache ApplicationCache { get; set; }

		// Token: 0x040010C7 RID: 4295
		[CompilerGenerated]
		private ApplicationCache a;
	}
}
