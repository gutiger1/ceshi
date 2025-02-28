using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x02000646 RID: 1606
	[SupportedBy("Chrome")]
	public class TraceLog
	{
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x0000D886 File Offset: 0x0000BA86
		// (set) Token: 0x06001E63 RID: 7779 RVA: 0x0000D88E File Offset: 0x0000BA8E
		public string Id { get; set; }

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x0000D897 File Offset: 0x0000BA97
		// (set) Token: 0x06001E65 RID: 7781 RVA: 0x0000D89F File Offset: 0x0000BA9F
		public Call[] Calls { get; set; }

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		// (set) Token: 0x06001E67 RID: 7783 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		public CallArgument[] Contexts { get; set; }

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x0000D8B9 File Offset: 0x0000BAB9
		// (set) Token: 0x06001E69 RID: 7785 RVA: 0x0000D8C1 File Offset: 0x0000BAC1
		public long StartOffset { get; set; }

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x0000D8CA File Offset: 0x0000BACA
		// (set) Token: 0x06001E6B RID: 7787 RVA: 0x0000D8D2 File Offset: 0x0000BAD2
		public bool Alive { get; set; }

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x0000D8DB File Offset: 0x0000BADB
		// (set) Token: 0x06001E6D RID: 7789 RVA: 0x0000D8E3 File Offset: 0x0000BAE3
		public double TotalAvailableCalls { get; set; }

		// Token: 0x040010A3 RID: 4259
		[CompilerGenerated]
		private string a;

		// Token: 0x040010A4 RID: 4260
		[CompilerGenerated]
		private Call[] b;

		// Token: 0x040010A5 RID: 4261
		[CompilerGenerated]
		private CallArgument[] c;

		// Token: 0x040010A6 RID: 4262
		[CompilerGenerated]
		private long d;

		// Token: 0x040010A7 RID: 4263
		[CompilerGenerated]
		private bool e;

		// Token: 0x040010A8 RID: 4264
		[CompilerGenerated]
		private double f;
	}
}
