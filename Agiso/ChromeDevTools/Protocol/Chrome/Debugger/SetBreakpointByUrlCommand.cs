using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005C7 RID: 1479
	[SupportedBy("Chrome")]
	[Command("Debugger.setBreakpointByUrl")]
	public class SetBreakpointByUrlCommand
	{
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x0000C973 File Offset: 0x0000AB73
		// (set) Token: 0x06001C1F RID: 7199 RVA: 0x0000C97B File Offset: 0x0000AB7B
		public long LineNumber { get; set; }

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0000C984 File Offset: 0x0000AB84
		// (set) Token: 0x06001C21 RID: 7201 RVA: 0x0000C98C File Offset: 0x0000AB8C
		public string Url { get; set; }

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x0000C995 File Offset: 0x0000AB95
		// (set) Token: 0x06001C23 RID: 7203 RVA: 0x0000C99D File Offset: 0x0000AB9D
		public string UrlRegex { get; set; }

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x0000C9A6 File Offset: 0x0000ABA6
		// (set) Token: 0x06001C25 RID: 7205 RVA: 0x0000C9AE File Offset: 0x0000ABAE
		public long ColumnNumber { get; set; }

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x0000C9B7 File Offset: 0x0000ABB7
		// (set) Token: 0x06001C27 RID: 7207 RVA: 0x0000C9BF File Offset: 0x0000ABBF
		public string Condition { get; set; }

		// Token: 0x04000FBB RID: 4027
		[CompilerGenerated]
		private long a;

		// Token: 0x04000FBC RID: 4028
		[CompilerGenerated]
		private string b;

		// Token: 0x04000FBD RID: 4029
		[CompilerGenerated]
		private string c;

		// Token: 0x04000FBE RID: 4030
		[CompilerGenerated]
		private long d;

		// Token: 0x04000FBF RID: 4031
		[CompilerGenerated]
		private string e;
	}
}
