using System;
using System.Drawing;

namespace Agiso.Windows
{
	// Token: 0x020006E5 RID: 1765
	public struct ProcessInfo
	{
		// Token: 0x04001CF3 RID: 7411
		public IntPtr hwnd;

		// Token: 0x04001CF4 RID: 7412
		public string ClassName;

		// Token: 0x04001CF5 RID: 7413
		public string WindowText;

		// Token: 0x04001CF6 RID: 7414
		public string path;

		// Token: 0x04001CF7 RID: 7415
		public int processsize;

		// Token: 0x04001CF8 RID: 7416
		public Point location;

		// Token: 0x04001CF9 RID: 7417
		public Size wsize;

		// Token: 0x04001CFA RID: 7418
		public Size csize;

		// Token: 0x04001CFB RID: 7419
		public DateTime starttime;

		// Token: 0x04001CFC RID: 7420
		public string runtime;

		// Token: 0x04001CFD RID: 7421
		public IntPtr phwnd;

		// Token: 0x04001CFE RID: 7422
		public int id;

		// Token: 0x04001CFF RID: 7423
		public string text;

		// Token: 0x04001D00 RID: 7424
		public int dwStyle;

		// Token: 0x04001D01 RID: 7425
		public int dwExStyle;

		// Token: 0x04001D02 RID: 7426
		public uint cxWindowBorders;

		// Token: 0x04001D03 RID: 7427
		public uint cyWindowBorders;
	}
}
