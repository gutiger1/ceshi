using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000610 RID: 1552
	[SupportedBy("Chrome")]
	public class SelectorList
	{
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		// (set) Token: 0x06001D62 RID: 7522 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		public Selector[] Selectors { get; set; }

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x0000D1D1 File Offset: 0x0000B3D1
		// (set) Token: 0x06001D64 RID: 7524 RVA: 0x0000D1D9 File Offset: 0x0000B3D9
		public string Text { get; set; }

		// Token: 0x04001038 RID: 4152
		[CompilerGenerated]
		private Selector[] a;

		// Token: 0x04001039 RID: 4153
		[CompilerGenerated]
		private string b;
	}
}
