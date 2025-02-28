using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Animation
{
	// Token: 0x0200065F RID: 1631
	[Event("Animation.animationPlayerCanceled")]
	[SupportedBy("Chrome")]
	public class AnimationPlayerCanceledEvent
	{
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x0000DC93 File Offset: 0x0000BE93
		// (set) Token: 0x06001EF6 RID: 7926 RVA: 0x0000DC9B File Offset: 0x0000BE9B
		public string PlayerId { get; set; }

		// Token: 0x040010E0 RID: 4320
		[CompilerGenerated]
		private string a;
	}
}
