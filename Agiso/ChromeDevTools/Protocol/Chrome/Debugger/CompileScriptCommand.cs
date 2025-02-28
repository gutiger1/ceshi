using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x02000588 RID: 1416
	[SupportedBy("Chrome")]
	[Command("Debugger.compileScript")]
	public class CompileScriptCommand
	{
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0000C203 File Offset: 0x0000A403
		// (set) Token: 0x06001B00 RID: 6912 RVA: 0x0000C20B File Offset: 0x0000A40B
		public string Expression { get; set; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x0000C214 File Offset: 0x0000A414
		// (set) Token: 0x06001B02 RID: 6914 RVA: 0x0000C21C File Offset: 0x0000A41C
		public string SourceURL { get; set; }

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x0000C225 File Offset: 0x0000A425
		// (set) Token: 0x06001B04 RID: 6916 RVA: 0x0000C22D File Offset: 0x0000A42D
		public bool PersistScript { get; set; }

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x0000C236 File Offset: 0x0000A436
		// (set) Token: 0x06001B06 RID: 6918 RVA: 0x0000C23E File Offset: 0x0000A43E
		public long ExecutionContextId { get; set; }

		// Token: 0x04000F4B RID: 3915
		[CompilerGenerated]
		private string a;

		// Token: 0x04000F4C RID: 3916
		[CompilerGenerated]
		private string b;

		// Token: 0x04000F4D RID: 3917
		[CompilerGenerated]
		private bool c;

		// Token: 0x04000F4E RID: 3918
		[CompilerGenerated]
		private long d;
	}
}
