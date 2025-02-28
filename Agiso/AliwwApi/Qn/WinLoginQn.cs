using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Agiso.AliwwApi.WinAlert;
using Agiso.Handler;
using Agiso.Utils;
using Agiso.Windows;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x0200075C RID: 1884
	public class WinLoginQn : WinLoginQnBase
	{
		// Token: 0x060025D1 RID: 9681 RVA: 0x0006E6D8 File Offset: 0x0006C8D8
		public override void InputNick(string nick, int retryTimes = 1)
		{
			for (int i = 0; i < retryTimes; i++)
			{
				base.LoginAreaWin.Click(115, 175, true);
				Thread.Sleep(100);
				if (!base.SetTextByClipboard(400, 225, nick, false))
				{
					base.SetText(400, 225, nick);
				}
				Thread.Sleep(200);
				this.ClickDrowUserList();
			}
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x0006E748 File Offset: 0x0006C948
		public override void InputPassword(string pw)
		{
			int num = this.e + 1;
			this.e = num;
			if (num % 2 == 0)
			{
				if (!base.SetTextByClipboard(400, 265, pw, true))
				{
					base.SetText(400, 265, pw);
				}
			}
			else
			{
				base.SetText(400, 265, pw);
			}
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x0000F39E File Offset: 0x0000D59E
		public override void ClickLoginBt()
		{
			base.SimulateMouseClick(440, 345, false, 1);
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x0000F3B2 File Offset: 0x0000D5B2
		public override void ClickBlank()
		{
			base.SimulateMouseClick(380, 150, false, 1);
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x0000F3C6 File Offset: 0x0000D5C6
		public override void ClickDrowUserList()
		{
			base.SimulateMouseClick(416, 239, false, 1);
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x0006E7A8 File Offset: 0x0006C9A8
		public new static List<WinLoginQn> GetList()
		{
			return WinFindableBase.GetList<WinLoginQn>(new WindowStruct("StandardFrame", "千牛 - 卖家工作台"), 0, false);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000677BC File Offset: 0x000659BC
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

		// Token: 0x060025D8 RID: 9688 RVA: 0x0006E7D0 File Offset: 0x0006C9D0
		public override void ClickRememberPwdCheckbox()
		{
			if (!base.IsRememberPwdChecked().GetValueOrDefault(false))
			{
				base.SimulateMouseClick(378, 368, false, 1);
				Thread.Sleep(50);
			}
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x0006E80C File Offset: 0x0006CA0C
		public override Bitmap CaptureWindowRememberPwd()
		{
			return base.CaptureWindow(new Agiso.Windows.Rectangle
			{
				Left = 341,
				Top = 361,
				Right = 351,
				Bottom = 373
			});
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x0006E85C File Offset: 0x0006CA5C
		public override Bitmap CaptureWindowPwdInput()
		{
			return base.CaptureWindow(new Agiso.Windows.Rectangle
			{
				Left = 339,
				Top = 242,
				Right = 570,
				Bottom = 264
			});
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x00067930 File Offset: 0x00065B30
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

		// Token: 0x060025DC RID: 9692 RVA: 0x000679C8 File Offset: 0x00065BC8
		public override Bitmap CaptureWindowErrorMsg()
		{
			return base.CaptureWindow(new Agiso.Windows.Rectangle
			{
				Left = 320,
				Top = 40,
				Right = 585,
				Bottom = 71
			});
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x0006E8AC File Offset: 0x0006CAAC
		public override Bitmap CaptureWindowNotErrorMsg()
		{
			return base.CaptureWindow(new Agiso.Windows.Rectangle
			{
				Left = 320,
				Top = 40,
				Right = 423,
				Bottom = 85
			});
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0000F3DA File Offset: 0x0000D5DA
		public override void ClickReturnButton()
		{
			base.SimulateMouseClick(454, 378, false, 1);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0000F123 File Offset: 0x0000D323
		public override void ClickSecurityRefresh()
		{
			base.SimulateMouseClick(415, 280, false, 1);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x0006E8F4 File Offset: 0x0006CAF4
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
					WindowInfo windowInfo = base.LoginAreaWin.FindWindowInDescendant("StandardWindow", "", false, new bool?(false));
					if (base.Visible && windowInfo != null && windowInfo.Visible)
					{
						qnLoginPageType = QnLoginPageType.LoginForm;
					}
					else if (Util.IsEmptyList<IntPtr>(Win32Extend.GetChildHandleList(base.LoginAreaWin.HWnd, null, null)))
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

		// Token: 0x060025E1 RID: 9697 RVA: 0x0006EA50 File Offset: 0x0006CC50
		public override bool IsAlreadyCollectedImageMd5(string md5)
		{
			return WinLoginQn.PASSWORD_ERROR_MD5_NEW.Contains(md5) || WinLoginQn.ACCOUNT_NOT_EXIST_MD5_NEW.Contains(md5) || WinLoginQn.ACCOUNT_HAS_LOGIN_MD5_NEW.Contains(md5) || WinLoginQn.NONE_ERROR_MD5_NEW.Contains(md5) || WinLoginQn.a.Contains(md5) || WinLoginQn.c.Contains(md5) || WinLoginQn.b.Contains(md5) || WinLoginQn.d.Contains(md5);
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0006EAD0 File Offset: 0x0006CCD0
		public override bool IsPasswordError(string md5)
		{
			return WinLoginQn.PASSWORD_ERROR_MD5_NEW.Contains(md5);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0006EAE8 File Offset: 0x0006CCE8
		public override bool IsAccountHasLogin(string md5)
		{
			return WinLoginQn.ACCOUNT_HAS_LOGIN_MD5_NEW.Contains(md5);
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x0006EB00 File Offset: 0x0006CD00
		public override bool IsNoneHandleError(string md5)
		{
			return WinLoginQn.NONE_ERROR_MD5_NEW.Contains(md5);
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x0006EB18 File Offset: 0x0006CD18
		public override bool IsAccountOrPasswordIsNull(string md5)
		{
			return WinLoginQn.a.Contains(md5);
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x0006EB30 File Offset: 0x0006CD30
		public override bool IsQnLoginErrorNeedToRestart(string md5)
		{
			return WinLoginQn.b.Contains(md5);
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x0006EB48 File Offset: 0x0006CD48
		public override bool IsAccountLimitLogin(string md5)
		{
			return WinLoginQn.c.Contains(md5) || WinLoginQn.ACCOUNT_NOT_EXIST_MD5_NEW.Contains(md5);
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x0006EB70 File Offset: 0x0006CD70
		public override bool IsSpecialEditionLoginError(string md5)
		{
			return WinLoginQn.d.Contains(md5);
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x0006EB88 File Offset: 0x0006CD88
		public override bool IsRemeberPwdChecked(string md5)
		{
			return WinLoginQn.REMEMBER_PWD_CHECKED.Contains(md5);
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x0006EBA0 File Offset: 0x0006CDA0
		public override bool IsRemeberPwdUnChecked(string md5)
		{
			return WinLoginQn.REMEMBER_PWD_UNCHECKED.Contains(md5);
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x0006EBB8 File Offset: 0x0006CDB8
		public override bool IsRemeberPwdCheckedUnknow(string md5)
		{
			return WinLoginQn.REMEMBER_PWD_CHECKED_UNKNOW.Contains(md5);
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x0006EBD0 File Offset: 0x0006CDD0
		public override bool IsPwdInputed(string md5)
		{
			return WinLoginQn.PWD_INPUTED.Contains(md5);
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x0006EBE8 File Offset: 0x0006CDE8
		public override bool IsPwdUnInput(string md5)
		{
			return WinLoginQn.PWD_UNINPUT.Contains(md5);
		}

		// Token: 0x04001F02 RID: 7938
		public static readonly string[] REMEMBER_PWD_CHECKED_UNKNOW = new string[] { "E6BA170404DC4A327CADFBD4DC1135EB", "FE251073C0D04AE5C81F916E26E3115E", "299D200548B17B3BE00BC8EDA90F275D", "1AF0826A209B91C2A9815F321EFADDD2" };

		// Token: 0x04001F03 RID: 7939
		public static readonly string[] REMEMBER_PWD_CHECKED = new string[] { "BC2E361A22C18567ECC30255E3DDD04E" };

		// Token: 0x04001F04 RID: 7940
		public static readonly string[] REMEMBER_PWD_UNCHECKED = new string[] { "31F0FAAF40BE78052BF80635947E6B7E", "0158AA0803C70E90D921711C7E95F3E7", "1453EE6A93B49B8DA3967569C5A1BD78" };

		// Token: 0x04001F05 RID: 7941
		public static readonly string[] PWD_INPUTED = new string[] { "4B381E4A7308C56E72906FCA1FA598AA", "B082EC524A5C8609385174333C2872D0", "3108934E1AF453739BFDAD3EF1FE0BFB", "D29C9C4BAF01A5FDF8775E7636967E30", "1D7890B6AD7C522580D60222F6A92B12", "F6121D66E4066285AE91913D1D38B38C", "ECBAC19BDBF0C5D8399A6226A5198431", "707DC0324EA78D0A3065A6B081EEDC96", "67745FC97C9BC93E1D1A0748E56C3130", "13336518F793E90218ECEA531224A78A" };

		// Token: 0x04001F06 RID: 7942
		public static readonly string[] PWD_UNINPUT = new string[] { "9D1865885292F7C7E6E06B4CD2003E99", "E75F247B4127408D1B146FF24160EA5C", "E9571865A5DA50FB842977EDA0F2966F", "3E3308DAC824B7DA190035F972E582FD", "32FA9B3BB6DEE05F4E6FE67AE6AE05FD", "752743B6B59D3ABFFEEC6983347A8CF0" };

		// Token: 0x04001F07 RID: 7943
		public static readonly string[] PASSWORD_ERROR_MD5_NEW = new string[] { "7F2839D912E52DD303ED3CB878FDE1D5", "CDBAE3291C76CD7A1B5CD40187143806", "6D32EAD16001E57BC5542611A23304FE", "4B62A9A561AA9CAC16ECC8DF0295C6C9" };

		// Token: 0x04001F08 RID: 7944
		public static readonly string[] ACCOUNT_NOT_EXIST_MD5_NEW = new string[] { "ECB12937FBA7D7864D61BE2C1192A8FC" };

		// Token: 0x04001F09 RID: 7945
		public static readonly string[] ACCOUNT_HAS_LOGIN_MD5_NEW = new string[] { "C02470CB13C6EC46617F74243E96670F" };

		// Token: 0x04001F0A RID: 7946
		public static readonly string[] NONE_ERROR_MD5_NEW = new string[] { "F2F27265C4C6886842B3A0583E9EE298", "EBBA1E16EAFEDDCEDC7C4F2DBE7ECF9C", "7FF00039A03092117515BF0F2EDCBF6D", "EFAE6AD963667DF8115DAF1EC41878D9", "A35D24780A329584840D40A77171A7A0", "81F14E87F127B1675B30CE5F1C4EBBAB", "6EBEF4B30CA0D2B1A1B21BDA65F068B7" };

		// Token: 0x04001F0B RID: 7947
		private static readonly string[] a = new string[] { "D475FD06EC06BDFF59AAC4225800C123" };

		// Token: 0x04001F0C RID: 7948
		private static readonly string[] b = new string[0];

		// Token: 0x04001F0D RID: 7949
		private static readonly string[] c = new string[] { "B88015C6A4EF3B680A0EC57211EAC33E", "401F6F3549766AC5A509AA0BD1A5D2AD", "606E19542EFE3923BE7EEF9F54CB6E5B", "4E47C972FB03A97043D0BAB4D3CADD1A", "8AF2D304A46EFCCF1760A0294A6A7644", "501D935ACF3E38849A47C9D78A0D8215", "8BDAE24C7062A1006DA2F9133D10A451", "A631BF628C3B00F7A761065D8C8A3F0C", "B0695258A6FA24881EDF0E610FAAB173", "439125EEF827AAD6F7274439300EFB3C" };

		// Token: 0x04001F0E RID: 7950
		private static readonly string[] d = new string[] { "6073755D60ACA92B86B4CCC77F46C93C" };

		// Token: 0x04001F0F RID: 7951
		private int e = 0;
	}
}
