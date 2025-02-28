using System;

namespace Agiso.Windows
{
	// Token: 0x020006BB RID: 1723
	public struct Rectangle
	{
		// Token: 0x06002316 RID: 8982 RVA: 0x00059CF4 File Offset: 0x00057EF4
		public int GetWidth()
		{
			return this.Right - this.Left;
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x00059D10 File Offset: 0x00057F10
		public int GetHeight()
		{
			return this.Bottom - this.Top;
		}

		// Token: 0x04001B7D RID: 7037
		public int Left;

		// Token: 0x04001B7E RID: 7038
		public int Top;

		// Token: 0x04001B7F RID: 7039
		public int Right;

		// Token: 0x04001B80 RID: 7040
		public int Bottom;
	}
}
