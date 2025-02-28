using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x02000757 RID: 1879
	public class WinCloseTip : WindowInfo, IWinValid
	{
		// Token: 0x060025AA RID: 9642 RVA: 0x0006D290 File Offset: 0x0006B490
		public static List<WinCloseTip> GetList(int processId = 0)
		{
			return WinFindableBase.GetList<WinCloseTip>(new WindowStruct("#32770", "关闭提醒"), processId, false);
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060025AB RID: 9643 RVA: 0x0006D2B8 File Offset: 0x0006B4B8
		public WindowInfo BtnSure
		{
			get
			{
				if (this.a == null)
				{
					IntPtr intPtr = WindowsAPI.FindWindowEx(base.HWnd, IntPtr.Zero, "StandardButton", "确定");
					if (intPtr != IntPtr.Zero)
					{
						this.a = new WindowInfo(intPtr);
					}
				}
				return this.a;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x0006D310 File Offset: 0x0006B510
		public WindowInfo BtnCancel
		{
			get
			{
				if (this.b == null)
				{
					IntPtr intPtr = WindowsAPI.FindWindowEx(base.HWnd, IntPtr.Zero, "StandardButton", "取消");
					if (intPtr != IntPtr.Zero)
					{
						this.b = new WindowInfo(intPtr);
					}
				}
				return this.b;
			}
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x0006D368 File Offset: 0x0006B568
		public bool IsValid()
		{
			return this.BtnSure != null && this.BtnSure.Visible && this.BtnCancel != null && this.BtnCancel.Visible;
		}

		// Token: 0x04001EFC RID: 7932
		private WindowInfo a;

		// Token: 0x04001EFD RID: 7933
		private WindowInfo b;
	}
}
