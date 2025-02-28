using System;

namespace Agiso.Windows
{
	// Token: 0x020006E4 RID: 1764
	public struct BY_HANDLE_FILE_INFORMATION
	{
		// Token: 0x04001CE8 RID: 7400
		public int dwFileAttributes;

		// Token: 0x04001CE9 RID: 7401
		public _FILETIME ftCreationTime;

		// Token: 0x04001CEA RID: 7402
		public _FILETIME ftLastAccessTime;

		// Token: 0x04001CEB RID: 7403
		public _FILETIME ftLastWriteTime;

		// Token: 0x04001CEC RID: 7404
		public int dwVolumeSerialNumber;

		// Token: 0x04001CED RID: 7405
		public int nFileSizeHigh;

		// Token: 0x04001CEE RID: 7406
		public int nFileSizeLow;

		// Token: 0x04001CEF RID: 7407
		public int nNumberOfLinks;

		// Token: 0x04001CF0 RID: 7408
		public int nFileIndexHigh;

		// Token: 0x04001CF1 RID: 7409
		public int nFileIndexLow;

		// Token: 0x04001CF2 RID: 7410
		public int dwOID;
	}
}
