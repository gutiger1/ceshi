using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x0200059F RID: 1439
	[Command("Debugger.getFunctionDetails")]
	[SupportedBy("Chrome")]
	public class GetFunctionDetailsCommand
	{
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0000C478 File Offset: 0x0000A678
		// (set) Token: 0x06001B61 RID: 7009 RVA: 0x0000C480 File Offset: 0x0000A680
		public string FunctionId { get; set; }

		// Token: 0x04000F70 RID: 3952
		[CompilerGenerated]
		private string a;
	}
}
