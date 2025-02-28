using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CacheStorage
{
	// Token: 0x02000648 RID: 1608
	[SupportedBy("Chrome")]
	public class Cache
	{
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x0000D90E File Offset: 0x0000BB0E
		// (set) Token: 0x06001E75 RID: 7797 RVA: 0x0000D916 File Offset: 0x0000BB16
		public string CacheId { get; set; }

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06001E76 RID: 7798 RVA: 0x0000D91F File Offset: 0x0000BB1F
		// (set) Token: 0x06001E77 RID: 7799 RVA: 0x0000D927 File Offset: 0x0000BB27
		public string SecurityOrigin { get; set; }

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x0000D930 File Offset: 0x0000BB30
		// (set) Token: 0x06001E79 RID: 7801 RVA: 0x0000D938 File Offset: 0x0000BB38
		public string CacheName { get; set; }

		// Token: 0x040010AB RID: 4267
		[CompilerGenerated]
		private string a;

		// Token: 0x040010AC RID: 4268
		[CompilerGenerated]
		private string b;

		// Token: 0x040010AD RID: 4269
		[CompilerGenerated]
		private string c;
	}
}
