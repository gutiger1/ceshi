using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Accessibility
{
	// Token: 0x0200067C RID: 1660
	[SupportedBy("Chrome")]
	[CommandResponse("Accessibility.getAXNode")]
	public class GetAXNodeCommandResponse
	{
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x0000DF2A File Offset: 0x0000C12A
		// (set) Token: 0x06001F5A RID: 8026 RVA: 0x0000DF32 File Offset: 0x0000C132
		public AXNode AccessibilityNode { get; set; }

		// Token: 0x0400113C RID: 4412
		[CompilerGenerated]
		private AXNode a;
	}
}
