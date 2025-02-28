using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006CA RID: 1738
	public struct OSVERSIONINFO
	{
		// Token: 0x04001C11 RID: 7185
		public int int_0;

		// Token: 0x04001C12 RID: 7186
		public int dwMajorVersion;

		// Token: 0x04001C13 RID: 7187
		public int dwMinorVersion;

		// Token: 0x04001C14 RID: 7188
		public int dwBuildNumber;

		// Token: 0x04001C15 RID: 7189
		public int dwPlatformId;

		// Token: 0x04001C16 RID: 7190
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string szCSDVersion;
	}
}
