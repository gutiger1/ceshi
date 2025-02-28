using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.ApplicationCache
{
	// Token: 0x0200065B RID: 1627
	[SupportedBy("Chrome")]
	[CommandResponse("ApplicationCache.getManifestForFrame")]
	public class GetManifestForFrameCommandResponse
	{
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x0000DB1D File Offset: 0x0000BD1D
		// (set) Token: 0x06001EC6 RID: 7878 RVA: 0x0000DB25 File Offset: 0x0000BD25
		public string ManifestURL { get; set; }

		// Token: 0x040010CA RID: 4298
		[CompilerGenerated]
		private string a;
	}
}
