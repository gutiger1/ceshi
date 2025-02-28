using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi.WinAlert
{
	// Token: 0x0200072E RID: 1838
	public class WinAlertNeedToAddFriend : WinAlertQn5, IWinValid
	{
		// Token: 0x0600247B RID: 9339 RVA: 0x00062ADC File Offset: 0x00060CDC
		public static List<WinAlertNeedToAddFriend> GetList(int processId = 0)
		{
			return WinAlertQn5.GetList<WinAlertNeedToAddFriend>("添加好友", processId);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x00062AF8 File Offset: 0x00060CF8
		public override bool IsValid()
		{
			return base.ConfirmBtn != null && base.CancelBtn != null;
		}
	}
}
