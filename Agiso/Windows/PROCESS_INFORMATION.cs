using System;

namespace Agiso.Windows
{
	// Token: 0x020006D0 RID: 1744
	public struct PROCESS_INFORMATION
	{
		// Token: 0x04001C5D RID: 7261
		public IntPtr hProcess;

		// Token: 0x04001C5E RID: 7262
		public IntPtr hThread;

		// Token: 0x04001C5F RID: 7263
		public uint dwProcessId;

		// Token: 0x04001C60 RID: 7264
		public uint dwThreadId;
	}
}
