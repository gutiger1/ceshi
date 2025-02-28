using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Animation
{
	// Token: 0x02000663 RID: 1635
	[SupportedBy("Chrome")]
	[Command("Animation.getAnimationPlayersForNode")]
	public class GetAnimationPlayersForNodeCommand
	{
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x0000DCC6 File Offset: 0x0000BEC6
		// (set) Token: 0x06001F00 RID: 7936 RVA: 0x0000DCCE File Offset: 0x0000BECE
		public long NodeId { get; set; }

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x0000DCD7 File Offset: 0x0000BED7
		// (set) Token: 0x06001F02 RID: 7938 RVA: 0x0000DCDF File Offset: 0x0000BEDF
		public bool IncludeSubtreeAnimations { get; set; }

		// Token: 0x040010E3 RID: 4323
		[CompilerGenerated]
		private long a;

		// Token: 0x040010E4 RID: 4324
		[CompilerGenerated]
		private bool b;
	}
}
