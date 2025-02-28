using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x020005ED RID: 1517
	[SupportedBy("Chrome")]
	[CommandResponse("CSS.addRule")]
	public class AddRuleCommandResponse
	{
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0000CCA3 File Offset: 0x0000AEA3
		// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x0000CCAB File Offset: 0x0000AEAB
		public CSSRule Rule { get; set; }

		// Token: 0x04000FEB RID: 4075
		[CompilerGenerated]
		private CSSRule a;
	}
}
