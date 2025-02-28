using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005D0 RID: 1488
	[SupportedBy("Chrome")]
	[CommandResponse("Debugger.setScriptSource")]
	public class SetScriptSourceCommandResponse
	{
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x0000CA83 File Offset: 0x0000AC83
		// (set) Token: 0x06001C48 RID: 7240 RVA: 0x0000CA8B File Offset: 0x0000AC8B
		public CallFrame[] CallFrames { get; set; }

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x0000CA94 File Offset: 0x0000AC94
		// (set) Token: 0x06001C4A RID: 7242 RVA: 0x0000CA9C File Offset: 0x0000AC9C
		public object Result { get; set; }

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x0000CAA5 File Offset: 0x0000ACA5
		// (set) Token: 0x06001C4C RID: 7244 RVA: 0x0000CAAD File Offset: 0x0000ACAD
		public StackTrace AsyncStackTrace { get; set; }

		// Token: 0x04000FCB RID: 4043
		[CompilerGenerated]
		private CallFrame[] a;

		// Token: 0x04000FCC RID: 4044
		[CompilerGenerated]
		private object b;

		// Token: 0x04000FCD RID: 4045
		[CompilerGenerated]
		private StackTrace c;
	}
}
