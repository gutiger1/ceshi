using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi
{
	// Token: 0x02000707 RID: 1799
	public class WinOutOfMemory : WindowInfo, IWinValid
	{
		// Token: 0x0600238E RID: 9102 RVA: 0x0005D508 File Offset: 0x0005B708
		public static List<WinOutOfMemory> GetList()
		{
			return WinFindableBase.GetList<WinOutOfMemory>(new WindowStruct("#32770", "Microsoft Windows"), 0, false);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x0005D530 File Offset: 0x0005B730
		public bool IsValid()
		{
			WindowInfo windowInfo = base.FindWindowInDescendant("DirectUIHWND", "", false, new bool?(false));
			WindowInfo windowInfo2;
			if (windowInfo != null)
			{
				if ((windowInfo2 = windowInfo.FindWindowInDescendant("Button", "关闭程序", false, new bool?(true))) != null)
				{
					goto IL_005B;
				}
			}
			windowInfo2 = ((windowInfo != null) ? windowInfo.FindWindowInDescendant("Button", "确定", false, new bool?(true)) : null);
			IL_005B:
			WindowInfo windowInfo3 = windowInfo2;
			return windowInfo != null && windowInfo.Visible && windowInfo3 != null && windowInfo3.Visible;
		}
	}
}
