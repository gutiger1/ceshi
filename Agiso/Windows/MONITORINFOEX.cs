using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006EB RID: 1771
	public struct MONITORINFOEX
	{
		// Token: 0x04001D1B RID: 7451
		public int cbSize;

		// Token: 0x04001D1C RID: 7452
		public Rectangle rcMonitor;

		// Token: 0x04001D1D RID: 7453
		public Rectangle rcWork;

		// Token: 0x04001D1E RID: 7454
		public int dwFlags;

		// Token: 0x04001D1F RID: 7455
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		public string szDevice;
	}
}
