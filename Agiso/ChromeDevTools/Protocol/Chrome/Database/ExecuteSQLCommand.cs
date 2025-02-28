using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Database
{
	// Token: 0x020005E8 RID: 1512
	[Command("Database.executeSQL")]
	[SupportedBy("Chrome")]
	public class ExecuteSQLCommand
	{
		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x0000CBF9 File Offset: 0x0000ADF9
		// (set) Token: 0x06001C8C RID: 7308 RVA: 0x0000CC01 File Offset: 0x0000AE01
		public string DatabaseId { get; set; }

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x0000CC0A File Offset: 0x0000AE0A
		// (set) Token: 0x06001C8E RID: 7310 RVA: 0x0000CC12 File Offset: 0x0000AE12
		public string Query { get; set; }

		// Token: 0x04000FE1 RID: 4065
		[CompilerGenerated]
		private string a;

		// Token: 0x04000FE2 RID: 4066
		[CompilerGenerated]
		private string b;
	}
}
