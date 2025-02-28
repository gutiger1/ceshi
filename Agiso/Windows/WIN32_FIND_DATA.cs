using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006C9 RID: 1737
	[BestFitMapping(false)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct WIN32_FIND_DATA
	{
		// Token: 0x04001C04 RID: 7172
		public int dwFileAttributes;

		// Token: 0x04001C05 RID: 7173
		public int ftCreationTime_dwLowDateTime;

		// Token: 0x04001C06 RID: 7174
		public int ftCreationTime_dwHighDateTime;

		// Token: 0x04001C07 RID: 7175
		public int ftLastAccessTime_dwLowDateTime;

		// Token: 0x04001C08 RID: 7176
		public int ftLastAccessTime_dwHighDateTime;

		// Token: 0x04001C09 RID: 7177
		public int ftLastWriteTime_dwLowDateTime;

		// Token: 0x04001C0A RID: 7178
		public int ftLastWriteTime_dwHighDateTime;

		// Token: 0x04001C0B RID: 7179
		public int nFileSizeHigh;

		// Token: 0x04001C0C RID: 7180
		public int nFileSizeLow;

		// Token: 0x04001C0D RID: 7181
		public int dwReserved0;

		// Token: 0x04001C0E RID: 7182
		public int dwReserved1;

		// Token: 0x04001C0F RID: 7183
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string cFileName;

		// Token: 0x04001C10 RID: 7184
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
		public string cAlternateFileName;
	}
}
