using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Database
{
	// Token: 0x020005E7 RID: 1511
	[SupportedBy("Chrome")]
	public class Error
	{
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x0000CBD7 File Offset: 0x0000ADD7
		// (set) Token: 0x06001C87 RID: 7303 RVA: 0x0000CBDF File Offset: 0x0000ADDF
		public string Message { get; set; }

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		// (set) Token: 0x06001C89 RID: 7305 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		public long Code { get; set; }

		// Token: 0x04000FDF RID: 4063
		[CompilerGenerated]
		private string a;

		// Token: 0x04000FE0 RID: 4064
		[CompilerGenerated]
		private long b;
	}
}
