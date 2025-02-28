using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006EC RID: 1772
	[StructLayout(LayoutKind.Explicit)]
	public struct INPUT
	{
		// Token: 0x04001D20 RID: 7456
		[FieldOffset(0)]
		public int type;

		// Token: 0x04001D21 RID: 7457
		[FieldOffset(4)]
		public MOUSEINPUT mi;

		// Token: 0x04001D22 RID: 7458
		[FieldOffset(4)]
		public KEYBDINPUT ki;

		// Token: 0x04001D23 RID: 7459
		[FieldOffset(4)]
		public HARDWAREINPUT hi;
	}
}
