using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CacheStorage
{
	// Token: 0x02000649 RID: 1609
	[SupportedBy("Chrome")]
	public class DataEntry
	{
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x0000D941 File Offset: 0x0000BB41
		// (set) Token: 0x06001E7C RID: 7804 RVA: 0x0000D949 File Offset: 0x0000BB49
		public string Request { get; set; }

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x0000D952 File Offset: 0x0000BB52
		// (set) Token: 0x06001E7E RID: 7806 RVA: 0x0000D95A File Offset: 0x0000BB5A
		public string Response { get; set; }

		// Token: 0x040010AE RID: 4270
		[CompilerGenerated]
		private string a;

		// Token: 0x040010AF RID: 4271
		[CompilerGenerated]
		private string b;
	}
}
