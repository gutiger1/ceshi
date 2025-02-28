using System;
using System.Runtime.InteropServices;

namespace Agiso.Windows
{
	// Token: 0x020006C5 RID: 1733
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct TokPriv1Luid
	{
		// Token: 0x04001BC5 RID: 7109
		public int Count;

		// Token: 0x04001BC6 RID: 7110
		public long Luid;

		// Token: 0x04001BC7 RID: 7111
		public int Attr;
	}
}
