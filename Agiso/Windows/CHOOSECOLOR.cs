using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006DA RID: 1754
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class CHOOSECOLOR
	{
		// Token: 0x04001C90 RID: 7312
		public int lStructSize = Marshal.SizeOf(typeof(CHOOSECOLOR));

		// Token: 0x04001C91 RID: 7313
		public IntPtr hwndOwner;

		// Token: 0x04001C92 RID: 7314
		public IntPtr hInstance;

		// Token: 0x04001C93 RID: 7315
		public int rgbResult;

		// Token: 0x04001C94 RID: 7316
		public IntPtr lpCustColors;

		// Token: 0x04001C95 RID: 7317
		public int Flags;

		// Token: 0x04001C96 RID: 7318
		public IntPtr lCustData = IntPtr.Zero;

		// Token: 0x04001C97 RID: 7319
		public WndProc lpfnHook;

		// Token: 0x04001C98 RID: 7320
		public string lpTemplateName;
	}
}
