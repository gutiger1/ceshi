using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi.WinAlert
{
	// Token: 0x02000735 RID: 1845
	public class WinAlertLastTodoMsgWhileLoginQn5 : WinAlertQn5, IWinValid
	{
		// Token: 0x060024A7 RID: 9383 RVA: 0x0006378C File Offset: 0x0006198C
		public static List<WinAlertLastTodoMsgWhileLoginQn5> GetList()
		{
			return WinAlertQn5.GetList<WinAlertLastTodoMsgWhileLoginQn5>("千牛卖家工作台", 0);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000637A8 File Offset: 0x000619A8
		public override bool IsValid()
		{
			return base.NoBtn != null && base.NoBtn.Visible && base.YesBtn != null && base.YesBtn.Visible;
		}
	}
}
