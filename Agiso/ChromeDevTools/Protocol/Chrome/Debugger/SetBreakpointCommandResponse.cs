using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005CA RID: 1482
	[CommandResponse("Debugger.setBreakpoint")]
	[SupportedBy("Chrome")]
	public class SetBreakpointCommandResponse
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x0000CA0C File Offset: 0x0000AC0C
		// (set) Token: 0x06001C34 RID: 7220 RVA: 0x0000CA14 File Offset: 0x0000AC14
		public string BreakpointId { get; set; }

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x0000CA1D File Offset: 0x0000AC1D
		// (set) Token: 0x06001C36 RID: 7222 RVA: 0x0000CA25 File Offset: 0x0000AC25
		public Location ActualLocation { get; set; }

		// Token: 0x04000FC4 RID: 4036
		[CompilerGenerated]
		private string a;

		// Token: 0x04000FC5 RID: 4037
		[CompilerGenerated]
		private Location b;
	}
}
