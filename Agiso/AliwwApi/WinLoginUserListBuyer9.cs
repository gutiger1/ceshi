using System;
using Agiso.Windows;

namespace Agiso.AliwwApi
{
	// Token: 0x02000716 RID: 1814
	public class WinLoginUserListBuyer9 : WindowInfo, IWinValid
	{
		// Token: 0x060023EC RID: 9196 RVA: 0x0005F5C0 File Offset: 0x0005D7C0
		public static WinLoginUserListBuyer9 Get(int processId)
		{
			return WinFindableBase.Get<WinLoginUserListBuyer9>(new WindowStruct("#32770", ""), processId);
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x0005F5E8 File Offset: 0x0005D7E8
		public WindowInfo WinUserList
		{
			get
			{
				if (this.a == null)
				{
					this.a = base.FindWindowInDescendant("WWUI.SuperListView", "", false, new bool?(false));
				}
				return this.a;
			}
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x0005F628 File Offset: 0x0005D828
		public bool IsValid()
		{
			WindowInfo windowInfo = base.FindWindowInDescendant("WWUI.SuperListView", "", false, new bool?(false));
			return windowInfo != null && windowInfo.HWnd != IntPtr.Zero;
		}

		// Token: 0x04001DE1 RID: 7649
		private WindowInfo a;
	}
}
