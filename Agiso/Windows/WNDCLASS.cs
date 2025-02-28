using System;

namespace Agiso.Windows
{
	// Token: 0x020006E2 RID: 1762
	public struct WNDCLASS
	{
		// Token: 0x04001CD0 RID: 7376
		public uint style;

		// Token: 0x04001CD1 RID: 7377
		public WNDPROC lpfnWndProc;

		// Token: 0x04001CD2 RID: 7378
		public int cbClsExtra;

		// Token: 0x04001CD3 RID: 7379
		public int cbWndExtra;

		// Token: 0x04001CD4 RID: 7380
		public IntPtr hInstance;

		// Token: 0x04001CD5 RID: 7381
		public IntPtr hIcon;

		// Token: 0x04001CD6 RID: 7382
		public IntPtr hCursor;

		// Token: 0x04001CD7 RID: 7383
		public IntPtr hbrBackground;

		// Token: 0x04001CD8 RID: 7384
		public string lpszMenuName;

		// Token: 0x04001CD9 RID: 7385
		public string lpszClassName;
	}
}
