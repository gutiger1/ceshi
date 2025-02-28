using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x0200059C RID: 1436
	[CommandResponse("Debugger.getBacktrace")]
	[SupportedBy("Chrome")]
	public class GetBacktraceCommandResponse
	{
		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x0000C434 File Offset: 0x0000A634
		// (set) Token: 0x06001B56 RID: 6998 RVA: 0x0000C43C File Offset: 0x0000A63C
		public CallFrame[] CallFrames { get; set; }

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0000C445 File Offset: 0x0000A645
		// (set) Token: 0x06001B58 RID: 7000 RVA: 0x0000C44D File Offset: 0x0000A64D
		public StackTrace AsyncStackTrace { get; set; }

		// Token: 0x04000F6C RID: 3948
		[CompilerGenerated]
		private CallFrame[] a;

		// Token: 0x04000F6D RID: 3949
		[CompilerGenerated]
		private StackTrace b;
	}
}
