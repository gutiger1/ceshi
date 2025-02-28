using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Console
{
	// Token: 0x02000629 RID: 1577
	[Event("Console.messageRepeatCountUpdated")]
	[SupportedBy("Chrome")]
	public class MessageRepeatCountUpdatedEvent
	{
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x0000D501 File Offset: 0x0000B701
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x0000D509 File Offset: 0x0000B709
		public long Count { get; set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x0000D512 File Offset: 0x0000B712
		// (set) Token: 0x06001DDE RID: 7646 RVA: 0x0000D51A File Offset: 0x0000B71A
		public double Timestamp { get; set; }

		// Token: 0x0400106E RID: 4206
		[CompilerGenerated]
		private long a;

		// Token: 0x0400106F RID: 4207
		[CompilerGenerated]
		private double b;
	}
}
