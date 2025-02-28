using System;

namespace Agiso.Windows
{
	// Token: 0x020006C2 RID: 1730
	public struct WINDOWINFO
	{
		// Token: 0x04001BA5 RID: 7077
		public int cbSize;

		// Token: 0x04001BA6 RID: 7078
		public Rectangle rcWindow;

		// Token: 0x04001BA7 RID: 7079
		public Rectangle rcClient;

		// Token: 0x04001BA8 RID: 7080
		public int dwStyle;

		// Token: 0x04001BA9 RID: 7081
		public int dwExStyle;

		// Token: 0x04001BAA RID: 7082
		public int dwWindowStatus;

		// Token: 0x04001BAB RID: 7083
		public uint cxWindowBorders;

		// Token: 0x04001BAC RID: 7084
		public uint cyWindowBorders;

		// Token: 0x04001BAD RID: 7085
		public int atomWindowType;

		// Token: 0x04001BAE RID: 7086
		public int wCreatorVersion;

		// Token: 0x04001BAF RID: 7087
		public IntPtr hWnd;

		// Token: 0x04001BB0 RID: 7088
		public string szWindowName;

		// Token: 0x04001BB1 RID: 7089
		public string szClassName;

		// Token: 0x04001BB2 RID: 7090
		public string szExePath;
	}
}
