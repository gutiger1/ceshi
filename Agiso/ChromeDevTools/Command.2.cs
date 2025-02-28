using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools
{
	// Token: 0x0200010A RID: 266
	public class Command<T> : Command
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x000047D5 File Offset: 0x000029D5
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x000047DD File Offset: 0x000029DD
		public T Params { get; set; }

		// Token: 0x04000522 RID: 1314
		[CompilerGenerated]
		private T a;
	}
}
