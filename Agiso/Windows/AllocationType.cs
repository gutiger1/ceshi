using System;

namespace Agiso.Windows
{
	// Token: 0x020006F9 RID: 1785
	[Flags]
	public enum AllocationType
	{
		// Token: 0x04001D70 RID: 7536
		Commit = 4096,
		// Token: 0x04001D71 RID: 7537
		Reserve = 8192,
		// Token: 0x04001D72 RID: 7538
		Decommit = 16384,
		// Token: 0x04001D73 RID: 7539
		Release = 32768,
		// Token: 0x04001D74 RID: 7540
		Reset = 524288,
		// Token: 0x04001D75 RID: 7541
		Physical = 4194304,
		// Token: 0x04001D76 RID: 7542
		TopDown = 1048576,
		// Token: 0x04001D77 RID: 7543
		WriteWatch = 2097152,
		// Token: 0x04001D78 RID: 7544
		LargePages = 536870912
	}
}
