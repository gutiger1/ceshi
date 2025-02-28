using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Animation
{
	// Token: 0x0200065E RID: 1630
	[SupportedBy("Chrome")]
	public class AnimationPlayer
	{
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x0000DC0B File Offset: 0x0000BE0B
		// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x0000DC13 File Offset: 0x0000BE13
		public string Id { get; set; }

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x0000DC24 File Offset: 0x0000BE24
		public bool PausedState { get; set; }

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x0000DC2D File Offset: 0x0000BE2D
		// (set) Token: 0x06001EE9 RID: 7913 RVA: 0x0000DC35 File Offset: 0x0000BE35
		public string PlayState { get; set; }

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x0000DC3E File Offset: 0x0000BE3E
		// (set) Token: 0x06001EEB RID: 7915 RVA: 0x0000DC46 File Offset: 0x0000BE46
		public double PlaybackRate { get; set; }

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x0000DC4F File Offset: 0x0000BE4F
		// (set) Token: 0x06001EED RID: 7917 RVA: 0x0000DC57 File Offset: 0x0000BE57
		public double StartTime { get; set; }

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0000DC60 File Offset: 0x0000BE60
		// (set) Token: 0x06001EEF RID: 7919 RVA: 0x0000DC68 File Offset: 0x0000BE68
		public double CurrentTime { get; set; }

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x0000DC71 File Offset: 0x0000BE71
		// (set) Token: 0x06001EF1 RID: 7921 RVA: 0x0000DC79 File Offset: 0x0000BE79
		public AnimationNode Source { get; set; }

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0000DC82 File Offset: 0x0000BE82
		// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x0000DC8A File Offset: 0x0000BE8A
		public string Type { get; set; }

		// Token: 0x040010D8 RID: 4312
		[CompilerGenerated]
		private string a;

		// Token: 0x040010D9 RID: 4313
		[CompilerGenerated]
		private bool b;

		// Token: 0x040010DA RID: 4314
		[CompilerGenerated]
		private string c;

		// Token: 0x040010DB RID: 4315
		[CompilerGenerated]
		private double d;

		// Token: 0x040010DC RID: 4316
		[CompilerGenerated]
		private double e;

		// Token: 0x040010DD RID: 4317
		[CompilerGenerated]
		private double f;

		// Token: 0x040010DE RID: 4318
		[CompilerGenerated]
		private AnimationNode g;

		// Token: 0x040010DF RID: 4319
		[CompilerGenerated]
		private string h;
	}
}
