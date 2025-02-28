using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace AliwwClient.Object
{
	// Token: 0x0200009D RID: 157
	public class UpgradeFile
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000037A9 File Offset: 0x000019A9
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x000037B1 File Offset: 0x000019B1
		[JsonProperty("DotNet2")]
		public string DotNet2 { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000037BA File Offset: 0x000019BA
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x000037C2 File Offset: 0x000019C2
		[JsonProperty("DotNet45")]
		public string DotNet45 { get; set; }

		// Token: 0x04000381 RID: 897
		[CompilerGenerated]
		private string a;

		// Token: 0x04000382 RID: 898
		[CompilerGenerated]
		private string b;
	}
}
