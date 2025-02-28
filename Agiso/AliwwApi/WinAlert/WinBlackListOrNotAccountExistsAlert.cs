using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi.WinAlert
{
	// Token: 0x02000733 RID: 1843
	public class WinBlackListOrNotAccountExistsAlert : WinAliwwAlert
	{
		// Token: 0x060024A0 RID: 9376 RVA: 0x000636B0 File Offset: 0x000618B0
		public static List<WinFileNotAcceptAlert> GetList(int processId = 0)
		{
			return WinFindableBase.GetList<WinFileNotAcceptAlert>(new WindowStruct("#32770", "阿里旺旺"), processId, false);
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000636D8 File Offset: 0x000618D8
		public static List<WinFileNotAcceptAlert> GetListQn(int processId = 0)
		{
			return WinFindableBase.GetList<WinFileNotAcceptAlert>(new WindowStruct("#32770", "千牛"), processId, false);
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x00063700 File Offset: 0x00061900
		public override bool IsValid()
		{
			return base.IsValid() && base.BtnSure1 != null && base.BtnSure1.Visible;
		}
	}
}
