using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005AF RID: 1455
	[Event("Debugger.paused")]
	[SupportedBy("Chrome")]
	public class PausedEvent
	{
		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0000C577 File Offset: 0x0000A777
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x0000C57F File Offset: 0x0000A77F
		public CallFrame[] CallFrames { get; set; }

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0000C588 File Offset: 0x0000A788
		// (set) Token: 0x06001B91 RID: 7057 RVA: 0x0000C590 File Offset: 0x0000A790
		public string Reason { get; set; }

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x0000C599 File Offset: 0x0000A799
		// (set) Token: 0x06001B93 RID: 7059 RVA: 0x0000C5A1 File Offset: 0x0000A7A1
		public object Data { get; set; }

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0000C5AA File Offset: 0x0000A7AA
		// (set) Token: 0x06001B95 RID: 7061 RVA: 0x0000C5B2 File Offset: 0x0000A7B2
		public string[] HitBreakpoints { get; set; }

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x0000C5BB File Offset: 0x0000A7BB
		// (set) Token: 0x06001B97 RID: 7063 RVA: 0x0000C5C3 File Offset: 0x0000A7C3
		public StackTrace AsyncStackTrace { get; set; }

		// Token: 0x04000F7F RID: 3967
		[CompilerGenerated]
		private CallFrame[] a;

		// Token: 0x04000F80 RID: 3968
		[CompilerGenerated]
		private string b;

		// Token: 0x04000F81 RID: 3969
		[CompilerGenerated]
		private object c;

		// Token: 0x04000F82 RID: 3970
		[CompilerGenerated]
		private string[] d;

		// Token: 0x04000F83 RID: 3971
		[CompilerGenerated]
		private StackTrace e;
	}
}
