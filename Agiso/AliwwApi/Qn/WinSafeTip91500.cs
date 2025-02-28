using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x0200074E RID: 1870
	public class WinSafeTip91500 : WinSafeTipBase, IWinValid
	{
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x0000F1A9 File Offset: 0x0000D3A9
		public override WindowInfo ChromWin
		{
			get
			{
				return base.FindWindowInDescendant("CefBrowserWindow", "", false, new bool?(false));
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x00068518 File Offset: 0x00066718
		public new static List<WinSafeTip91500> GetList(int processId = 0)
		{
			return WinFindableBase.GetList<WinSafeTip91500>(new WindowStruct("Qt5152QWindowIcon", "千牛"), processId, false);
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x00068540 File Offset: 0x00066740
		public bool IsValid()
		{
			WindowInfo windowInfo = base.FindWindowInDescendant("Qt5152QWindowIcon", "千牛工作台", false, new bool?(false));
			return windowInfo != null && windowInfo.FindWindowInDescendant("CefBrowserWindow", "", false, new bool?(false)) != null;
		}
	}
}
