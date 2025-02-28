using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000586 RID: 1414
	[CommandResponse("Debugger.canSetScriptSource")]
	[SupportedBy("Chrome")]
	public class CanSetScriptSourceCommandResponse
	{
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		public bool Result { get; set; }

		// Token: 0x04000F48 RID: 3912
		[CompilerGenerated]
		private bool a;
	}
}
