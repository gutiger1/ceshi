using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools
{
	// Token: 0x02000116 RID: 278
	public class Event<T> : Event
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x000048E3 File Offset: 0x00002AE3
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x000048EB File Offset: 0x00002AEB
		public T Params { get; set; }

		// Token: 0x0400052E RID: 1326
		[CompilerGenerated]
		private T a;
	}
}
