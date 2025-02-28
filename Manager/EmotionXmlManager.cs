using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml;
using Agiso;
using Agiso.Utils;
using Agiso.WwService.Sdk.Response;

namespace AliwwClient.Manager
{
	// Token: 0x020000B1 RID: 177
	public class EmotionXmlManager : IEmotionManager
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0003F99C File Offset: 0x0003DB9C
		private string a()
		{
			return AppConfig.AliWorkbenchDataPath + "\\cntaobao" + this.b.Replace(":", "=") + "\\emotions\\DefCusEmotions";
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00003CDB File Offset: 0x00001EDB
		public EmotionXmlManager(string userNick)
		{
			this.b = userNick;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0003F9D8 File Offset: 0x0003DBD8
		public List<EmotionItem> Get()
		{
			string text = this.a();
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string text2 = text + "\\EmotionConfig.xml";
			List<EmotionItem> list;
			if (!File.Exists(text2))
			{
				list = null;
			}
			else
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(text2);
				XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/xparam/Item");
				if (xmlNodeList == null || xmlNodeList.Count <= 0)
				{
					list = null;
				}
				else
				{
					List<EmotionItem> list2 = new List<EmotionItem>();
					foreach (object obj in xmlNodeList)
					{
						XmlNode xmlNode = (XmlNode)obj;
						if (xmlNode.ChildNodes != null && xmlNode.ChildNodes.Count > 0)
						{
							EmotionItem emotionItem = new EmotionItem();
							foreach (object obj2 in xmlNode.ChildNodes)
							{
								XmlNode xmlNode2 = (XmlNode)obj2;
								string name = xmlNode2.Name;
								string text3 = name;
								if (!(text3 == "ShortCut"))
								{
									if (!(text3 == "OriginalFile"))
									{
										if (!(text3 == "FixedFile"))
										{
											if (!(text3 == "BigFixedFile"))
											{
												if (text3 == "MD5")
												{
													emotionItem.MD5 = xmlNode2.InnerText;
												}
											}
											else
											{
												emotionItem.BigFixedFile = xmlNode2.InnerText;
											}
										}
										else
										{
											emotionItem.FixedFile = xmlNode2.InnerText;
										}
									}
									else
									{
										emotionItem.OriginalFile = xmlNode2.InnerText;
									}
								}
								else
								{
									emotionItem.ShortCut = xmlNode2.InnerText;
								}
							}
							list2.Add(emotionItem);
						}
					}
					list = list2;
				}
			}
			return list;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0003FBF8 File Offset: 0x0003DDF8
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
				string text = this.a();
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				string text2 = text + "\\EmotionConfig.xml";
				string text3 = text + "\\EmotionConfig.xml.back";
				try
				{
					if (File.Exists(text2))
					{
						File.Copy(text2, text3, true);
					}
					XmlDocument xmlDocument = new XmlDocument();
					XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
					xmlDocument.AppendChild(xmlDeclaration);
					XmlElement xmlElement = xmlDocument.CreateElement("xparam");
					xmlDocument.AppendChild(xmlElement);
					XmlNode xmlNode = xmlDocument.SelectSingleNode("xparam");
					List<EmotionItem> list = this.Get();
					if (list != null && list.Count > 0)
					{
						EmotionXmlManager.a a = new EmotionXmlManager.a();
						a.a = emotions.Select(new Func<Emotion, string>(EmotionXmlManager.<>c.<>9.a)).ToList<string>();
						list = list.Where(new Func<EmotionItem, bool>(a.b)).ToList<EmotionItem>();
						foreach (EmotionItem emotionItem in list)
						{
							string extension = Path.GetExtension(emotionItem.OriginalFile);
							string text4 = emotionItem.OriginalFile.Replace(extension, "big.bmp");
							string text5 = emotionItem.OriginalFile.Replace(extension, "fixed.bmp");
							XmlElement xmlElement2 = xmlDocument.CreateElement("Item");
							xmlNode.AppendChild(xmlElement2);
							xmlElement2.AppendChild(this.a(xmlDocument, "ShortCut", emotionItem.ShortCut));
							xmlElement2.AppendChild(this.a(xmlDocument, "Meaning", ""));
							xmlElement2.AppendChild(this.a(xmlDocument, "OriginalFile", emotionItem.OriginalFile));
							xmlElement2.AppendChild(this.a(xmlDocument, "BigFixedFile", text4));
							xmlElement2.AppendChild(this.a(xmlDocument, "FixedFile", text5));
							xmlElement2.AppendChild(this.a(xmlDocument, "GroupName", "DefCusEmotions"));
							xmlElement2.AppendChild(this.a(xmlDocument, "IsSys", "0"));
							xmlElement2.AppendChild(this.a(xmlDocument, "MD5", emotionItem.MD5));
						}
					}
					bool flag2 = true;
					foreach (Emotion emotion in emotions)
					{
						string text6;
						string text7;
						string text8;
						if (this.a(emotion.OriginalFile, emotion.PicUrl, out text6, out text7, out text8))
						{
							XmlElement xmlElement3 = xmlDocument.CreateElement("Item");
							xmlNode.AppendChild(xmlElement3);
							xmlElement3.AppendChild(this.a(xmlDocument, "ShortCut", emotion.QuickSymbol));
							xmlElement3.AppendChild(this.a(xmlDocument, "Meaning", ""));
							xmlElement3.AppendChild(this.a(xmlDocument, "OriginalFile", text6));
							xmlElement3.AppendChild(this.a(xmlDocument, "FixedFile", text7));
							xmlElement3.AppendChild(this.a(xmlDocument, "BigFixedFile", text8));
							xmlElement3.AppendChild(this.a(xmlDocument, "GroupName", "DefCusEmotions"));
							xmlElement3.AppendChild(this.a(xmlDocument, "IsSys", "0"));
							xmlElement3.AppendChild(this.a(xmlDocument, "MD5", Path.GetFileNameWithoutExtension(emotion.OriginalFile)));
						}
						else
						{
							string text9 = this.b + "-" + emotion.QuickSymbol;
							if (!EmotionXmlManager.a.ContainsKey(text9))
							{
								flag2 = false;
								EmotionXmlManager.a[text9] = 1;
							}
							else if (EmotionXmlManager.a[text9] < 3)
							{
								flag2 = false;
								Dictionary<string, int> dictionary = EmotionXmlManager.a;
								string text10 = text9;
								Dictionary<string, int> dictionary2 = EmotionXmlManager.a;
								string text11 = text9;
								int num = dictionary2[text11] + 1;
								dictionary2[text11] = num;
								dictionary[text10] = num;
							}
							else
							{
								EmotionXmlManager.a.Remove(text9);
							}
						}
					}
					xmlDocument.Save(text2);
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

		// Token: 0x06000529 RID: 1321 RVA: 0x0004010C File Offset: 0x0003E30C
		public void Delete(string quickSymbol, out bool hasChange)
		{
			EmotionXmlManager.b b = new EmotionXmlManager.b();
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
					string text = this.a();
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					string text2 = text + "\\EmotionConfig.xml";
					string text3 = text + "\\EmotionConfig.xml.back";
					try
					{
						if (File.Exists(text2))
						{
							File.Copy(text2, text3, true);
						}
						XmlDocument xmlDocument = new XmlDocument();
						XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
						xmlDocument.AppendChild(xmlDeclaration);
						XmlElement xmlElement = xmlDocument.CreateElement("xparam");
						xmlDocument.AppendChild(xmlElement);
						XmlNode xmlNode = xmlDocument.SelectSingleNode("xparam");
						foreach (EmotionItem emotionItem in list2)
						{
							string extension = Path.GetExtension(emotionItem.OriginalFile);
							string text4 = emotionItem.OriginalFile.Replace(extension, "big.bmp");
							string text5 = emotionItem.OriginalFile.Replace(extension, "fixed.bmp");
							XmlElement xmlElement2 = xmlDocument.CreateElement("Item");
							xmlNode.AppendChild(xmlElement2);
							xmlElement2.AppendChild(this.a(xmlDocument, "ShortCut", emotionItem.ShortCut));
							xmlElement2.AppendChild(this.a(xmlDocument, "Meaning", ""));
							xmlElement2.AppendChild(this.a(xmlDocument, "OriginalFile", emotionItem.OriginalFile));
							xmlElement2.AppendChild(this.a(xmlDocument, "BigFixedFile", text4));
							xmlElement2.AppendChild(this.a(xmlDocument, "FixedFile", text5));
							xmlElement2.AppendChild(this.a(xmlDocument, "GroupName", "DefCusEmotions"));
							xmlElement2.AppendChild(this.a(xmlDocument, "IsSys", "0"));
							xmlElement2.AppendChild(this.a(xmlDocument, "MD5", Path.GetFileNameWithoutExtension(emotionItem.OriginalFile)));
						}
						xmlDocument.Save(text2);
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

		// Token: 0x0600052A RID: 1322 RVA: 0x000403FC File Offset: 0x0003E5FC
		private bool a(string A_0, string A_1, out string A_2, out string A_3, out string A_4)
		{
			string text = this.a();
			A_2 = "";
			A_3 = "";
			A_4 = "";
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(A_1);
			HttpWebResponse httpWebResponse;
			try
			{
				httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			}
			catch (WebException ex)
			{
				httpWebResponse = (HttpWebResponse)ex.Response;
				LogWriter.WriteLog(string.Format("获取图片失败，失败原因：{0}，StatusCode: {1},{2},url:{3}", new object[] { ex, httpWebResponse.StatusCode, httpWebResponse.StatusDescription, A_1 }), 1);
				return false;
			}
			bool flag;
			if (httpWebResponse.StatusCode.ToString() == "OK")
			{
				Image image = Image.FromStream(httpWebResponse.GetResponseStream());
				string extension = Path.GetExtension(A_0);
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
				A_2 = text4 + extension;
				A_4 = A_2.Replace(extension, "big.bmp");
				A_3 = A_2.Replace(extension, "fixed.bmp");
				string text5 = Path.Combine(text, A_2);
				if (File.Exists(text5))
				{
					flag = true;
				}
				else
				{
					image.Save(text5, imageFormat);
					image.Dispose();
					string text6 = Path.Combine(text, A_4);
					string text7 = Path.Combine(text, A_3);
					Util.MakeThumbnail(text5, text6, 50, 50, "HW");
					Util.MakeThumbnail(text5, text7, 24, 24, "HW");
					flag = true;
				}
			}
			else
			{
				LogWriter.WriteLog(string.Format("获取图片失败,url:{0},status:{1},{2}", A_1, httpWebResponse.StatusCode, httpWebResponse.StatusDescription), 1);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00040614 File Offset: 0x0003E814
		private XmlElement a(XmlDocument A_0, string A_1, string A_2)
		{
			XmlElement xmlElement = A_0.CreateElement(A_1);
			xmlElement.InnerText = A_2;
			return xmlElement;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00040634 File Offset: 0x0003E834
		public bool IsFileExists()
		{
			string text = this.a() + "\\EmotionConfig.xml";
			return File.Exists(text);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0003A434 File Offset: 0x00038634
		public bool HasUserId()
		{
			return true;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0004065C File Offset: 0x0003E85C
		public FileInfo GetFileInfo()
		{
			string text = this.a() + "\\EmotionConfig.xml";
			return new FileInfo(text);
		}

		// Token: 0x040003E2 RID: 994
		private static Dictionary<string, int> a = new Dictionary<string, int>();

		// Token: 0x040003E3 RID: 995
		private string b;

		// Token: 0x040003E4 RID: 996
		private string c;

		// Token: 0x020000B3 RID: 179
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x06000534 RID: 1332 RVA: 0x00003D05 File Offset: 0x00001F05
			internal bool b(EmotionItem A_0)
			{
				return !this.a.Contains(A_0.ShortCut);
			}

			// Token: 0x040003E7 RID: 999
			public List<string> a;
		}

		// Token: 0x020000B4 RID: 180
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x06000536 RID: 1334 RVA: 0x00003D1B File Offset: 0x00001F1B
			internal bool b(EmotionItem A_0)
			{
				return A_0.ShortCut != this.a;
			}

			// Token: 0x040003E8 RID: 1000
			public string a;
		}
	}
}
