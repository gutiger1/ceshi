using System;
using System.Runtime.InteropServices;

namespace AliwwClient
{
	// Token: 0x02000065 RID: 101
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class TVITEM
	{
		// Token: 0x040002D9 RID: 729
		public int mask;

		// Token: 0x040002DA RID: 730
		public IntPtr hItem;

		// Token: 0x040002DB RID: 731
		public int state;

		// Token: 0x040002DC RID: 732
		public int stateMask;

		// Token: 0x040002DD RID: 733
		public IntPtr pszText;

		// Token: 0x040002DE RID: 734
		public int cchTextMax;

		// Token: 0x040002DF RID: 735
		public int iImage;

		// Token: 0x040002E0 RID: 736
		public int iSelectedImage;

		// Token: 0x040002E1 RID: 737
		public int cChildren;

		// Token: 0x040002E2 RID: 738
		public IntPtr lParam;

		// Token: 0x040002E3 RID: 739
		public int int_0;
	}
}
