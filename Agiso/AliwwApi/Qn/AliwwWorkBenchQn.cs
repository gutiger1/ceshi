using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Agiso.AliwwApi.Object;
using Agiso.Utils;
using Agiso.Windows;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x02000760 RID: 1888
	public class AliwwWorkBenchQn : WinChromeContainerQn, IWinValid
	{
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x0006F178 File Offset: 0x0006D378
		public string UserNick
		{
			get
			{
				return base.Info.WindowName.Replace(" - 工作台", "").Replace("-千牛工作台", "");
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x0000F47F File Offset: 0x0000D67F
		// (set) Token: 0x060025FF RID: 9727 RVA: 0x0000F487 File Offset: 0x0000D687
		public AliwwVersion AliwwVersion { get; set; }

		// Token: 0x06002600 RID: 9728 RVA: 0x0006F1B4 File Offset: 0x0006D3B4
		public static List<AliwwWorkBenchQn> GetList(string userNick)
		{
			List<AliwwWorkBenchQn> list = WinFindableBase.GetList<AliwwWorkBenchQn>(new WindowStruct("StandardFrame", userNick + " - 工作台"), 0, false) ?? new List<AliwwWorkBenchQn>();
			if (Util.IsEmptyList<AliwwWorkBenchQn>(list))
			{
				list = WinFindableBase.GetList<AliwwWorkBenchQn>(new WindowStruct("Qt5152QWindowIcon", userNick + "-千牛工作台"), 0, false) ?? new List<AliwwWorkBenchQn>();
			}
			return list;
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x0006F21C File Offset: 0x0006D41C
		public static AliwwWorkBenchQn Get(string userNick)
		{
			AliwwWorkBenchQn aliwwWorkBenchQn = WinFindableBase.Get<AliwwWorkBenchQn>(new WindowStruct("StandardFrame", userNick + " - 工作台"), 0);
			if (aliwwWorkBenchQn == null)
			{
				aliwwWorkBenchQn = WinFindableBase.Get<AliwwWorkBenchQn>(new WindowStruct("Qt5152QWindowIcon", userNick + "-千牛工作台"), 0);
			}
			return aliwwWorkBenchQn;
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x0006F270 File Offset: 0x0006D470
		public static AliwwWorkBenchQn Get(int processId)
		{
			List<AliwwWorkBenchQn> list = WinFindableBase.GetList<AliwwWorkBenchQn>(new WindowStruct("StandardFrame", " - 工作台"), processId, true);
			if (Util.IsEmptyList<AliwwWorkBenchQn>(list))
			{
				list = WinFindableBase.GetList<AliwwWorkBenchQn>(new WindowStruct("Qt5152QWindowIcon", "-千牛工作台"), processId, true);
			}
			return list.FirstOrDefault<AliwwWorkBenchQn>();
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x0006F2C0 File Offset: 0x0006D4C0
		public bool IsValid()
		{
			bool flag;
			if (base.Info.WindowName.EndsWith("-千牛工作台"))
			{
				this.AliwwVersion = AliwwVersion.QianNiu9;
				flag = base.FindWindowInDescendant("Qt5152QWindowIcon", "千牛工作台", false, new bool?(false)) != null;
			}
			else
			{
				this.AliwwVersion = AliwwVersion.QianNiu5;
				WindowInfo windowInfo = base.FindWindowInDescendant("Aef_WidgetWin_0", "benchwebctrl:custompage_datapanel", false, new bool?(false));
				if (windowInfo != null)
				{
					flag = true;
				}
				else
				{
					windowInfo = base.FindWindowInDescendant("Aef_WidgetWin_0", "benchwebctrl:custompage_homepage", false, new bool?(false));
					if (windowInfo != null)
					{
						flag = true;
					}
					else
					{
						WindowInfo windowInfo2 = base.FindWindowInDescendant("EditComponent", "搜索应用、常用网址", false, new bool?(false));
						if (windowInfo2 != null)
						{
							flag = true;
						}
						else
						{
							List<WindowInfo> list = base.FindWindowsInDescendant("EditComponent", null, false, new bool?(false));
							if (list != null)
							{
								foreach (WindowInfo windowInfo3 in list)
								{
									string text = windowInfo3.GetText();
									if ("搜索应用、常用网址".Equals(text))
									{
										return true;
									}
									if ("搜索功能、网址、应用".Equals(text))
									{
										return true;
									}
									if ("搜索网址/功能/商品/头条/帮助".Equals(text))
									{
										return true;
									}
								}
							}
							WindowInfo windowInfo4 = base.FindWindowInDescendant("ToolBarPlus", null, false, new bool?(false));
							WindowInfo windowInfo5 = base.FindWindowInDescendant("StackPanel", null, false, new bool?(false));
							WindowInfo windowInfo6 = base.FindWindowInDescendant("SuperTabCtrl", null, false, new bool?(false));
							WindowInfo windowInfo7 = base.FindWindowInDescendant("Aef_WidgetWin_0", null, false, new bool?(false));
							flag = (windowInfo4 != null && windowInfo5 != null && windowInfo7 != null) || (windowInfo5 != null && windowInfo6 != null && windowInfo7 != null);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x04001F14 RID: 7956
		[CompilerGenerated]
		private AliwwVersion a;
	}
}
