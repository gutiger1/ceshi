using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006C1 RID: 1729
	public struct COPYDATASTRUCT
	{
		// Token: 0x04001BA2 RID: 7074
		public IntPtr dwData;

		// Token: 0x04001BA3 RID: 7075
		public int cbData;

		// Token: 0x04001BA4 RID: 7076
		[MarshalAs(UnmanagedType.LPStr)]
		public string lpData;
	}
}
