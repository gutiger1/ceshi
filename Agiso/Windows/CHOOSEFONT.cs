using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006DB RID: 1755
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class CHOOSEFONT
	{
		// Token: 0x04001C99 RID: 7321
		public int lStructSize = Marshal.SizeOf(typeof(CHOOSEFONT));

		// Token: 0x04001C9A RID: 7322
		public IntPtr hwndOwner;

		// Token: 0x04001C9B RID: 7323
		public IntPtr hDC;

		// Token: 0x04001C9C RID: 7324
		public IntPtr lpLogFont;

		// Token: 0x04001C9D RID: 7325
		public int iPointSize;

		// Token: 0x04001C9E RID: 7326
		public int Flags;

		// Token: 0x04001C9F RID: 7327
		public int rgbColors;

		// Token: 0x04001CA0 RID: 7328
		public IntPtr lCustData = IntPtr.Zero;

		// Token: 0x04001CA1 RID: 7329
		public WndProc lpfnHook;

		// Token: 0x04001CA2 RID: 7330
		public string lpTemplateName;

		// Token: 0x04001CA3 RID: 7331
		public IntPtr hInstance;

		// Token: 0x04001CA4 RID: 7332
		public string lpszStyle;

		// Token: 0x04001CA5 RID: 7333
		public short nFontType;

		// Token: 0x04001CA6 RID: 7334
		public short ___MISSING_ALIGNMENT__;

		// Token: 0x04001CA7 RID: 7335
		public int nSizeMin;

		// Token: 0x04001CA8 RID: 7336
		public int nSizeMax;
	}
}
