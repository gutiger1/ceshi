using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005C0 RID: 1472
	[SupportedBy("Chrome")]
	[Command("Debugger.searchInContent")]
	public class SearchInContentCommand
	{
		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0000C8DA File Offset: 0x0000AADA
		// (set) Token: 0x06001C06 RID: 7174 RVA: 0x0000C8E2 File Offset: 0x0000AAE2
		public string ScriptId { get; set; }

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0000C8EB File Offset: 0x0000AAEB
		// (set) Token: 0x06001C08 RID: 7176 RVA: 0x0000C8F3 File Offset: 0x0000AAF3
		public string Query { get; set; }

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0000C8FC File Offset: 0x0000AAFC
		// (set) Token: 0x06001C0A RID: 7178 RVA: 0x0000C904 File Offset: 0x0000AB04
		public bool CaseSensitive { get; set; }

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0000C90D File Offset: 0x0000AB0D
		// (set) Token: 0x06001C0C RID: 7180 RVA: 0x0000C915 File Offset: 0x0000AB15
		public bool IsRegex { get; set; }

		// Token: 0x04000FB2 RID: 4018
		[CompilerGenerated]
		private string a;

		// Token: 0x04000FB3 RID: 4019
		[CompilerGenerated]
		private string b;

		// Token: 0x04000FB4 RID: 4020
		[CompilerGenerated]
		private bool c;

		// Token: 0x04000FB5 RID: 4021
		[CompilerGenerated]
		private bool d;
	}
}
