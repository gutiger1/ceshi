using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Animation
{
	// Token: 0x02000669 RID: 1641
	[Command("Animation.setCurrentTime")]
	[SupportedBy("Chrome")]
	public class SetCurrentTimeCommand
	{
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x0000DD4E File Offset: 0x0000BF4E
		// (set) Token: 0x06001F16 RID: 7958 RVA: 0x0000DD56 File Offset: 0x0000BF56
		public double CurrentTime { get; set; }

		// Token: 0x040010EB RID: 4331
		[CompilerGenerated]
		private double a;
	}
}
