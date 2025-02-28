using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005B7 RID: 1463
	[CommandResponse("Debugger.restartFrame")]
	[SupportedBy("Chrome")]
	public class RestartFrameCommandResponse
	{
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0000C6CB File Offset: 0x0000A8CB
		// (set) Token: 0x06001BBF RID: 7103 RVA: 0x0000C6D3 File Offset: 0x0000A8D3
		public CallFrame[] CallFrames { get; set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x0000C6DC File Offset: 0x0000A8DC
		// (set) Token: 0x06001BC1 RID: 7105 RVA: 0x0000C6E4 File Offset: 0x0000A8E4
		public object Result { get; set; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x0000C6ED File Offset: 0x0000A8ED
		// (set) Token: 0x06001BC3 RID: 7107 RVA: 0x0000C6F5 File Offset: 0x0000A8F5
		public StackTrace AsyncStackTrace { get; set; }

		// Token: 0x04000F93 RID: 3987
		[CompilerGenerated]
		private CallFrame[] a;

		// Token: 0x04000F94 RID: 3988
		[CompilerGenerated]
		private object b;

		// Token: 0x04000F95 RID: 3989
		[CompilerGenerated]
		private StackTrace c;
	}
}
