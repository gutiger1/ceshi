using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agiso.AliwwApi.Object;
using Agiso.AliwwApi.Qn.PlugIn;
using Agiso.AliwwApi.WinAlert;
using Agiso.Extensions;
using Agiso.Handler;
using Agiso.Manager;
using Agiso.Object;
using Agiso.Utils;
using Agiso.Windows;
using Agiso.WwService.Sdk;
using Agiso.WwService.Sdk.Domain;
using Agiso.WwService.Sdk.Response;
using Agiso.WwWebSocket.Model;
using AliwwClient;
using AliwwClient.Cache;
using AliwwClient.Enums;
using AliwwClient.Manager;
using AliwwClient.Object;
using AliwwClient.Properties;
using AliwwClient.Server;
using AliwwClient.WebSocketServer;
using AliwwClient.WebSocketServer.Extensions;
using HtmlAgilityPack;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x0200074F RID: 1871
	public class AliwwQn : Aliww, IIM
	{
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x0000F1C3 File Offset: 0x0000D3C3
		// (set) Token: 0x06002565 RID: 9573 RVA: 0x0000F1CB File Offset: 0x0000D3CB
		public AliwwOptionQn5 Option { get; set; } = new AliwwOptionQn5();

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x00068588 File Offset: 0x00066788
		public MsgSendSoftware SendSoftware
		{
			get
			{
				QnVersionType qnVersion = AppConfig.AgentSettings.QnVersion;
				QnVersionType qnVersionType = qnVersion;
				if (qnVersionType != 1)
				{
					if (qnVersionType == 2)
					{
						return MsgSendSoftware.QN604;
					}
				}
				return MsgSendSoftware.QN;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x000685B4 File Offset: 0x000667B4
		public override AliwwVersion AliwwVersion
		{
			get
			{
				return AliwwVersion.QianNiu5;
			}
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000685C4 File Offset: 0x000667C4
		public AliwwQn(AldsAccountInfo account)
			: base((account == null) ? null : account.UserNick)
		{
			this.e = account;
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x0006863C File Offset: 0x0006683C
		private AliwwWorkBenchQn r()
		{
			if (this.g == null)
			{
				this.g = AliwwWorkBenchQn.Get(this.e.UserNick);
				if (this.g == null && this.e.DisplayUserNick != this.e.UserNick)
				{
					this.g = AliwwWorkBenchQn.Get(this.e.DisplayUserNick);
				}
				if (this.g != null)
				{
					this.d = this.g.ProcessId;
				}
			}
			return this.g;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000686CC File Offset: 0x000668CC
		private AliwwTalkWindow q()
		{
			if (this.h == null)
			{
				this.i = AliwwTalkWindowQn.Get(this.e.UserNick);
				if (this.i == null && this.e.DisplayUserNick != this.e.UserNick)
				{
					this.i = AliwwTalkWindowQn.Get(this.e.DisplayUserNick);
				}
				if (this.i != null)
				{
					this.h = AliwwTalkWindow.ParseFromWindowInfo(this.i);
					this.h.AliVersion = this.i.AliwwVersion;
					this.h.UserNick = base.UserNick;
					this.h.CurrentAliww = this;
					this.d = this.h.ProcessId;
				}
			}
			return this.h;
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000687A4 File Offset: 0x000669A4
		public WinLoginQnBase GetLoginWin()
		{
			if (this.f == null)
			{
				List<WinLoginQnBase> list = WinLoginQnBase.GetList();
				if (!Util.IsEmptyList<WinLoginQnBase>(list))
				{
					foreach (WinLoginQnBase winLoginQnBase in list)
					{
						bool? responding = winLoginQnBase.Responding;
						if (responding != null)
						{
							if (!responding.Value)
							{
								Win32Extend.KillProcessById(winLoginQnBase.ProcessId, null);
							}
							else if (winLoginQnBase.Visible && winLoginQnBase.LoginAreaWin.Visible && winLoginQnBase.GetCurrPage() == QnLoginPageType.LoginForm)
							{
								this.f = winLoginQnBase;
								WinLoginQnBase winLoginQnBase2 = this.f;
								this.d = ((winLoginQnBase2 != null) ? winLoginQnBase2.ProcessId : 0);
								return this.f;
							}
						}
					}
				}
			}
			return this.f;
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x0006889C File Offset: 0x00066A9C
		private LoginBehavior LoginSession
		{
			get
			{
				return AppConfig.AliwwWebScoketServer.LoginSession;
			}
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000688B8 File Offset: 0x00066AB8
		private ErrCodeInfo o()
		{
			AliwwQn.a a = new AliwwQn.a();
			a.b = this;
			ErrCodeInfo errCodeInfo;
			if (this.LoginSession == null)
			{
				int num = this.r + 1;
				this.r = num;
				if (num % 5 == 0)
				{
					this.n();
				}
				errCodeInfo = null;
			}
			else
			{
				AliwwQn.s = 0;
				this.r = 0;
				if (this.LoginSession.ConnectionState != 1 || string.IsNullOrEmpty(this.LoginSession.CurrUrl))
				{
					errCodeInfo = null;
				}
				else
				{
					if (this.LoginSession.CurrUrl.Contains("passport.taobao.com/ac/pc_risk_center.htm"))
					{
						if (this.q == null)
						{
							this.q = new DateTime?(DateTime.Now);
						}
					}
					else
					{
						this.q = null;
					}
					a.a = "";
					if (this.LoginSession.CurrUrl.Contains("login.m.taobao.com/noCaptcha.htm"))
					{
						a.a = this.LoginSession.GetHtml();
						if (string.IsNullOrEmpty(a.a))
						{
							return null;
						}
						if (a.a.Contains("404 Not Found"))
						{
							this.f.ClickReturnButton();
							return null;
						}
						HtmlDocument htmlDocument = new HtmlDocument();
						htmlDocument.LoadHtml(a.a);
						HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='nc_1-stage-1']");
						if (htmlNode == null)
						{
							return null;
						}
						if (!htmlNode.Attributes["style"].Value.Contains("display: none"))
						{
							int num = this.o + 1;
							this.o = num;
							if (num < 5)
							{
								this.f.Slide(380, 230, 520, 230);
								Thread.Sleep(200);
							}
							else
							{
								string sideOffset = this.LoginSession.GetSideOffset();
								if (!string.IsNullOrEmpty(sideOffset))
								{
									LogWriter.WriteLog("滑动条位置：" + sideOffset, 1);
									Hashtable hashtable = JSON.Decode(sideOffset) as Hashtable;
									int num2 = Util.ToInt(((Hashtable)hashtable["slide"])["left"]);
									int num3 = 320 + num2 + 10;
									int num4 = 37 + Util.ToInt(((Hashtable)hashtable["slide"])["top"]) + 10;
									int num5 = 320 + num2 + Util.ToInt(((Hashtable)hashtable["slide"])["width"]) - 10;
									int num6 = num4;
									this.f.Slide(num3, num4, num5, num6);
									Thread.Sleep(200);
								}
							}
							return null;
						}
						HtmlNode htmlNode2 = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='nc_1-stage-2']");
						string text;
						if (htmlNode2 == null)
						{
							text = null;
						}
						else
						{
							HtmlAttribute htmlAttribute = htmlNode2.Attributes["style"];
							text = ((htmlAttribute != null) ? htmlAttribute.Value : null);
						}
						string text2 = text;
						if (text2 != null && !text2.Contains("display: none") && !text2.Contains("display:none"))
						{
							int num7 = 0;
							string text3 = "";
							do
							{
								if (num7 > 0)
								{
									this.LoginSession.ReloadImageCaptcha();
									Thread.Sleep(500);
								}
								string imageCaptcha = this.LoginSession.GetImageCaptcha();
								string text4 = ((num7 % 2 == 0) ? "B942" : "C183");
								if (!string.IsNullOrEmpty(imageCaptcha))
								{
									text3 = AppConfig.GetPicValidCodeByBase64(imageCaptcha, text4);
								}
								else
								{
									Thread.Sleep(100);
									using (Bitmap bitmapFromDC = this.f.GetBitmapFromDC(0))
									{
										Util.CollectPicMd5(bitmapFromDC, "新版千牛图片截图_");
									}
									using (Bitmap bitmap = this.f.CaptureWindowPicValidCode())
									{
										if (bitmap != null)
										{
											using (MemoryStream memoryStream = new MemoryStream())
											{
												bitmap.Save(memoryStream, ImageFormat.Jpeg);
												byte[] buffer = memoryStream.GetBuffer();
												memoryStream.Close();
												text3 = AppConfig.GetPicValidCode(buffer, text4);
											}
										}
									}
								}
								if (text3 == null)
								{
									goto IL_047A;
								}
								num7++;
							}
							while (num7 < 5 && (text3.Length != 4 || text3 == "-100"));
							this.LoginSession.SubmitImageCaptcha(text3);
							return null;
							IL_047A:
							return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginFailPicValidCodeRequiredGetPicCodeError);
						}
						HtmlNode htmlNode3 = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='nc_1-stage-3']");
						if (!htmlNode3.Attributes["style"].Value.Contains("display: none"))
						{
							int num = this.p + 1;
							this.p = num;
							if (num < 5)
							{
								this.f.ClickSecurityRefresh();
							}
							else
							{
								this.LoginSession.ClickRefresh();
							}
						}
					}
					else if (this.LoginSession.CurrUrl.Contains("passport.taobao.com/iv/mini/identity_verify.htm"))
					{
						a.a = this.LoginSession.GetHtml();
						if (string.IsNullOrEmpty(a.a) || !a.a.Contains("手机"))
						{
							return null;
						}
						if (a.a.Contains("你正在使用手机短信验证身份，请联系主账号获取验证码"))
						{
							return new ErrCodeInfo(ErrCodeType.LoginLimitNeedMasterAccountSmsCode);
						}
					}
					else if (this.LoginSession.CurrUrl.Contains("passport.taobao.com/iv/h5/h_5_identity_verify.htm"))
					{
						a.a = this.LoginSession.GetHtml();
						if (string.IsNullOrEmpty(a.a))
						{
							return null;
						}
						if (a.a.Contains("身份证"))
						{
							return new ErrCodeInfo(ErrCodeType.NeedIdVerification);
						}
						if (!a.a.Contains("手机"))
						{
							return null;
						}
						if (a.a.Contains("已发送短信，下一步") && a.a.Contains("发送至") && a.a.Contains("完成验证"))
						{
							return new ErrCodeInfo(ErrCodeType.NeedSendSmsToValidate);
						}
						string text5 = AppConfig.AgentPhones.FirstOrDefault(new Func<string, bool>(a.e));
						if (string.IsNullOrEmpty(text5))
						{
							return new ErrCodeInfo(ErrCodeType.BindingIncorrectMobile);
						}
						this.j = text5.Substring(0, 3) + "******" + text5.Substring(9);
						bool flag = false;
						for (int i = 0; i < 5; i++)
						{
							if (this.r() != null)
							{
								return null;
							}
							if (this.f.GetCurrPage() != QnLoginPageType.SecurityCheck)
							{
								return null;
							}
							this.LoginSession.ClickSmsButton();
							Thread.Sleep(500);
							List<string> list = this.a(this.e.UserNick, this.j, 5);
							if (list != null && list.Count > 0)
							{
								flag = true;
								foreach (string text6 in list)
								{
									this.LoginSession.SubmitMobileCaptcha(text6);
									Application.DoEvents();
									int num8 = 5000;
									Func<bool> func;
									if ((func = a.c) == null)
									{
										func = (a.c = new Func<bool>(a.f));
									}
									if (Util.CheckWait(num8, func, 300))
									{
										AppConfig.QnAgentServiceClient.RemoveVerificationCodeSMS(text6, this.j);
										if (this.f.GetCurrPage() == QnLoginPageType.SelectRole)
										{
											int num9 = 5000;
											Func<bool> func2;
											if ((func2 = a.d) == null)
											{
												func2 = (a.d = new Func<bool>(a.e));
											}
											if (!Util.CheckWait(num9, func2, 200))
											{
												return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginFailNeedToSelectSellerType);
											}
										}
										return null;
									}
								}
							}
						}
						if (flag)
						{
							return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginFailAllSmsCodeInvalid);
						}
						return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginFailGetSmsValidCodeNull);
					}
					else
					{
						if (this.LoginSession.CurrUrl.Contains("zizhanghao.taobao.com/authenticate/login/login_auth_intercept.htm"))
						{
							return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginAuthInterceptNeedToScanQr);
						}
						if (this.LoginSession.CurrUrl.Contains("passport.taobao.com/iv/remote/h5/login_check.htm") || this.LoginSession.CurrUrl.Contains("passport.taobao.com/iv/remote/mini/request.htm"))
						{
							a.a = this.LoginSession.GetHtml();
							if (string.IsNullOrEmpty(a.a))
							{
								return null;
							}
							HtmlDocument htmlDocument = new HtmlDocument();
							htmlDocument.LoadHtml(a.a);
							HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//span[@id='nc_1_n1z']");
							if (htmlNode == null)
							{
								return null;
							}
							this.f.LoginAreaWin.Slide(12, 275, 275, 275);
							Thread.Sleep(200);
						}
						else
						{
							if (this.LoginSession.CurrUrl.Contains("login.taobao.com/member/qrcode.htm"))
							{
								this.f.ClickReturnButton();
								return null;
							}
							if (this.LoginSession.CurrUrl.Contains("passport.taobao.com/ac/pc_risk_center.htm"))
							{
								if (this.q != null && (DateTime.Now - this.q.Value).TotalSeconds >= 5.0)
								{
									this.f.ClickReturnButton();
									return null;
								}
								return null;
							}
							else
							{
								if (this.LoginSession.CurrUrl.Contains("passport.taobao.com/iv/mini/verify_modes.htm"))
								{
									return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginFailNeedToConcatTb);
								}
								if (this.LoginSession.CurrUrl.Contains("passport.taobao.com/iv/h5/h_5_normal_validate.htm") || this.LoginSession.CurrUrl.Contains("passport.taobao.com/iv/h5/h_5_verify_modes.htm") || this.LoginSession.CurrUrl.Contains("passport.taobao.com/iv/mini/normal_validate.htm"))
								{
									return null;
								}
								if (this.LoginSession.CurrUrl.Contains("passport.taobao.com/ac/h5/new_open.htm"))
								{
									return new ErrCodeInfo(ErrCodeType.LimitLogin, "安全检测到你的账号存在异常，已被保护。请拖动滑块检测你的使用环境是否安全，并根据提示完成自助开通。");
								}
								if (this.LoginSession.CurrUrl.Contains("passport.taobao.com/ac/pc_risk_center_iv_select.htm"))
								{
									return new ErrCodeInfo(ErrCodeType.LimitLogin, "账号被限制登录了，请本地登录下千牛，按照千牛上的提示完成解封");
								}
								using (Bitmap bitmapFromDC2 = this.f.GetBitmapFromDC(0))
								{
									FileItem fileItem = new FileItem("loginErrComponent.jpg", Util.smethod_1(bitmapFromDC2, ImageFormat.Jpeg), "image/jpeg");
									AppConfig.QnAgentServiceClient.AutoLoginError(base.UserNick, fileItem, "登录千牛时，遇到未知的登录校验页", 268435455L, "", "");
								}
								LogWriter.WriteLog("未知的登录校验页：" + this.LoginSession.CurrUrl, 1);
							}
						}
					}
					errCodeInfo = null;
				}
			}
			return errCodeInfo;
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x00069434 File Offset: 0x00067634
		private void n()
		{
			if (this.LoginSession == null && this.f.SecurityChromeWin != null)
			{
				this.f.SecurityChromeWin.Click(true);
				Thread.Sleep(500);
				this.f.DoSendKeys("{F12}", true);
				Thread.Sleep(500);
				List<WindowInfo> list = Win32Extend.GetAllDesktopWindows().Where(new Func<WindowInfo, bool>(AliwwQn.<>c.<>9.g)).ToList<WindowInfo>();
				WindowInfo windowInfo = list.FirstOrDefault(new Func<WindowInfo, bool>(AliwwQn.<>c.<>9.f));
				if (windowInfo != null)
				{
					windowInfo.SetForegroundWindow();
					windowInfo.DoSendKeys("^`", true);
					Thread.Sleep(1000);
					ClipboardProxy.SetText(Resources.qnProxy, TextDataFormat.Text, 10);
					Thread.Sleep(1000);
					windowInfo.DoSendKeys("{ENTER}", true);
					Thread.Sleep(500);
					windowInfo.Close(true);
				}
				else if (list.FirstOrDefault(new Func<WindowInfo, bool>(AliwwQn.<>c.<>9.e)) != null)
				{
					this.f.ClickReturnButton();
					Thread.Sleep(100);
				}
				else
				{
					WindowInfo windowInfo2 = list.FirstOrDefault(new Func<WindowInfo, bool>(AliwwQn.<>c.<>9.d));
					if (windowInfo2 != null)
					{
						LogWriter.WriteLog("检测到需要植入js，" + JSON.Encode(windowInfo2.Info), 1);
					}
				}
			}
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000695DC File Offset: 0x000677DC
		private void m()
		{
			this.e();
			Win32Extend.CloseWindowListByClassAndName("Qt5152QWindowIcon", "-服务态度提醒", 0, true, 200);
			Win32Extend.CloseWindowListByClassAndName("Qt5152QWindowIcon", "错误", 0, true, 200);
			Win32Extend.CloseWindowListByClassAndName("Qt5152QWindowIcon", "-会话移除二次确认", 0, true, 200);
			Win32Extend.CloseWindowListByClassAndName("Qt5152QWindowIcon", "询问", 0, true, 200);
			Win32Extend.CloseWindowListByClassAndName("", "- 快速异常检测失败", 0, true, 200);
			Win32Extend.CloseWindowListByClassAndName("Qt5152QWindowToolSaveBits", "消息提醒", 0, true, 200);
			WinQnStopWork.CloseAll(0);
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x00069688 File Offset: 0x00067888
		public ErrCodeInfo DoLogin()
		{
			if (this.f.GetCurrPage() == QnLoginPageType.LoginForm)
			{
				this.m();
				string text = ((this.v % 2 == 0) ? this.e.UserNick : this.e.DisplayUserNick);
				int num = this.v;
				this.v = num + 1;
				if (num < 2)
				{
					this.f.InputNick(text, 2);
					Thread.Sleep(100);
					if (!this.f.IsPwdRememberAutoInputed().GetValueOrDefault())
					{
						this.f.InputPassword(this.e.QnAccountPwd);
						Thread.Sleep(100);
					}
				}
				else
				{
					this.f.InputNick(text, 1);
					Thread.Sleep(100);
					this.f.InputPassword(this.e.QnAccountPwd);
					Thread.Sleep(100);
				}
				this.f.ClickRememberPwdCheckbox();
				this.f.ClickLoginBt();
				Thread.Sleep(1000);
			}
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct("", "-旺旺登录失败"), 0, true);
			ErrCodeInfo errCodeInfo;
			if (!Util.IsEmptyList<WindowInfo>(windowListByClassAndName))
			{
				windowListByClassAndName.ForEach(new Action<WindowInfo>(AliwwQn.<>c.<>9.c));
				errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailWwLoginErrorNetworkError);
			}
			else
			{
				for (;;)
				{
					switch (this.f.GetCurrPage())
					{
					case QnLoginPageType.None:
						goto IL_01A7;
					case QnLoginPageType.LoginForm:
						goto IL_01D8;
					case QnLoginPageType.Logining:
						goto IL_0347;
					case QnLoginPageType.SecurityCheck:
					{
						WinSafeTipBase winSafeTipBase = WinSafeTipBase.Get(this.f.ProcessId);
						if (winSafeTipBase != null)
						{
							winSafeTipBase.Close(true);
							Thread.Sleep(100);
							continue;
						}
						goto IL_039C;
					}
					case QnLoginPageType.SelectRole:
						goto IL_05A5;
					}
					break;
				}
				goto IL_05A0;
				IL_01A7:
				if (Util.CheckWait(5000, new Func<bool>(this.a), 200))
				{
					return new ErrCodeInfo(ErrCodeType.CallSuccQn);
				}
				goto IL_05A0;
				IL_01D8:
				using (Bitmap bitmap = this.f.CaptureWindowErrorMsg())
				{
					string text2 = Util.ComputeHashMd5(bitmap);
					if (this.f.IsPasswordError(text2))
					{
						return new ErrCodeInfo(ErrCodeType.PasswordError);
					}
					if (this.f.IsNoneHandleError(text2))
					{
						Thread.Sleep(100);
						return null;
					}
					if (this.f.IsAccountHasLogin(text2))
					{
						return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginErrorUserHasLoginOnThisMachine);
					}
					if (this.f.IsAccountLimitLogin(text2))
					{
						return new ErrCodeInfo(ErrCodeType.LimitLogin);
					}
					if (this.f.IsAccountOrPasswordIsNull(text2))
					{
						return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginAccountOrPwdIsNull);
					}
					if (this.f.IsQnLoginErrorNeedToRestart(text2))
					{
						return new ErrCodeInfo(ErrCodeType.LoginQnErrorNeedToRestart);
					}
					if (this.f.IsSpecialEditionLoginError(text2))
					{
						return new ErrCodeInfo(ErrCodeType.SpecialEditionLoginError);
					}
					if (!this.f.IsAlreadyCollectedImageMd5(text2))
					{
						using (Bitmap bitmap2 = this.f.CaptureWindowNotErrorMsg())
						{
							string text3 = Util.ComputeHashMd5(bitmap2);
							if (!new string[] { "AF11CAFE62BB0CAD01865B8DECC9ED3C" }.Contains(text3))
							{
								Util.CollectPicMd5(bitmap, "千牛未收集的错误类型_");
							}
						}
					}
					return new ErrCodeInfo(ErrCodeType.LoginAliwwFindError);
				}
				IL_0347:
				if (Util.CheckWait(5000, new Func<bool>(this.c), 200) && (this.r() != null || this.q() != null))
				{
					return new ErrCodeInfo(ErrCodeType.CallSuccQn);
				}
				return new ErrCodeInfo(ErrCodeType.QnLogining);
				IL_039C:
				int num2 = 0;
				this.o = 0;
				int num3 = 40;
				DateTime now = DateTime.Now;
				ErrCodeInfo errCodeInfo2;
				for (;;)
				{
					Thread.Sleep(500);
					errCodeInfo2 = this.o();
					if (errCodeInfo2 != null)
					{
						break;
					}
					if (this.LoginSession == null && (DateTime.Now - now).TotalSeconds >= 10.0)
					{
						if (!this.t)
						{
							this.t = true;
							AliwwQn.s++;
						}
						if (AliwwQn.s >= 2)
						{
							AliwwQn.s = 0;
							FileItem fileItem = null;
							if (this.f != null)
							{
								using (Bitmap bitmapFromDC = this.f.GetBitmapFromDC(0))
								{
									byte[] array = Util.smethod_1(bitmapFromDC, ImageFormat.Png);
									fileItem = new FileItem("screenshot.jpg", array, "image/jpeg");
								}
							}
							AppConfig.QnAgentServiceClient.AutoLoginError(base.UserNick, fileItem, "连续多次登录失败，js植入失败。请检查下服务器，可能需要重启服务器！", 268435455L, "", "");
						}
						this.a("检测到一直在安全校验页面，重新安装下证书");
					}
					num2++;
					if (num2 >= num3 || this.f.GetCurrPage() != QnLoginPageType.SecurityCheck)
					{
						goto IL_04E6;
					}
				}
				return errCodeInfo2;
				IL_04E6:
				if (num2 >= num3 && this.f.GetCurrPage() == QnLoginPageType.SecurityCheck)
				{
					string text4 = "LoginSession:{0},CurrUrl:{1}\r\n,ConnectionState:{2},\r\nhtml:{3}";
					object[] array2 = new object[4];
					array2[0] = this.LoginSession != null;
					int num4 = 1;
					LoginBehavior loginSession = this.LoginSession;
					array2[num4] = ((loginSession != null) ? loginSession.CurrUrl : null);
					int num5 = 2;
					LoginBehavior loginSession2 = this.LoginSession;
					array2[num5] = ((loginSession2 != null) ? new WebSocketState?(loginSession2.ConnectionState) : null);
					int num6 = 3;
					LoginBehavior loginSession3 = this.LoginSession;
					array2[num6] = ((loginSession3 != null) ? loginSession3.GetHtml() : null);
					LogWriter.WriteLog(string.Format(text4, array2), 1);
					string text5 = JsonConvert.SerializeObject(AgentHttpListener.RequestItemQueue);
					LogWriter.WriteLog(text5, 1);
					return new ErrCodeInfo(ErrCodeType.SecurityCheckFail);
				}
				IL_05A0:
				return null;
				IL_05A5:
				if (Util.CheckWait(5000, new Func<bool>(this.b), 200))
				{
					errCodeInfo = null;
				}
				else
				{
					errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailQn5LoginFailNeedToSelectSellerType);
				}
			}
			return errCodeInfo;
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x00069CB8 File Offset: 0x00067EB8
		public ErrCodeInfo SendMsg(string toUserNick, string toOpenUid, string msgBody, string siteId = "cntaobao")
		{
			AliwwQn.b b = new AliwwQn.b();
			b.b = this;
			b.d = msgBody;
			b.f = toUserNick;
			b.g = siteId;
			b.h = toOpenUid;
			uint num = Win32Extend.GetMemoryLoad();
			if (num >= 80U)
			{
				global::k.a().ClearUnLongOpenQn(base.UserNick);
			}
			num = Win32Extend.GetMemoryLoad();
			if (num >= 80U)
			{
				AppConfig.ClearMemorySilent("检测到内存大于80%，清理下内存");
				global::k.a().CloseOneLongOpenQn("检测到内存大于80%，关掉一个常挂的千牛");
			}
			this.h();
			List<WinFloatBase> list = WinFloatBase.GetList();
			if (list != null)
			{
				list.ForEach(new Action<WinFloatBase>(AliwwQn.<>c.<>9.b));
			}
			WinQnStopWork.CloseAll(0);
			AliwwErrorReport.CloseQn(0);
			this.j();
			WinSafeTipBase.CloseAll();
			AliwwQn.smethod_0();
			this.g();
			List<WindowInfo> list2 = Win32Extend.GetAllDesktopWindows().Where(new Func<WindowInfo, bool>(AliwwQn.<>c.<>9.b)).ToList<WindowInfo>();
			foreach (WindowInfo windowInfo in list2)
			{
				windowInfo.Close(true);
			}
			DateTime now = DateTime.Now;
			b.i = null;
			b.c = 0;
			b.e = 0;
			if (AliwwQn.n > 2)
			{
				AliwwQn.n = 0;
				global::k.a().ClearAllQnProc(true, true, true, true, "千牛登录时，连续出现提示错误太多次了，杀掉所有千牛");
				Thread.Sleep(2000);
			}
			if (this.q() != null)
			{
				this.l();
				this.k();
				this.a(false);
				this.i();
				AliwwQn.m.Remove(base.UserNick);
			}
			else
			{
				if (this.r() != null)
				{
					this.l();
					this.k();
					this.a(false);
					AliwwQn.m.Remove(base.UserNick);
				}
				else
				{
					if (this.GetLoginWin() == null)
					{
						try
						{
							if (File.Exists(AppConfig.GetExeFullFileNameQnNew(AppConfig.AgentSettings.QnVersion)))
							{
								File.Delete(AppConfig.GetExeFullFileNameQnNew(AppConfig.AgentSettings.QnVersion));
							}
						}
						catch
						{
						}
						b.a = Process.Start(AppConfig.GetExeFullFileNameQn(AppConfig.AgentSettings.QnVersion));
						if (b.a != null)
						{
							Util.CheckWait(10000, new Func<bool>(b.w), 100);
						}
						if (!Util.CheckWait(2000, new Func<bool>(b.v), 300))
						{
							if (Interlocked.Increment(ref AliwwQn.b) >= 3)
							{
								Interlocked.Exchange(ref AliwwQn.b, 0);
								AppConfig.QnAgentServiceClient.AutoLoginError(base.UserNick, null, "启动千牛连续多次超时，请检查下服务器，可能需要重启服务器！", 268435455L, "", "");
							}
							return new ErrCodeInfo(ErrCodeType.StartQnTimeout);
						}
						Interlocked.Exchange(ref AliwwQn.b, 0);
						b.i = new ErrCodeInfo(ErrCodeType.CallSuccQn);
					}
					AliwwQn.c c = new AliwwQn.c();
					c.c = b;
					if (AppConfig.AliwwClientMode == AliwwClientMode.自挂模式)
					{
						return new ErrCodeInfo(ErrCodeType.const_50);
					}
					if (string.IsNullOrEmpty(this.e.QnAccountPwd))
					{
						return new ErrCodeInfo(ErrCodeType.SendFailQn5NoPwd);
					}
					c.b = 45;
					c.a = DateTime.Now;
					Task<ErrCodeInfo> task = Task.Run<ErrCodeInfo>(new Func<ErrCodeInfo>(c.d));
					if (task.Wait(TimeSpan.FromSeconds((double)(c.b + 5))))
					{
						if (task.Result == null)
						{
							if (this.q() != null)
							{
								AliwwQn.n = 0;
								goto IL_04CC;
							}
							if (this.r() != null)
							{
								AliwwQn.n = 0;
								goto IL_03CD;
							}
						}
						return task.Result;
					}
					LogWriter.WriteLog("当前登录页面：" + this.f.GetCurrPage().ToString(), 1);
					if (this.f.GetCurrPage() == QnLoginPageType.SecurityCheck)
					{
						LoginBehavior loginSession = this.LoginSession;
						string text;
						if (loginSession != null)
						{
							if ((text = loginSession.CurrUrl) != null)
							{
								goto IL_06D6;
							}
						}
						text = "";
						IL_06D6:
						if (text.Contains("passport.taobao.com/iv/mini/identity_verify.htm"))
						{
							return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginFailMobileValidTimeout);
						}
					}
					this.KillProcess();
					return new ErrCodeInfo(ErrCodeType.LoginQnTimeout, string.Format("登录时间超过{0}s，{1}", c.b + 5, this.f.GetCurrPage()));
				}
				IL_03CD:
				if (this.q() == null && this.r() != null)
				{
					AliwwQn.d d = new AliwwQn.d();
					d.b = b;
					AliwwQn.a.Remove(base.UserNick);
					this.g.Close(true);
					d.b.i = new ErrCodeInfo(ErrCodeType.SendFailChatWinNotFoundAndBenchWinHasFound);
					d.a = DateTime.Now;
					this.CallUser(d.b.f, d.b.g, d.b.h);
					Task<ErrCodeInfo> task2 = Task.Run<ErrCodeInfo>(new Func<ErrCodeInfo>(d.c));
					if (!task2.Wait(TimeSpan.FromSeconds(25.0)))
					{
						this.KillProcess();
						return new ErrCodeInfo(ErrCodeType.CallTalkWinTimeOutInTalkWin, base.UserNick + "，呼叫聊天窗口超过25s");
					}
					if (task2.Result != null)
					{
						return task2.Result;
					}
				}
			}
			IL_04CC:
			ErrCodeInfo errCodeInfo;
			if (this.q() != null)
			{
				AliwwQn.e e = new AliwwQn.e();
				e.i = b;
				List<WinFloatBase> list3 = WinFloatBase.GetList();
				if (list3 != null)
				{
					list3.ForEach(new Action<WinFloatBase>(AliwwQn.<>c.<>9.a));
				}
				AliwwQn.a.Remove(base.UserNick);
				AliwwWorkBenchQn aliwwWorkBenchQn = this.g;
				if (aliwwWorkBenchQn != null)
				{
					aliwwWorkBenchQn.Close(true);
				}
				Win32Extend.CloseWindowListByClassAndName("Qt5152QWindowIcon", "询问", 0, true, 200);
				base.CurrUserCache.LastSendProcessId = this.h.ProcessId;
				base.CurrUserCache.LastSendSoftware = this.SendSoftware;
				e.i.i = new ErrCodeInfo(ErrCodeType.CallFailCheckTargetNickUnEquals);
				e.c = DateTime.Now;
				e.b = 0;
				e.e = 0;
				e.a = AppConfig.BuyerInfoCache.GetRecentOpenUidByAldsOpenUid(e.i.h);
				e.f = null;
				e.d = PlugInContainerManagerFactory.Get(this.i, base.UserNick);
				e.h = 30;
				e.g = 0;
				Task<ErrCodeInfo> task3 = Task.Run<ErrCodeInfo>(new Func<ErrCodeInfo>(e.j));
				if (task3.Wait(TimeSpan.FromSeconds((double)(e.h + 5))))
				{
					errCodeInfo = task3.Result;
				}
				else
				{
					errCodeInfo = new ErrCodeInfo(ErrCodeType.SendMsgTimeOutInTalkWin, string.Format("{0}在聊天窗口发送时间超过{1}s", base.UserNick, e.h + 5));
				}
			}
			else
			{
				errCodeInfo = b.i;
			}
			return errCodeInfo;
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x0006A410 File Offset: 0x00068610
		private bool l()
		{
			List<WinAlertNeedToAddFriend> list = WinAlertNeedToAddFriend.GetList(this.d);
			if (list != null && list.Count > 0)
			{
				foreach (WinAlertNeedToAddFriend winAlertNeedToAddFriend in list)
				{
					winAlertNeedToAddFriend.Close(true);
				}
				int num = this.w + 1;
				this.w = num;
				if (num >= 2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x0006A4A4 File Offset: 0x000686A4
		private bool k()
		{
			List<WinAlertRefuseToAddFriend> list = WinAlertRefuseToAddFriend.GetList(this.d);
			bool flag;
			if (list != null && list.Count > 0)
			{
				foreach (WinAlertRefuseToAddFriend winAlertRefuseToAddFriend in list)
				{
					winAlertRefuseToAddFriend.Close(true);
				}
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x0006A51C File Offset: 0x0006871C
		private bool a(bool A_0 = true)
		{
			List<WinFileNotAcceptAlert> listQn = WinBlackListOrNotAccountExistsAlert.GetListQn(this.d);
			bool flag;
			if (listQn != null && listQn.Count > 0)
			{
				foreach (WinFileNotAcceptAlert winFileNotAcceptAlert in listQn)
				{
					if (A_0)
					{
						using (Bitmap bitmapFromDC = winFileNotAcceptAlert.GetBitmapFromDC(0))
						{
							Util.CollectPicMd5(bitmapFromDC, "黑名单_" + Util.GetMasterNick(base.UserNick) + "_");
						}
					}
					winFileNotAcceptAlert.Close(true);
				}
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x0006A5DC File Offset: 0x000687DC
		private void j()
		{
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct("StandardFrame", "软件更新"), 0, false);
			foreach (WindowInfo windowInfo in windowListByClassAndName)
			{
				windowInfo.Close(true);
			}
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		private void i()
		{
			List<WinCloseTip> list = WinCloseTip.GetList(this.d);
			if (list != null)
			{
				list.ForEach(new Action<WinCloseTip>(AliwwQn.<>c.<>9.a));
			}
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x0006A644 File Offset: 0x00068844
		private bool h()
		{
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName("#32770", "警告", 0, true, true, false);
			bool flag = false;
			if (windowListByClassAndName != null && windowListByClassAndName.Count > 0)
			{
				foreach (WindowInfo windowInfo in windowListByClassAndName)
				{
					using (Bitmap bitmapFromDC = windowInfo.GetBitmapFromDC(0))
					{
						Util.CollectPicMd5(bitmapFromDC, "警告，千牛已经掉线_");
						int processId = windowInfo.ProcessId;
						if (processId == this.d)
						{
							this.KillProcess();
							flag = true;
						}
						else
						{
							Win32Extend.KillProcessById(processId, null);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x0006A710 File Offset: 0x00068910
		private bool g()
		{
			WindowInfo windowInfo = Win32Extend.FindWindowByClassAndName("StandardFrame", base.UserNick + " - 与服务器连接中断");
			bool flag;
			if (windowInfo != null)
			{
				this.KillProcess();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x0006A750 File Offset: 0x00068950
		private bool f()
		{
			return Win32Extend.FindWindowByClassAndName("StandardFrame", base.UserNick + " - 与服务器连接中断") != null;
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x0006A77C File Offset: 0x0006897C
		private List<string> a(string A_0, string A_1, int A_2 = 20)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < A_2; i++)
			{
				List<VerificationCodeSMS> smsValidCode = AppConfig.GetSmsValidCode(true, A_0, A_1);
				if (smsValidCode != null && smsValidCode.Count > 0)
				{
					smsValidCode.Sort();
					for (int j = smsValidCode.Count - 1; j >= 0; j--)
					{
						string verificationCode = smsValidCode[j].VerificationCode;
						if (!list.Contains(verificationCode))
						{
							list.Add(verificationCode);
						}
					}
					return list;
				}
				Thread.Sleep(1000);
			}
			return list;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x0006A814 File Offset: 0x00068A14
		public ErrCodeInfo CallUser(string toUserNick, string siteId, string toOpenUid)
		{
			ErrCodeInfo errCodeInfo;
			if (AppConfig.AllowAutoLogin && !string.IsNullOrEmpty(this.e.QnAccountPwd))
			{
				try
				{
					if (Win32Extend.KillProcessByNameWithCmd("new_AliWorkbench") && Directory.Exists(AppConfig.AgentQnSetupDir + "new"))
					{
						Directory.Delete(AppConfig.AgentQnSetupDir + "new", true);
					}
				}
				catch (Exception)
				{
				}
				errCodeInfo = base.CallingUserNickQianNiu(toUserNick, siteId, toOpenUid);
			}
			else
			{
				errCodeInfo = base.CallUser(toUserNick, siteId, toOpenUid, ref this.h);
			}
			return errCodeInfo;
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x0006A8B0 File Offset: 0x00068AB0
		public void CloseCurrentChat()
		{
			if (this.h != null)
			{
				this.h.CloseCurrentChat();
			}
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x0006A8D4 File Offset: 0x00068AD4
		private void e()
		{
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct("Qt5152QWindowToolSaveBits", "-千牛智能检测"), 0, true);
			if (!Util.IsEmptyList<WindowInfo>(windowListByClassAndName))
			{
				windowListByClassAndName.ForEach(new Action<WindowInfo>(AliwwQn.<>c.<>9.a));
				Thread.Sleep(200);
			}
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x0006A934 File Offset: 0x00068B34
		public void KillProcess()
		{
			try
			{
				UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(base.UserNick);
				AldsBehavior aldsSession = userCacheOrCreate.AldsSession;
				if (aldsSession != null)
				{
					aldsSession.CloseSession();
				}
				RecentBehavior recentSession = userCacheOrCreate.RecentSession;
				if (recentSession != null)
				{
					recentSession.CloseSession();
				}
				bool flag;
				if (this.h == null && this.g == null)
				{
					WinLoginQnBase winLoginQnBase = this.f;
					if (winLoginQnBase == null || !winLoginQnBase.Visible)
					{
						flag = this.d > 0;
						goto IL_005F;
					}
				}
				flag = true;
				IL_005F:
				if (flag)
				{
					AliwwQn.f f = new AliwwQn.f();
					Win32Extend.KillProcessById(this.d, null);
					f.a = this.d;
					Task.Run(new Action(f.b));
					this.d = 0;
				}
				userCacheOrCreate.ClearRecentSession();
				userCacheOrCreate.ClearAldsSession();
				userCacheOrCreate.LastSendProcessId = 0;
				userCacheOrCreate.LastSendSoftware = MsgSendSoftware.Undefined;
			}
			catch
			{
			}
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x0006AA08 File Offset: 0x00068C08
		public static void smethod_0()
		{
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName("#32770", "千牛工作台", 0, true, true, false);
			foreach (WindowInfo windowInfo in windowListByClassAndName)
			{
				if (windowInfo.Visible)
				{
					WindowInfo windowInfo2 = windowInfo.FindWindowInDescendant("Button", "关闭程序", false, new bool?(false));
					if (windowInfo2 != null && windowInfo2.Visible)
					{
						WindowInfo windowInfo3 = windowInfo.FindWindowInDescendant("Button", "等待程序响应", false, new bool?(false));
						if (windowInfo3 != null && windowInfo3.Visible)
						{
							windowInfo2.Click(true);
							AppConfig.QnAgentServiceClient.AutoLoginError("agiso", null, "关闭程序未响应", 268435455L, "", "");
							Thread.Sleep(200);
						}
					}
				}
			}
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x0006AB0C File Offset: 0x00068D0C
		private void d()
		{
			try
			{
				Screen primaryScreen = Screen.PrimaryScreen;
				global::System.Drawing.Rectangle bounds = primaryScreen.Bounds;
				int width = bounds.Width;
				int height = bounds.Height;
				Image image = new Bitmap(width, height);
				Graphics graphics = Graphics.FromImage(image);
				graphics.CopyFromScreen(new global::System.Drawing.Point(0, 0), new global::System.Drawing.Point(0, 0), new Size(width, height));
				string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ScreenshotMd5");
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				image.Save(Path.Combine(text, "屏幕截图" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg"));
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(string.Format("屏幕截图异常，{0}", ex), 1);
			}
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x0006ABF0 File Offset: 0x00068DF0
		private void a(string A_0)
		{
			string text;
			if (Util.UseCmd("netsh http show sslcert ipport=0.0.0.0:443", out text) && text.Contains("系统找不到指定的文件"))
			{
				if (!string.IsNullOrEmpty(A_0))
				{
					LogWriter.WriteLog(A_0, 1);
				}
				AgentHttpListener agentHttpListenerInstance = Form1.AgentHttpListenerInstance;
				if (agentHttpListenerInstance != null)
				{
					agentHttpListenerInstance.Stop();
				}
				Util.UseCmd("netsh http add sslcert ipport=0.0.0.0:443 certhash=96f6c1dc0a553891b1ebbb146ea3848825d4f568 appid={e19bb691-ed15-4be9-9aa0-65848f780347}");
				Form1.AgentHttpListenerInstance = new AgentHttpListener();
				Form1.AgentHttpListenerInstance.Start();
				List<WinLoginQnBase> list = WinLoginQnBase.GetList();
				if (list != null)
				{
					list.ForEach(new Action<WinLoginQnBase>(AliwwQn.<>c.<>9.a));
				}
			}
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x0000F23E File Offset: 0x0000D43E
		[CompilerGenerated]
		private bool c()
		{
			return this.f.GetCurrPage() != QnLoginPageType.Logining || this.r() != null || this.q() != null;
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x0006AC90 File Offset: 0x00068E90
		[CompilerGenerated]
		private bool b()
		{
			bool flag;
			if (this.f.GetCurrPage() == QnLoginPageType.SelectRole)
			{
				this.f.ClickSelectSellerType();
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x0006ACC0 File Offset: 0x00068EC0
		[CompilerGenerated]
		private bool a()
		{
			return this.r() != null || this.q() != null;
		}

		// Token: 0x04001EB6 RID: 7862
		[CompilerGenerated]
		private static readonly Dictionary<string, int> a = new Dictionary<string, int>();

		// Token: 0x04001EB7 RID: 7863
		private static int b = 0;

		// Token: 0x04001EB8 RID: 7864
		private static Dictionary<string, int> c = new Dictionary<string, int>();

		// Token: 0x04001EB9 RID: 7865
		private int d;

		// Token: 0x04001EBA RID: 7866
		private AldsAccountInfo e;

		// Token: 0x04001EBB RID: 7867
		private WinLoginQnBase f;

		// Token: 0x04001EBC RID: 7868
		private AliwwWorkBenchQn g;

		// Token: 0x04001EBD RID: 7869
		private AliwwTalkWindow h;

		// Token: 0x04001EBE RID: 7870
		private AliwwTalkWindowQn i;

		// Token: 0x04001EBF RID: 7871
		private string j = null;

		// Token: 0x04001EC0 RID: 7872
		private int k = 0;

		// Token: 0x04001EC1 RID: 7873
		[CompilerGenerated]
		private AliwwOptionQn5 l;

		// Token: 0x04001EC2 RID: 7874
		private static Dictionary<string, int> m = new Dictionary<string, int>();

		// Token: 0x04001EC3 RID: 7875
		private static int n = 0;

		// Token: 0x04001EC4 RID: 7876
		private int o = 0;

		// Token: 0x04001EC5 RID: 7877
		private int p = 0;

		// Token: 0x04001EC6 RID: 7878
		private DateTime? q = null;

		// Token: 0x04001EC7 RID: 7879
		private int r = 0;

		// Token: 0x04001EC8 RID: 7880
		private static int s = 0;

		// Token: 0x04001EC9 RID: 7881
		private bool t = false;

		// Token: 0x04001ECA RID: 7882
		private int v = 0;

		// Token: 0x04001ECB RID: 7883
		private int w = 0;

		// Token: 0x02000751 RID: 1873
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x06002596 RID: 9622 RVA: 0x0006AD54 File Offset: 0x00068F54
			internal bool e(string A_0)
			{
				return this.a.Contains(A_0.Substring(0, 3) + "******" + A_0.Substring(9)) || this.a.Contains("*******" + A_0.Substring(7)) || this.a.Contains(A_0.Substring(0, 3) + "****" + A_0.Substring(7));
			}

			// Token: 0x06002597 RID: 9623 RVA: 0x0006ADD0 File Offset: 0x00068FD0
			internal bool f()
			{
				bool flag;
				if (this.b.r() == null && this.b.f.GetCurrPage() == QnLoginPageType.SecurityCheck)
				{
					LoginBehavior loginSession = this.b.LoginSession;
					string text;
					if (loginSession != null)
					{
						if ((text = loginSession.CurrUrl) != null)
						{
							goto IL_0040;
						}
					}
					text = "";
					IL_0040:
					flag = !text.Contains("passport.taobao.com/iv/h5/h_5_identity_verify.htm");
				}
				else
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x06002598 RID: 9624 RVA: 0x0006AE30 File Offset: 0x00069030
			internal bool e()
			{
				bool flag;
				if (this.b.f.GetCurrPage() == QnLoginPageType.SelectRole)
				{
					this.b.f.ClickSelectSellerType();
					flag = false;
				}
				else
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x04001EDA RID: 7898
			public string a;

			// Token: 0x04001EDB RID: 7899
			public AliwwQn b;

			// Token: 0x04001EDC RID: 7900
			public Func<bool> c;

			// Token: 0x04001EDD RID: 7901
			public Func<bool> d;
		}

		// Token: 0x02000752 RID: 1874
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x0600259A RID: 9626 RVA: 0x0006AE6C File Offset: 0x0006906C
			internal bool w()
			{
				return this.a.MainWindowHandle != IntPtr.Zero;
			}

			// Token: 0x0600259B RID: 9627 RVA: 0x0000F2B4 File Offset: 0x0000D4B4
			internal bool v()
			{
				return this.b.GetLoginWin() != null;
			}

			// Token: 0x0600259C RID: 9628 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
			internal bool u()
			{
				return this.b.r() != null;
			}

			// Token: 0x0600259D RID: 9629 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
			internal bool t()
			{
				return !this.b.f();
			}

			// Token: 0x0600259E RID: 9630 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
			internal bool s()
			{
				return !this.b.f();
			}

			// Token: 0x0600259F RID: 9631 RVA: 0x0000F2E4 File Offset: 0x0000D4E4
			internal bool r()
			{
				return this.b.h.FindWindowInDescendant("Chrome_RenderWidgetHostHWND", "Chrome Legacy Window", false, new bool?(false)) != null;
			}

			// Token: 0x060025A0 RID: 9632 RVA: 0x0000F30C File Offset: 0x0000D50C
			internal bool q()
			{
				return !this.b.CurrUserCache.IsAldsSessionNull;
			}

			// Token: 0x060025A1 RID: 9633 RVA: 0x0000F30C File Offset: 0x0000D50C
			internal bool p()
			{
				return !this.b.CurrUserCache.IsAldsSessionNull;
			}

			// Token: 0x04001EDE RID: 7902
			public Process a;

			// Token: 0x04001EDF RID: 7903
			public AliwwQn b;

			// Token: 0x04001EE0 RID: 7904
			public int c;

			// Token: 0x04001EE1 RID: 7905
			public string d;

			// Token: 0x04001EE2 RID: 7906
			public int e;

			// Token: 0x04001EE3 RID: 7907
			public string f;

			// Token: 0x04001EE4 RID: 7908
			public string g;

			// Token: 0x04001EE5 RID: 7909
			public string h;

			// Token: 0x04001EE6 RID: 7910
			public ErrCodeInfo i;

			// Token: 0x04001EE7 RID: 7911
			public Func<bool> j;

			// Token: 0x04001EE8 RID: 7912
			public Func<bool> k;

			// Token: 0x04001EE9 RID: 7913
			public Func<bool> l;

			// Token: 0x04001EEA RID: 7914
			public Func<bool> m;

			// Token: 0x04001EEB RID: 7915
			public Func<bool> n;

			// Token: 0x04001EEC RID: 7916
			public Func<bool> o;
		}

		// Token: 0x02000753 RID: 1875
		[CompilerGenerated]
		private sealed class c
		{
			// Token: 0x060025A3 RID: 9635 RVA: 0x0006AE90 File Offset: 0x00069090
			internal ErrCodeInfo d()
			{
				/*
An exception occurred when decompiling this method (060025A3)

ICSharpCode.Decompiler.DecompilerException: Error decompiling Agiso.Object.ErrCodeInfo Agiso.AliwwApi.Qn.AliwwQn/c::d()

 ---> System.ArgumentOutOfRangeException: length ('-1') must be a non-negative value. (Parameter 'length')
Actual value was -1.
   at System.ArgumentOutOfRangeException.ThrowNegative[T](T value, String paramName)
   at System.Array.CopyImpl(Array sourceArray, Int32 sourceIndex, Array destinationArray, Int32 destinationIndex, Int32 length, Boolean reliable)
   at System.Array.Copy(Array sourceArray, Array destinationArray, Int32 length)
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 51
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 403
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 278
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 117
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 88
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 92
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1683
*/;
			}

			// Token: 0x04001EED RID: 7917
			public DateTime a;

			// Token: 0x04001EEE RID: 7918
			public int b;

			// Token: 0x04001EEF RID: 7919
			public AliwwQn.b c;
		}

		// Token: 0x02000754 RID: 1876
		[CompilerGenerated]
		private sealed class d
		{
			// Token: 0x060025A5 RID: 9637 RVA: 0x0006BF04 File Offset: 0x0006A104
			internal ErrCodeInfo c()
			{
				int i = 0;
				while (i < 60)
				{
					AliwwMessageInfo amiThreadProcessing = Form1.AmiThreadProcessing;
					ErrCodeInfo errCodeInfo;
					if (amiThreadProcessing == null || !amiThreadProcessing.Stop)
					{
						if (this.b.b.q() == null)
						{
							this.b.b.e();
							if (!this.b.b.h())
							{
								int num = 3000;
								Func<bool> func;
								if ((func = this.b.k) == null)
								{
									func = (this.b.k = new Func<bool>(this.b.t));
								}
								if (Util.CheckWait(num, func, 100))
								{
									bool? responding = this.b.b.g.Responding;
									if (responding != null)
									{
										if (responding.Value)
										{
											if ((DateTime.Now - this.a).TotalSeconds <= 20.0)
											{
												if (!AliwwErrorReport.CloseQn(this.b.b.d))
												{
													if (!WinQnStopWork.CloseAll(this.b.b.d))
													{
														if (i > 10)
														{
															if (this.b.b.l())
															{
																this.b.i = new ErrCodeInfo(ErrCodeType.CallFailTargetNickReceiveFriendOnly);
															}
															if (this.b.b.k())
															{
																this.b.i = new ErrCodeInfo(ErrCodeType.CallFailTargetNickRefuseToAddFriend);
															}
															if (this.b.b.a(true))
															{
																AliwwQn aliwwQn = this.b.b;
																int num2 = this.b.b.k + 1;
																aliwwQn.k = num2;
																if (num2 == 2)
																{
																	return new ErrCodeInfo(ErrCodeType.CallFailTargetNickInBlackListOrNotExists);
																}
															}
															if (i == 30)
															{
																if (!string.IsNullOrEmpty(this.b.h))
																{
																	this.b.b.CallingUserNickProtocol("**", this.b.g, this.b.h);
																}
																else
																{
																	this.b.b.CallingUserNickProtocol(this.b.f, this.b.g, this.b.h);
																}
															}
														}
														if (i % 10 == 5)
														{
															this.b.b.CallUser(this.b.f, this.b.g, this.b.h);
														}
														Thread.Sleep(300);
														i++;
														continue;
													}
													errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailBenchWinHasFoundButCrash);
												}
												else
												{
													errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailAliwwErrorReport);
												}
											}
											else
											{
												this.b.b.KillProcess();
												errCodeInfo = new ErrCodeInfo(ErrCodeType.CallTalkWinTimeOutInTalkWin, this.b.b.UserNick + "，呼叫聊天窗口超过20s");
											}
										}
										else
										{
											this.b.b.KillProcess();
											errCodeInfo = new ErrCodeInfo(ErrCodeType.QnNoResponse, "工作台没有响应");
										}
									}
									else
									{
										errCodeInfo = new ErrCodeInfo(ErrCodeType.QnWorkbenchWinBeClosed);
									}
								}
								else
								{
									this.b.b.KillProcess();
									errCodeInfo = new ErrCodeInfo(ErrCodeType.QnLinkageInterrupt);
								}
							}
							else
							{
								this.b.b.KillProcess();
								errCodeInfo = new ErrCodeInfo(ErrCodeType.MaybeQnOffline);
							}
						}
						else
						{
							errCodeInfo = null;
						}
					}
					else
					{
						errCodeInfo = new ErrCodeInfo(ErrCodeType.MsgWasStoppedWhileSending);
					}
					return errCodeInfo;
				}
				if (this.b.b.g())
				{
					return new ErrCodeInfo(ErrCodeType.QnLinkageInterrupt);
				}
				return this.b.i;
			}

			// Token: 0x04001EF0 RID: 7920
			public DateTime a;

			// Token: 0x04001EF1 RID: 7921
			public AliwwQn.b b;
		}

		// Token: 0x02000755 RID: 1877
		[CompilerGenerated]
		private sealed class e
		{
			// Token: 0x060025A7 RID: 9639 RVA: 0x0006C2C0 File Offset: 0x0006A4C0
			internal ErrCodeInfo j()
			{
				int num = -1;
				ErrCodeInfo errCodeInfo;
				string text;
				string text4;
				for (;;)
				{
					num++;
					this.i.b.e();
					AliwwMessageInfo amiThreadProcessing = Form1.AmiThreadProcessing;
					if (amiThreadProcessing != null && amiThreadProcessing.Stop)
					{
						goto IL_0FA5;
					}
					int num2 = 3000;
					Func<bool> func;
					if ((func = this.i.l) == null)
					{
						func = (this.i.l = new Func<bool>(this.i.s));
					}
					if (!Util.CheckWait(num2, func, 100))
					{
						goto IL_0F87;
					}
					bool? responding = this.i.b.h.Responding;
					if (responding == null)
					{
						goto IL_0F79;
					}
					if (!responding.Value)
					{
						goto IL_0F55;
					}
					bool flag;
					if (this.i.b.CurrUserCache.IsRecentSessionNull)
					{
						int num3 = 10000;
						Func<bool> func2;
						if ((func2 = this.i.m) == null)
						{
							func2 = (this.i.m = new Func<bool>(this.i.r));
						}
						flag = !Util.CheckWait(num3, func2, 200);
					}
					else
					{
						flag = false;
					}
					if (!flag)
					{
						if (num == 0)
						{
							goto IL_0110;
						}
						if (num == 5)
						{
							goto IL_0110;
						}
						bool flag2 = false;
						IL_0123:
						if (flag2)
						{
							this.i.b.CallingUserNickProtocol("**", this.i.g, this.i.h);
						}
						else if (num % 8 == 0)
						{
							this.i.b.CallUser(this.i.f, this.i.g, (!string.IsNullOrEmpty(this.a)) ? this.a : this.i.h);
						}
						else if (num % 5 == 1)
						{
							if (this.i.b.l())
							{
								this.i.i = new ErrCodeInfo(ErrCodeType.CallFailTargetNickReceiveFriendOnly);
							}
							if (this.i.b.k())
							{
								this.i.i = new ErrCodeInfo(ErrCodeType.CallFailTargetNickRefuseToAddFriend);
							}
							if (this.i.b.a(true))
							{
								AliwwQn aliwwQn = this.i.b;
								int num4 = this.i.b.k + 1;
								aliwwQn.k = num4;
								if (num4 == 2)
								{
									goto IL_095A;
								}
							}
							if (this.i.i.ErrCode == ErrCodeType.CallFailTargetNickReceiveFriendOnly || this.i.i.ErrCode == ErrCodeType.CallFailTargetNickRefuseToAddFriend)
							{
								int num4 = this.b + 1;
								this.b = num4;
								if (num4 >= 2)
								{
									goto IL_096B;
								}
							}
						}
						if (this.i.b.h())
						{
							goto IL_0F23;
						}
						if (AliwwErrorReport.CloseQn(this.i.b.d))
						{
							goto IL_0F12;
						}
						if (WinQnStopWork.CloseAll(this.i.b.d))
						{
							goto IL_0F01;
						}
						if (this.i.b.CurrUserCache.IsSessionNull)
						{
							Thread.Sleep(200);
						}
						else
						{
							if (this.i.b.CurrUserCache.IsAldsSessionNull && !this.i.b.CurrUserCache.IsRecentSessionNull)
							{
								long userId = this.i.b.CurrUserCache.RecentSession.UserId;
								if (userId > 0L && !QnUserDbManager.IsAldsPluginFirst(userId))
								{
									QnUserDbManager.SetAldsPluginFirst(new long?(userId));
								}
							}
							bool flag3;
							if (this.i.b.CurrUserCache.IsAldsSessionNull && !this.i.b.CurrUserCache.IsRecentSessionNull)
							{
								QnAgentInfo agentInfo = AppConfig.GetAgentInfo(Util.GetMasterNick(this.i.b.UserNick));
								flag3 = ((agentInfo != null) ? agentInfo.LongOpen : ((DateTime.Now - this.c).TotalSeconds >= 15.0 || this.i.d.IsActivateMsg()));
							}
							else
							{
								flag3 = false;
							}
							if (flag3)
							{
								if ((errCodeInfo = this.d.ClickAldsPlugIn()) != null)
								{
									if (errCodeInfo.ErrCode != ErrCodeType.AldsPlugNotFound)
									{
										goto IL_097D;
									}
									if (this.i.f.Contains("**"))
									{
										int num4 = this.e + 1;
										this.e = num4;
										if (this.e == 5 && this.i.b.CurrUserCache.AldsPlugNoticeTime.AddHours(24.0) <= DateTime.Now)
										{
											this.i.b.CurrUserCache.AldsPlugNoticeTime = DateTime.Now;
											AppConfig.QnAgentServiceClient.AutoLoginError(this.i.b.UserNick, null, "", 268435455L, "\t\t您好，您的代挂子账号未设置自动发货插件，请登录代挂子账号添加插件。\r\n\t\t如未添加插件，可能会导致消息发送失败。\r\n\t\t如已添加插件还是提示，请去掉不相关插件。\r\n\t\t如有疑问，请联系千牛在线客服“agiso”", "https://www.yuque.com/agiso/aldstb/bicdskond0xkc740");
										}
									}
								}
								Thread.Sleep(1000);
							}
							if (this.i.d.IsActivateMsg())
							{
								int num5 = 5000;
								Func<bool> func3;
								if ((func3 = this.i.n) == null)
								{
									func3 = (this.i.n = new Func<bool>(this.i.q));
								}
								if (!Util.CheckWait(num5, func3, 200))
								{
									this.d.ClickAldsPlugIn();
									int num6 = 5000;
									Func<bool> func4;
									if ((func4 = this.i.o) == null)
									{
										func4 = (this.i.o = new Func<bool>(this.i.p));
									}
									if (!Util.CheckWait(num6, func4, 200))
									{
										goto IL_0986;
									}
								}
							}
							if (string.IsNullOrEmpty(this.a) && this.i.b.CurrUserCache.IsAldsSessionNull && this.i.f.Contains("**") && !string.IsNullOrEmpty(this.i.h))
							{
								if (this.f == null)
								{
									this.f = this.i.b.CurrUserCache.RecentSession.GetActiveUserInfo();
								}
								if (this.f != null)
								{
									text = "";
									BuyerNickGetManager buyerNickGetManager = new BuyerNickGetManager(this.i.f, this.i.h);
									string text2 = buyerNickGetManager.GetBuyerNick(out text, 3);
									if (string.IsNullOrEmpty(text))
									{
										text2 = Util.GetMasterNick(text2);
										if ("cntaobao" + text2 == this.f.Uid)
										{
											this.a = this.f.SecurityUID;
											AppConfig.BuyerInfoCache.Add(text2, this.i.h, this.f.SecurityUID);
										}
										else
										{
											string displayNick = this.i.b.CurrUserCache.RecentSession.GetDisplayNick("**", this.f.SecurityUID);
											if (text2 == displayNick)
											{
												this.a = this.f.SecurityUID;
												AppConfig.BuyerInfoCache.Add(displayNick, this.i.h, this.f.SecurityUID);
											}
											else
											{
												LogWriter.WriteLog(string.Concat(new string[]
												{
													"buyerNick : ",
													text2,
													", displayNick: ",
													displayNick,
													", activateNick: ",
													this.f.Uid
												}), 1);
											}
										}
									}
									if (string.IsNullOrEmpty(this.a) && string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(this.f.Uid) && !this.f.Uid.Contains("**"))
									{
										string buyerNickByAldsOpenUid = AppConfig.BuyerInfoCache.GetBuyerNickByAldsOpenUid(this.i.h);
										if (!string.IsNullOrEmpty(buyerNickByAldsOpenUid))
										{
											if ("cntaobao" + buyerNickByAldsOpenUid == this.f.Uid)
											{
												this.a = this.f.SecurityUID;
											}
										}
										else
										{
											string text3 = this.f.Uid.Substring(8);
											AgentGetOpenUidByNickResponse openUidByNick = AppConfig.QnAgentServiceClient.GetOpenUidByNick(text3);
											if (!openUidByNick.IsError)
											{
												if (openUidByNick.OpenUid == this.i.h)
												{
													AppConfig.BuyerInfoCache.Add(text3, this.i.h, this.f.SecurityUID);
													this.a = this.f.SecurityUID;
												}
											}
											else if (!string.IsNullOrEmpty(text))
											{
												goto IL_0A2A;
											}
										}
									}
								}
							}
							text4 = ((!string.IsNullOrEmpty(this.a)) ? this.a : this.i.h);
							BehaviorBase session = this.i.b.CurrUserCache.GetSession(text4);
							if (session != null && session.CheckCallUserInfoEqualsByQnApi(this.i.f, text4, out this.f))
							{
								goto IL_0A3D;
							}
							Application.DoEvents();
							Thread.Sleep(200);
						}
						if ((DateTime.Now - this.c).TotalSeconds >= (double)this.h)
						{
							break;
						}
						continue;
						IL_0110:
						flag2 = !string.IsNullOrEmpty(this.i.h);
						goto IL_0123;
					}
					goto IL_0F41;
				}
				goto IL_0C7D;
				IL_095A:
				return new ErrCodeInfo(ErrCodeType.CallFailTargetNickInBlackListOrNotExists);
				IL_096B:
				return this.i.i;
				IL_097D:
				return errCodeInfo;
				IL_0986:
				IEnumerable<string> enumerable = AppConfig.AliwwWebScoketServer.ServerList.SelectMany(new Func<AgisoWebSocketServer, IEnumerable<IWebSocketSession>>(AliwwQn.<>c.<>9.a)).Cast<BehaviorBase>().Select(new Func<BehaviorBase, string>(AliwwQn.<>c.<>9.a));
				LogWriter.WriteLog("userNick: " + this.i.b.CurrUserCache.UserNick + ", " + string.Join("，", enumerable), 1);
				return new ErrCodeInfo(ErrCodeType.AldsPlugNotFound);
				IL_0A2A:
				return new ErrCodeInfo(ErrCodeType.GetBuyerNickFail, text);
				IL_0A3D:
				if (this.i.b.Option.IsOnlyCall)
				{
					this.i.i = new ErrCodeInfo(ErrCodeType.CallSuccQn);
					if (this.i.b.Option.IsNeedScreenshot)
					{
						string text5 = AppDomain.CurrentDomain.BaseDirectory + "\\AldsScreenshot";
						if (!Directory.Exists(text5))
						{
							Directory.CreateDirectory(text5);
						}
						this.i.b.h.SetMaximizeWindow();
						Thread.Sleep(1000);
						using (Bitmap bitmapFromDC = this.i.b.h.GetBitmapFromDC(0))
						{
							bitmapFromDC.Save(text5 + "/" + this.i.b.Option.ScreenshotFileName + ".jpg", ImageFormat.Jpeg);
						}
						this.i.b.h.SetRestoreWindow();
					}
					return this.i.i;
				}
				string[] array = this.i.d.Split(new string[] { "\r\t\r\n", "\n\t\n", "{$旺旺分段符}" }, StringSplitOptions.RemoveEmptyEntries);
				this.g = array.Length;
				int num7 = this.g * 3 + 15;
				if (num7 > this.h)
				{
					this.h = ((num7 > 60) ? 60 : num7);
				}
				this.i.i = this.i.b.h.SendToTalkWindowWholeMsg(this.i.f, text4, this.i.d, "cntaobao");
				if (this.i.i.ErrCode == ErrCodeType.SendSucc && AppConfig.AgentSettings.AutoMinimizeTalkWindow)
				{
					this.i.b.h.SetMinimizeWindow();
				}
				this.i.i = this.i.b.CheckMsgIsSendSucc(this.i.b.h, this.i.d, this.i.f, this.i.i);
				IL_0C7D:
				if (this.i.b.g())
				{
					return new ErrCodeInfo(ErrCodeType.QnLinkageInterrupt);
				}
				if (this.i.b.CurrUserCache.IsRecentSessionNull)
				{
					string text6 = "未找到连接：{0}\r\n";
					WindowInfo windowInfo = this.i.b.h.FindWindowInDescendant("StandardButton", "切换气泡模式", false, new bool?(false));
					bool? flag4;
					if (windowInfo == null)
					{
						flag4 = null;
					}
					else
					{
						WindowInfo parentWin = windowInfo.GetParentWin();
						if (parentWin == null)
						{
							flag4 = null;
						}
						else
						{
							WindowInfo parentWin2 = parentWin.GetParentWin();
							if (parentWin2 == null)
							{
								flag4 = null;
							}
							else
							{
								WindowInfo parentWin3 = parentWin2.GetParentWin();
								flag4 = ((parentWin3 != null) ? new bool?(parentWin3.Visible) : null);
							}
						}
					}
					bool? flag5 = flag4;
					string text7 = string.Format(text6, flag5.GetValueOrDefault());
					WindowTreeNode treeNode = this.i.b.h.GetTreeNode();
					LogWriter.WriteLog(text7 + ((treeNode != null) ? treeNode.WriteTreeNode("") : null), 1);
					return new ErrCodeInfo(ErrCodeType.FindTalkWindowButRecentIsNull);
				}
				if ((DateTime.Now - this.c).TotalSeconds <= (double)this.h)
				{
					ErrCodeInfo errCodeInfo2 = this.i.i;
					if (errCodeInfo2 != null && errCodeInfo2.ErrCode == ErrCodeType.CallFailCheckTargetNickUnEquals)
					{
						LogWriter.WriteLog(string.Concat(new string[]
						{
							"“",
							this.i.b.UserNick,
							"”查找到聊天窗口，但检验聊天人不通过，aldsOpenUid：",
							this.i.h,
							"，recentOpenUid：",
							this.a,
							"，",
							(this.f == null) ? "获取到聊天人信息为空" : JSON.Encode(this.f)
						}), 1);
						if (this.i.f == "x**")
						{
							return new ErrCodeInfo(ErrCodeType.SendFailOnlyIdleBuyerNotTbBuyer);
						}
					}
					return this.i.i;
				}
				if (this.i.f == "x**")
				{
					return new ErrCodeInfo(ErrCodeType.SendFailOnlyIdleBuyerNotTbBuyer);
				}
				this.i.b.KillProcess();
				if (this.g >= 15)
				{
					return new ErrCodeInfo(ErrCodeType.SendMsgTimeOutInTalkWinOfTooManyMsg);
				}
				return new ErrCodeInfo(ErrCodeType.SendMsgTimeOutInTalkWin);
				IL_0F01:
				return new ErrCodeInfo(ErrCodeType.SendFailBenchWinHasFoundButCrash);
				IL_0F12:
				return new ErrCodeInfo(ErrCodeType.SendFailAliwwErrorReport);
				IL_0F23:
				this.i.b.KillProcess();
				return new ErrCodeInfo(ErrCodeType.MaybeQnOffline);
				IL_0F41:
				return new ErrCodeInfo(ErrCodeType.FindTalkWindowButRecentIsNull, "聊天窗口的chrome窗口未加载出来");
				IL_0F55:
				this.i.b.KillProcess();
				return new ErrCodeInfo(ErrCodeType.QnNoResponse, "聊天窗口没有响应");
				IL_0F79:
				return new ErrCodeInfo(ErrCodeType.QnAliTalkWinBeClosed);
				IL_0F87:
				this.i.b.KillProcess();
				return new ErrCodeInfo(ErrCodeType.QnLinkageInterrupt);
				IL_0FA5:
				return new ErrCodeInfo(ErrCodeType.MsgWasStoppedWhileSending);
			}

			// Token: 0x04001EF2 RID: 7922
			public string a;

			// Token: 0x04001EF3 RID: 7923
			public int b;

			// Token: 0x04001EF4 RID: 7924
			public DateTime c;

			// Token: 0x04001EF5 RID: 7925
			public IPlugInContainerManager d;

			// Token: 0x04001EF6 RID: 7926
			public int e;

			// Token: 0x04001EF7 RID: 7927
			public ActiveUserInfo f;

			// Token: 0x04001EF8 RID: 7928
			public int g;

			// Token: 0x04001EF9 RID: 7929
			public int h;

			// Token: 0x04001EFA RID: 7930
			public AliwwQn.b i;
		}

		// Token: 0x02000756 RID: 1878
		[CompilerGenerated]
		private sealed class f
		{
			// Token: 0x060025A9 RID: 9641 RVA: 0x0000F321 File Offset: 0x0000D521
			internal void b()
			{
				Win32Extend.KillProcessAndChildrenById(this.a, 2, "AliApp");
				Win32Extend.KillProcessAndChildrenById(this.a, 2, "AliRender");
			}

			// Token: 0x04001EFB RID: 7931
			public int a;
		}
	}
}
