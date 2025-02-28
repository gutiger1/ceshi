using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Agiso.Handler;
using Agiso.Utils;
using Agiso.Windows;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x0200074B RID: 1867
	public abstract class WinSafeTipBase : WindowInfo
	{
		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x00068238 File Offset: 0x00066438
		public bool IsCompleteLoad
		{
			get
			{
				bool flag;
				using (Bitmap bitmap = base.CaptureWindow(new Agiso.Windows.Rectangle
				{
					Left = 342,
					Top = 157,
					Right = 433,
					Bottom = 233
				}))
				{
					flag = !Util.IsBlankImg(bitmap);
				}
				return flag;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06002552 RID: 9554
		public abstract WindowInfo ChromWin { get; }

		// Token: 0x06002553 RID: 9555 RVA: 0x000682AC File Offset: 0x000664AC
		public void Slide(int retryTimes = 3)
		{
			do
			{
				base.SimulateMouseClick(392, 412, true, 1);
				Thread.Sleep(100);
				base.Slide(273, 416, 511, 414);
				Thread.Sleep(1500);
			}
			while (base.Visible && --retryTimes > 0);
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x00068310 File Offset: 0x00066510
		public void RefreshWin()
		{
			WinSafeTipBase.a a = new WinSafeTipBase.a();
			a.a = this;
			this.ChromWin.SimulateMouseClick(10, 10, false, 1);
			SendKeysExtend.SendWait("{F5}");
			if (!Util.CheckWait(1000, new Func<bool>(a.f), 300))
			{
				SendKeysExtend.SendWait("{F12}");
				a.b = null;
				Util.CheckWait(2000, new Func<bool>(a.e), 100);
				if (a.b != null)
				{
					for (int i = 0; i < 2; i++)
					{
						Thread.Sleep(500);
						a.b.SimulateMouseClick(10, 10, false, 1);
						SendKeysExtend.SendWait("{F5}");
						int num = 2000;
						Func<bool> func;
						if ((func = a.c) == null)
						{
							func = (a.c = new Func<bool>(a.d));
						}
						if (Util.CheckWait(num, func, 300))
						{
							break;
						}
					}
					a.b.Close(true);
				}
			}
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x00068410 File Offset: 0x00066610
		public static List<WinSafeTipBase> GetList(int processId = 0)
		{
			List<WinSafeTip> list = WinSafeTip.GetList(processId);
			List<WinSafeTipBase> list2 = ((list != null) ? list.ToList<WinSafeTipBase>() : null);
			if (Util.IsEmptyList<WinSafeTipBase>(list2))
			{
				List<WinSafeTip91500> list3 = WinSafeTip91500.GetList(processId);
				list2 = ((list3 != null) ? list3.ToList<WinSafeTipBase>() : null);
			}
			return list2;
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x00068450 File Offset: 0x00066650
		public static WinSafeTipBase Get(int processId)
		{
			return WinSafeTipBase.GetList(processId).FirstOrDefault<WinSafeTipBase>();
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x0006846C File Offset: 0x0006666C
		public static void CloseAll()
		{
			List<WinSafeTipBase> list = WinSafeTipBase.GetList(0);
			if (!Util.IsEmptyList<WinSafeTipBase>(list))
			{
				foreach (WinSafeTipBase winSafeTipBase in list)
				{
					winSafeTipBase.Close(true);
				}
			}
		}

		// Token: 0x0200074D RID: 1869
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x0600255D RID: 9565 RVA: 0x0000F19C File Offset: 0x0000D39C
			internal bool f()
			{
				return this.a.IsCompleteLoad;
			}

			// Token: 0x0600255E RID: 9566 RVA: 0x000684D0 File Offset: 0x000666D0
			internal bool e()
			{
				this.b = Win32Extend.GetAllDesktopWindows().FirstOrDefault(new Func<WindowInfo, bool>(WinSafeTipBase.<>c.<>9.a));
				return this.b != null;
			}

			// Token: 0x0600255F RID: 9567 RVA: 0x0000F19C File Offset: 0x0000D39C
			internal bool d()
			{
				return this.a.IsCompleteLoad;
			}

			// Token: 0x04001EB3 RID: 7859
			public WinSafeTipBase a;

			// Token: 0x04001EB4 RID: 7860
			public WindowInfo b;

			// Token: 0x04001EB5 RID: 7861
			public Func<bool> c;
		}
	}
}
