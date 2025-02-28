using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x0200062B RID: 1579
	[SupportedBy("Chrome")]
	public class Call
	{
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x0000D523 File Offset: 0x0000B723
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x0000D52B File Offset: 0x0000B72B
		public string ContextId { get; set; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x0000D534 File Offset: 0x0000B734
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x0000D53C File Offset: 0x0000B73C
		public string FunctionName { get; set; }

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x0000D545 File Offset: 0x0000B745
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x0000D54D File Offset: 0x0000B74D
		public CallArgument[] Arguments { get; set; }

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x0000D556 File Offset: 0x0000B756
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x0000D55E File Offset: 0x0000B75E
		public CallArgument Result { get; set; }

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x0000D567 File Offset: 0x0000B767
		// (set) Token: 0x06001DEA RID: 7658 RVA: 0x0000D56F File Offset: 0x0000B76F
		public bool IsDrawingCall { get; set; }

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x0000D578 File Offset: 0x0000B778
		// (set) Token: 0x06001DEC RID: 7660 RVA: 0x0000D580 File Offset: 0x0000B780
		public bool IsFrameEndCall { get; set; }

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x0000D589 File Offset: 0x0000B789
		// (set) Token: 0x06001DEE RID: 7662 RVA: 0x0000D591 File Offset: 0x0000B791
		public string Property { get; set; }

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0000D59A File Offset: 0x0000B79A
		// (set) Token: 0x06001DF0 RID: 7664 RVA: 0x0000D5A2 File Offset: 0x0000B7A2
		public CallArgument Value { get; set; }

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x0000D5AB File Offset: 0x0000B7AB
		// (set) Token: 0x06001DF2 RID: 7666 RVA: 0x0000D5B3 File Offset: 0x0000B7B3
		public string SourceURL { get; set; }

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x0000D5BC File Offset: 0x0000B7BC
		// (set) Token: 0x06001DF4 RID: 7668 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		public long LineNumber { get; set; }

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x0000D5CD File Offset: 0x0000B7CD
		// (set) Token: 0x06001DF6 RID: 7670 RVA: 0x0000D5D5 File Offset: 0x0000B7D5
		public long ColumnNumber { get; set; }

		// Token: 0x04001070 RID: 4208
		[CompilerGenerated]
		private string a;

		// Token: 0x04001071 RID: 4209
		[CompilerGenerated]
		private string b;

		// Token: 0x04001072 RID: 4210
		[CompilerGenerated]
		private CallArgument[] c;

		// Token: 0x04001073 RID: 4211
		[CompilerGenerated]
		private CallArgument d;

		// Token: 0x04001074 RID: 4212
		[CompilerGenerated]
		private bool e;

		// Token: 0x04001075 RID: 4213
		[CompilerGenerated]
		private bool f;

		// Token: 0x04001076 RID: 4214
		[CompilerGenerated]
		private string g;

		// Token: 0x04001077 RID: 4215
		[CompilerGenerated]
		private CallArgument h;

		// Token: 0x04001078 RID: 4216
		[CompilerGenerated]
		private string i;

		// Token: 0x04001079 RID: 4217
		[CompilerGenerated]
		private long j;

		// Token: 0x0400107A RID: 4218
		[CompilerGenerated]
		private long k;
	}
}
