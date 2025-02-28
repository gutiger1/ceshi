using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CacheStorage
{
	// Token: 0x0200064E RID: 1614
	[Command("CacheStorage.requestEntries")]
	[SupportedBy("Chrome")]
	public class RequestEntriesCommand
	{
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x0000D996 File Offset: 0x0000BB96
		// (set) Token: 0x06001E8B RID: 7819 RVA: 0x0000D99E File Offset: 0x0000BB9E
		public string CacheId { get; set; }

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0000D9A7 File Offset: 0x0000BBA7
		// (set) Token: 0x06001E8D RID: 7821 RVA: 0x0000D9AF File Offset: 0x0000BBAF
		public long SkipCount { get; set; }

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		// (set) Token: 0x06001E8F RID: 7823 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
		public long PageSize { get; set; }

		// Token: 0x040010B3 RID: 4275
		[CompilerGenerated]
		private string a;

		// Token: 0x040010B4 RID: 4276
		[CompilerGenerated]
		private long b;

		// Token: 0x040010B5 RID: 4277
		[CompilerGenerated]
		private long c;
	}
}
