using System;
using System.Runtime.CompilerServices;
using Agiso.ChromeDevTools.Protocol.Chrome.Runtime;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000584 RID: 1412
	[SupportedBy("Chrome")]
	public class CallFrame
	{
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x0000C159 File Offset: 0x0000A359
		// (set) Token: 0x06001AE8 RID: 6888 RVA: 0x0000C161 File Offset: 0x0000A361
		public string CallFrameId { get; set; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x0000C16A File Offset: 0x0000A36A
		// (set) Token: 0x06001AEA RID: 6890 RVA: 0x0000C172 File Offset: 0x0000A372
		public string FunctionName { get; set; }

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x0000C17B File Offset: 0x0000A37B
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x0000C183 File Offset: 0x0000A383
		public Location FunctionLocation { get; set; }

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x0000C18C File Offset: 0x0000A38C
		// (set) Token: 0x06001AEE RID: 6894 RVA: 0x0000C194 File Offset: 0x0000A394
		public Location Location { get; set; }

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x0000C19D File Offset: 0x0000A39D
		// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x0000C1A5 File Offset: 0x0000A3A5
		public Scope[] ScopeChain { get; set; }

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x0000C1AE File Offset: 0x0000A3AE
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x0000C1B6 File Offset: 0x0000A3B6
		public RemoteObject This { get; set; }

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0000C1BF File Offset: 0x0000A3BF
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x0000C1C7 File Offset: 0x0000A3C7
		public RemoteObject ReturnValue { get; set; }

		// Token: 0x04000F41 RID: 3905
		[CompilerGenerated]
		private string a;

		// Token: 0x04000F42 RID: 3906
		[CompilerGenerated]
		private string b;

		// Token: 0x04000F43 RID: 3907
		[CompilerGenerated]
		private Location c;

		// Token: 0x04000F44 RID: 3908
		[CompilerGenerated]
		private Location d;

		// Token: 0x04000F45 RID: 3909
		[CompilerGenerated]
		private Scope[] e;

		// Token: 0x04000F46 RID: 3910
		[CompilerGenerated]
		private RemoteObject f;

		// Token: 0x04000F47 RID: 3911
		[CompilerGenerated]
		private RemoteObject g;
	}
}
