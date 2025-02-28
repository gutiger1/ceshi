using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005B6 RID: 1462
	[SupportedBy("Chrome")]
	[Command("Debugger.restartFrame")]
	public class RestartFrameCommand
	{
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0000C6BA File Offset: 0x0000A8BA
		// (set) Token: 0x06001BBC RID: 7100 RVA: 0x0000C6C2 File Offset: 0x0000A8C2
		public string CallFrameId { get; set; }

		// Token: 0x04000F92 RID: 3986
		[CompilerGenerated]
		private string a;
	}
}
