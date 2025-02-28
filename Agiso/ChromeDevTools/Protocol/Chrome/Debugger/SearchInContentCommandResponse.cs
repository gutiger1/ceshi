using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005C1 RID: 1473
	[CommandResponse("Debugger.searchInContent")]
	[SupportedBy("Chrome")]
	public class SearchInContentCommandResponse
	{
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x0000C91E File Offset: 0x0000AB1E
		// (set) Token: 0x06001C0F RID: 7183 RVA: 0x0000C926 File Offset: 0x0000AB26
		public SearchMatch[] Result { get; set; }

		// Token: 0x04000FB6 RID: 4022
		[CompilerGenerated]
		private SearchMatch[] a;
	}
}
