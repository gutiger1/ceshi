using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Console
{
	// Token: 0x02000620 RID: 1568
	[SupportedBy("Chrome")]
	public class CallFrame
	{
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x0000D39C File Offset: 0x0000B59C
		// (set) Token: 0x06001DA9 RID: 7593 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		public string FunctionName { get; set; }

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x0000D3AD File Offset: 0x0000B5AD
		// (set) Token: 0x06001DAB RID: 7595 RVA: 0x0000D3B5 File Offset: 0x0000B5B5
		public string ScriptId { get; set; }

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0000D3BE File Offset: 0x0000B5BE
		// (set) Token: 0x06001DAD RID: 7597 RVA: 0x0000D3C6 File Offset: 0x0000B5C6
		public string Url { get; set; }

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x0000D3CF File Offset: 0x0000B5CF
		// (set) Token: 0x06001DAF RID: 7599 RVA: 0x0000D3D7 File Offset: 0x0000B5D7
		public long LineNumber { get; set; }

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
		// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x0000D3E8 File Offset: 0x0000B5E8
		public long ColumnNumber { get; set; }

		// Token: 0x04001059 RID: 4185
		[CompilerGenerated]
		private string a;

		// Token: 0x0400105A RID: 4186
		[CompilerGenerated]
		private string b;

		// Token: 0x0400105B RID: 4187
		[CompilerGenerated]
		private string c;

		// Token: 0x0400105C RID: 4188
		[CompilerGenerated]
		private long d;

		// Token: 0x0400105D RID: 4189
		[CompilerGenerated]
		private long e;
	}
}
