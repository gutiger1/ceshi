using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Agiso.Handler;
using Agiso.Utils;
using Agiso.Windows;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x0200075A RID: 1882
	public class WinQnStopWork : WindowInfo, IWinValid
	{
		// Token: 0x060025C6 RID: 9670 RVA: 0x0006E434 File Offset: 0x0006C634
		public static List<WinQnStopWork> GetList()
		{
			return WinFindableBase.GetList<WinQnStopWork>(new WindowStruct("#32770", "千牛工作台"), 0, false);
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x0006E45C File Offset: 0x0006C65C
		private void b()
		{
			if (this.CloseBtn != null)
			{
				this.CloseBtn.Click(true);
			}
			else
			{
				base.Close(true);
			}
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0006E48C File Offset: 0x0006C68C
		public static bool CloseAll(int processId = 0)
		{
			bool flag = false;
			List<WinQnStopWork> list = WinQnStopWork.GetList();
			if (list != null)
			{
				foreach (WinQnStopWork winQnStopWork in list)
				{
					if (processId > 0 && !flag && winQnStopWork.ProcessId == processId)
					{
						flag = true;
					}
					using (Bitmap bitmapFromDC = winQnStopWork.GetBitmapFromDC(0))
					{
						LogWriter.WriteLog("已停止工作的窗口已截图，请去后台关注", 1);
						Util.CollectPicMd5(bitmapFromDC, "已停止工作_");
					}
					winQnStopWork.b();
					Win32Extend.KillProcessById(winQnStopWork.ProcessId, null);
				}
			}
			return flag;
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x0000F38D File Offset: 0x0000D58D
		// (set) Token: 0x060025CA RID: 9674 RVA: 0x0000F395 File Offset: 0x0000D595
		private WindowInfo CloseBtn { get; set; }

		// Token: 0x060025CB RID: 9675 RVA: 0x0006E554 File Offset: 0x0006C754
		public bool IsValid()
		{
			bool flag = false;
			bool flag2;
			try
			{
				WindowInfo windowInfo = base.FindWindowInDescendant("DirectUIHWND", "", false, new bool?(true));
				WindowInfo windowInfo2;
				if (windowInfo != null)
				{
					if ((windowInfo2 = windowInfo.FindWindowInDescendant("Button", "关闭程序", true, new bool?(true))) != null)
					{
						goto IL_0058;
					}
				}
				windowInfo2 = ((windowInfo != null) ? windowInfo.FindWindowInDescendant("Button", null, false, new bool?(true)) : null);
				IL_0058:
				WindowInfo windowInfo3 = windowInfo2;
				if (windowInfo3 != null)
				{
					this.CloseBtn = windowInfo3;
					flag = true;
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			finally
			{
				if (!flag)
				{
					LogWriter.WriteLog("千牛工作台：" + base.GetTreeNode().WriteTreeNode(""), 1);
				}
			}
			return flag2;
		}

		// Token: 0x04001F01 RID: 7937
		[CompilerGenerated]
		private WindowInfo a;
	}
}
