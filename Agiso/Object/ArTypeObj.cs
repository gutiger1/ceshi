using System;
using System.Runtime.CompilerServices;

namespace Agiso.Object
{
	// Token: 0x02000697 RID: 1687
	public class ArTypeObj
	{
		// Token: 0x0600209D RID: 8349 RVA: 0x0000E7D5 File Offset: 0x0000C9D5
		public ArTypeObj(EnumArType arTypeValue, string arTypeText)
		{
			this.ArTypeValue = (int)arTypeValue;
			this.ArTypeText = arTypeText;
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		// (set) Token: 0x0600209F RID: 8351 RVA: 0x0000E7F4 File Offset: 0x0000C9F4
		public int ArTypeValue { get; set; }

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x0000E7FD File Offset: 0x0000C9FD
		// (set) Token: 0x060020A1 RID: 8353 RVA: 0x0000E805 File Offset: 0x0000CA05
		public string ArTypeText { get; set; }

		// Token: 0x04001269 RID: 4713
		[CompilerGenerated]
		private int a;

		// Token: 0x0400126A RID: 4714
		[CompilerGenerated]
		private string b;
	}
}
