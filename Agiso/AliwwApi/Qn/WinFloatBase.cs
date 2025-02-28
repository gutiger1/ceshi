using System;
using System.Collections.Generic;
using System.Linq;
using Agiso.Utils;
using Agiso.Windows;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x02000746 RID: 1862
	public abstract class WinFloatBase : WindowInfo
	{
		// Token: 0x06002503 RID: 9475 RVA: 0x00067688 File Offset: 0x00065888
		public static List<WinFloatBase> GetList()
		{
			List<WinFloat> list = WinFloat.GetList();
			List<WinFloatBase> list2 = ((list != null) ? list.ToList<WinFloatBase>() : null);
			if (Util.IsEmptyList<WinFloatBase>(list2))
			{
				List<WinFloat91500> list3 = WinFloat91500.GetList();
				list2 = ((list3 != null) ? list3.ToList<WinFloatBase>() : null);
			}
			return list2;
		}

		// Token: 0x06002504 RID: 9476
		public abstract void CallAliwwTalkWin();
	}
}
