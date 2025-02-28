using System;
using System.Threading;
using System.Windows.Forms;
using Agiso.Windows;

namespace Agiso.AliwwApi
{
	// Token: 0x02000715 RID: 1813
	public class WinLoginMobileCaptchaBuyer9 : WinChromeContainer
	{
		// Token: 0x060023E3 RID: 9187 RVA: 0x0005F470 File Offset: 0x0005D670
		public static WinLoginMobileCaptchaBuyer9 Get()
		{
			return WinFindableBase.Get<WinLoginMobileCaptchaBuyer9>(new WindowStruct("#32770", "安全验证"), 0);
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x0005F498 File Offset: 0x0005D698
		public WindowInfo OkBtn
		{
			get
			{
				if (this.a == null)
				{
					this.a = base.FindWindowInDescendant("StandardButton", "确定", false, new bool?(false));
				}
				return this.a;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x0005F4D8 File Offset: 0x0005D6D8
		public WindowInfo Input
		{
			get
			{
				if (this.b == null)
				{
					this.b = base.FindWindowInDescendant("SearchInput", "", false, new bool?(false));
				}
				return this.b;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x0005F518 File Offset: 0x0005D718
		public WindowInfo SendCaptchaBtn
		{
			get
			{
				if (this.c == null)
				{
					this.c = base.FindWindowInDescendant("StandardButton", "发送验证码", false, new bool?(false));
				}
				return this.c;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x0005F558 File Offset: 0x0005D758
		public WindowInfo MobileDropbox
		{
			get
			{
				if (this.d == null)
				{
					this.d = base.FindWindowInDescendant("ComboBoxEx", null, false, new bool?(false));
				}
				return this.d;
			}
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x0000ECD6 File Offset: 0x0000CED6
		public void SubmitCaptcha(string captcha)
		{
			this.Input.SetText(captcha);
			Thread.Sleep(50);
			this.OkBtn.Click(15, 15, true);
			Application.DoEvents();
			Thread.Sleep(2000);
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x0000ED0A File Offset: 0x0000CF0A
		public void GetCaptcha()
		{
			this.SendCaptchaBtn.Click(15, 15, true);
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x0005F594 File Offset: 0x0005D794
		public bool IsValid()
		{
			return this.OkBtn != null && this.Input != null && this.SendCaptchaBtn != null;
		}

		// Token: 0x04001DDD RID: 7645
		private WindowInfo a;

		// Token: 0x04001DDE RID: 7646
		private WindowInfo b;

		// Token: 0x04001DDF RID: 7647
		private WindowInfo c;

		// Token: 0x04001DE0 RID: 7648
		private WindowInfo d;
	}
}
