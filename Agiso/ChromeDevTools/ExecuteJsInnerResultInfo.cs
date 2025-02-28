using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Agiso.ChromeDevTools
{
	// Token: 0x02000105 RID: 261
	public class ExecuteJsInnerResultInfo
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x000046C5 File Offset: 0x000028C5
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x000046CD File Offset: 0x000028CD
		[JsonProperty("result")]
		public ExecuteJsInnerInnerResultInfo result { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x000046D6 File Offset: 0x000028D6
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x000046DE File Offset: 0x000028DE
		[JsonProperty("wasThrown")]
		public bool wasThrown { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x000046E7 File Offset: 0x000028E7
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x000046EF File Offset: 0x000028EF
		[JsonProperty("exceptionDetails")]
		public ExecuteJsInnerInnerExceptionDetailsInfo exceptionDetails { get; set; }

		// Token: 0x04000512 RID: 1298
		[CompilerGenerated]
		private ExecuteJsInnerInnerResultInfo a;

		// Token: 0x04000513 RID: 1299
		[CompilerGenerated]
		private bool b;

		// Token: 0x04000514 RID: 1300
		[CompilerGenerated]
		private ExecuteJsInnerInnerExceptionDetailsInfo c;
	}
}
