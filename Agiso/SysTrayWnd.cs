using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Agiso.Utils;
using Agiso.Windows;

namespace Agiso
{
	// Token: 0x020000D8 RID: 216
	public class SysTrayWnd
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x000472E4 File Offset: 0x000454E4
		public static IntPtr GetTrayWnd()
		{
			IntPtr intPtr = WindowsAPI.FindWindow("Shell_TrayWnd", null);
			if (intPtr != IntPtr.Zero)
			{
				intPtr = WindowsAPI.FindWindowEx(intPtr, IntPtr.Zero, "TrayNotifyWnd", null);
				if (intPtr != IntPtr.Zero)
				{
					intPtr = WindowsAPI.FindWindowEx(intPtr, IntPtr.Zero, "SysPager", null);
					if (intPtr != IntPtr.Zero)
					{
						intPtr = WindowsAPI.FindWindowEx(intPtr, IntPtr.Zero, "ToolbarWindow32", null);
					}
				}
			}
			return intPtr;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00047364 File Offset: 0x00045564
		public static IntPtr GetNotifyIconOverflowWindow()
		{
			IntPtr intPtr = WindowsAPI.FindWindow("NotifyIconOverflowWindow", null);
			if (intPtr != IntPtr.Zero)
			{
				intPtr = WindowsAPI.FindWindowEx(intPtr, IntPtr.Zero, "ToolbarWindow32", null);
			}
			return intPtr;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000473A4 File Offset: 0x000455A4
		public static SysTrayWnd.TrayItemData[] GetTrayWndDetail(IntPtr hTrayWnd)
		{
			SortedList<string, SysTrayWnd.TrayItemData> sortedList = new SortedList<string, SysTrayWnd.TrayItemData>();
			SysTrayWnd.TrayItemData[] array;
			if (hTrayWnd == IntPtr.Zero)
			{
				array = new SysTrayWnd.TrayItemData[0];
			}
			else
			{
				try
				{
					TBBUTTON tbbutton = new TBBUTTON();
					IntPtr intPtr = IntPtr.Zero;
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					WindowsAPI.GetWindowThreadProcessId(hTrayWnd, out num);
					intPtr = WindowsAPI.OpenProcess(2035711U, 0, (uint)num);
					num2 = WindowsAPI.VirtualAllocEx(intPtr, 0, Marshal.SizeOf<TBBUTTON>(tbbutton), 4096, 4);
					num3 = WindowsAPI.SendMessage(hTrayWnd, 1048, 0, 0);
					for (int i = 0; i < num3; i++)
					{
						try
						{
							SysTrayWnd.TrayItemData trayItemData = default(SysTrayWnd.TrayItemData);
							TRAYDATA traydata = new TRAYDATA();
							int num4 = 0;
							int num5 = 0;
							IntPtr intPtr2 = IntPtr.Zero;
							string text = string.Empty;
							int num6 = WindowsAPI.SendMessage(hTrayWnd, 1047, i, num2);
							IntPtr intPtr3 = Marshal.AllocHGlobal(Marshal.SizeOf<TBBUTTON>(tbbutton));
							IntPtr intPtr4 = Marshal.AllocHGlobal(Marshal.SizeOf<TRAYDATA>(traydata));
							num6 = WindowsAPI.ReadProcessMemory(intPtr, num2, intPtr3, Marshal.SizeOf<TBBUTTON>(tbbutton), out num4);
							Marshal.PtrToStructure<TBBUTTON>(intPtr3, tbbutton);
							num6 = WindowsAPI.ReadProcessMemory(intPtr, tbbutton.dwData.ToInt32(), intPtr4, Marshal.SizeOf<TRAYDATA>(traydata), out num4);
							Marshal.PtrToStructure<TRAYDATA>(intPtr4, traydata);
							byte[] array2 = new byte[1024];
							num6 = WindowsAPI.ReadProcessMemory(intPtr, tbbutton.iString.ToInt32(), array2, 1024, out num4);
							text = Encoding.Unicode.GetString(array2);
							if (!string.IsNullOrEmpty(text))
							{
								int num7 = text.IndexOf('\0');
								text = text.Substring(0, num7);
							}
							WindowsAPI.GetWindowThreadProcessId(traydata.hwnd, out num5);
							intPtr2 = WindowsAPI.OpenProcess(1024U, 0, (uint)num5);
							StringBuilder stringBuilder = new StringBuilder(256);
							if (intPtr2 != IntPtr.Zero)
							{
								WindowsAPI.GetProcessImageFileName(intPtr2, stringBuilder, stringBuilder.Capacity);
							}
							string text2 = string.Empty;
							if (stringBuilder.Length > 0)
							{
								int num8 = stringBuilder.ToString().IndexOf("\\", "\\Device\\HarddiskVolume".Length);
								string text3 = stringBuilder.ToString().Substring(0, num8);
								for (int j = 65; j <= 90; j++)
								{
									StringBuilder stringBuilder2 = new StringBuilder(256);
									num6 = WindowsAPI.QueryDosDevice(((char)j).ToString() + ":", stringBuilder2, stringBuilder2.Capacity);
									if (num6 != 0 && stringBuilder2.ToString() == text3)
									{
										text2 = ((char)j).ToString() + ":" + stringBuilder.ToString().Replace(text3, "");
										break;
									}
								}
							}
							trayItemData.int_0 = num5;
							trayItemData.hIcon = traydata.hIcon;
							trayItemData.hProcess = intPtr2;
							trayItemData.hWnd = traydata.hwnd;
							trayItemData.idBitmap = tbbutton.iBitmap;
							trayItemData.idCommand = tbbutton.idCommand;
							trayItemData.lpProcImagePath = text2;
							trayItemData.lpTrayToolTip = text;
							trayItemData.uID = traydata.uID;
							trayItemData.uCallbackMessage = traydata.uCallbackMessage;
							sortedList[string.Format("{0:d8}", tbbutton.idCommand)] = trayItemData;
						}
						catch (Exception ex)
						{
							LogWriter.WriteLog(ex.ToString(), 1);
						}
					}
					WindowsAPI.VirtualFreeEx(intPtr, num2, Marshal.SizeOf<TBBUTTON>(tbbutton), 32768U);
					WindowsAPI.CloseHandle(intPtr);
					SysTrayWnd.TrayItemData[] array3 = new SysTrayWnd.TrayItemData[sortedList.Count];
					sortedList.Values.CopyTo(array3, 0);
					array = array3;
				}
				catch (SEHException ex2)
				{
					throw ex2;
				}
				catch (Exception ex3)
				{
					throw ex3;
				}
			}
			return array;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00004213 File Offset: 0x00002413
		public static void CloseInActiveTrayWnd()
		{
			SysTrayWnd.a(SysTrayWnd.GetTrayWnd());
			SysTrayWnd.a(SysTrayWnd.GetNotifyIconOverflowWindow());
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00047788 File Offset: 0x00045988
		private static void a(IntPtr A_0)
		{
			try
			{
				SysTrayWnd.TrayItemData[] trayWndDetail = SysTrayWnd.GetTrayWndDetail(A_0);
				for (int i = trayWndDetail.Length - 1; i >= 0; i--)
				{
					int num = 0;
					WindowsAPI.GetExitCodeProcess(trayWndDetail[i].hProcess, ref num);
					if (num != 259)
					{
						SysTrayWnd.TrayItemData trayItemData = trayWndDetail[i];
						NOTIFYICONDATA notifyicondata = default(NOTIFYICONDATA);
						notifyicondata.hWnd = trayItemData.hWnd;
						notifyicondata.uID = trayItemData.uID;
						notifyicondata.uCallbackMessage = trayItemData.uCallbackMessage;
						notifyicondata.hIcon = trayItemData.hIcon;
						notifyicondata.uFlags = 7U;
						WindowsAPI.Shell_NotifyIconA(2, ref notifyicondata);
					}
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
			}
		}

		// Token: 0x020000D9 RID: 217
		public struct TrayItemData
		{
			// Token: 0x040004A0 RID: 1184
			public int int_0;

			// Token: 0x040004A1 RID: 1185
			public byte fsState;

			// Token: 0x040004A2 RID: 1186
			public byte fsStyle;

			// Token: 0x040004A3 RID: 1187
			public IntPtr hIcon;

			// Token: 0x040004A4 RID: 1188
			public IntPtr hProcess;

			// Token: 0x040004A5 RID: 1189
			public IntPtr hWnd;

			// Token: 0x040004A6 RID: 1190
			public int idBitmap;

			// Token: 0x040004A7 RID: 1191
			public int idCommand;

			// Token: 0x040004A8 RID: 1192
			public string lpProcImagePath;

			// Token: 0x040004A9 RID: 1193
			public string lpTrayToolTip;

			// Token: 0x040004AA RID: 1194
			public uint uID;

			// Token: 0x040004AB RID: 1195
			public uint uCallbackMessage;
		}
	}
}
