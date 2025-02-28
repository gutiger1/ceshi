using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005AA RID: 1450
	[CommandResponse("Debugger.getStepInPositions")]
	[SupportedBy("Chrome")]
	public class GetStepInPositionsCommandResponse
	{
		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x0000C533 File Offset: 0x0000A733
		// (set) Token: 0x06001B82 RID: 7042 RVA: 0x0000C53B File Offset: 0x0000A73B
		public Location[] StepInPositions { get; set; }

		// Token: 0x04000F7B RID: 3963
		[CompilerGenerated]
		private Location[] a;
	}
}
