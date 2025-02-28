using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x02000609 RID: 1545
	[SupportedBy("Chrome")]
	public class MediaQuery
	{
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0000D0C1 File Offset: 0x0000B2C1
		// (set) Token: 0x06001D3D RID: 7485 RVA: 0x0000D0C9 File Offset: 0x0000B2C9
		public MediaQueryExpression[] Expressions { get; set; }

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x0000D0D2 File Offset: 0x0000B2D2
		// (set) Token: 0x06001D3F RID: 7487 RVA: 0x0000D0DA File Offset: 0x0000B2DA
		public bool Active { get; set; }

		// Token: 0x04001029 RID: 4137
		[CompilerGenerated]
		private MediaQueryExpression[] a;

		// Token: 0x0400102A RID: 4138
		[CompilerGenerated]
		private bool b;
	}
}
