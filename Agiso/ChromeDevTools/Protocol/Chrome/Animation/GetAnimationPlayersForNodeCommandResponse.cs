using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Animation
{
	// Token: 0x02000664 RID: 1636
	[CommandResponse("Animation.getAnimationPlayersForNode")]
	[SupportedBy("Chrome")]
	public class GetAnimationPlayersForNodeCommandResponse
	{
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		// (set) Token: 0x06001F05 RID: 7941 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		public AnimationPlayer[] AnimationPlayers { get; set; }

		// Token: 0x040010E5 RID: 4325
		[CompilerGenerated]
		private AnimationPlayer[] a;
	}
}
