using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005A7 RID: 1447
	[SupportedBy("Chrome")]
	[Command("Debugger.getScriptSource")]
	public class GetScriptSourceCommand
	{
		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x0000C500 File Offset: 0x0000A700
		// (set) Token: 0x06001B79 RID: 7033 RVA: 0x0000C508 File Offset: 0x0000A708
		public string ScriptId { get; set; }

		// Token: 0x04000F78 RID: 3960
		[CompilerGenerated]
		private string a;
	}
}
