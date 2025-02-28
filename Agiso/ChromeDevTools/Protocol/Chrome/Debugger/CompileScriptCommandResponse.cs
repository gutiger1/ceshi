using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000589 RID: 1417
	[CommandResponse("Debugger.compileScript")]
	[SupportedBy("Chrome")]
	public class CompileScriptCommandResponse
	{
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x0000C247 File Offset: 0x0000A447
		// (set) Token: 0x06001B09 RID: 6921 RVA: 0x0000C24F File Offset: 0x0000A44F
		public string ScriptId { get; set; }

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x0000C258 File Offset: 0x0000A458
		// (set) Token: 0x06001B0B RID: 6923 RVA: 0x0000C260 File Offset: 0x0000A460
		public ExceptionDetails ExceptionDetails { get; set; }

		// Token: 0x04000F4F RID: 3919
		[CompilerGenerated]
		private string a;

		// Token: 0x04000F50 RID: 3920
		[CompilerGenerated]
		private ExceptionDetails b;
	}
}
