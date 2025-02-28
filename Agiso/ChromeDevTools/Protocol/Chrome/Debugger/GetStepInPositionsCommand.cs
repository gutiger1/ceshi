using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005A9 RID: 1449
	[SupportedBy("Chrome")]
	[Command("Debugger.getStepInPositions")]
	public class GetStepInPositionsCommand
	{
		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x0000C522 File Offset: 0x0000A722
		// (set) Token: 0x06001B7F RID: 7039 RVA: 0x0000C52A File Offset: 0x0000A72A
		public string CallFrameId { get; set; }

		// Token: 0x04000F7A RID: 3962
		[CompilerGenerated]
		private string a;
	}
}
