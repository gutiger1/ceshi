using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005A6 RID: 1446
	[CommandResponse("Debugger.getPromises")]
	[SupportedBy("Chrome")]
	public class GetPromisesCommandResponse
	{
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0000C4EF File Offset: 0x0000A6EF
		// (set) Token: 0x06001B76 RID: 7030 RVA: 0x0000C4F7 File Offset: 0x0000A6F7
		public PromiseDetails[] Promises { get; set; }

		// Token: 0x04000F77 RID: 3959
		[CompilerGenerated]
		private PromiseDetails[] a;
	}
}
