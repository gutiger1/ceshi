using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Agiso.AliwwApi.Object;
using Agiso.Extensions;
using Agiso.Handler;
using Agiso.Object;
using Agiso.Windows;
using AliwwClient.Cache;
using AliwwClient.WebSocketServer;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x0200075D RID: 1885
	public class AliwwTalkWindowQn : WinChromeContainerQn, IWinValid
	{
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x0000F3FE File Offset: 0x0000D5FE
		public string UserNick
		{
			get
			{
				return base.Info.WindowName.Replace(" - 接待中心", "").Replace("-接待中心", "").Replace("-千牛", "");
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x0000F43B File Offset: 0x0000D63B
		// (set) Token: 0x060025F2 RID: 9714 RVA: 0x0000F443 File Offset: 0x0000D643
		public AliwwVersion AliwwVersion { get; set; }

		// Token: 0x060025F3 RID: 9715 RVA: 0x0006EE5C File Offset: 0x0006D05C
		public static List<AliwwTalkWindowQn> GetList(int ProcessId = 0)
		{
			IEnumerable<WindowInfo> enumerable = Win32Extend.GetAllDesktopWindows().Where(new Func<WindowInfo, bool>(AliwwTalkWindowQn.<>c.<>9.a));
			List<AliwwTalkWindowQn> list = new List<AliwwTalkWindowQn>();
			foreach (WindowInfo windowInfo in enumerable)
			{
				AliwwTalkWindowQn aliwwTalkWindowQn;
				if ((ProcessId <= 0 || windowInfo.ProcessId == ProcessId) && windowInfo.TryConvert(out aliwwTalkWindowQn))
				{
					list.Add(aliwwTalkWindowQn);
				}
			}
			return list;
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x0006EEFC File Offset: 0x0006D0FC
		public static AliwwTalkWindowQn Get(string userNick)
		{
			AliwwTalkWindowQn aliwwTalkWindowQn = WinFindableBase.Get<AliwwTalkWindowQn>(new WindowStruct("StandardFrame", userNick + " - 接待中心"), 0);
			if (aliwwTalkWindowQn == null)
			{
				aliwwTalkWindowQn = WinFindableBase.Get<AliwwTalkWindowQn>(new WindowStruct("Qt5152QWindowIcon", userNick + "-接待中心"), 0);
			}
			if (aliwwTalkWindowQn == null)
			{
				aliwwTalkWindowQn = WinFindableBase.Get<AliwwTalkWindowQn>(new WindowStruct("Qt5152QWindowIcon", userNick + "-千牛"), 0);
			}
			return aliwwTalkWindowQn;
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x0006EF74 File Offset: 0x0006D174
		public bool IsValid()
		{
			bool flag;
			if (base.Info.WindowName.EndsWith("-接待中心") || base.Info.WindowName.EndsWith("-千牛"))
			{
				this.AliwwVersion = AliwwVersion.QianNiu9;
				flag = base.FindWindowInDescendant("Qt5152QWindowIcon", "千牛工作台", false, new bool?(false)) != null;
			}
			else
			{
				this.AliwwVersion = AliwwVersion.QianNiu5;
				WindowInfo windowInfo = base.FindWindowInDescendant("SplitterBar", null, false, new bool?(false));
				flag = windowInfo != null && windowInfo.FindWindowInDescendant("RichEditComponent", null, false, new bool?(false)) != null;
			}
			return flag;
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x0006F024 File Offset: 0x0006D224
		public override void KillProcess()
		{
			AliwwTalkWindowQn.a a = new AliwwTalkWindowQn.a();
			UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(this.UserNick);
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
			base.KillProcess();
			a.a = base.ProcessId;
			Task.Run(new Action(a.b));
			userCacheOrCreate.ClearRecentSession();
			userCacheOrCreate.ClearAldsSession();
			userCacheOrCreate.LastSendProcessId = 0;
			userCacheOrCreate.LastSendSoftware = MsgSendSoftware.Undefined;
		}

		// Token: 0x04001F10 RID: 7952
		[CompilerGenerated]
		private AliwwVersion a;

		// Token: 0x0200075F RID: 1887
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x060025FC RID: 9724 RVA: 0x0000F459 File Offset: 0x0000D659
			internal void b()
			{
				Win32Extend.KillProcessAndChildrenById(this.a, 2, "AliApp");
				Win32Extend.KillProcessAndChildrenById(this.a, 2, "AliRender");
			}

			// Token: 0x04001F13 RID: 7955
			public int a;
		}
	}
}
