using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005C9 RID: 1481
	[SupportedBy("Chrome")]
	[Command("Debugger.setBreakpoint")]
	public class SetBreakpointCommand
	{
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x0000C9EA File Offset: 0x0000ABEA
		// (set) Token: 0x06001C2F RID: 7215 RVA: 0x0000C9F2 File Offset: 0x0000ABF2
		public Location Location { get; set; }

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0000C9FB File Offset: 0x0000ABFB
		// (set) Token: 0x06001C31 RID: 7217 RVA: 0x0000CA03 File Offset: 0x0000AC03
		public string Condition { get; set; }

		// Token: 0x04000FC2 RID: 4034
		[CompilerGenerated]
		private Location a;

		// Token: 0x04000FC3 RID: 4035
		[CompilerGenerated]
		private string b;
	}
}
