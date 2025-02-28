using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x020005FF RID: 1535
	[CommandResponse("CSS.getInlineStylesForNode")]
	[SupportedBy("Chrome")]
	public class GetInlineStylesForNodeCommandResponse
	{
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0000CFB1 File Offset: 0x0000B1B1
		// (set) Token: 0x06001D13 RID: 7443 RVA: 0x0000CFB9 File Offset: 0x0000B1B9
		public CSSStyle InlineStyle { get; set; }

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0000CFC2 File Offset: 0x0000B1C2
		// (set) Token: 0x06001D15 RID: 7445 RVA: 0x0000CFCA File Offset: 0x0000B1CA
		public CSSStyle AttributesStyle { get; set; }

		// Token: 0x04001019 RID: 4121
		[CompilerGenerated]
		private CSSStyle a;

		// Token: 0x0400101A RID: 4122
		[CompilerGenerated]
		private CSSStyle b;
	}
}
