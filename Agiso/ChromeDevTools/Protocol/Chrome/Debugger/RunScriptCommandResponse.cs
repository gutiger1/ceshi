using System;
using System.Runtime.CompilerServices;
using Agiso.ChromeDevTools.Protocol.Chrome.Runtime;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005BC RID: 1468
	[CommandResponse("Debugger.runScript")]
	[SupportedBy("Chrome")]
	public class RunScriptCommandResponse
	{
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x0000C742 File Offset: 0x0000A942
		// (set) Token: 0x06001BD2 RID: 7122 RVA: 0x0000C74A File Offset: 0x0000A94A
		public RemoteObject Result { get; set; }

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x0000C753 File Offset: 0x0000A953
		// (set) Token: 0x06001BD4 RID: 7124 RVA: 0x0000C75B File Offset: 0x0000A95B
		public ExceptionDetails ExceptionDetails { get; set; }

		// Token: 0x04000F9A RID: 3994
		[CompilerGenerated]
		private RemoteObject a;

		// Token: 0x04000F9B RID: 3995
		[CompilerGenerated]
		private ExceptionDetails b;
	}
}
