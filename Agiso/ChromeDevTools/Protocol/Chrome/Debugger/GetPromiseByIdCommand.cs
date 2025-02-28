using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005A3 RID: 1443
	[Command("Debugger.getPromiseById")]
	[SupportedBy("Chrome")]
	public class GetPromiseByIdCommand
	{
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		// (set) Token: 0x06001B6D RID: 7021 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		public long PromiseId { get; set; }

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0000C4CD File Offset: 0x0000A6CD
		// (set) Token: 0x06001B6F RID: 7023 RVA: 0x0000C4D5 File Offset: 0x0000A6D5
		public string ObjectGroup { get; set; }

		// Token: 0x04000F74 RID: 3956
		[CompilerGenerated]
		private long a;

		// Token: 0x04000F75 RID: 3957
		[CompilerGenerated]
		private string b;
	}
}
