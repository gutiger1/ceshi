using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005BE RID: 1470
	[SupportedBy("Chrome")]
	[Event("Debugger.scriptFailedToParse")]
	public class ScriptFailedToParseEvent
	{
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0000C786 File Offset: 0x0000A986
		// (set) Token: 0x06001BDC RID: 7132 RVA: 0x0000C78E File Offset: 0x0000A98E
		public string ScriptId { get; set; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0000C797 File Offset: 0x0000A997
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x0000C79F File Offset: 0x0000A99F
		public string Url { get; set; }

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		public long StartLine { get; set; }

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x0000C7B9 File Offset: 0x0000A9B9
		// (set) Token: 0x06001BE2 RID: 7138 RVA: 0x0000C7C1 File Offset: 0x0000A9C1
		public long StartColumn { get; set; }

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x0000C7CA File Offset: 0x0000A9CA
		// (set) Token: 0x06001BE4 RID: 7140 RVA: 0x0000C7D2 File Offset: 0x0000A9D2
		public long EndLine { get; set; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x0000C7DB File Offset: 0x0000A9DB
		// (set) Token: 0x06001BE6 RID: 7142 RVA: 0x0000C7E3 File Offset: 0x0000A9E3
		public long EndColumn { get; set; }

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		// (set) Token: 0x06001BE8 RID: 7144 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
		public bool IsContentScript { get; set; }

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x0000C7FD File Offset: 0x0000A9FD
		// (set) Token: 0x06001BEA RID: 7146 RVA: 0x0000C805 File Offset: 0x0000AA05
		public bool IsInternalScript { get; set; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0000C80E File Offset: 0x0000AA0E
		// (set) Token: 0x06001BEC RID: 7148 RVA: 0x0000C816 File Offset: 0x0000AA16
		public string SourceMapURL { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x0000C81F File Offset: 0x0000AA1F
		// (set) Token: 0x06001BEE RID: 7150 RVA: 0x0000C827 File Offset: 0x0000AA27
		public bool HasSourceURL { get; set; }

		// Token: 0x04000F9E RID: 3998
		[CompilerGenerated]
		private string a;

		// Token: 0x04000F9F RID: 3999
		[CompilerGenerated]
		private string b;

		// Token: 0x04000FA0 RID: 4000
		[CompilerGenerated]
		private long c;

		// Token: 0x04000FA1 RID: 4001
		[CompilerGenerated]
		private long d;

		// Token: 0x04000FA2 RID: 4002
		[CompilerGenerated]
		private long e;

		// Token: 0x04000FA3 RID: 4003
		[CompilerGenerated]
		private long f;

		// Token: 0x04000FA4 RID: 4004
		[CompilerGenerated]
		private bool g;

		// Token: 0x04000FA5 RID: 4005
		[CompilerGenerated]
		private bool h;

		// Token: 0x04000FA6 RID: 4006
		[CompilerGenerated]
		private string i;

		// Token: 0x04000FA7 RID: 4007
		[CompilerGenerated]
		private bool j;
	}
}
