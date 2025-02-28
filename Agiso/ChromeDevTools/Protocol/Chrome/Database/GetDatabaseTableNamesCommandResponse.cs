using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Database
{
	// Token: 0x020005EB RID: 1515
	[CommandResponse("Database.getDatabaseTableNames")]
	[SupportedBy("Chrome")]
	public class GetDatabaseTableNamesCommandResponse
	{
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x0000CC5F File Offset: 0x0000AE5F
		// (set) Token: 0x06001C9B RID: 7323 RVA: 0x0000CC67 File Offset: 0x0000AE67
		public string[] TableNames { get; set; }

		// Token: 0x04000FE7 RID: 4071
		[CompilerGenerated]
		private string[] a;
	}
}
