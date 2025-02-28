using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006F6 RID: 1782
	[StructLayout(LayoutKind.Sequential)]
	public class TRAYDATA
	{
		// Token: 0x04001D59 RID: 7513
		public IntPtr hwnd;

		// Token: 0x04001D5A RID: 7514
		public uint uID;

		// Token: 0x04001D5B RID: 7515
		public uint uCallbackMessage;

		// Token: 0x04001D5C RID: 7516
		public int Reserved0;

		// Token: 0x04001D5D RID: 7517
		public int Reserved1;

		// Token: 0x04001D5E RID: 7518
		public IntPtr hIcon;
	}
}
