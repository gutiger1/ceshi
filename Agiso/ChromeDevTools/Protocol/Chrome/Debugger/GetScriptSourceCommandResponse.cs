using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005A8 RID: 1448
	[CommandResponse("Debugger.getScriptSource")]
	[SupportedBy("Chrome")]
	public class GetScriptSourceCommandResponse
	{
		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x0000C511 File Offset: 0x0000A711
		// (set) Token: 0x06001B7C RID: 7036 RVA: 0x0000C519 File Offset: 0x0000A719
		public string ScriptSource { get; set; }

		// Token: 0x04000F79 RID: 3961
		[CompilerGenerated]
		private string a;
	}
}
