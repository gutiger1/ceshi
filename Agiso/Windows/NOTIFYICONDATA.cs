using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006E3 RID: 1763
	public struct NOTIFYICONDATA
	{
		// Token: 0x04001CDA RID: 7386
		public int cbSize;

		// Token: 0x04001CDB RID: 7387
		public IntPtr hWnd;

		// Token: 0x04001CDC RID: 7388
		public uint uID;

		// Token: 0x04001CDD RID: 7389
		public uint uFlags;

		// Token: 0x04001CDE RID: 7390
		public uint uCallbackMessage;

		// Token: 0x04001CDF RID: 7391
		public IntPtr hIcon;

		// Token: 0x04001CE0 RID: 7392
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string szTip;

		// Token: 0x04001CE1 RID: 7393
		public int dwState;

		// Token: 0x04001CE2 RID: 7394
		public int dwStateMask;

		// Token: 0x04001CE3 RID: 7395
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string szInfo;

		// Token: 0x04001CE4 RID: 7396
		public uint uTimeout;

		// Token: 0x04001CE5 RID: 7397
		public uint uVersion;

		// Token: 0x04001CE6 RID: 7398
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string szInfoTitle;

		// Token: 0x04001CE7 RID: 7399
		public int dwInfoFlags;
	}
}
