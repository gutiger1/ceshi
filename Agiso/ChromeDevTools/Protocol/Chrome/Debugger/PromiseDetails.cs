using System;
using System.Runtime.CompilerServices;
using Agiso.ChromeDevTools.Protocol.Chrome.Console;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005B0 RID: 1456
	[SupportedBy("Chrome")]
	public class PromiseDetails
	{
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x0000C5CC File Offset: 0x0000A7CC
		// (set) Token: 0x06001B9A RID: 7066 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
		public long Id { get; set; }

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x0000C5DD File Offset: 0x0000A7DD
		// (set) Token: 0x06001B9C RID: 7068 RVA: 0x0000C5E5 File Offset: 0x0000A7E5
		public string Status { get; set; }

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x0000C5EE File Offset: 0x0000A7EE
		// (set) Token: 0x06001B9E RID: 7070 RVA: 0x0000C5F6 File Offset: 0x0000A7F6
		public long ParentId { get; set; }

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x0000C5FF File Offset: 0x0000A7FF
		// (set) Token: 0x06001BA0 RID: 7072 RVA: 0x0000C607 File Offset: 0x0000A807
		public CallFrame CallFrame { get; set; }

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0000C610 File Offset: 0x0000A810
		// (set) Token: 0x06001BA2 RID: 7074 RVA: 0x0000C618 File Offset: 0x0000A818
		public double CreationTime { get; set; }

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x0000C621 File Offset: 0x0000A821
		// (set) Token: 0x06001BA4 RID: 7076 RVA: 0x0000C629 File Offset: 0x0000A829
		public double SettlementTime { get; set; }

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x0000C632 File Offset: 0x0000A832
		// (set) Token: 0x06001BA6 RID: 7078 RVA: 0x0000C63A File Offset: 0x0000A83A
		public CallFrame[] CreationStack { get; set; }

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x0000C643 File Offset: 0x0000A843
		// (set) Token: 0x06001BA8 RID: 7080 RVA: 0x0000C64B File Offset: 0x0000A84B
		public AsyncStackTrace AsyncCreationStack { get; set; }

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x0000C654 File Offset: 0x0000A854
		// (set) Token: 0x06001BAA RID: 7082 RVA: 0x0000C65C File Offset: 0x0000A85C
		public CallFrame[] SettlementStack { get; set; }

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x0000C665 File Offset: 0x0000A865
		// (set) Token: 0x06001BAC RID: 7084 RVA: 0x0000C66D File Offset: 0x0000A86D
		public AsyncStackTrace AsyncSettlementStack { get; set; }

		// Token: 0x04000F84 RID: 3972
		[CompilerGenerated]
		private long a;

		// Token: 0x04000F85 RID: 3973
		[CompilerGenerated]
		private string b;

		// Token: 0x04000F86 RID: 3974
		[CompilerGenerated]
		private long c;

		// Token: 0x04000F87 RID: 3975
		[CompilerGenerated]
		private CallFrame d;

		// Token: 0x04000F88 RID: 3976
		[CompilerGenerated]
		private double e;

		// Token: 0x04000F89 RID: 3977
		[CompilerGenerated]
		private double f;

		// Token: 0x04000F8A RID: 3978
		[CompilerGenerated]
		private CallFrame[] g;

		// Token: 0x04000F8B RID: 3979
		[CompilerGenerated]
		private AsyncStackTrace h;

		// Token: 0x04000F8C RID: 3980
		[CompilerGenerated]
		private CallFrame[] i;

		// Token: 0x04000F8D RID: 3981
		[CompilerGenerated]
		private AsyncStackTrace j;
	}
}
