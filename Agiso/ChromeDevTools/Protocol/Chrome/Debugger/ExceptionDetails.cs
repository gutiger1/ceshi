using System;
using System.Runtime.CompilerServices;
using Agiso.ChromeDevTools.Protocol.Chrome.Console;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000596 RID: 1430
	[SupportedBy("Chrome")]
	public class ExceptionDetails
	{
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x0000C346 File Offset: 0x0000A546
		// (set) Token: 0x06001B34 RID: 6964 RVA: 0x0000C34E File Offset: 0x0000A54E
		public string Text { get; set; }

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0000C357 File Offset: 0x0000A557
		// (set) Token: 0x06001B36 RID: 6966 RVA: 0x0000C35F File Offset: 0x0000A55F
		public string Url { get; set; }

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x0000C368 File Offset: 0x0000A568
		// (set) Token: 0x06001B38 RID: 6968 RVA: 0x0000C370 File Offset: 0x0000A570
		public string ScriptId { get; set; }

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x0000C379 File Offset: 0x0000A579
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x0000C381 File Offset: 0x0000A581
		public long Line { get; set; }

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0000C38A File Offset: 0x0000A58A
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x0000C392 File Offset: 0x0000A592
		public long Column { get; set; }

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0000C39B File Offset: 0x0000A59B
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x0000C3A3 File Offset: 0x0000A5A3
		public CallFrame[] StackTrace { get; set; }

		// Token: 0x04000F5E RID: 3934
		[CompilerGenerated]
		private string a;

		// Token: 0x04000F5F RID: 3935
		[CompilerGenerated]
		private string b;

		// Token: 0x04000F60 RID: 3936
		[CompilerGenerated]
		private string c;

		// Token: 0x04000F61 RID: 3937
		[CompilerGenerated]
		private long d;

		// Token: 0x04000F62 RID: 3938
		[CompilerGenerated]
		private long e;

		// Token: 0x04000F63 RID: 3939
		[CompilerGenerated]
		private CallFrame[] f;
	}
}
