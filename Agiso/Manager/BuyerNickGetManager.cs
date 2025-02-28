using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using Agiso.Utils;

namespace Agiso.Manager
{
	// Token: 0x020000E3 RID: 227
	public class BuyerNickGetManager
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x00004363 File Offset: 0x00002563
		public BuyerNickGetManager(string buyerNick, string aldsOpenUid)
		{
			this.a = buyerNick;
			this.b = aldsOpenUid;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00048704 File Offset: 0x00046904
		private static HttpClient HttpClient
		{
			get
			{
				if (BuyerNickGetManager.c == null)
				{
					BuyerNickGetManager.c = new HttpClient
					{
						BaseAddress = new Uri("https://amos.alicdn.com")
					};
					BuyerNickGetManager.c.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Aef/6.00 Qianniu/9.10.00N Safari/537.36");
					BuyerNickGetManager.c.DefaultRequestHeaders.Add("referer", "https://qn.taobao.com/");
					BuyerNickGetManager.c.Timeout = TimeSpan.FromSeconds(5000.0);
				}
				return BuyerNickGetManager.c;
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0004878C File Offset: 0x0004698C
		public string GetBuyerNick(out string errorMsg, int retryTime = 3)
		{
			errorMsg = "";
			string text = "";
			string text2;
			try
			{
				if (!string.IsNullOrEmpty(this.a) && !this.a.Contains("**"))
				{
					text2 = this.a;
				}
				else if (string.IsNullOrEmpty(this.b))
				{
					string text3;
					errorMsg = (text3 = "buyerNick包含*并且buyerOpenUid也为空");
					text = text3;
					text2 = "";
				}
				else
				{
					string buyerNickByAldsOpenUid = AppConfig.BuyerInfoCache.GetBuyerNickByAldsOpenUid(this.b);
					if (!string.IsNullOrEmpty(buyerNickByAldsOpenUid))
					{
						text2 = buyerNickByAldsOpenUid;
					}
					else
					{
						while (retryTime-- > 0)
						{
							try
							{
								int num = new Random().Next(300, 1000);
								int num2 = new Random().Next(100, 800);
								string text4 = string.Format("/getRealCid.aw?fromurl=localhost/aldsTb/&toId=cntaobao{0}&siteid=cntaobao&status=2&charset=utf-8&clientX={1}&clientY={2}&fromId=cntaobao&scene=&toRole=&source=light&client=false&encryptToUid={3}&bizType=1&bizDomain=taobao&pageSource=&appKey=12292026&sceneParam=%7B%22toRole%22%3A%22buyer%22%EF%BC%8C%22bizRef%22%3A%20%22%22%7D", new object[]
								{
									HttpUtility.UrlEncode(this.a),
									num,
									num2,
									this.b
								});
								HttpResponseMessage result = BuyerNickGetManager.HttpClient.GetAsync(text4).Result;
								if (!result.IsSuccessStatusCode)
								{
									errorMsg = string.Format("获取BuyerNick失败，{0}-{1}", result.StatusCode, result.ReasonPhrase);
									text = string.Format("获取BuyerNick失败，Url：http://amos.alicdn.com{0}，{1}-{2}", text4, result.StatusCode, result.ReasonPhrase);
									this.a();
									Thread.Sleep(300);
								}
								else
								{
									string result2 = result.Content.ReadAsStringAsync().Result;
									string text5;
									if (result2 != null)
									{
										if ((text5 = result2.Trim(new char[] { '\r', '\n' })) != null)
										{
											goto IL_019E;
										}
									}
									text5 = "";
									IL_019E:
									string text6 = text5;
									Match match = BuyerNickGetManager.e.Match(text6);
									if (!match.Success)
									{
										errorMsg = "获取BuyerNick失败，返回的数据无法匹配";
										text = "获取BuyerNick失败，返回的数据无法匹配，Url：http://amos.alicdn.com" + text4 + "，返回数据：" + text6;
										this.a();
										Thread.Sleep(300);
									}
									else
									{
										Interlocked.Exchange(ref BuyerNickGetManager.d, 0);
										string text7 = match.Groups["uid"].Value.Replace("cntaobao", "");
										if (string.IsNullOrEmpty(text7))
										{
											errorMsg = "获取的买家昵称为空";
											text = "获取的买家昵称为空，Url：http://amos.alicdn.com" + text4 + "，返回数据：" + text6;
											break;
										}
										if (text7.Contains("**"))
										{
											errorMsg = "获取的买家昵称已是脱敏";
											text = "获取的买家昵称已是脱敏，Url：http://amos.alicdn.com" + text4 + "，返回数据：" + text6;
											break;
										}
										errorMsg = "";
										text = "";
										AppConfig.BuyerInfoCache.UpdateAldsOpenUid(text7, this.b);
										return text7;
									}
								}
							}
							catch (Exception ex)
							{
								errorMsg = "获取BuyerNick出现异常了";
								text = string.Format("获取BuyerNick出现异常了，异常信息：{0}", ex);
								Thread.Sleep(300);
							}
						}
						text2 = null;
					}
				}
			}
			finally
			{
				if (!string.IsNullOrEmpty(text))
				{
					LogWriter.WriteLog(text, 1);
				}
			}
			return text2;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00048ABC File Offset: 0x00046CBC
		private void a()
		{
			Interlocked.Increment(ref BuyerNickGetManager.d);
			if (BuyerNickGetManager.d >= 10 * BuyerNickGetManager.f)
			{
				BuyerNickGetManager.f++;
				Interlocked.Exchange(ref BuyerNickGetManager.d, 0);
				LogWriter.WriteLog("连续获取BuyerNick失败次数达到10次，停止旺旺发送消息", 1);
				AppConfig.QnAgentServiceClient.AutoLoginError("agiso", null, "连续获取BuyerNick失败次数达到10次，停止旺旺发送消息，请关注服务器！", 268435455L, "", "");
			}
		}

		// Token: 0x040004C7 RID: 1223
		private string a;

		// Token: 0x040004C8 RID: 1224
		private string b;

		// Token: 0x040004C9 RID: 1225
		private static HttpClient c;

		// Token: 0x040004CA RID: 1226
		private static int d = 0;

		// Token: 0x040004CB RID: 1227
		private static Regex e = new Regex("realcid='(?<uid>.+)';encryptUid='(?<openUid>.+)'");

		// Token: 0x040004CC RID: 1228
		private static int f = 1;
	}
}
