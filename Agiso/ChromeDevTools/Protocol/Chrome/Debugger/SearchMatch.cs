using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005C2 RID: 1474
	[SupportedBy("Chrome")]
	public class SearchMatch
	{
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x0000C92F File Offset: 0x0000AB2F
		// (set) Token: 0x06001C12 RID: 7186 RVA: 0x0000C937 File Offset: 0x0000AB37
		public double LineNumber { get; set; }

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x0000C940 File Offset: 0x0000AB40
		// (set) Token: 0x06001C14 RID: 7188 RVA: 0x0000C948 File Offset: 0x0000AB48
		public string LineContent { get; set; }

		// Token: 0x04000FB7 RID: 4023
		[CompilerGenerated]
		private double a;

		// Token: 0x04000FB8 RID: 4024
		[CompilerGenerated]
		private string b;
	}
}
