using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x020005EE RID: 1518
	[SupportedBy("Chrome")]
	[Command("CSS.createStyleSheet")]
	public class CreateStyleSheetCommand
	{
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		// (set) Token: 0x06001CA8 RID: 7336 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		public string FrameId { get; set; }

		// Token: 0x04000FEC RID: 4076
		[CompilerGenerated]
		private string a;
	}
}
