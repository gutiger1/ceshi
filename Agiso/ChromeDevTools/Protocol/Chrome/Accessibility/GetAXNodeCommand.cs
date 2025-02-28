using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Accessibility
{
	// Token: 0x0200067B RID: 1659
	[SupportedBy("Chrome")]
	[Command("Accessibility.getAXNode")]
	public class GetAXNodeCommand
	{
		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x0000DF19 File Offset: 0x0000C119
		// (set) Token: 0x06001F57 RID: 8023 RVA: 0x0000DF21 File Offset: 0x0000C121
		public long NodeId { get; set; }

		// Token: 0x0400113B RID: 4411
		[CompilerGenerated]
		private long a;
	}
}
