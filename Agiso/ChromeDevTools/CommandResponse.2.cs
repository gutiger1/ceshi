using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools
{
	// Token: 0x0200010F RID: 271
	public class CommandResponse<T> : CommandResponse
	{
		// Token: 0x06000875 RID: 2165 RVA: 0x00004832 File Offset: 0x00002A32
		public CommandResponse()
		{
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0000483B File Offset: 0x00002A3B
		public CommandResponse(T result)
		{
			this.Result = result;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0000484B File Offset: 0x00002A4B
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x00004853 File Offset: 0x00002A53
		public T Result { get; private set; }

		// Token: 0x04000526 RID: 1318
		[CompilerGenerated]
		private T a;
	}
}
