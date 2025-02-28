using System;
using System.Collections.Generic;
using System.Threading;
using Agiso.Windows;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x0200075B RID: 1883
	public class WinFloat : WinFloatBase, IWinValid
	{
		// Token: 0x060025CD RID: 9677 RVA: 0x0006E610 File Offset: 0x0006C810
		public new static List<WinFloat> GetList()
		{
			return WinFindableBase.GetList<WinFloat>(new WindowStruct("StandardFrame", ""), 0, false);
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x0006E638 File Offset: 0x0006C838
		public override void CallAliwwTalkWin()
		{
			WindowInfo windowInfo = base.FindWindowInDescendant("ToolBarPlus", "", false, new bool?(false));
			if (windowInfo != null)
			{
				windowInfo.Click(10, 10, true);
				Thread.Sleep(100);
			}
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x0006E678 File Offset: 0x0006C878
		public bool IsValid()
		{
			WindowInfo windowInfo = base.FindWindowInDescendant("ToolBarPlus", "", false, new bool?(false));
			WindowInfo windowInfo2 = base.FindWindowInDescendant("StandardButton", "", false, new bool?(false));
			return windowInfo != null && windowInfo2 != null && windowInfo.Visible && windowInfo2.Visible;
		}
	}
}
