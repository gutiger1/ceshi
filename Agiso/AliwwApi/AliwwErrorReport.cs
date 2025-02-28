using System;
using System.Collections.Generic;
using Agiso.Handler;
using Agiso.Windows;

namespace Agiso.AliwwApi
{
	// Token: 0x0200070B RID: 1803
	public class AliwwErrorReport
	{
		// Token: 0x0600239A RID: 9114 RVA: 0x0000EC2E File Offset: 0x0000CE2E
		public static void Close()
		{
			AliwwErrorReport.CloseQn(0);
			AliwwErrorReport.CloseWw(0);
			AliwwErrorReport.CloseErrorQn();
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0005D820 File Offset: 0x0005BA20
		public static bool CloseQn(int processId = 0)
		{
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct("#32770", "千牛错误报告程序"), 0, false);
			bool flag = false;
			if (windowListByClassAndName != null)
			{
				foreach (WindowInfo windowInfo in windowListByClassAndName)
				{
					if (processId > 0 && !flag && windowInfo.ProcessId == processId)
					{
						flag = true;
					}
					windowInfo.Close(true);
				}
			}
			return flag;
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0005D8AC File Offset: 0x0005BAAC
		public static bool CloseErrorQn()
		{
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct("#32770", "千牛[9.11.01N] - AliWorkbench.exe"), 0, false);
			bool flag = false;
			if (windowListByClassAndName != null)
			{
				foreach (WindowInfo windowInfo in windowListByClassAndName)
				{
					windowInfo.Close(true);
				}
			}
			return flag;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x0005D920 File Offset: 0x0005BB20
		public static bool CloseWw(int processId = 0)
		{
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct("#32770", "阿里旺旺错误报告程序"), 0, false);
			bool flag = false;
			if (windowListByClassAndName != null)
			{
				foreach (WindowInfo windowInfo in windowListByClassAndName)
				{
					if (processId > 0 && !flag && windowInfo.ProcessId == processId)
					{
						flag = true;
					}
					windowInfo.Close(true);
				}
			}
			return flag;
		}
	}
}
