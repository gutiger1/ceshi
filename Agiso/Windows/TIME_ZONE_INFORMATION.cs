using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006E8 RID: 1768
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct TIME_ZONE_INFORMATION
	{
		// Token: 0x04001D0B RID: 7435
		public long Bias;

		// Token: 0x04001D0C RID: 7436
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string StandardName;

		// Token: 0x04001D0D RID: 7437
		public SYSTEMTIME StandardDate;

		// Token: 0x04001D0E RID: 7438
		public long StandardBias;

		// Token: 0x04001D0F RID: 7439
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string DaylightName;

		// Token: 0x04001D10 RID: 7440
		private SYSTEMTIME a;

		// Token: 0x04001D11 RID: 7441
		public long DaylightBias;
	}
}
