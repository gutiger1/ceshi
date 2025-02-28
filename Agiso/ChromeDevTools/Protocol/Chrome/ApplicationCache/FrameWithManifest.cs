using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.ApplicationCache
{
	// Token: 0x02000655 RID: 1621
	[SupportedBy("Chrome")]
	public class FrameWithManifest
	{
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x0000DAA6 File Offset: 0x0000BCA6
		// (set) Token: 0x06001EB2 RID: 7858 RVA: 0x0000DAAE File Offset: 0x0000BCAE
		public string FrameId { get; set; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x0000DAB7 File Offset: 0x0000BCB7
		// (set) Token: 0x06001EB4 RID: 7860 RVA: 0x0000DABF File Offset: 0x0000BCBF
		public string ManifestURL { get; set; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
		// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x0000DAD0 File Offset: 0x0000BCD0
		public long Status { get; set; }

		// Token: 0x040010C3 RID: 4291
		[CompilerGenerated]
		private string a;

		// Token: 0x040010C4 RID: 4292
		[CompilerGenerated]
		private string b;

		// Token: 0x040010C5 RID: 4293
		[CompilerGenerated]
		private long c;
	}
}
