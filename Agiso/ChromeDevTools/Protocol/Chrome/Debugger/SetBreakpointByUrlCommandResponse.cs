using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005C8 RID: 1480
	[SupportedBy("Chrome")]
	[CommandResponse("Debugger.setBreakpointByUrl")]
	public class SetBreakpointByUrlCommandResponse
	{
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		// (set) Token: 0x06001C2A RID: 7210 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		public string BreakpointId { get; set; }

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x0000C9D9 File Offset: 0x0000ABD9
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x0000C9E1 File Offset: 0x0000ABE1
		public Location[] Locations { get; set; }

		// Token: 0x04000FC0 RID: 4032
		[CompilerGenerated]
		private string a;

		// Token: 0x04000FC1 RID: 4033
		[CompilerGenerated]
		private Location[] b;
	}
}
