using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Agiso.Extensions;
using Agiso.Utils;
using Agiso.Windows;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x02000748 RID: 1864
	public abstract class WinLoginQnBase : WinChromeContainer, IWinValid
	{
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x00067F54 File Offset: 0x00066154
		public WindowInfo LoginAreaWin
		{
			get
			{
				if (this.a == null)
				{
					this.a = base.FindWindowInDescendant("AuthSDKMainUI", "LoginUI", false, new bool?(false));
				}
				return this.a;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x00067F94 File Offset: 0x00066194
		public WindowInfo SecurityChromeWin
		{
			get
			{
				return this.LoginAreaWin.FindWindowInDescendant("CefBrowserWindow", "", false, new bool?(false));
			}
		}

		// Token: 0x06002527 RID: 9511
		public abstract Bitmap CaptureWindowErrorMsg();

		// Token: 0x06002528 RID: 9512
		public abstract Bitmap CaptureWindowNotErrorMsg();

		// Token: 0x06002529 RID: 9513
		public abstract Bitmap CaptureWindowPicValidCode();

		// Token: 0x0600252A RID: 9514
		public abstract Bitmap CaptureWindowPwdInput();

		// Token: 0x0600252B RID: 9515
		public abstract Bitmap CaptureWindowRememberPwd();

		// Token: 0x0600252C RID: 9516
		public abstract void ClickBlank();

		// Token: 0x0600252D RID: 9517
		public abstract void ClickDrowUserList();

		// Token: 0x0600252E RID: 9518
		public abstract void ClickLoginBt();

		// Token: 0x0600252F RID: 9519
		public abstract void ClickRememberPwdCheckbox();

		// Token: 0x06002530 RID: 9520
		public abstract void ClickReturnButton();

		// Token: 0x06002531 RID: 9521
		public abstract void ClickSecurityRefresh();

		// Token: 0x06002532 RID: 9522
		public abstract void ClickSelectSellerType();

		// Token: 0x06002533 RID: 9523
		public abstract void InputNick(string nick, int retryTimes = 1);

		// Token: 0x06002534 RID: 9524
		public abstract void InputPassword(string pw);

		// Token: 0x06002535 RID: 9525 RVA: 0x00067FC0 File Offset: 0x000661C0
		public bool? IsPwdRememberAutoInputed()
		{
			bool? flag;
			using (Bitmap bitmap = this.CaptureWindowPwdInput())
			{
				string text = Util.ComputeHashMd5(bitmap);
				if (this.IsPwdInputed(text))
				{
					flag = new bool?(true);
				}
				else if (this.IsPwdUnInput(text))
				{
					flag = new bool?(false);
				}
				else
				{
					Util.CollectPicMd5(bitmap, "是否已输密码_");
					flag = null;
				}
			}
			return flag;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x00068034 File Offset: 0x00066234
		public bool? IsRememberPwdChecked()
		{
			int num = 0;
			bool? flag;
			for (;;)
			{
				this.ClickBlank();
				using (Bitmap bitmap = this.CaptureWindowRememberPwd())
				{
					string text = Util.ComputeHashMd5(bitmap);
					if (this.IsRemeberPwdChecked(text))
					{
						flag = new bool?(true);
						break;
					}
					if (this.IsRemeberPwdUnChecked(text))
					{
						flag = new bool?(false);
						break;
					}
					if (!this.IsRemeberPwdCheckedUnknow(text) || num++ >= 3)
					{
						Util.CollectPicMd5(bitmap, "是否已勾选记住密码_");
						flag = null;
						break;
					}
					Thread.Sleep(100);
				}
			}
			return flag;
		}

		// Token: 0x06002537 RID: 9527
		public abstract QnLoginPageType GetCurrPage();

		// Token: 0x06002538 RID: 9528 RVA: 0x000680DC File Offset: 0x000662DC
		public bool IsValid()
		{
			return base.Visible && this.LoginAreaWin != null;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00068100 File Offset: 0x00066300
		public static List<WinLoginQnBase> GetList()
		{
			List<WinLoginQnBase> list3;
			if (AppConfig.AgentSettings.QnVersion == 1)
			{
				List<WinLoginQn91500> list = WinLoginQn91500.GetList();
				if (list != null)
				{
					list.ForEach(new Action<WinLoginQn91500>(WinLoginQnBase.<>c.<>9.a));
				}
				List<WinLoginQn> list2 = WinLoginQn.GetList();
				list3 = ((list2 != null) ? list2.ToList<WinLoginQnBase>() : null);
			}
			else
			{
				List<WinLoginQn> list4 = WinLoginQn.GetList();
				if (list4 != null)
				{
					list4.ForEach(new Action<WinLoginQn>(WinLoginQnBase.<>c.<>9.a));
				}
				List<WinLoginQn91500> list5 = WinLoginQn91500.GetList();
				list3 = ((list5 != null) ? list5.ToList<WinLoginQnBase>() : null);
			}
			return list3;
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000681A4 File Offset: 0x000663A4
		public static bool TryParse(IntPtr hwnd, out WinLoginQnBase result)
		{
			WindowInfo windowInfo = new WindowInfo(hwnd);
			bool flag2;
			if (windowInfo.Info.WindowName == "千牛登录")
			{
				WinLoginQn91500 winLoginQn;
				bool flag = windowInfo.TryConvert(out winLoginQn);
				result = winLoginQn;
				flag2 = flag;
			}
			else
			{
				WinLoginQn winLoginQn2;
				bool flag3 = windowInfo.TryConvert(out winLoginQn2);
				result = winLoginQn2;
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x0600253B RID: 9531
		public abstract bool IsAlreadyCollectedImageMd5(string md5);

		// Token: 0x0600253C RID: 9532
		public abstract bool IsPasswordError(string md5);

		// Token: 0x0600253D RID: 9533
		public abstract bool IsAccountHasLogin(string md5);

		// Token: 0x0600253E RID: 9534
		public abstract bool IsNoneHandleError(string md5);

		// Token: 0x0600253F RID: 9535
		public abstract bool IsAccountOrPasswordIsNull(string md5);

		// Token: 0x06002540 RID: 9536
		public abstract bool IsQnLoginErrorNeedToRestart(string md5);

		// Token: 0x06002541 RID: 9537
		public abstract bool IsAccountLimitLogin(string md5);

		// Token: 0x06002542 RID: 9538
		public abstract bool IsSpecialEditionLoginError(string md5);

		// Token: 0x06002543 RID: 9539
		public abstract bool IsRemeberPwdChecked(string md5);

		// Token: 0x06002544 RID: 9540
		public abstract bool IsRemeberPwdUnChecked(string md5);

		// Token: 0x06002545 RID: 9541
		public abstract bool IsRemeberPwdCheckedUnknow(string md5);

		// Token: 0x06002546 RID: 9542
		public abstract bool IsPwdInputed(string md5);

		// Token: 0x06002547 RID: 9543
		public abstract bool IsPwdUnInput(string md5);

		// Token: 0x04001EAD RID: 7853
		private WindowInfo a;
	}
}
