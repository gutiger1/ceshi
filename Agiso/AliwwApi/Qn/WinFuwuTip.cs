using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x02000744 RID: 1860
	public class WinFuwuTip : WindowInfo
	{
		// Token: 0x060024FD RID: 9469 RVA: 0x00067638 File Offset: 0x00065838
		public static List<WinFuwuTip> GetList(int processId = 0)
		{
			return WinFindableBase.GetList<WinFuwuTip>(new WindowStruct("Qt5152QWindowIcon", "-服务态度提醒"), processId, true);
		}
	}
}
