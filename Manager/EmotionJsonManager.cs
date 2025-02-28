using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Agiso;
using Agiso.DbManager;
using Agiso.Object;
using Agiso.Utils;
using Agiso.WwService.Sdk.Response;
using Newtonsoft.Json;

namespace AliwwClient.Manager
{
	// Token: 0x020000AB RID: 171
	public class EmotionJsonManager : IEmotionManager
	{
		// Token: 0x0600050C RID: 1292 RVA: 0x0003F250 File Offset: 0x0003D450
		private string a(long A_0)
		{
			return string.Format("{0}\\NewAppData\\{1}#3\\emotions\\MyEmotion", AppConfig.AliWorkbenchDataPath, A_0);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00003C7E File Offset: 0x00001E7E
		public EmotionJsonManager(string userNick)
		{
			this.c = userNick;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0003F278 File Offset: 0x0003D478
		public bool Append(List<Emotion> emotions, out bool hasChange)
		{
			hasChange = false;
			bool flag;
			if (emotions == null || emotions.Count <= 0)
			{
				flag = true;
			}
			else
			{
				long num = this.a();
				string text = this.a(num);
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				string text2 = text + "\\EmotionConfig.json";
				string text3 = text + "\\EmotionConfig.json.back";
				try
				{
					if (File.Exists(text2))
					{
						File.Copy(text2, text3, true);
					}
					List<EmotionItem> list = this.Get() ?? new List<EmotionItem>();
					if (list.Count > 0)
					{
						EmotionJsonManager.a a = new EmotionJsonManager.a();
						a.a = emotions.Select(new Func<Emotion, string>(EmotionJsonManager.<>c.<>9.a)).ToList<string>();
						list = list.Where(new Func<EmotionItem, bool>(a.b)).ToList<EmotionItem>();
					}
					bool flag2 = true;
					foreach (Emotion emotion in emotions)
					{
						string text4;
						string text5;
						if (this.a(num, emotion.OriginalFile, emotion.PicUrl, out text4, out text5))
						{
							list.Add(new EmotionItem
							{
								Id = Path.GetFileNameWithoutExtension(text4),
								ShortCut = emotion.QuickSymbol,
								OriginalFile = text4,
								FixedFile = text5
							});
						}
						else
						{
							string text6 = this.c + "-" + emotion.QuickSymbol;
							if (!EmotionJsonManager.b.ContainsKey(text6))
							{
								flag2 = false;
								EmotionJsonManager.b[text6] = 1;
							}
							else if (EmotionJsonManager.b[text6] < 3)
							{
								flag2 = false;
								Dictionary<string, int> dictionary = EmotionJsonManager.b;
								string text7 = text6;
								Dictionary<string, int> dictionary2 = EmotionJsonManager.b;
								string text8 = text6;
								int num2 = dictionary2[text8] + 1;
								dictionary2[text8] = num2;
								dictionary[text7] = num2;
							}
							else
							{
								EmotionJsonManager.b.Remove(text6);
							}
						}
					}
					File.WriteAllText(text2, JsonConvert.SerializeObject(list));
					hasChange = true;
					flag = flag2;
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(ex.ToString(), 1);
					if (File.Exists(text3))
					{
						File.Copy(text3, text2, true);
					}
					flag = false;
				}
				finally
				{
					if (File.Exists(text3))
					{
						File.Delete(text3);
					}
				}
			}
			return flag;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0003F510 File Offset: 0x0003D710
		public void Delete(string quickSymbol, out bool hasChange)
		{
			EmotionJsonManager.b b = new EmotionJsonManager.b();
			b.a = quickSymbol;
			hasChange = false;
			List<EmotionItem> list = this.Get();
			if (list == null)
			{
				hasChange = false;
			}
			else
			{
				List<EmotionItem> list2 = list.Where(new Func<EmotionItem, bool>(b.b)).ToList<EmotionItem>();
				if (list.Count == list2.Count)
				{
					hasChange = false;
				}
				else
				{
					long num = this.a();
					string text = this.a(num);
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					string text2 = text + "\\EmotionConfig.json";
					string text3 = text + "\\EmotionConfig.json.back";
					try
					{
						if (File.Exists(text2))
						{
							File.Copy(text2, text3, true);
						}
						File.WriteAllText(text2, JsonConvert.SerializeObject(list2));
						hasChange = true;
					}
					catch (Exception ex)
					{
						LogWriter.WriteLog(ex.ToString(), 1);
						if (File.Exists(text3))
						{
							File.Copy(text3, text2, true);
						}
					}
					finally
					{
						if (File.Exists(text3))
						{
							File.Delete(text3);
						}
					}
				}
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0003F630 File Offset: 0x0003D830
		public List<EmotionItem> Get()
		{
			long num = this.a();
			string text = this.a(num);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string text2 = text + "\\EmotionConfig.json";
			List<EmotionItem> list;
			if (!File.Exists(text2))
			{
				list = null;
			}
			else
			{
				string text3 = File.ReadAllText(text2);
				list = JsonConvert.DeserializeObject<List<EmotionItem>>(text3);
			}
			return list;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0003F68C File Offset: 0x0003D88C
		public bool IsFileExists()
		{
			long num = this.a();
			string text = this.a(num) + "\\EmotionConfig.json";
			return File.Exists(text);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0003F6BC File Offset: 0x0003D8BC
		private long a()
		{
			if (!this.HasUserId())
			{
				throw new Exception("查无卖家用户信息");
			}
			return EmotionJsonManager.a[this.c];
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0003F6F4 File Offset: 0x0003D8F4
		private bool a(long A_0, string A_1, string A_2, out string A_3, out string A_4)
		{
			string text = this.a(A_0);
			A_3 = "";
			A_4 = "";
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(A_2);
			HttpWebResponse httpWebResponse;
			try
			{
				httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			}
			catch (WebException ex)
			{
				httpWebResponse = (HttpWebResponse)ex.Response;
				LogWriter.WriteLog(string.Format("获取图片失败，失败原因：{0}，StatusCode: {1},{2},url:{3}", new object[] { ex, httpWebResponse.StatusCode, httpWebResponse.StatusDescription, A_2 }), 1);
				return false;
			}
			bool flag;
			if (httpWebResponse.StatusCode.ToString() == "OK")
			{
				Image image = Image.FromStream(httpWebResponse.GetResponseStream());
				string extension = Path.GetExtension(A_1);
				ImageFormat imageFormat = ImageFormat.Jpeg;
				string text2 = extension;
				string text3 = text2;
				if (!(text3 == ".jpg"))
				{
					if (!(text3 == ".gif"))
					{
						if (!(text3 == ".png"))
						{
							if (text3 == ".bmp")
							{
								imageFormat = ImageFormat.Bmp;
							}
						}
						else
						{
							imageFormat = ImageFormat.Png;
						}
					}
					else
					{
						imageFormat = ImageFormat.Gif;
					}
				}
				else
				{
					imageFormat = ImageFormat.Jpeg;
				}
				string text4 = Util.ComputeHashMd5(image);
				A_3 = text4 + extension;
				A_4 = A_3.Replace(extension, "fixed.bmp");
				string text5 = Path.Combine(text, A_3);
				if (File.Exists(text5))
				{
					flag = true;
				}
				else
				{
					image.Save(text5, imageFormat);
					image.Dispose();
					string text6 = Path.Combine(text, A_4);
					Util.MakeThumbnail(text5, text6, 200, 200, "W");
					flag = true;
				}
			}
			else
			{
				LogWriter.WriteLog(string.Format("获取图片失败,url:{0},status:{1},{2}", A_2, httpWebResponse.StatusCode, httpWebResponse.StatusDescription), 1);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0003F8DC File Offset: 0x0003DADC
		public bool HasUserId()
		{
			bool flag;
			if (EmotionJsonManager.a.ContainsKey(this.c))
			{
				flag = true;
			}
			else
			{
				AliwwUserInfo aliwwUserInfo = AliwwUserInfoManager.Get(this.c);
				if (aliwwUserInfo == null)
				{
					flag = false;
				}
				else
				{
					EmotionJsonManager.a[this.c] = aliwwUserInfo.UserId;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0003F930 File Offset: 0x0003DB30
		public FileInfo GetFileInfo()
		{
			long num = this.a();
			string text = this.a(num) + "\\EmotionConfig.json";
			return new FileInfo(text);
		}

		// Token: 0x040003DB RID: 987
		[CompilerGenerated]
		private static readonly Dictionary<string, long> a = new Dictionary<string, long>();

		// Token: 0x040003DC RID: 988
		private static Dictionary<string, int> b = new Dictionary<string, int>();

		// Token: 0x040003DD RID: 989
		private string c;

		// Token: 0x020000AD RID: 173
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x0600051B RID: 1307 RVA: 0x00003CB2 File Offset: 0x00001EB2
			internal bool b(EmotionItem A_0)
			{
				return !this.a.Contains(A_0.ShortCut);
			}

			// Token: 0x040003E0 RID: 992
			public List<string> a;
		}

		// Token: 0x020000AE RID: 174
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x0600051D RID: 1309 RVA: 0x00003CC8 File Offset: 0x00001EC8
			internal bool b(EmotionItem A_0)
			{
				return A_0.ShortCut != this.a;
			}

			// Token: 0x040003E1 RID: 993
			public string a;
		}
	}
}
