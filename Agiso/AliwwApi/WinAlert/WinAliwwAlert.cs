using System;
using System.Collections.Generic;
using System.Linq;
using Agiso.Handler;
using Agiso.Windows;

namespace Agiso.AliwwApi.WinAlert
{
	// Token: 0x02000731 RID: 1841
	public class WinAliwwAlert : WindowInfo, IWinValid
	{
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x00062D7C File Offset: 0x00060F7C
		public WindowInfo BtnSure1
		{
			get
			{
				if (this.e == null || this.e.HWnd == IntPtr.Zero)
				{
					this.a();
				}
				return this.e;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x00062DBC File Offset: 0x00060FBC
		public WindowInfo BtnSure2
		{
			get
			{
				if (this.f == null || this.f.HWnd == IntPtr.Zero)
				{
					this.a();
				}
				return this.f;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x00062DFC File Offset: 0x00060FFC
		public WindowInfo BtnCancel2
		{
			get
			{
				if (this.g == null || this.g.HWnd == IntPtr.Zero)
				{
					this.a();
				}
				return this.g;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600248B RID: 9355 RVA: 0x00062E3C File Offset: 0x0006103C
		public WindowInfo BtnYes3
		{
			get
			{
				if (this.h == null || this.h.HWnd == IntPtr.Zero)
				{
					this.a();
				}
				return this.h;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x00062E7C File Offset: 0x0006107C
		public WindowInfo BtnNo3
		{
			get
			{
				if (this.i == null || this.i.HWnd == IntPtr.Zero)
				{
					this.a();
				}
				return this.i;
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x00062EBC File Offset: 0x000610BC
		public WindowInfo BtnRetry4
		{
			get
			{
				if (this.j == null || this.j.HWnd == IntPtr.Zero)
				{
					this.a();
				}
				return this.j;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x00062EFC File Offset: 0x000610FC
		public WindowInfo BtnCancel4
		{
			get
			{
				if (this.k == null || this.k.HWnd == IntPtr.Zero)
				{
					this.a();
				}
				return this.k;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x00062F3C File Offset: 0x0006113C
		public WindowInfo BtnYes5
		{
			get
			{
				if (this.l == null || this.l.HWnd == IntPtr.Zero)
				{
					this.a();
				}
				return this.l;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x00062F7C File Offset: 0x0006117C
		public WindowInfo BtnNo5
		{
			get
			{
				if (this.m == null || this.m.HWnd == IntPtr.Zero)
				{
					this.a();
				}
				return this.m;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x00062FBC File Offset: 0x000611BC
		public WindowInfo BtnCancel5
		{
			get
			{
				if (this.n == null || this.n.HWnd == IntPtr.Zero)
				{
					this.a();
				}
				return this.n;
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x00062FFC File Offset: 0x000611FC
		public WindowInfo BtnStop6
		{
			get
			{
				if (this.o == null || this.o.HWnd == IntPtr.Zero)
				{
					this.a();
				}
				return this.o;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x0006303C File Offset: 0x0006123C
		public WindowInfo BtnRetry6
		{
			get
			{
				if (this.p == null || this.p.HWnd == IntPtr.Zero)
				{
					this.a();
				}
				return this.p;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x0006307C File Offset: 0x0006127C
		public WindowInfo BtnIgnore6
		{
			get
			{
				if (this.q == null || this.q.HWnd == IntPtr.Zero)
				{
					this.a();
				}
				return this.q;
			}
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x000630BC File Offset: 0x000612BC
		private void a()
		{
			List<IntPtr> childHandleList = Win32Extend.GetChildHandleList(base.HWnd, "StandardWindow", null);
			if (childHandleList != null && childHandleList.Count >= 6)
			{
				foreach (IntPtr intPtr in childHandleList)
				{
					List<IntPtr> childHandleList2 = Win32Extend.GetChildHandleList(intPtr, null, null);
					switch (childHandleList2.Count)
					{
					case 1:
					{
						WindowInfo windowFromHandler = WindowInfo.GetWindowFromHandler(childHandleList2[0]);
						if (windowFromHandler.Info.WindowName == "确定")
						{
							this.e = windowFromHandler;
							continue;
						}
						continue;
					}
					case 2:
					{
						List<WindowInfo> list = new List<WindowInfo>();
						foreach (IntPtr intPtr2 in childHandleList2)
						{
							list.Add(WindowInfo.GetWindowFromHandler(intPtr2));
						}
						if (list.FirstOrDefault(new Func<WindowInfo, bool>(WinAliwwAlert.<>c.<>9.e)) != null)
						{
							using (List<WindowInfo>.Enumerator enumerator3 = list.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									WindowInfo windowInfo = enumerator3.Current;
									string windowName = windowInfo.Info.WindowName;
									string text = windowName;
									if (!(text == "确定"))
									{
										if (text == "取消")
										{
											this.g = windowInfo;
										}
									}
									else
									{
										this.f = windowInfo;
									}
								}
								continue;
							}
						}
						if (list.FirstOrDefault(new Func<WindowInfo, bool>(WinAliwwAlert.<>c.<>9.d)) != null)
						{
							using (List<WindowInfo>.Enumerator enumerator4 = list.GetEnumerator())
							{
								while (enumerator4.MoveNext())
								{
									WindowInfo windowInfo2 = enumerator4.Current;
									string windowName2 = windowInfo2.Info.WindowName;
									string text2 = windowName2;
									if (!(text2 == "是"))
									{
										if (text2 == "否")
										{
											this.i = windowInfo2;
										}
									}
									else
									{
										this.h = windowInfo2;
									}
								}
								continue;
							}
						}
						if (list.FirstOrDefault(new Func<WindowInfo, bool>(WinAliwwAlert.<>c.<>9.c)) == null)
						{
							continue;
						}
						using (List<WindowInfo>.Enumerator enumerator5 = list.GetEnumerator())
						{
							while (enumerator5.MoveNext())
							{
								WindowInfo windowInfo3 = enumerator5.Current;
								string windowName3 = windowInfo3.Info.WindowName;
								string text3 = windowName3;
								if (!(text3 == "重试"))
								{
									if (text3 == "取消")
									{
										this.k = windowInfo3;
									}
								}
								else
								{
									this.j = windowInfo3;
								}
							}
							continue;
						}
						break;
					}
					case 3:
						break;
					default:
						return;
					}
					List<WindowInfo> list2 = new List<WindowInfo>();
					foreach (IntPtr intPtr3 in childHandleList2)
					{
						list2.Add(WindowInfo.GetWindowFromHandler(intPtr3));
					}
					if (list2.FirstOrDefault(new Func<WindowInfo, bool>(WinAliwwAlert.<>c.<>9.b)) != null)
					{
						using (List<WindowInfo>.Enumerator enumerator7 = list2.GetEnumerator())
						{
							while (enumerator7.MoveNext())
							{
								WindowInfo windowInfo4 = enumerator7.Current;
								string windowName4 = windowInfo4.Info.WindowName;
								string text4 = windowName4;
								if (!(text4 == "是"))
								{
									if (!(text4 == "否"))
									{
										if (text4 == "取消")
										{
											this.n = windowInfo4;
										}
									}
									else
									{
										this.m = windowInfo4;
									}
								}
								else
								{
									this.l = windowInfo4;
								}
							}
							continue;
						}
					}
					if (list2.FirstOrDefault(new Func<WindowInfo, bool>(WinAliwwAlert.<>c.<>9.a)) != null)
					{
						foreach (WindowInfo windowInfo5 in list2)
						{
							string windowName5 = windowInfo5.Info.WindowName;
							string text5 = windowName5;
							if (!(text5 == "终止"))
							{
								if (!(text5 == "重试"))
								{
									if (text5 == "忽略")
									{
										this.q = windowInfo5;
									}
								}
								else
								{
									this.p = windowInfo5;
								}
							}
							else
							{
								this.o = windowInfo5;
							}
						}
					}
				}
			}
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x00063624 File Offset: 0x00061824
		public virtual bool IsValid()
		{
			if (this.e == null)
			{
				this.a();
			}
			return this.e != null && this.f != null && this.g != null && this.h != null && this.i != null && this.j != null && this.k != null && this.l != null && this.m != null && this.n != null && this.o != null && this.p != null && this.q != null;
		}

		// Token: 0x04001E39 RID: 7737
		private static readonly List<string> a = new List<string>();

		// Token: 0x04001E3A RID: 7738
		private static readonly List<string> b = new List<string>();

		// Token: 0x04001E3B RID: 7739
		private static readonly List<string> c = new List<string> { "B3D0B52EFCA6DF45E832D6AADB9CB2AF" };

		// Token: 0x04001E3C RID: 7740
		private static List<string> d = new List<string>();

		// Token: 0x04001E3D RID: 7741
		private WindowInfo e;

		// Token: 0x04001E3E RID: 7742
		private WindowInfo f;

		// Token: 0x04001E3F RID: 7743
		private WindowInfo g;

		// Token: 0x04001E40 RID: 7744
		private WindowInfo h;

		// Token: 0x04001E41 RID: 7745
		private WindowInfo i;

		// Token: 0x04001E42 RID: 7746
		private WindowInfo j;

		// Token: 0x04001E43 RID: 7747
		private WindowInfo k;

		// Token: 0x04001E44 RID: 7748
		private WindowInfo l;

		// Token: 0x04001E45 RID: 7749
		private WindowInfo m;

		// Token: 0x04001E46 RID: 7750
		private WindowInfo n;

		// Token: 0x04001E47 RID: 7751
		private WindowInfo o;

		// Token: 0x04001E48 RID: 7752
		private WindowInfo p;

		// Token: 0x04001E49 RID: 7753
		private WindowInfo q;
	}
}
