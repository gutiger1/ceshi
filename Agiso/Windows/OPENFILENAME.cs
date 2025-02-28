using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006CC RID: 1740
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class OPENFILENAME
	{
		// Token: 0x04001C22 RID: 7202
		public int structSize = 0;

		// Token: 0x04001C23 RID: 7203
		public IntPtr dlgOwner = IntPtr.Zero;

		// Token: 0x04001C24 RID: 7204
		public IntPtr instance = IntPtr.Zero;

		// Token: 0x04001C25 RID: 7205
		public string filter = null;

		// Token: 0x04001C26 RID: 7206
		public string customFilter = null;

		// Token: 0x04001C27 RID: 7207
		public int maxCustFilter = 0;

		// Token: 0x04001C28 RID: 7208
		public int filterIndex = 0;

		// Token: 0x04001C29 RID: 7209
		public string file = null;

		// Token: 0x04001C2A RID: 7210
		public int maxFile = 0;

		// Token: 0x04001C2B RID: 7211
		public string fileTitle = null;

		// Token: 0x04001C2C RID: 7212
		public int maxFileTitle = 0;

		// Token: 0x04001C2D RID: 7213
		public string initialDir = null;

		// Token: 0x04001C2E RID: 7214
		public string title = null;

		// Token: 0x04001C2F RID: 7215
		public int flags = 0;

		// Token: 0x04001C30 RID: 7216
		public short fileOffset = 0;

		// Token: 0x04001C31 RID: 7217
		public short fileExtension = 0;

		// Token: 0x04001C32 RID: 7218
		public string defExt = null;

		// Token: 0x04001C33 RID: 7219
		public IntPtr custData = IntPtr.Zero;

		// Token: 0x04001C34 RID: 7220
		public IntPtr hook = IntPtr.Zero;

		// Token: 0x04001C35 RID: 7221
		public string templateName = null;

		// Token: 0x04001C36 RID: 7222
		public IntPtr reservedPtr = IntPtr.Zero;

		// Token: 0x04001C37 RID: 7223
		public int reservedInt = 0;

		// Token: 0x04001C38 RID: 7224
		public int flagsEx = 0;
	}
}
