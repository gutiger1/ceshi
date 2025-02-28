using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Animation
{
	// Token: 0x02000660 RID: 1632
	[Event("Animation.animationPlayerCreated")]
	[SupportedBy("Chrome")]
	public class AnimationPlayerCreatedEvent
	{
		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		// (set) Token: 0x06001EF9 RID: 7929 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		public AnimationPlayer Player { get; set; }

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x0000DCB5 File Offset: 0x0000BEB5
		// (set) Token: 0x06001EFB RID: 7931 RVA: 0x0000DCBD File Offset: 0x0000BEBD
		public bool ResetTimeline { get; set; }

		// Token: 0x040010E1 RID: 4321
		[CompilerGenerated]
		private AnimationPlayer a;

		// Token: 0x040010E2 RID: 4322
		[CompilerGenerated]
		private bool b;
	}
}
