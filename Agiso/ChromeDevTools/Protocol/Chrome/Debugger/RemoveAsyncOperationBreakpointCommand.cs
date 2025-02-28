using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005B2 RID: 1458
	[Command("Debugger.removeAsyncOperationBreakpoint")]
	[SupportedBy("Chrome")]
	public class RemoveAsyncOperationBreakpointCommand
	{
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0000C698 File Offset: 0x0000A898
		// (set) Token: 0x06001BB4 RID: 7092 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
		public long OperationId { get; set; }

		// Token: 0x04000F90 RID: 3984
		[CompilerGenerated]
		private long a;
	}
}
