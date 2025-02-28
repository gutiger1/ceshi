using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Animation
{
	// Token: 0x0200066B RID: 1643
	[SupportedBy("Chrome")]
	[Command("Animation.setPlaybackRate")]
	public class SetPlaybackRateCommand
	{
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x0000DD5F File Offset: 0x0000BF5F
		// (set) Token: 0x06001F1A RID: 7962 RVA: 0x0000DD67 File Offset: 0x0000BF67
		public double PlaybackRate { get; set; }

		// Token: 0x040010EC RID: 4332
		[CompilerGenerated]
		private double a;
	}
}
