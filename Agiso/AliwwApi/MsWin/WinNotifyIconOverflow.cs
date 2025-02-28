using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi.MsWin
{
	// Token: 0x02000736 RID: 1846
	public class WinNotifyIconOverflow : WinNotifyWndBase, IWinValid
	{
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x000637E0 File Offset: 0x000619E0
		public override WindowInfo NotifyAreaWin
		{
			get
			{
				if (this.a == null)
				{
					this.a = base.FindWindowInDescendant("ToolbarWindow32", "溢出通知区域", false, new bool?(false));
				}
				return this.a;
			}
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x00063820 File Offset: 0x00061A20
		public static List<WinNotifyIconOverflow> GetList()
		{
			return WinFindableBase.GetList<WinNotifyIconOverflow>(new WindowStruct("NotifyIconOverflowWindow", ""), 0, false);
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x00063848 File Offset: 0x00061A48
		public bool IsValid()
		{
			return this.NotifyAreaWin != null;
		}

		// Token: 0x04001E50 RID: 7760
		private WindowInfo a;
	}
}
