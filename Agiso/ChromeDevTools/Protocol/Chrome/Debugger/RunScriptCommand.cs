using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005BB RID: 1467
	[SupportedBy("Chrome")]
	[Command("Debugger.runScript")]
	public class RunScriptCommand
	{
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x0000C6FE File Offset: 0x0000A8FE
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x0000C706 File Offset: 0x0000A906
		public string ScriptId { get; set; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x0000C70F File Offset: 0x0000A90F
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x0000C717 File Offset: 0x0000A917
		public long ExecutionContextId { get; set; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0000C720 File Offset: 0x0000A920
		// (set) Token: 0x06001BCD RID: 7117 RVA: 0x0000C728 File Offset: 0x0000A928
		public string ObjectGroup { get; set; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0000C731 File Offset: 0x0000A931
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x0000C739 File Offset: 0x0000A939
		public bool DoNotPauseOnExceptionsAndMuteConsole { get; set; }

		// Token: 0x04000F96 RID: 3990
		[CompilerGenerated]
		private string a;

		// Token: 0x04000F97 RID: 3991
		[CompilerGenerated]
		private long b;

		// Token: 0x04000F98 RID: 3992
		[CompilerGenerated]
		private string c;

		// Token: 0x04000F99 RID: 3993
		[CompilerGenerated]
		private bool d;
	}
}
