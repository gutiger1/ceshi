using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005D6 RID: 1494
	[Command("Debugger.skipStackFrames")]
	[SupportedBy("Chrome")]
	public class SkipStackFramesCommand
	{
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x0000CB2D File Offset: 0x0000AD2D
		// (set) Token: 0x06001C62 RID: 7266 RVA: 0x0000CB35 File Offset: 0x0000AD35
		public string Script { get; set; }

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x0000CB3E File Offset: 0x0000AD3E
		// (set) Token: 0x06001C64 RID: 7268 RVA: 0x0000CB46 File Offset: 0x0000AD46
		public bool SkipContentScripts { get; set; }

		// Token: 0x04000FD5 RID: 4053
		[CompilerGenerated]
		private string a;

		// Token: 0x04000FD6 RID: 4054
		[CompilerGenerated]
		private bool b;
	}
}
