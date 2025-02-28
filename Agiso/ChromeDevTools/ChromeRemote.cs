using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Agiso.ChromeDevTools
{
	// Token: 0x02000102 RID: 258
	public class ChromeRemote
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x00051EB8 File Offset: 0x000500B8
		private static a b<a>(string A_0)
		{
			return JsonConvert.DeserializeObject<a>(A_0);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00051ED0 File Offset: 0x000500D0
		private static a a<a>(Stream A_0)
		{
			string text = "";
			using (StreamReader streamReader = new StreamReader(A_0))
			{
				text = streamReader.ReadToEnd();
			}
			return ChromeRemote.b<a>(text);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00051F18 File Offset: 0x00050118
		public static bool ValidatePort(int port)
		{
			bool flag;
			if (port <= 0)
			{
				flag = false;
			}
			else
			{
				try
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format("http://127.0.0.1:{0}", port));
					httpWebRequest.Timeout = 1000;
					using (Stream responseStream = httpWebRequest.GetResponse().GetResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							string text = streamReader.ReadToEnd();
							if (!string.IsNullOrEmpty(text))
							{
								text = text.ToLower();
							}
							flag = text.Contains("inspectable");
						}
					}
				}
				catch (Exception)
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00051FE4 File Offset: 0x000501E4
		private static a a<a>(string A_0)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(A_0 + "/json");
			httpWebRequest.Timeout = 1000;
			a a;
			using (Stream responseStream = httpWebRequest.GetResponse().GetResponseStream())
			{
				using (StreamReader streamReader = new StreamReader(responseStream))
				{
					string text = streamReader.ReadToEnd();
					a = ChromeRemote.b<a>(text);
				}
			}
			return a;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0005206C File Offset: 0x0005026C
		private static List<RemoteSessionsResponse> a(string A_0)
		{
			List<RemoteSessionsResponse> list = ChromeRemote.a<List<RemoteSessionsResponse>>(A_0);
			List<RemoteSessionsResponse> list2;
			if (list == null)
			{
				list2 = null;
			}
			else
			{
				List<RemoteSessionsResponse> list3 = new List<RemoteSessionsResponse>();
				foreach (RemoteSessionsResponse remoteSessionsResponse in list)
				{
					if (remoteSessionsResponse.devtoolsFrontendUrl != null)
					{
						list3.Add(remoteSessionsResponse);
					}
				}
				list2 = list3;
			}
			return list2;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000520E4 File Offset: 0x000502E4
		public static RemoteSessionsResponse GetLocalAvailableSessions(int port, string urlContain, string urlNotContain)
		{
			RemoteSessionsResponse remoteSessionsResponse;
			if (port <= 0)
			{
				remoteSessionsResponse = null;
			}
			else
			{
				List<RemoteSessionsResponse> list = ChromeRemote.a(string.Format("http://127.0.0.1:{0}", port));
				if (list != null && list.Count > 0)
				{
					if (string.IsNullOrEmpty(urlContain) && string.IsNullOrEmpty(urlNotContain))
					{
						return list[0];
					}
					foreach (RemoteSessionsResponse remoteSessionsResponse2 in list)
					{
						if (!string.IsNullOrEmpty(remoteSessionsResponse2.url) && remoteSessionsResponse2.url.Trim().Contains(urlContain) && (string.IsNullOrEmpty(urlNotContain) || !remoteSessionsResponse2.url.Contains(urlNotContain)))
						{
							return remoteSessionsResponse2;
						}
					}
				}
				remoteSessionsResponse = null;
			}
			return remoteSessionsResponse;
		}
	}
}
