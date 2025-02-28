using System;
using System.Runtime.CompilerServices;

namespace Agiso.DbManager
{
	// Token: 0x020006A0 RID: 1696
	public struct SellerSendMsgInfo
	{
		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x0000E85C File Offset: 0x0000CA5C
		// (set) Token: 0x060020DC RID: 8412 RVA: 0x0000E864 File Offset: 0x0000CA64
		public string SellerNick { get; set; }

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x0000E86D File Offset: 0x0000CA6D
		// (set) Token: 0x060020DE RID: 8414 RVA: 0x0000E875 File Offset: 0x0000CA75
		public int MsgCount { get; set; }

		// Token: 0x04001280 RID: 4736
		[CompilerGenerated]
		private string a;

		// Token: 0x04001281 RID: 4737
		[CompilerGenerated]
		private int b;
	}
}
