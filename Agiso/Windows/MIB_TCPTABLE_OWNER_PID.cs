using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006F5 RID: 1781
	public struct MIB_TCPTABLE_OWNER_PID
	{
		// Token: 0x04001D57 RID: 7511
		public uint dwNumEntries;

		// Token: 0x04001D58 RID: 7512
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
		public MIB_TCPROW_OWNER_PID[] table;
	}
}
