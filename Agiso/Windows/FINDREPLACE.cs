using System;

namespace Agiso.Windows
{
	// Token: 0x020006D8 RID: 1752
	public struct FINDREPLACE
	{
		// Token: 0x04001C85 RID: 7301
		public int lStructSize;

		// Token: 0x04001C86 RID: 7302
		public IntPtr hwndOwner;

		// Token: 0x04001C87 RID: 7303
		public IntPtr hInstance;

		// Token: 0x04001C88 RID: 7304
		public int Flags;

		// Token: 0x04001C89 RID: 7305
		public string lpstrFindWhat;

		// Token: 0x04001C8A RID: 7306
		public string lpstrReplaceWith;

		// Token: 0x04001C8B RID: 7307
		public ushort wFindWhatLen;

		// Token: 0x04001C8C RID: 7308
		public ushort wReplaceWithLen;

		// Token: 0x04001C8D RID: 7309
		public uint lCustData;

		// Token: 0x04001C8E RID: 7310
		public FRHookProc lpfnHook;

		// Token: 0x04001C8F RID: 7311
		public string lpTemplateName;
	}
}
