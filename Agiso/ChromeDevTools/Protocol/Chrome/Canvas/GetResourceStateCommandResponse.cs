using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x02000639 RID: 1593
	[CommandResponse("Canvas.getResourceState")]
	[SupportedBy("Chrome")]
	public class GetResourceStateCommandResponse
	{
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x0000D710 File Offset: 0x0000B910
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x0000D718 File Offset: 0x0000B918
		public ResourceState ResourceState { get; set; }

		// Token: 0x0400108D RID: 4237
		[CompilerGenerated]
		private ResourceState a;
	}
}
