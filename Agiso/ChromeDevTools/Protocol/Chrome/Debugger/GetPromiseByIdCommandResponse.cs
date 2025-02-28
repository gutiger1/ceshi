using System;
using System.Runtime.CompilerServices;
using Agiso.ChromeDevTools.Protocol.Chrome.Runtime;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005A4 RID: 1444
	[SupportedBy("Chrome")]
	[CommandResponse("Debugger.getPromiseById")]
	public class GetPromiseByIdCommandResponse
	{
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0000C4DE File Offset: 0x0000A6DE
		// (set) Token: 0x06001B72 RID: 7026 RVA: 0x0000C4E6 File Offset: 0x0000A6E6
		public RemoteObject Promise { get; set; }

		// Token: 0x04000F76 RID: 3958
		[CompilerGenerated]
		private RemoteObject a;
	}
}
