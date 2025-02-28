using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Agiso.ChromeDevTools
{
	// Token: 0x02000104 RID: 260
	public class ExecuteJsResultInfo
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00004681 File Offset: 0x00002881
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x00004689 File Offset: 0x00002889
		[JsonProperty("id")]
		public int id { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00004692 File Offset: 0x00002892
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x0000469A File Offset: 0x0000289A
		[JsonProperty("result")]
		public ExecuteJsInnerResultInfo result { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x000046AC File Offset: 0x000028AC
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x000046A3 File Offset: 0x000028A3
		[JsonProperty("isError")]
		public bool isError { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x000046BD File Offset: 0x000028BD
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x000046B4 File Offset: 0x000028B4
		[JsonProperty("errorMsg")]
		public string errorMsg { get; set; }

		// Token: 0x0400050E RID: 1294
		[CompilerGenerated]
		private int a;

		// Token: 0x0400050F RID: 1295
		[CompilerGenerated]
		private ExecuteJsInnerResultInfo b;

		// Token: 0x04000510 RID: 1296
		[CompilerGenerated]
		private bool c;

		// Token: 0x04000511 RID: 1297
		[CompilerGenerated]
		private string d;
	}
}
