using System;
using System.Collections.Generic;
using System.Linq;
using Agiso.AliwwApi.Object;
using Agiso.Extensions;
using Agiso.Utils;
using Agiso.Windows;
using AliwwClient.Cache;
using AliwwClient.WebSocketServer.Extensions;
using HtmlAgilityPack;

namespace Agiso.AliwwApi.Qn
{
	// Token: 0x02000758 RID: 1880
	public class WinChromeRecent : WinChromeContainer, IWinHtmlRecentContainer, IWinHtmlContainer
	{
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x0006D3A0 File Offset: 0x0006B5A0
		public List<string> UrlContainList
		{
			get
			{
				return new List<string> { "/recent.html", "/recent606.html" };
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x0006D3CC File Offset: 0x0006B5CC
		public string UrlNotContain
		{
			get
			{
				return "type=1&";
			}
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x0006D3E4 File Offset: 0x0006B5E4
		public static WinChromeRecent Get(IntPtr parent)
		{
			WindowInfo windowInfo = WinChromeRecent.FindAefWin(parent);
			return windowInfo.Convert<WinChromeRecent>();
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x0006D400 File Offset: 0x0006B600
		public string GetInnerText(string userNick)
		{
			string html = this.GetHtml(userNick);
			HtmlDocument doc = HtmlAgilityPackHelper.GetDoc(html);
			string text;
			if (doc == null)
			{
				text = null;
			}
			else
			{
				HtmlNode elementbyId = doc.GetElementbyId("J_Recent");
				if (elementbyId == null)
				{
					text = "";
				}
				else
				{
					text = elementbyId.InnerText;
				}
			}
			return text;
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x0006D450 File Offset: 0x0006B650
		public string GetHtml(string userNick)
		{
			UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(userNick);
			string text = base.GetHtmlAuto(userCacheOrCreate, "/recent.html", this.UrlNotContain);
			if (string.IsNullOrEmpty(text))
			{
				text = base.GetHtmlAuto(userCacheOrCreate, "/recent606.html", this.UrlNotContain);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = base.GetHtml("alires:///offlinepkg/ww/recent/src/index.html", null);
			}
			return text ?? "";
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x0006D4B8 File Offset: 0x0006B6B8
		public AliwwMsgElement GetLastReceiveMessage(string userNick)
		{
			string html = this.GetHtml(userNick);
			HtmlDocument doc = HtmlAgilityPackHelper.GetDoc(html);
			AliwwMsgElement aliwwMsgElement;
			if (doc == null)
			{
				aliwwMsgElement = null;
			}
			else
			{
				AliwwMsgElement aliwwMsgElement2;
				if ((aliwwMsgElement2 = this.b(doc, "")) == null)
				{
					aliwwMsgElement2 = this.a(doc) ?? this.b(doc);
				}
				AliwwMsgElement aliwwMsgElement3 = aliwwMsgElement2;
				aliwwMsgElement = aliwwMsgElement3;
			}
			return aliwwMsgElement;
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x0006D508 File Offset: 0x0006B708
		public AliwwMsgElement GetRealLastRcvMsg(string userNick, bool getSellerLastMsg)
		{
			string html = this.GetHtml(userNick);
			HtmlDocument doc = HtmlAgilityPackHelper.GetDoc(html);
			AliwwMsgElement aliwwMsgElement;
			if (doc == null)
			{
				aliwwMsgElement = null;
			}
			else
			{
				AliwwMsgElement aliwwMsgElement2;
				if ((aliwwMsgElement2 = this.b(doc, getSellerLastMsg ? userNick : "")) == null)
				{
					aliwwMsgElement2 = this.a(doc, getSellerLastMsg ? userNick : "");
				}
				AliwwMsgElement aliwwMsgElement3 = aliwwMsgElement2;
				aliwwMsgElement = aliwwMsgElement3;
			}
			return aliwwMsgElement;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x0006D560 File Offset: 0x0006B760
		private AliwwMsgElement b(HtmlDocument A_0, string A_1 = "")
		{
			HtmlNode elementbyId = A_0.GetElementbyId("J_msg_list");
			AliwwMsgElement aliwwMsgElement;
			if (elementbyId == null)
			{
				aliwwMsgElement = null;
			}
			else
			{
				HtmlNode htmlNode = elementbyId.SelectSingleNode("./div[contains(@class,'J_msg ')]" + ((!string.IsNullOrEmpty(A_1)) ? ("[@data-fromnick='" + A_1 + "']") : "") + "[last()]");
				if (htmlNode == null)
				{
					aliwwMsgElement = null;
				}
				else
				{
					HtmlNode nextSibling = htmlNode.NextSibling;
					HtmlNode htmlNode2 = ((nextSibling != null) ? nextSibling.SelectSingleNode("./div[contains(@class, 'dx-system')]") : null);
					if (htmlNode2 != null && htmlNode2.InnerText.Contains("账号禁言"))
					{
						AliwwMsgElement aliwwMsgElement2 = new AliwwMsgElement
						{
							IsSysMsg = true
						};
						aliwwMsgElement2.MsgContent = new AliwwMsgElementMsgContent();
						aliwwMsgElement2.MsgContent.AppendContentText(htmlNode2.InnerText);
						aliwwMsgElement2.MsgContent.AppendContentHtml(htmlNode2.InnerHtml);
						aliwwMsgElement = aliwwMsgElement2;
					}
					else
					{
						AliwwMsgElement aliwwMsgElement3 = new AliwwMsgElement();
						aliwwMsgElement3.MsgHead = new AliwwMsgElementMsgHead();
						aliwwMsgElement3.MsgContent = new AliwwMsgElementMsgContent();
						if (htmlNode.Attributes.Contains("id"))
						{
							AliwwMsgElement aliwwMsgElement4 = aliwwMsgElement3;
							string value = htmlNode.Attributes["id"].Value;
							aliwwMsgElement4.MsgId = Util.ToLong((value != null) ? value.Replace(".PNM", "") : null);
						}
						if (htmlNode.Attributes.Contains("class") && htmlNode.Attributes["class"].Value.Contains("imui-msg-system"))
						{
							aliwwMsgElement3.IsSysMsg = true;
							this.a(htmlNode.InnerText, true, ref aliwwMsgElement3);
						}
						else
						{
							if (htmlNode.Attributes.Contains("data-fromnick"))
							{
								aliwwMsgElement3.MsgHead.SenderName = htmlNode.Attributes["data-fromnick"].Value;
							}
							aliwwMsgElement3.MsgHead.SenderSite = "cntaobao";
							aliwwMsgElement3.MsgSendId = aliwwMsgElement3.MsgHead.SenderSite + aliwwMsgElement3.MsgHead.SenderName;
							if (htmlNode.Attributes.Contains("data-sendtime"))
							{
								aliwwMsgElement3.MsgHead.MsgTime = Util.TimeSpanToDateTime(htmlNode.Attributes["data-sendtime"].Value);
							}
							HtmlNode htmlNode3 = htmlNode.SelectSingleNode("./div[@class='imui-msg-content']");
							if (htmlNode3 != null)
							{
								HtmlNode htmlNode4 = htmlNode3.SelectSingleNode(".//div[@class='msg-body-text']");
								if (htmlNode4 != null)
								{
									aliwwMsgElement3.MsgContent.AppendContentHtml(htmlNode4.InnerHtml);
									aliwwMsgElement3.MsgContent.AppendContentText(htmlNode4.InnerText);
								}
								this.a(htmlNode3, aliwwMsgElement3);
								HtmlNode htmlNode5 = htmlNode3.SelectSingleNode("./div[contains(@class, 'imui-msg-system')]");
								if (htmlNode5 != null)
								{
									aliwwMsgElement3.IsSysMsg = true;
									this.a(htmlNode.InnerText, true, ref aliwwMsgElement3);
								}
							}
						}
						aliwwMsgElement = aliwwMsgElement3;
					}
				}
			}
			return aliwwMsgElement;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x0006D82C File Offset: 0x0006BA2C
		private void a(HtmlNode A_0, AliwwMsgElement A_1)
		{
			HtmlNode htmlNode = null;
			HtmlNodeCollection htmlNodeCollection = A_0.SelectNodes(".//span[@class='status-read-done']");
			if (!Util.IsEmptyList<HtmlNode>(htmlNodeCollection))
			{
				HtmlNode htmlNode2;
				if ((htmlNode2 = htmlNodeCollection.FirstOrDefault(new Func<HtmlNode, bool>(WinChromeRecent.<>c.<>9.b))) == null)
				{
					htmlNode2 = htmlNodeCollection.FirstOrDefault(new Func<HtmlNode, bool>(WinChromeRecent.<>c.<>9.a));
				}
				htmlNode = htmlNode2;
			}
			if (htmlNode == null)
			{
				htmlNode = A_0.SelectSingleNode(".//div[@class='imui-msg-op-wrap']/div[@class='imui-msg-status']");
			}
			if (htmlNode != null && !string.IsNullOrEmpty(htmlNode.InnerHtml))
			{
				if (htmlNode.InnerHtml.Contains("已读"))
				{
					A_1.MsgType = MsgType.已读;
				}
				else if (htmlNode.InnerHtml.Contains("未读"))
				{
					A_1.MsgType = MsgType.未读;
				}
				else
				{
					HtmlNode htmlNode3 = htmlNode.SelectSingleNode(".//span[contains(@class, 'status-error')]");
					if (htmlNode3 != null)
					{
						A_1.MsgType = MsgType.小红点;
					}
				}
			}
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x0006D920 File Offset: 0x0006BB20
		private AliwwMsgElement b(HtmlDocument A_0)
		{
			HtmlNode elementbyId = A_0.GetElementbyId("J_Recent");
			AliwwMsgElement aliwwMsgElement;
			if (elementbyId == null)
			{
				aliwwMsgElement = null;
			}
			else
			{
				HtmlNode htmlNode = elementbyId.SelectSingleNode(string.Format(".//div[contains(@class,'{0}')][last()]", " chat-msg "));
				if (htmlNode == null)
				{
					aliwwMsgElement = null;
				}
				else
				{
					htmlNode.SelectSingleNode(string.Format(".//*[contains(@class,'{0}')]", "sender-name"));
					htmlNode.SelectSingleNode(string.Format(".//*[contains(@class,'{0}')]", "time"));
					HtmlNode htmlNode2 = htmlNode.SelectSingleNode(string.Format(".//*[contains(@class,'{0}')]", "J_MsgBody"));
					AliwwMsgElement aliwwMsgElement2 = new AliwwMsgElement();
					aliwwMsgElement2.MsgHead = new AliwwMsgElementMsgHead();
					aliwwMsgElement2.MsgContent = new AliwwMsgElementMsgContent();
					this.a(htmlNode, ref aliwwMsgElement2);
					this.b(htmlNode2, ref aliwwMsgElement2);
					aliwwMsgElement = aliwwMsgElement2;
				}
			}
			return aliwwMsgElement;
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x0006D9F0 File Offset: 0x0006BBF0
		private AliwwMsgElement a(HtmlDocument A_0)
		{
			HtmlNode elementbyId = A_0.GetElementbyId("J_msgContainer");
			AliwwMsgElement aliwwMsgElement;
			if (elementbyId == null)
			{
				aliwwMsgElement = null;
			}
			else
			{
				HtmlNode htmlNode = elementbyId.SelectSingleNode("//div[contains(@class, 'imui-msg-content')]/div[contains(@class, 'imui-msg-content-inner')]/div[contains(@class,'imui-msg-head')]");
				if (htmlNode == null)
				{
					aliwwMsgElement = null;
				}
				else
				{
					AliwwMsgElement aliwwMsgElement2 = new AliwwMsgElement();
					aliwwMsgElement2.MsgHead = new AliwwMsgElementMsgHead();
					aliwwMsgElement2.MsgContent = new AliwwMsgElementMsgContent();
					HtmlNode htmlNode2 = htmlNode.SelectSingleNode("//span[starts-with(@class,'imui-msg-sender')]");
					if (htmlNode2 == null)
					{
						aliwwMsgElement = null;
					}
					else
					{
						aliwwMsgElement2.MsgHead.SenderName = htmlNode2.InnerText.Trim();
						aliwwMsgElement2.MsgHead.SenderSite = "cntaobao";
						aliwwMsgElement2.MsgSendId = aliwwMsgElement2.MsgHead.SenderSite + aliwwMsgElement2.MsgHead.SenderName;
						HtmlNode htmlNode3 = htmlNode.SelectSingleNode("//span[contains(@class, 'imui-msg-date')]");
						if (htmlNode3 != null)
						{
							DateTime? dateTime = Util.ToDateTime(htmlNode3.InnerText.Trim());
							aliwwMsgElement2.MsgHead.MsgTime = ((dateTime != null) ? dateTime.Value : DateTime.MaxValue);
						}
						HtmlNode htmlNode4 = elementbyId.SelectSingleNode("//div[contains(@class, 'msg-content-body')]/div[contains(@class, 'msg-body-html')]");
						if (htmlNode4 != null)
						{
							aliwwMsgElement2.MsgContent.AppendContentHtml(htmlNode4.InnerHtml);
							aliwwMsgElement2.MsgContent.AppendContentText(htmlNode4.InnerText);
						}
						aliwwMsgElement = aliwwMsgElement2;
					}
				}
			}
			return aliwwMsgElement;
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x0006DB3C File Offset: 0x0006BD3C
		public AliwwMsgElement GetQnLastReceiveMessage(string userNick)
		{
			UserCache userCacheOrCreate = AppConfig.GetUserCacheOrCreate(userNick);
			string text = ((userCacheOrCreate != null) ? userCacheOrCreate.RecentSession.GetHtml() : null);
			AliwwMsgElement aliwwMsgElement;
			if (string.IsNullOrEmpty(text))
			{
				aliwwMsgElement = null;
			}
			else
			{
				HtmlDocument doc = HtmlAgilityPackHelper.GetDoc(text);
				aliwwMsgElement = this.b(doc, "");
			}
			return aliwwMsgElement;
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x0006DB88 File Offset: 0x0006BD88
		private AliwwMsgElement a(HtmlDocument A_0, string A_1 = "")
		{
			HtmlNode elementbyId = A_0.GetElementbyId("J_msgWrap");
			AliwwMsgElement aliwwMsgElement;
			if (elementbyId == null)
			{
				aliwwMsgElement = null;
			}
			else
			{
				HtmlNode htmlNode = elementbyId.SelectSingleNode(".//div[contains(@class,'J_msg ')]" + ((!string.IsNullOrEmpty(A_1)) ? ("[@data-nick='" + A_1 + "']") : "") + "[last()]");
				if (htmlNode == null)
				{
					aliwwMsgElement = null;
				}
				else
				{
					AliwwMsgElement aliwwMsgElement2 = new AliwwMsgElement();
					aliwwMsgElement2.MsgHead = new AliwwMsgElementMsgHead();
					aliwwMsgElement2.MsgContent = new AliwwMsgElementMsgContent();
					if (htmlNode.Attributes.Contains("data-nick"))
					{
						aliwwMsgElement2.MsgHead.SenderName = htmlNode.Attributes["data-nick"].Value;
					}
					else if (htmlNode.Attributes.Contains("class") && htmlNode.Attributes["class"].Value.Contains("imui-msg-system"))
					{
						aliwwMsgElement2.IsSysMsg = true;
					}
					if (htmlNode.Attributes.Contains("data-appkey"))
					{
						aliwwMsgElement2.MsgHead.SenderSite = htmlNode.Attributes["data-appkey"].Value;
					}
					aliwwMsgElement2.MsgSendId = aliwwMsgElement2.MsgHead.SenderSite + aliwwMsgElement2.MsgHead.SenderName;
					if (htmlNode.Attributes.Contains("data-time"))
					{
						aliwwMsgElement2.MsgHead.MsgTime = Util.TimeSpanToDateTime(htmlNode.Attributes["data-time"].Value);
					}
					HtmlNode htmlNode2 = htmlNode.SelectSingleNode(string.Format(".//*[contains(@class,'{0}')]", "msg-body-html"));
					if (htmlNode2 != null)
					{
						aliwwMsgElement2.MsgContent.AppendContentHtml(htmlNode2.InnerHtml);
						aliwwMsgElement2.MsgContent.AppendContentText(htmlNode2.InnerText);
					}
					HtmlNode htmlNode3 = htmlNode.SelectSingleNode(".//div[@class='imui-msg-op-wrap']/div[@class='imui-msg-status']");
					if (htmlNode3 != null && !string.IsNullOrEmpty(htmlNode3.InnerHtml))
					{
						if (htmlNode3.InnerHtml.Contains("已读"))
						{
							aliwwMsgElement2.MsgType = MsgType.已读;
						}
						if (htmlNode3.InnerHtml.Contains("未读"))
						{
							aliwwMsgElement2.MsgType = MsgType.未读;
						}
					}
					if (aliwwMsgElement2.IsSysMsg)
					{
						this.a(htmlNode.InnerText, htmlNode2 == null, ref aliwwMsgElement2);
					}
					aliwwMsgElement = aliwwMsgElement2;
				}
			}
			return aliwwMsgElement;
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x0006DDD4 File Offset: 0x0006BFD4
		private void a(string A_0, bool A_1, ref AliwwMsgElement A_2)
		{
			if (A_0.Contains("给您发送了一个震屏"))
			{
				A_2.MsgHead.SenderName = A_0.Replace("给您发送了一个震屏", "");
				if (A_1)
				{
					A_2.MsgContent.AppendContentHtml("给您发送了一个震屏");
					A_2.MsgContent.AppendContentText("给您发送了一个震屏");
				}
			}
			else if (A_0.Contains("给你发送了一个振屏"))
			{
				A_2.MsgHead.SenderName = A_0.Replace("给你发送了一个振屏", "");
				if (A_1)
				{
					A_2.MsgContent.AppendContentHtml("给你发送了一个振屏");
					A_2.MsgContent.AppendContentText("给你发送了一个振屏");
				}
			}
			else if (A_0.Contains("撤回了一条消息"))
			{
				A_2.MsgHead.SenderName = A_0.Replace("撤回了一条消息", "");
				if (A_1)
				{
					A_2.MsgContent.AppendContentHtml("撤回了一条消息");
					A_2.MsgContent.AppendContentText("撤回了一条消息");
				}
			}
			else
			{
				A_2.MsgContent.AppendContentHtml(A_0);
				A_2.MsgContent.AppendContentText(A_0);
			}
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x0006DF00 File Offset: 0x0006C100
		private void b(HtmlNode A_0, ref AliwwMsgElement A_1)
		{
			if (A_0 != null)
			{
				HtmlNode htmlNode = A_0.SelectSingleNode(string.Format(".//*[contains(@class,'{0}')]", "J_MsgContent"));
				if (htmlNode != null)
				{
					if (A_1.MsgHead.SenderName == null && (htmlNode.Attributes.Contains("data-sendername") && htmlNode.Attributes["data-sendername"].Value != null))
					{
						A_1.MsgHead.SenderName = htmlNode.Attributes["data-sendername"].Value.Trim();
					}
					if (A_1.MsgHead.MsgTime < new DateTime(2010, 1, 1) && (htmlNode.Attributes.Contains("data-time") && htmlNode.Attributes["data-time"].Value != null))
					{
						DateTime? dateTime = Util.ToDateTime(htmlNode.Attributes["data-time"].Value.Trim());
						A_1.MsgHead.MsgTime = ((dateTime != null) ? dateTime.Value : DateTime.MaxValue);
					}
					if (A_1.MsgSendId == null && (htmlNode.Attributes.Contains("data-id") && htmlNode.Attributes["data-id"].Value != null))
					{
						A_1.MsgSendId = htmlNode.Attributes["data-id"].Value.Trim();
					}
					if (A_1.MsgContent.ContentHtml == null)
					{
						A_1.MsgContent.AppendContentHtml(htmlNode.InnerHtml);
						A_1.MsgContent.AppendContentText(htmlNode.InnerText);
					}
				}
			}
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0006E0CC File Offset: 0x0006C2CC
		private void a(HtmlNode A_0, ref AliwwMsgElement A_1)
		{
			if (A_0 != null)
			{
				HtmlNode htmlNode = A_0.SelectSingleNode(string.Format(".//*[contains(@class,'{0}')]", "sender-name"));
				HtmlNode htmlNode2 = A_0.SelectSingleNode(string.Format(".//*[contains(@class,'{0}')]", "time"));
				HtmlNode htmlNode3 = A_0.SelectSingleNode(string.Format(".//*[contains(@class,'{0}')]", "J_MsgBody"));
				if (htmlNode == null)
				{
					this.a(A_0.PreviousSibling, ref A_1);
				}
				else
				{
					A_1.MsgHead.SenderName = htmlNode.InnerText.Trim();
					if (htmlNode.Attributes.Contains("data-senderid") && htmlNode.Attributes["data-senderid"].Value != null)
					{
						A_1.MsgSendId = htmlNode.Attributes["data-senderid"].Value.Trim();
					}
					else
					{
						A_1.MsgSendId = "cntaobao" + A_1.MsgHead.SenderName;
					}
				}
				if (htmlNode2 != null)
				{
					string text = htmlNode2.InnerText.Trim().Replace("(", "").Replace(")", "");
					if (text.IndexOf("-") < 0 && text.IndexOf("/") < 0 && text.IndexOf("月") < 0)
					{
						text = DateTime.Today.ToString("yyyy-MM-dd") + " " + text;
					}
					if (text.EndsWith(":") || text.EndsWith("："))
					{
						text = text.Substring(0, text.Length - 1);
					}
					DateTime? dateTime = Util.ToDateTime(text);
					A_1.MsgHead.MsgTime = ((dateTime != null) ? dateTime.Value : DateTime.MinValue);
				}
				if (htmlNode3 != null)
				{
					A_1.MsgContent.AppendContentText(htmlNode3.InnerText.Trim());
					A_1.MsgContent.AppendContentHtml(htmlNode3.InnerHtml.Trim());
				}
			}
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x0006E2F0 File Offset: 0x0006C4F0
		public string GetCurrentTargetUserNickByContent(string userNick)
		{
			AliwwMsgElement lastReceiveMessage = this.GetLastReceiveMessage(userNick);
			string text;
			if (lastReceiveMessage != null)
			{
				text = lastReceiveMessage.SenderNick;
			}
			else
			{
				string html = this.GetHtml(userNick);
				HtmlDocument doc = HtmlAgilityPackHelper.GetDoc(html);
				if (doc == null)
				{
					text = null;
				}
				else
				{
					HtmlNode elementbyId = doc.GetElementbyId("J_Body");
					if (elementbyId == null)
					{
						text = null;
					}
					else
					{
						HtmlNode htmlNode = elementbyId.SelectSingleNode(string.Format(".//div[contains(@class,'{0}') and not(contains(@class,'{1}'))][last()]", " chat-msg ", "msg-from-self"));
						if (htmlNode == null)
						{
							text = null;
						}
						else
						{
							HtmlNode htmlNode2 = htmlNode.SelectSingleNode(string.Format(".//*[contains(@class,'{0}')]", "sender-name"));
							if (htmlNode2 == null)
							{
								text = "";
							}
							else
							{
								text = htmlNode2.InnerText.Trim();
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x0006E3B4 File Offset: 0x0006C5B4
		public static WindowInfo FindAefWin(IntPtr parent)
		{
			WindowInfo windowInfo = new WindowInfo(parent);
			WindowInfo windowInfo2 = windowInfo.FindWindowInDescendant("Aef_RenderWidgetHostHWND", null, false, new bool?(false));
			if (windowInfo2 == null)
			{
				windowInfo2 = windowInfo.FindWindowInDescendant("CefBrowserWindow", null, false, new bool?(false));
			}
			if (windowInfo2 == null)
			{
				windowInfo2 = windowInfo.FindWindowInDescendant("Chrome_RenderWidgetHostHWND", null, false, new bool?(false));
			}
			if (windowInfo2 == null)
			{
				windowInfo2 = windowInfo.FindWindowInDescendant("Aef_WidgetWin_0", null, false, new bool?(false));
			}
			return windowInfo2;
		}
	}
}
