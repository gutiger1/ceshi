using System;

namespace Agiso.Windows
{
	// Token: 0x020006C8 RID: 1736
	public struct SHELLEXECUTEINFO
	{
		// Token: 0x04001BF5 RID: 7157
		public int cbSize;

		// Token: 0x04001BF6 RID: 7158
		public int fMask;

		// Token: 0x04001BF7 RID: 7159
		public IntPtr hwnd;

		// Token: 0x04001BF8 RID: 7160
		public string lpVerb;

		// Token: 0x04001BF9 RID: 7161
		public string lpFile;

		// Token: 0x04001BFA RID: 7162
		public string lpParameters;

		// Token: 0x04001BFB RID: 7163
		public string lpDirectory;

		// Token: 0x04001BFC RID: 7164
		public int nShow;

		// Token: 0x04001BFD RID: 7165
		public IntPtr hInstApp;

		// Token: 0x04001BFE RID: 7166
		public IntPtr lpIDList;

		// Token: 0x04001BFF RID: 7167
		public string lpClass;

		// Token: 0x04001C00 RID: 7168
		public IntPtr hkeyClass;

		// Token: 0x04001C01 RID: 7169
		public int dwHotKey;

		// Token: 0x04001C02 RID: 7170
		public IntPtr hIcon;

		// Token: 0x04001C03 RID: 7171
		public IntPtr hProcess;
	}
}
