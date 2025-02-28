using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006CF RID: 1743
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct INTERNET_CACHE_ENTRY_INFO
	{
		// Token: 0x04001C4D RID: 7245
		public int dwStructSize;

		// Token: 0x04001C4E RID: 7246
		public IntPtr lpszSourceUrlName;

		// Token: 0x04001C4F RID: 7247
		public IntPtr lpszLocalFileName;

		// Token: 0x04001C50 RID: 7248
		public int CacheEntryType;

		// Token: 0x04001C51 RID: 7249
		public int dwUseCount;

		// Token: 0x04001C52 RID: 7250
		public int dwHitRate;

		// Token: 0x04001C53 RID: 7251
		public int dwSizeLow;

		// Token: 0x04001C54 RID: 7252
		public int dwSizeHigh;

		// Token: 0x04001C55 RID: 7253
		public _FILETIME LastModifiedTime;

		// Token: 0x04001C56 RID: 7254
		public _FILETIME ExpireTime;

		// Token: 0x04001C57 RID: 7255
		public _FILETIME LastAccessTime;

		// Token: 0x04001C58 RID: 7256
		public _FILETIME LastSyncTime;

		// Token: 0x04001C59 RID: 7257
		public IntPtr lpHeaderInfo;

		// Token: 0x04001C5A RID: 7258
		public int dwHeaderInfoSize;

		// Token: 0x04001C5B RID: 7259
		public IntPtr lpszFileExtension;

		// Token: 0x04001C5C RID: 7260
		public int dwExemptDelta;
	}
}
