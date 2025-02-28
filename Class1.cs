using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AliwwClient
{
	// Token: 0x02000064 RID: 100
	public class Class1
	{
		// Token: 0x060002EB RID: 747
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int RegisterWindowMessage(string lpString);

		// Token: 0x060002EC RID: 748
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

		// Token: 0x060002ED RID: 749
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SendMessage(int hWnd, int Msg, int wparam, int lparam);

		// Token: 0x060002EE RID: 750 RVA: 0x00003173 File Offset: 0x00001373
		public void RegisterControlforMessages()
		{
			Class1.RegisterWindowMessage("WM_GETTEXT");
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00034B20 File Offset: 0x00032D20
		public string GetControlText(IntPtr hWnd)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = Class1.SendMessage((int)hWnd, 14, 0, 0).ToInt32();
			if (num > 0)
			{
				stringBuilder = new StringBuilder(num + 1);
				Class1.SendMessage(hWnd, 13U, stringBuilder.Capacity, stringBuilder);
			}
			return stringBuilder.ToString();
		}
	}
}
