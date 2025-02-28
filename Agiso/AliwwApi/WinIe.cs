using System;
using Agiso.AliwwApi.Object;
using Agiso.Extensions;
using Agiso.Handler;
using Agiso.Utils;
using Agiso.Windows;
using HtmlAgilityPack;
using mshtml;

namespace Agiso.AliwwApi
{
	// Token: 0x02000711 RID: 1809
	public class WinIe : WindowInfo, IWinHtmlRecentContainer, IWinHtmlContainer
	{
		// Token: 0x060023C4 RID: 9156 RVA: 0x0005E834 File Offset: 0x0005CA34
		public string GetInnerText(string userNick)
		{
			IntPtr hwnd = base.HWnd;
			string text;
			if (hwnd == IntPtr.Zero)
			{
				text = null;
			}
			else
			{
				IHTMLDocument2 ihtmldocument = null;
				IHTMLDocument3 ihtmldocument2 = null;
				try
				{
					ihtmldocument = (IHTMLDocument2)Win32Extend.GetHtmlDocument(hwnd);
				}
				catch
				{
				}
				try
				{
					ihtmldocument2 = (IHTMLDocument3)Win32Extend.GetHtmlDocument(hwnd);
				}
				catch
				{
				}
				if (ihtmldocument2 != null)
				{
					IHTMLElement elementById = ihtmldocument2.getElementById("History");
					IHTMLElement elementById2 = ihtmldocument2.getElementById("content");
					if (elementById2 == null || string.IsNullOrEmpty(elementById2.innerText))
					{
						text = "";
					}
					else if (elementById != null && !string.IsNullOrEmpty(elementById.innerText))
					{
						text = elementById2.innerText.Replace(elementById.innerText, "");
					}
					else
					{
						text = elementById2.innerText;
					}
				}
				else if (ihtmldocument != null && ihtmldocument.body != null)
				{
					text = ihtmldocument.body.innerText;
				}
				else
				{
					text = "";
				}
			}
			return text;
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x0005E94C File Offset: 0x0005CB4C
		public string GetHtml(string userNick)
		{
			string text;
			try
			{
				text = Win32Extend.GetHtmlText(base.HWnd);
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog("GetHtml时异常: " + ex.ToString(), 1);
				text = "";
			}
			return text;
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x0005E99C File Offset: 0x0005CB9C
		public static WinIe Get(IntPtr parent)
		{
			WindowInfo windowInfo = new WindowInfo(parent).FindWindowInDescendant("Internet Explorer_Server", null, false, new bool?(false));
			WinIe winIe;
			if (windowInfo != null)
			{
				winIe = windowInfo.Convert<WinIe>();
			}
			else
			{
				winIe = null;
			}
			return winIe;
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x0005E9D8 File Offset: 0x0005CBD8
		public AliwwMsgElement GetLastReceiveMessage(string userNick)
		{
			string html = this.GetHtml(userNick);
			AliwwMsgElement aliwwMsgElement;
			if (string.IsNullOrEmpty(html))
			{
				aliwwMsgElement = null;
			}
			else
			{
				AliwwMsgElement aliwwMsgElement2 = new AliwwMsgElement();
				HtmlNode htmlNode = HtmlNode.CreateNode("<div>" + html + "</div>");
				HtmlNodeCollection htmlNodeCollection = htmlNode.SelectNodes(string.Format("//*[@id=\"{0}\"]", "MsgElement"));
				if (htmlNodeCollection == null || htmlNodeCollection.Count == 0)
				{
					aliwwMsgElement = aliwwMsgElement2;
				}
				else
				{
					HtmlNode htmlNode2 = htmlNodeCollection[htmlNodeCollection.Count - 1];
					aliwwMsgElement2.MsgSendId = htmlNode2.Attributes["SendID"].Value;
					HtmlNode htmlNode3 = htmlNode2.SelectSingleNode(string.Format(".//*[@id=\"{0}\"]", "SenderName"));
					HtmlNode htmlNode4 = htmlNode2.SelectSingleNode(string.Format(".//*[@id=\"{0}\"]", "SenderSite"));
					HtmlNode htmlNode5 = htmlNode2.SelectSingleNode(string.Format(".//*[@id=\"{0}\"]", "MsgTime"));
					aliwwMsgElement2.MsgHead = new AliwwMsgElementMsgHead();
					if (htmlNode5 != null)
					{
						string text = htmlNode5.InnerText.Trim().Replace("(", "").Replace(")", "");
						if (text.IndexOf("-") < 0 && text.IndexOf("/") < 0 && text.IndexOf("月") < 0)
						{
							text = DateTime.Today.ToString("yyyy-MM-dd") + " " + text;
						}
						if (text.EndsWith(":") || text.EndsWith("："))
						{
							text = text.Substring(0, text.Length - 1);
						}
						DateTime? dateTime = Util.ToDateTime(text);
						aliwwMsgElement2.MsgHead.MsgTime = ((dateTime != null) ? dateTime.Value : DateTime.MinValue);
					}
					if (htmlNode3 != null)
					{
						aliwwMsgElement2.MsgHead.SenderName = htmlNode3.InnerText;
					}
					if (htmlNode4 != null)
					{
						aliwwMsgElement2.MsgHead.SenderSite = htmlNode4.InnerText;
					}
					aliwwMsgElement2.MsgContent = new AliwwMsgElementMsgContent();
					HtmlNode htmlNode6 = htmlNode2.SelectSingleNode(string.Format(".//*[@id=\"{0}\"]", "MsgContent"));
					if (htmlNode6 != null)
					{
						aliwwMsgElement2.MsgContent.AppendContentText(htmlNode6.InnerText);
						aliwwMsgElement2.MsgContent.AppendContentHtml(htmlNode6.InnerHtml);
					}
					aliwwMsgElement = aliwwMsgElement2;
				}
			}
			return aliwwMsgElement;
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x0005EC48 File Offset: 0x0005CE48
		public string GetCurrentTargetUserNickByContent(string userNick)
		{
			string html = this.GetHtml(userNick);
			HtmlNode htmlNode = HtmlNode.CreateNode("<div>" + html + "</div>");
			HtmlNode htmlNode2 = htmlNode.SelectSingleNode(string.Format("//*[@class=\"{0}\"]", "MsgHead"));
			string text;
			if (htmlNode2 == null)
			{
				text = "";
			}
			else
			{
				htmlNode2 = htmlNode2.SelectSingleNode(string.Format(".//*[@id=\"{0}\"]", "SenderName"));
				if (htmlNode2 == null)
				{
					text = "";
				}
				else
				{
					text = htmlNode2.InnerText.Trim();
				}
			}
			return text;
		}
	}
}
