using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Agiso.Utils;
using Agiso.Windows;
using mshtml;

namespace Agiso.Handler
{
	// Token: 0x020000F3 RID: 243
	public static class Win32Extend
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x0004DAF8 File Offset: 0x0004BCF8
		public static WindowInfo FindWindowByClassAndName(string szClassName, string szWindowName)
		{
			IntPtr intPtr = WindowsAPI.FindWindow(szClassName, szWindowName);
			if (intPtr == IntPtr.Zero)
			{
				string text = Util.StrConvSimple(szWindowName);
				string text2 = Util.StrConvSimple(szClassName);
				intPtr = WindowsAPI.FindWindow(text2, text);
			}
			if (intPtr == IntPtr.Zero)
			{
				string text3 = Util.StrConvTraditional(szWindowName);
				string text4 = Util.StrConvTraditional(szClassName);
				intPtr = WindowsAPI.FindWindow(text4, text3);
			}
			return WindowInfo.GetWindowFromHandler(intPtr);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0004DB64 File Offset: 0x0004BD64
		public static List<WindowInfo> GetWindowListByClass(string szClassName)
		{
			List<WindowInfo> list;
			if (string.IsNullOrEmpty(szClassName))
			{
				list = null;
			}
			else
			{
				list = Win32Extend.GetWindowListByClassAndName(new WindowStruct(szClassName, ""), 0, false);
			}
			return list;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0004DB94 File Offset: 0x0004BD94
		public static WindowStruct GetWindowInfoFromHandler(IntPtr hWnd)
		{
			WindowStruct windowStruct;
			if (hWnd == IntPtr.Zero)
			{
				windowStruct = null;
			}
			else
			{
				WindowStruct windowStruct2 = new WindowStruct();
				int num;
				WindowsAPI.GetWindowThreadProcessId(hWnd, out num);
				StringBuilder stringBuilder = new StringBuilder(256);
				if (num != Win32Extend.a.Id)
				{
					WindowsAPI.GetWindowText(hWnd, stringBuilder, stringBuilder.Capacity);
					windowStruct2.WindowName = stringBuilder.ToString();
				}
				else
				{
					windowStruct2.WindowName = "";
				}
				WindowsAPI.GetClassName(hWnd, stringBuilder, stringBuilder.Capacity);
				windowStruct2.ClassName = stringBuilder.ToString();
				windowStruct = windowStruct2;
			}
			return windowStruct;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0004DC28 File Offset: 0x0004BE28
		public static List<WindowInfo> GetWindowListByClassAndName(WindowStruct wnd, int processId = 0, bool allowMatchBlur = false)
		{
			return Win32Extend.GetWindowListByClassAndName(wnd.ClassName, wnd.WindowName, processId, true, true, allowMatchBlur);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0004DC50 File Offset: 0x0004BE50
		public static List<WindowInfo> GetWindowListByClassAndName(string className, string windowName, int processId = 0, bool checkSimple = true, bool checkTraditional = true, bool allowMatchBlur = false)
		{
			List<WindowInfo> list = new List<WindowInfo>();
			List<WindowInfo> list2 = Win32Extend.GetAllDesktopWindows().Where(new Func<WindowInfo, bool>(Win32Extend.<>c.<>9.a)).ToList<WindowInfo>();
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			if (checkSimple)
			{
				text = Util.StrConvSimple(windowName);
				text2 = Util.StrConvSimple(className);
			}
			if (checkTraditional)
			{
				text3 = Util.StrConvTraditional(windowName);
				text4 = Util.StrConvTraditional(className);
			}
			for (int i = 0; i < list2.Count; i++)
			{
				if (processId <= 0 || list2[i].ProcessId == processId)
				{
					WindowInfo windowInfo = list2[i];
					if (Win32Extend.a(windowInfo.Info.ClassName, windowInfo.Info.WindowName, className, windowName, allowMatchBlur))
					{
						list.Add(list2[i]);
					}
					else if (checkSimple && Win32Extend.a(Util.StrConvSimple(windowInfo.Info.ClassName), Util.StrConvSimple(windowInfo.Info.WindowName), text2, text, allowMatchBlur))
					{
						list.Add(list2[i]);
					}
					else if (checkTraditional && Win32Extend.a(Util.StrConvTraditional(windowInfo.Info.ClassName), Util.StrConvTraditional(windowInfo.Info.WindowName), text4, text3, allowMatchBlur))
					{
						list.Add(list2[i]);
					}
				}
			}
			return list;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0004DDDC File Offset: 0x0004BFDC
		private static bool a(string A_0, string A_1, string A_2, string A_3, bool A_4)
		{
			bool flag;
			if (A_4)
			{
				flag = (string.IsNullOrEmpty(A_2) || A_0.Contains(A_2)) && (string.IsNullOrEmpty(A_3) || A_1.Contains(A_3));
			}
			else
			{
				flag = (A_2 == null || A_2 == A_0) && (A_3 == null || A_3 == A_1);
			}
			return flag;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0004DE34 File Offset: 0x0004C034
		public static List<WindowInfo> GetAllDesktopWindows()
		{
			Win32Extend.c c = new Win32Extend.c();
			c.a = new List<WindowInfo>();
			WindowsAPI.EnumWindows(new WindowsAPI.WNDENUMPROC(c.b), 0);
			return c.a;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0004DE70 File Offset: 0x0004C070
		public static uint GetMemoryLoad()
		{
			MEMORYSTATUS memorystatus = default(MEMORYSTATUS);
			WindowsAPI.GlobalMemoryStatus(ref memorystatus);
			return memorystatus.dwMemoryLoad;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0004DE94 File Offset: 0x0004C094
		public static object GetHtmlDocument(IntPtr hWnd)
		{
			object obj = new object();
			Guid guid = default(Guid);
			uint num = WindowsAPI.RegisterWindowMessage("WM_Html_GETOBJECT");
			uint num2 = WindowsAPI.SendMessage(hWnd, num, 0U, 0U);
			WindowsAPI.ObjectFromLresult(num2, ref guid, 0, ref obj);
			return obj;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0004DED8 File Offset: 0x0004C0D8
		private static HtmlDocument a(IntPtr A_0)
		{
			WebBrowser webBrowser = new WebBrowser();
			string htmlText = Win32Extend.GetHtmlText(A_0);
			webBrowser.DocumentText = "<html></html>";
			webBrowser.ScriptErrorsSuppressed = true;
			webBrowser.Document.Write(htmlText);
			return webBrowser.Document;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0004DF1C File Offset: 0x0004C11C
		public static string GetHtmlText(IntPtr hWnd)
		{
			object obj = new object();
			Guid guid = default(Guid);
			uint num = WindowsAPI.RegisterWindowMessage("WM_Html_GETOBJECT");
			uint num2 = WindowsAPI.SendMessage(hWnd, num, 0U, 0U);
			WindowsAPI.ObjectFromLresult(num2, ref guid, 0, ref obj);
			try
			{
				IHTMLDocument2 ihtmldocument = (IHTMLDocument2)obj;
				if (ihtmldocument == null || ihtmldocument.body == null)
				{
					return "";
				}
				return ihtmldocument.body.innerHTML;
			}
			catch
			{
				LogWriter.WriteLog("丢失了 mshtml 文件，无法用接口获得发送结果。改用反射方式获得发送结果！", 1);
			}
			string text;
			if (obj == null)
			{
				text = "";
			}
			else
			{
				PropertyInfo property = obj.GetType().GetProperty("body");
				if (property == null)
				{
					LogWriter.WriteLog("不支持使用反射方式获得发送结果，返回空。此时默认发送结果为成功！", 1);
					text = "";
				}
				else
				{
					object value = obj.GetType().GetProperty("body").GetValue(obj, null);
					PropertyInfo property2 = value.GetType().GetProperty("outerHTML");
					if (property2 == null)
					{
						LogWriter.WriteLog("outerHTMLProperty is null", 1);
						text = "";
					}
					else
					{
						object value2 = value.GetType().GetProperty("outerHTML").GetValue(value, null);
						if (value2 == null)
						{
							text = "";
						}
						else
						{
							text = value2.ToString();
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0004E084 File Offset: 0x0004C284
		public static string GetText(IntPtr hWnd)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(202);
			WindowsAPI.SendMessage(hWnd, 13, 100, intPtr);
			string text = Marshal.PtrToStringAnsi(intPtr);
			Marshal.FreeHGlobal(intPtr);
			return text;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0004E0B8 File Offset: 0x0004C2B8
		public static bool ComparisonStringIsMatch(string source, string target, CompareWindowOption option = null)
		{
			bool flag;
			if (source == null)
			{
				flag = true;
			}
			else if (option == null)
			{
				flag = source.Equals(target);
			}
			else if (target == null)
			{
				flag = false;
			}
			else if (option.IsAllowBlur)
			{
				flag = target.IndexOf(source, option.ComparisonType) >= 0;
			}
			else
			{
				flag = target.Equals(source, option.ComparisonType);
			}
			return flag;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0004E118 File Offset: 0x0004C318
		public static List<IntPtr> GetChildHandleList(IntPtr hwndParent, string className, string windowName = null)
		{
			List<IntPtr> list = new List<IntPtr>();
			IntPtr intPtr = WindowsAPI.FindWindowEx(hwndParent, IntPtr.Zero, className, windowName);
			while (intPtr != IntPtr.Zero)
			{
				list.Add(intPtr);
				intPtr = WindowsAPI.FindWindowEx(hwndParent, intPtr, className, windowName);
			}
			return list;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0004E15C File Offset: 0x0004C35C
		public static List<WindowInfo> FindWindowsInDescendant(IntPtr hwndParent, FindWindowOption option, string className, string windowName, bool? onlyVisit = false)
		{
			List<WindowInfo> list = new List<WindowInfo>();
			List<IntPtr> childHandleList = Win32Extend.GetChildHandleList(hwndParent, null, null);
			foreach (IntPtr intPtr in childHandleList)
			{
				WindowInfo windowFromHandler = WindowInfo.GetWindowFromHandler(intPtr);
				if (windowFromHandler != null && (!onlyVisit.GetValueOrDefault() || windowFromHandler.Visible))
				{
					bool flag = true;
					if (className != null)
					{
						flag = Win32Extend.ComparisonStringIsMatch(className, windowFromHandler.Info.ClassName, option.ClassNameComparisonType);
					}
					if (flag && windowName != null)
					{
						flag = Win32Extend.ComparisonStringIsMatch(windowName, windowFromHandler.Info.WindowName, option.WindowNameComparisonType);
					}
					if (flag)
					{
						list.Add(windowFromHandler);
						if (option.IsOnlyFirst)
						{
							return list;
						}
					}
					List<WindowInfo> list2 = Win32Extend.FindWindowsInDescendant(windowFromHandler.HWnd, option, className, windowName, onlyVisit);
					if (list2 != null && list2.Count > 0)
					{
						list.AddRange(list2);
						if (option.IsOnlyFirst)
						{
							return list;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0004E28C File Offset: 0x0004C48C
		public static WindowTreeNode GetTreeNode(IntPtr hwnd)
		{
			WindowInfo windowFromHandler = WindowInfo.GetWindowFromHandler(hwnd);
			WindowTreeNode windowTreeNode;
			if (windowFromHandler == null || windowFromHandler.ProcessId <= 0)
			{
				windowTreeNode = null;
			}
			else
			{
				WindowTreeNode windowTreeNode2 = new WindowTreeNode(windowFromHandler);
				List<IntPtr> childHandleList = Win32Extend.GetChildHandleList(hwnd, null, null);
				foreach (IntPtr intPtr in childHandleList)
				{
					WindowTreeNode treeNode = Win32Extend.GetTreeNode(intPtr);
					if (treeNode != null)
					{
						windowTreeNode2.ChildList.Add(treeNode);
					}
				}
				windowTreeNode = windowTreeNode2;
			}
			return windowTreeNode;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0004E328 File Offset: 0x0004C528
		private static IntPtr a(IntPtr A_0, Stack<string> A_1)
		{
			IntPtr intPtr;
			if (A_1.Count == 0)
			{
				intPtr = A_0;
			}
			else
			{
				string text = A_1.Pop();
				List<IntPtr> childHandleList = Win32Extend.GetChildHandleList(A_0, text, null);
				if (childHandleList.Count == 0)
				{
					A_1.Push(text);
					intPtr = IntPtr.Zero;
				}
				else
				{
					foreach (IntPtr intPtr2 in childHandleList)
					{
						IntPtr intPtr3 = Win32Extend.a(intPtr2, A_1);
						if (intPtr3 != IntPtr.Zero)
						{
							return intPtr3;
						}
					}
					A_1.Push(text);
					intPtr = IntPtr.Zero;
				}
			}
			return intPtr;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0004E3DC File Offset: 0x0004C5DC
		public static IntPtr GetChildHandleByClassTreePath(IntPtr hwndParent, params string[] treePath)
		{
			Stack<string> stack = new Stack<string>();
			for (int i = treePath.Length - 1; i >= 0; i--)
			{
				stack.Push(treePath[i]);
			}
			return Win32Extend.a(hwndParent, stack);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0004E418 File Offset: 0x0004C618
		private static IntPtr a(IntPtr A_0, Stack<string> A_1, ref List<IntPtr> A_2)
		{
			IntPtr intPtr;
			if (A_1.Count == 0)
			{
				A_2.Add(A_0);
				intPtr = A_0;
			}
			else
			{
				string text = A_1.Pop();
				List<IntPtr> childHandleList = Win32Extend.GetChildHandleList(A_0, text, null);
				if (childHandleList.Count == 0)
				{
					A_1.Push(text);
					intPtr = IntPtr.Zero;
				}
				else
				{
					foreach (IntPtr intPtr2 in childHandleList)
					{
						IntPtr intPtr3 = Win32Extend.a(intPtr2, A_1, ref A_2);
						if (!(intPtr3 == IntPtr.Zero))
						{
						}
					}
					A_1.Push(text);
					intPtr = IntPtr.Zero;
				}
			}
			return intPtr;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0004E4CC File Offset: 0x0004C6CC
		public static List<IntPtr> GetChildHandlesByClassTreePath(IntPtr hwndParent, params string[] treePath)
		{
			List<IntPtr> list = new List<IntPtr>();
			Stack<string> stack = new Stack<string>();
			for (int i = treePath.Length - 1; i >= 0; i--)
			{
				stack.Push(treePath[i]);
			}
			Win32Extend.a(hwndParent, stack, ref list);
			return list;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0004E514 File Offset: 0x0004C714
		private static IntPtr a(IntPtr A_0, Stack<string> A_1, ref Stack<IntPtr> A_2)
		{
			IntPtr intPtr;
			if (A_1.Count == 0)
			{
				intPtr = A_0;
			}
			else
			{
				string text = A_1.Pop();
				A_2.Push(A_0);
				List<IntPtr> childHandleList = Win32Extend.GetChildHandleList(A_0, text, null);
				if (childHandleList.Count == 0)
				{
					A_2.Pop();
					A_1.Push(text);
					intPtr = IntPtr.Zero;
				}
				else
				{
					foreach (IntPtr intPtr2 in childHandleList)
					{
						IntPtr intPtr3 = Win32Extend.a(intPtr2, A_1, ref A_2);
						if (intPtr3 != IntPtr.Zero)
						{
							return intPtr3;
						}
					}
					A_2.Pop();
					A_1.Push(text);
					intPtr = IntPtr.Zero;
				}
			}
			return intPtr;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0004E5E0 File Offset: 0x0004C7E0
		public static List<IntPtr> GetChildHandleListByClassTreePath(IntPtr hwndParent, params string[] treePath)
		{
			List<IntPtr> list = new List<IntPtr>();
			Stack<IntPtr> stack = new Stack<IntPtr>();
			Stack<string> stack2 = new Stack<string>();
			for (int i = treePath.Length - 1; i >= 0; i--)
			{
				stack2.Push(treePath[i]);
			}
			IntPtr intPtr = Win32Extend.a(hwndParent, stack2, ref stack);
			for (int j = stack.Count - 1; j >= 0; j--)
			{
				list.Insert(0, stack.Pop());
			}
			list.Add(intPtr);
			return list;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0004E664 File Offset: 0x0004C864
		public static List<WindowInfo> GetChildWindowListByClassTreePath(IntPtr hwndParent, params string[] treePath)
		{
			List<WindowInfo> list = new List<WindowInfo>();
			List<IntPtr> childHandleListByClassTreePath = Win32Extend.GetChildHandleListByClassTreePath(hwndParent, treePath);
			if (childHandleListByClassTreePath != null && childHandleListByClassTreePath.Count > 0)
			{
				for (int i = 0; i < childHandleListByClassTreePath.Count; i++)
				{
					WindowInfo windowFromHandler = WindowInfo.GetWindowFromHandler(childHandleListByClassTreePath[i]);
					if (windowFromHandler != null)
					{
						list.Add(windowFromHandler);
					}
				}
			}
			return list;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0004E6C8 File Offset: 0x0004C8C8
		public static List<int> GetPort(IntPtr hwnd)
		{
			Win32Extend.a a = new Win32Extend.a();
			List<int> list = new List<int>();
			WindowsAPI.GetWindowThreadProcessId(hwnd, out a.a);
			List<int> list2;
			if (a.a == 0)
			{
				list2 = list;
			}
			else
			{
				List<MIB_TCPROW_OWNER_PID> allTCPConnections_agiso = Win32Extend.GetAllTCPConnections_agiso();
				list2 = allTCPConnections_agiso.Where(new Func<MIB_TCPROW_OWNER_PID, bool>(a.b)).Select(new Func<MIB_TCPROW_OWNER_PID, int>(Win32Extend.<>c.<>9.a)).Distinct<int>()
					.ToList<int>();
			}
			return list2;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0004E748 File Offset: 0x0004C948
		private static IntPtr a()
		{
			IntPtr intPtr = WindowsAPI.FindWindow("Shell_TrayWnd", null);
			intPtr = WindowsAPI.FindWindowEx(intPtr, IntPtr.Zero, "TrayNotifyWnd", null);
			intPtr = WindowsAPI.FindWindowEx(intPtr, IntPtr.Zero, "SysPager", null);
			return WindowsAPI.FindWindowEx(intPtr, IntPtr.Zero, "ToolbarWindow32", null);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0004E79C File Offset: 0x0004C99C
		public static void RefreshWinNotifyArea()
		{
			IntPtr intPtr = Win32Extend.a();
			Rectangle rectangle;
			WindowsAPI.GetClientRect(intPtr, out rectangle);
			for (int i = 0; i < rectangle.Right; i += 2)
			{
				for (int j = 0; j < rectangle.Bottom; j += 2)
				{
					IntPtr intPtr2 = (IntPtr)((j << 16) | (i & 65535));
					WindowsAPI.SendMessage(intPtr, 512, 0, intPtr2);
				}
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0004E800 File Offset: 0x0004CA00
		public static List<int> GetProcListWhileMemoryTooLargeByName(string processName, int overMBs = 120)
		{
			List<int> list = new List<int>();
			try
			{
				List<KeyValuePair<int, float>> memory = Win32Extend.GetMemory(processName);
				foreach (KeyValuePair<int, float> keyValuePair in memory)
				{
					int key = keyValuePair.Key;
					float value = keyValuePair.Value;
					if (value > (float)(overMBs * 1024))
					{
						list.Add(key);
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0004E894 File Offset: 0x0004CA94
		public static void KillProcess(Process pro)
		{
			if (pro != null)
			{
				try
				{
					pro.CloseMainWindow();
				}
				catch
				{
				}
				try
				{
					pro.Kill();
				}
				catch
				{
				}
				try
				{
					pro.Close();
				}
				catch
				{
				}
				try
				{
					pro.Dispose();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0004E90C File Offset: 0x0004CB0C
		public static void KillProcessById(int processId, string onlyKillWhileNameEquals = null)
		{
			try
			{
				Process processById = Process.GetProcessById(processId);
				if (processById != null)
				{
					using (processById)
					{
						if (string.IsNullOrEmpty(onlyKillWhileNameEquals) || onlyKillWhileNameEquals.Equals(processById.ProcessName, StringComparison.OrdinalIgnoreCase))
						{
							Win32Extend.KillProcess(processById);
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0004E978 File Offset: 0x0004CB78
		public static bool KillProcessByName(string processName)
		{
			bool flag = false;
			try
			{
				Process[] processesByName = Process.GetProcessesByName(processName);
				foreach (Process process in processesByName)
				{
					using (process)
					{
						Win32Extend.KillProcess(process);
						flag = true;
					}
				}
			}
			catch
			{
			}
			return flag;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0004E9E0 File Offset: 0x0004CBE0
		public static void KillProcessAndChildrenById(int pid, int deep, string onlyKillWhileNameEquals)
		{
			try
			{
				if (deep-- >= 0)
				{
					using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT ProcessID FROM Win32_Process WHERE ParentProcessID=" + pid.ToString()))
					{
						using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
						{
							foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
							{
								ManagementObject managementObject = (ManagementObject)managementBaseObject;
								Win32Extend.KillProcessAndChildrenById(Util.ToInt(managementObject["ProcessID"]), deep, onlyKillWhileNameEquals);
							}
						}
					}
					Win32Extend.KillProcessById(pid, onlyKillWhileNameEquals);
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
				Win32Extend.KillProcessById(pid, onlyKillWhileNameEquals);
			}
			finally
			{
				deep++;
			}
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0004EAE4 File Offset: 0x0004CCE4
		public static int GetParentProcessId(int childProcessId)
		{
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId=" + childProcessId.ToString()))
				{
					using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
					{
						using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								ManagementObject managementObject = (ManagementObject)enumerator.Current;
								return Util.ToInt(managementObject["ParentProcessId"]);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
			}
			try
			{
				using (Process processById = Process.GetProcessById(childProcessId))
				{
					ParentProcessUtilities parentProcessUtilities = default(ParentProcessUtilities);
					int num2;
					int num = WindowsAPI.NtQueryInformationProcess(processById.Handle, 0, ref parentProcessUtilities, Marshal.SizeOf<ParentProcessUtilities>(parentProcessUtilities), out num2);
					if (num == 0)
					{
						return parentProcessUtilities.f.ToInt32();
					}
				}
			}
			catch (Exception ex2)
			{
				LogWriter.WriteLog(ex2.ToString(), 1);
			}
			return 0;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0004EC34 File Offset: 0x0004CE34
		public static List<int> GetChildProcessIds(int parentProcessId)
		{
			List<int> list = new List<int>();
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT ProcessId FROM Win32_Process WHERE  ParentProcessId =" + parentProcessId.ToString()))
				{
					using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
					{
						foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							list.Add(Util.ToInt(managementObject["ProcessId"]));
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
			}
			return list;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0004ED10 File Offset: 0x0004CF10
		public static List<KeyValuePair<int, float>> GetMemory(string processName)
		{
			List<KeyValuePair<int, float>> list = new List<KeyValuePair<int, float>>();
			Process[] processesByName = Process.GetProcessesByName(processName);
			for (int i = 0; i < processesByName.Length; i++)
			{
				try
				{
					PerformanceCounter performanceCounter = ((i > 0) ? new PerformanceCounter("Process", "ID Process", processesByName[i].ProcessName + "#" + i.ToString()) : new PerformanceCounter("Process", "ID Process", processesByName[i].ProcessName));
					PerformanceCounter performanceCounter2 = ((i > 0) ? new PerformanceCounter("Process", "Working Set - Private", processesByName[i].ProcessName + "#" + i.ToString()) : new PerformanceCounter("Process", "Working Set - Private", processesByName[i].ProcessName));
					list.Add(new KeyValuePair<int, float>((int)performanceCounter.NextValue(), performanceCounter2.NextValue() / 1024f));
				}
				catch
				{
				}
			}
			return list;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0004EE14 File Offset: 0x0004D014
		public static Dictionary<int, float> GetCpuUse(params string[] processNames)
		{
			Dictionary<int, float> dictionary = new Dictionary<int, float>();
			Dictionary<int, float> dictionary2;
			if (processNames == null)
			{
				dictionary2 = dictionary;
			}
			else
			{
				try
				{
					Dictionary<int, PerformanceCounter> dictionary3 = new Dictionary<int, PerformanceCounter>();
					foreach (string text in processNames)
					{
						Process[] processesByName = Process.GetProcessesByName(text);
						if (processesByName.Length != 0)
						{
							for (int j = 0; j < processesByName.Length; j++)
							{
								PerformanceCounter performanceCounter = ((j > 0) ? new PerformanceCounter("Process", "% Processor Time", processesByName[j].ProcessName + "#" + j.ToString()) : new PerformanceCounter("Process", "% Processor Time", processesByName[j].ProcessName));
								try
								{
									performanceCounter.NextValue();
									dictionary3.Add(processesByName[j].Id, performanceCounter);
									goto IL_00D1;
								}
								catch
								{
									goto IL_00D1;
								}
								break;
								IL_00D1:;
							}
						}
					}
					Thread.Sleep(1000);
					foreach (KeyValuePair<int, PerformanceCounter> keyValuePair in dictionary3)
					{
						try
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value.NextValue() / (float)Environment.ProcessorCount);
						}
						catch
						{
						}
					}
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(ex.ToString(), 1);
				}
				dictionary2 = dictionary;
			}
			return dictionary2;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0004EFD4 File Offset: 0x0004D1D4
		public static List<MIB_TCPROW_OWNER_PID> GetAllTCPConnections_agiso()
		{
			return Win32Extend.a<MIB_TCPROW_OWNER_PID>(TCP_IP_VERSION.IP_v4.GetHashCode());
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0004EFF8 File Offset: 0x0004D1F8
		private static List<a> a<a>(int A_0)
		{
			List<a> list = new List<a>();
			int num = 0;
			uint num2 = WindowsAPI.GetExtendedTcpTable(IntPtr.Zero, ref num, true, A_0, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_LISTENER, 0U);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			try
			{
				num2 = WindowsAPI.GetExtendedTcpTable(intPtr, ref num, true, A_0, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_LISTENER, 0U);
				if (num2 > 0U)
				{
					return list;
				}
				MIB_TCPTABLE_OWNER_PID mib_TCPTABLE_OWNER_PID = (MIB_TCPTABLE_OWNER_PID)Marshal.PtrToStructure(intPtr, typeof(MIB_TCPTABLE_OWNER_PID));
				int num3 = Marshal.SizeOf(typeof(a));
				uint dwNumEntries = mib_TCPTABLE_OWNER_PID.dwNumEntries;
				IntPtr intPtr2 = (IntPtr)((long)intPtr + 4L);
				int num4 = 0;
				while ((long)num4 < (long)((ulong)dwNumEntries))
				{
					a a = (a)((object)Marshal.PtrToStructure(intPtr2, typeof(a)));
					list.Add(a);
					intPtr2 = (IntPtr)((long)intPtr2 + (long)num3);
					num4++;
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return list;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0004F0F0 File Offset: 0x0004D2F0
		public static List<MIB_TCPROW_OWNER_PID> smethod_0()
		{
			return Win32Extend.a<MIB_TCPROW_OWNER_PID, MIB_TCPTABLE_OWNER_PID>(TCP_IP_VERSION.IP_v4.GetHashCode());
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0004F114 File Offset: 0x0004D314
		private static List<a> a<a, b>(int A_0)
		{
			List<a> list = new List<a>();
			int num = 0;
			FieldInfo field = typeof(b).GetField("dwNumEntries");
			uint num2 = WindowsAPI.GetExtendedTcpTable(IntPtr.Zero, ref num, true, A_0, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0U);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			try
			{
				num2 = WindowsAPI.GetExtendedTcpTable(intPtr, ref num, true, A_0, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0U);
				if (num2 > 0U)
				{
					return list;
				}
				b b = (b)((object)Marshal.PtrToStructure(intPtr, typeof(b)));
				int num3 = Marshal.SizeOf(typeof(a));
				uint num4 = (uint)field.GetValue(b);
				IntPtr intPtr2 = (IntPtr)((long)intPtr + 4L);
				int num5 = 0;
				while ((long)num5 < (long)((ulong)num4))
				{
					a a = (a)((object)Marshal.PtrToStructure(intPtr2, typeof(a)));
					list.Add(a);
					intPtr2 = (IntPtr)((long)intPtr2 + (long)num3);
					num5++;
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return list;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0004F230 File Offset: 0x0004D430
		public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
		{
			string text;
			if (!File.Exists(iniFilePath))
			{
				text = string.Empty;
			}
			else
			{
				try
				{
					StringBuilder stringBuilder = new StringBuilder(1024);
					WindowsAPI.GetPrivateProfileString(Section, Key, NoText, stringBuilder, 1024, iniFilePath);
					text = stringBuilder.ToString();
				}
				catch
				{
					text = string.Empty;
				}
			}
			return text;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0004F290 File Offset: 0x0004D490
		public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
		{
			bool flag;
			if (!File.Exists(iniFilePath))
			{
				flag = false;
			}
			else
			{
				try
				{
					flag = WindowsAPI.WritePrivateProfileString(Section, Key, Value, iniFilePath);
				}
				catch
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0004F2D0 File Offset: 0x0004D4D0
		public static bool CheckProcessIdIsRuing(int pid)
		{
			if (pid > 0)
			{
				try
				{
					Process processById = Process.GetProcessById(pid);
					processById.Dispose();
					return true;
				}
				catch (Exception)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0004F310 File Offset: 0x0004D510
		public static IntPtr GetOwnerWindow(IntPtr hWnd)
		{
			return WindowsAPI.GetWindow(hWnd, 4U);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0004F328 File Offset: 0x0004D528
		public static bool KillProcessByIdWithCmd(int pid)
		{
			string text = string.Format("taskkill /f /pid {0} /t", pid);
			return Util.UseCmd(text);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0004F350 File Offset: 0x0004D550
		public static bool KillProcessByNameWithCmd(string processName)
		{
			string text = "taskkill /f /im " + processName + ".exe /t";
			return Util.UseCmd(text);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0004F378 File Offset: 0x0004D578
		public static float GetSreenScale()
		{
			IntPtr dc = WindowsAPI.GetDC(WindowsAPI.GetDesktopWindow());
			int deviceCaps = WindowsAPI.GetDeviceCaps(dc, 118);
			WindowsAPI.ReleaseDC(IntPtr.Zero, dc);
			return (float)deviceCaps / (float)Screen.PrimaryScreen.Bounds.Width;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0004F3C4 File Offset: 0x0004D5C4
		public static OSName GetVersion()
		{
			OSVERSIONINFO osversioninfo = default(OSVERSIONINFO);
			osversioninfo.int_0 = Marshal.SizeOf<OSVERSIONINFO>(osversioninfo);
			if (WindowsAPI.GetVersionEx(ref osversioninfo))
			{
				if ((long)osversioninfo.dwPlatformId == 2L)
				{
					switch (osversioninfo.dwMajorVersion)
					{
					case 3:
						return OSName.WinNT3;
					case 4:
						return OSName.WinNT4;
					case 5:
						switch (osversioninfo.dwMinorVersion)
						{
						case 0:
							return OSName.Win2000;
						case 1:
							return OSName.WinXP;
						case 2:
							return OSName.Win2003;
						}
						break;
					case 6:
						switch (osversioninfo.dwMinorVersion)
						{
						case 0:
							return OSName.WinVista;
						case 1:
							return OSName.Win7;
						case 2:
							return OSName.Win8;
						case 3:
							return OSName.Win8_1;
						case 4:
							return OSName.Win10_Preview;
						}
						break;
					case 10:
						return OSName.Win10;
					}
				}
				else if ((long)osversioninfo.dwMajorVersion == 4L)
				{
					int dwMinorVersion = osversioninfo.dwMinorVersion;
					if ((long)dwMinorVersion == 0L)
					{
						return OSName.Win95;
					}
					if ((long)dwMinorVersion == 10L)
					{
						return OSName.Win98;
					}
					if ((long)dwMinorVersion == 90L)
					{
						return OSName.WinME;
					}
				}
			}
			return OSName.UNKNOWN;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0004F540 File Offset: 0x0004D740
		public static void CloseWindowListByClassAndName(string className, string windowName, int processId = 0, bool allowMatchBlur = false, int sleepTime = 200)
		{
			Win32Extend.b b = new Win32Extend.b();
			b.a = sleepTime;
			List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct(className, windowName), processId, allowMatchBlur);
			if (windowListByClassAndName != null)
			{
				windowListByClassAndName.ForEach(new Action<WindowInfo>(b.b));
			}
		}

		// Token: 0x040004E8 RID: 1256
		private static Process a = Process.GetCurrentProcess();

		// Token: 0x020000F5 RID: 245
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x060007B1 RID: 1969 RVA: 0x0000456F File Offset: 0x0000276F
			internal bool b(MIB_TCPROW_OWNER_PID A_0)
			{
				return (ulong)A_0.ProcessId == (ulong)((long)this.a) && A_0.localAddr == 16777343U;
			}

			// Token: 0x040004EC RID: 1260
			public int a;
		}

		// Token: 0x020000F6 RID: 246
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x060007B3 RID: 1971 RVA: 0x00004592 File Offset: 0x00002792
			internal void b(WindowInfo A_0)
			{
				A_0.Close(true);
				Thread.Sleep(this.a);
			}

			// Token: 0x040004ED RID: 1261
			public int a;
		}

		// Token: 0x020000F7 RID: 247
		[CompilerGenerated]
		private sealed class c
		{
			// Token: 0x060007B5 RID: 1973 RVA: 0x0004F580 File Offset: 0x0004D780
			internal bool b(IntPtr A_0, int A_1)
			{
				WindowInfo windowFromHandler = WindowInfo.GetWindowFromHandler(A_0);
				if (windowFromHandler != null)
				{
					this.a.Add(windowFromHandler);
				}
				return true;
			}

			// Token: 0x040004EE RID: 1262
			public List<WindowInfo> a;
		}
	}
}
