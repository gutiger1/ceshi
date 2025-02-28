using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi.WinAlert
{
	// Token: 0x02000734 RID: 1844
	public class WinFileNotAcceptAlert : WinAliwwAlert
	{
		// Token: 0x060024A4 RID: 9380 RVA: 0x000636B0 File Offset: 0x000618B0
		public static List<WinFileNotAcceptAlert> GetList(int processId = 0)
		{
			return WinFindableBase.GetList<WinFileNotAcceptAlert>(new WindowStruct("#32770", "阿里旺旺"), processId, false);
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x0006373C File Offset: 0x0006193C
		public override bool IsValid()
		{
			return base.IsValid() && (base.BtnSure2 != null && base.BtnSure2.Visible && base.BtnCancel2 != null) && base.BtnCancel2.Visible;
		}
	}
}
