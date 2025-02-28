using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000583 RID: 1411
	[SupportedBy("Chrome")]
	[Event("Debugger.breakpointResolved")]
	public class BreakpointResolvedEvent
	{
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x0000C137 File Offset: 0x0000A337
		// (set) Token: 0x06001AE3 RID: 6883 RVA: 0x0000C13F File Offset: 0x0000A33F
		public string BreakpointId { get; set; }

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x0000C148 File Offset: 0x0000A348
		// (set) Token: 0x06001AE5 RID: 6885 RVA: 0x0000C150 File Offset: 0x0000A350
		public Location Location { get; set; }

		// Token: 0x04000F3F RID: 3903
		[CompilerGenerated]
		private string a;

		// Token: 0x04000F40 RID: 3904
		[CompilerGenerated]
		private Location b;
	}
}
