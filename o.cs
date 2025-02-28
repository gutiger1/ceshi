using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Agiso.AliwwApi;
using Agiso.AliwwApi.Object;
using Agiso.Windows;

// Token: 0x02000017 RID: 23
internal abstract class o
{
	// Token: 0x06000077 RID: 119 RVA: 0x00002646 File Offset: 0x00000846
	[CompilerGenerated]
	protected bool e()
	{
		return this.b;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0000264E File Offset: 0x0000084E
	[CompilerGenerated]
	protected void a(bool A_0)
	{
		this.b = A_0;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x000025CE File Offset: 0x000007CE
	public o()
	{
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00002657 File Offset: 0x00000857
	public o(string A_0)
	{
		this.a = A_0;
	}

	// Token: 0x0600007B RID: 123
	public abstract void f();

	// Token: 0x0600007C RID: 124
	public abstract AliwwVersion g();

	// Token: 0x0600007D RID: 125
	public abstract AliwwMainWindow h();

	// Token: 0x0600007E RID: 126
	public abstract AliwwTalkWindow i();

	// Token: 0x0600007F RID: 127
	public abstract AliwwTalkWindow c(string A_0);

	// Token: 0x06000080 RID: 128
	public abstract bool d(string A_0);

	// Token: 0x06000081 RID: 129
	public abstract WindowInfo a(string A_0, List<WindowInfo> A_1);

	// Token: 0x06000082 RID: 130
	public abstract List<WindowInfo> j();

	// Token: 0x06000083 RID: 131
	public abstract List<WindowInfo> k();

	// Token: 0x06000084 RID: 132
	public abstract WindowInfo[] l();

	// Token: 0x06000085 RID: 133
	public abstract WindowInfo[] m();

	// Token: 0x06000086 RID: 134 RVA: 0x0001117C File Offset: 0x0000F37C
	public WindowInfo[] d()
	{
		List<WindowInfo> list = this.j();
		WindowInfo[] array;
		if (list == null)
		{
			array = null;
		}
		else
		{
			array = list.ToArray();
		}
		return array;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x000111A4 File Offset: 0x0000F3A4
	private void c()
	{
		WindowInfo[] array = this.d();
		if (this.e() && array != null)
		{
			foreach (WindowInfo windowInfo in array)
			{
				windowInfo.Close(true);
				Application.DoEvents();
			}
		}
	}

	// Token: 0x06000088 RID: 136 RVA: 0x000111EC File Offset: 0x0000F3EC
	private void a(Dictionary<IntPtr, int> A_0 = null)
	{
		if (A_0 == null)
		{
			A_0 = new Dictionary<IntPtr, int>();
		}
		WindowInfo[] array = this.m();
		if (array != null)
		{
			int num = 0;
			foreach (WindowInfo windowInfo in array)
			{
				windowInfo.Close(true);
				if (A_0.ContainsKey(windowInfo.HWnd))
				{
					A_0[windowInfo.HWnd] = A_0[windowInfo.HWnd] + 1;
				}
				else
				{
					A_0.Add(windowInfo.HWnd, 1);
				}
				if (A_0.ContainsKey(windowInfo.HWnd) && A_0[windowInfo.HWnd] > 2)
				{
					num++;
				}
				Application.DoEvents();
			}
			if (array.Length - num > 0)
			{
				Application.DoEvents();
				Thread.Sleep(200);
				this.a(A_0);
			}
		}
	}

	// Token: 0x06000089 RID: 137 RVA: 0x000112C8 File Offset: 0x0000F4C8
	private void a()
	{
		WindowInfo[] array = this.l();
		if (array != null)
		{
			foreach (WindowInfo windowInfo in array)
			{
				windowInfo.Close(true);
				Application.DoEvents();
			}
		}
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00011304 File Offset: 0x0000F504
	public AliwwTalkWindow a(string A_0)
	{
		AliwwTalkWindow aliwwTalkWindow = this.i();
		if (aliwwTalkWindow == null)
		{
			aliwwTalkWindow = this.c(A_0);
		}
		if (aliwwTalkWindow != null)
		{
			aliwwTalkWindow.UserNick = this.a;
		}
		return aliwwTalkWindow;
	}

	// Token: 0x0400004E RID: 78
	private string a;

	// Token: 0x0400004F RID: 79
	[CompilerGenerated]
	private bool b;
}
