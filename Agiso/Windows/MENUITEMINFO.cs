using System;

namespace Agiso.Windows
{
	// Token: 0x020006DE RID: 1758
	public struct MENUITEMINFO
	{
		// Token: 0x04001CBF RID: 7359
		public uint cbSize;

		// Token: 0x04001CC0 RID: 7360
		public uint fMask;

		// Token: 0x04001CC1 RID: 7361
		public uint fType;

		// Token: 0x04001CC2 RID: 7362
		public uint fState;

		// Token: 0x04001CC3 RID: 7363
		public int wID;

		// Token: 0x04001CC4 RID: 7364
		public int hSubMenu;

		// Token: 0x04001CC5 RID: 7365
		public int hbmpChecked;

		// Token: 0x04001CC6 RID: 7366
		public int hbmpUnchecked;

		// Token: 0x04001CC7 RID: 7367
		public int dwItemData;

		// Token: 0x04001CC8 RID: 7368
		public IntPtr dwTypeData;

		// Token: 0x04001CC9 RID: 7369
		public uint cch;
	}
}
