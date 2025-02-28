using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace AliwwClient.Object
{
	// Token: 0x0200009B RID: 155
	public class PongInfo
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00003710 File Offset: 0x00001910
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00003718 File Offset: 0x00001918
		[JsonProperty("key")]
		public string Key { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00003721 File Offset: 0x00001921
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00003729 File Offset: 0x00001929
		[JsonProperty("nick")]
		public string Nick { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00003732 File Offset: 0x00001932
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0000373A File Offset: 0x0000193A
		[JsonProperty("status")]
		public string Status { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00003743 File Offset: 0x00001943
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0000374B File Offset: 0x0000194B
		[JsonProperty("type")]
		public string Type { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00003754 File Offset: 0x00001954
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x0000375C File Offset: 0x0000195C
		[JsonProperty("json")]
		public string Json { get; set; }

		// Token: 0x04000378 RID: 888
		[CompilerGenerated]
		private string a;

		// Token: 0x04000379 RID: 889
		[CompilerGenerated]
		private string b;

		// Token: 0x0400037A RID: 890
		[CompilerGenerated]
		private string c;

		// Token: 0x0400037B RID: 891
		[CompilerGenerated]
		private string d;

		// Token: 0x0400037C RID: 892
		[CompilerGenerated]
		private string e;
	}
}
