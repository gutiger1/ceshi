using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006C0 RID: 1728
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public class _SHFILEOPSTRUCT
	{
		// Token: 0x04001B9A RID: 7066
		public IntPtr hwnd;

		// Token: 0x04001B9B RID: 7067
		public uint wFunc;

		// Token: 0x04001B9C RID: 7068
		public string pFrom;

		// Token: 0x04001B9D RID: 7069
		public string pTo;

		// Token: 0x04001B9E RID: 7070
		public ushort fFlags;

		// Token: 0x04001B9F RID: 7071
		public int fAnyOperationsAborted;

		// Token: 0x04001BA0 RID: 7072
		public IntPtr hNameMappings;

		// Token: 0x04001BA1 RID: 7073
		public string lpszProgressTitle;
	}
}
