using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Agiso.Windows
{
	// Token: 0x020006B0 RID: 1712
	public static class WindowsAPI
	{
		// Token: 0x06002172 RID: 8562
		[DllImport("user32.dll")]
		public static extern uint SendMessage(IntPtr handle, uint message, uint wParam, uint lParam);

		// Token: 0x06002173 RID: 8563
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SendMessage(IntPtr handle, uint message, int WParam, IntPtr LParam);

		// Token: 0x06002174 RID: 8564
		[DllImport("user32.dll ", CharSet = CharSet.Unicode)]
		public static extern int SendMessage(int hWnd, int msg, IntPtr wParam, IntPtr lparam);

		// Token: 0x06002175 RID: 8565
		[DllImport("user32.dll ", CharSet = CharSet.Unicode)]
		public static extern int SendMessage(int hWnd, int msg, IntPtr wParam, TVITEM lparam);

		// Token: 0x06002176 RID: 8566
		[DllImport("user32.dll ")]
		public static extern int GetWindowText(int hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x06002177 RID: 8567
		[DllImport("user32.dll ")]
		public static extern int GetClassNameA(int hwnd, StringBuilder lpClassName, int nMaxCount);

		// Token: 0x06002178 RID: 8568
		[DllImport("kernel32 ", CharSet = CharSet.Unicode)]
		public static extern int CopyMemory(StringBuilder Destination, IntPtr Source, int Length);

		// Token: 0x06002179 RID: 8569
		[DllImport("kernel32 ", CharSet = CharSet.Unicode)]
		public static extern int GlobalAlloc(int wFlags, int dwBytes);

		// Token: 0x0600217A RID: 8570
		[DllImport("kernel32 ", CharSet = CharSet.Unicode)]
		public static extern int GlobalFree(IntPtr hMem);

		// Token: 0x0600217B RID: 8571
		[DllImport("kernel32.dll ")]
		public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, out int lpBuffer, int nSize, out int lpNumberOfBytesRead);

		// Token: 0x0600217C RID: 8572
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int PostMessage(IntPtr handle, uint message, uint WParam, uint LParam);

		// Token: 0x0600217D RID: 8573
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWindowEnabled(IntPtr handle);

		// Token: 0x0600217E RID: 8574
		[DllImport("user32.dll")]
		public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

		// Token: 0x0600217F RID: 8575
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		public static extern void mouse_event(int flag, int x, int y, int cButtons, int dwExtraInfo);

		// Token: 0x06002180 RID: 8576
		[DllImport("user32.dll")]
		public static extern int GetWindowRect(IntPtr handle, out Rectangle rectangle);

		// Token: 0x06002181 RID: 8577
		[DllImport("user32.dll")]
		public static extern IntPtr GetTopWindow(IntPtr hWnd);

		// Token: 0x06002182 RID: 8578
		[DllImport("user32.dll")]
		public static extern IntPtr GetNextWindow(IntPtr hWnd, uint wCmd);

		// Token: 0x06002183 RID: 8579
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr VirtualAllocEx(IntPtr processHandle, IntPtr memoryAddress, int dwSize, int flAllocationType, int flProtect);

		// Token: 0x06002184 RID: 8580
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool VirtualFreeEx(IntPtr processHandle, IntPtr memoryAddress, int dwSize, int dwFreeType);

		// Token: 0x06002185 RID: 8581
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

		// Token: 0x06002186 RID: 8582
		[DllImport("OLEACC.DLL")]
		public static extern int ObjectFromLresult(uint lResult, ref Guid riid, int wParam, [MarshalAs(UnmanagedType.Interface)] [In] [Out] ref object ppvObject);

		// Token: 0x06002187 RID: 8583
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		public static extern int SetWindowsHookEx(int idHook, WindowsAPI.HookProc lpfn, IntPtr hInstance, int threadId);

		// Token: 0x06002188 RID: 8584
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		public static extern bool UnhookWindowsHookEx(int idHook);

		// Token: 0x06002189 RID: 8585
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

		// Token: 0x0600218A RID: 8586
		[DllImport("shell32.dll")]
		public static extern int ShellAbout(IntPtr hWnd, string caption, string text, IntPtr iconHandle);

		// Token: 0x0600218B RID: 8587
		[DllImport("user32.dll")]
		public static extern bool BlockInput(bool enable);

		// Token: 0x0600218C RID: 8588
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

		// Token: 0x0600218D RID: 8589
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, uint lParam);

		// Token: 0x0600218E RID: 8590
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, StringBuilder lParam);

		// Token: 0x0600218F RID: 8591
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

		// Token: 0x06002190 RID: 8592
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref int lParam);

		// Token: 0x06002191 RID: 8593
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool PostThreadMessage(int threadId, uint msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06002192 RID: 8594
		[DllImport("user32.dll")]
		public static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, int uType);

		// Token: 0x06002193 RID: 8595
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetShortPathName(string lpszLongPath, StringBuilder lpszShortPath, int cchBuffer);

		// Token: 0x06002194 RID: 8596
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetShortPathName(string lpszLongPath, string lpszShortPath, int cchBuffer);

		// Token: 0x06002195 RID: 8597
		[DllImport("winmm.dll", CharSet = CharSet.Auto)]
		public static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int cchReturn, IntPtr hwndCallback);

		// Token: 0x06002196 RID: 8598
		[DllImport("gdi32.dll")]
		public static extern bool BitBlt(IntPtr handle, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);

		// Token: 0x06002197 RID: 8599
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

		// Token: 0x06002198 RID: 8600
		[DllImport("kernel32")]
		public static extern void GetSystemInfo(ref SYSTEM_INFO systemInfo);

		// Token: 0x06002199 RID: 8601
		[DllImport("kernel32")]
		public static extern void GlobalMemoryStatus(ref MEMORYSTATUS memoryInfo);

		// Token: 0x0600219A RID: 8602
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool OpenProcessToken(IntPtr ProcessHandle, int DesiredAccess, ref IntPtr TokenHandle);

		// Token: 0x0600219B RID: 8603
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, ref long pluid);

		// Token: 0x0600219C RID: 8604
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, bool DisableAllPrivileges, ref TokPriv1Luid NewState, int BufferLength, IntPtr PreviousState, IntPtr ReturnLength);

		// Token: 0x0600219D RID: 8605
		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool ExitWindowsEx(int uFlags, int dwReason);

		// Token: 0x0600219E RID: 8606
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int ChangeDisplaySettings([In] ref DEVMODE lpDevMode, int dwFlags);

		// Token: 0x0600219F RID: 8607
		[DllImport("kernel32.dll")]
		public static extern bool CloseHandle(IntPtr hObject);

		// Token: 0x060021A0 RID: 8608
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetForegroundWindow();

		// Token: 0x060021A1 RID: 8609
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetForegroundWindow(IntPtr handleIntPtr);

		// Token: 0x060021A2 RID: 8610
		[DllImport("user32.dll")]
		public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

		// Token: 0x060021A3 RID: 8611
		[DllImport("user32.dll")]
		public static extern bool EnumWindows(WindowsAPI.WNDENUMPROC lpEnumFunc, int lParam);

		// Token: 0x060021A4 RID: 8612
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr FindWindow(string className, string captionName);

		// Token: 0x060021A5 RID: 8613
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childHandle, string className, string captionName);

		// Token: 0x060021A6 RID: 8614
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr handle, StringBuilder captionName, int maxCount);

		// Token: 0x060021A7 RID: 8615
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetClassName(IntPtr handle, StringBuilder className, int maxCount);

		// Token: 0x060021A8 RID: 8616
		[DllImport("user32.dll")]
		public static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref COMBOBOXINFO info);

		// Token: 0x060021A9 RID: 8617
		[DllImport("Shlwapi.dll")]
		public static extern void SHAutoComplete(IntPtr hwndEdit, int dwFlags);

		// Token: 0x060021AA RID: 8618
		[DllImport("winmm.dll")]
		public static extern bool PlaySound(string pszSound, IntPtr hmod, int fdwSound);

		// Token: 0x060021AB RID: 8619
		[DllImport("user32.dll")]
		public static extern bool GetCaretPos(ref Point lpPoint);

		// Token: 0x060021AC RID: 8620
		[DllImport("user32.dll")]
		public static extern bool GetCursorPos(out Point lpPoint);

		// Token: 0x060021AD RID: 8621
		[DllImport("winmm.dll")]
		public static extern bool mciExecute(string pszCommand);

		// Token: 0x060021AE RID: 8622
		[DllImport("shell32.dll")]
		public static extern uint ExtractIconEx(string lpszFile, int nIconIndex, int[] phiconLarge, int[] phiconSmall, uint nIcons);

		// Token: 0x060021AF RID: 8623
		[DllImport("shell32.dll")]
		public static extern int ExtractIcon(int hInst, string lpszExeFileName, int nIconIndex);

		// Token: 0x060021B0 RID: 8624
		[DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetSystemMenu(IntPtr hWnd, int bRevert);

		// Token: 0x060021B1 RID: 8625
		[DllImport("kernel32.dll")]
		public static extern bool GetTempPath(int ccBuffer, StringBuilder lpszBuffer);

		// Token: 0x060021B2 RID: 8626
		[DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		public static extern bool InsertMenuW(IntPtr hMenu, uint uPosition, uint uFlags, uint uint_0, string lpNewItem);

		// Token: 0x060021B3 RID: 8627
		[DllImport("user32.dll")]
		public static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

		// Token: 0x060021B4 RID: 8628
		[DllImport("user32.dll")]
		public static extern bool RemoveMenu(IntPtr hMenu, uint nPosition, uint wFlags);

		// Token: 0x060021B5 RID: 8629
		[DllImport("kernel32.dll")]
		public static extern int GetWindowsDirectory(StringBuilder lpBuffer, int uSize);

		// Token: 0x060021B6 RID: 8630
		[DllImport("kernel32")]
		public static extern void GetSystemDirectory(StringBuilder lpBuffer, int uSize);

		// Token: 0x060021B7 RID: 8631
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr formHandle, int nCmdShow);

		// Token: 0x060021B8 RID: 8632
		[DllImport("user32.dll")]
		public static extern IntPtr GetDesktopWindow();

		// Token: 0x060021B9 RID: 8633
		[DllImport("kernel32.dll")]
		public static extern void Sleep(int dwMilliseconds);

		// Token: 0x060021BA RID: 8634
		[DllImport("user32.dll")]
		public static extern bool ClipCursor(ref Rectangle lpRect);

		// Token: 0x060021BB RID: 8635
		[DllImport("user32.dll")]
		public static extern int ShowCursor(bool bShow);

		// Token: 0x060021BC RID: 8636
		[DllImport("user32.dll")]
		public static extern bool DestroyCursor(IntPtr hCursor);

		// Token: 0x060021BD RID: 8637
		[DllImport("user32.dll")]
		public static extern IntPtr GetDlgItem(IntPtr hDlg, int int_0);

		// Token: 0x060021BE RID: 8638
		[DllImport("user32.dll")]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		// Token: 0x060021BF RID: 8639
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x060021C0 RID: 8640
		[DllImport("user32.dll")]
		public static extern IntPtr GetActiveWindow();

		// Token: 0x060021C1 RID: 8641
		[DllImport("kernel32.dll", ExactSpelling = true)]
		public static extern IntPtr GetCurrentProcess();

		// Token: 0x060021C2 RID: 8642
		[DllImport("kernel32.dll")]
		public static extern int GetCurrentProcessId();

		// Token: 0x060021C3 RID: 8643
		[DllImport("kernel32.dll")]
		public static extern bool GetExitCodeProcess(IntPtr hProcess, ref int lpExitCode);

		// Token: 0x060021C4 RID: 8644
		[DllImport("user32.dll")]
		public static extern IntPtr WindowFromPoint(Point Point);

		// Token: 0x060021C5 RID: 8645
		[DllImport("mpr.dll")]
		public static extern int WNetGetConnection(string lpLocalName, StringBuilder lpRemoteName, int lpnLength);

		// Token: 0x060021C6 RID: 8646
		[DllImport("user32.dll")]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		// Token: 0x060021C7 RID: 8647
		[DllImport("user32.dll")]
		public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		// Token: 0x060021C8 RID: 8648
		[DllImport("gdi32.dll")]
		public static extern bool GetWindowExtEx(IntPtr hdc, ref Size lpSize);

		// Token: 0x060021C9 RID: 8649
		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr hWnd);

		// Token: 0x060021CA RID: 8650
		[DllImport("user32.dll")]
		public static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

		// Token: 0x060021CB RID: 8651
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowDC(IntPtr handle);

		// Token: 0x060021CC RID: 8652
		[DllImport("user32.dll")]
		public static extern uint GetWindowModuleFileName(IntPtr hwnd, StringBuilder lpszFileName, uint cchFileNameMax);

		// Token: 0x060021CD RID: 8653
		[DllImport("user32.dll")]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		// Token: 0x060021CE RID: 8654
		[DllImport("kernel32.dll")]
		public static extern void GetSystemTime(ref SYSTEMTIME lpSystemTime);

		// Token: 0x060021CF RID: 8655
		[DllImport("kernel32.dll")]
		public static extern bool SetSystemTime(ref SYSTEMTIME lpSystemTime);

		// Token: 0x060021D0 RID: 8656
		[DllImport("gdi32.dll")]
		public static extern bool GetCharWidthFloatA(IntPtr hdc, uint iFirstChar, uint iLastChar, ref float pxBuffer);

		// Token: 0x060021D1 RID: 8657
		[DllImport("gdi32.dll")]
		public static extern bool GetCharWidth32A(IntPtr hdc, uint iFirstChar, uint iLastChar, ref int lpBuffer);

		// Token: 0x060021D2 RID: 8658
		[DllImport("user32.dll")]
		public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		// Token: 0x060021D3 RID: 8659
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenFile(string lpFileName, ref OFSTRUCT lpReOpenBuff, uint uStyle);

		// Token: 0x060021D4 RID: 8660
		[DllImport("kernel32.dll")]
		public static extern bool SetFileShortName(IntPtr hFile, string lpShortName);

		// Token: 0x060021D5 RID: 8661
		[DllImport("kernel32")]
		public static extern bool QueryPerformanceCounter(ref long PerformanceCount);

		// Token: 0x060021D6 RID: 8662
		[DllImport("kernel32.dll")]
		public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, uint dwFlagsAndAttributes, int hTemplateFile);

		// Token: 0x060021D7 RID: 8663
		[DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int SHFileOperation(_SHFILEOPSTRUCT str);

		// Token: 0x060021D8 RID: 8664
		[DllImport("user32.dll")]
		public static extern bool AdjustWindowRect(ref Rectangle lpRect, int dwStyle, bool bMenu);

		// Token: 0x060021D9 RID: 8665
		[DllImport("user32.dll")]
		public static extern bool AdjustWindowRectEx(Rectangle lpRect, int dwStyle, bool bMenu, int dwExStyle);

		// Token: 0x060021DA RID: 8666
		[DllImport("kernel32.dll")]
		public static extern int GetUserDefaultLangID();

		// Token: 0x060021DB RID: 8667
		[DllImport("kernel32.dll")]
		public static extern int GetUserDefaultLCID();

		// Token: 0x060021DC RID: 8668
		[DllImport("kernel32.dll")]
		public static extern int GetSystemDefaultLangID();

		// Token: 0x060021DD RID: 8669
		[DllImport("shell32.dll")]
		public static extern int FindExecutable(string lpFile, string lpDirectory, StringBuilder lpResult);

		// Token: 0x060021DE RID: 8670
		[DllImport("kernel32.dll")]
		public static extern int GetTickCount();

		// Token: 0x060021DF RID: 8671
		[DllImport("advapi32.dll")]
		public static extern bool AbortSystemShutdown(string lpMachineName);

		// Token: 0x060021E0 RID: 8672
		[DllImport("user32")]
		public static extern short GetKeyState(int nVirtKey);

		// Token: 0x060021E1 RID: 8673
		[DllImport("user32")]
		public static extern bool LockWindowUpdate(IntPtr hWndLock);

		// Token: 0x060021E2 RID: 8674
		[DllImport("kernel32.dll")]
		public static extern bool GetComputerName(StringBuilder lpBuffer, ref int lpnSize);

		// Token: 0x060021E3 RID: 8675
		[DllImport("shell32.dll")]
		public static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

		// Token: 0x060021E4 RID: 8676
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern int WaitForSingleObject(IntPtr hHandle, int dwMilliseconds);

		// Token: 0x060021E5 RID: 8677
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr FindFirstFile(string pFileName, ref WIN32_FIND_DATA pFindFileData);

		// Token: 0x060021E6 RID: 8678
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool FindNextFile(IntPtr hndFindFile, ref WIN32_FIND_DATA lpFindFileData);

		// Token: 0x060021E7 RID: 8679
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool FindClose(IntPtr hndFindFile);

		// Token: 0x060021E8 RID: 8680
		[DllImport("psapi.dll", CharSet = CharSet.Auto)]
		public static extern int GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpFilename, int nSize);

		// Token: 0x060021E9 RID: 8681
		[DllImport("kernel32.dll")]
		public static extern int GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);

		// Token: 0x060021EA RID: 8682
		[DllImport("kernel32.dll")]
		public static extern bool GetVersionEx(ref OSVERSIONINFO lpVersionInformation);

		// Token: 0x060021EB RID: 8683
		[DllImport("kernel32.dll")]
		public static extern bool GetVersionEx(ref OSVERSIONINFOEX lpVersionInformation);

		// Token: 0x060021EC RID: 8684
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetOpenFileName(OPENFILENAME lpofn);

		// Token: 0x060021ED RID: 8685
		[DllImport("kernel32.dll")]
		public static extern int GetStartupInfo(ref STARTUPINFO lpStartupInfo);

		// Token: 0x060021EE RID: 8686
		[DllImport("kernel32.dll")]
		public static extern int GetNumberOfConsoleMouseButtons(int lpNumberOfMouseButtons);

		// Token: 0x060021EF RID: 8687
		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string lpLibFileName);

		// Token: 0x060021F0 RID: 8688
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

		// Token: 0x060021F1 RID: 8689
		[DllImport("kernel32.dll")]
		public static extern bool FreeLibrary(IntPtr hLibModule);

		// Token: 0x060021F2 RID: 8690
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetModuleHandle(string name);

		// Token: 0x060021F3 RID: 8691
		[DllImport("kernel32.dll")]
		public static extern int GetFullPathName(string lpFileName, int nBufferLength, StringBuilder lpBuffer, string lpFilePart);

		// Token: 0x060021F4 RID: 8692
		[DllImport("user32.dll")]
		public static extern int GetMessagePos();

		// Token: 0x060021F5 RID: 8693 RVA: 0x00059CC8 File Offset: 0x00057EC8
		public static int GET_X_LPARAM(int lParam)
		{
			return lParam & 65535;
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00059CE0 File Offset: 0x00057EE0
		public static int GET_Y_LPARAM(int lParam)
		{
			return lParam >> 16;
		}

		// Token: 0x060021F7 RID: 8695
		[DllImport("user32.dll")]
		public static extern IntPtr GetParent(IntPtr hWnd);

		// Token: 0x060021F8 RID: 8696
		[DllImport("kernel32.dll")]
		public static extern bool GetProcessTimes(IntPtr hProcess, ref _FILETIME lpCreationTime, ref _FILETIME lpExitTime, ref _FILETIME lpKernelTime, ref _FILETIME lpUserTime);

		// Token: 0x060021F9 RID: 8697
		[DllImport("kernel32.dll")]
		public static extern int GetLastError();

		// Token: 0x060021FA RID: 8698
		[DllImport("kernel32.dll")]
		public static extern bool FileTimeToLocalFileTime([In] ref _FILETIME lpFileTime, out _FILETIME lpLocalTime);

		// Token: 0x060021FB RID: 8699
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int FileTimeToSystemTime(IntPtr lpFileTime, IntPtr lpSystemTime);

		// Token: 0x060021FC RID: 8700
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int FileTimeToSystemTime(ref _FILETIME lpFileTime, ref SYSTEMTIME lpSystemTime);

		// Token: 0x060021FD RID: 8701
		[DllImport("kernel32.dll")]
		public static extern bool LocalFileTimeToFileTime([In] ref _FILETIME lpLocalTime, out _FILETIME lpFileTime);

		// Token: 0x060021FE RID: 8702
		[DllImport("kernel32.dll")]
		public static extern bool SystemTimeToFileTime([In] ref SYSTEMTIME lpSystemTime, out _FILETIME lpFileTime);

		// Token: 0x060021FF RID: 8703
		[DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr FindFirstUrlCacheEntry([MarshalAs(UnmanagedType.LPTStr)] string lpszUrlSearchPattern, IntPtr lpFirstCacheEntryInfo, ref int lpdwFirstCacheEntryInfoBufferSize);

		// Token: 0x06002200 RID: 8704
		[DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool FindNextUrlCacheEntry(IntPtr hEnumHandle, IntPtr lpNextCacheEntryInfo, ref int lpdwNextCacheEntryInfoBufferSize);

		// Token: 0x06002201 RID: 8705
		[DllImport("wininet.dll")]
		public static extern bool FindCloseUrlCache(IntPtr hEnumHandle);

		// Token: 0x06002202 RID: 8706
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x06002203 RID: 8707
		[DllImport("kernel32.dll", ExactSpelling = true)]
		public static extern void RtlZeroMemory(IntPtr Destination, int Length);

		// Token: 0x06002204 RID: 8708
		[DllImport("user32.dll")]
		public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

		// Token: 0x06002205 RID: 8709
		[DllImport("user32.dll")]
		public static extern IntPtr LoadCursor(IntPtr hInstance, string lpCursorName);

		// Token: 0x06002206 RID: 8710
		[DllImport("user32.dll")]
		public static extern IntPtr LoadCursorFromFile(string lpFileName);

		// Token: 0x06002207 RID: 8711
		[DllImport("user32.dll")]
		public static extern IntPtr SetCursor(IntPtr hCursor);

		// Token: 0x06002208 RID: 8712
		[DllImport("user32.dll")]
		public static extern IntPtr CreateCursor(IntPtr hInst, int xHotSpot, int yHotSpot, int nWidth, int nHeight, ref int int_0, ref int int_1);

		// Token: 0x06002209 RID: 8713
		[DllImport("user32.dll")]
		public static extern IntPtr CopyIcon(IntPtr hIcon);

		// Token: 0x0600220A RID: 8714
		[DllImport("user32.dll")]
		public static extern bool GetClipCursor(ref Rectangle lpRect);

		// Token: 0x0600220B RID: 8715
		[DllImport("user32.dll")]
		public static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		// Token: 0x0600220C RID: 8716
		[DllImport("user32.dll")]
		public static extern bool ShowOwnedPopups(IntPtr hWnd, bool fShow);

		// Token: 0x0600220D RID: 8717
		[DllImport("IpHlpApi.dll")]
		public static extern int GetIfTable(byte[] pIfTable, ref uint pdwSize, bool bOrder);

		// Token: 0x0600220E RID: 8718
		[DllImport("IpHlpApi.dll")]
		public static extern int SendARP(int DestIP, int SrcIP, ref IntPtr pMacAddr, ref IntPtr PhyAddrLen);

		// Token: 0x0600220F RID: 8719
		[DllImport("Ws2_32.dll")]
		public static extern int inet_addr(string cp);

		// Token: 0x06002210 RID: 8720
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetSaveFileName(ref OPENFILENAME lpofn);

		// Token: 0x06002211 RID: 8721
		[DllImport("kernel32.dll")]
		public static extern uint WinExec(string lpCmdLine, uint uCmdShow);

		// Token: 0x06002212 RID: 8722
		[DllImport("shell32.dll")]
		public static extern bool Shell_NotifyIconA(int dwMessage, ref NOTIFYICONDATA lpData);

		// Token: 0x06002213 RID: 8723
		[DllImport("user32.dll")]
		public static extern int SetCursorPos(int x, int y);

		// Token: 0x06002214 RID: 8724
		[DllImport("user32.dll")]
		public static extern long SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

		// Token: 0x06002215 RID: 8725
		[DllImport("user32.dll")]
		public static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, int bAlpha, int dwFlags);

		// Token: 0x06002216 RID: 8726
		[DllImport("user32.dll")]
		public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		// Token: 0x06002217 RID: 8727
		[DllImport("shell32.dll")]
		public static extern int ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

		// Token: 0x06002218 RID: 8728
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(int fdwAccess, bool fInherit, int int_0);

		// Token: 0x06002219 RID: 8729
		[DllImport("user32.dll")]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		// Token: 0x0600221A RID: 8730
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		public static extern bool CreateProcess(StringBuilder lpApplicationName, StringBuilder lpCommandLine, SECURITY_ATTRIBUTES lpProcessAttributes, SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, int dwCreationFlags, StringBuilder lpEnvironment, StringBuilder lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, ref PROCESS_INFORMATION lpProcessInformation);

		// Token: 0x0600221B RID: 8731
		[DllImport("kernel32.dll")]
		public static extern bool TerminateProcess(IntPtr hProcess, int uExitCode);

		// Token: 0x0600221C RID: 8732
		[DllImport("kernel32.dll")]
		public static extern int GetProcessId(IntPtr Process);

		// Token: 0x0600221D RID: 8733
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern bool Module32First(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

		// Token: 0x0600221E RID: 8734
		[DllImport("kernel32.dll")]
		public static extern IntPtr CreateToolhelp32Snapshot(int dwFlags, int th32ProcessID);

		// Token: 0x0600221F RID: 8735
		[DllImport("psapi.dll")]
		public static extern bool EnumProcessModules(IntPtr hProcess, IntPtr[] lphModule, int cb, ref int lpcbNeeded);

		// Token: 0x06002220 RID: 8736
		[DllImport("user32.dll")]
		public static extern IntPtr SetCapture(IntPtr hWnd);

		// Token: 0x06002221 RID: 8737
		[DllImport("kernel32.dll")]
		public static extern bool SetComputerName(string lpComputerName);

		// Token: 0x06002222 RID: 8738
		[DllImport("user32.dll")]
		public static extern bool SetCaretPos(int X, int Y);

		// Token: 0x06002223 RID: 8739
		[DllImport("kernel32.dll")]
		public static extern bool SetEnvironmentVariable(string lpName, string lpValue);

		// Token: 0x06002224 RID: 8740
		[DllImport("user32.dll")]
		public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

		// Token: 0x06002225 RID: 8741
		[DllImport("user32.dll")]
		public static extern bool SetScrollRange(IntPtr hWnd, int nBar, int nMinPos, int nMaxPos, bool bRedraw);

		// Token: 0x06002226 RID: 8742
		[DllImport("user32.dll")]
		public static extern bool EnumChildWindows(IntPtr hWndParent, WindowsAPI.ChildWindowsProc lpEnumFunc, int lParam);

		// Token: 0x06002227 RID: 8743
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindow(IntPtr hWnd, uint wCmd);

		// Token: 0x06002228 RID: 8744
		[DllImport("user32.dll")]
		public static extern int SetScrollInfo(IntPtr hwnd, int fnBar, SCROLLINFO lpsi, bool fRedraw);

		// Token: 0x06002229 RID: 8745
		[DllImport("user32.dll")]
		public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

		// Token: 0x0600222A RID: 8746
		[DllImport("user32.dll")]
		public static extern bool EnumDisplayDevices(string lpDevice, int iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, int dwFlags);

		// Token: 0x0600222B RID: 8747
		[DllImport("user32")]
		public static extern int CallWindowProc(int lpPrevWndFunc, IntPtr hWnd, int Msg, int wParam, int lParam);

		// Token: 0x0600222C RID: 8748
		[DllImport("user32.dll")]
		public static extern IntPtr ChildWindowFromPoint(IntPtr hWndParent, Point Point);

		// Token: 0x0600222D RID: 8749
		[DllImport("gdi32.dll")]
		public static extern bool Rectangle(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

		// Token: 0x0600222E RID: 8750
		[DllImport("gdi32.dll")]
		public static extern int SetROP2(IntPtr hdc, int fnDrawMode);

		// Token: 0x0600222F RID: 8751
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreatePen(int fnPenStyle, int nWidth, int crColor);

		// Token: 0x06002230 RID: 8752
		[DllImport("gdi32.dll")]
		public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		// Token: 0x06002231 RID: 8753
		[DllImport("user32.dll")]
		public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

		// Token: 0x06002232 RID: 8754
		[DllImport("user32.dll")]
		public static extern bool BringWindowToTop(IntPtr hWnd);

		// Token: 0x06002233 RID: 8755
		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);

		// Token: 0x06002234 RID: 8756
		[DllImport("user32.dll")]
		public static extern IntPtr ReleaseDC(IntPtr hDC);

		// Token: 0x06002235 RID: 8757
		[DllImport("user32.dll")]
		public static extern IntPtr ReleaseDC(IntPtr handle, IntPtr hDC);

		// Token: 0x06002236 RID: 8758
		[DllImport("kernel32.dll")]
		public static extern string GetCommandLine();

		// Token: 0x06002237 RID: 8759
		[DllImport("user32.dll")]
		public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);

		// Token: 0x06002238 RID: 8760
		[DllImport("user32.dll")]
		public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		// Token: 0x06002239 RID: 8761
		[DllImport("shell32.dll")]
		public static extern int SHGetFileInfo(string pszPath, int dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

		// Token: 0x0600223A RID: 8762
		[DllImport("shell32.dll")]
		public static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, string lpIconPath, ref int lpiIcon);

		// Token: 0x0600223B RID: 8763
		[DllImport("shell32.dll")]
		public static extern IntPtr ExtractAssociatedIconEx(IntPtr hInst, string lpIconPath, ref int lpiIcon, ref int lpiIconId);

		// Token: 0x0600223C RID: 8764
		[DllImport("user32.dll")]
		public static extern int FillRect(IntPtr hDC, ref Rectangle lprc, IntPtr hbr);

		// Token: 0x0600223D RID: 8765
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateHatchBrush(int fnStyle, int clrref);

		// Token: 0x0600223E RID: 8766
		[DllImport("user32.dll")]
		public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

		// Token: 0x0600223F RID: 8767
		[DllImport("user32.dll")]
		public static extern bool FlashWindowEx(ref FLASHWINFO pfwi);

		// Token: 0x06002240 RID: 8768
		[DllImport("comdlg32.dll")]
		public static extern IntPtr FindText(ref FINDREPLACE lpfr);

		// Token: 0x06002241 RID: 8769
		[DllImport("comdlg32.dll")]
		public static extern IntPtr ReplaceText(ref FINDREPLACE lpfr);

		// Token: 0x06002242 RID: 8770
		[DllImport("comdlg32.dll")]
		public static extern bool ChooseColor(ref CHOOSECOLOR lpcc);

		// Token: 0x06002243 RID: 8771
		[DllImport("comdlg32.dll")]
		public static extern bool ChooseFont(ref CHOOSEFONT lpcf);

		// Token: 0x06002244 RID: 8772
		[DllImport("user32.dll")]
		public static extern bool CloseWindow(IntPtr hWnd);

		// Token: 0x06002245 RID: 8773
		[DllImport("user32.dll")]
		public static extern bool CloseDesktop(IntPtr hDesktop);

		// Token: 0x06002246 RID: 8774
		[DllImport("user32.dll")]
		public static extern int GetSystemMetrics(int nIndex);

		// Token: 0x06002247 RID: 8775
		[DllImport("user32.dll")]
		public static extern bool AppendMenu(IntPtr hMenu, uint uFlags, uint uint_0, string lpNewItem);

		// Token: 0x06002248 RID: 8776
		[DllImport("user32.dll")]
		public static extern bool AnyPopup();

		// Token: 0x06002249 RID: 8777
		[DllImport("user32.dll")]
		public static extern IntPtr CreateMenu();

		// Token: 0x0600224A RID: 8778
		[DllImport("user32.dll")]
		public static extern IntPtr CreatePopupMenu();

		// Token: 0x0600224B RID: 8779
		[DllImport("user32.dll")]
		public static extern bool DestroyMenu(IntPtr hMenu);

		// Token: 0x0600224C RID: 8780
		[DllImport("user32.dll")]
		public static extern IntPtr GetMenu(IntPtr handle);

		// Token: 0x0600224D RID: 8781
		[DllImport("user32.dll")]
		public static extern int GetMenuItemCount(IntPtr menuHandle);

		// Token: 0x0600224E RID: 8782
		[DllImport("user32.dll")]
		public static extern IntPtr GetSubMenu(IntPtr menuHandle, int index);

		// Token: 0x0600224F RID: 8783
		[DllImport("user32.dll")]
		public static extern uint GetMenuItemID(IntPtr subMenuhandle, int index);

		// Token: 0x06002250 RID: 8784
		[DllImport("user32.dll")]
		public static extern bool GetMenuItemInfo(IntPtr menuHandle, ref MENUINFO menuInfo);

		// Token: 0x06002251 RID: 8785
		[DllImport("user32.dll")]
		public static extern bool SetMenuInfo(IntPtr menuHandle, ref MENUINFO menuIfo);

		// Token: 0x06002252 RID: 8786
		[DllImport("gdi32")]
		public static extern IntPtr CreatePatternBrush(IntPtr hbmp);

		// Token: 0x06002253 RID: 8787
		[DllImport("user32.dll")]
		public static extern bool ModifyMenu(IntPtr hMnu, uint uPosition, uint uFlags, uint uint_0, string lpNewItem);

		// Token: 0x06002254 RID: 8788
		[DllImport("user32.dll")]
		public static extern bool IsCharAlpha(char ch);

		// Token: 0x06002255 RID: 8789
		[DllImport("user32.dll")]
		public static extern bool IsCharAlphaNumeric(string ch);

		// Token: 0x06002256 RID: 8790
		[DllImport("user32.dll")]
		public static extern bool IsCharLower(char ch);

		// Token: 0x06002257 RID: 8791
		[DllImport("user32.dll")]
		public static extern bool IsCharUpper(char ch);

		// Token: 0x06002258 RID: 8792
		[DllImport("user32.dll")]
		public static extern bool IsWindow(IntPtr hWnd);

		// Token: 0x06002259 RID: 8793
		[DllImport("user32.dll")]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		// Token: 0x0600225A RID: 8794
		[DllImport("user32.dll")]
		public static extern bool IsIconic(IntPtr hWnd);

		// Token: 0x0600225B RID: 8795
		[DllImport("user32.dll")]
		public static extern bool IsZoomed(IntPtr hWnd);

		// Token: 0x0600225C RID: 8796
		[DllImport("Shlwapi.dll")]
		public static extern bool PathIsContentType(string pszPath, string pszContentType);

		// Token: 0x0600225D RID: 8797
		[DllImport("Shlwapi.dll")]
		public static extern bool PathIsDirectory(string pszPath);

		// Token: 0x0600225E RID: 8798
		[DllImport("Shlwapi.dll")]
		public static extern bool PathIsDirectoryEmpty(string pszPath);

		// Token: 0x0600225F RID: 8799
		[DllImport("Shlwapi.dll")]
		public static extern bool PathIsFileSpec(string lpszPath);

		// Token: 0x06002260 RID: 8800
		[DllImport("Shlwapi.dll")]
		public static extern string PathGetArgs(string pszPath);

		// Token: 0x06002261 RID: 8801
		[DllImport("Shlwapi.dll")]
		public static extern bool PathIsPrefix(string pszPrefix, string pszPath);

		// Token: 0x06002262 RID: 8802
		[DllImport("Shlwapi.dll")]
		public static extern bool PathIsRelative(string lpszPath);

		// Token: 0x06002263 RID: 8803
		[DllImport("Shlwapi.dll")]
		public static extern bool PathIsRoot(string pPath);

		// Token: 0x06002264 RID: 8804
		[DllImport("Shlwapi.dll")]
		public static extern bool PathIsSameRoot(string pszPath1, string pszPath2);

		// Token: 0x06002265 RID: 8805
		[DllImport("Shlwapi.dll")]
		public static extern bool PathIsURL(string pszPath);

		// Token: 0x06002266 RID: 8806
		[DllImport("Shlwapi.dll")]
		public static extern bool PathMatchSpec(string pszFile, string pszSpec);

		// Token: 0x06002267 RID: 8807
		[DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
		public static extern string PathRemoveBackslash(StringBuilder lpszPath);

		// Token: 0x06002268 RID: 8808
		[DllImport("Shlwapi.dll")]
		public static extern void PathRemoveArgs(StringBuilder pszPath);

		// Token: 0x06002269 RID: 8809
		[DllImport("Shlwapi.dll")]
		public static extern void PathRemoveBlanks(StringBuilder lpszString);

		// Token: 0x0600226A RID: 8810
		[DllImport("Shlwapi.dll")]
		public static extern void PathRemoveExtension(StringBuilder pszPath);

		// Token: 0x0600226B RID: 8811
		[DllImport("Shlwapi.dll")]
		public static extern bool PathRenameExtension(StringBuilder pszPath, string pszExt);

		// Token: 0x0600226C RID: 8812
		[DllImport("gdi32.dll")]
		public static extern int SetTextCharacterExtra(IntPtr hdc, int nCharExtra);

		// Token: 0x0600226D RID: 8813
		[DllImport("gdi32.dll")]
		public static extern int GetTextCharacterExtra(IntPtr hdc);

		// Token: 0x0600226E RID: 8814
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr CreateFontIndirect(ref LOGFONT lplf);

		// Token: 0x0600226F RID: 8815
		[DllImport("user32.dll")]
		public static extern int RegisterClass(ref WNDCLASS Class);

		// Token: 0x06002270 RID: 8816
		[DllImport("user32.dll")]
		public static extern int DefWindowProc(IntPtr hWnd, uint Msg, int wParam, int lParam);

		// Token: 0x06002271 RID: 8817
		[DllImport("user32.dll")]
		public static extern uint RegisterWindowMessage(string lpString);

		// Token: 0x06002272 RID: 8818
		[DllImport("kernel32.dll")]
		public static extern bool GetThreadTimes(IntPtr hThread, ref _FILETIME lpCreationTime, ref _FILETIME lpExitTime, ref _FILETIME lpKernelTime, ref _FILETIME lpUserTime);

		// Token: 0x06002273 RID: 8819
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetCurrentThread();

		// Token: 0x06002274 RID: 8820
		[DllImport("kernel32.dll")]
		public static extern int GetCurrentThreadId();

		// Token: 0x06002275 RID: 8821
		[DllImport("kernel32.dll")]
		public static extern int GetThreadPriority(IntPtr hThread);

		// Token: 0x06002276 RID: 8822
		[DllImport("kernel32.dll")]
		public static extern bool SetThreadPriority(IntPtr hThread, int nPriority);

		// Token: 0x06002277 RID: 8823
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenThread(int dwDesiredAccess, bool bInheritHandle, int dwThreadId);

		// Token: 0x06002278 RID: 8824
		[DllImport("user32.dll")]
		public static extern IntPtr LoadIcon(IntPtr hInstance, string lpIconName);

		// Token: 0x06002279 RID: 8825
		[DllImport("user32.dll")]
		public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

		// Token: 0x0600227A RID: 8826
		[DllImport("uxtheme.dll")]
		public static extern IntPtr SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

		// Token: 0x0600227B RID: 8827
		[DllImport("user32.dll")]
		public static extern bool GetMenuItemInfo(IntPtr hMenu, uint uItem, bool fByPosition, ref MENUITEMINFO lpmii);

		// Token: 0x0600227C RID: 8828
		[DllImport("user32.dll")]
		public static extern bool SetMenuItemInfo(IntPtr hMenu, uint uItem, bool fByPosition, ref MENUITEMINFO lpmii);

		// Token: 0x0600227D RID: 8829
		[DllImport("user32.dll")]
		public static extern bool UpdateWindow(IntPtr hWnd);

		// Token: 0x0600227E RID: 8830
		[DllImport("uxtheme")]
		public static extern IntPtr GetWindowTheme(IntPtr hWnd);

		// Token: 0x0600227F RID: 8831
		[DllImport("kernel32.dll")]
		public static extern int GetFileAttributes(string lpFileName);

		// Token: 0x06002280 RID: 8832
		[DllImport("kernel32.dll")]
		public static extern bool SetLocalTime(ref SYSTEMTIME lpSystemTime);

		// Token: 0x06002281 RID: 8833
		[DllImport("advapi32.dll")]
		public static extern long RegCloseKey(IntPtr hKey);

		// Token: 0x06002282 RID: 8834
		[DllImport("advapi32.dll", CharSet = CharSet.Auto)]
		public static extern int RegCreateKeyEx(IntPtr hKey, string lpSubKey, int Reserved, string lpClass, int dwOptions, int samDesigner, SECURITY_ATTRIBUTES lpSecurityAttributes, out IntPtr hkResult, out int lpdwDisposition);

		// Token: 0x06002283 RID: 8835
		[DllImport("user32.dll")]
		public static extern bool ScreenToClient(IntPtr hWnd, Point lpPoint);

		// Token: 0x06002284 RID: 8836
		[DllImport("gdi32.dll")]
		public static extern int GetPixel(IntPtr hdc, int nXPos, int nYPos);

		// Token: 0x06002285 RID: 8837
		[DllImport("user32")]
		public static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

		// Token: 0x06002286 RID: 8838
		[DllImport("kernel32.dll")]
		public static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

		// Token: 0x06002287 RID: 8839
		[DllImport("kernel32.dll")]
		public static extern bool GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

		// Token: 0x06002288 RID: 8840
		[DllImport("kernel32.dll")]
		public static extern bool GetPrivateProfileSection(string lpAppName, StringBuilder lpReturnedString, int nSize, string lpFileName);

		// Token: 0x06002289 RID: 8841
		[DllImport("kernel32.dll")]
		public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

		// Token: 0x0600228A RID: 8842
		[DllImport("kernel32.dll")]
		public static extern bool WritePrivateProfileSection(string lpAppName, string lpString, string lpFileName);

		// Token: 0x0600228B RID: 8843
		[DllImport("kernel32.dll")]
		public static extern bool WritePrivateProfileStruct(string lpszSection, string lpszKey, StringBuilder lpStruct, uint uSizeStruct, string szFile);

		// Token: 0x0600228C RID: 8844
		[DllImport("user32.dll")]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fuWinIni);

		// Token: 0x0600228D RID: 8845
		[DllImport("gdi32.dll")]
		public static extern bool GetTextExtentPoint32(IntPtr hdc, string lpString, int c, ref Size lpSize);

		// Token: 0x0600228E RID: 8846
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

		// Token: 0x0600228F RID: 8847
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		// Token: 0x06002290 RID: 8848
		[DllImport("gdi32.dll")]
		public static extern bool DeleteDC(IntPtr hdc);

		// Token: 0x06002291 RID: 8849
		[DllImport("user32.dll")]
		public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

		// Token: 0x06002292 RID: 8850
		[DllImport("gdi32.dll")]
		public static extern bool GetWindowOrgEx(IntPtr hdc, ref Point lpPoint);

		// Token: 0x06002293 RID: 8851
		[DllImport("winmm.dll")]
		public static extern int waveOutSetVolume(IntPtr hwo, long dwVolume);

		// Token: 0x06002294 RID: 8852
		[DllImport("winmm.dll")]
		public static extern int waveOutGetVolume(IntPtr hwo, out long dwVolume);

		// Token: 0x06002295 RID: 8853
		[DllImport("user32.dll")]
		public static extern IntPtr CreateWindowEx(int dwExStyle, string lpClassName, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

		// Token: 0x06002296 RID: 8854
		[DllImport("user32.dll")]
		public static extern IntPtr ChildWindowFromPointEx(IntPtr hwndParent, Point pt, uint uFlags);

		// Token: 0x06002297 RID: 8855
		[DllImport("user32.dll")]
		public static extern bool OpenIcon(IntPtr hWnd);

		// Token: 0x06002298 RID: 8856
		[DllImport("user32.dll")]
		public static extern int CascadeWindows(IntPtr hwndParent, uint wHow, ref Rectangle lpRect, uint cKids, IntPtr lpKids);

		// Token: 0x06002299 RID: 8857
		[DllImport("user32.dll")]
		public static extern int TileWindows(IntPtr hwndParent, uint wHow, ref Rectangle lpRect, uint cKids, IntPtr lpKids);

		// Token: 0x0600229A RID: 8858
		[DllImport("user32.dll")]
		public static extern uint ArrangeIconicWindows(IntPtr hWnd);

		// Token: 0x0600229B RID: 8859
		[DllImport("gdi32.dll")]
		public static extern bool CancelDC(IntPtr hdc);

		// Token: 0x0600229C RID: 8860
		[DllImport("user32.dll")]
		public static extern bool CopyRect(string lprcDst, ref Rectangle lprcSrc);

		// Token: 0x0600229D RID: 8861
		[DllImport("user32.dll")]
		public static extern int CountClipboardFormats();

		// Token: 0x0600229E RID: 8862
		[DllImport("kernel32.dll")]
		public static extern IntPtr CreateThread(SECURITY_ATTRIBUTES lpsa, int cbStack, IntPtr lpStartAddr, int lpvThreadParam, int fdwCreate, int int_0);

		// Token: 0x0600229F RID: 8863
		[DllImport("kernel32.dll")]
		public static extern int ThreadProc(int lpParameter);

		// Token: 0x060022A0 RID: 8864
		[DllImport("user32.dll")]
		public static extern IntPtr BeginDeferWindowPos(int nNumWindows);

		// Token: 0x060022A1 RID: 8865
		[DllImport("user32.dll")]
		public static extern IntPtr DeferWindowPos(IntPtr hWinPosInfo, IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

		// Token: 0x060022A2 RID: 8866
		[DllImport("user32.dll")]
		public static extern bool EndDeferWindowPos(IntPtr hWinPosInfo);

		// Token: 0x060022A3 RID: 8867
		[DllImport("kernel32.dll")]
		public static extern bool Beep(int dwFreq, int dwDuration);

		// Token: 0x060022A4 RID: 8868
		[DllImport("kernel32.dll")]
		public static extern int GetFileSize(IntPtr hFile, ref int lpFileSizeHigh);

		// Token: 0x060022A5 RID: 8869
		[DllImport("kernel32.dll")]
		public static extern int GetFileType(IntPtr hFile);

		// Token: 0x060022A6 RID: 8870
		[DllImport("kernel32.dll")]
		public static extern bool GetFileInformationByHandle(IntPtr hFile, ref BY_HANDLE_FILE_INFORMATION lpFileInformation);

		// Token: 0x060022A7 RID: 8871
		[DllImport("user32.dll")]
		public static extern bool SetWindowText(IntPtr hWnd, string lpString);

		// Token: 0x060022A8 RID: 8872
		[DllImport("user32.dll")]
		public static extern bool IsWindowUnicode(IntPtr hWnd);

		// Token: 0x060022A9 RID: 8873
		[DllImport("user32.dll")]
		private static extern IntPtr SetActiveWindow(IntPtr A_0);

		// Token: 0x060022AA RID: 8874
		[DllImport("user32.dll")]
		public static extern IntPtr GetDCEx(IntPtr hWnd, Rectangle hrgnClip, int flags);

		// Token: 0x060022AB RID: 8875
		[DllImport("user32.dll")]
		public static extern int SaveDC(IntPtr hdc);

		// Token: 0x060022AC RID: 8876
		[DllImport("user32.dll")]
		public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

		// Token: 0x060022AD RID: 8877
		[DllImport("user32.dll")]
		public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, ref Point lpPoints, uint cPoints);

		// Token: 0x060022AE RID: 8878
		[DllImport("user32.dll")]
		public static extern IntPtr WindowFromDC(IntPtr hDC);

		// Token: 0x060022AF RID: 8879
		[DllImport("kernel32.dll")]
		public static extern int GetCompressedFileSize(string lpFileName, ref int lpFileSizeHigh);

		// Token: 0x060022B0 RID: 8880
		[DllImport("kernel32.dll")]
		public static extern bool LockFile(IntPtr hFile, int dwFileOffsetLow, int dwFileOffsetHigh, int nNumberOfBytesToLockLow, int nNumberOfBytesToLockHigh);

		// Token: 0x060022B1 RID: 8881
		[DllImport("kernel32.dll")]
		public static extern long CompareFileTime(_FILETIME lpFileTime1, _FILETIME lpFileTime2);

		// Token: 0x060022B2 RID: 8882
		[DllImport("kernel32.dll")]
		public static extern bool GetVolumeInformation(string lpRootPathName, string lpVolumeNameBuffer, int nVolumeNameSize, int lpVolumeSerialNumber, int lpMaximumComponentLength, int lpFileSystemFlags, string lpFileSystemNameBuffer, int nFileSystemNameSize);

		// Token: 0x060022B3 RID: 8883
		[DllImport("user32.dll")]
		public static extern bool IsRectEmpty(ref Rectangle lprc);

		// Token: 0x060022B4 RID: 8884
		[DllImport("advapi32.dll")]
		public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

		// Token: 0x060022B5 RID: 8885
		[DllImport("kernel32.dll")]
		public static extern bool VerifyVersionInfo(ref OSVERSIONINFOEX lpVersionInfo, int dwTypeMask, int dwlConditionMask);

		// Token: 0x060022B6 RID: 8886
		[DllImport("psapi.dll")]
		public static extern bool GetModuleInformation(IntPtr hProcess, IntPtr hModule, ref MODULEINFO lpmodinfo, int cb);

		// Token: 0x060022B7 RID: 8887
		[DllImport("psapi.dll")]
		public static extern bool EnumProcesses(int[] pProcessIds, int cb, ref int pBytesReturned);

		// Token: 0x060022B8 RID: 8888
		[DllImport("user32.dll")]
		public static extern bool EnumThreadWindows(int dwThreadId, WindowsAPI.ThreadWindowsProc lpfn, int lParam);

		// Token: 0x060022B9 RID: 8889
		[DllImport("wininet.dll")]
		public static extern bool InternetGetConnectedState(ref int lpdwFlags, int dwReserved);

		// Token: 0x060022BA RID: 8890
		[DllImport("Sensapi.dll")]
		public static extern bool IsNetworkAlive(ref int lpdwFlags);

		// Token: 0x060022BB RID: 8891
		[DllImport("user32.dll")]
		public static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

		// Token: 0x060022BC RID: 8892
		[DllImport("kernel32.dll")]
		public static extern int GetTimeZoneInformation(ref TIME_ZONE_INFORMATION lpTimeZoneInformation);

		// Token: 0x060022BD RID: 8893
		[DllImport("kernel32.dll")]
		public static extern int GetTimeFormat(int Locale, int dwFlags, ref SYSTEMTIME lpTime, string lpFormat, StringBuilder lpTimeStr, int cchTime);

		// Token: 0x060022BE RID: 8894
		[DllImport("user32.dll")]
		public static extern bool GetIconInfo(IntPtr hIcon, ref ICONINFO piconinfo);

		// Token: 0x060022BF RID: 8895
		[DllImport("kernel32.dll")]
		public static extern int GetDateFormat(int Locale, int dwFlags, ref SYSTEMTIME lpDate, string lpFormat, StringBuilder lpDateStr, int cchDate);

		// Token: 0x060022C0 RID: 8896
		[DllImport("kernel32.dll")]
		public static extern uint GetDriveType(string lpRootPathName);

		// Token: 0x060022C1 RID: 8897
		[DllImport("gdi32.dll")]
		public static extern bool BeginPath(IntPtr hdc);

		// Token: 0x060022C2 RID: 8898
		[DllImport("gdi32.dll")]
		public static extern int SetBkMode(IntPtr hdc, int iBkMode);

		// Token: 0x060022C3 RID: 8899
		[DllImport("gdi32.dll")]
		public static extern bool EndPath(IntPtr hdc);

		// Token: 0x060022C4 RID: 8900
		[DllImport("gdi32.dll")]
		public static extern IntPtr PathToRegion(IntPtr hdc);

		// Token: 0x060022C5 RID: 8901
		[DllImport("gdi32.dll")]
		public static extern bool Ellipse(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

		// Token: 0x060022C6 RID: 8902
		[DllImport("kernel32.dll", EntryPoint = "RtlZeroMemory")]
		public static extern bool RtlZeroMemory_1(IntPtr Destination, int Length);

		// Token: 0x060022C7 RID: 8903
		[DllImport("gdi32.dll")]
		public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int int_0, int int_1, int nWidthSrc, int nHeightSrc, int dwRop);

		// Token: 0x060022C8 RID: 8904
		[DllImport("gdi32.dll")]
		public static extern int GetStretchBltMode(IntPtr hdc);

		// Token: 0x060022C9 RID: 8905
		[DllImport("kernel32.dll")]
		public static extern bool GetBinaryType(string lpApplicationName, ref int lpBinaryType);

		// Token: 0x060022CA RID: 8906
		[DllImport("gdi32.dll")]
		public static extern bool RestoreDC(IntPtr hdc, int nSavedDC);

		// Token: 0x060022CB RID: 8907
		[DllImport("gdi32.dll")]
		public static extern bool GetDCOrgEx(IntPtr hdc, ref Point lpPoint);

		// Token: 0x060022CC RID: 8908
		[DllImport("gdi32.dll")]
		public static extern bool PatBlt(IntPtr hdc, int nXLeft, int nYLeft, int nWidth, int nHeight, int dwRop);

		// Token: 0x060022CD RID: 8909
		[DllImport("gdi32.dll")]
		public static extern bool PlgBlt(IntPtr hdcDest, ref Point lpPoint, IntPtr hdcSrc, int nXSrc, int nYSrc, int nWidth, int nHeight, IntPtr hbmMask, int xMask, int yMask);

		// Token: 0x060022CE RID: 8910
		[DllImport("gdi32.dll")]
		public static extern bool MaskBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, IntPtr hbmMask, int xMask, int yMask, int dwRop);

		// Token: 0x060022CF RID: 8911
		[DllImport("gdi32.dll")]
		public static extern bool TransparentBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int hHeightDest, IntPtr hdcSrc, int int_0, int int_1, int nWidthSrc, int nHeightSrc, uint crTransparent);

		// Token: 0x060022D0 RID: 8912
		[DllImport("gdi32.dll")]
		public static extern int SetPixel(IntPtr hdc, int X, int Y, int crColor);

		// Token: 0x060022D1 RID: 8913
		[DllImport("gdi32.dll")]
		public static extern bool SetPixelV(IntPtr hdc, int X, int Y, int crColor);

		// Token: 0x060022D2 RID: 8914
		[DllImport("gdi32.dll")]
		public static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);

		// Token: 0x060022D3 RID: 8915
		[DllImport("gdi32.dll")]
		public static extern bool LineDDA(int nXStart, int nYStart, int nXEnd, int nYEnd, IntPtr lpLineFunc, IntPtr lpData);

		// Token: 0x060022D4 RID: 8916
		[DllImport("gdi32.dll")]
		public static extern bool Polyline(IntPtr hdc, ref Point lppt, int cPoints);

		// Token: 0x060022D5 RID: 8917
		[DllImport("gdi32.dll")]
		public static extern bool PolylineTo(IntPtr hdc, ref Point lppt, int cCount);

		// Token: 0x060022D6 RID: 8918
		[DllImport("gdi32.dll")]
		public static extern bool PolyBezier(IntPtr hdc, ref Point lppt, int cPoints);

		// Token: 0x060022D7 RID: 8919
		[DllImport("gdi32.dll")]
		public static extern bool PolyDraw(IntPtr hdc, ref Point lppt, ref byte lpbTypes, int cCount);

		// Token: 0x060022D8 RID: 8920
		[DllImport("user32.dll")]
		public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

		// Token: 0x060022D9 RID: 8921
		[DllImport("user32.dll")]
		public static extern int RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

		// Token: 0x060022DA RID: 8922
		[DllImport("user32.dll")]
		public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

		// Token: 0x060022DB RID: 8923
		[DllImport("user32.dll")]
		public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

		// Token: 0x060022DC RID: 8924
		[DllImport("user32.dll")]
		public static extern bool OffsetRect(ref Rectangle lprc, int dx, int dy);

		// Token: 0x060022DD RID: 8925
		[DllImport("shell32.dll")]
		public static extern int SHFormatDrive(IntPtr hwnd, uint drive, uint fmtID, uint options);

		// Token: 0x060022DE RID: 8926
		[DllImport("gdi32.dll")]
		public static extern bool ScaleViewportExtEx(IntPtr hdc, int Xnum, int Xdenom, int Ynum, int Ydenom, ref Size lpSize);

		// Token: 0x060022DF RID: 8927
		[DllImport("gdi32.dll")]
		public static extern bool ScaleWindowExtEx(IntPtr hdc, int Xnum, int Xdenom, int Ynum, int Ydenom, ref Size lpSize);

		// Token: 0x060022E0 RID: 8928
		[DllImport("gdi32.dll")]
		public static extern bool GetCharWidth(IntPtr hdc, uint iFirstChar, uint iLastChar, ref int lpBuffer);

		// Token: 0x060022E1 RID: 8929
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

		// Token: 0x060022E2 RID: 8930
		[DllImport("user32.dll")]
		public static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

		// Token: 0x060022E3 RID: 8931
		[DllImport("user32.dll")]
		public static extern IntPtr LoadBitmap(IntPtr hInstance, string lpBitmapName);

		// Token: 0x060022E4 RID: 8932
		[DllImport("kernel32.dll")]
		public static extern void ExitProcess(uint uExitCode);

		// Token: 0x060022E5 RID: 8933
		[DllImport("shell32.dll")]
		public static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, int dwFlags);

		// Token: 0x060022E6 RID: 8934
		[DllImport("user32.dll")]
		public static extern bool InvalidateRect(int hWnd, ref Rectangle lpRect, bool bErase);

		// Token: 0x060022E7 RID: 8935
		[DllImport("wininet.dll")]
		public static extern bool InternetGetConnectedStateEx(ref int lpdwFlags, string lpszConnectionName, int dwNameLen, int dwReserved);

		// Token: 0x060022E8 RID: 8936
		[DllImport("user32.dll")]
		public static extern int DrawText(IntPtr hDC, string lpString, int nCount, ref Rectangle lpRect, uint uFormat);

		// Token: 0x060022E9 RID: 8937
		[DllImport("user32")]
		public static extern bool PaintDesktop(IntPtr hdc);

		// Token: 0x060022EA RID: 8938
		[DllImport("user32.dll")]
		public static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);

		// Token: 0x060022EB RID: 8939
		[DllImport("gdi32.dll")]
		public static extern int GetNearestColor(IntPtr hdc, int crColor);

		// Token: 0x060022EC RID: 8940
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateBitmap(int nWidth, int nHeight, uint cPlanes, uint cBitsPerPel, IntPtr lpvBits);

		// Token: 0x060022ED RID: 8941
		[DllImport("gdi32.dll")]
		public static extern bool TextOut(IntPtr hdc, int nXStart, int nYStart, string lpString, int cbString);

		// Token: 0x060022EE RID: 8942
		[DllImport("gdi32.dll")]
		public static extern bool ExtTextOut(IntPtr hdc, int X, int Y, uint fuOptions, ref Rectangle lprc, string lpString, uint cbCount, int[] lpDx);

		// Token: 0x060022EF RID: 8943
		[DllImport("gdi32.dll")]
		public static extern int DrawTextEx(IntPtr hdc, string lpchText, int cchText, Rectangle lprc, uint uint_0, DRAWTEXTPARAMS drawtextparams_0);

		// Token: 0x060022F0 RID: 8944
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessageTimeoutW(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, int flags, int timeout, out IntPtr pdwResult);

		// Token: 0x060022F1 RID: 8945
		[DllImport("user32.dll")]
		public static extern int GetClassLong(IntPtr hWnd, int index);

		// Token: 0x060022F2 RID: 8946
		[DllImport("user32.dll")]
		public static extern bool LockWorkStation();

		// Token: 0x060022F3 RID: 8947
		[DllImport("IpHlpApi.dll", SetLastError = true)]
		public static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, TCP_TABLE_CLASS tblClass, uint reserved = 0U);

		// Token: 0x060022F4 RID: 8948
		[DllImport("psapi.dll")]
		public static extern int GetProcessImageFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);

		// Token: 0x060022F5 RID: 8949
		[DllImport("psapi.dll")]
		public static extern int GetLogicalDriveStrings(int nSize, StringBuilder szDriveStr);

		// Token: 0x060022F6 RID: 8950
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern int VirtualAllocEx(IntPtr hProcess, int lpAddress, int dwSize, int flAllocationType, int flProtect);

		// Token: 0x060022F7 RID: 8951
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool VirtualFreeEx(IntPtr hProcess, int lpAddress, int dwSize, uint dwFreeType);

		// Token: 0x060022F8 RID: 8952
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		// Token: 0x060022F9 RID: 8953
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

		// Token: 0x060022FA RID: 8954
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern int QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, int ucchMax);

		// Token: 0x060022FB RID: 8955
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern int ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, IntPtr lpBuffer, int nSize, out int lpNumberOfBytesRead);

		// Token: 0x060022FC RID: 8956
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern int ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);

		// Token: 0x060022FD RID: 8957
		[DllImport("psapi.dll")]
		public static extern int EmptyWorkingSet(IntPtr hwProc);

		// Token: 0x060022FE RID: 8958
		[DllImport("gdi32.dll")]
		public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		// Token: 0x060022FF RID: 8959
		[DllImport("ntdll.dll")]
		public static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref ParentProcessUtilities processInformation, int processInformationLength, out int returnLength);

		// Token: 0x06002300 RID: 8960
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

		// Token: 0x020006B1 RID: 1713
		// (Invoke) Token: 0x06002302 RID: 8962
		public delegate int HookProc(int nCode, int wParam, IntPtr lParam);

		// Token: 0x020006B2 RID: 1714
		// (Invoke) Token: 0x06002306 RID: 8966
		public delegate bool WNDENUMPROC(IntPtr hWnd, int lParam);

		// Token: 0x020006B3 RID: 1715
		// (Invoke) Token: 0x0600230A RID: 8970
		public delegate bool ChildWindowsProc(IntPtr hwnd, int lParam);

		// Token: 0x020006B4 RID: 1716
		// (Invoke) Token: 0x0600230E RID: 8974
		public delegate bool ThreadWindowsProc(IntPtr hwnd, int lParam);
	}
}
