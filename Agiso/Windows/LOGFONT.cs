using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006DC RID: 1756
	[StructLayout(LayoutKind.Sequential)]
	public class LOGFONT
	{
		// Token: 0x04001CA9 RID: 7337
		public const int LF_FACESIZE = 32;

		// Token: 0x04001CAA RID: 7338
		public int lfHeight;

		// Token: 0x04001CAB RID: 7339
		public int lfWidth;

		// Token: 0x04001CAC RID: 7340
		public int lfEscapement;

		// Token: 0x04001CAD RID: 7341
		public int lfOrientation;

		// Token: 0x04001CAE RID: 7342
		public int lfWeight;

		// Token: 0x04001CAF RID: 7343
		public byte lfItalic;

		// Token: 0x04001CB0 RID: 7344
		public byte lfUnderline;

		// Token: 0x04001CB1 RID: 7345
		public byte lfStrikeOut;

		// Token: 0x04001CB2 RID: 7346
		public byte lfCharSet;

		// Token: 0x04001CB3 RID: 7347
		public byte lfOutPrecision;

		// Token: 0x04001CB4 RID: 7348
		public byte lfClipPrecision;

		// Token: 0x04001CB5 RID: 7349
		public byte lfQuality;

		// Token: 0x04001CB6 RID: 7350
		public byte lfPitchAndFamily;

		// Token: 0x04001CB7 RID: 7351
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string lfFaceName;
	}
}
