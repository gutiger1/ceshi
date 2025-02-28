using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi.MsWin
{
	// Token: 0x02000738 RID: 1848
	public class WinShellTrayWnd : WinNotifyWndBase, IWinValid
	{
		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060024B1 RID: 9393 RVA: 0x000638D8 File Offset: 0x00061AD8
		public WindowInfo ShowOverflowBtn
		{
			get
			{
				if (this.a == null)
				{
					this.a = base.FindWindowInDescendant("Button", "", false, new bool?(false));
				}
				return this.a;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x00063918 File Offset: 0x00061B18
		public override WindowInfo NotifyAreaWin
		{
			get
			{
				if (this.b == null)
				{
					this.b = base.FindWindowInDescendant("ToolbarWindow32", "用户升级的通知区域", false, new bool?(false));
				}
				return this.b;
			}
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x00063958 File Offset: 0x00061B58
		public static List<WinShellTrayWnd> GetList()
		{
			return WinFindableBase.GetList<WinShellTrayWnd>(new WindowStruct("Shell_TrayWnd", ""), 0, false);
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x00063980 File Offset: 0x00061B80
		public bool IsValid()
		{
			return this.NotifyAreaWin != null && this.ShowOverflowBtn != null;
		}

		// Token: 0x04001E51 RID: 7761
		private WindowInfo a;

		// Token: 0x04001E52 RID: 7762
		private WindowInfo b;
	}
}
