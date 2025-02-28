using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.ApplicationCache
{
	// Token: 0x02000659 RID: 1625
	[SupportedBy("Chrome")]
	[CommandResponse("ApplicationCache.getFramesWithManifests")]
	public class GetFramesWithManifestsCommandResponse
	{
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x0000DAFB File Offset: 0x0000BCFB
		// (set) Token: 0x06001EC0 RID: 7872 RVA: 0x0000DB03 File Offset: 0x0000BD03
		public FrameWithManifest[] FrameIds { get; set; }

		// Token: 0x040010C8 RID: 4296
		[CompilerGenerated]
		private FrameWithManifest[] a;
	}
}
