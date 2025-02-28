using System;
using System.Runtime.CompilerServices;
using Agiso.ChromeDevTools.Protocol.Chrome.Console;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000580 RID: 1408
	[SupportedBy("Chrome")]
	public class AsyncOperation
	{
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x0000C0D1 File Offset: 0x0000A2D1
		// (set) Token: 0x06001AD4 RID: 6868 RVA: 0x0000C0D9 File Offset: 0x0000A2D9
		public long Id { get; set; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x0000C0E2 File Offset: 0x0000A2E2
		// (set) Token: 0x06001AD6 RID: 6870 RVA: 0x0000C0EA File Offset: 0x0000A2EA
		public string Description { get; set; }

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x0000C0F3 File Offset: 0x0000A2F3
		// (set) Token: 0x06001AD8 RID: 6872 RVA: 0x0000C0FB File Offset: 0x0000A2FB
		public CallFrame[] StackTrace { get; set; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x0000C104 File Offset: 0x0000A304
		// (set) Token: 0x06001ADA RID: 6874 RVA: 0x0000C10C File Offset: 0x0000A30C
		public AsyncStackTrace AsyncStackTrace { get; set; }

		// Token: 0x04000F39 RID: 3897
		[CompilerGenerated]
		private long a;

		// Token: 0x04000F3A RID: 3898
		[CompilerGenerated]
		private string b;

		// Token: 0x04000F3B RID: 3899
		[CompilerGenerated]
		private CallFrame[] c;

		// Token: 0x04000F3C RID: 3900
		[CompilerGenerated]
		private AsyncStackTrace d;
	}
}
