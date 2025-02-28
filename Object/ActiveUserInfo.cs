using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace AliwwClient.Object
{
	// Token: 0x02000099 RID: 153
	public class ActiveUserInfo
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x000036DD File Offset: 0x000018DD
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x000036E5 File Offset: 0x000018E5
		[JsonProperty("uid")]
		public string Uid { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x000036EE File Offset: 0x000018EE
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x000036F6 File Offset: 0x000018F6
		[JsonProperty("securityUID")]
		public string SecurityUID { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x000036FF File Offset: 0x000018FF
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x00003707 File Offset: 0x00001907
		[JsonProperty("bizDomain")]
		public string BizDomain { get; set; }

		// Token: 0x04000374 RID: 884
		[CompilerGenerated]
		private string a;

		// Token: 0x04000375 RID: 885
		[CompilerGenerated]
		private string b;

		// Token: 0x04000376 RID: 886
		[CompilerGenerated]
		private string c;
	}
}
