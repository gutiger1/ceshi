using System;
using System.Runtime.CompilerServices;

namespace AliwwClient.Server
{
	// Token: 0x0200008B RID: 139
	public class RequestItem
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00003513 File Offset: 0x00001713
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000351B File Offset: 0x0000171B
		public string Url { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00003524 File Offset: 0x00001724
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0000352C File Offset: 0x0000172C
		public string ReferUrl { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00003535 File Offset: 0x00001735
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0000353D File Offset: 0x0000173D
		public DateTime RequestTime { get; set; }

		// Token: 0x04000346 RID: 838
		[CompilerGenerated]
		private string a;

		// Token: 0x04000347 RID: 839
		[CompilerGenerated]
		private string b;

		// Token: 0x04000348 RID: 840
		[CompilerGenerated]
		private DateTime c;
	}
}
