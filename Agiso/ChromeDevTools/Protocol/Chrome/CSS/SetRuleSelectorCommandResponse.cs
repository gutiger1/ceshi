using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000616 RID: 1558
	[CommandResponse("CSS.setRuleSelector")]
	[SupportedBy("Chrome")]
	public class SetRuleSelectorCommandResponse
	{
		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x0000D29D File Offset: 0x0000B49D
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x0000D2A5 File Offset: 0x0000B4A5
		public CSSRule Rule { get; set; }

		// Token: 0x04001045 RID: 4165
		[CompilerGenerated]
		private CSSRule a;
	}
}
