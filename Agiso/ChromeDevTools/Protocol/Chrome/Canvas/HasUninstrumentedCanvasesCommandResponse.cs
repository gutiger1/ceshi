using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Canvas
{
	// Token: 0x0200063D RID: 1597
	[SupportedBy("Chrome")]
	[CommandResponse("Canvas.hasUninstrumentedCanvases")]
	public class HasUninstrumentedCanvasesCommandResponse
	{
		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x0000D765 File Offset: 0x0000B965
		// (set) Token: 0x06001E38 RID: 7736 RVA: 0x0000D76D File Offset: 0x0000B96D
		public bool Result { get; set; }

		// Token: 0x04001092 RID: 4242
		[CompilerGenerated]
		private bool a;
	}
}
