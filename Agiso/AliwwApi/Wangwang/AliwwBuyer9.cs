using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agiso.AliwwApi.Object;
using Agiso.AliwwApi.Qn;
using Agiso.AliwwApi.WinAlert;
using Agiso.Extensions;
using Agiso.Handler;
using Agiso.Manager;
using Agiso.Object;
using Agiso.Utils;
using Agiso.Windows;
using Agiso.WwService.Sdk;
using Agiso.WwService.Sdk.Domain;
using AliwwClient;
using AliwwClient.Cache;

namespace Agiso.AliwwApi.Wangwang
{
	// Token: 0x02000739 RID: 1849
	public class AliwwBuyer9 : Aliww, IIM
	{
		// Token: 0x060024B6 RID: 9398 RVA: 0x000639AC File Offset: 0x00061BAC
		private void a(string A_0)
		{
			this.h.AppendFormat("{0}\t{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), A_0);
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000639E0 File Offset: 0x00061BE0
		public bool IsAlreadyCollectedImageMd5(string md5)
		{
			return AliwwBuyer9.ACCOUNT_PROTECTED.Contains(md5) || AliwwBuyer9.ACCOUNT_NOT_EXISTS.Contains(md5) || AliwwBuyer9.ACCOUNT_NEED_SECURITY_CHECK.Contains(md5) || AliwwBuyer9.ACCOUNT_LIMITED2.Contains(md5) || AliwwBuyer9.PASSWORD_ERROR_MD5.Contains(md5) || AliwwBuyer9.PASSWORD_NOT_INPUT.Contains(md5) || AliwwBuyer9.PASSWORD_AUTO_EXPIRE.Contains(md5) || AliwwBuyer9.AUTO_LOGIN_FAILED.Contains(md5) || AliwwBuyer9.ACCOUNT_NOT_INPUT.Contains(md5) || AliwwBuyer9.ACCOUNT_LIMITED.Contains(md5) || AliwwBuyer9.ACCOUNT_SUB_EXCEPTION.Contains(md5) || AliwwBuyer9.REQUEST_LIMIT_MD5.Contains(md5) || AliwwBuyer9.NONE_ERROR_MD5.Contains(md5) || AliwwBuyer9.NETWORK_ERROR_MD5.Contains(md5) || AliwwBuyer9.NETWORK_TIMEOUT_MD5.Contains(md5) || AliwwBuyer9.CAPTCHA_ERROR_MD5.Contains(md5) || AliwwBuyer9.CAPTCHA_ERROR_PLASE_RETRY_MD5.Contains(md5) || AliwwBuyer9.CAPTCHA_REQUEST_CONTINUAL_MD5.Contains(md5) || AliwwBuyer9.SYSTEM_ERROR.Contains(md5) || AliwwBuyer9.PLEASE_REQUEST_CAPTCHA_FIRST.Contains(md5) || AliwwBuyer9.UNKNOW_ERROR.Contains(md5) || AliwwBuyer9.b.Contains(md5) || AliwwBuyer9.c.Contains(md5) || AliwwBuyer9.d.Contains(md5) || AliwwBuyer9.f.Contains(md5);
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x00063B6C File Offset: 0x00061D6C
		public MsgSendSoftware SendSoftware
		{
			get
			{
				return MsgSendSoftware.AliwwBuyer9;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x0000EFEF File Offset: 0x0000D1EF
		// (set) Token: 0x060024BA RID: 9402 RVA: 0x0000EFF7 File Offset: 0x0000D1F7
		public AliwwOptionQn5 Option { get; set; } = new AliwwOptionQn5();

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060024BB RID: 9403 RVA: 0x00063B80 File Offset: 0x00061D80
		public override AliwwVersion AliwwVersion
		{
			get
			{
				return AliwwVersion.AliwwBuyer9;
			}
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x00063B90 File Offset: 0x00061D90
		public AliwwBuyer9(AldsAccountInfo account)
			: base((account == null) ? null : account.UserNick)
		{
			this.n = account;
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x00063C28 File Offset: 0x00061E28
		public AliwwBuyer9(AldsAccountInfo account, string userSiteId)
			: base(account.UserNick, userSiteId)
		{
			this.n = account;
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x00063CBC File Offset: 0x00061EBC
		public WinAliwwMainBuyer9 GetMainWindow()
		{
			if (this.p == null)
			{
				this.GetMainWindow(0);
			}
			return this.p;
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x00063CE4 File Offset: 0x00061EE4
		public void GetMainWindow(int processId)
		{
			List<WinAliwwMainBuyer9> list = WinAliwwMainBuyer9.GetList(processId);
			if (!Util.IsEmptyList<WinAliwwMainBuyer9>(list))
			{
				foreach (WinAliwwMainBuyer9 winAliwwMainBuyer in list)
				{
					if (!string.IsNullOrEmpty(winAliwwMainBuyer.Nick) && AppConfig.CheckUserNickEqual(this.n.UserNick, winAliwwMainBuyer.Nick))
					{
						this.p = winAliwwMainBuyer;
						break;
					}
				}
				if (processId > 0 && this.p == null)
				{
					this.p = list[0];
					if (list[0].IsUndefined)
					{
						LogWriter.WriteLog("旺旺登录成功，但显示Nick是Undefined，Nick：" + list[0].Nick, 1);
					}
				}
				if (this.p != null)
				{
					this.l = this.p.ProcessId;
				}
			}
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x00063DD8 File Offset: 0x00061FD8
		public AliwwTalkWindow GetAliwwTalkWin()
		{
			if (this.q == null)
			{
				this.q = AliwwTalkWindow.ParseFromWindowInfo(Win32Extend.FindWindowByClassAndName("StandardFrame", "阿里旺旺 - " + base.UserNick.Trim()));
				if (this.q != null)
				{
					this.q.UserNick = base.UserNick;
					this.q.CurrentAliww = this;
					this.q.AliVersion = AliwwVersion.AliwwBuyer9;
					this.l = this.q.ProcessId;
				}
			}
			return this.q;
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x00063E6C File Offset: 0x0006206C
		public WinLoginBuyer9 GetLoginWin()
		{
			if (this.o == null)
			{
				this.o = WinLoginBuyer9.GetList().FirstOrDefault(new Func<WinLoginBuyer9, bool>(AliwwBuyer9.<>c.<>9.d));
				if (this.o != null)
				{
					this.l = this.o.ProcessId;
				}
			}
			return this.o;
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x00063ED8 File Offset: 0x000620D8
		public WindowInfo GeTipWindow()
		{
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct
			{
				ClassName = "#32770",
				WindowName = "提示"
			}, this.l, false);
			foreach (WindowInfo windowInfo in windowListByClassAndName)
			{
				if (windowInfo.FindWindowsInDescendant("StandardButton", "确认", false, new bool?(false)).Count > 0)
				{
					return windowInfo;
				}
			}
			return null;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x00063F78 File Offset: 0x00062178
		public WindowInfo GetLoginWinArea()
		{
			if (this.o != null)
			{
				WindowInfo loginAreaWin = this.o.LoginAreaWin;
				if (loginAreaWin != null)
				{
					return loginAreaWin.Visible ? loginAreaWin : null;
				}
			}
			return null;
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x00063FB4 File Offset: 0x000621B4
		public AliwwTalkWindowBuyer9 GetAliwwTalkWindowBuyer9(WindowInfo source)
		{
			AliwwTalkWindowBuyer9 aliwwTalkWindowBuyer;
			if (source == null)
			{
				aliwwTalkWindowBuyer = null;
			}
			else
			{
				aliwwTalkWindowBuyer = source.Convert<AliwwTalkWindowBuyer9>();
			}
			return aliwwTalkWindowBuyer;
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x00063FD4 File Offset: 0x000621D4
		public ErrCodeInfo SendMsg(string toUserNick, string toOpenUid, string msgBody, string siteId = "cntaobao")
		{
			BuyerNickGetManager buyerNickGetManager = new BuyerNickGetManager(toUserNick, toOpenUid);
			string text;
			toUserNick = buyerNickGetManager.GetBuyerNick(out text, 3);
			ErrCodeInfo errCodeInfo;
			if (!string.IsNullOrEmpty(text))
			{
				errCodeInfo = new ErrCodeInfo(ErrCodeType.GetBuyerNickFail, text);
			}
			else
			{
				DateTime now = DateTime.Now;
				int num = 0;
				int num2 = 0;
				List<WinFloatBase> list = WinFloatBase.GetList();
				if (list != null)
				{
					list.ForEach(new Action<WinFloatBase>(AliwwBuyer9.<>c.<>9.a));
				}
				WinQnStopWork.CloseAll(0);
				AliwwErrorReport.CloseWw(0);
				List<WinLoginBuyer9> list2 = WinLoginBuyer9.GetList().Where(new Func<WinLoginBuyer9, bool>(AliwwBuyer9.<>c.<>9.c)).ToList<WinLoginBuyer9>();
				if (list2.Count > 0)
				{
					foreach (WinLoginBuyer9 winLoginBuyer in list2)
					{
						winLoginBuyer.Close(true);
					}
					if (!Util.CheckWait(2000, new Func<bool>(AliwwBuyer9.<>c.<>9.b), 300))
					{
						LogWriter.WriteLog("找到空闲的聊天窗口，2s内还未关闭", 1);
					}
				}
				WindowInfo windowInfo = AliwwTalkWindowQn.Get(base.UserNick);
				if (windowInfo != null)
				{
					windowInfo.KillProcess();
				}
				else
				{
					windowInfo = AliwwWorkBenchQn.Get(base.UserNick);
					if (windowInfo != null)
					{
						windowInfo.KillProcess();
					}
				}
				int num3 = 2;
				int i = 0;
				while (i < 50)
				{
					if (this.GetAliwwTalkWin() == null)
					{
						if (this.GetMainWindow() == null)
						{
							if ((DateTime.Now - now).TotalSeconds > 20.0)
							{
								return new ErrCodeInfo(ErrCodeType.SendFailWwLoginWinNotFoundWhileAutoLogin);
							}
							if (i < num3 || this.GetLoginWin() == null)
							{
								if (i == num3 && this.GetLoginWin() == null)
								{
									AliwwBuyer9.a a = new AliwwBuyer9.a();
									this.a("GetLoginWinArea null");
									WindowInfo windowInfo2 = Win32Extend.FindWindowByClassAndName("StandardFrame", "阿里旺旺 - ");
									if (windowInfo2 != null)
									{
										string text2 = "invalidTalkWin found:: ";
										WindowTreeNode treeNode = windowInfo2.GetTreeNode();
										this.a(text2 + ((treeNode != null) ? treeNode.WriteTreeNode("") : null));
										windowInfo2.KillProcessAndChildren("AliExternal");
									}
									if (!File.Exists(AppConfig.GetExeFullFileNameAliwwNew()))
									{
										goto IL_02E7;
									}
									try
									{
										File.Delete(AppConfig.GetExeFullFileNameAliwwNew());
										goto IL_02E7;
									}
									catch
									{
										goto IL_02E7;
									}
									IL_0266:
									this.o = new WindowInfo(a.a.MainWindowHandle).Convert<WinLoginBuyer9>();
									if (this.GetLoginWinArea() == null)
									{
										LogWriter.WriteLog("旺旺呼叫登录窗成功，但是窗口无效", 1);
										using (Bitmap bitmapFromDC = this.o.GetBitmapFromDC(0))
										{
											Util.CollectPicMd5(bitmapFromDC, "旺旺呼叫登录窗成功，但是无效_");
										}
										this.o = null;
										goto IL_02CD;
									}
									this.l = a.a.Id;
									goto IL_03B2;
									IL_02E7:
									a.a = Process.Start(AppConfig.GetExeFullFileNameAliww(), "/run:desktop");
									if (Util.CheckWait(13000, new Func<bool>(a.b), 100))
									{
										goto IL_0266;
									}
									this.a("UnStrat! ");
									if (this.GetLoginWin() == null)
									{
										return new ErrCodeInfo(ErrCodeType.StartWwTimeout);
									}
									goto IL_03B2;
								}
								IL_02CD:
								Application.DoEvents();
								Thread.Sleep(300);
								i++;
								continue;
							}
							IL_03B2:
							this.a("__TAG_LOGINWIN_FOUND");
							DateTime now2 = DateTime.Now;
							int num4 = 0;
							int num5 = 45;
							int num6 = 0;
							int num7 = 0;
							int num8 = 0;
							for (int j = 0; j < 31; j++)
							{
								this.a(string.Format("{0}----GetAliwwTalkWin() == null && GetMainWindow() == null", j));
								AliwwMessageInfo amiThreadProcessing = Form1.AmiThreadProcessing;
								if (amiThreadProcessing != null && amiThreadProcessing.Stop)
								{
									return new ErrCodeInfo(ErrCodeType.MsgWasStoppedWhileSending);
								}
								if (this.GetAliwwTalkWin() != null)
								{
									if (!string.IsNullOrEmpty(this.z))
									{
										AppConfig.QnAgentServiceClient.RemoveVerificationCodeSMS(this.z, this.t);
									}
									Interlocked.Exchange(ref AliwwBuyer9.WwLoginErrorAccountNum, 0);
									goto IL_0FB5;
								}
								if (this.GetMainWindow() != null)
								{
									if (!string.IsNullOrEmpty(this.z))
									{
										AppConfig.QnAgentServiceClient.RemoveVerificationCodeSMS(this.z, this.t);
									}
									Interlocked.Exchange(ref AliwwBuyer9.WwLoginErrorAccountNum, 0);
									break;
								}
								if ((DateTime.Now - now2).TotalSeconds > (double)num5 || j == 30)
								{
									LogWriter.WriteLog(this.h.ToString(), 1);
									if (this.o != null)
									{
										using (Bitmap bitmapFromDC2 = this.o.GetBitmapFromDC(0))
										{
											FileItem fileItem = new FileItem("loginTooLong.jpg", Util.smethod_1(bitmapFromDC2, ImageFormat.Jpeg), "image/jpeg");
											AppConfig.QnAgentServiceClient.AutoLoginError(base.UserNick, fileItem, (j == 30) ? "尝试旺旺自动登录时，尝试太久了，尝试了30次了" : string.Format("尝试旺旺自动登录时，尝试太久了, 用时超过{0}", num5), 268435455L, "", "");
										}
									}
									this.KillProcess();
									return new ErrCodeInfo(ErrCodeType.SendFailWwTryToLoginTooManySeconds);
								}
								if (AliwwErrorReport.CloseWw(this.l))
								{
									return new ErrCodeInfo(ErrCodeType.SendFailAliwwErrorReport);
								}
								if (WinQnStopWork.CloseAll(this.l))
								{
									return new ErrCodeInfo(ErrCodeType.SendFailBenchWinHasFoundButCrash);
								}
								List<WinLoginBuyer9> list3 = WinLoginBuyer9.GetList();
								if (!list3.ContainWindow(this.o))
								{
									if (++num4 > 10)
									{
										return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginWinHasBeenCloseByManual);
									}
									Thread.Sleep(100);
								}
								else
								{
									if (this.o.Visible)
									{
										this.a("----DoLogin()----");
										ErrCodeInfo errCodeInfo2 = this.DoLogin(msgBody);
										ErrCodeType errCode = errCodeInfo2.ErrCode;
										ErrCodeType errCodeType = errCode;
										switch (errCodeType)
										{
										case ErrCodeType.SendFailWwLoginErrorNetworkError:
											if (this.w > 2)
											{
												this.o.KillProcess();
												this.a("NetworkError reset loginWin!");
												return errCodeInfo2;
											}
											goto IL_06AF;
										case ErrCodeType.SendFailWwLoginErrorComponentShowTimesTooMany:
										case ErrCodeType.SendFailWwLoginWinNotFoundWhileAutoLogin:
										case ErrCodeType.LoginAliwwSuccessButNickIsUndefined:
										case ErrCodeType.SpecialEditionLoginError:
										case ErrCodeType.LoginQnErrorNeedToRestart:
										case ErrCodeType.StartQnTimeout:
										case ErrCodeType.LoginQnTimeout:
										case ErrCodeType.SecurityCheckError:
										case ErrCodeType.SecurityCheckFail:
										case ErrCodeType.StartWwTimeout:
										case ErrCodeType.LoginWinNotFound:
											goto IL_06AF;
										case ErrCodeType.MobilePhoneAuthenticationIsNotBound:
											if (this.y >= 4)
											{
												this.o.KillProcess();
												return errCodeInfo2;
											}
											goto IL_06AF;
										case ErrCodeType.StillOnLoginWinArea:
										case ErrCodeType.ResetLoginFormFail:
											if (++num7 > 5)
											{
												this.o.KillProcess();
												return errCodeInfo2;
											}
											goto IL_06AF;
										case ErrCodeType.RequestLimit:
											this.o.KillProcess();
											this.a("RequestLimit reset loginWin!");
											return new ErrCodeInfo(ErrCodeType.RequestLimit);
										case ErrCodeType.LoadMobileCaptchaWinTimeOut:
										case ErrCodeType.GetMobileCaptchaTimeout:
										case ErrCodeType.LoginAliwwFindError:
											this.a("switch error 3 case.\r\n");
											if (num++ <= 8)
											{
												goto IL_06AF;
											}
											if (AliwwBuyer9.m.ContainsKey(this.n.UserNick))
											{
												int num9 = AliwwBuyer9.m[this.n.UserNick];
												AliwwBuyer9.m[this.n.UserNick] = num9 + 1;
											}
											else
											{
												AliwwBuyer9.m.Add(this.n.UserNick, 1);
											}
											if (AliwwBuyer9.m[this.n.UserNick] >= 2 && !msgBody.IsActivateMsg())
											{
												using (Bitmap bitmapFromDC3 = this.o.GetBitmapFromDC(0))
												{
													byte[] array = Util.smethod_1(bitmapFromDC3, ImageFormat.Jpeg);
													FileItem fileItem2 = new FileItem("screenshot.jpg", array, "image/jpeg");
													AppConfig.QnAgentServiceClient.AutoLoginError(this.n.UserNick, fileItem2, Util.GetEnumDescription(ErrCodeType.SendFailWwLoginErrorComponentShowTimesTooMany), 268435455L, "", "");
												}
											}
											this.o.KillProcess();
											this.a("LoginAliwwFindError reset loginWin!");
											return new ErrCodeInfo(ErrCodeType.SendFailWwLoginErrorComponentShowTimesTooMany);
										case ErrCodeType.BindingIncorrectMobile:
										case ErrCodeType.LimitLogin:
											goto IL_078A;
										case ErrCodeType.WarnLogined:
											break;
										case ErrCodeType.PasswordError:
											if (this.x > 5)
											{
												this.o.KillProcess();
												this.a("PasswordError reset loginWin!");
												return errCodeInfo2;
											}
											goto IL_06AF;
										case ErrCodeType.LoginAliwwTimeout:
										{
											string text3 = "{0}，旺旺登录超时，登录窗口状态：{1}";
											object userNick = base.UserNick;
											WinLoginBuyer9 winLoginBuyer2 = this.o;
											LogWriter.WriteLog(string.Format(text3, userNick, (winLoginBuyer2 != null) ? new bool?(winLoginBuyer2.Visible) : null), 1);
											if (this.l > 0)
											{
												try
												{
													Process.GetProcessById(this.l);
												}
												catch (ArgumentException)
												{
													return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginWinHasBeenCloseByManual);
												}
											}
											if (++num6 >= 2)
											{
												this.a("LoginAliwwTimeout reset loginWin!");
												LogWriter.WriteLog("旺旺登录超时" + this.h.ToString(), 1);
												this.o.KillProcess();
												return errCodeInfo2;
											}
											goto IL_06AF;
										}
										default:
											switch (errCodeType)
											{
											case ErrCodeType.SendFailQn5LoginAuthInterceptNeedToScanQr:
												goto IL_078A;
											case ErrCodeType.SendFailQn5LoginFailNeedToSelectSellerType:
											case ErrCodeType.SendFailQn5LoginFailPicValidCodeRequiredInputOrButtonNotFound:
												goto IL_06AF;
											case ErrCodeType.SendFailQn5LoginFailPicValidCodeRequiredTryTooManyTimes:
												this.a("SendFailQn5LoginFailPicValidCodeRequiredTryTooManyTimes");
												if (++num8 >= 5)
												{
													this.o.KillProcess();
													return errCodeInfo2;
												}
												goto IL_06AF;
											case ErrCodeType.SendFailQn5LoginFailPicValidCodeRequiredGetPicCodeError:
												break;
											default:
												if (errCodeType - ErrCodeType.SendFailQn5LoginFailAllSmsCodeInvalid > 1)
												{
													goto IL_06AF;
												}
												this.o.KillProcessAndChildren("AliExternal");
												this.a("SendFailQn5LoginFailGetSmsValidCodeNull reset loginWin!");
												return errCodeInfo2;
											}
											break;
										}
										if (errCodeInfo2.ErrCode == ErrCodeType.WarnLogined)
										{
											this.CallUser(toUserNick, siteId, toOpenUid);
										}
										if (++num2 > 3)
										{
											this.KillAllProcess();
											return errCodeInfo2;
										}
										goto IL_06AF;
										IL_078A:
										this.o.KillProcess();
										this.a("LimitLogin reset loginWin!");
										return errCodeInfo2;
									}
									IL_06AF:
									Application.DoEvents();
									Thread.Sleep(400);
								}
							}
						}
						else
						{
							this.f();
							this.a(false);
						}
						if (this.GetAliwwTalkWin() == null && this.GetMainWindow() != null)
						{
							AliwwBuyer9.b b = new AliwwBuyer9.b();
							b.b = this;
							b.a = "";
							if (!Util.CheckWait(3000, new Func<bool>(b.c), 500))
							{
								if (string.IsNullOrEmpty(b.a))
								{
									Interlocked.Increment(ref AliwwBuyer9.SendFailWwGetBenchWinAccountNullNum);
									if (AliwwBuyer9.SendFailWwGetBenchWinAccountNullNum >= 2)
									{
										string enumDescription = Util.GetEnumDescription(ErrCodeType.SendFailWwGetBenchWinAccountNull);
										using (Bitmap bitmapFromDC4 = this.p.GetBitmapFromDC(0))
										{
											FileItem fileItem3 = new FileItem("SendFailWwGetBenchWinAccountNull.jpg", Util.smethod_1(bitmapFromDC4, ImageFormat.Jpeg), "image/jpeg");
											AppConfig.QnAgentServiceClient.AutoLoginError(base.UserNick, fileItem3, enumDescription, 268435455L, "", "");
										}
										global::k.a().ClearAllQnProc(true, true, false, true, "旺旺发送消息时，" + enumDescription + "。杀掉所有的旺旺。");
										Interlocked.Exchange(ref AliwwBuyer9.SendFailWwGetBenchWinAccountNullNum, 0);
									}
									this.KillProcess();
									return new ErrCodeInfo(ErrCodeType.SendFailWwGetBenchWinAccountNull);
								}
								Interlocked.Exchange(ref AliwwBuyer9.SendFailWwGetBenchWinAccountNullNum, 0);
								using (Bitmap bitmapFromDC5 = this.p.GetBitmapFromDC(0))
								{
									Bitmap bitmap = bitmapFromDC5;
									string text4 = "旺旺登录错账号_";
									string text5 = this.n.UserNick.Replace(":", "=");
									string text6 = "_";
									string text7 = b.a;
									Util.CollectPicMd5(bitmap, text4 + text5 + text6 + ((text7 != null) ? text7.Replace(":", "=") : null));
								}
								LogWriter.WriteLog("登录错账号，" + this.n.UserNick + "--" + b.a, 1);
								Interlocked.Increment(ref AliwwBuyer9.WwLoginErrorAccountNum);
								if (AliwwBuyer9.WwLoginErrorAccountNum > 0 && AliwwBuyer9.WwLoginErrorAccountNum % 3 == 0)
								{
									AppConfig.QnAgentServiceClient.AutoLoginError(base.UserNick, null, "旺旺登录出现多次登录错账号的情况，请关注服务器情况", 268435455L, "", "");
								}
								else if (AliwwBuyer9.WwLoginErrorAccountNum >= 10)
								{
									Interlocked.Exchange(ref AliwwBuyer9.WwLoginErrorAccountNum, 0);
									AppConfig.AgentSettings.LoginOnAliwwBuyer = false;
									AppConfig.QnAgentServiceClient.AutoLoginError(base.UserNick, null, "旺旺登录出现多次登录错账号的情况，服务器限制旺旺登录", 268435455L, "", "");
									AppConfig.QnAgentServiceClient.UpdateAgentHostSetting(AppConfig.AgentSettings);
								}
								this.KillProcess();
								return new ErrCodeInfo(ErrCodeType.LoginErrorAccount);
							}
							else
							{
								this.a("__TAG_WORKBENCH_FOUND");
								DateTime now3 = DateTime.Now;
								int num10 = (AppConfig.AgentSettings.LoginOnQn ? 25 : 60);
								int num11 = (AppConfig.AgentSettings.LoginOnQn ? 10 : 20);
								for (int k = 0; k < num10; k++)
								{
									this.a("__TAG_WORKBENCH_FOUND" + k.ToString());
									AliwwMessageInfo amiThreadProcessing2 = Form1.AmiThreadProcessing;
									if (amiThreadProcessing2 != null && amiThreadProcessing2.Stop)
									{
										return new ErrCodeInfo(ErrCodeType.MsgWasStoppedWhileSending);
									}
									if (this.GetAliwwTalkWin() != null)
									{
										goto IL_0FB5;
									}
									if ((DateTime.Now - now3).TotalSeconds > (double)num11)
									{
										this.KillProcess();
										this.a("登录成功后找到了工作台窗口，但未找到聊天窗口");
										LogWriter.WriteLog(this.h.ToString(), 1);
										this.c();
										this.a();
										return new ErrCodeInfo(ErrCodeType.SendFailChatWinNotFoundAndBenchWinHasFound);
									}
									if (AliwwErrorReport.CloseWw(this.l))
									{
										return new ErrCodeInfo(ErrCodeType.SendFailAliwwErrorReport);
									}
									if (WinQnStopWork.CloseAll(this.l))
									{
										return new ErrCodeInfo(ErrCodeType.SendFailBenchWinHasFoundButCrash);
									}
									if (this.f())
									{
										return new ErrCodeInfo(ErrCodeType.CallFailTargetNickReceiveFriendOnly);
									}
									if (this.a(true))
									{
										int num12 = this.u + 1;
										this.u = num12;
										if (num12 == 2)
										{
											return new ErrCodeInfo(ErrCodeType.CallFailTargetNickInBlackListOrNotExists);
										}
									}
									if ((this.q == null || !this.q.Visible) && k % 5 == 0)
									{
										if (k % 10 == 0)
										{
											this.p.OpenChat(toUserNick);
										}
										else
										{
											this.CallUser(toUserNick, siteId, toOpenUid);
										}
									}
									List<WinLoginBuyer9> list4 = WinLoginBuyer9.GetList().Where(new Func<WinLoginBuyer9, bool>(AliwwBuyer9.<>c.<>9.a)).ToList<WinLoginBuyer9>();
									if (list4 != null && list4.Count >= 3)
									{
										LogWriter.WriteLog("注意:主窗口存在，但是呼叫出太多登录窗了", 1);
									}
									Thread.Sleep(300);
								}
							}
						}
						if (this.GetAliwwTalkWin() == null)
						{
							this.a("呼叫成功，但却找不到聊天窗口");
							LogWriter.WriteLog(this.h.ToString(), 1);
							this.KillProcess();
							this.c();
							this.a();
							return new ErrCodeInfo(ErrCodeType.SendFailChatWinNotFoundAndBenchWinHasFound);
						}
					}
					else
					{
						this.f();
						this.a(false);
					}
					IL_0FB5:
					if (this.GetAliwwTalkWin() == null)
					{
						this.KillProcess();
						return new ErrCodeInfo(ErrCodeType.CallFailSpecifySendWindowNotFound);
					}
					this.b();
					this.a("GetAlitwTalkWindow() != null || GetAliwwTalkWin().");
					AliwwBuyer9.m.Remove(this.n.UserNick);
					UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(this.n.UserNick);
					userCacheOrCreate.LastSendProcessId = this.q.ProcessId;
					userCacheOrCreate.LastSendSoftware = MsgSendSoftware.AliwwBuyer9;
					if (this.Option.IsOnlyCall)
					{
						this.CallUser(toUserNick, siteId, toOpenUid);
						Thread.Sleep(500);
						if (this.Option.IsNeedScreenshot)
						{
							string text8 = AppDomain.CurrentDomain.BaseDirectory + "/AldsScreenshot";
							if (!Directory.Exists(text8))
							{
								Directory.CreateDirectory(text8);
							}
							this.q.SetMaximizeWindow();
							Thread.Sleep(1000);
							using (Bitmap bitmapFromDC6 = this.q.GetBitmapFromDC(0))
							{
								bitmapFromDC6.Save(text8 + "/" + this.Option.ScreenshotFileName + ".jpg", ImageFormat.Jpeg);
							}
							this.q.SetRestoreWindow();
						}
						return new ErrCodeInfo(ErrCodeType.CallSuccWw);
					}
					ErrCodeInfo errCodeInfo3 = this.DoSendMsg(toUserNick, toOpenUid, msgBody, siteId);
					if (errCodeInfo3 != null && errCodeInfo3.ErrCode == ErrCodeType.CallFailCheckTargetNickUnEquals)
					{
						this.KillProcess();
					}
					return errCodeInfo3;
				}
				if (this.o != null)
				{
					goto IL_03B2;
				}
				errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailWwLoginWinNotFoundWhileAutoLogin);
			}
			return errCodeInfo;
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x0006518C File Offset: 0x0006338C
		public ErrCodeInfo DoSendMsg(string toUserNick, string toOpenUid, string msgBody, string siteId = "cntaobao")
		{
			ErrCodeInfo errCodeInfo = new ErrCodeInfo(ErrCodeType.CallFailCheckTargetNickUnEquals);
			if (this.GetMainWindow() != null)
			{
				WinAliwwMainBuyer9 mainWindow = this.GetMainWindow();
				if (mainWindow != null)
				{
					mainWindow.OpenChat(toUserNick);
				}
			}
			else
			{
				this.CallUser(toUserNick, siteId, toOpenUid);
			}
			Thread.Sleep(500);
			int i = 0;
			while (i < 10)
			{
				AliwwMessageInfo amiThreadProcessing = Form1.AmiThreadProcessing;
				ErrCodeInfo errCodeInfo2;
				if (amiThreadProcessing == null || !amiThreadProcessing.Stop)
				{
					if (!AliwwErrorReport.CloseWw(this.l))
					{
						if (!WinQnStopWork.CloseAll(this.l))
						{
							if (!this.f())
							{
								if (this.a(true))
								{
									int num = this.u + 1;
									this.u = num;
									if (num == 2)
									{
										return new ErrCodeInfo(ErrCodeType.CallFailTargetNickInBlackListOrNotExists);
									}
								}
								if (!this.q.CheckCallUserInfoEquals(toUserNick, toOpenUid, 1, "cntaobao"))
								{
									if (i == 5)
									{
										this.CallUser(toUserNick, siteId, toOpenUid);
									}
									else if (i == 3)
									{
										WinAliwwMainBuyer9 mainWindow2 = this.GetMainWindow();
										if (mainWindow2 != null)
										{
											mainWindow2.OpenChat(toUserNick);
										}
									}
									Application.DoEvents();
									Thread.Sleep(200);
									i++;
									continue;
								}
								errCodeInfo = this.q.SendToTalkWindowWholeMsgForAutoReply(toUserNick, toOpenUid, msgBody);
								IL_0126:
								if (errCodeInfo != null && errCodeInfo.ErrCode == ErrCodeType.SendSucc)
								{
									Thread.Sleep(500);
								}
								errCodeInfo2 = errCodeInfo;
							}
							else
							{
								errCodeInfo2 = new ErrCodeInfo(ErrCodeType.CallFailTargetNickReceiveFriendOnly);
							}
						}
						else
						{
							errCodeInfo2 = new ErrCodeInfo(ErrCodeType.SendFailBenchWinHasFoundButCrash);
						}
					}
					else
					{
						errCodeInfo2 = new ErrCodeInfo(ErrCodeType.SendFailAliwwErrorReport);
					}
				}
				else
				{
					errCodeInfo2 = new ErrCodeInfo(ErrCodeType.MsgWasStoppedWhileSending);
				}
				return errCodeInfo2;
			}
			goto IL_0126;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x0006531C File Offset: 0x0006351C
		public ErrCodeInfo DoLogin(string msgBody)
		{
			int num = 15;
			string text = AppDomain.CurrentDomain.BaseDirectory + "\\Screenshot\\";
			AppDomain.CurrentDomain.BaseDirectory + "\\Screenshot\\Captcha\\";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			WindowInfo loginErrorComponent = this.o.GetLoginErrorComponent();
			if (loginErrorComponent != null)
			{
				loginErrorComponent.Hide();
			}
			if (this.GetLoginWinArea() != null)
			{
				if (!this.o.ResetLoginBtn())
				{
					string text2 = "登录窗口失效：";
					WindowTreeNode treeNode = this.o.GetTreeNode();
					LogWriter.WriteLog(text2 + ((treeNode != null) ? treeNode.WriteTreeNode("") : null), 1);
					return new ErrCodeInfo(ErrCodeType.ResetLoginFormFail);
				}
				WinLoginBuyer9 winLoginBuyer = this.o;
				string userNick = this.n.UserNick;
				string qnAccountPwd = this.n.QnAccountPwd;
				bool longOpen = this.n.LongOpen;
				int num2 = this.i;
				this.i = num2 + 1;
				winLoginBuyer.DoLogin(userNick, qnAccountPwd, longOpen, num2);
				Thread.Sleep(100);
			}
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			DateTime? dateTime = null;
			DateTime? dateTime2 = null;
			while (stopwatch.Elapsed.TotalSeconds < (double)num)
			{
				AliwwBuyer9.c c = new AliwwBuyer9.c();
				AliwwMessageInfo amiThreadProcessing = Form1.AmiThreadProcessing;
				if (amiThreadProcessing != null && amiThreadProcessing.Stop)
				{
					return new ErrCodeInfo(ErrCodeType.MsgWasStoppedWhileSending);
				}
				if (this.e())
				{
					LogWriter.WriteLog("HasWarnLogined::\r\n" + this.h.ToString(), 1);
					return new ErrCodeInfo(ErrCodeType.WarnLogined);
				}
				if (this.GetAliwwTalkWin() == null)
				{
					if (this.GetMainWindow() == null)
					{
						WindowInfo windowInfo = this.GeTipWindow();
						if (windowInfo == null)
						{
							if (!this.o.Visible)
							{
								this.a("检测到登录窗已经被隐藏了");
							}
							if (this.o.Visible && this.o.LoginAreaWin != null && this.o.LoginAreaWin.Visible)
							{
								this.a("还在登陆界面");
								if (dateTime == null)
								{
									dateTime = new DateTime?(DateTime.Now);
								}
								if (dateTime != null && (DateTime.Now - dateTime.Value).TotalSeconds >= 2.0)
								{
									return new ErrCodeInfo(ErrCodeType.StillOnLoginWinArea);
								}
								dateTime2 = null;
							}
							if (this.o.Visible && this.o.LoginLoadingWin != null && this.o.LoginLoadingWin.Visible)
							{
								this.a("还在登陆等待界面");
								dateTime = null;
								if (dateTime2 == null)
								{
									dateTime2 = new DateTime?(DateTime.Now);
								}
								if (dateTime2 != null && (DateTime.Now - dateTime2.Value).TotalSeconds >= 6.0)
								{
									return new ErrCodeInfo(ErrCodeType.LoginAliwwTimeout);
								}
							}
							IntPtr intPtr = WindowsAPI.FindWindow("#32770", "阿里旺旺 - 登录验证");
							if (intPtr != IntPtr.Zero)
							{
								AliwwBuyer9.d d = new AliwwBuyer9.d();
								this.a("找到“阿里旺旺 - 登录验证”窗口");
								dateTime = null;
								dateTime2 = null;
								d.a = new WindowInfo(intPtr).Convert<WinLoginImageCaptchaBuyer9>();
								if (this.l == d.a.ProcessId)
								{
									Util.CheckWait(1000, new Func<bool>(d.b), 100);
									if ("安全验证".Equals(d.a.GetText()))
									{
										continue;
									}
									if (d.a.Visible)
									{
										int num3 = 0;
										string picValidCode;
										do
										{
											Thread.Sleep(50);
											byte[] buffer;
											using (Bitmap bitmapFromDC = d.a.CaptchaChrome.GetBitmapFromDC(0))
											{
												MemoryStream memoryStream = new MemoryStream();
												bitmapFromDC.Save(memoryStream, ImageFormat.Jpeg);
												buffer = memoryStream.GetBuffer();
												memoryStream.Close();
												bitmapFromDC.Dispose();
											}
											this.a(string.Format("获取图形验证码。retryTime: {0}", num3));
											picValidCode = AppConfig.GetPicValidCode(buffer, "B942");
											if (picValidCode == null)
											{
												goto IL_0C5B;
											}
											num3++;
										}
										while (num3 < 5 && picValidCode == "-10");
										this.a(string.Format("提交图形验证码。retryTime: {0}", num3));
										if (!string.IsNullOrEmpty(picValidCode) && !(picValidCode == "-10"))
										{
											d.a.SubmitCaptcha(picValidCode);
											Util.CheckWait(3000, new Func<bool>(AliwwBuyer9.<>c.<>9.a), 100);
											goto IL_04FB;
										}
										return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginFailPicValidCodeRequiredGetPicCodeError);
										IL_0C5B:
										return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginFailPicValidCodeRequiredGetPicCodeError);
									}
									string text3 = "captchaWin UnVisible";
									WindowTreeNode treeNode2 = d.a.GetTreeNode();
									this.a(text3 + ((treeNode2 != null) ? treeNode2.WriteTreeNode("") : null));
								}
								else
								{
									string text4 = "找到图形验证码窗口，但是线程ID不一致、或名称不符合。\r\n";
									WindowTreeNode treeNode3 = d.a.GetTreeNode();
									this.a(text4 + ((treeNode3 != null) ? treeNode3.WriteTreeNode("") : null));
								}
							}
							IL_04FB:
							c.a = WinLoginMobileCaptchaBuyer9.Get();
							if (c.a != null)
							{
								this.a("找到“安全验证”窗口");
								dateTime = null;
								dateTime2 = null;
								string text5 = "first print: ";
								WindowTreeNode treeNode4 = c.a.GetTreeNode();
								this.a(text5 + ((treeNode4 != null) ? treeNode4.WriteTreeNode("") : null));
								if (this.l == c.a.ProcessId)
								{
									AliwwBuyer9.e e = new AliwwBuyer9.e();
									e.b = c;
									e.a = null;
									bool flag = Util.CheckWait(3000, new Func<bool>(e.c), 100);
									if ("阿里旺旺 - 登录验证".Equals(e.b.a.GetText()))
									{
										continue;
									}
									if (!flag)
									{
										using (Bitmap bitmapFromDC2 = e.b.a.GetBitmapFromDC(0))
										{
											string text6 = Util.ComputeHashMd5(bitmapFromDC2);
											if (AliwwBuyer9.g.Contains(text6))
											{
												ErrCodeInfo errCodeInfo = new ErrCodeInfo(ErrCodeType.MobilePhoneAuthenticationIsNotBound);
												int num2 = this.y + 1;
												this.y = num2;
												if (num2 >= 4)
												{
													base.RejectUser(msgBody, errCodeInfo, e.b.a, "");
												}
												e.b.a.Close(true);
												return errCodeInfo;
											}
										}
										if (e.a != null)
										{
											this.a("wi的句柄:" + e.a.HWnd.ToString("X2"));
										}
										if (e.b.a != null)
										{
											string text7 = "second print: ";
											WindowTreeNode treeNode5 = e.b.a.GetTreeNode();
											this.a(text7 + ((treeNode5 != null) ? treeNode5.WriteTreeNode("") : null));
											e.b.a.Close(true);
										}
										return new ErrCodeInfo(ErrCodeType.LoadMobileCaptchaWinTimeOut);
									}
									num = 20;
									if (e.b.a.SendCaptchaBtn == null || !e.b.a.SendCaptchaBtn.Visible || e.b.a.SendCaptchaBtn.Disabled)
									{
										throw new Exception("找不到发送短信验证码的按钮");
									}
									string text8 = e.a.GetText();
									if (string.IsNullOrEmpty(text8))
									{
										text8 = e.a.Info.WindowName;
									}
									if (!"手机短信".Equals(text8) && !"手机短信(156****6121)".Equals(text8) && !"手机短信(170****0621)".Equals(text8))
									{
										string text9 = string.Format("{0}----手机绑定错误,{1}", this.n.UserNick, JSON.Encode(e.a.Info));
										this.a(text9);
										ErrCodeInfo errCodeInfo2 = new ErrCodeInfo(ErrCodeType.BindingIncorrectMobile);
										base.RejectUser(msgBody, errCodeInfo2, e.b.a, "");
										e.b.a.Close(true);
										return errCodeInfo2;
									}
									string text10 = text8;
									string text11 = text10;
									if (!(text11 == "手机短信(156****6121)"))
									{
										if (!(text11 == "手机短信(170****0621)"))
										{
											LogWriter.WriteLog("旺旺获取短信窗口信息不匹配，窗口信息：" + text8, 1);
										}
										else
										{
											this.t = "170****0621";
										}
									}
									else
									{
										this.t = "156****6121";
									}
									if (this.k == null)
									{
										AppConfig.GetSmsValidCode(true, this.n.UserNick, this.t);
										this.k = new DateTime?(DateTime.Now);
									}
									bool flag2 = false;
									if (this.s.Count <= 0)
									{
										e.b.a.GetCaptcha();
										flag2 = true;
									}
									if (!this.j)
									{
										this.j = true;
										Thread.Sleep(3500);
									}
									if (!flag2 || Util.CheckWait(5000, new Func<bool>(e.b.b), 100))
									{
										int num4 = 0;
										while (++num4 < 5)
										{
											using (Bitmap bitmap = e.b.a.CaptureWindow(new Agiso.Windows.Rectangle
											{
												Left = 80,
												Top = 170,
												Right = 276,
												Bottom = 192
											}))
											{
												string text12 = Util.ComputeHashMd5(bitmap);
												if (!AliwwBuyer9.a.Contains(text12))
												{
													Util.CollectPicMd5(bitmap, "短信错误截图_");
												}
											}
											if (this.s.Count <= 0)
											{
												Dictionary<string, DateTime> dictionary = AliwwBuyer9.a(this.n.UserNick, this.t, 4);
												if (dictionary.Count > 0)
												{
													foreach (KeyValuePair<string, DateTime> keyValuePair in dictionary)
													{
														if (!this.r.Contains(keyValuePair.Key) && !this.s.Contains(keyValuePair.Key))
														{
															this.s.Add(keyValuePair.Key);
														}
													}
												}
											}
											if (this.s.Count <= 0)
											{
												Thread.Sleep(500);
											}
											else
											{
												string text13 = this.s[0];
												this.z = text13;
												this.s.RemoveAt(0);
												this.r.Add(text13);
												e.b.a.SubmitCaptcha(text13);
												IL_0921:
												if (num4 < 5)
												{
													goto IL_095E;
												}
												e.b.a.Close(true);
												if (this.r.Count > 0)
												{
													return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginFailAllSmsCodeInvalid);
												}
												return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginFailGetSmsValidCodeNull);
											}
										}
										goto IL_0921;
									}
									e.b.a.Close(true);
									return new ErrCodeInfo(ErrCodeType.GetMobileCaptchaTimeout);
								}
								else
								{
									string text14 = "找到手机验证码窗口，但是线程ID不一致、或名称不符合。\r\n";
									WindowTreeNode treeNode6 = c.a.GetTreeNode();
									this.a(text14 + ((treeNode6 != null) ? treeNode6.WriteTreeNode("") : null));
								}
							}
							IL_095E:
							WindowInfo loginErrorComponent2 = this.o.GetLoginErrorComponent();
							if (loginErrorComponent2 != null)
							{
								dateTime = null;
								dateTime2 = null;
								this.a("找到错误窗口LoginErrorComponent!");
								using (Bitmap bitmapFromDC3 = loginErrorComponent2.GetBitmapFromDC(0))
								{
									string text15 = Util.ComputeHashMd5(bitmapFromDC3);
									if (AliwwBuyer9.NONE_ERROR_MD5.Contains(text15))
									{
										loginErrorComponent2.Hide();
									}
									else
									{
										if (AliwwBuyer9.PASSWORD_ERROR_MD5.Contains(text15))
										{
											ErrCodeInfo errCodeInfo3 = new ErrCodeInfo(ErrCodeType.PasswordError);
											int num2 = this.x + 1;
											this.x = num2;
											if (num2 > 5)
											{
												base.RejectUser(msgBody, errCodeInfo3, loginErrorComponent2, "");
											}
											loginErrorComponent2.Hide();
											return errCodeInfo3;
										}
										if (AliwwBuyer9.NETWORK_ERROR_MD5.Contains(text15) || AliwwBuyer9.NETWORK_TIMEOUT_MD5.Contains(text15))
										{
											int num2 = this.w + 1;
											this.w = num2;
											if (num2 > 2)
											{
												return new ErrCodeInfo(ErrCodeType.SendFailWwLoginErrorNetworkError);
											}
										}
										else
										{
											if (AliwwBuyer9.REQUEST_LIMIT_MD5.Contains(text15) || AliwwBuyer9.ACCOUNT_SUB_EXCEPTION.Contains(text15) || AliwwBuyer9.b.Contains(text15) || AliwwBuyer9.c.Contains(text15))
											{
												loginErrorComponent2.Hide();
												return new ErrCodeInfo(ErrCodeType.RequestLimit);
											}
											if (AliwwBuyer9.ACCOUNT_LIMITED.Contains(text15) || AliwwBuyer9.d.Contains(text15))
											{
												byte[] array = Util.smethod_1(bitmapFromDC3, ImageFormat.Png);
												FileItem fileItem = new FileItem("screenshot.jpg", array, "image/jpeg");
												AppConfig.QnAgentServiceClient.AutoLoginError(this.n.UserNick, fileItem, null, 1L, base.GetNoticeSellerMsg(ErrCodeType.RequestLimit, ""), "");
												loginErrorComponent2.Hide();
												return new ErrCodeInfo(ErrCodeType.RequestLimit);
											}
											if (AliwwBuyer9.CAPTCHA_ERROR_MD5.Contains(text15))
											{
												return new ErrCodeInfo(ErrCodeType.SendFailQn5LoginFailPicValidCodeRequiredTryTooManyTimes);
											}
											if (!this.IsAlreadyCollectedImageMd5(text15))
											{
												bitmapFromDC3.Save(string.Concat(new string[]
												{
													text,
													DateTime.Now.ToString("yyyyMMddHHmmss"),
													"_",
													text15,
													".png"
												}), ImageFormat.Png);
											}
											Thread.Sleep(1000);
											loginErrorComponent2.Hide();
											return new ErrCodeInfo(ErrCodeType.LoginAliwwFindError);
										}
									}
								}
							}
							this.a("再次循环找各种类型窗口!");
							Application.DoEvents();
							Thread.Sleep(100);
							continue;
						}
						Thread.Sleep(500);
						using (Bitmap bitmapFromDC4 = windowInfo.GetBitmapFromDC(0))
						{
							string text16 = Util.ComputeHashMd5(bitmapFromDC4);
							ErrCodeInfo errCodeInfo4 = new ErrCodeInfo(ErrCodeType.LimitLogin);
							if (AliwwBuyer9.e.Contains(text16))
							{
								errCodeInfo4 = new ErrCodeInfo(ErrCodeType.SendFailQn5LoginAuthInterceptNeedToScanQr);
							}
							base.RejectUser(msgBody, errCodeInfo4, windowInfo, "");
							return errCodeInfo4;
						}
					}
					return new ErrCodeInfo(ErrCodeType.CallSuccWw);
				}
				return new ErrCodeInfo(ErrCodeType.CallSuccWw);
			}
			return new ErrCodeInfo(ErrCodeType.LoginAliwwTimeout);
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000662F8 File Offset: 0x000644F8
		private bool f()
		{
			List<WinAlertNeedToAddFriend> list = WinAlertNeedToAddFriend.GetList(this.l);
			if (list != null && list.Count > 0)
			{
				foreach (WinAlertNeedToAddFriend winAlertNeedToAddFriend in list)
				{
					winAlertNeedToAddFriend.Close(true);
				}
				int num = this.aa + 1;
				this.aa = num;
				if (num >= 2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x0006638C File Offset: 0x0006458C
		private bool a(bool A_0 = true)
		{
			List<WinFileNotAcceptAlert> list = WinBlackListOrNotAccountExistsAlert.GetList(this.l);
			bool flag;
			if (list != null && list.Count > 0)
			{
				foreach (WinFileNotAcceptAlert winFileNotAcceptAlert in list)
				{
					if (A_0)
					{
						using (Bitmap bitmap = winFileNotAcceptAlert.CaptureWindow(new Agiso.Windows.Rectangle
						{
							Left = 85,
							Right = 335,
							Top = 50,
							Bottom = 108
						}))
						{
							Util.CollectPicMd5(bitmap, "旺旺_黑名单_内容_" + Util.GetMasterNick(this.n.UserNick) + "_");
						}
						using (Bitmap bitmapFromDC = winFileNotAcceptAlert.GetBitmapFromDC(0))
						{
							Util.CollectPicMd5(bitmapFromDC, "旺旺_黑名单_" + Util.GetMasterNick(this.n.UserNick) + "_");
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

		// Token: 0x060024CA RID: 9418 RVA: 0x000664D8 File Offset: 0x000646D8
		private bool e()
		{
			List<WindowInfo> allDesktopWindows = Win32Extend.GetAllDesktopWindows();
			bool flag = false;
			foreach (WindowInfo windowInfo in allDesktopWindows)
			{
				if (windowInfo.Info.ClassName == "StandardWindow" && string.IsNullOrEmpty(windowInfo.Info.WindowName))
				{
					Agiso.Windows.Rectangle clientRect = windowInfo.GetClientRect();
					if (clientRect.GetHeight() >= 55 && clientRect.GetHeight() < 65)
					{
						if (this.l == windowInfo.ProcessId)
						{
							string text = "找到的已登录的提示\r\n";
							WindowTreeNode treeNode = windowInfo.GetTreeNode();
							LogWriter.WriteLog(text + ((treeNode != null) ? treeNode.WriteTreeNode("") : null), 2);
							flag = true;
						}
						windowInfo.Hide();
					}
				}
			}
			if (flag)
			{
				Thread.Sleep(1000);
			}
			return flag;
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000665D0 File Offset: 0x000647D0
		private static Dictionary<string, DateTime> a(string A_0, string A_1, int A_2 = 20)
		{
			Dictionary<string, DateTime> dictionary = new Dictionary<string, DateTime>();
			for (int i = 0; i < A_2; i++)
			{
				List<VerificationCodeSMS> smsValidCode = AppConfig.GetSmsValidCode(true, A_0, A_1);
				if (smsValidCode != null && smsValidCode.Count > 0)
				{
					smsValidCode.Sort();
					for (int j = smsValidCode.Count - 1; j >= 0; j--)
					{
						VerificationCodeSMS verificationCodeSMS = smsValidCode[j];
						if (!dictionary.ContainsKey(verificationCodeSMS.VerificationCode))
						{
							dictionary.Add(verificationCodeSMS.VerificationCode, verificationCodeSMS.ReceiveTime);
						}
					}
					return dictionary;
				}
				if (i + 1 < A_2)
				{
					Thread.Sleep(500);
				}
			}
			return dictionary;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x0006667C File Offset: 0x0006487C
		public ErrCodeInfo CallUser(string toUserNick, string siteId, string toOpenUid)
		{
			ErrCodeInfo errCodeInfo;
			if (AppConfig.AllowAutoLogin)
			{
				if (!string.IsNullOrEmpty(this.n.QnAccountPwd))
				{
					errCodeInfo = base.CallingUserNickQianNiu(toUserNick, siteId, toOpenUid);
				}
				else
				{
					errCodeInfo = new ErrCodeInfo(ErrCodeType.SendFailQn5NoPwd);
				}
			}
			else
			{
				errCodeInfo = base.CallUser(toUserNick, siteId, toOpenUid, ref this.q);
			}
			return errCodeInfo;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000666D0 File Offset: 0x000648D0
		public void CloseCurrentChat()
		{
			if (this.q != null)
			{
				this.q.CloseCurrentChat();
				List<WinFileNotAcceptAlert> list = WinFileNotAcceptAlert.GetList(0);
				if (list != null)
				{
					foreach (WinFileNotAcceptAlert winFileNotAcceptAlert in list)
					{
						winFileNotAcceptAlert.BtnSure2.Click(true);
					}
				}
			}
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x0006674C File Offset: 0x0006494C
		public void KillProcess()
		{
			if (this.p != null)
			{
				this.p.ClickExit();
				Thread.Sleep(500);
			}
			try
			{
				bool flag = false;
				if (this.q != null)
				{
					this.q.KillProcessAndChildren("AliExternal");
					flag = true;
				}
				if (!flag && this.p != null)
				{
					this.p.KillProcessAndChildren("AliExternal");
					flag = true;
				}
				if (!flag && this.o != null)
				{
					this.o.KillProcessAndChildren("AliExternal");
				}
				if (this.l > 0)
				{
					Win32Extend.KillProcessById(this.l, null);
				}
				UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(base.UserNick);
				userCacheOrCreate.LastSendProcessId = 0;
				userCacheOrCreate.LastSendSoftware = MsgSendSoftware.Undefined;
			}
			catch
			{
			}
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x0000F000 File Offset: 0x0000D200
		public void KillAllProcess()
		{
			Win32Extend.KillProcessByNameWithCmd("AliIM");
			Win32Extend.KillProcessByNameWithCmd("AliExternal");
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x0006682C File Offset: 0x00064A2C
		private void c()
		{
			if (AliwwBuyer9.ab.ContainsKey(base.UserNick))
			{
				Dictionary<string, int> dictionary = AliwwBuyer9.ab;
				string userNick = base.UserNick;
				Dictionary<string, int> dictionary2 = AliwwBuyer9.ab;
				string userNick2 = base.UserNick;
				int num = dictionary2[userNick2] + 1;
				dictionary2[userNick2] = num;
				dictionary[userNick] = num;
			}
			else
			{
				AliwwBuyer9.ab[base.UserNick] = 1;
			}
			if (AliwwBuyer9.ab[base.UserNick] >= 2)
			{
				if (!AppConfig.UserNickLoginOnQnList.Contains(base.UserNick))
				{
					AppConfig.UserNickLoginOnQnList.Add(base.UserNick);
				}
				AliwwBuyer9.ab.Remove(base.UserNick);
			}
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000668DC File Offset: 0x00064ADC
		private void b()
		{
			if (AliwwBuyer9.ab.ContainsKey(base.UserNick))
			{
				int num = AliwwBuyer9.ab[base.UserNick];
				if (num == 1)
				{
					AliwwBuyer9.ab.Remove(base.UserNick);
				}
				else
				{
					AliwwBuyer9.ab[base.UserNick] = num - 1;
				}
			}
			AppConfig.UserNickLoginOnQnList.Remove(base.UserNick);
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x0006694C File Offset: 0x00064B4C
		private void a()
		{
			string text = Path.Combine("C:\\Program Files (x86)\\AliWangWang\\", "profiles");
			string[] directories = Directory.GetDirectories(text);
			foreach (string text2 in directories)
			{
				AliwwBuyer9.f f = new AliwwBuyer9.f();
				f.a = new DirectoryInfo(text2);
				if (f.a.Name == "system")
				{
					if (!AliwwBuyer9.ac)
					{
						AliwwBuyer9.ac = true;
						Task.Run(new Action(f.b));
					}
				}
				else
				{
					try
					{
						f.a.Delete(true);
					}
					catch (Exception ex)
					{
						LogWriter.WriteLog(string.Format("删除文件夹出错，{0},{1}", f.a.FullName, ex), 1);
					}
				}
			}
		}

		// Token: 0x04001E53 RID: 7763
		private static List<string> a = new List<string> { "94C8310B0EA1C8AEFC0EB58CF96DAA52", "194B4B64FED9D707F22A657AC78D9701", "9C01D2B2034EB07829F7BF40C8D3790B", "9FDAB1CC2ED253FF23BF030AB096AA88" };

		// Token: 0x04001E54 RID: 7764
		public static readonly string[] ACCOUNT_PROTECTED = new string[] { "60235EB9CCD2657891E7FF31D2294712", "EF088A9DA4D3DD92649D33701F6C9688", "83757C1EFC76714D728A60B77C330822", "975F7047E430AB9C31C43DBA67DD59E4", "B406DDF52B940184920B5D232C78A472", "D934FA29E918AC959A4945FB08AF9A85", "C5C041CA16832FF6E06455C4F38FAFA0", "E8CCF9956D84ECF9911FCF1C5217BBF8", "0727767312A280F2DC84905ED0A2AEEB", "44F2C045BBB3FC9CBD879C00185EC688" };

		// Token: 0x04001E55 RID: 7765
		public static readonly string[] ACCOUNT_NOT_EXISTS = new string[] { "FADA44B05508109B5D88197E7EC8C411" };

		// Token: 0x04001E56 RID: 7766
		public static readonly string[] ACCOUNT_NEED_SECURITY_CHECK = new string[] { "C30BC35BD713FC55177C81EFF08C49C3", "34AAA25D06D74821EE219DA2BA668B8B" };

		// Token: 0x04001E57 RID: 7767
		public static readonly string[] ACCOUNT_LIMITED2 = new string[] { "33E3662D8BA94795BB9D8EDC64CD5258" };

		// Token: 0x04001E58 RID: 7768
		public static readonly string[] PASSWORD_ERROR_MD5 = new string[] { "EDFFD7E82A14117B6A0F53D50656C6B7", "6A84938E783A9C08E6AACD5449D3E69F", "95953872055ECE3EBF8C44BC12177E04" };

		// Token: 0x04001E59 RID: 7769
		public static readonly string[] PASSWORD_NOT_INPUT = new string[] { "5CA4863046528B260C93C0FCF57BC08F", "64C1B4AB5ED0ADAF2EA37695B1807E64" };

		// Token: 0x04001E5A RID: 7770
		public static readonly string[] PASSWORD_AUTO_EXPIRE = new string[] { "79B8CFD290537327B2273C5BCD370C60", "2248B362C07C1450659E92EBC36030F6", "6B142C1B32A135062317131F3BD48202" };

		// Token: 0x04001E5B RID: 7771
		public static readonly string[] AUTO_LOGIN_FAILED = new string[] { "ED215861DEB06C978EC433A9D0AFB1E2" };

		// Token: 0x04001E5C RID: 7772
		public static readonly string[] ACCOUNT_NOT_INPUT = new string[] { "F0899CE17D2601A5E6300C33E635E629", "C79B7F2789003C24235A0DEF910A977E" };

		// Token: 0x04001E5D RID: 7773
		public static readonly string[] ACCOUNT_LIMITED = new string[] { "A2FA61DE3E35862F2C782D6F4B8B399E" };

		// Token: 0x04001E5E RID: 7774
		public static readonly string[] ACCOUNT_SUB_EXCEPTION = new string[] { "99E039F58DCD2A944638096B74FAFBE3", "1B918E232932357525DA04B57B2631CB", "02645A0591083FEFAFF6D72C2D8131AB" };

		// Token: 0x04001E5F RID: 7775
		public static readonly string[] REQUEST_LIMIT_MD5 = new string[] { "245AB96862102161835F48B92146B9A1", "AA795C63A7AED019E5003B894E298041" };

		// Token: 0x04001E60 RID: 7776
		public static readonly string[] NONE_ERROR_MD5 = new string[] { "8587B68EFF94598878583C6BC72D6C41", "1FAB47580E9167F8F5E23FB3551923B8" };

		// Token: 0x04001E61 RID: 7777
		public static readonly string[] NETWORK_ERROR_MD5 = new string[] { "649E959541651AD8E78E593A8390673B", "FFC0D123620A2F38356CE3075AA897F6" };

		// Token: 0x04001E62 RID: 7778
		public static readonly string[] NETWORK_TIMEOUT_MD5 = new string[] { "931FFA09D7C9B1ACFD43A0F5D984B054", "519577FCDDCC2D46F99122443B088E00" };

		// Token: 0x04001E63 RID: 7779
		public static readonly string[] CAPTCHA_ERROR_MD5 = new string[] { "8A93DDDBB7AF2043A7B05B021137EB47", "F277597D3975C9C07F39B30605E9E5C1" };

		// Token: 0x04001E64 RID: 7780
		public static readonly string[] CAPTCHA_ERROR_PLASE_RETRY_MD5 = new string[] { "4EF55FC8B5D634AE711F435165DAA4DC", "B0DE78436A722EE8D4D9D2B869392AA7" };

		// Token: 0x04001E65 RID: 7781
		public static readonly string[] CAPTCHA_REQUEST_CONTINUAL_MD5 = new string[] { "4AF819614A158E3CEBA115B54D0A6B9D" };

		// Token: 0x04001E66 RID: 7782
		public static readonly string[] SYSTEM_ERROR = new string[] { "035576F2949EF1F0A7D0C5CF0804CAB3", "97AF00FE1C86D064D62DADAE7760B234" };

		// Token: 0x04001E67 RID: 7783
		public static readonly string[] PLEASE_REQUEST_CAPTCHA_FIRST = new string[] { "8AD8517D18EF57D3D4DA0311BF0CE265" };

		// Token: 0x04001E68 RID: 7784
		public static readonly string[] UNKNOW_ERROR = new string[] { "1F2E966A5C35C8DCC9ABB664B9EDEB89", "1C44094891ADB03E396EEAC80520DF67" };

		// Token: 0x04001E69 RID: 7785
		private static readonly string[] b = new string[] { "0F50C50E1B99C10BE3EDABECFA424F12" };

		// Token: 0x04001E6A RID: 7786
		private static readonly string[] c = new string[] { "3D73ACF9553C7AA5A45D3B9DA35B4BD9" };

		// Token: 0x04001E6B RID: 7787
		private static readonly string[] d = new string[] { "0109B7F8A3B49D0F7D0F6F103AD135BA" };

		// Token: 0x04001E6C RID: 7788
		private static readonly string[] e = new string[] { "743EBE6052B89A1784232756A81F4D88" };

		// Token: 0x04001E6D RID: 7789
		private static readonly string[] f = new string[] { "79EBAB5756BA63B7C62392D879E4E22D", "1AF182D4CABDB3D6B3831967F1633362", "F597DE339ECEA2BD90C56D2748CF0F24" };

		// Token: 0x04001E6E RID: 7790
		private static readonly string[] g = new string[] { "3187592914142E27BAD5FA300C79CE4D", "DA814C1ADF4C1194A667B548D4A47AA8" };

		// Token: 0x04001E6F RID: 7791
		private StringBuilder h = new StringBuilder();

		// Token: 0x04001E70 RID: 7792
		public static int WwLoginErrorAccountNum;

		// Token: 0x04001E71 RID: 7793
		public static int SendFailWwGetBenchWinAccountNullNum;

		// Token: 0x04001E72 RID: 7794
		private int i = 0;

		// Token: 0x04001E73 RID: 7795
		private bool j = false;

		// Token: 0x04001E74 RID: 7796
		private DateTime? k;

		// Token: 0x04001E75 RID: 7797
		private int l;

		// Token: 0x04001E76 RID: 7798
		private static Dictionary<string, int> m = new Dictionary<string, int>();

		// Token: 0x04001E77 RID: 7799
		private AldsAccountInfo n;

		// Token: 0x04001E78 RID: 7800
		private WinLoginBuyer9 o;

		// Token: 0x04001E79 RID: 7801
		private WinAliwwMainBuyer9 p;

		// Token: 0x04001E7A RID: 7802
		private AliwwTalkWindow q;

		// Token: 0x04001E7B RID: 7803
		private List<string> r = new List<string>();

		// Token: 0x04001E7C RID: 7804
		private List<string> s = new List<string>();

		// Token: 0x04001E7D RID: 7805
		private string t = null;

		// Token: 0x04001E7E RID: 7806
		private int u = 0;

		// Token: 0x04001E7F RID: 7807
		[CompilerGenerated]
		private AliwwOptionQn5 v;

		// Token: 0x04001E80 RID: 7808
		private int w = 0;

		// Token: 0x04001E81 RID: 7809
		private int x = 0;

		// Token: 0x04001E82 RID: 7810
		private int y = 0;

		// Token: 0x04001E83 RID: 7811
		private string z = "";

		// Token: 0x04001E84 RID: 7812
		private int aa = 0;

		// Token: 0x04001E85 RID: 7813
		[CompilerGenerated]
		private static readonly Dictionary<string, int> ab = new Dictionary<string, int>();

		// Token: 0x04001E86 RID: 7814
		private static bool ac = false;

		// Token: 0x0200073B RID: 1851
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x060024DE RID: 9438 RVA: 0x00066DDC File Offset: 0x00064FDC
			internal bool b()
			{
				return this.a.MainWindowHandle != IntPtr.Zero;
			}

			// Token: 0x04001E8F RID: 7823
			public Process a;
		}

		// Token: 0x0200073C RID: 1852
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x060024E0 RID: 9440 RVA: 0x00066E00 File Offset: 0x00065000
			internal bool c()
			{
				this.a = this.b.p.Nick;
				return AppConfig.CheckUserNickEqual(this.b.n.UserNick, this.a);
			}

			// Token: 0x04001E90 RID: 7824
			public string a;

			// Token: 0x04001E91 RID: 7825
			public AliwwBuyer9 b;
		}

		// Token: 0x0200073D RID: 1853
		[CompilerGenerated]
		private sealed class c
		{
			// Token: 0x060024E2 RID: 9442 RVA: 0x00066E40 File Offset: 0x00065040
			internal bool b()
			{
				return !this.a.OkBtn.Disabled;
			}

			// Token: 0x04001E92 RID: 7826
			public WinLoginMobileCaptchaBuyer9 a;
		}

		// Token: 0x0200073E RID: 1854
		[CompilerGenerated]
		private sealed class d
		{
			// Token: 0x060024E4 RID: 9444 RVA: 0x00066E60 File Offset: 0x00065060
			internal bool b()
			{
				return "安全验证".Equals(this.a.GetText()) || this.a.Visible;
			}

			// Token: 0x04001E93 RID: 7827
			public WinLoginImageCaptchaBuyer9 a;
		}

		// Token: 0x0200073F RID: 1855
		[CompilerGenerated]
		private sealed class e
		{
			// Token: 0x060024E6 RID: 9446 RVA: 0x00066E98 File Offset: 0x00065098
			internal bool c()
			{
				bool flag;
				if ("阿里旺旺 - 登录验证".Equals(this.b.a.GetText()))
				{
					flag = true;
				}
				else
				{
					this.a = this.b.a.MobileDropbox;
					flag = this.a != null && this.a.GetText().Contains("手机短信") && this.b.a.SendCaptchaBtn != null && this.b.a.SendCaptchaBtn.Visible && !this.b.a.SendCaptchaBtn.Disabled;
				}
				return flag;
			}

			// Token: 0x04001E94 RID: 7828
			public WindowInfo a;

			// Token: 0x04001E95 RID: 7829
			public AliwwBuyer9.c b;
		}

		// Token: 0x02000740 RID: 1856
		[CompilerGenerated]
		private sealed class f
		{
			// Token: 0x060024E8 RID: 9448 RVA: 0x00066F4C File Offset: 0x0006514C
			internal void b()
			{
				try
				{
					DirectoryInfo[] directories = this.a.GetDirectories("cust_portrait_new1");
					if (directories != null && directories.Length != 0)
					{
						foreach (DirectoryInfo directoryInfo in directories)
						{
							try
							{
								directoryInfo.Delete(true);
							}
							catch (Exception ex)
							{
								LogWriter.WriteLog(string.Format("删除文件夹出错，{0}，{1}", directoryInfo.FullName, ex), 1);
							}
						}
					}
				}
				finally
				{
					AliwwBuyer9.ac = false;
				}
			}

			// Token: 0x04001E96 RID: 7830
			public DirectoryInfo a;
		}
	}
}
