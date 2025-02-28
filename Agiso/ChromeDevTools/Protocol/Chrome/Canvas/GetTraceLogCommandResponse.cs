using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x0200063B RID: 1595
	[SupportedBy("Chrome")]
	[CommandResponse("Canvas.getTraceLog")]
	public class GetTraceLogCommandResponse
	{
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x0000D754 File Offset: 0x0000B954
		// (set) Token: 0x06001E34 RID: 7732 RVA: 0x0000D75C File Offset: 0x0000B95C
		public TraceLog TraceLog { get; set; }

		// Token: 0x04001091 RID: 4241
		[CompilerGenerated]
		private TraceLog a;
	}
}
