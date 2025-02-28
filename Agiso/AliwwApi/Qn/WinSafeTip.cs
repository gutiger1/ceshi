using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x0200074A RID: 1866
	public class WinSafeTip : WinSafeTipBase, IWinValid
	{
		// Token: 0x0600254D RID: 9549 RVA: 0x000681F8 File Offset: 0x000663F8
		public new static List<WinSafeTip> GetList(int processId = 0)
		{
			return WinFindableBase.GetList<WinSafeTip>(new WindowStruct("StandardFrame", "安全提示"), processId, false);
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x00068220 File Offset: 0x00066420
		public bool IsValid()
		{
			return this.ChromWin != null;
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x0000F154 File Offset: 0x0000D354
		public override WindowInfo ChromWin
		{
			get
			{
				return base.FindWindowInDescendant("PrivateWebCtrl", "", false, new bool?(false));
			}
		}
	}
}
