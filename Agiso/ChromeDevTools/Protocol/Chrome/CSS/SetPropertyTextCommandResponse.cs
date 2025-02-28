using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000614 RID: 1556
	[CommandResponse("CSS.setPropertyText")]
	[SupportedBy("Chrome")]
	public class SetPropertyTextCommandResponse
	{
		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x0000D259 File Offset: 0x0000B459
		// (set) Token: 0x06001D78 RID: 7544 RVA: 0x0000D261 File Offset: 0x0000B461
		public CSSStyle Style { get; set; }

		// Token: 0x04001041 RID: 4161
		[CompilerGenerated]
		private CSSStyle a;
	}
}
