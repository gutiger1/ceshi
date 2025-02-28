using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Agiso.Utils;

namespace AliwwClient.WebSocketServer.Extensions
{
	// Token: 0x02000083 RID: 131
	public static class LoginBehaviorExtension
	{
		// Token: 0x0600039D RID: 925 RVA: 0x00039438 File Offset: 0x00037638
		public static string GetHtml(this LoginBehavior session)
		{
			LoginBehaviorExtension.a a = new LoginBehaviorExtension.a();
			a.a = session;
			string text;
			if (a.a == null)
			{
				text = "";
			}
			else
			{
				a.b = Guid.NewGuid().ToString().Replace("-", "");
				a.a.DicWebSocketString[a.b] = null;
				string text2 = "";
				if (a.a.SendTo(JSON.Encode(new g<string, string>("getHtml", a.b))) && Util.CheckWait(1500, new Func<bool>(a.c), 200))
				{
					text2 = a.a.DicWebSocketString[a.b];
				}
				a.a.DicWebSocketString.Remove(a.b);
				text = text2;
			}
			return text;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00039520 File Offset: 0x00037720
		public static string GetSideOffset(this LoginBehavior session)
		{
			LoginBehaviorExtension.b b = new LoginBehaviorExtension.b();
			b.a = session;
			string text;
			if (b.a == null)
			{
				text = "";
			}
			else
			{
				b.b = Guid.NewGuid().ToString().Replace("-", "");
				b.a.DicWebSocketString[b.b] = null;
				string text2 = "";
				if (b.a.SendTo(JSON.Encode(new g<string, string>("getSideOffset", b.b))) && Util.CheckWait(1000, new Func<bool>(b.c), 200))
				{
					text2 = b.a.DicWebSocketString[b.b];
				}
				b.a.DicWebSocketString.Remove(b.b);
				text = text2;
			}
			return text;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00039608 File Offset: 0x00037808
		public static string GetImageCaptcha(this LoginBehavior session)
		{
			LoginBehaviorExtension.c c = new LoginBehaviorExtension.c();
			c.a = session;
			string text;
			if (c.a == null)
			{
				text = "";
			}
			else
			{
				c.b = Guid.NewGuid().ToString().Replace("-", "");
				c.a.DicWebSocketString[c.b] = null;
				string text2 = "";
				if (c.a.SendTo(JSON.Encode(new g<string, string>("getImageCaptcha", c.b))) && Util.CheckWait(1000, new Func<bool>(c.c), 200))
				{
					text2 = c.a.DicWebSocketString[c.b];
					int num = text2.IndexOf(',');
					if (num > 0)
					{
						return text2.Substring(num + 1);
					}
				}
				c.a.DicWebSocketString.Remove(c.b);
				text = text2;
			}
			return text;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00039710 File Offset: 0x00037910
		public static void ReloadImageCaptcha(this LoginBehavior session)
		{
			if (session != null)
			{
				session.SendTo(JSON.Encode(new h<string>("reloadImageCaptcha")));
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0003973C File Offset: 0x0003793C
		public static void SubmitImageCaptcha(this LoginBehavior session, string captcha)
		{
			if (session != null)
			{
				Thread.Sleep(1000);
				session.SendTo(JSON.Encode(new i<string, string>("submitImageCaptcha", captcha)));
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00039774 File Offset: 0x00037974
		public static void ClickSmsButton(this LoginBehavior session)
		{
			if (session != null)
			{
				session.SendTo(JSON.Encode(new h<string>("clickSmsButton")));
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000397A0 File Offset: 0x000379A0
		public static void SubmitMobileCaptcha(this LoginBehavior session, string captcha)
		{
			if (session != null)
			{
				session.SendTo(JSON.Encode(new i<string, string>("submitMobileCaptcha", captcha)));
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x000397CC File Offset: 0x000379CC
		public static void ResendMobileCaptcha(this LoginBehavior session)
		{
			if (session != null)
			{
				session.SendTo(JSON.Encode(new h<string>("resendMobileCaptcha")));
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00003450 File Offset: 0x00001650
		public static void ClickRefresh(this LoginBehavior session)
		{
			if (session != null)
			{
				session.SendTo(JSON.Encode(new h<string>("clickRefresh")));
			}
		}

		// Token: 0x02000084 RID: 132
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x060003A7 RID: 935 RVA: 0x000397F8 File Offset: 0x000379F8
			internal bool c()
			{
				return !string.IsNullOrEmpty(this.a.DicWebSocketString[this.b]);
			}

			// Token: 0x0400032A RID: 810
			public LoginBehavior a;

			// Token: 0x0400032B RID: 811
			public string b;
		}

		// Token: 0x02000085 RID: 133
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x060003A9 RID: 937 RVA: 0x00039824 File Offset: 0x00037A24
			internal bool c()
			{
				return !string.IsNullOrEmpty(this.a.DicWebSocketString[this.b]);
			}

			// Token: 0x0400032C RID: 812
			public LoginBehavior a;

			// Token: 0x0400032D RID: 813
			public string b;
		}

		// Token: 0x02000086 RID: 134
		[CompilerGenerated]
		private sealed class c
		{
			// Token: 0x060003AB RID: 939 RVA: 0x00039850 File Offset: 0x00037A50
			internal bool c()
			{
				return !string.IsNullOrEmpty(this.a.DicWebSocketString[this.b]);
			}

			// Token: 0x0400032E RID: 814
			public LoginBehavior a;

			// Token: 0x0400032F RID: 815
			public string b;
		}
	}
}
