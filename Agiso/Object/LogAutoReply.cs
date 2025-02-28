using System;
using System.Runtime.CompilerServices;

namespace Agiso.Object
{
	// Token: 0x02000685 RID: 1669
	public class LogAutoReply
	{
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x0000E1B9 File Offset: 0x0000C3B9
		// (set) Token: 0x06001FB6 RID: 8118 RVA: 0x0000E1C1 File Offset: 0x0000C3C1
		public long IdNo { get; set; }

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x0000E1CA File Offset: 0x0000C3CA
		// (set) Token: 0x06001FB8 RID: 8120 RVA: 0x0000E1D2 File Offset: 0x0000C3D2
		public string SellerNick { get; set; }

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x0000E1DB File Offset: 0x0000C3DB
		// (set) Token: 0x06001FBA RID: 8122 RVA: 0x0000E1E3 File Offset: 0x0000C3E3
		public string SenderNick { get; set; }

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x0000E1EC File Offset: 0x0000C3EC
		// (set) Token: 0x06001FBC RID: 8124 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		public string SenderId { get; set; }

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x0000E1FD File Offset: 0x0000C3FD
		// (set) Token: 0x06001FBE RID: 8126 RVA: 0x0000E205 File Offset: 0x0000C405
		public string SenderOpenUid { get; set; }

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x0000E20E File Offset: 0x0000C40E
		// (set) Token: 0x06001FC0 RID: 8128 RVA: 0x0000E216 File Offset: 0x0000C416
		public string ConsultWord { get; set; }

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x0000E21F File Offset: 0x0000C41F
		// (set) Token: 0x06001FC2 RID: 8130 RVA: 0x0000E227 File Offset: 0x0000C427
		public string ConsultTime { get; set; }

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x0000E230 File Offset: 0x0000C430
		// (set) Token: 0x06001FC4 RID: 8132 RVA: 0x0000E238 File Offset: 0x0000C438
		public EnumArType MatchType { get; set; }

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x0000E241 File Offset: 0x0000C441
		// (set) Token: 0x06001FC6 RID: 8134 RVA: 0x0000E249 File Offset: 0x0000C449
		public string KeyWord { get; set; }

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x0000E252 File Offset: 0x0000C452
		// (set) Token: 0x06001FC8 RID: 8136 RVA: 0x0000E25A File Offset: 0x0000C45A
		public string ReplyWord { get; set; }

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x0000E263 File Offset: 0x0000C463
		// (set) Token: 0x06001FCA RID: 8138 RVA: 0x0000E26B File Offset: 0x0000C46B
		public string DutyManualNick { get; set; }

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x0000E274 File Offset: 0x0000C474
		// (set) Token: 0x06001FCC RID: 8140 RVA: 0x0000E27C File Offset: 0x0000C47C
		public int FromType { get; set; }

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x0000E285 File Offset: 0x0000C485
		// (set) Token: 0x06001FCE RID: 8142 RVA: 0x0000E28D File Offset: 0x0000C48D
		public int IsTransferFail { get; set; }

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x0000E296 File Offset: 0x0000C496
		// (set) Token: 0x06001FD0 RID: 8144 RVA: 0x0000E29E File Offset: 0x0000C49E
		public string TransferFailMsg { get; set; }

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x0000E2A7 File Offset: 0x0000C4A7
		// (set) Token: 0x06001FD2 RID: 8146 RVA: 0x0000E2AF File Offset: 0x0000C4AF
		public DateTime CreateTime { get; set; }

		// Token: 0x040011F0 RID: 4592
		[CompilerGenerated]
		private long a;

		// Token: 0x040011F1 RID: 4593
		[CompilerGenerated]
		private string b;

		// Token: 0x040011F2 RID: 4594
		[CompilerGenerated]
		private string c;

		// Token: 0x040011F3 RID: 4595
		[CompilerGenerated]
		private string d;

		// Token: 0x040011F4 RID: 4596
		[CompilerGenerated]
		private string e;

		// Token: 0x040011F5 RID: 4597
		[CompilerGenerated]
		private string f;

		// Token: 0x040011F6 RID: 4598
		[CompilerGenerated]
		private string g;

		// Token: 0x040011F7 RID: 4599
		[CompilerGenerated]
		private EnumArType h;

		// Token: 0x040011F8 RID: 4600
		[CompilerGenerated]
		private string i;

		// Token: 0x040011F9 RID: 4601
		[CompilerGenerated]
		private string j;

		// Token: 0x040011FA RID: 4602
		[CompilerGenerated]
		private string k;

		// Token: 0x040011FB RID: 4603
		[CompilerGenerated]
		private int l;

		// Token: 0x040011FC RID: 4604
		[CompilerGenerated]
		private int m;

		// Token: 0x040011FD RID: 4605
		[CompilerGenerated]
		private string n;

		// Token: 0x040011FE RID: 4606
		[CompilerGenerated]
		private DateTime o;
	}
}
