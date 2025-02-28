using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Agiso.Handler;
using Agiso.Object;
using Agiso.Utils;
using Agiso.Windows;
using AliwwClient.Cache;

namespace Agiso.AliwwApi.Qn.PlugIn
{
	// Token: 0x02000764 RID: 1892
	public class PlugInContainerManager : IPlugInContainerManager
	{
		// Token: 0x06002609 RID: 9737 RVA: 0x0000F4B8 File Offset: 0x0000D6B8
		public PlugInContainerManager(AliwwTalkWindowQn aliwwTalkWin, string userNick)
		{
			this.a = aliwwTalkWin;
			this.b = userNick;
			this.c = AppConfig.GetUserCacheOrCreate(this.b);
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x0006F55C File Offset: 0x0006D75C
		private WindowInfo WinContainerHeader
		{
			get
			{
				if (this.d == null)
				{
					this.d = this.a.FindWindowInDescendant("StandardWindow", "ID_EChatExtendHeadWidget", false, new bool?(false));
				}
				return this.d;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x0006F5A0 File Offset: 0x0006D7A0
		private WindowInfo WinContainerBody
		{
			get
			{
				if (this.e == null)
				{
					WindowInfo windowInfo = this.a.FindWindowInDescendant("StandardWindow", "ID_EChatExtendWidget", false, new bool?(false));
					if (windowInfo != null)
					{
						this.e = windowInfo.FindWindowInDescendant("StandardWindow", "EChatExtendPluginMiniappWidget", false, new bool?(false));
					}
				}
				return this.e;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x0006F604 File Offset: 0x0006D804
		public WindowInfo WinAutoReplyPlugIn
		{
			get
			{
				if (this.f == null)
				{
					WindowInfo winContainerBody = this.WinContainerBody;
					this.f = ((winContainerBody != null) ? winContainerBody.FindWindowInDescendant("StandardWindow", "EChatExtendPluginMiniappWidget_Miniapp_9124_智能客服", false, new bool?(false)) : null);
				}
				return this.f;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x0006F650 File Offset: 0x0006D850
		public WindowInfo WinAldsPlugIn
		{
			get
			{
				if (this.g == null)
				{
					WindowInfo winContainerBody = this.WinContainerBody;
					WindowInfo windowInfo = ((winContainerBody != null) ? winContainerBody.FindWindowInDescendant("PrivateWebCtrl", "EChatExtendPluginMiniappWidget_Plugin_9294_自动发货", false, new bool?(false)) : null);
					if (windowInfo != null)
					{
						this.g = windowInfo.GetParentWin();
					}
				}
				return this.g;
			}
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x0006F6A8 File Offset: 0x0006D8A8
		public ErrCodeInfo ClickAldsPlugIn()
		{
			ErrCodeInfo errCodeInfo;
			if (this.WinContainerHeader == null || this.WinContainerBody == null)
			{
				LogWriter.WriteLog("WinContainerHeader或WinContainerBody为空", 1);
				errCodeInfo = null;
			}
			else
			{
				if (!this.WinContainerBody.Visible)
				{
					Thread.Sleep(200);
					if (!this.WinContainerBody.Visible)
					{
						return null;
					}
				}
				List<WindowInfo> list = this.WinContainerBody.EnumChildWindowList();
				if (list == null)
				{
					LogWriter.WriteLog("winEChatExtendWidgetChildList == null", 1);
					errCodeInfo = null;
				}
				else
				{
					WindowInfo winAldsPlugIn = this.WinAldsPlugIn;
					if (winAldsPlugIn == null || !winAldsPlugIn.Visible)
					{
						this.WinContainerHeader.Click(195, 17, true);
						if (!Util.CheckWait(3000, new Func<bool>(this.a), 200))
						{
							LogWriter.WriteLog("WinContainerBody.EnumChildWindowList().Count < 2", 1);
							return null;
						}
						WindowInfo winAldsPlugIn2 = this.WinAldsPlugIn;
						if (winAldsPlugIn2 == null || !winAldsPlugIn2.Visible)
						{
							List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName("StandardFrame", "UIMaskWindow", 0, true, true, false);
							WindowInfo windowInfo = ((windowListByClassAndName != null) ? windowListByClassAndName.FirstOrDefault(new Func<WindowInfo, bool>(this.a)) : null);
							if (windowInfo != null)
							{
								windowInfo.Close(true);
							}
							WindowInfo windowInfo2 = this.WinContainerHeader.FindWindowInDescendant("SuperTabCtrl", null, false, new bool?(false));
							int num = 5;
							for (int i = 1; i < num; i++)
							{
								int count = this.WinContainerBody.EnumChildWindowList().Count;
								int count2 = list.Count;
								int width = windowInfo2.GetClientRect().GetWidth();
								int height = windowInfo2.GetClientRect().GetHeight();
								double num2 = ((double)i + 0.5) * (double)width / (double)num;
								int num3 = height / 2;
								this.WinContainerHeader.Click((int)num2, num3, true);
								Thread.Sleep(200);
								WindowInfo winAldsPlugIn3 = this.WinAldsPlugIn;
								if (winAldsPlugIn3 != null && winAldsPlugIn3.Visible)
								{
									break;
								}
							}
						}
					}
					WindowInfo winAldsPlugIn4 = this.WinAldsPlugIn;
					if (winAldsPlugIn4 != null && winAldsPlugIn4.Visible)
					{
						List<WindowInfo> allDesktopWindows = Win32Extend.GetAllDesktopWindows();
						foreach (WindowInfo windowInfo3 in allDesktopWindows)
						{
							if ((windowInfo3.Info.WindowName ?? "").EndsWith("-超时提醒") || (windowInfo3.Info.WindowName ?? "").EndsWith(" - 新消息提醒"))
							{
								windowInfo3.Close(true);
							}
							else if ((windowInfo3.Info.WindowName ?? "").EndsWith(" - 与服务器连接中断"))
							{
								if (windowInfo3.ProcessId == this.a.ProcessId)
								{
									return new ErrCodeInfo(ErrCodeType.QnLinkageInterrupt);
								}
								Win32Extend.KillProcessById(windowInfo3.ProcessId, null);
							}
						}
						List<WindowInfo> windowListByClassAndName2 = Win32Extend.GetWindowListByClassAndName(new WindowStruct("Qt5152QWindowToolSaveBits", "消息提醒"), 0, true);
						if (windowListByClassAndName2 != null)
						{
							windowListByClassAndName2.ForEach(new Action<WindowInfo>(PlugInContainerManager.<>c.<>9.a));
						}
						WindowInfo winContainerBody = this.WinContainerBody;
						WindowInfo windowInfo4 = ((winContainerBody != null) ? winContainerBody.FindWindowInDescendant("StandardWindow", "自动发货授权", false, new bool?(false)) : null);
						if (windowInfo4 != null && windowInfo4.Visible)
						{
							int height2 = windowInfo4.GetClientRect().GetHeight();
							windowInfo4.SimulateMouseClick(150, height2 - 30, true, 1);
						}
						errCodeInfo = null;
					}
					else
					{
						errCodeInfo = new ErrCodeInfo(ErrCodeType.AldsPlugNotFound);
					}
				}
			}
			return errCodeInfo;
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x0006FA64 File Offset: 0x0006DC64
		[CompilerGenerated]
		private bool a()
		{
			return this.WinContainerBody.EnumChildWindowList().Count >= 2;
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
		[CompilerGenerated]
		private bool a(WindowInfo A_0)
		{
			return A_0.ProcessId == this.a.ProcessId;
		}

		// Token: 0x04001F18 RID: 7960
		private AliwwTalkWindowQn a;

		// Token: 0x04001F19 RID: 7961
		private string b;

		// Token: 0x04001F1A RID: 7962
		private UserCache c;

		// Token: 0x04001F1B RID: 7963
		private WindowInfo d;

		// Token: 0x04001F1C RID: 7964
		private WindowInfo e;

		// Token: 0x04001F1D RID: 7965
		private WindowInfo f;

		// Token: 0x04001F1E RID: 7966
		private WindowInfo g;
	}
}
