using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005D8 RID: 1496
	[SupportedBy("Chrome")]
	public class StackTrace
	{
		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x0000CB4F File Offset: 0x0000AD4F
		// (set) Token: 0x06001C68 RID: 7272 RVA: 0x0000CB57 File Offset: 0x0000AD57
		public CallFrame[] CallFrames { get; set; }

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x0000CB60 File Offset: 0x0000AD60
		// (set) Token: 0x06001C6A RID: 7274 RVA: 0x0000CB68 File Offset: 0x0000AD68
		public string Description { get; set; }

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0000CB71 File Offset: 0x0000AD71
		// (set) Token: 0x06001C6C RID: 7276 RVA: 0x0000CB79 File Offset: 0x0000AD79
		public StackTrace AsyncStackTrace { get; set; }

		// Token: 0x04000FD7 RID: 4055
		[CompilerGenerated]
		private CallFrame[] a;

		// Token: 0x04000FD8 RID: 4056
		[CompilerGenerated]
		private string b;

		// Token: 0x04000FD9 RID: 4057
		[CompilerGenerated]
		private StackTrace c;
	}
}
