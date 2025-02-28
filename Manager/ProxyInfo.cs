using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace AliwwClient.Manager
{
	// Token: 0x020000BB RID: 187
	public class ProxyInfo
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00003E50 File Offset: 0x00002050
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x00003E58 File Offset: 0x00002058
		[JsonProperty("bUse")]
		public bool BUse { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00003E61 File Offset: 0x00002061
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x00003E69 File Offset: 0x00002069
		[JsonProperty("type")]
		public int Type { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00003E72 File Offset: 0x00002072
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00003E7A File Offset: 0x0000207A
		[JsonProperty("host")]
		public string Host { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00003E83 File Offset: 0x00002083
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x00003E8B File Offset: 0x0000208B
		[JsonProperty("port")]
		public int Port { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00003E94 File Offset: 0x00002094
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00003E9C File Offset: 0x0000209C
		[JsonProperty("user")]
		public string User { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00003EA5 File Offset: 0x000020A5
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x00003EAD File Offset: 0x000020AD
		[JsonProperty("pass")]
		public string Pass { get; set; }

		// Token: 0x04000401 RID: 1025
		[CompilerGenerated]
		private bool a;

		// Token: 0x04000402 RID: 1026
		[CompilerGenerated]
		private int b;

		// Token: 0x04000403 RID: 1027
		[CompilerGenerated]
		private string c;

		// Token: 0x04000404 RID: 1028
		[CompilerGenerated]
		private int d;

		// Token: 0x04000405 RID: 1029
		[CompilerGenerated]
		private string e;

		// Token: 0x04000406 RID: 1030
		[CompilerGenerated]
		private string f;
	}
}
