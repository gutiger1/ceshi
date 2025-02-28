using System;

namespace Agiso.Windows
{
	// Token: 0x020006FA RID: 1786
	[Flags]
	public enum MemoryProtection
	{
		// Token: 0x04001D7A RID: 7546
		Execute = 16,
		// Token: 0x04001D7B RID: 7547
		ExecuteRead = 32,
		// Token: 0x04001D7C RID: 7548
		ExecuteReadWrite = 64,
		// Token: 0x04001D7D RID: 7549
		ExecuteWriteCopy = 128,
		// Token: 0x04001D7E RID: 7550
		NoAccess = 1,
		// Token: 0x04001D7F RID: 7551
		ReadOnly = 2,
		// Token: 0x04001D80 RID: 7552
		ReadWrite = 4,
		// Token: 0x04001D81 RID: 7553
		WriteCopy = 8,
		// Token: 0x04001D82 RID: 7554
		GuardModifierflag = 256,
		// Token: 0x04001D83 RID: 7555
		NoCacheModifierflag = 512,
		// Token: 0x04001D84 RID: 7556
		WriteCombineModifierflag = 1024
	}
}
