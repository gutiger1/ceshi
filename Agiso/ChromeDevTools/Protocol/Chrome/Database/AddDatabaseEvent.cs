using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Database
{
	// Token: 0x020005E1 RID: 1505
	[Event("Database.addDatabase")]
	[SupportedBy("Chrome")]
	public class AddDatabaseEvent
	{
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x0000CB82 File Offset: 0x0000AD82
		// (set) Token: 0x06001C77 RID: 7287 RVA: 0x0000CB8A File Offset: 0x0000AD8A
		public Database Database { get; set; }

		// Token: 0x04000FDA RID: 4058
		[CompilerGenerated]
		private Database a;
	}
}
