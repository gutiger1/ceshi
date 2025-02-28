using System;
using System.Runtime.CompilerServices;

namespace Agiso.AliwwApi.Object
{
	// Token: 0x0200072B RID: 1835
	public class AliwwMsgElementMsgHead
	{
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x0000EEC0 File Offset: 0x0000D0C0
		// (set) Token: 0x06002470 RID: 9328 RVA: 0x0000EEC8 File Offset: 0x0000D0C8
		public string SenderName { get; set; }

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x0000EED1 File Offset: 0x0000D0D1
		// (set) Token: 0x06002472 RID: 9330 RVA: 0x0000EED9 File Offset: 0x0000D0D9
		public string SenderSite { get; set; }

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x0000EEE2 File Offset: 0x0000D0E2
		// (set) Token: 0x06002474 RID: 9332 RVA: 0x0000EEEA File Offset: 0x0000D0EA
		public DateTime MsgTime { get; set; }

		// Token: 0x04001E2E RID: 7726
		[CompilerGenerated]
		private string a;

		// Token: 0x04001E2F RID: 7727
		[CompilerGenerated]
		private string b;

		// Token: 0x04001E30 RID: 7728
		[CompilerGenerated]
		private DateTime c;
	}
}
