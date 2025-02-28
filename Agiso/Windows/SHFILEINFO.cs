using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006D5 RID: 1749
	public struct SHFILEINFO
	{
		// Token: 0x04001C7B RID: 7291
		public IntPtr hIcon;

		// Token: 0x04001C7C RID: 7292
		public int iIcon;

		// Token: 0x04001C7D RID: 7293
		public int dwAttributes;

		// Token: 0x04001C7E RID: 7294
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string szDisplayName;

		// Token: 0x04001C7F RID: 7295
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
		public string szTypeName;
	}
}
