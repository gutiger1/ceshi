using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005A0 RID: 1440
	[SupportedBy("Chrome")]
	[CommandResponse("Debugger.getFunctionDetails")]
	public class GetFunctionDetailsCommandResponse
	{
		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0000C489 File Offset: 0x0000A689
		// (set) Token: 0x06001B64 RID: 7012 RVA: 0x0000C491 File Offset: 0x0000A691
		public FunctionDetails Details { get; set; }

		// Token: 0x04000F71 RID: 3953
		[CompilerGenerated]
		private FunctionDetails a;
	}
}
