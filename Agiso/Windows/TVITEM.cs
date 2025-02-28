using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006B8 RID: 1720
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct TVITEM
	{
		// Token: 0x04001B66 RID: 7014
		public int mask;

		// Token: 0x04001B67 RID: 7015
		public IntPtr hItem;

		// Token: 0x04001B68 RID: 7016
		public int state;

		// Token: 0x04001B69 RID: 7017
		public int stateMask;

		// Token: 0x04001B6A RID: 7018
		public IntPtr pszText;

		// Token: 0x04001B6B RID: 7019
		public int cchTextMax;

		// Token: 0x04001B6C RID: 7020
		public int iImage;

		// Token: 0x04001B6D RID: 7021
		public int iSelectedImage;

		// Token: 0x04001B6E RID: 7022
		public int cChildren;

		// Token: 0x04001B6F RID: 7023
		public IntPtr lParam;

		// Token: 0x04001B70 RID: 7024
		public int int_0;
	}
}
