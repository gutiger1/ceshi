using System;

namespace Agiso.Windows
{
	// Token: 0x020006CD RID: 1741
	public struct STARTUPINFO
	{
		// Token: 0x04001C39 RID: 7225
		public int cb;

		// Token: 0x04001C3A RID: 7226
		public string lpReserved;

		// Token: 0x04001C3B RID: 7227
		public string lpDesktop;

		// Token: 0x04001C3C RID: 7228
		public string lpTitle;

		// Token: 0x04001C3D RID: 7229
		public int dwX;

		// Token: 0x04001C3E RID: 7230
		public int dwY;

		// Token: 0x04001C3F RID: 7231
		public int dwXSize;

		// Token: 0x04001C40 RID: 7232
		public int dwYSize;

		// Token: 0x04001C41 RID: 7233
		public int dwXCountChars;

		// Token: 0x04001C42 RID: 7234
		public int dwYCountChars;

		// Token: 0x04001C43 RID: 7235
		public int dwFillAttribute;

		// Token: 0x04001C44 RID: 7236
		public int dwFlags;

		// Token: 0x04001C45 RID: 7237
		public int wShowWindow;

		// Token: 0x04001C46 RID: 7238
		public int cbReserved2;

		// Token: 0x04001C47 RID: 7239
		public byte lpReserved2;

		// Token: 0x04001C48 RID: 7240
		public IntPtr hStdInput;

		// Token: 0x04001C49 RID: 7241
		public IntPtr htdOutput;

		// Token: 0x04001C4A RID: 7242
		public IntPtr hStdError;
	}
}
