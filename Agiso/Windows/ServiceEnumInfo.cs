using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006E7 RID: 1767
	public struct ServiceEnumInfo
	{
		// Token: 0x04001D07 RID: 7431
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
		public string szPrefix;

		// Token: 0x04001D08 RID: 7432
		public string szDllName;

		// Token: 0x04001D09 RID: 7433
		public IntPtr hServiceHandle;

		// Token: 0x04001D0A RID: 7434
		public int dwServiceState;
	}
}
