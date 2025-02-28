using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi.Wangwang
{
	// Token: 0x02000741 RID: 1857
	public class AliwwTalkWindowBuyer9 : WinChromeContainerQn, IWinValid
	{
		// Token: 0x060024E9 RID: 9449 RVA: 0x00066FDC File Offset: 0x000651DC
		public static List<AliwwTalkWindowBuyer9> GetList(string userNick)
		{
			return WinFindableBase.GetList<AliwwTalkWindowBuyer9>(new WindowStruct("StandardFrame", "阿里旺旺 - " + userNick), 0, false);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0006700C File Offset: 0x0006520C
		public static AliwwTalkWindowBuyer9 Get(string userNick)
		{
			return WinFindableBase.Get<AliwwTalkWindowBuyer9>(new WindowStruct("StandardFrame", "阿里旺旺 - " + userNick), 0);
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x00067038 File Offset: 0x00065238
		public bool IsValid()
		{
			WindowInfo windowInfo = base.FindWindowInDescendant("SplitterBar", null, false, new bool?(false));
			return windowInfo != null && windowInfo.FindWindowInDescendant("RichEditComponent", null, false, new bool?(false)) != null;
		}
	}
}
