using System;
using System.Threading;
using System.Windows.Forms;
using Agiso.Utils;
using Agiso.Windows;
using HtmlAgilityPack;

namespace Agiso.AliwwApi
{
	// Token: 0x02000714 RID: 1812
	public class WinLoginImageCaptchaBuyer9 : WinChromeContainer
	{
		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060023DC RID: 9180 RVA: 0x0005F2B0 File Offset: 0x0005D4B0
		public WindowInfo OkBtn
		{
			get
			{
				if (this.a == null)
				{
					this.a = base.FindWindowInDescendant("StandardButton", "确定", false, new bool?(false));
				}
				return this.a;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x0005F2F0 File Offset: 0x0005D4F0
		public WindowInfo Input
		{
			get
			{
				if (this.b == null)
				{
					this.b = base.FindWindowInDescendant("SearchInput", "", false, new bool?(false));
				}
				return this.b;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x0005F330 File Offset: 0x0005D530
		public WindowInfo CaptchaChrome
		{
			get
			{
				if (this.c == null)
				{
					this.c = base.FindWindowInDescendant("WebControl", "", false, new bool?(false));
				}
				return this.c;
			}
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x0000ECAC File Offset: 0x0000CEAC
		public void SubmitCaptcha(string captcha)
		{
			this.Input.SetText(captcha);
			this.OkBtn.Click(15, 15, true);
			Application.DoEvents();
			Thread.Sleep(100);
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x0005F370 File Offset: 0x0005D570
		public string GetCaptchaPath()
		{
			for (int i = 0; i < 30; i++)
			{
				string html = this.GetHtml("alires:///WebUI/SecurityCode/securitycode.htm", null);
				if (string.IsNullOrEmpty(html))
				{
					i += 9;
					Thread.Sleep(100);
				}
				else
				{
					HtmlDocument doc = HtmlAgilityPackHelper.GetDoc(html);
					if (doc != null)
					{
						HtmlNode elementbyId = doc.GetElementbyId("scode");
						if (elementbyId != null)
						{
							HtmlAttribute htmlAttribute = elementbyId.Attributes["src"];
							if (htmlAttribute != null)
							{
								string value = htmlAttribute.Value;
								if (!string.IsNullOrEmpty(value))
								{
									if (value.ToLower().StartsWith("http"))
									{
										return value;
									}
									LogWriter.WriteLog("无效的图片URL: " + value + "\r\n" + html, 1);
								}
							}
						}
					}
				}
			}
			return "";
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x0005F44C File Offset: 0x0005D64C
		public bool IsValid()
		{
			return this.OkBtn != null && this.Input != null;
		}

		// Token: 0x04001DDA RID: 7642
		private WindowInfo a;

		// Token: 0x04001DDB RID: 7643
		private WindowInfo b;

		// Token: 0x04001DDC RID: 7644
		private WindowInfo c;
	}
}
