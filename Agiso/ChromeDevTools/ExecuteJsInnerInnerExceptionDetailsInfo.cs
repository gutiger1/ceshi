using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Agiso.ChromeDevTools
{
	// Token: 0x02000107 RID: 263
	public class ExecuteJsInnerInnerExceptionDetailsInfo
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0000475E File Offset: 0x0000295E
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x00004766 File Offset: 0x00002966
		[JsonProperty("text")]
		public string text { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0000476F File Offset: 0x0000296F
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x00004777 File Offset: 0x00002977
		[JsonProperty("url")]
		public string url { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00004780 File Offset: 0x00002980
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x00004788 File Offset: 0x00002988
		[JsonProperty("line")]
		public int line { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00004791 File Offset: 0x00002991
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x00004799 File Offset: 0x00002999
		[JsonProperty("column")]
		public int column { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000047A2 File Offset: 0x000029A2
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x000047AA File Offset: 0x000029AA
		[JsonProperty("scriptId")]
		public string scriptId { get; set; }

		// Token: 0x0400051B RID: 1307
		[CompilerGenerated]
		private string a;

		// Token: 0x0400051C RID: 1308
		[CompilerGenerated]
		private string b;

		// Token: 0x0400051D RID: 1309
		[CompilerGenerated]
		private int c;

		// Token: 0x0400051E RID: 1310
		[CompilerGenerated]
		private int d;

		// Token: 0x0400051F RID: 1311
		[CompilerGenerated]
		private string e;
	}
}
