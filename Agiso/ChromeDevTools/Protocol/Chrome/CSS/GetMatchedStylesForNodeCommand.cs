using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000600 RID: 1536
	[SupportedBy("Chrome")]
	[Command("CSS.getMatchedStylesForNode")]
	public class GetMatchedStylesForNodeCommand
	{
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0000CFD3 File Offset: 0x0000B1D3
		// (set) Token: 0x06001D18 RID: 7448 RVA: 0x0000CFDB File Offset: 0x0000B1DB
		public long NodeId { get; set; }

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0000CFE4 File Offset: 0x0000B1E4
		// (set) Token: 0x06001D1A RID: 7450 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		public bool ExcludePseudo { get; set; }

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0000CFF5 File Offset: 0x0000B1F5
		// (set) Token: 0x06001D1C RID: 7452 RVA: 0x0000CFFD File Offset: 0x0000B1FD
		public bool ExcludeInherited { get; set; }

		// Token: 0x0400101B RID: 4123
		[CompilerGenerated]
		private long a;

		// Token: 0x0400101C RID: 4124
		[CompilerGenerated]
		private bool b;

		// Token: 0x0400101D RID: 4125
		[CompilerGenerated]
		private bool c;
	}
}
