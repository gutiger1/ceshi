using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005CB RID: 1483
	[Command("Debugger.setBreakpointsActive")]
	[SupportedBy("Chrome")]
	public class SetBreakpointsActiveCommand
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0000CA2E File Offset: 0x0000AC2E
		// (set) Token: 0x06001C39 RID: 7225 RVA: 0x0000CA36 File Offset: 0x0000AC36
		public bool Active { get; set; }

		// Token: 0x04000FC6 RID: 4038
		[CompilerGenerated]
		private bool a;
	}
}
