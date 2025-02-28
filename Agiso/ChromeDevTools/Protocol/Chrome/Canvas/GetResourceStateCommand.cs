using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x02000638 RID: 1592
	[Command("Canvas.getResourceState")]
	[SupportedBy("Chrome")]
	public class GetResourceStateCommand
	{
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x0000D6EE File Offset: 0x0000B8EE
		// (set) Token: 0x06001E25 RID: 7717 RVA: 0x0000D6F6 File Offset: 0x0000B8F6
		public string TraceLogId { get; set; }

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x0000D6FF File Offset: 0x0000B8FF
		// (set) Token: 0x06001E27 RID: 7719 RVA: 0x0000D707 File Offset: 0x0000B907
		public string ResourceId { get; set; }

		// Token: 0x0400108B RID: 4235
		[CompilerGenerated]
		private string a;

		// Token: 0x0400108C RID: 4236
		[CompilerGenerated]
		private string b;
	}
}
