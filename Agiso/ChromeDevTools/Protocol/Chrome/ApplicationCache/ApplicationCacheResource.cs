using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.ApplicationCache
{
	// Token: 0x02000651 RID: 1617
	[SupportedBy("Chrome")]
	public class ApplicationCacheResource
	{
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x0000DA40 File Offset: 0x0000BC40
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x0000DA48 File Offset: 0x0000BC48
		public string Url { get; set; }

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x0000DA51 File Offset: 0x0000BC51
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x0000DA59 File Offset: 0x0000BC59
		public long Size { get; set; }

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x0000DA62 File Offset: 0x0000BC62
		// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x0000DA6A File Offset: 0x0000BC6A
		public string Type { get; set; }

		// Token: 0x040010BD RID: 4285
		[CompilerGenerated]
		private string a;

		// Token: 0x040010BE RID: 4286
		[CompilerGenerated]
		private long b;

		// Token: 0x040010BF RID: 4287
		[CompilerGenerated]
		private string c;
	}
}
