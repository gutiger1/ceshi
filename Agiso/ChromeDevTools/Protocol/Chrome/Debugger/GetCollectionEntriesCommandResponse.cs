using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x0200059E RID: 1438
	[CommandResponse("Debugger.getCollectionEntries")]
	[SupportedBy("Chrome")]
	public class GetCollectionEntriesCommandResponse
	{
		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x0000C467 File Offset: 0x0000A667
		// (set) Token: 0x06001B5E RID: 7006 RVA: 0x0000C46F File Offset: 0x0000A66F
		public CollectionEntry[] Entries { get; set; }

		// Token: 0x04000F6F RID: 3951
		[CompilerGenerated]
		private CollectionEntry[] a;
	}
}
