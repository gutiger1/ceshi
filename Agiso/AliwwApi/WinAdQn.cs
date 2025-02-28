using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi
{
	// Token: 0x0200070A RID: 1802
	public class WinAdQn : WindowInfo, IWinValid
	{
		// Token: 0x06002396 RID: 9110 RVA: 0x0005D71C File Offset: 0x0005B91C
		public static List<WinAdQn> GetList()
		{
			return WinFindableBase.GetList<WinAdQn>(new WindowStruct("#32770", null), 0, false);
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x0005D740 File Offset: 0x0005B940
		public static WinAdQn Get()
		{
			return WinFindableBase.Get<WinAdQn>(new WindowStruct("#32770", null), 0);
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x0005D764 File Offset: 0x0005B964
		public bool IsValid()
		{
			WindowTreeNode treeNode = base.GetTreeNode();
			bool flag;
			if (treeNode == null)
			{
				flag = false;
			}
			else if (treeNode.ChildList.Count != 1)
			{
				flag = false;
			}
			else if (treeNode.ChildList[0].ChildList.Count != 1)
			{
				flag = false;
			}
			else if (treeNode.ChildList[0].ChildList[0].ChildList.Count != 1)
			{
				flag = false;
			}
			else
			{
				WindowTreeNode windowTreeNode = treeNode.ChildList[0];
				flag = "PrivateWebCtrl".Equals(windowTreeNode.Info.ClassName);
			}
			return flag;
		}
	}
}
