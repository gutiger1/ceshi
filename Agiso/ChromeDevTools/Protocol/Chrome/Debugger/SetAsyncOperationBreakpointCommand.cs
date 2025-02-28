using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005C5 RID: 1477
	[Command("Debugger.setAsyncOperationBreakpoint")]
	[SupportedBy("Chrome")]
	public class SetAsyncOperationBreakpointCommand
	{
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x0000C962 File Offset: 0x0000AB62
		// (set) Token: 0x06001C1B RID: 7195 RVA: 0x0000C96A File Offset: 0x0000AB6A
		public long OperationId { get; set; }

		// Token: 0x04000FBA RID: 4026
		[CompilerGenerated]
		private long a;
	}
}
