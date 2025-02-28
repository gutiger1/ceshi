using System;
using System.Runtime.CompilerServices;
using Agiso.ChromeDevTools.Protocol.Chrome.Runtime;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Console
{
	// Token: 0x02000623 RID: 1571
	[SupportedBy("Chrome")]
	public class ConsoleMessage
	{
		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x0000D3F1 File Offset: 0x0000B5F1
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x0000D3F9 File Offset: 0x0000B5F9
		public string Source { get; set; }

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x0000D402 File Offset: 0x0000B602
		// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x0000D40A File Offset: 0x0000B60A
		public string Level { get; set; }

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x0000D413 File Offset: 0x0000B613
		// (set) Token: 0x06001DBA RID: 7610 RVA: 0x0000D41B File Offset: 0x0000B61B
		public string Text { get; set; }

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x0000D424 File Offset: 0x0000B624
		// (set) Token: 0x06001DBC RID: 7612 RVA: 0x0000D42C File Offset: 0x0000B62C
		public string Type { get; set; }

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x0000D435 File Offset: 0x0000B635
		// (set) Token: 0x06001DBE RID: 7614 RVA: 0x0000D43D File Offset: 0x0000B63D
		public string ScriptId { get; set; }

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x0000D446 File Offset: 0x0000B646
		// (set) Token: 0x06001DC0 RID: 7616 RVA: 0x0000D44E File Offset: 0x0000B64E
		public string Url { get; set; }

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x0000D457 File Offset: 0x0000B657
		// (set) Token: 0x06001DC2 RID: 7618 RVA: 0x0000D45F File Offset: 0x0000B65F
		public long Line { get; set; }

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x0000D468 File Offset: 0x0000B668
		// (set) Token: 0x06001DC4 RID: 7620 RVA: 0x0000D470 File Offset: 0x0000B670
		public long Column { get; set; }

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x0000D479 File Offset: 0x0000B679
		// (set) Token: 0x06001DC6 RID: 7622 RVA: 0x0000D481 File Offset: 0x0000B681
		public long RepeatCount { get; set; }

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x0000D48A File Offset: 0x0000B68A
		// (set) Token: 0x06001DC8 RID: 7624 RVA: 0x0000D492 File Offset: 0x0000B692
		public RemoteObject[] Parameters { get; set; }

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x0000D49B File Offset: 0x0000B69B
		// (set) Token: 0x06001DCA RID: 7626 RVA: 0x0000D4A3 File Offset: 0x0000B6A3
		public CallFrame[] StackTrace { get; set; }

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001DCB RID: 7627 RVA: 0x0000D4AC File Offset: 0x0000B6AC
		// (set) Token: 0x06001DCC RID: 7628 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		public AsyncStackTrace AsyncStackTrace { get; set; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x0000D4BD File Offset: 0x0000B6BD
		// (set) Token: 0x06001DCE RID: 7630 RVA: 0x0000D4C5 File Offset: 0x0000B6C5
		public string NetworkRequestId { get; set; }

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06001DCF RID: 7631 RVA: 0x0000D4CE File Offset: 0x0000B6CE
		// (set) Token: 0x06001DD0 RID: 7632 RVA: 0x0000D4D6 File Offset: 0x0000B6D6
		public double Timestamp { get; set; }

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x0000D4DF File Offset: 0x0000B6DF
		// (set) Token: 0x06001DD2 RID: 7634 RVA: 0x0000D4E7 File Offset: 0x0000B6E7
		public long ExecutionContextId { get; set; }

		// Token: 0x0400105E RID: 4190
		[CompilerGenerated]
		private string a;

		// Token: 0x0400105F RID: 4191
		[CompilerGenerated]
		private string b;

		// Token: 0x04001060 RID: 4192
		[CompilerGenerated]
		private string c;

		// Token: 0x04001061 RID: 4193
		[CompilerGenerated]
		private string d;

		// Token: 0x04001062 RID: 4194
		[CompilerGenerated]
		private string e;

		// Token: 0x04001063 RID: 4195
		[CompilerGenerated]
		private string f;

		// Token: 0x04001064 RID: 4196
		[CompilerGenerated]
		private long g;

		// Token: 0x04001065 RID: 4197
		[CompilerGenerated]
		private long h;

		// Token: 0x04001066 RID: 4198
		[CompilerGenerated]
		private long i;

		// Token: 0x04001067 RID: 4199
		[CompilerGenerated]
		private RemoteObject[] j;

		// Token: 0x04001068 RID: 4200
		[CompilerGenerated]
		private CallFrame[] k;

		// Token: 0x04001069 RID: 4201
		[CompilerGenerated]
		private AsyncStackTrace l;

		// Token: 0x0400106A RID: 4202
		[CompilerGenerated]
		private string m;

		// Token: 0x0400106B RID: 4203
		[CompilerGenerated]
		private double n;

		// Token: 0x0400106C RID: 4204
		[CompilerGenerated]
		private long o;
	}
}
