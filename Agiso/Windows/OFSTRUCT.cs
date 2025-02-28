using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006BF RID: 1727
	public struct OFSTRUCT
	{
		// Token: 0x04001B94 RID: 7060
		public byte cBytes;

		// Token: 0x04001B95 RID: 7061
		public byte fFixedDisk;

		// Token: 0x04001B96 RID: 7062
		public ushort nErrCode;

		// Token: 0x04001B97 RID: 7063
		public ushort Reserved1;

		// Token: 0x04001B98 RID: 7064
		public ushort Reserved2;

		// Token: 0x04001B99 RID: 7065
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
		public string szPathName;
	}
}
