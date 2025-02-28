using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AliwwClient
{
	// Token: 0x02000066 RID: 102
	public class TreeViewApi
	{
		// Token: 0x060002F2 RID: 754
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern int SendMessage(int A_0, int A_1, IntPtr A_2, IntPtr A_3);

		// Token: 0x060002F3 RID: 755
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern int SendMessage(int A_0, int A_1, IntPtr A_2, TVITEM A_3);

		// Token: 0x060002F4 RID: 756
		[DllImport("user32.dll")]
		public static extern int GetWindowText(int hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x060002F5 RID: 757
		[DllImport("user32.dll")]
		public static extern int EnumChildWindows(int hWndParent, TreeViewApi.MyCallBack lpfn, StringBuilder value);

		// Token: 0x060002F6 RID: 758
		[DllImport("user32.dll")]
		public static extern int GetClassNameA(int hwnd, StringBuilder lpClassName, int nMaxCount);

		// Token: 0x060002F7 RID: 759
		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		public static extern int CopyMemory(StringBuilder Destination, IntPtr Source, int Length);

		// Token: 0x060002F8 RID: 760
		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		public static extern int GlobalAlloc(int wFlags, int dwBytes);

		// Token: 0x060002F9 RID: 761
		[DllImport("kernel32", CharSet = CharSet.Unicode)]
		public static extern int GlobalFree(IntPtr hMem);

		// Token: 0x060002FA RID: 762 RVA: 0x00034B74 File Offset: 0x00032D74
		public static bool SelectNode(IntPtr TreeViewHwnd, IntPtr ItemHwnd)
		{
			int num = TreeViewApi.SendMessage(TreeViewHwnd.ToInt32(), 4363, new IntPtr(9), ItemHwnd);
			return num != 0;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00034BA8 File Offset: 0x00032DA8
		public static IntPtr GetRoot(IntPtr TreeViewHwnd)
		{
			TVITEM tvitem = new TVITEM();
			IntPtr intPtr = Marshal.AllocHGlobal(1024);
			tvitem.hItem = TreeViewHwnd;
			tvitem.mask = 1;
			tvitem.pszText = intPtr;
			tvitem.cchTextMax = 1024;
			int num = TreeViewApi.SendMessage(TreeViewHwnd.ToInt32(), 4362, new IntPtr(0), tvitem);
			Marshal.FreeHGlobal(intPtr);
			return new IntPtr(num);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00034C10 File Offset: 0x00032E10
		public static IntPtr GetNextItem(IntPtr TreeViewHwnd, IntPtr PrevHwnd)
		{
			int num = TreeViewApi.SendMessage(TreeViewHwnd.ToInt32(), 4362, new IntPtr(1), PrevHwnd);
			return new IntPtr(num);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00034C40 File Offset: 0x00032E40
		public static IntPtr GetFirstChildItem(IntPtr TreeViewHwnd, IntPtr ParentHwnd)
		{
			int num = TreeViewApi.SendMessage(TreeViewHwnd.ToInt32(), 4362, new IntPtr(4), ParentHwnd);
			return new IntPtr(num);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00034C70 File Offset: 0x00032E70
		public static string GetItemText(IntPtr TreeViewHwnd, IntPtr ItemHwnd)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			int num = TreeViewApi.GlobalAlloc(0, 1024);
			if (num > 0)
			{
				TVITEM tvitem = new TVITEM();
				tvitem.mask = 1;
				tvitem.int_0 = ItemHwnd.ToInt32();
				tvitem.pszText = new IntPtr(num);
				tvitem.cchTextMax = 1023;
				TreeViewApi.SendMessage(TreeViewHwnd.ToInt32(), 4364, IntPtr.Zero, tvitem);
				TreeViewApi.CopyMemory(stringBuilder, new IntPtr(num), 1024);
				TreeViewApi.GlobalFree(new IntPtr(num));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00034D0C File Offset: 0x00032F0C
		public static string GetItemText2(IntPtr TreeViewHwnd, IntPtr ItemHwnd)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(1024);
			TVITEM tvitem = new TVITEM();
			tvitem.hItem = ItemHwnd;
			tvitem.mask = 1;
			tvitem.pszText = intPtr;
			tvitem.cchTextMax = 1024;
			StringBuilder stringBuilder = new StringBuilder(1024);
			TreeViewApi.SendMessage(TreeViewHwnd.ToInt32(), 4364, new IntPtr(0), tvitem);
			TreeViewApi.CopyMemory(stringBuilder, intPtr, 1024);
			Marshal.FreeHGlobal(intPtr);
			return stringBuilder.ToString();
		}

		// Token: 0x02000067 RID: 103
		// (Invoke) Token: 0x06000302 RID: 770
		public delegate bool MyCallBack(IntPtr hwnd, int lParam);
	}
}
