using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.ApplicationCache
{
	// Token: 0x02000652 RID: 1618
	[SupportedBy("Chrome")]
	[Event("ApplicationCache.applicationCacheStatusUpdated")]
	public class ApplicationCacheStatusUpdatedEvent
	{
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0000DA73 File Offset: 0x0000BC73
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x0000DA7B File Offset: 0x0000BC7B
		public string FrameId { get; set; }

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x0000DA84 File Offset: 0x0000BC84
		// (set) Token: 0x06001EAB RID: 7851 RVA: 0x0000DA8C File Offset: 0x0000BC8C
		public string ManifestURL { get; set; }

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x0000DA95 File Offset: 0x0000BC95
		// (set) Token: 0x06001EAD RID: 7853 RVA: 0x0000DA9D File Offset: 0x0000BC9D
		public long Status { get; set; }

		// Token: 0x040010C0 RID: 4288
		[CompilerGenerated]
		private string a;

		// Token: 0x040010C1 RID: 4289
		[CompilerGenerated]
		private string b;

		// Token: 0x040010C2 RID: 4290
		[CompilerGenerated]
		private long c;
	}
}
