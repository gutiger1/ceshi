using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005B4 RID: 1460
	[Command("Debugger.removeBreakpoint")]
	[SupportedBy("Chrome")]
	public class RemoveBreakpointCommand
	{
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x0000C6A9 File Offset: 0x0000A8A9
		// (set) Token: 0x06001BB8 RID: 7096 RVA: 0x0000C6B1 File Offset: 0x0000A8B1
		public string BreakpointId { get; set; }

		// Token: 0x04000F91 RID: 3985
		[CompilerGenerated]
		private string a;
	}
}
