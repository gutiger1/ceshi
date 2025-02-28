using System;
using System.Collections.Generic;
using System.Threading;
using Agiso.Extensions;
using Agiso.Handler;
using Agiso.Object;
using Agiso.Utils;
using Agiso.Windows;

namespace Agiso.AliwwApi
{
	// Token: 0x02000725 RID: 1829
	public class WinTransferCallCoolMenu : WindowInfo, IWinTransferCall
	{
		// Token: 0x0600244B RID: 9291 RVA: 0x000628B8 File Offset: 0x00060AB8
		public static WinTransferCallCoolMenu Find()
		{
			List<WindowInfo> windowListByClass = Win32Extend.GetWindowListByClass("coolmenu");
			WinTransferCallCoolMenu winTransferCallCoolMenu;
			if (windowListByClass == null || windowListByClass.Count <= 0)
			{
				winTransferCallCoolMenu = null;
			}
			else
			{
				foreach (WindowInfo windowInfo in windowListByClass)
				{
					if (WinTransferCallCoolMenu.ValidTree(windowInfo))
					{
						return windowInfo.Convert<WinTransferCallCoolMenu>();
					}
				}
				winTransferCallCoolMenu = null;
			}
			return winTransferCallCoolMenu;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x0003A434 File Offset: 0x00038634
		public static bool ValidTree(WindowInfo win)
		{
			return true;
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x0006293C File Offset: 0x00060B3C
		public ErrCodeInfo TransferCall(string buyerNick)
		{
			int num = Util.ToInt(buyerNick);
			if (num < 1)
			{
				num = 1;
			}
			if (num > 200)
			{
				num = 200;
			}
			base.Click(103, 3 + 24 * num, true);
			Thread.Sleep(300);
			WinTransferCallCoolMenu winTransferCallCoolMenu = WinTransferCallCoolMenu.Find();
			ErrCodeInfo errCodeInfo;
			if (winTransferCallCoolMenu != null)
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.TransferCallFailed);
			}
			else
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.TransferCallFinish);
			}
			return errCodeInfo;
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x0000EDE7 File Offset: 0x0000CFE7
		public ErrCodeInfo TransferCallToGroup(int idx = 1)
		{
			throw new Exception("旺旺2013卖家版不支持转发到组。");
		}
	}
}
