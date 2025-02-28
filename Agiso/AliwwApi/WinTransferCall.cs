using System;
using System.Collections.Generic;
using System.Threading;
using Agiso.Extensions;
using Agiso.Handler;
using Agiso.Object;
using Agiso.Windows;

namespace Agiso.AliwwApi
{
	// Token: 0x02000721 RID: 1825
	public class WinTransferCall : WindowInfo, IWinTransferCall
	{
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x0600243E RID: 9278 RVA: 0x000623B4 File Offset: 0x000605B4
		private WindowInfo BtnTransferToPeople
		{
			get
			{
				if (this.a == null)
				{
					List<WindowInfo> list = Win32Extend.FindWindowsInDescendant(base.HWnd, new FindWindowOption
					{
						IsOnlyFirst = true
					}, "StandardButton", "转发到人", new bool?(false));
					if (list != null && list.Count > 0)
					{
						this.a = list[0];
					}
				}
				return this.a;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x0006241C File Offset: 0x0006061C
		private WindowInfo BtnTransferToGroup
		{
			get
			{
				if (this.b == null)
				{
					List<WindowInfo> list = Win32Extend.FindWindowsInDescendant(base.HWnd, new FindWindowOption
					{
						IsOnlyFirst = true
					}, "StandardButton", "转发到组", new bool?(false));
					if (list != null && list.Count > 0)
					{
						this.b = list[0];
					}
				}
				return this.b;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x00062484 File Offset: 0x00060684
		private WinTxtInput FindInput
		{
			get
			{
				if (this.c == null)
				{
					for (int i = 0; i < 10; i++)
					{
						IntPtr childHandleByClassTreePath = Win32Extend.GetChildHandleByClassTreePath(base.HWnd, new string[] { "StandardWindow", "EditComponent" });
						WindowInfo windowFromHandler = WindowInfo.GetWindowFromHandler(childHandleByClassTreePath);
						if (windowFromHandler != null)
						{
							this.c = windowFromHandler.Convert<WinTxtInput>();
							break;
						}
						Thread.Sleep(100);
					}
				}
				return this.c;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x000624FC File Offset: 0x000606FC
		private WindowInfo SuperListView
		{
			get
			{
				if (this.d == null)
				{
					WindowInfo childWindow = base.GetChildWindow("WWUI.SuperListView", null, IntPtr.Zero, 0);
					if (childWindow != null)
					{
						this.d = childWindow;
					}
				}
				return this.d;
			}
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x00062540 File Offset: 0x00060740
		public static WinTransferCall Find()
		{
			List<WindowInfo> windowListByClass = Win32Extend.GetWindowListByClass("PopupWindow");
			WinTransferCall winTransferCall;
			if (windowListByClass == null || windowListByClass.Count <= 0)
			{
				winTransferCall = null;
			}
			else
			{
				foreach (WindowInfo windowInfo in windowListByClass)
				{
					if (WinTransferCall.ValidTree(windowInfo))
					{
						return windowInfo.Convert<WinTransferCall>();
					}
				}
				winTransferCall = null;
			}
			return winTransferCall;
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000625C4 File Offset: 0x000607C4
		public static bool ValidTree(WindowInfo win)
		{
			WindowInfo childWindow = win.GetChildWindow("StandardButton", "批量转发", IntPtr.Zero, 0);
			return childWindow != null && childWindow.HWnd != IntPtr.Zero;
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x0006260C File Offset: 0x0006080C
		public ErrCodeInfo TransferCall(string manualNick)
		{
			if (this.BtnTransferToPeople != null)
			{
				this.BtnTransferToPeople.Click(true);
			}
			Thread.Sleep(100);
			if (!string.IsNullOrEmpty(manualNick) && this.FindInput != null)
			{
				this.FindInput.SetText(manualNick);
			}
			Thread.Sleep(300);
			base.Click(103, 103, true);
			Thread.Sleep(300);
			WinTransferCall winTransferCall = WinTransferCall.Find();
			ErrCodeInfo errCodeInfo;
			if (winTransferCall != null && winTransferCall.HWnd != IntPtr.Zero && winTransferCall.SuperListView != null && winTransferCall.SuperListView.HWnd != IntPtr.Zero)
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.TransferCallFailed);
			}
			else
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.TransferCallFinish);
			}
			return errCodeInfo;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000626D4 File Offset: 0x000608D4
		public ErrCodeInfo TransferCallToGroup(int idx = 1)
		{
			if (idx < 1)
			{
				idx = 1;
			}
			if (idx > 8)
			{
				idx = 8;
			}
			if (this.BtnTransferToGroup != null)
			{
				this.BtnTransferToGroup.Click(true);
			}
			Thread.Sleep(300);
			base.Click(47, 69 + (idx - 1) * 23, true);
			Thread.Sleep(300);
			WinTransferCall winTransferCall = WinTransferCall.Find();
			ErrCodeInfo errCodeInfo;
			if (winTransferCall != null && winTransferCall.HWnd != IntPtr.Zero && winTransferCall.SuperListView != null && winTransferCall.SuperListView.HWnd != IntPtr.Zero)
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.TransferCallFailed);
			}
			else
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.TransferCallFinish);
			}
			return errCodeInfo;
		}

		// Token: 0x04001E01 RID: 7681
		private WindowInfo a;

		// Token: 0x04001E02 RID: 7682
		private WindowInfo b;

		// Token: 0x04001E03 RID: 7683
		private WinTxtInput c = null;

		// Token: 0x04001E04 RID: 7684
		private WindowInfo d = null;
	}
}
