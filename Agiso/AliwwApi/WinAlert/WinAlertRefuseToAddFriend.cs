using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi.WinAlert
{
	// Token: 0x02000730 RID: 1840
	public class WinAlertRefuseToAddFriend : WinAlertQn5, IWinValid
	{
		// Token: 0x06002485 RID: 9349 RVA: 0x00062D24 File Offset: 0x00060F24
		public static List<WinAlertRefuseToAddFriend> GetList(int processId = 0)
		{
			return WinAlertQn5.GetList<WinAlertRefuseToAddFriend>("阿里旺旺", processId);
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x00062D40 File Offset: 0x00060F40
		public override bool IsValid()
		{
			return base.ConfirmBtn != null && base.YesBtn == null && base.NoBtn == null && base.CancelBtn == null;
		}
	}
}
