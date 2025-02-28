using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005AC RID: 1452
	[SupportedBy("Chrome")]
	public class Location
	{
		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x0000C544 File Offset: 0x0000A744
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x0000C54C File Offset: 0x0000A74C
		public string ScriptId { get; set; }

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x0000C555 File Offset: 0x0000A755
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x0000C55D File Offset: 0x0000A75D
		public long LineNumber { get; set; }

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x0000C566 File Offset: 0x0000A766
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x0000C56E File Offset: 0x0000A76E
		public long ColumnNumber { get; set; }

		// Token: 0x04000F7C RID: 3964
		[CompilerGenerated]
		private string a;

		// Token: 0x04000F7D RID: 3965
		[CompilerGenerated]
		private long b;

		// Token: 0x04000F7E RID: 3966
		[CompilerGenerated]
		private long c;
	}
}
