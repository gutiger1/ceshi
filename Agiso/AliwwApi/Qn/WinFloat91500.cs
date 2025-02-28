using System;
using System.Collections.Generic;
using System.Threading;
using Agiso.Windows;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x02000745 RID: 1861
	public class WinFloat91500 : WinFloatBase, IWinValid
	{
		// Token: 0x060024FF RID: 9471 RVA: 0x00067660 File Offset: 0x00065860
		public new static List<WinFloat91500> GetList()
		{
			return WinFindableBase.GetList<WinFloat91500>(new WindowStruct("Qt5152QWindowToolSaveBits", "悬浮条"), 0, false);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x0000F0AF File Offset: 0x0000D2AF
		public override void CallAliwwTalkWin()
		{
			base.Click(105, 55, true);
			Thread.Sleep(100);
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x0003A434 File Offset: 0x00038634
		public bool IsValid()
		{
			return true;
		}
	}
}
