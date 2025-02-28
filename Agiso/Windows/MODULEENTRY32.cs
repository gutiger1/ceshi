using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006D2 RID: 1746
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct MODULEENTRY32
	{
		// Token: 0x04001C64 RID: 7268
		public int dwSize;

		// Token: 0x04001C65 RID: 7269
		public int th32ModuleID;

		// Token: 0x04001C66 RID: 7270
		public int th32ProcessID;

		// Token: 0x04001C67 RID: 7271
		public int GlblcntUsage;

		// Token: 0x04001C68 RID: 7272
		public int ProccntUsage;

		// Token: 0x04001C69 RID: 7273
		public byte modBaseAddr;

		// Token: 0x04001C6A RID: 7274
		public int modBaseSize;

		// Token: 0x04001C6B RID: 7275
		public IntPtr hModule;

		// Token: 0x04001C6C RID: 7276
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string szModule;

		// Token: 0x04001C6D RID: 7277
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string szExePath;
	}
}
