using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools
{
	// Token: 0x02000109 RID: 265
	public class Command : ICommand
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x000047B3 File Offset: 0x000029B3
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x000047BB File Offset: 0x000029BB
		public long Id { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x000047C4 File Offset: 0x000029C4
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x000047CC File Offset: 0x000029CC
		public string Method { get; set; }

		// Token: 0x04000520 RID: 1312
		[CompilerGenerated]
		private long a;

		// Token: 0x04000521 RID: 1313
		[CompilerGenerated]
		private string b;
	}
}
