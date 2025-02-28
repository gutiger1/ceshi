using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005A2 RID: 1442
	[CommandResponse("Debugger.getGeneratorObjectDetails")]
	[SupportedBy("Chrome")]
	public class GetGeneratorObjectDetailsCommandResponse
	{
		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x0000C4AB File Offset: 0x0000A6AB
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x0000C4B3 File Offset: 0x0000A6B3
		public GeneratorObjectDetails Details { get; set; }

		// Token: 0x04000F73 RID: 3955
		[CompilerGenerated]
		private GeneratorObjectDetails a;
	}
}
