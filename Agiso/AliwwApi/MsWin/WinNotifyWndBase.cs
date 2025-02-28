using System;
using Agiso.Windows;

namespace Agiso.AliwwApi.MsWin
{
	// Token: 0x02000737 RID: 1847
	public abstract class WinNotifyWndBase : WindowInfo
	{
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060024AE RID: 9390
		public abstract WindowInfo NotifyAreaWin { get; }

		// Token: 0x060024AF RID: 9391 RVA: 0x00063868 File Offset: 0x00061A68
		public void RefreshWinNotifyArea()
		{
			Rectangle rectangle;
			WindowsAPI.GetClientRect(this.NotifyAreaWin.HWnd, out rectangle);
			for (int i = 0; i < rectangle.Right; i += 4)
			{
				for (int j = 0; j < rectangle.Bottom; j += 4)
				{
					IntPtr intPtr = (IntPtr)((j << 16) | (i & 65535));
					WindowsAPI.SendMessage(this.NotifyAreaWin.HWnd, 512, 0, intPtr);
				}
			}
		}
	}
}
