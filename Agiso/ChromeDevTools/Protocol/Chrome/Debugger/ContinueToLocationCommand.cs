using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x0200058A RID: 1418
	[Command("Debugger.continueToLocation")]
	[SupportedBy("Chrome")]
	public class ContinueToLocationCommand
	{
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x0000C269 File Offset: 0x0000A469
		// (set) Token: 0x06001B0E RID: 6926 RVA: 0x0000C271 File Offset: 0x0000A471
		public Location Location { get; set; }

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x0000C27A File Offset: 0x0000A47A
		// (set) Token: 0x06001B10 RID: 6928 RVA: 0x0000C282 File Offset: 0x0000A482
		public bool InterstatementLocation { get; set; }

		// Token: 0x04000F51 RID: 3921
		[CompilerGenerated]
		private Location a;

		// Token: 0x04000F52 RID: 3922
		[CompilerGenerated]
		private bool b;
	}
}
