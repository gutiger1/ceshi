using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Agiso.AliwwApi.WinAlert;
using Agiso.Utils;
using Agiso.Windows;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x02000747 RID: 1863
	public class WinLoginQn91500 : WinLoginQnBase
	{
		// Token: 0x06002506 RID: 9478 RVA: 0x000676C4 File Offset: 0x000658C4
		public override void InputNick(string nick, int retryTimes = 1)
		{
			for (int i = 0; i < retryTimes; i++)
			{
				base.LoginAreaWin.Click(233, 213, true);
				Thread.Sleep(100);
				if (!base.SetTextByClipboard(480, 255, nick, false))
				{
					base.SetText(480, 255, nick);
				}
				Thread.Sleep(200);
				this.ClickDrowUserList();
			}
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x00067734 File Offset: 0x00065934
		public override void InputPassword(string pw)
		{
			int num = this.e + 1;
			this.e = num;
			if (num % 2 == 0)
			{
				if (!base.SetTextByClipboard(478, 308, pw, true))
				{
					base.SetText(478, 308, pw);
				}
			}
			else
			{
				base.SetText(478, 308, pw);
			}
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		public override void ClickLoginBt()
		{
			base.SimulateMouseClick(533, 409, false, 2);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x0000F0E0 File Offset: 0x0000D2E0
		public override void ClickBlank()
		{
			base.SimulateMouseClick(376, 366, false, 1);
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x0000F0F4 File Offset: 0x0000D2F4
		public override void ClickDrowUserList()
		{
			base.SimulateMouseClick(506, 286, false, 1);
			Thread.Sleep(100);
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x00067794 File Offset: 0x00065994
		public new static List<WinLoginQn91500> GetList()
		{
			return WinFindableBase.GetList<WinLoginQn91500>(new WindowStruct("Qt5152QWindowIcon", "千牛登录"), 0, false);
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000677BC File Offset: 0x000659BC
		public override void ClickSelectSellerType()
		{
			base.SetForegroundWindow();
			base.SimulateMouseClick(490, 205, false, 1);
			Application.DoEvents();
			Thread.Sleep(600);
			base.SimulateMouseClick(445, 275, false, 1);
			Application.DoEvents();
			Thread.Sleep(500);
			base.SimulateMouseClick(440, 286, false, 1);
			Thread.Sleep(1000);
			WindowInfo windowInfo = base.FindWindowInDescendant("StandardButton", "提交", false, new bool?(false));
			if (windowInfo != null)
			{
				windowInfo.Click(true);
			}
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x00067854 File Offset: 0x00065A54
		public override void ClickRememberPwdCheckbox()
		{
			if (!base.IsRememberPwdChecked().GetValueOrDefault(false))
			{
				base.SimulateMouseClick(409, 442, false, 1);
				Thread.Sleep(50);
			}
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x00067890 File Offset: 0x00065A90
		public override Bitmap CaptureWindowRememberPwd()
		{
			return base.CaptureWindow(new Agiso.Windows.Rectangle
			{
				Left = 402,
				Top = 433,
				Right = 416,
				Bottom = 447
			});
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000678E0 File Offset: 0x00065AE0
		public override Bitmap CaptureWindowPwdInput()
		{
			return base.CaptureWindow(new Agiso.Windows.Rectangle
			{
				Left = 436,
				Top = 294,
				Right = 555,
				Bottom = 317
			});
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x00067930 File Offset: 0x00065B30
		public override Bitmap CaptureWindowPicValidCode()
		{
			return base.CaptureWindow(new Agiso.Windows.Rectangle
			{
				Left = 488,
				Top = 224,
				Right = 570,
				Bottom = 252
			});
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00067980 File Offset: 0x00065B80
		public override Bitmap CaptureWindowErrorMsg()
		{
			return base.CaptureWindow(new Agiso.Windows.Rectangle
			{
				Left = 320,
				Top = 49,
				Right = 585,
				Bottom = 79
			});
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000679C8 File Offset: 0x00065BC8
		public override Bitmap CaptureWindowNotErrorMsg()
		{
			return base.CaptureWindow(new Agiso.Windows.Rectangle
			{
				Left = 320,
				Top = 40,
				Right = 585,
				Bottom = 71
			});
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x0000F10F File Offset: 0x0000D30F
		public override void ClickReturnButton()
		{
			base.SimulateMouseClick(541, 516, false, 1);
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x0000F123 File Offset: 0x0000D323
		public override void ClickSecurityRefresh()
		{
			base.SimulateMouseClick(415, 280, false, 1);
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x00067A10 File Offset: 0x00065C10
		public override QnLoginPageType GetCurrPage()
		{
			QnLoginPageType qnLoginPageType;
			if (!base.Visible)
			{
				qnLoginPageType = QnLoginPageType.None;
			}
			else
			{
				List<WinAlertLastTodoMsgWhileLoginQn5> list = WinAlertLastTodoMsgWhileLoginQn5.GetList();
				if (list != null && list.Count > 0)
				{
					foreach (WinAlertLastTodoMsgWhileLoginQn5 winAlertLastTodoMsgWhileLoginQn in list)
					{
						winAlertLastTodoMsgWhileLoginQn.NoBtn.Click(false);
					}
					Thread.Sleep(500);
				}
				if (!Util.IsEmptyList<WinSafeTipBase>(WinSafeTipBase.GetList(base.ProcessId)))
				{
					qnLoginPageType = QnLoginPageType.SecurityCheck;
				}
				else if (base.LoginAreaWin == null || !base.LoginAreaWin.Visible)
				{
					if (base.FindWindowInDescendant("StandardButton", "提交", false, new bool?(false)) != null)
					{
						qnLoginPageType = QnLoginPageType.SelectRole;
					}
					else
					{
						qnLoginPageType = QnLoginPageType.None;
					}
				}
				else
				{
					using (Bitmap bitmap = base.CaptureWindow(new Agiso.Windows.Rectangle
					{
						Left = 442,
						Top = 395,
						Right = 502,
						Bottom = 417
					}))
					{
						string text = Util.ComputeHashMd5(bitmap);
						if (new List<string> { "97989000260D327BF0D1CF31412F120F" }.Contains(text))
						{
							return QnLoginPageType.LoginForm;
						}
					}
					if (base.IsPwdRememberAutoInputed() != null || base.IsRememberPwdChecked() != null)
					{
						qnLoginPageType = QnLoginPageType.LoginForm;
					}
					else if (base.SecurityChromeWin != null && base.SecurityChromeWin.Visible)
					{
						qnLoginPageType = QnLoginPageType.SecurityCheck;
					}
					else
					{
						qnLoginPageType = QnLoginPageType.Logining;
					}
				}
			}
			return qnLoginPageType;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x00067BC8 File Offset: 0x00065DC8
		public override bool IsAlreadyCollectedImageMd5(string md5)
		{
			return WinLoginQn91500.PASSWORD_ERROR_MD5_NEW.Contains(md5) || WinLoginQn91500.ACCOUNT_NOT_EXIST_MD5_NEW.Contains(md5) || WinLoginQn91500.ACCOUNT_HAS_LOGIN_MD5_NEW.Contains(md5) || WinLoginQn91500.NONE_ERROR_MD5_NEW.Contains(md5) || WinLoginQn91500.a.Contains(md5) || WinLoginQn91500.c.Contains(md5) || WinLoginQn91500.b.Contains(md5) || WinLoginQn91500.d.Contains(md5);
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x00067C48 File Offset: 0x00065E48
		public override bool IsPasswordError(string md5)
		{
			return WinLoginQn91500.PASSWORD_ERROR_MD5_NEW.Contains(md5);
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00067C60 File Offset: 0x00065E60
		public override bool IsAccountHasLogin(string md5)
		{
			return WinLoginQn91500.ACCOUNT_HAS_LOGIN_MD5_NEW.Contains(md5);
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00067C78 File Offset: 0x00065E78
		public override bool IsNoneHandleError(string md5)
		{
			return WinLoginQn91500.NONE_ERROR_MD5_NEW.Contains(md5);
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x00067C90 File Offset: 0x00065E90
		public override bool IsAccountOrPasswordIsNull(string md5)
		{
			return WinLoginQn91500.a.Contains(md5);
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00067CA8 File Offset: 0x00065EA8
		public override bool IsQnLoginErrorNeedToRestart(string md5)
		{
			return WinLoginQn91500.b.Contains(md5);
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00067CC0 File Offset: 0x00065EC0
		public override bool IsAccountLimitLogin(string md5)
		{
			return WinLoginQn91500.c.Contains(md5) || WinLoginQn91500.ACCOUNT_NOT_EXIST_MD5_NEW.Contains(md5);
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x00067CE8 File Offset: 0x00065EE8
		public override bool IsSpecialEditionLoginError(string md5)
		{
			return WinLoginQn91500.d.Contains(md5);
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x00067D00 File Offset: 0x00065F00
		public override bool IsRemeberPwdChecked(string md5)
		{
			return WinLoginQn91500.REMEMBER_PWD_CHECKED.Contains(md5);
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x00067D18 File Offset: 0x00065F18
		public override bool IsRemeberPwdUnChecked(string md5)
		{
			return WinLoginQn91500.REMEMBER_PWD_UNCHECKED.Contains(md5);
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x00067D30 File Offset: 0x00065F30
		public override bool IsRemeberPwdCheckedUnknow(string md5)
		{
			return WinLoginQn91500.REMEMBER_PWD_CHECKED_UNKNOW.Contains(md5);
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x00067D48 File Offset: 0x00065F48
		public override bool IsPwdInputed(string md5)
		{
			return WinLoginQn91500.PWD_INPUTED.Contains(md5);
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x00067D60 File Offset: 0x00065F60
		public override bool IsPwdUnInput(string md5)
		{
			return WinLoginQn91500.PWD_UNINPUT.Contains(md5);
		}

		// Token: 0x04001E9F RID: 7839
		public static readonly string[] REMEMBER_PWD_CHECKED_UNKNOW = new string[] { "EB1DCEC5A509D42B593BC7AD3AD9CA43", "9343DEC68CB9D5205F7673B313A44979", "2F5020CC73A960772980526AD6E4F662" };

		// Token: 0x04001EA0 RID: 7840
		public static readonly string[] REMEMBER_PWD_CHECKED = new string[] { "51C8C113BCD6672125F8DE7356DB7539" };

		// Token: 0x04001EA1 RID: 7841
		public static readonly string[] REMEMBER_PWD_UNCHECKED = new string[] { "C542AB0599954EDEFE4F259289258278" };

		// Token: 0x04001EA2 RID: 7842
		public static readonly string[] PWD_INPUTED = new string[]
		{
			"7B37C08954790B8EBD76890A9B1384F7", "071B967CA18D6380A0B7E8D6F7F55BFD", "613C1ABB4A56D53D5762F6659761C91F", "60ED3A1517BA980BC3D5C11E651EC0E8", "F6D5642739583B1431B91B61F0C9FCB6", "5EC1209BFF43502A92FD8C3AC72CDE21", "F76D485FBDE813B5291B847F78218559", "7E0E9A16D033398C7EDB9B815572A9DB", "40B7717A6D06D2B8CF609D99F48E7365", "F855233C36A200C10E137BB7565FACA4",
			"0AE22127AF17D1DD136EC9F5B6F6A266"
		};

		// Token: 0x04001EA3 RID: 7843
		public static readonly string[] PWD_UNINPUT = new string[] { "9817A4F7D2D3A84420C362891CE5769A" };

		// Token: 0x04001EA4 RID: 7844
		public static readonly string[] PASSWORD_ERROR_MD5_NEW = new string[] { "A66074676113C6DE9E0621B342BC50F6", "7B98194F6B366729279F68EF0BDE4DCD", "4BBE086840BF9E90F7D65A247AA79E62" };

		// Token: 0x04001EA5 RID: 7845
		public static readonly string[] ACCOUNT_NOT_EXIST_MD5_NEW = new string[0];

		// Token: 0x04001EA6 RID: 7846
		public static readonly string[] ACCOUNT_HAS_LOGIN_MD5_NEW = new string[] { "1D3C52F9022818BBF073211C7649E3DF", "3CAC520433B90C47F015B76E4491F587" };

		// Token: 0x04001EA7 RID: 7847
		public static readonly string[] NONE_ERROR_MD5_NEW = new string[] { "BC307D67FF5A41DA07A84776B96B8387", "9B13484242D7E819D815F4622B972CC4", "D8BD3394AD35E53DFB5FB727698C29DB", "2A28AAC235D6F00459F5FCAB3BA98038" };

		// Token: 0x04001EA8 RID: 7848
		private static readonly string[] a = new string[] { "2C80C31634255D3815DCA7648507AFDA" };

		// Token: 0x04001EA9 RID: 7849
		private static readonly string[] b = new string[0];

		// Token: 0x04001EAA RID: 7850
		private static readonly string[] c = new string[] { "700DDB8FCCFC01E396C7E343CA9788B2", "1B2078C6F130757603CAB43E034AF0C8", "ED9525D0107CE3A891A983627E3F6DE9", "480B45234505DB9FDDD0CD899A326365", "AA9C24E9B9BE20A5EC6632382213123F", "D585A60F7B141C635B2708F32F14FAD3", "8FF3CE9252958098E0105CA9047F5335", "61898048D178E4BC656175722BDDD2AA" };

		// Token: 0x04001EAB RID: 7851
		private static readonly string[] d = new string[0];

		// Token: 0x04001EAC RID: 7852
		private int e = 0;
	}
}
