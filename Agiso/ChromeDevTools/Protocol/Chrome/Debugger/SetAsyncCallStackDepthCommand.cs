using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005C3 RID: 1475
	[Command("Debugger.setAsyncCallStackDepth")]
	[SupportedBy("Chrome")]
	public class SetAsyncCallStackDepthCommand
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x0000C951 File Offset: 0x0000AB51
		// (set) Token: 0x06001C17 RID: 7191 RVA: 0x0000C959 File Offset: 0x0000AB59
		public long MaxDepth { get; set; }

		// Token: 0x04000FB9 RID: 4025
		[CompilerGenerated]
		private long a;
	}
}
