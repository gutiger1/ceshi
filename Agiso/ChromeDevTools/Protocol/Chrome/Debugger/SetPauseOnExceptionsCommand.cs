using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005CD RID: 1485
	[SupportedBy("Chrome")]
	[Command("Debugger.setPauseOnExceptions")]
	public class SetPauseOnExceptionsCommand
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0000CA3F File Offset: 0x0000AC3F
		// (set) Token: 0x06001C3D RID: 7229 RVA: 0x0000CA47 File Offset: 0x0000AC47
		public string State { get; set; }

		// Token: 0x04000FC7 RID: 4039
		[CompilerGenerated]
		private string a;
	}
}
