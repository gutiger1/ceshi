using System;

namespace Agiso.Windows
{
	// Token: 0x020006ED RID: 1773
	public struct KEYBDINPUT
	{
		// Token: 0x04001D24 RID: 7460
		public short wVk;

		// Token: 0x04001D25 RID: 7461
		public short wScan;

		// Token: 0x04001D26 RID: 7462
		public int dwFlags;

		// Token: 0x04001D27 RID: 7463
		public int time;

		// Token: 0x04001D28 RID: 7464
		public IntPtr dwExtraInfo;
	}
}
