using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools
{
	// Token: 0x0200010E RID: 270
	public class CommandResponse : ICommandResponse
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00004810 File Offset: 0x00002A10
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x00004818 File Offset: 0x00002A18
		public long Id { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00004821 File Offset: 0x00002A21
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x00004829 File Offset: 0x00002A29
		public string Method { get; set; }

		// Token: 0x04000524 RID: 1316
		[CompilerGenerated]
		private long a;

		// Token: 0x04000525 RID: 1317
		[CompilerGenerated]
		private string b;
	}
}
