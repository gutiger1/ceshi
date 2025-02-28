using System;
using System.Runtime.CompilerServices;

namespace Agiso.Object
{
	// Token: 0x0200068F RID: 1679
	public class LogSyncEmotion
	{
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x0000E3EE File Offset: 0x0000C5EE
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x0000E3F6 File Offset: 0x0000C5F6
		public long UserId { get; set; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x0000E3FF File Offset: 0x0000C5FF
		// (set) Token: 0x06002004 RID: 8196 RVA: 0x0000E407 File Offset: 0x0000C607
		public string SellerNick { get; set; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x0000E410 File Offset: 0x0000C610
		// (set) Token: 0x06002006 RID: 8198 RVA: 0x0000E418 File Offset: 0x0000C618
		public string LastUserNick { get; set; }

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x0000E421 File Offset: 0x0000C621
		// (set) Token: 0x06002008 RID: 8200 RVA: 0x0000E429 File Offset: 0x0000C629
		public DateTime LastSyncTime { get; set; }

		// Token: 0x04001232 RID: 4658
		[CompilerGenerated]
		private long a;

		// Token: 0x04001233 RID: 4659
		[CompilerGenerated]
		private string b;

		// Token: 0x04001234 RID: 4660
		[CompilerGenerated]
		private string c;

		// Token: 0x04001235 RID: 4661
		[CompilerGenerated]
		private DateTime d;
	}
}
