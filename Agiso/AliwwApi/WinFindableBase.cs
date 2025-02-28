using System;
using System.Collections.Generic;
using Agiso.Extensions;
using Agiso.Handler;
using Agiso.Windows;

namespace Agiso.AliwwApi
{
	// Token: 0x02000717 RID: 1815
	public class WinFindableBase
	{
		// Token: 0x060023F0 RID: 9200 RVA: 0x0005F670 File Offset: 0x0005D870
		public static List<T> GetList<T>(WindowStruct ws, int processId = 0, bool allowMatchBlur = false) where T : WindowInfo, new()
		{
			List<T> list = new List<T>();
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(ws, processId, allowMatchBlur);
			if (windowListByClassAndName != null && windowListByClassAndName.Count > 0)
			{
				foreach (WindowInfo windowInfo in windowListByClassAndName)
				{
					T t;
					if (windowInfo.TryConvert(out t))
					{
						list.Add(t);
					}
				}
			}
			return list;
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x0005F6F4 File Offset: 0x0005D8F4
		public static T Get<T>(WindowStruct ws, int processId = 0) where T : WindowInfo, new()
		{
			WindowInfo windowInfo = Win32Extend.FindWindowByClassAndName(ws.ClassName, ws.WindowName);
			T t;
			T t2;
			if (windowInfo == null)
			{
				t = default(T);
			}
			else if (processId > 0 && windowInfo.ProcessId != processId)
			{
				t = default(T);
			}
			else if (windowInfo.TryConvert(out t2))
			{
				t = t2;
			}
			else
			{
				t = default(T);
			}
			return t;
		}
	}
}
