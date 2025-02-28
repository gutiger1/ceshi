using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x0200059D RID: 1437
	[Command("Debugger.getCollectionEntries")]
	[SupportedBy("Chrome")]
	public class GetCollectionEntriesCommand
	{
		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0000C456 File Offset: 0x0000A656
		// (set) Token: 0x06001B5B RID: 7003 RVA: 0x0000C45E File Offset: 0x0000A65E
		public string ObjectId { get; set; }

		// Token: 0x04000F6E RID: 3950
		[CompilerGenerated]
		private string a;
	}
}
