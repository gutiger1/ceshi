using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x020005EF RID: 1519
	[SupportedBy("Chrome")]
	[CommandResponse("CSS.createStyleSheet")]
	public class CreateStyleSheetCommandResponse
	{
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x0000CCC5 File Offset: 0x0000AEC5
		// (set) Token: 0x06001CAB RID: 7339 RVA: 0x0000CCCD File Offset: 0x0000AECD
		public string StyleSheetId { get; set; }

		// Token: 0x04000FED RID: 4077
		[CompilerGenerated]
		private string a;
	}
}
