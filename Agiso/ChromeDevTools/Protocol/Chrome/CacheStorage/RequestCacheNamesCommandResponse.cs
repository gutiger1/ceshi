using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CacheStorage
{
	// Token: 0x0200064D RID: 1613
	[CommandResponse("CacheStorage.requestCacheNames")]
	[SupportedBy("Chrome")]
	public class RequestCacheNamesCommandResponse
	{
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0000D985 File Offset: 0x0000BB85
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x0000D98D File Offset: 0x0000BB8D
		public Cache[] Caches { get; set; }

		// Token: 0x040010B2 RID: 4274
		[CompilerGenerated]
		private Cache[] a;
	}
}
