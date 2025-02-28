using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Agiso.Utils;

namespace Agiso
{
	// Token: 0x020000CC RID: 204
	public class ClipboardProxy
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x00046248 File Offset: 0x00044448
		public void Hold()
		{
			try
			{
				AppConfig.WriteLog("ClipboardHolder.Hold Start", LogType.LogForTraceHold, 1);
				if (ClipboardProxy.c())
				{
					this.b = "Text";
					this.a = ClipboardProxy.GetText(1);
				}
				else if (ClipboardProxy.b())
				{
					this.b = "FileDrop";
					this.a = ClipboardProxy.f();
				}
				else if (ClipboardProxy.ContainsImage(10))
				{
					this.b = "Image";
					this.a = ClipboardProxy.e();
				}
				else if (ClipboardProxy.a())
				{
					this.b = "Audio";
					this.a = ClipboardProxy.d();
				}
				AppConfig.WriteLog("ClipboardHolder.Hold End", LogType.LogForTraceHold, 1);
			}
			catch
			{
				this.a = null;
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00046310 File Offset: 0x00044510
		public void Recover()
		{
			try
			{
				if (this.a != null)
				{
					AppConfig.WriteLog("ClipboardHolder.Recover Start", LogType.LogForTraceHold, 1);
					string text = this.b;
					string text2 = text;
					if (!(text2 == "Text"))
					{
						if (!(text2 == "FileDrop"))
						{
							if (!(text2 == "Image"))
							{
								if (text2 == "Audio")
								{
									ClipboardProxy.a((Stream)this.a);
								}
							}
							else
							{
								ClipboardProxy.a((Image)this.a);
							}
						}
						else
						{
							ClipboardProxy.a((StringCollection)this.a);
						}
					}
					else
					{
						ClipboardProxy.a((string)this.a);
					}
					AppConfig.WriteLog("ClipboardHolder.Recover End", LogType.LogForTraceHold, 1);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000463E4 File Offset: 0x000445E4
		public static string GetText(int restryTimes = 10)
		{
			for (int i = 0; i < restryTimes; i++)
			{
				try
				{
					return ClipboardProxy.Invoke<string>(new Func<string>(ClipboardProxy.<>c.<>9.i));
				}
				catch (Exception ex)
				{
					if (i == restryTimes - 1)
					{
						LogWriter.WriteLog(string.Format("获取剪贴板内容时出错了，{0}", ex), 1);
					}
					Thread.Sleep(100);
				}
			}
			return "";
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00046464 File Offset: 0x00044664
		private static StringCollection f()
		{
			return ClipboardProxy.Invoke<StringCollection>(new Func<StringCollection>(ClipboardProxy.<>c.<>9.h));
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00046498 File Offset: 0x00044698
		private static Image e()
		{
			return ClipboardProxy.Invoke<Image>(new Func<Image>(ClipboardProxy.<>c.<>9.g));
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000464CC File Offset: 0x000446CC
		private static Stream d()
		{
			return ClipboardProxy.Invoke<Stream>(new Func<Stream>(ClipboardProxy.<>c.<>9.f));
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00046500 File Offset: 0x00044700
		public static bool SetText(string text, TextDataFormat format, int restryTimes = 10)
		{
			ClipboardProxy.e e = new ClipboardProxy.e();
			e.a = text;
			e.b = format;
			for (int i = 0; i < restryTimes; i++)
			{
				try
				{
					Action action;
					if ((action = e.c) == null)
					{
						action = (e.c = new Action(e.d));
					}
					ClipboardProxy.Invoke(action);
					return true;
				}
				catch (Exception ex)
				{
					if (i == restryTimes - 1)
					{
						LogWriter.WriteLog(string.Format("剪贴板赋值时出错了，{0}", ex), 1);
					}
					Thread.Sleep(100);
				}
			}
			return false;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00046594 File Offset: 0x00044794
		private static a Invoke<a>(Func<a> A_0)
		{
			a a;
			if (AppConfig.AllowAutoLogin && k.a().InvokeRequired)
			{
				a = (a)((object)k.a().Invoke(A_0));
			}
			else
			{
				a = A_0();
			}
			return a;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000465D4 File Offset: 0x000447D4
		private static void Invoke(Action A_0)
		{
			if (AppConfig.AllowAutoLogin && k.a().InvokeRequired)
			{
				k.a().Invoke(A_0);
			}
			else
			{
				A_0();
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0004660C File Offset: 0x0004480C
		private static bool c()
		{
			return ClipboardProxy.Invoke<bool>(new Func<bool>(ClipboardProxy.<>c.<>9.e));
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00046640 File Offset: 0x00044840
		private static bool b()
		{
			return ClipboardProxy.Invoke<bool>(new Func<bool>(ClipboardProxy.<>c.<>9.d));
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00046674 File Offset: 0x00044874
		public static bool ContainsImage(int restryTimes = 10)
		{
			for (int i = 0; i < restryTimes; i++)
			{
				try
				{
					return ClipboardProxy.Invoke<bool>(new Func<bool>(ClipboardProxy.<>c.<>9.c));
				}
				catch (Exception ex)
				{
					if (i == restryTimes - 1)
					{
						LogWriter.WriteLog(string.Format("剪贴板包含图片判断时出错了，{0}", ex), 1);
					}
					Thread.Sleep(100);
				}
			}
			return false;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000466F0 File Offset: 0x000448F0
		private static bool a()
		{
			return ClipboardProxy.Invoke<bool>(new Func<bool>(ClipboardProxy.<>c.<>9.b));
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00046724 File Offset: 0x00044924
		private static void a(string A_0)
		{
			ClipboardProxy.a a = new ClipboardProxy.a();
			a.a = A_0;
			ClipboardProxy.Invoke(new Action(a.b));
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00046750 File Offset: 0x00044950
		private static void a(StringCollection A_0)
		{
			ClipboardProxy.b b = new ClipboardProxy.b();
			b.a = A_0;
			ClipboardProxy.Invoke(new Action(b.b));
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0004677C File Offset: 0x0004497C
		private static void a(Image A_0)
		{
			ClipboardProxy.c c = new ClipboardProxy.c();
			c.a = A_0;
			ClipboardProxy.Invoke(new Action(c.b));
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000467A8 File Offset: 0x000449A8
		private static void a(Stream A_0)
		{
			ClipboardProxy.d d = new ClipboardProxy.d();
			d.a = A_0;
			ClipboardProxy.Invoke(new Action(d.b));
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000467D4 File Offset: 0x000449D4
		public static bool Clear()
		{
			for (int i = 0; i < 10; i++)
			{
				try
				{
					ClipboardProxy.Invoke(new Action(ClipboardProxy.<>c.<>9.a));
					return true;
				}
				catch (Exception ex)
				{
					if (i == 9)
					{
						LogWriter.WriteLog(string.Format("清空剪贴板时出错了，{0}", ex), 1);
					}
					Thread.Sleep(100);
				}
			}
			return false;
		}

		// Token: 0x0400048D RID: 1165
		private object a = null;

		// Token: 0x0400048E RID: 1166
		private string b = "";

		// Token: 0x020000CE RID: 206
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x06000655 RID: 1621 RVA: 0x000041CC File Offset: 0x000023CC
			internal void b()
			{
				Clipboard.SetText(this.a);
			}

			// Token: 0x04000499 RID: 1177
			public string a;
		}

		// Token: 0x020000CF RID: 207
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x06000657 RID: 1623 RVA: 0x000041D9 File Offset: 0x000023D9
			internal void b()
			{
				Clipboard.SetFileDropList(this.a);
			}

			// Token: 0x0400049A RID: 1178
			public StringCollection a;
		}

		// Token: 0x020000D0 RID: 208
		[CompilerGenerated]
		private sealed class c
		{
			// Token: 0x06000659 RID: 1625 RVA: 0x000041E6 File Offset: 0x000023E6
			internal void b()
			{
				Clipboard.SetImage(this.a);
			}

			// Token: 0x0400049B RID: 1179
			public Image a;
		}

		// Token: 0x020000D1 RID: 209
		[CompilerGenerated]
		private sealed class d
		{
			// Token: 0x0600065B RID: 1627 RVA: 0x000041F3 File Offset: 0x000023F3
			internal void b()
			{
				Clipboard.SetAudio(this.a);
			}

			// Token: 0x0400049C RID: 1180
			public Stream a;
		}

		// Token: 0x020000D2 RID: 210
		[CompilerGenerated]
		private sealed class e
		{
			// Token: 0x0600065D RID: 1629 RVA: 0x00004200 File Offset: 0x00002400
			internal void d()
			{
				Clipboard.SetText(this.a, this.b);
			}

			// Token: 0x0400049D RID: 1181
			public string a;

			// Token: 0x0400049E RID: 1182
			public TextDataFormat b;

			// Token: 0x0400049F RID: 1183
			public Action c;
		}
	}
}
