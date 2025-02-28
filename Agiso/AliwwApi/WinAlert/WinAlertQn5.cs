using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.AliwwApi.WinAlert
{
	// Token: 0x0200072F RID: 1839
	public class WinAlertQn5 : WindowInfo, IWinValid
	{
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x00062B24 File Offset: 0x00060D24
		public WindowInfo ConfirmBtn
		{
			get
			{
				if (this.a == null)
				{
					this.a = base.FindWindowInDescendant("StandardButton", "确定", false, new bool?(false));
					if (this.a == null)
					{
						this.a = base.FindWindowInDescendant("StandardButton", "确 定", false, new bool?(false));
					}
					if (this.a == null)
					{
						this.a = base.FindWindowInDescendant("StandardButton", "确  定", false, new bool?(false));
					}
				}
				return this.a;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x00062BBC File Offset: 0x00060DBC
		public WindowInfo CancelBtn
		{
			get
			{
				if (this.b == null)
				{
					this.b = base.FindWindowInDescendant("StandardButton", "取消", false, new bool?(false));
					if (this.b == null)
					{
						this.b = base.FindWindowInDescendant("StandardButton", "取 消", false, new bool?(false));
					}
					if (this.b == null)
					{
						this.b = base.FindWindowInDescendant("StandardButton", "取  消", false, new bool?(false));
					}
				}
				return this.b;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x00062C54 File Offset: 0x00060E54
		public WindowInfo YesBtn
		{
			get
			{
				if (this.c == null)
				{
					this.c = base.FindWindowInDescendant("StandardButton", "是", false, new bool?(false));
				}
				return this.c;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x00062C94 File Offset: 0x00060E94
		public WindowInfo NoBtn
		{
			get
			{
				if (this.d == null)
				{
					this.d = base.FindWindowInDescendant("StandardButton", "否", false, new bool?(false));
				}
				return this.d;
			}
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x00062CD4 File Offset: 0x00060ED4
		protected static List<T> GetList<T>(string title, int processId = 0) where T : WinAlertQn5, new()
		{
			return WinFindableBase.GetList<T>(new WindowStruct("#32770", title), processId, false);
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x00062CF8 File Offset: 0x00060EF8
		public virtual bool IsValid()
		{
			return this.ConfirmBtn != null || this.YesBtn != null;
		}

		// Token: 0x04001E35 RID: 7733
		private WindowInfo a;

		// Token: 0x04001E36 RID: 7734
		private WindowInfo b;

		// Token: 0x04001E37 RID: 7735
		private WindowInfo c;

		// Token: 0x04001E38 RID: 7736
		private WindowInfo d;
	}
}
