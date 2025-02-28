using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.ApplicationCache
{
	// Token: 0x02000650 RID: 1616
	[SupportedBy("Chrome")]
	public class ApplicationCache
	{
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0000D9EB File Offset: 0x0000BBEB
		// (set) Token: 0x06001E97 RID: 7831 RVA: 0x0000D9F3 File Offset: 0x0000BBF3
		public string ManifestURL { get; set; }

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		// (set) Token: 0x06001E99 RID: 7833 RVA: 0x0000DA04 File Offset: 0x0000BC04
		public double Size { get; set; }

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x0000DA0D File Offset: 0x0000BC0D
		// (set) Token: 0x06001E9B RID: 7835 RVA: 0x0000DA15 File Offset: 0x0000BC15
		public double CreationTime { get; set; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x0000DA1E File Offset: 0x0000BC1E
		// (set) Token: 0x06001E9D RID: 7837 RVA: 0x0000DA26 File Offset: 0x0000BC26
		public double UpdateTime { get; set; }

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x0000DA2F File Offset: 0x0000BC2F
		// (set) Token: 0x06001E9F RID: 7839 RVA: 0x0000DA37 File Offset: 0x0000BC37
		public ApplicationCacheResource[] Resources { get; set; }

		// Token: 0x040010B8 RID: 4280
		[CompilerGenerated]
		private string a;

		// Token: 0x040010B9 RID: 4281
		[CompilerGenerated]
		private double b;

		// Token: 0x040010BA RID: 4282
		[CompilerGenerated]
		private double c;

		// Token: 0x040010BB RID: 4283
		[CompilerGenerated]
		private double d;

		// Token: 0x040010BC RID: 4284
		[CompilerGenerated]
		private ApplicationCacheResource[] e;
	}
}
