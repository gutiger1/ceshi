using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006F7 RID: 1783
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class TBBUTTON
	{
		// Token: 0x04001D5F RID: 7519
		public int iBitmap;

		// Token: 0x04001D60 RID: 7520
		public int idCommand;

		// Token: 0x04001D61 RID: 7521
		public IntPtr fsStateStylePadding;

		// Token: 0x04001D62 RID: 7522
		public IntPtr dwData;

		// Token: 0x04001D63 RID: 7523
		public IntPtr iString;
	}
}
