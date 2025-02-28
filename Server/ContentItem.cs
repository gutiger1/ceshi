using System;
using System.Runtime.CompilerServices;

namespace AliwwClient.Server
{
	// Token: 0x0200008A RID: 138
	public class ContentItem
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x000034F1 File Offset: 0x000016F1
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x000034F9 File Offset: 0x000016F9
		public string Content { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00003502 File Offset: 0x00001702
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0000350A File Offset: 0x0000170A
		public DateTime ExpireTime { get; set; }

		// Token: 0x04000344 RID: 836
		[CompilerGenerated]
		private string a;

		// Token: 0x04000345 RID: 837
		[CompilerGenerated]
		private DateTime b;
	}
}
