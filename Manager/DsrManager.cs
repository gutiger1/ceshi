using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Agiso.Utils;
using Agiso.WwService.Sdk.Domain;
using HtmlAgilityPack;

namespace AliwwClient.Manager
{
	// Token: 0x020000AA RID: 170
	public class DsrManager
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0003DE14 File Offset: 0x0003C014
		public string SellerNick
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0003DE2C File Offset: 0x0003C02C
		public long NumIid
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0003DE44 File Offset: 0x0003C044
		public string Title
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0003DE5C File Offset: 0x0003C05C
		public string SellerType
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0003DE74 File Offset: 0x0003C074
		public string DsrUrl
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0003DE8C File Offset: 0x0003C08C
		public long ShopId
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00003C48 File Offset: 0x00001E48
		public DsrManager(string sellerNick, long numIid, string title, string dsrUrl, string sellerType, long shopId)
		{
			this.a = sellerNick;
			this.b = numIid;
			this.c = title;
			this.e = dsrUrl;
			this.d = sellerType;
			this.f = shopId;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0003DEA4 File Offset: 0x0003C0A4
		public DsrManager(AgisoDsrTask dsrTask)
		{
			this.a = dsrTask.SellerNick;
			this.b = dsrTask.DsrNumIid;
			this.c = dsrTask.DsrTitle;
			this.e = dsrTask.DsrUrl;
			this.d = dsrTask.SellerType;
			this.f = dsrTask.ShopId;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0003DF00 File Offset: 0x0003C100
		public bool Start(out RateStatisticalDayReport report, out string errMsg)
		{
			DateTime dateTime = DateTime.Today.AddDays(-1.0);
			DateTime today = DateTime.Today;
			report = new RateStatisticalDayReport
			{
				Days = dateTime,
				SellerNick = this.a
			};
			errMsg = "";
			bool flag;
			try
			{
				if (string.IsNullOrEmpty(this.e))
				{
					this.GetRateUrl(out errMsg);
				}
				if (string.IsNullOrEmpty(this.e))
				{
					flag = false;
				}
				else
				{
					string text = this.DoGet(this.e ?? "", this.GetRateReferer(), 2000);
					flag = this.HandlingRateHtml(text, out report, out errMsg);
				}
			}
			catch (Exception ex)
			{
				errMsg = ex.ToString();
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0003DFC4 File Offset: 0x0003C1C4
		public bool HandlingRateHtml(string rateHtml, out RateStatisticalDayReport report, out string errMsg)
		{
			report = new RateStatisticalDayReport
			{
				SellerNick = this.a,
				Days = DateTime.Today.AddDays(-1.0)
			};
			errMsg = "";
			try
			{
				HtmlDocument htmlDocument = new HtmlDocument();
				htmlDocument.LoadHtml(rateHtml);
				HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//ul[@id='dsr']/li[contains(@class,'dsr-item')]");
				if (htmlNodeCollection != null)
				{
					this.a(htmlNodeCollection, ref report);
					this.b(htmlDocument, ref report);
					this.a(htmlDocument, ref report);
					return true;
				}
				if (rateHtml.Contains("该店铺尚未收到评价"))
				{
					return true;
				}
				errMsg = this.a + "，动态评分页面的样式已变化，请注意";
				LogWriter.WriteLog(rateHtml, 1);
				return false;
			}
			catch (Exception ex)
			{
				errMsg = ex.ToString();
			}
			return false;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0003E0A0 File Offset: 0x0003C2A0
		public bool HandlingItemHtml(string itemHtml, out string errMsg)
		{
			try
			{
				if (itemHtml.Contains("您查看的宝贝不存在，可能已下架或者被转移"))
				{
					errMsg = "您查看的宝贝不存在，可能已下架或者被转移";
					return false;
				}
				List<string> list = new List<string>();
				Regex regex = new Regex("rate.taobao.com/user-rate-(.*?)[\\.\\?\"]", RegexOptions.IgnoreCase | RegexOptions.Multiline);
				if (regex.IsMatch(itemHtml))
				{
					MatchCollection matchCollection = regex.Matches(itemHtml);
					foreach (object obj in matchCollection)
					{
						Match match = (Match)obj;
						string value = match.Groups[1].Value;
						if (!list.Contains(value))
						{
							list.Add(value);
						}
					}
					this.e = string.Join(";", list.ToArray());
					errMsg = "";
					return true;
				}
				errMsg = "宝贝的页面样式已经变更了，请注意";
				LogWriter.WriteLog(itemHtml, 1);
			}
			catch (Exception ex)
			{
				errMsg = ex.ToString();
			}
			return false;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0003E1B8 File Offset: 0x0003C3B8
		private void a(HtmlNodeCollection A_0, ref RateStatisticalDayReport A_1)
		{
			for (int i = 0; i < A_0.Count; i++)
			{
				HtmlNode htmlNode = A_0[i];
				HtmlNode htmlNode2 = htmlNode.SelectSingleNode("div[@class='item-scrib']");
				if (htmlNode2 != null)
				{
					HtmlNode htmlNode3 = htmlNode2.SelectSingleNode("em[contains(@class, 'count')]");
					HtmlNode htmlNode4 = htmlNode2.SelectSingleNode("em/strong[contains(@class, 'percent')]");
					string text;
					if (htmlNode4 != null && htmlNode4.Attributes["class"] != null)
					{
						if (htmlNode4.Attributes["class"].Value.Contains("lower"))
						{
							text = "-" + htmlNode4.InnerText;
						}
						else if (htmlNode4.Attributes["class"].Value.Contains("normal"))
						{
							text = "0%";
						}
						else
						{
							text = htmlNode4.InnerText;
						}
					}
					else
					{
						text = "0%";
					}
					switch (i)
					{
					case 0:
						A_1.ItemDescScore = ((htmlNode3 == null || htmlNode3.Attributes["title"] == null) ? 0m : Util.ToDecimal(htmlNode3.Attributes["title"].Value.Replace("分", "")));
						A_1.ItemDescScorePercent = text;
						break;
					case 1:
						A_1.SellerServiceScore = ((htmlNode3 == null || htmlNode3.Attributes["title"] == null) ? 0m : Util.ToDecimal(htmlNode3.Attributes["title"].Value.Replace("分", "")));
						A_1.SellerServiceScorePercent = text;
						break;
					case 2:
						A_1.LogisticsServiceScore = ((htmlNode3 == null || htmlNode3.Attributes["title"] == null) ? 0m : Util.ToDecimal(htmlNode3.Attributes["title"].Value.Replace("分", "")));
						A_1.LogisticsServiceScorePercent = text;
						break;
					}
				}
				HtmlNode htmlNode5 = htmlNode.SelectSingleNode("div[@class='dsr-info-box']/div[@class='box-wrap']");
				if (htmlNode5 != null)
				{
					HtmlNode htmlNode6 = htmlNode5.SelectSingleNode("div[@class='total']/span[last()]");
					HtmlNode htmlNode7 = htmlNode5.SelectSingleNode("div[contains(@class, 'count5')]/em[@class='h']");
					HtmlNode htmlNode8 = htmlNode5.SelectSingleNode("div[contains(@class, 'count4')]/em[@class='h']");
					HtmlNode htmlNode9 = htmlNode5.SelectSingleNode("div[contains(@class, 'count3')]/em[@class='h']");
					HtmlNode htmlNode10 = htmlNode5.SelectSingleNode("div[contains(@class, 'count2')]/em[@class='h']");
					HtmlNode htmlNode11 = htmlNode5.SelectSingleNode("div[contains(@class, 'count1')]/em[@class='h']");
					switch (i)
					{
					case 0:
						A_1.ItemDescScorePeople = ((htmlNode6 != null) ? Util.ToInt(htmlNode6.InnerText) : 0);
						A_1.ItemDescScoreCount5 = ((htmlNode7 != null) ? htmlNode7.InnerText : "0%");
						A_1.ItemDescScoreCount4 = ((htmlNode8 != null) ? htmlNode8.InnerText : "0%");
						A_1.ItemDescScoreCount3 = ((htmlNode9 != null) ? htmlNode9.InnerText : "0%");
						A_1.ItemDescScoreCount2 = ((htmlNode10 != null) ? htmlNode10.InnerText : "0%");
						A_1.ItemDescScoreCount1 = ((htmlNode11 != null) ? htmlNode11.InnerText : "0%");
						break;
					case 1:
						A_1.SellerServiceScorePeople = ((htmlNode6 != null) ? Util.ToInt(htmlNode6.InnerText) : 0);
						A_1.SellerServiceScoreCount5 = ((htmlNode7 != null) ? htmlNode7.InnerText : "0%");
						A_1.SellerServiceScoreCount4 = ((htmlNode8 != null) ? htmlNode8.InnerText : "0%");
						A_1.SellerServiceScoreCount3 = ((htmlNode9 != null) ? htmlNode9.InnerText : "0%");
						A_1.SellerServiceScoreCount2 = ((htmlNode10 != null) ? htmlNode10.InnerText : "0%");
						A_1.SellerServiceScoreCount1 = ((htmlNode11 != null) ? htmlNode11.InnerText : "0%");
						break;
					case 2:
						A_1.LogisticsServiceScorePeople = ((htmlNode6 != null) ? Util.ToInt(htmlNode6.InnerText) : 0);
						A_1.LogisticsServiceScoreCount5 = ((htmlNode7 != null) ? htmlNode7.InnerText : "0%");
						A_1.LogisticsServiceScoreCount4 = ((htmlNode8 != null) ? htmlNode8.InnerText : "0%");
						A_1.LogisticsServiceScoreCount3 = ((htmlNode9 != null) ? htmlNode9.InnerText : "0%");
						A_1.LogisticsServiceScoreCount2 = ((htmlNode10 != null) ? htmlNode10.InnerText : "0%");
						A_1.LogisticsServiceScoreCount1 = ((htmlNode11 != null) ? htmlNode11.InnerText : "0%");
						break;
					}
				}
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0003E65C File Offset: 0x0003C85C
		private void b(HtmlDocument A_0, ref RateStatisticalDayReport A_1)
		{
			HtmlNodeCollection htmlNodeCollection = A_0.DocumentNode.SelectNodes("//div[contains(@class, 'seller-rate-info')]/div[@id='J_show_list']/ul/li");
			if (htmlNodeCollection != null)
			{
				for (int i = 0; i < htmlNodeCollection.Count; i++)
				{
					HtmlNodeCollection htmlNodeCollection2 = htmlNodeCollection[i].SelectNodes("table/tbody/tr");
					if (htmlNodeCollection2 != null && htmlNodeCollection2.Count >= 2)
					{
						HtmlNode htmlNode = htmlNodeCollection2[1].SelectSingleNode("td[contains(@class,'rateok')]/a");
						HtmlNode htmlNode2 = htmlNodeCollection2[1].SelectSingleNode("td[contains(@class,'ratenormal')]/a");
						HtmlNode htmlNode3 = htmlNodeCollection2[1].SelectSingleNode("td[contains(@class,'ratebad')]/a");
						switch (i)
						{
						case 0:
							A_1.LastWeekGoodRateCount = ((htmlNode != null) ? Util.ToInt(htmlNode.InnerText) : 0);
							A_1.LastWeekNeutralRateCount = ((htmlNode2 != null) ? Util.ToInt(htmlNode2.InnerText) : 0);
							A_1.LastWeekBadRateCount = ((htmlNode3 != null) ? Util.ToInt(htmlNode3.InnerText) : 0);
							break;
						case 1:
							A_1.LastMonthGoodRateCount = ((htmlNode != null) ? Util.ToInt(htmlNode.InnerText) : 0);
							A_1.LastMonthNeutralRateCount = ((htmlNode2 != null) ? Util.ToInt(htmlNode2.InnerText) : 0);
							A_1.LastMonthBadRateCount = ((htmlNode3 != null) ? Util.ToInt(htmlNode3.InnerText) : 0);
							break;
						case 2:
							A_1.LastHalfYearGoodRateCount = ((htmlNode != null) ? Util.ToInt(htmlNode.InnerText) : 0);
							A_1.LastHalfYearNeutralRateCount = ((htmlNode2 != null) ? Util.ToInt(htmlNode2.InnerText) : 0);
							A_1.LastHalfYearBadRateCount = ((htmlNode3 != null) ? Util.ToInt(htmlNode3.InnerText) : 0);
							break;
						case 3:
							A_1.HalfYearAgoGoodRateCount = ((htmlNode != null) ? Util.ToInt(htmlNode.InnerText) : 0);
							A_1.HalfYearAgoNeutralRateCount = ((htmlNode2 != null) ? Util.ToInt(htmlNode2.InnerText) : 0);
							A_1.HalfYearAgoBadRateCount = ((htmlNode3 != null) ? Util.ToInt(htmlNode3.InnerText) : 0);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0003E868 File Offset: 0x0003CA68
		private void a(HtmlDocument A_0, ref RateStatisticalDayReport A_1)
		{
			HtmlNodeCollection htmlNodeCollection = A_0.DocumentNode.SelectNodes("//div[contains(@class,'J_TBR_MonthInfo_Summary')]/table[contains(@class, tb-rate-table)]\r\n            /tbody/tr[contains(@class, 'J_TBR_MonthInfo_SummaryTR')]");
			if (htmlNodeCollection != null)
			{
				string text = this.d.ToUpper();
				string text2 = text;
				if (!(text2 == "B"))
				{
					if (text2 == "C")
					{
						for (int i = 0; i < htmlNodeCollection.Count; i++)
						{
							HtmlNode htmlNode = htmlNodeCollection[i].SelectSingleNode("td[contains(@class, 'my-val')]");
							HtmlNode htmlNode2 = htmlNodeCollection[i].SelectSingleNode("td[contains(@class, 'ind-val')]");
							int num = i;
							int num2 = num;
							if (num2 != 0)
							{
								if (num2 == 1)
								{
									A_1.DisputeRefundRate = ((htmlNode != null) ? htmlNode.InnerText : "0%");
									A_1.DisputeRefundRateAvg = ((htmlNode2 != null) ? htmlNode2.InnerText : "0%");
								}
							}
							else
							{
								A_1.AfterSaleRate = ((htmlNode != null) ? htmlNode.InnerText : "0%");
								A_1.AfterSaleRateAvg = ((htmlNode2 != null) ? htmlNode2.InnerText : "0%");
							}
						}
					}
				}
				else
				{
					for (int j = 0; j < htmlNodeCollection.Count; j++)
					{
						HtmlNode htmlNode3 = htmlNodeCollection[j].SelectSingleNode("td[contains(@class, 'my-val')]");
						HtmlNode htmlNode4 = htmlNodeCollection[j].SelectSingleNode("td[contains(@class, 'ind-val')]");
						switch (j)
						{
						case 0:
							A_1.DisputeRefundRate = ((htmlNode3 != null) ? htmlNode3.InnerText : "0%");
							A_1.DisputeRefundRateAvg = ((htmlNode4 != null) ? htmlNode4.InnerText : "0%");
							break;
						case 1:
							A_1.RefundNotReturnAutoCompleteTime = ((htmlNode3 != null) ? htmlNode3.InnerText : "0天");
							A_1.RefundNotReturnAutoCompleteTimeAvg = ((htmlNode4 != null) ? htmlNode4.InnerText : "0天");
							break;
						case 2:
							A_1.RefundAndGoodsReturnAutoCompleteTime = ((htmlNode3 != null) ? htmlNode3.InnerText : "0天");
							A_1.RefundAndGoodsReturnAutoCompleteTimeAvg = ((htmlNode4 != null) ? htmlNode4.InnerText : "0天");
							break;
						case 3:
							A_1.SellerCompleteRefundRate = ((htmlNode3 != null) ? htmlNode3.InnerText : "0%");
							A_1.SellerCompleteRefundRateAvg = ((htmlNode4 != null) ? htmlNode4.InnerText : "0%");
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0003EAD0 File Offset: 0x0003CCD0
		public void GetRateUrl(out string errMsg)
		{
			errMsg = "";
			try
			{
				string text = string.Concat(new string[]
				{
					"https://s.taobao.com/search?q=",
					HttpUtility.UrlEncode(this.c, Encoding.UTF8).ToUpper(),
					"&imgfile=&js=1&stats_click=search_radio_all%3A1&initiative_id=staobaoz_",
					DateTime.Now.ToString("yyyyMMdd"),
					"&ie=utf8"
				});
				string text2 = this.d.ToUpper();
				string text3 = text2;
				string text4;
				if (!(text3 == "B"))
				{
					if (text3 == "C")
					{
					}
					text4 = string.Format("https://item.taobao.com/item.htm?spm=a230r.1.14.1.649e7babr7m9UV&id={0}&ns=1&abbucket=16#detail", this.b);
				}
				else
				{
					text4 = string.Format("https://detail.tmall.com/item.htm?spm=a230r.1.14.1.739461068hM34b&id={0}&cm_id=140105335569ed55e27b&abbucket=16", this.b);
				}
				string text5 = this.DoGet(text4, text, 2000);
				HtmlDocument htmlDocument = new HtmlDocument();
				htmlDocument.LoadHtml(text5);
				string text6 = this.d.ToUpper();
				string text7 = text6;
				if (!(text7 == "B"))
				{
					if (!(text7 == "C"))
					{
						errMsg = this.a + "，未知卖家类型";
					}
					else
					{
						HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='tb-shop-info-bd']/div[@class='tb-shop-rate']/dl/dd/a");
						HtmlNode htmlNode2 = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='J_Pine']");
						if (htmlNode2 != null && htmlNode2.Attributes["data-shopid"] != null)
						{
							this.f = Util.ToLong(htmlNode2.Attributes["data-shopid"].Value);
						}
						if (htmlNode == null)
						{
							HtmlNode htmlNode3 = htmlDocument.DocumentNode.SelectSingleNode("//div[contains(@class, 'tb-shop-info-bd')]/div[contains(@class, 'tb-shop-rate') and contains(@class, 'tb-shop-rate-empty')]");
							if (htmlNode3 == null)
							{
								errMsg = this.a + "，淘宝宝贝的页面样式已经变更了，请注意";
								LogWriter.WriteLog(text5, 1);
								if (this.f > 0L)
								{
									string text8 = this.DoGet(string.Format("https://shop{0}.taobao.com", this.f), string.Concat(new string[]
									{
										"https://shopsearch.taobao.com/search?app=shopsearch&q=",
										HttpUtility.UrlEncode(this.a),
										"&js=1&initiative_id=staobaoz_",
										DateTime.Today.ToString("yyyyMMdd"),
										"&ie=utf8"
									}), 1000);
									HtmlDocument htmlDocument2 = new HtmlDocument();
									htmlDocument2.LoadHtml(text8);
									HtmlNode htmlNode4 = htmlDocument2.DocumentNode.SelectSingleNode("//div[contains(@class, 'shop-dynamic-score')]/ul/li[1]/a");
									if (htmlNode4 != null)
									{
										errMsg = "";
										this.e = this.a(htmlNode4.Attributes["href"].Value);
									}
									else
									{
										errMsg = this.a + "，天猫店铺的页面样式已经变更了，请注意";
										LogWriter.WriteLog(text8, 1);
									}
								}
							}
						}
						else
						{
							string value = htmlNode.Attributes["href"].Value;
							this.e = this.a(value);
						}
					}
				}
				else
				{
					HtmlNode htmlNode5 = htmlDocument.DocumentNode.SelectSingleNode("//div[starts-with(@class, 'bd')]/div[contains(@class,'shop-rate')]/ul/li/a");
					HtmlNode htmlNode6 = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='LineZing']");
					if (htmlNode6 != null && htmlNode6.Attributes["shopid"] != null)
					{
						this.f = Util.ToLong(htmlNode6.Attributes["shopid"].Value);
					}
					if (htmlNode5 == null)
					{
						errMsg = this.a + "，天猫宝贝的页面样式已经变更了，请注意";
						LogWriter.WriteLog(text5, 1);
						if (this.f > 0L)
						{
							string text9 = this.DoGet(string.Format("https://shop{0}.taobao.com", this.f), string.Concat(new string[]
							{
								"https://shopsearch.taobao.com/search?app=shopsearch&q=",
								HttpUtility.UrlEncode(this.a),
								"&js=1&initiative_id=staobaoz_",
								DateTime.Today.ToString("yyyyMMdd"),
								"&ie=utf8"
							}), 1000);
							HtmlDocument htmlDocument3 = new HtmlDocument();
							htmlDocument3.LoadHtml(text9);
							HtmlNode htmlNode7 = htmlDocument3.DocumentNode.SelectSingleNode("//div[@id='dsr-ratelink']");
							if (htmlNode7 != null)
							{
								errMsg = "";
								this.e = this.a(htmlNode7.Attributes["value"].Value);
							}
							else
							{
								errMsg = this.a + "，天猫店铺的页面样式已经变更了，请注意";
								LogWriter.WriteLog(text9, 1);
							}
						}
					}
					else
					{
						string value2 = htmlNode5.Attributes["href"].Value;
						this.e = this.a(value2);
					}
				}
			}
			catch (Exception ex)
			{
				errMsg = ex.ToString();
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0003EFB0 File Offset: 0x0003D1B0
		private string a(string A_0)
		{
			string text;
			if (string.IsNullOrEmpty(A_0))
			{
				text = null;
			}
			else
			{
				text = "https:" + A_0;
			}
			return text;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0003EFD8 File Offset: 0x0003D1D8
		public string GetRateReferer()
		{
			string text = this.d.ToUpper();
			string text2 = text;
			string text3;
			if (!(text2 == "B"))
			{
				if (!(text2 == "C"))
				{
					text3 = "";
				}
				else
				{
					text3 = string.Format("https://item.taobao.com/item.htm?spm=a230r.1.14.22.7e124b67yc0Olc&id={0}&ns=1&abbucket=16", this.b);
				}
			}
			else
			{
				text3 = string.Format("https://detail.tmall.com/item.htm?spm=a230r.1.14.1.7e124b67yc0Olc&id={0}&cm_id=140105335569ed55e27b&abbucket=16", this.b);
			}
			return text3;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0003F048 File Offset: 0x0003D248
		public string DoGet(string url, string referer, int timeOutMinSecond = 1000)
		{
			HttpWebRequest webRequest = Util.GetWebRequest(url, "GET");
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
			if (!string.IsNullOrEmpty(referer))
			{
				webRequest.Referer = referer;
			}
			webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
			webRequest.Headers["cookie"] = string.Concat(new string[]
			{
				"cna=2YR5EjO8qA4CAa8rLK1KQP;sn=",
				Util.GetRandomValue<string>(new List<string>
				{
					"a", "b", "c", "d", "e", "f", "g", "h", "i", "j",
					"k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
					"u", "v", "w", "x", "y", "z"
				}),
				Util.GenerateRandomString(new Random().Next(5, 10)),
				"%3A",
				Util.GenerateRandomString(new Random().Next(3, 6)),
				"; enc=c0O4N6tVKkrPDDkyVIYWvk5FkzCscZZbpiDk6S1oZ4poo8h96zIuYZN3IO%2FQwiOt9agXzRrdwqES%2FnIVENOjKg%3D%3D; hng=CN%7Czh-CN%7CCNY%7C156; thw=cn; t=6a6957f8abe240535c612e7c7dd9c7af; _cc_=URm48syIZQ%3D%3D; tg=0; mt=ci=0_1&np=; l=AqSkFyEUfveg/JxCpRA7Nqu4/KiXS8in; uc3=id2=&nk2=&lg2=; tracknick=; cq=ccp%3D1; cookie2=1318f2da130b98d126eb502787b69bf3; v=0; _tb_token_=e31366e54e67d; _m_h5_tk=4dca2afd86c0913104a75e3e242adb57_1542603598293; _m_h5_tk_enc=c410890a052c1fe776283b7732ad2d4d; ali_ab=59.57.95.41.1541469642632.7; isg=BBQUw5bIMzUIdqfiFzvE5w9s61JGxTM8aogHPq71oB8imbTj1n0I58qYndGkenCv"
			});
			HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
			Encoding encoding = Encoding.GetEncoding(httpWebResponse.CharacterSet);
			return Util.GetResponseAsString(httpWebResponse, encoding);
		}

		// Token: 0x040003D5 RID: 981
		private string a;

		// Token: 0x040003D6 RID: 982
		private long b;

		// Token: 0x040003D7 RID: 983
		private string c;

		// Token: 0x040003D8 RID: 984
		private string d;

		// Token: 0x040003D9 RID: 985
		private string e;

		// Token: 0x040003DA RID: 986
		private long f;
	}
}
