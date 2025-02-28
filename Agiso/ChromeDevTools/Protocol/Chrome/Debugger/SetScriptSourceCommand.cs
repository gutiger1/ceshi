using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005CF RID: 1487
	[Command("Debugger.setScriptSource")]
	[SupportedBy("Chrome")]
	public class SetScriptSourceCommand
	{
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0000CA50 File Offset: 0x0000AC50
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x0000CA58 File Offset: 0x0000AC58
		public string ScriptId { get; set; }

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x0000CA61 File Offset: 0x0000AC61
		// (set) Token: 0x06001C43 RID: 7235 RVA: 0x0000CA69 File Offset: 0x0000AC69
		public string ScriptSource { get; set; }

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0000CA72 File Offset: 0x0000AC72
		// (set) Token: 0x06001C45 RID: 7237 RVA: 0x0000CA7A File Offset: 0x0000AC7A
		public bool Preview { get; set; }

		// Token: 0x04000FC8 RID: 4040
		[CompilerGenerated]
		private string a;

		// Token: 0x04000FC9 RID: 4041
		[CompilerGenerated]
		private string b;

		// Token: 0x04000FCA RID: 4042
		[CompilerGenerated]
		private bool c;
	}
}
