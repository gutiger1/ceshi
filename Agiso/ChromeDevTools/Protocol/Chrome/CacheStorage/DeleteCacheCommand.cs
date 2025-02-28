using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CacheStorage
{
	// Token: 0x0200064A RID: 1610
	[Command("CacheStorage.deleteCache")]
	[SupportedBy("Chrome")]
	public class DeleteCacheCommand
	{
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0000D963 File Offset: 0x0000BB63
		// (set) Token: 0x06001E81 RID: 7809 RVA: 0x0000D96B File Offset: 0x0000BB6B
		public string CacheId { get; set; }

		// Token: 0x040010B0 RID: 4272
		[CompilerGenerated]
		private string a;
	}
}
