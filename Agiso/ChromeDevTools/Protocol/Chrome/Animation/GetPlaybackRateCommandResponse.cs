using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Animation
{
	// Token: 0x02000666 RID: 1638
	[CommandResponse("Animation.getPlaybackRate")]
	[SupportedBy("Chrome")]
	public class GetPlaybackRateCommandResponse
	{
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x0000DCF9 File Offset: 0x0000BEF9
		// (set) Token: 0x06001F09 RID: 7945 RVA: 0x0000DD01 File Offset: 0x0000BF01
		public double PlaybackRate { get; set; }

		// Token: 0x040010E6 RID: 4326
		[CompilerGenerated]
		private double a;
	}
}
