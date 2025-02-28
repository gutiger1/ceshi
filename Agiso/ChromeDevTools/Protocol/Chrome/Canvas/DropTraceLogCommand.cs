using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x02000632 RID: 1586
	[SupportedBy("Chrome")]
	[Command("Canvas.dropTraceLog")]
	public class DropTraceLogCommand
	{
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x0000D677 File Offset: 0x0000B877
		// (set) Token: 0x06001E11 RID: 7697 RVA: 0x0000D67F File Offset: 0x0000B87F
		public string TraceLogId { get; set; }

		// Token: 0x04001084 RID: 4228
		[CompilerGenerated]
		private string a;
	}
}
