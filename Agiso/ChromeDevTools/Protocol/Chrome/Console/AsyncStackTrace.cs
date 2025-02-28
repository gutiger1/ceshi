using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Console
{
	// Token: 0x0200061F RID: 1567
	[SupportedBy("Chrome")]
	public class AsyncStackTrace
	{
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x0000D369 File Offset: 0x0000B569
		// (set) Token: 0x06001DA2 RID: 7586 RVA: 0x0000D371 File Offset: 0x0000B571
		public CallFrame[] CallFrames { get; set; }

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x0000D37A File Offset: 0x0000B57A
		// (set) Token: 0x06001DA4 RID: 7588 RVA: 0x0000D382 File Offset: 0x0000B582
		public string Description { get; set; }

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x0000D38B File Offset: 0x0000B58B
		// (set) Token: 0x06001DA6 RID: 7590 RVA: 0x0000D393 File Offset: 0x0000B593
		[JsonProperty("asyncStackTrace")]
		public AsyncStackTrace AsyncStackTraceChild { get; set; }

		// Token: 0x04001056 RID: 4182
		[CompilerGenerated]
		private CallFrame[] a;

		// Token: 0x04001057 RID: 4183
		[CompilerGenerated]
		private string b;

		// Token: 0x04001058 RID: 4184
		[CompilerGenerated]
		private AsyncStackTrace c;
	}
}
