using System;
using System.Runtime.CompilerServices;
using Agiso.ChromeDevTools.Protocol.Chrome.Runtime;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x0200059A RID: 1434
	[SupportedBy("Chrome")]
	public class GeneratorObjectDetails
	{
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		// (set) Token: 0x06001B4C RID: 6988 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		public RemoteObject Function { get; set; }

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0000C401 File Offset: 0x0000A601
		// (set) Token: 0x06001B4E RID: 6990 RVA: 0x0000C409 File Offset: 0x0000A609
		public string FunctionName { get; set; }

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x0000C412 File Offset: 0x0000A612
		// (set) Token: 0x06001B50 RID: 6992 RVA: 0x0000C41A File Offset: 0x0000A61A
		public string Status { get; set; }

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x0000C423 File Offset: 0x0000A623
		// (set) Token: 0x06001B52 RID: 6994 RVA: 0x0000C42B File Offset: 0x0000A62B
		public Location Location { get; set; }

		// Token: 0x04000F68 RID: 3944
		[CompilerGenerated]
		private RemoteObject a;

		// Token: 0x04000F69 RID: 3945
		[CompilerGenerated]
		private string b;

		// Token: 0x04000F6A RID: 3946
		[CompilerGenerated]
		private string c;

		// Token: 0x04000F6B RID: 3947
		[CompilerGenerated]
		private Location d;
	}
}
