using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006D4 RID: 1748
	public struct DISPLAY_DEVICE
	{
		// Token: 0x04001C75 RID: 7285
		public int cb;

		// Token: 0x04001C76 RID: 7286
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string DeviceName;

		// Token: 0x04001C77 RID: 7287
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string DeviceString;

		// Token: 0x04001C78 RID: 7288
		public int StateFlags;

		// Token: 0x04001C79 RID: 7289
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string DeviceID;

		// Token: 0x04001C7A RID: 7290
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string DeviceKey;
	}
}
