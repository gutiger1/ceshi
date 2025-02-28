using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Database
{
	// Token: 0x020005EA RID: 1514
	[Command("Database.getDatabaseTableNames")]
	[SupportedBy("Chrome")]
	public class GetDatabaseTableNamesCommand
	{
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x0000CC4E File Offset: 0x0000AE4E
		// (set) Token: 0x06001C98 RID: 7320 RVA: 0x0000CC56 File Offset: 0x0000AE56
		public string DatabaseId { get; set; }

		// Token: 0x04000FE6 RID: 4070
		[CompilerGenerated]
		private string a;
	}
}
