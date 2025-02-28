using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Console
{
	// Token: 0x02000628 RID: 1576
	[SupportedBy("Chrome")]
	[Event("Console.messageAdded")]
	public class MessageAddedEvent
	{
		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x0000D4F0 File Offset: 0x0000B6F0
		// (set) Token: 0x06001DD9 RID: 7641 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		public ConsoleMessage Message { get; set; }

		// Token: 0x0400106D RID: 4205
		[CompilerGenerated]
		private ConsoleMessage a;
	}
}
