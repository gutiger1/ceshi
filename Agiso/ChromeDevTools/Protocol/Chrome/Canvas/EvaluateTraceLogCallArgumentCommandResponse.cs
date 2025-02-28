using System;
using System.Runtime.CompilerServices;
using Agiso.ChromeDevTools.Protocol.Chrome.Runtime;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x02000637 RID: 1591
	[SupportedBy("Chrome")]
	[CommandResponse("Canvas.evaluateTraceLogCallArgument")]
	public class EvaluateTraceLogCallArgumentCommandResponse
	{
		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x0000D6CC File Offset: 0x0000B8CC
		// (set) Token: 0x06001E20 RID: 7712 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		public RemoteObject Result { get; set; }

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x0000D6DD File Offset: 0x0000B8DD
		// (set) Token: 0x06001E22 RID: 7714 RVA: 0x0000D6E5 File Offset: 0x0000B8E5
		public ResourceState ResourceState { get; set; }

		// Token: 0x04001089 RID: 4233
		[CompilerGenerated]
		private RemoteObject a;

		// Token: 0x0400108A RID: 4234
		[CompilerGenerated]
		private ResourceState b;
	}
}
