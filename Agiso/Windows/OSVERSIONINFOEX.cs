using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006CB RID: 1739
	public struct OSVERSIONINFOEX
	{
		// Token: 0x04001C17 RID: 7191
		public int int_0;

		// Token: 0x04001C18 RID: 7192
		public int dwMajorVersion;

		// Token: 0x04001C19 RID: 7193
		public int dwMinorVersion;

		// Token: 0x04001C1A RID: 7194
		public int dwBuildNumber;

		// Token: 0x04001C1B RID: 7195
		public int dwPlatformId;

		// Token: 0x04001C1C RID: 7196
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string szCSDVersion;

		// Token: 0x04001C1D RID: 7197
		public short wServicePackMajor;

		// Token: 0x04001C1E RID: 7198
		public short wServicePackMinor;

		// Token: 0x04001C1F RID: 7199
		public short wSuiteMask;

		// Token: 0x04001C20 RID: 7200
		public byte wProductType;

		// Token: 0x04001C21 RID: 7201
		public byte wReserved;
	}
}
