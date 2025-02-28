using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000603 RID: 1539
	[SupportedBy("Chrome")]
	[CommandResponse("CSS.getMediaQueries")]
	public class GetMediaQueriesCommandResponse
	{
		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x0000D039 File Offset: 0x0000B239
		// (set) Token: 0x06001D27 RID: 7463 RVA: 0x0000D041 File Offset: 0x0000B241
		public CSSMedia[] Medias { get; set; }

		// Token: 0x04001021 RID: 4129
		[CompilerGenerated]
		private CSSMedia[] a;
	}
}
