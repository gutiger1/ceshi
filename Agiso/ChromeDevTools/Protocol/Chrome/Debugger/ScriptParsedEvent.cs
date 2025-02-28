using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.Debugger
{
	// Token: 0x020005BF RID: 1471
	[SupportedBy("Chrome")]
	[Event("Debugger.scriptParsed")]
	public class ScriptParsedEvent
	{
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0000C830 File Offset: 0x0000AA30
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x0000C838 File Offset: 0x0000AA38
		public string ScriptId { get; set; }

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x0000C841 File Offset: 0x0000AA41
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x0000C849 File Offset: 0x0000AA49
		public string Url { get; set; }

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x0000C852 File Offset: 0x0000AA52
		// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x0000C85A File Offset: 0x0000AA5A
		public long StartLine { get; set; }

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x0000C863 File Offset: 0x0000AA63
		// (set) Token: 0x06001BF7 RID: 7159 RVA: 0x0000C86B File Offset: 0x0000AA6B
		public long StartColumn { get; set; }

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0000C874 File Offset: 0x0000AA74
		// (set) Token: 0x06001BF9 RID: 7161 RVA: 0x0000C87C File Offset: 0x0000AA7C
		public long EndLine { get; set; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0000C885 File Offset: 0x0000AA85
		// (set) Token: 0x06001BFB RID: 7163 RVA: 0x0000C88D File Offset: 0x0000AA8D
		public long EndColumn { get; set; }

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x0000C896 File Offset: 0x0000AA96
		// (set) Token: 0x06001BFD RID: 7165 RVA: 0x0000C89E File Offset: 0x0000AA9E
		public bool IsContentScript { get; set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x0000C8A7 File Offset: 0x0000AAA7
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x0000C8AF File Offset: 0x0000AAAF
		public bool IsInternalScript { get; set; }

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		// (set) Token: 0x06001C01 RID: 7169 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
		public string SourceMapURL { get; set; }

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x0000C8C9 File Offset: 0x0000AAC9
		// (set) Token: 0x06001C03 RID: 7171 RVA: 0x0000C8D1 File Offset: 0x0000AAD1
		public bool HasSourceURL { get; set; }

		// Token: 0x04000FA8 RID: 4008
		[CompilerGenerated]
		private string a;

		// Token: 0x04000FA9 RID: 4009
		[CompilerGenerated]
		private string b;

		// Token: 0x04000FAA RID: 4010
		[CompilerGenerated]
		private long c;

		// Token: 0x04000FAB RID: 4011
		[CompilerGenerated]
		private long d;

		// Token: 0x04000FAC RID: 4012
		[CompilerGenerated]
		private long e;

		// Token: 0x04000FAD RID: 4013
		[CompilerGenerated]
		private long f;

		// Token: 0x04000FAE RID: 4014
		[CompilerGenerated]
		private bool g;

		// Token: 0x04000FAF RID: 4015
		[CompilerGenerated]
		private bool h;

		// Token: 0x04000FB0 RID: 4016
		[CompilerGenerated]
		private string i;

		// Token: 0x04000FB1 RID: 4017
		[CompilerGenerated]
		private bool j;
	}
}
