using System;

namespace Agiso.Windows
{
	// Token: 0x020006C4 RID: 1732
	public struct MEMORYSTATUS
	{
		// Token: 0x04001BBD RID: 7101
		public uint dwLength;

		// Token: 0x04001BBE RID: 7102
		public uint dwMemoryLoad;

		// Token: 0x04001BBF RID: 7103
		public uint dwTotalPhys;

		// Token: 0x04001BC0 RID: 7104
		public uint dwAvailPhys;

		// Token: 0x04001BC1 RID: 7105
		public uint dwTotalPageFile;

		// Token: 0x04001BC2 RID: 7106
		public uint dwAvailPageFile;

		// Token: 0x04001BC3 RID: 7107
		public uint dwTotalVirtual;

		// Token: 0x04001BC4 RID: 7108
		public uint dwAvailVirtual;
	}
}
