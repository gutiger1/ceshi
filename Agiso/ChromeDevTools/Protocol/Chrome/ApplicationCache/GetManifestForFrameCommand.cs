using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.ApplicationCache
{
	// Token: 0x0200065A RID: 1626
	[Command("ApplicationCache.getManifestForFrame")]
	[SupportedBy("Chrome")]
	public class GetManifestForFrameCommand
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x0000DB0C File Offset: 0x0000BD0C
		// (set) Token: 0x06001EC3 RID: 7875 RVA: 0x0000DB14 File Offset: 0x0000BD14
		public string FrameId { get; set; }

		// Token: 0x040010C9 RID: 4297
		[CompilerGenerated]
		private string a;
	}
}
