using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x0200060F RID: 1551
	[SupportedBy("Chrome")]
	public class Selector
	{
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0000D19E File Offset: 0x0000B39E
		// (set) Token: 0x06001D5D RID: 7517 RVA: 0x0000D1A6 File Offset: 0x0000B3A6
		public string Value { get; set; }

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x0000D1AF File Offset: 0x0000B3AF
		// (set) Token: 0x06001D5F RID: 7519 RVA: 0x0000D1B7 File Offset: 0x0000B3B7
		public SourceRange Range { get; set; }

		// Token: 0x04001036 RID: 4150
		[CompilerGenerated]
		private string a;

		// Token: 0x04001037 RID: 4151
		[CompilerGenerated]
		private SourceRange b;
	}
}
