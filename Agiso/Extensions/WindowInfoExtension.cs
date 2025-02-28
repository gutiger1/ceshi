using System;
using System.Collections.Generic;
using Agiso.Windows;

namespace Agiso.Extensions
{
	// Token: 0x020006FD RID: 1789
	public static class WindowInfoExtension
	{
		// Token: 0x06002334 RID: 9012 RVA: 0x00059E90 File Offset: 0x00058090
		public static bool ContainWindow<T>(this List<T> list, WindowInfo item) where T : WindowInfo
		{
			bool flag;
			if (list == null || list.Count == 0)
			{
				flag = false;
			}
			else if (item == null)
			{
				flag = false;
			}
			else
			{
				foreach (T t in list)
				{
					if (t.HWnd == item.HWnd)
					{
						return true;
					}
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x00059F18 File Offset: 0x00058118
		public static T Convert<T>(this WindowInfo win) where T : WindowInfo, new()
		{
			T t;
			if (win == null)
			{
				t = default(T);
			}
			else
			{
				T t2 = new T();
				t2.HWnd = win.HWnd;
				IWinValid winValid = t2 as IWinValid;
				if (winValid != null && !winValid.IsValid())
				{
					t = default(T);
				}
				else
				{
					t = t2;
				}
			}
			return t;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00059F80 File Offset: 0x00058180
		public static bool TryConvert<T>(this WindowInfo win, out T result) where T : WindowInfo, new()
		{
			result = win.Convert<T>();
			return result != null;
		}
	}
}
