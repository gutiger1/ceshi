using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Agiso.Handler;
using Agiso.Utils;

namespace Agiso.Windows
{
	// Token: 0x020006AC RID: 1708
	public class WindowInfo
	{
		// Token: 0x0600211E RID: 8478 RVA: 0x00058308 File Offset: 0x00056508
		public IntPtr GetParent()
		{
			return WindowsAPI.GetParent(this.HWnd);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x00058324 File Offset: 0x00056524
		public WindowInfo GetParentWin()
		{
			IntPtr parent = this.GetParent();
			return WindowInfo.GetWindowFromHandler(parent);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x00058340 File Offset: 0x00056540
		public WindowInfo FindParentWin(string className = null, string captionName = null, bool isAllowBlur = false)
		{
			WindowInfo parentWin = this.GetParentWin();
			WindowInfo windowInfo;
			if (parentWin == null)
			{
				windowInfo = null;
			}
			else if (Win32Extend.ComparisonStringIsMatch(className, parentWin.Info.ClassName, new CompareWindowOption
			{
				IsAllowBlur = isAllowBlur
			}) && Win32Extend.ComparisonStringIsMatch(captionName, parentWin.Info.WindowName, new CompareWindowOption
			{
				IsAllowBlur = isAllowBlur
			}))
			{
				windowInfo = parentWin;
			}
			else
			{
				windowInfo = parentWin.FindParentWin(className, captionName, isAllowBlur);
			}
			return windowInfo;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000583B0 File Offset: 0x000565B0
		public WindowInfo GetNextWindow(string className = null, string captionName = null)
		{
			IntPtr parent = this.GetParent();
			IntPtr intPtr = WindowsAPI.FindWindowEx(parent, this.HWnd, className, captionName);
			return WindowInfo.GetWindowFromHandler(intPtr);
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000583DC File Offset: 0x000565DC
		public List<WindowInfo> GetBrotherWindows(string className = null, string captionName = null, bool isAllowBlur = false)
		{
			WindowInfo parentWin = this.GetParentWin();
			List<WindowInfo> list;
			if (parentWin == null)
			{
				list = null;
			}
			else
			{
				List<WindowInfo> list2 = new List<WindowInfo>();
				List<WindowInfo> list3 = this.EnumChildWindowList();
				foreach (WindowInfo windowInfo in list3)
				{
					if (!(windowInfo.HWnd == this.HWnd) && (Win32Extend.ComparisonStringIsMatch(className, windowInfo.Info.ClassName, new CompareWindowOption
					{
						IsAllowBlur = isAllowBlur
					}) && Win32Extend.ComparisonStringIsMatch(captionName, windowInfo.Info.WindowName, new CompareWindowOption
					{
						IsAllowBlur = isAllowBlur
					})))
					{
						list2.Add(windowInfo);
					}
				}
				list = list2;
			}
			return list;
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0000E915 File Offset: 0x0000CB15
		protected WindowInfo()
		{
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0000E925 File Offset: 0x0000CB25
		public WindowInfo(IntPtr hWndParam)
		{
			this.HWnd = hWndParam;
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x0000E93C File Offset: 0x0000CB3C
		// (set) Token: 0x06002126 RID: 8486 RVA: 0x0000E944 File Offset: 0x0000CB44
		public IntPtr HWnd { get; set; }

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x000584AC File Offset: 0x000566AC
		public WindowStruct Info
		{
			get
			{
				if (this.b == null)
				{
					this.b = Win32Extend.GetWindowInfoFromHandler(this.HWnd);
					if (this.b == null)
					{
						this.b = new WindowStruct();
					}
				}
				return this.b;
			}
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000584F4 File Offset: 0x000566F4
		public void Click(int x, int y, bool doByPost = true)
		{
			uint num = (uint)(x + y * 65536);
			if (doByPost)
			{
				WindowsAPI.PostMessage(this.HWnd, 513U, 1U, num);
				WindowsAPI.PostMessage(this.HWnd, 514U, 0U, num);
			}
			else
			{
				WindowsAPI.SendMessage(this.HWnd, 513, 1, num);
				WindowsAPI.SendMessage(this.HWnd, 514, 0, num);
			}
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0005855C File Offset: 0x0005675C
		public void Click(bool doByPost = true)
		{
			if (doByPost)
			{
				WindowsAPI.PostMessage(this.HWnd, 513U, 0U, 0U);
				WindowsAPI.PostMessage(this.HWnd, 514U, 0U, 0U);
				Thread.Sleep(100);
			}
			else
			{
				WindowsAPI.SendMessage(this.HWnd, 513, 0, 0);
				WindowsAPI.SendMessage(this.HWnd, 514, 0, 0);
			}
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x000585C4 File Offset: 0x000567C4
		public void Close(bool doByPost = true)
		{
			if (doByPost)
			{
				WindowsAPI.PostMessage(this.HWnd, 16U, 0U, 0U);
				Thread.Sleep(100);
			}
			else
			{
				WindowsAPI.SendMessage(this.HWnd, 16, 0, 0);
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00058600 File Offset: 0x00056800
		public void KeyUpCtrl(bool doByPost = false)
		{
			if (doByPost)
			{
				WindowsAPI.PostMessage(this.HWnd, 257U, 131072U, 0U);
				WindowsAPI.PostMessage(this.HWnd, 257U, 17U, 0U);
			}
			else
			{
				WindowsAPI.SendMessage(this.HWnd, 257, 131072, 0);
				WindowsAPI.SendMessage(this.HWnd, 257, 17, 0);
			}
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00058668 File Offset: 0x00056868
		public void KeyPress(Keys key, bool doByPost = true)
		{
			if (doByPost)
			{
				WindowsAPI.PostMessage(this.HWnd, 256U, (uint)key, 0U);
				WindowsAPI.PostMessage(this.HWnd, 258U, (uint)key, 0U);
				WindowsAPI.PostMessage(this.HWnd, 257U, (uint)key, 0U);
				Thread.Sleep(200);
			}
			else
			{
				WindowsAPI.SendMessage(this.HWnd, 256U, (uint)key, 0U);
				WindowsAPI.SendMessage(this.HWnd, 258U, (uint)key, 0U);
				WindowsAPI.SendMessage(this.HWnd, 257U, (uint)key, 0U);
			}
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x0000E94D File Offset: 0x0000CB4D
		public void KeyPressEnter(bool doByPost = false)
		{
			this.KeyPress(Keys.Return, doByPost);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0000E958 File Offset: 0x0000CB58
		public void KeyPressEscape(bool doByPost = false)
		{
			this.KeyPress(Keys.Escape, doByPost);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x000586F8 File Offset: 0x000568F8
		public void KeyPressDown(bool doByPost = false)
		{
			if (doByPost)
			{
				WindowsAPI.PostMessage(this.HWnd, 256U, 40U, 22020097U);
				WindowsAPI.PostMessage(this.HWnd, 257U, 40U, 3243245569U);
				WindowsAPI.PostMessage(this.HWnd, 260U, 18U, 540540929U);
			}
			else
			{
				WindowsAPI.SendMessage(this.HWnd, 256, 40, 22020097);
				WindowsAPI.SendMessage(this.HWnd, 257, 40, 3243245569U);
				WindowsAPI.SendMessage(this.HWnd, 260, 18, 540540929);
			}
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x0005879C File Offset: 0x0005699C
		public bool DoSendKeys(string key, bool doByWait = true)
		{
			this.SetForegroundWindow();
			bool flag;
			try
			{
				AppConfig.WriteLog("WindowsInfo.DoSendKeys Start", LogType.LogForTraceHold, 1);
				if (doByWait)
				{
					SendKeysExtend.SendWait(key);
				}
				else
				{
					SendKeysExtend.Send(key);
				}
				AppConfig.WriteLog("WindowsInfo.DoSendKeys End", LogType.LogForTraceHold, 1);
				flag = true;
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x00058804 File Offset: 0x00056A04
		public void PressKey(int key, int action)
		{
			switch (action)
			{
			case 1:
				WindowsAPI.keybd_event((byte)key, 0, 0, 0);
				return;
			case 2:
				WindowsAPI.keybd_event((byte)key, 0, 2, 0);
				return;
			}
			WindowsAPI.keybd_event((byte)key, 0, 0, 0);
			WindowsAPI.keybd_event((byte)key, 0, 2, 0);
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0000E963 File Offset: 0x0000CB63
		public void PressControlAndOtherKey(int key)
		{
			this.PressKey(17, 1);
			this.PressKey(key, 1);
			this.PressKey(key, 2);
			this.PressKey(17, 2);
			Thread.Sleep(200);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0000E991 File Offset: 0x0000CB91
		public void SetFocus()
		{
			WindowsAPI.SendMessage(this.HWnd, 7, 0, 0);
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0000E9A2 File Offset: 0x0000CBA2
		public void KillFocus()
		{
			WindowsAPI.SendMessage(this.HWnd, 8, 0, 0);
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x0000E9B3 File Offset: 0x0000CBB3
		public void ShowWindowAsync()
		{
			WindowsAPI.ShowWindowAsync(this.HWnd, 1);
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0000E9C2 File Offset: 0x0000CBC2
		public void SetForegroundWindow()
		{
			WindowsAPI.SetForegroundWindow(this.HWnd);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		public void SetMinimizeWindow()
		{
			WindowsAPI.SendMessage(this.HWnd, 274, 61472, 0);
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0000E9E9 File Offset: 0x0000CBE9
		public void SetMaximizeWindow()
		{
			WindowsAPI.SendMessage(this.HWnd, 274, 61488, 0);
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0000EA02 File Offset: 0x0000CC02
		public void SetRestoreWindow()
		{
			WindowsAPI.SendMessage(this.HWnd, 274, 61728, 0);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0000EA1B File Offset: 0x0000CC1B
		public void SetText(string msg)
		{
			WindowsAPI.SendMessage(this.HWnd, 12, 0, msg);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00058858 File Offset: 0x00056A58
		public bool ClearText()
		{
			for (int i = 0; i < 10; i++)
			{
				WindowsAPI.SendMessage(this.HWnd, 12, 0, "");
				Application.DoEvents();
				string text = Win32Extend.GetText(this.HWnd).Trim();
				if (string.IsNullOrEmpty(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000588B0 File Offset: 0x00056AB0
		public string GetText()
		{
			string text = Win32Extend.GetText(this.HWnd);
			if (text == null)
			{
				text = "";
			}
			return text;
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000588D8 File Offset: 0x00056AD8
		public int GetTextLength()
		{
			return WindowsAPI.SendMessage(this.HWnd, 14, 0, 0);
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x0000EA2D File Offset: 0x0000CC2D
		public void SetInputEnable()
		{
			WindowsAPI.EnableWindow(this.HWnd, true);
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x0000EA3C File Offset: 0x0000CC3C
		public void SetInputDisable()
		{
			WindowsAPI.EnableWindow(this.HWnd, false);
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x000588F8 File Offset: 0x00056AF8
		public WindowInfo GetChildWindow(string szClassNameParam, string szWindowNameParam, IntPtr brother, int idx)
		{
			IntPtr intPtr = WindowsAPI.FindWindowEx(this.HWnd, brother, szClassNameParam, szWindowNameParam);
			WindowInfo windowInfo;
			if (intPtr == IntPtr.Zero)
			{
				windowInfo = null;
			}
			else
			{
				WindowInfo windowInfo2 = new WindowInfo(intPtr);
				if (idx <= 0)
				{
					windowInfo = windowInfo2;
				}
				else
				{
					windowInfo = this.GetChildWindow(szClassNameParam, szWindowNameParam, windowInfo2.HWnd, --idx);
				}
			}
			return windowInfo;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x00058954 File Offset: 0x00056B54
		public List<WindowInfo> FindWindowsInDescendant(string szClassNameParam, string szWindowNameParam, bool isAllowBlur = false, bool? onlyVisit = false)
		{
			FindWindowOption findWindowOption = new FindWindowOption
			{
				IsOnlyFirst = true,
				ClassNameComparisonType = new CompareWindowOption
				{
					ComparisonType = StringComparison.OrdinalIgnoreCase,
					IsAllowBlur = isAllowBlur
				},
				WindowNameComparisonType = new CompareWindowOption
				{
					ComparisonType = StringComparison.OrdinalIgnoreCase,
					IsAllowBlur = isAllowBlur
				}
			};
			return this.FindWindowsInDescendant(szClassNameParam, szWindowNameParam, findWindowOption, onlyVisit);
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000589B0 File Offset: 0x00056BB0
		public List<WindowInfo> FindWindowsInDescendant(string szClassNameParam, string szWindowNameParam, FindWindowOption option, bool? onlyVisit = false)
		{
			return Win32Extend.FindWindowsInDescendant(this.HWnd, option, szClassNameParam, szWindowNameParam, onlyVisit);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000589D0 File Offset: 0x00056BD0
		public WindowInfo FindWindowInDescendant(string szClassNameParam, string szWindowNameParam, FindWindowOption option, bool? onlyVisit = false)
		{
			if (option == null)
			{
				option = new FindWindowOption
				{
					IsOnlyFirst = true
				};
			}
			else
			{
				option.IsOnlyFirst = true;
			}
			List<WindowInfo> list = this.FindWindowsInDescendant(szClassNameParam, szWindowNameParam, option, onlyVisit);
			WindowInfo windowInfo;
			if (list != null && list.Count > 0)
			{
				windowInfo = list[0];
			}
			else
			{
				windowInfo = null;
			}
			return windowInfo;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x00058A24 File Offset: 0x00056C24
		public WindowInfo FindWindowInDescendant(string szClassNameParam, string szWindowNameParam, bool isAllowBlur = false, bool? onlyVisit = false)
		{
			FindWindowOption findWindowOption = new FindWindowOption
			{
				IsOnlyFirst = true,
				ClassNameComparisonType = new CompareWindowOption
				{
					ComparisonType = StringComparison.OrdinalIgnoreCase,
					IsAllowBlur = isAllowBlur
				},
				WindowNameComparisonType = new CompareWindowOption
				{
					ComparisonType = StringComparison.OrdinalIgnoreCase,
					IsAllowBlur = isAllowBlur
				}
			};
			return this.FindWindowInDescendant(szClassNameParam, szWindowNameParam, findWindowOption, onlyVisit);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x00058A80 File Offset: 0x00056C80
		public WindowTreeNode GetTreeNode()
		{
			return Win32Extend.GetTreeNode(this.HWnd);
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x00058A9C File Offset: 0x00056C9C
		public List<WindowInfo> EnumChildWindowList()
		{
			List<WindowInfo> list = new List<WindowInfo>();
			List<IntPtr> childHandleList = Win32Extend.GetChildHandleList(this.HWnd, null, null);
			if (childHandleList != null && childHandleList.Count > 0)
			{
				foreach (IntPtr intPtr in childHandleList)
				{
					WindowInfo windowFromHandler = WindowInfo.GetWindowFromHandler(intPtr);
					if (windowFromHandler != null)
					{
						list.Add(windowFromHandler);
					}
				}
			}
			return list;
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x00058B28 File Offset: 0x00056D28
		public int ProcessId
		{
			get
			{
				if (this.d == 0)
				{
					WindowsAPI.GetWindowThreadProcessId(this.HWnd, out this.d);
				}
				return this.d;
			}
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x00058B5C File Offset: 0x00056D5C
		public string GetModuleFileNameEx()
		{
			IntPtr intPtr = WindowsAPI.OpenProcess(2035711, false, this.ProcessId);
			StringBuilder stringBuilder = new StringBuilder(500);
			WindowsAPI.GetModuleFileNameEx(intPtr, IntPtr.Zero, stringBuilder, stringBuilder.Capacity);
			WindowsAPI.CloseHandle(intPtr);
			return stringBuilder.ToString();
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x00058BA8 File Offset: 0x00056DA8
		public string GetModuleFileName()
		{
			IntPtr intPtr = WindowsAPI.OpenProcess(2035711, false, this.ProcessId);
			StringBuilder stringBuilder = new StringBuilder(500);
			WindowsAPI.GetModuleFileName(intPtr, stringBuilder, stringBuilder.Capacity);
			WindowsAPI.CloseHandle(intPtr);
			return stringBuilder.ToString();
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x00058BF0 File Offset: 0x00056DF0
		public string GetProcessImageFileName()
		{
			IntPtr intPtr = WindowsAPI.OpenProcess(2035711, false, this.ProcessId);
			StringBuilder stringBuilder = new StringBuilder(500);
			WindowsAPI.GetProcessImageFileName(intPtr, stringBuilder, stringBuilder.Capacity);
			WindowsAPI.CloseHandle(intPtr);
			string text = stringBuilder.ToString();
			return this.DosPathToNtPath(text);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x00058C40 File Offset: 0x00056E40
		public string DosPathToNtPath(string pszDosPath)
		{
			StringBuilder stringBuilder = new StringBuilder(500);
			WindowsAPI.GetLogicalDriveStrings(stringBuilder.Capacity, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x00058C70 File Offset: 0x00056E70
		public bool Visible
		{
			get
			{
				return (WindowsAPI.GetWindowLong(this.HWnd, -16) & 268435456) > 0;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x00058C94 File Offset: 0x00056E94
		public bool Enabled
		{
			get
			{
				return !this.Disabled;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x00058CAC File Offset: 0x00056EAC
		public bool Disabled
		{
			get
			{
				return (WindowsAPI.GetWindowLong(this.HWnd, -16) & 134217728) > 0;
			}
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0000EA4B File Offset: 0x0000CC4B
		public void Show()
		{
			WindowsAPI.ShowWindow(this.HWnd, 5);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0000EA5A File Offset: 0x0000CC5A
		public void Hide()
		{
			WindowsAPI.ShowWindow(this.HWnd, 0);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x00058CD0 File Offset: 0x00056ED0
		public IntPtr GetOwnerWindow()
		{
			return Win32Extend.GetOwnerWindow(this.HWnd);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x00058CEC File Offset: 0x00056EEC
		public void CloseOwnedWindow()
		{
			List<WindowInfo> allDesktopWindows = Win32Extend.GetAllDesktopWindows();
			foreach (WindowInfo windowInfo in allDesktopWindows)
			{
				if (windowInfo.ProcessId == this.ProcessId && windowInfo.Visible && Win32Extend.GetOwnerWindow(windowInfo.HWnd) == this.HWnd)
				{
					Bitmap bitmapFromDC = windowInfo.GetBitmapFromDC(0);
					Util.CollectPicMd5(bitmapFromDC, "弹窗_");
					windowInfo.Close(true);
				}
			}
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x00058D88 File Offset: 0x00056F88
		public virtual void KillProcess()
		{
			if (this.ProcessId != 0)
			{
				Win32Extend.KillProcessById(this.ProcessId, null);
			}
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00058DAC File Offset: 0x00056FAC
		public void KillProcessAndChildren(string subProcessNameOnlyKill)
		{
			if (string.IsNullOrEmpty(subProcessNameOnlyKill))
			{
				throw new Exception("有出现很多次自动退出程序，不知道是不是杀了 AliwwClient 自己的线程了。因此暂时禁止 null 的传入。");
			}
			if (this.ProcessId != 0)
			{
				Win32Extend.KillProcessAndChildrenById(this.ProcessId, 3, subProcessNameOnlyKill);
				Win32Extend.KillProcessById(this.ProcessId, null);
			}
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00058DF4 File Offset: 0x00056FF4
		public Rectangle GetClientRect()
		{
			Rectangle rectangle;
			WindowsAPI.GetClientRect(this.HWnd, out rectangle);
			return rectangle;
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00058E14 File Offset: 0x00057014
		public Bitmap GetBitmapFromDC(int offset = 0)
		{
			int num;
			int num2;
			return this.GetBitmapFromDC(out num, out num2, offset);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00058E30 File Offset: 0x00057030
		public Bitmap GetBitmapFromDC(out int width, out int height, int offset = 0)
		{
			Rectangle rectangle;
			WindowsAPI.GetClientRect(this.HWnd, out rectangle);
			width = rectangle.Right - rectangle.Left;
			height = rectangle.Bottom - rectangle.Top;
			Bitmap bitmap;
			if (height <= 0 || width <= 0 || offset < 0)
			{
				bitmap = null;
			}
			else
			{
				IntPtr intPtr = IntPtr.Zero;
				IntPtr intPtr2 = IntPtr.Zero;
				IntPtr intPtr3 = IntPtr.Zero;
				IntPtr intPtr4 = IntPtr.Zero;
				try
				{
					intPtr = WindowsAPI.GetDC(this.HWnd);
					intPtr2 = WindowsAPI.CreateCompatibleDC(intPtr);
					intPtr3 = WindowsAPI.CreateCompatibleBitmap(intPtr, width, height);
					intPtr4 = WindowsAPI.SelectObject(intPtr2, intPtr3);
					WindowsAPI.BitBlt(intPtr2, 0, 0, width, height, intPtr, 0, 0, 13369376);
					intPtr3 = WindowsAPI.SelectObject(intPtr2, intPtr4);
					if (offset == 0 || offset * 2 >= width || offset * 2 >= height)
					{
						bitmap = Image.FromHbitmap(intPtr3);
					}
					else
					{
						Bitmap bitmap2 = Image.FromHbitmap(intPtr3);
						int num = width - offset * 2;
						int num2 = height - offset * 2;
						Bitmap bitmap3 = new Bitmap(num, num2);
						Graphics graphics = Graphics.FromImage(bitmap3);
						graphics.DrawImage(bitmap2, new Rectangle(0, 0, num, num2), new Rectangle(offset, offset, num, num2), GraphicsUnit.Pixel);
						bitmap2.Dispose();
						graphics.Dispose();
						bitmap = bitmap3;
					}
				}
				finally
				{
					WindowsAPI.DeleteObject(intPtr3);
					WindowsAPI.DeleteObject(intPtr4);
					WindowsAPI.DeleteDC(intPtr2);
					WindowsAPI.ReleaseDC(this.HWnd, intPtr);
				}
			}
			return bitmap;
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x00058FA0 File Offset: 0x000571A0
		private Bitmap a(out int A_0, out int A_1)
		{
			Rectangle rectangle;
			WindowsAPI.GetClientRect(this.HWnd, out rectangle);
			A_0 = rectangle.Right - rectangle.Left;
			A_1 = rectangle.Bottom - rectangle.Top;
			IntPtr windowDC = WindowsAPI.GetWindowDC(this.HWnd);
			IntPtr intPtr = WindowsAPI.CreateCompatibleDC(windowDC);
			try
			{
				IntPtr intPtr2 = WindowsAPI.CreateCompatibleBitmap(windowDC, A_0, A_1);
				WindowsAPI.SelectObject(intPtr, intPtr2);
				WindowsAPI.PrintWindow(this.HWnd, intPtr, 0U);
				return Image.FromHbitmap(intPtr2);
			}
			catch
			{
				A_0 = 0;
				A_1 = 0;
			}
			finally
			{
				WindowsAPI.DeleteDC(intPtr);
				WindowsAPI.ReleaseDC(this.HWnd, windowDC);
			}
			return null;
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x00059058 File Offset: 0x00057258
		private Bitmap c()
		{
			Bitmap bitmap = new Bitmap(100, 50);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				IntPtr hdc = graphics.GetHdc();
				WindowsAPI.SendMessage(this.HWnd, 15, (int)hdc, IntPtr.Zero);
				graphics.ReleaseHdc(hdc);
			}
			return bitmap;
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000590BC File Offset: 0x000572BC
		public Bitmap CaptureWindow(Rectangle rect)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			Bitmap bitmap = null;
			try
			{
				if (!this.Visible)
				{
					return null;
				}
				intPtr = WindowsAPI.GetWindowDC(this.HWnd);
				intPtr2 = WindowsAPI.CreateCompatibleDC(intPtr);
				intPtr3 = WindowsAPI.CreateCompatibleDC(intPtr);
				Rectangle rectangle = default(Rectangle);
				WindowsAPI.GetClientRect(this.HWnd, out rectangle);
				IntPtr intPtr4 = WindowsAPI.CreateCompatibleBitmap(intPtr, this.a(rectangle.Right - rectangle.Left), this.a(rectangle.Bottom - rectangle.Top));
				IntPtr intPtr5 = WindowsAPI.CreateCompatibleBitmap(intPtr, this.a(rect.Right - rect.Left), this.a(rect.Bottom - rect.Top));
				WindowsAPI.SelectObject(intPtr2, intPtr4);
				WindowsAPI.SelectObject(intPtr3, intPtr5);
				if (!WindowsAPI.PrintWindow(this.HWnd, intPtr2, 0U))
				{
					return null;
				}
				if (!WindowsAPI.BitBlt(intPtr3, 0, 0, this.a(rect.Right - rect.Left), this.a(rect.Bottom - rect.Top), intPtr2, this.a(rect.Left), this.a(rect.Top), 1087111200))
				{
					return null;
				}
				bitmap = Image.FromHbitmap(intPtr5);
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(string.Format("截图指定窗口区域发生错误，Visible：{0}，异常：{1}", this.Visible, ex), 1);
				if (ex is ExternalException && ex.Message.Contains("GDI+ 中发生一般性错误"))
				{
					throw (ExternalException)ex;
				}
				return null;
			}
			finally
			{
				WindowsAPI.DeleteDC(intPtr);
				if (intPtr2 != IntPtr.Zero)
				{
					WindowsAPI.DeleteDC(intPtr2);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					WindowsAPI.DeleteDC(intPtr3);
				}
			}
			return bitmap;
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x000592C8 File Offset: 0x000574C8
		private int a(int A_0)
		{
			if (this.e == null)
			{
				this.e = new float?(Win32Extend.GetSreenScale());
			}
			if (AppConfig.IsEnableLog(LogType.LogSendMsg))
			{
				LogWriter.WriteLog(string.Format("_sreenScale: {0}", this.e), 1);
			}
			return (int)(this.e * (float)A_0).Value;
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0005935C File Offset: 0x0005755C
		public static WindowInfo GetWindowFromHandler(IntPtr hWnd)
		{
			WindowInfo windowInfo;
			if (hWnd == IntPtr.Zero)
			{
				windowInfo = null;
			}
			else
			{
				windowInfo = new WindowInfo(hWnd);
			}
			return windowInfo;
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x00059384 File Offset: 0x00057584
		public Dictionary<IntPtr, int> DictCount
		{
			get
			{
				if (this.f == null)
				{
					this.f = new Dictionary<IntPtr, int>();
				}
				return this.f;
			}
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000593B0 File Offset: 0x000575B0
		public void SimulateMouseClick(int x, int y, bool needToDraw = false, int clickTimes = 1)
		{
			this.SetForegroundWindow();
			this.SetFocus();
			if (needToDraw)
			{
				IntPtr dc = WindowsAPI.GetDC(this.HWnd);
				Graphics graphics = Graphics.FromHdc(dc);
				graphics.FillEllipse(Brushes.Red, x, y, 10, 10);
			}
			Rectangle rectangle;
			WindowsAPI.GetWindowRect(this.HWnd, out rectangle);
			int num = (rectangle.Left + x) * 65535 / Screen.PrimaryScreen.Bounds.Width;
			int num2 = (rectangle.Top + y) * 65535 / Screen.PrimaryScreen.Bounds.Height;
			WindowsAPI.mouse_event(32769, num, num2, 0, 0);
			Thread.Sleep(100);
			WindowsAPI.mouse_event(32769, num, num2, 0, 0);
			Thread.Sleep(100);
			if (AppConfig.AllowAutoLogin)
			{
				List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName("#32770", "关闭 Windows", 0, true, true, false);
				if (!Util.IsEmptyList<WindowInfo>(windowListByClassAndName))
				{
					windowListByClassAndName.ForEach(new Action<WindowInfo>(WindowInfo.<>c.<>9.a));
					Thread.Sleep(1000);
				}
			}
			this.b();
			for (int i = 0; i < clickTimes; i++)
			{
				WindowsAPI.mouse_event(32770, num, num2, 0, 0);
				WindowsAPI.mouse_event(32772, num, num2, 0, 0);
				Thread.Sleep(100);
			}
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x00059508 File Offset: 0x00057708
		private void b()
		{
			IntPtr foregroundWindow = WindowsAPI.GetForegroundWindow();
			if (!this.HWnd.Equals(foregroundWindow))
			{
				if (foregroundWindow == IntPtr.Zero)
				{
					return;
				}
				WindowInfo windowInfo = new WindowInfo(foregroundWindow);
				if (windowInfo.Info.WindowName.EndsWith(" - 工作台") || windowInfo.Info.WindowName.EndsWith("-千牛工作台") || (windowInfo.Info.WindowName == "千牛登录" && windowInfo.Info.ClassName == "Qt5152QWindowIcon") || windowInfo.Info.WindowName == "图形验证码识别器")
				{
					return;
				}
				if (this.DictCount.ContainsKey(foregroundWindow))
				{
					Dictionary<IntPtr, int> dictCount = this.DictCount;
					IntPtr intPtr = foregroundWindow;
					int num = dictCount[intPtr];
					dictCount[intPtr] = num + 1;
				}
				else
				{
					this.DictCount[foregroundWindow] = 1;
				}
			}
			this.a();
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x00059608 File Offset: 0x00057808
		private void a()
		{
			List<KeyValuePair<IntPtr, int>> list = this.DictCount.ToList<KeyValuePair<IntPtr, int>>();
			foreach (KeyValuePair<IntPtr, int> keyValuePair in list)
			{
				if (keyValuePair.Value >= 3)
				{
					WindowInfo windowInfo = new WindowInfo(keyValuePair.Key);
					windowInfo.Close(true);
					this.DictCount.Remove(keyValuePair.Key);
				}
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002161 RID: 8545 RVA: 0x00059694 File Offset: 0x00057894
		public bool? Responding
		{
			get
			{
				bool? flag;
				try
				{
					using (Process processById = Process.GetProcessById(this.ProcessId))
					{
						flag = new bool?(processById.Responding);
					}
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(ex.ToString(), 1);
					flag = null;
				}
				return flag;
			}
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x00059700 File Offset: 0x00057900
		public void Slide(int startX, int startY, int endX, int endY)
		{
			this.SetForegroundWindow();
			Thread.Sleep(100);
			Rectangle rectangle;
			WindowsAPI.GetWindowRect(this.HWnd, out rectangle);
			new Random();
			int num = (rectangle.Left + startX) * 65535 / Screen.PrimaryScreen.Bounds.Width;
			int num2 = (rectangle.Top + startY) * 65535 / Screen.PrimaryScreen.Bounds.Height;
			int num3 = (rectangle.Left + startX + (endX - startX) * 4 / 10) * 65535 / Screen.PrimaryScreen.Bounds.Width;
			int num4 = num2 + 1;
			int num5 = (rectangle.Left + startX + (endX - startX) * 7 / 10) * 65535 / Screen.PrimaryScreen.Bounds.Width;
			int num6 = num2 - 1;
			int num7 = (rectangle.Left + startX + (endX - startX) * 9 / 10) * 65535 / Screen.PrimaryScreen.Bounds.Width;
			int num8 = num2 + 1;
			int num9 = (rectangle.Left + endX) * 65535 / Screen.PrimaryScreen.Bounds.Width;
			int num10 = (rectangle.Top + endY) * 65535 / Screen.PrimaryScreen.Bounds.Height;
			IntPtr dc = WindowsAPI.GetDC(this.HWnd);
			if (!(dc == IntPtr.Zero))
			{
				Graphics graphics = Graphics.FromHdc(dc);
				graphics.FillEllipse(Brushes.Red, startX, startY, 10, 10);
				WindowsAPI.mouse_event(32769, num, num2, 0, 0);
				Thread.Sleep(100);
				WindowsAPI.mouse_event(32770, num, num2, 0, 0);
				Thread.Sleep(100);
				graphics.FillEllipse(Brushes.Red, num3, num4, 10, 10);
				WindowsAPI.mouse_event(32769, num3, num4, 0, 0);
				Thread.Sleep(300);
				graphics.FillEllipse(Brushes.Red, num5, num6, 10, 10);
				WindowsAPI.mouse_event(32769, num5, num6, 0, 0);
				Thread.Sleep(300);
				graphics.FillEllipse(Brushes.Red, num7, num8, 10, 10);
				WindowsAPI.mouse_event(32769, num7, num8, 0, 0);
				Thread.Sleep(500);
				graphics.FillEllipse(Brushes.Red, endX, endY, 10, 10);
				WindowsAPI.mouse_event(32769, num9, num10, 0, 0);
				Thread.Sleep(100);
				WindowsAPI.mouse_event(32772, num9, num10, 0, 0);
				WindowsAPI.ReleaseDC(this.HWnd, dc);
			}
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x00059994 File Offset: 0x00057B94
		public bool SetTextByClipboard(int x, int y, string text, bool simulateMouse = false)
		{
			bool flag;
			try
			{
				this.SetRestoreWindow();
				this.SetForegroundWindow();
				if (simulateMouse)
				{
					this.SimulateMouseClick(x, y, false, 1);
				}
				else
				{
					this.Click(x, y, true);
				}
				Thread.Sleep(100);
				if (this.HWnd != WindowsAPI.GetForegroundWindow())
				{
					flag = false;
				}
				else
				{
					AppConfig.WriteLog("SetTextByClipboard：setText,Begin", LogType.LogForPasswordOrAccountIsNull, 1);
					if (!ClipboardProxy.SetText(text, TextDataFormat.UnicodeText, 10))
					{
						flag = false;
					}
					else
					{
						Thread.Sleep(100);
						try
						{
							string text2 = ClipboardProxy.GetText(10);
							AppConfig.WriteLog("SetTextByClipboard：getText," + Util.MixString(text2, 4, 4, "****"), LogType.LogForPasswordOrAccountIsNull, 1);
						}
						catch (Exception ex)
						{
							AppConfig.WriteLog(string.Format("SetTextByClipboard：getText出错了，{0}", ex), LogType.LogForPasswordOrAccountIsNull, 1);
						}
						AppConfig.WriteLog("SetTextByClipboard：setText,End  ^a,Begin", LogType.LogForPasswordOrAccountIsNull, 1);
						SendKeysExtend.SendWait("^a");
						AppConfig.WriteLog("SetTextByClipboard：^a,End  ^v,Begin", LogType.LogForPasswordOrAccountIsNull, 1);
						SendKeysExtend.SendWait("^v");
						AppConfig.WriteLog("SetTextByClipboard：^v,End", LogType.LogForPasswordOrAccountIsNull, 1);
						flag = true;
					}
				}
			}
			catch (Exception ex2)
			{
				LogWriter.WriteLog(ex2.ToString(), 1);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x00059AC8 File Offset: 0x00057CC8
		public void SetText(int x, int y, string text)
		{
			this.SetRestoreWindow();
			this.SetForegroundWindow();
			this.SimulateMouseClick(x, y, false, 1);
			Thread.Sleep(100);
			if (!(this.HWnd != WindowsAPI.GetForegroundWindow()))
			{
				AppConfig.WriteLog("SetText：^a,Begin", LogType.LogForPasswordOrAccountIsNull, 1);
				SendKeysExtend.SendWait("^a");
				AppConfig.WriteLog("SetText：^a,End  {DEL},Begin", LogType.LogForPasswordOrAccountIsNull, 1);
				SendKeysExtend.SendWait("{DEL}");
				AppConfig.WriteLog("SetText：{DEL},End  setText“" + Util.MixString(text, 4, 4, "****") + "”,Begin", LogType.LogForPasswordOrAccountIsNull, 1);
				SendKeysExtend.SendWait(text);
				AppConfig.WriteLog("setText：setText,End", LogType.LogForPasswordOrAccountIsNull, 1);
			}
		}

		// Token: 0x0400128D RID: 4749
		[CompilerGenerated]
		private IntPtr a;

		// Token: 0x0400128E RID: 4750
		private WindowStruct b;

		// Token: 0x0400128F RID: 4751
		private int d = 0;

		// Token: 0x04001290 RID: 4752
		private float? e;

		// Token: 0x04001291 RID: 4753
		private Dictionary<IntPtr, int> f;
	}
}
