using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Animation
{
	// Token: 0x0200066D RID: 1645
	[Command("Animation.setTiming")]
	[SupportedBy("Chrome")]
	public class SetTimingCommand
	{
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x0000DD70 File Offset: 0x0000BF70
		// (set) Token: 0x06001F1E RID: 7966 RVA: 0x0000DD78 File Offset: 0x0000BF78
		public string PlayerId { get; set; }

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x0000DD81 File Offset: 0x0000BF81
		// (set) Token: 0x06001F20 RID: 7968 RVA: 0x0000DD89 File Offset: 0x0000BF89
		public double Duration { get; set; }

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x0000DD92 File Offset: 0x0000BF92
		// (set) Token: 0x06001F22 RID: 7970 RVA: 0x0000DD9A File Offset: 0x0000BF9A
		public double Delay { get; set; }

		// Token: 0x040010ED RID: 4333
		[CompilerGenerated]
		private string a;

		// Token: 0x040010EE RID: 4334
		[CompilerGenerated]
		private double b;

		// Token: 0x040010EF RID: 4335
		[CompilerGenerated]
		private double c;
	}
}
