using System;
using System.Runtime.CompilerServices;

namespace Agiso.Handler
{
	// Token: 0x020000F2 RID: 242
	public class FindWindowOption
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00004519 File Offset: 0x00002719
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x00004521 File Offset: 0x00002721
		public bool IsOnlyFirst { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0000452A File Offset: 0x0000272A
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x00004532 File Offset: 0x00002732
		public CompareWindowOption ClassNameComparisonType { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0000453B File Offset: 0x0000273B
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x00004543 File Offset: 0x00002743
		public CompareWindowOption WindowNameComparisonType { get; set; }

		// Token: 0x040004E5 RID: 1253
		[CompilerGenerated]
		private bool a;

		// Token: 0x040004E6 RID: 1254
		[CompilerGenerated]
		private CompareWindowOption b;

		// Token: 0x040004E7 RID: 1255
		[CompilerGenerated]
		private CompareWindowOption c;
	}
}
