using System;

namespace Agiso.Windows
{
	// Token: 0x020006F8 RID: 1784
	[Flags]
	public enum ProcessAccessFlags : uint
	{
		// Token: 0x04001D65 RID: 7525
		All = 2035711U,
		// Token: 0x04001D66 RID: 7526
		Terminate = 1U,
		// Token: 0x04001D67 RID: 7527
		CreateThread = 2U,
		// Token: 0x04001D68 RID: 7528
		flag_3 = 8U,
		// Token: 0x04001D69 RID: 7529
		VMRead = 16U,
		// Token: 0x04001D6A RID: 7530
		VMWrite = 32U,
		// Token: 0x04001D6B RID: 7531
		DupHandle = 64U,
		// Token: 0x04001D6C RID: 7532
		SetInformation = 512U,
		// Token: 0x04001D6D RID: 7533
		QueryInformation = 1024U,
		// Token: 0x04001D6E RID: 7534
		Synchronize = 1048576U
	}
}
