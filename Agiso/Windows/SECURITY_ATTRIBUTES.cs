using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006D1 RID: 1745
	[StructLayout(LayoutKind.Sequential)]
	public class SECURITY_ATTRIBUTES
	{
		// Token: 0x04001C61 RID: 7265
		public int nLength;

		// Token: 0x04001C62 RID: 7266
		public string lpSecurityDescriptor;

		// Token: 0x04001C63 RID: 7267
		public bool bInheritHandle;
	}
}
