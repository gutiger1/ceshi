using System;
using System.Runtime.CompilerServices;

namespace Agiso.Handler
{
	// Token: 0x020000F1 RID: 241
	public class CompareWindowOption
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x000044E7 File Offset: 0x000026E7
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x000044EF File Offset: 0x000026EF
		public StringComparison ComparisonType { get; set; } = StringComparison.OrdinalIgnoreCase;

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x000044F8 File Offset: 0x000026F8
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x00004500 File Offset: 0x00002700
		public bool IsAllowBlur { get; set; }

		// Token: 0x040004E3 RID: 1251
		[CompilerGenerated]
		private StringComparison a;

		// Token: 0x040004E4 RID: 1252
		[CompilerGenerated]
		private bool b;
	}
}
