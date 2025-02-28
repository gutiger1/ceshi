using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005A1 RID: 1441
	[SupportedBy("Chrome")]
	[Command("Debugger.getGeneratorObjectDetails")]
	public class GetGeneratorObjectDetailsCommand
	{
		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0000C49A File Offset: 0x0000A69A
		// (set) Token: 0x06001B67 RID: 7015 RVA: 0x0000C4A2 File Offset: 0x0000A6A2
		public string ObjectId { get; set; }

		// Token: 0x04000F72 RID: 3954
		[CompilerGenerated]
		private string a;
	}
}
