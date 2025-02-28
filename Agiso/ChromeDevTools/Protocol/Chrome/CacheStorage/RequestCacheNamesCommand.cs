using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CacheStorage
{
	// Token: 0x0200064C RID: 1612
	[Command("CacheStorage.requestCacheNames")]
	[SupportedBy("Chrome")]
	public class RequestCacheNamesCommand
	{
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001E84 RID: 7812 RVA: 0x0000D974 File Offset: 0x0000BB74
		// (set) Token: 0x06001E85 RID: 7813 RVA: 0x0000D97C File Offset: 0x0000BB7C
		public string SecurityOrigin { get; set; }

		// Token: 0x040010B1 RID: 4273
		[CompilerGenerated]
		private string a;
	}
}
