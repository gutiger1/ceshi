using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Animation
{
	// Token: 0x0200065D RID: 1629
	[SupportedBy("Chrome")]
	public class AnimationNode
	{
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x0000DB3F File Offset: 0x0000BD3F
		// (set) Token: 0x06001ECC RID: 7884 RVA: 0x0000DB47 File Offset: 0x0000BD47
		public double Delay { get; set; }

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x0000DB50 File Offset: 0x0000BD50
		// (set) Token: 0x06001ECE RID: 7886 RVA: 0x0000DB58 File Offset: 0x0000BD58
		public double EndDelay { get; set; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x0000DB61 File Offset: 0x0000BD61
		// (set) Token: 0x06001ED0 RID: 7888 RVA: 0x0000DB69 File Offset: 0x0000BD69
		public double PlaybackRate { get; set; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x0000DB72 File Offset: 0x0000BD72
		// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x0000DB7A File Offset: 0x0000BD7A
		public double IterationStart { get; set; }

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x0000DB83 File Offset: 0x0000BD83
		// (set) Token: 0x06001ED4 RID: 7892 RVA: 0x0000DB8B File Offset: 0x0000BD8B
		public double Iterations { get; set; }

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x0000DB94 File Offset: 0x0000BD94
		// (set) Token: 0x06001ED6 RID: 7894 RVA: 0x0000DB9C File Offset: 0x0000BD9C
		public double Duration { get; set; }

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x0000DBA5 File Offset: 0x0000BDA5
		// (set) Token: 0x06001ED8 RID: 7896 RVA: 0x0000DBAD File Offset: 0x0000BDAD
		public string Direction { get; set; }

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x0000DBB6 File Offset: 0x0000BDB6
		// (set) Token: 0x06001EDA RID: 7898 RVA: 0x0000DBBE File Offset: 0x0000BDBE
		public string Fill { get; set; }

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x0000DBC7 File Offset: 0x0000BDC7
		// (set) Token: 0x06001EDC RID: 7900 RVA: 0x0000DBCF File Offset: 0x0000BDCF
		public string Name { get; set; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		// (set) Token: 0x06001EDE RID: 7902 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		public long BackendNodeId { get; set; }

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x0000DBE9 File Offset: 0x0000BDE9
		// (set) Token: 0x06001EE0 RID: 7904 RVA: 0x0000DBF1 File Offset: 0x0000BDF1
		public KeyframesRule KeyframesRule { get; set; }

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x0000DBFA File Offset: 0x0000BDFA
		// (set) Token: 0x06001EE2 RID: 7906 RVA: 0x0000DC02 File Offset: 0x0000BE02
		public string Easing { get; set; }

		// Token: 0x040010CC RID: 4300
		[CompilerGenerated]
		private double a;

		// Token: 0x040010CD RID: 4301
		[CompilerGenerated]
		private double b;

		// Token: 0x040010CE RID: 4302
		[CompilerGenerated]
		private double c;

		// Token: 0x040010CF RID: 4303
		[CompilerGenerated]
		private double d;

		// Token: 0x040010D0 RID: 4304
		[CompilerGenerated]
		private double e;

		// Token: 0x040010D1 RID: 4305
		[CompilerGenerated]
		private double f;

		// Token: 0x040010D2 RID: 4306
		[CompilerGenerated]
		private string g;

		// Token: 0x040010D3 RID: 4307
		[CompilerGenerated]
		private string h;

		// Token: 0x040010D4 RID: 4308
		[CompilerGenerated]
		private string i;

		// Token: 0x040010D5 RID: 4309
		[CompilerGenerated]
		private long j;

		// Token: 0x040010D6 RID: 4310
		[CompilerGenerated]
		private KeyframesRule k;

		// Token: 0x040010D7 RID: 4311
		[CompilerGenerated]
		private string l;
	}
}
