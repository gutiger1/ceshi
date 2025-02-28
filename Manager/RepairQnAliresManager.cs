using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Agiso;
using Agiso.Handler;
using Agiso.Object;
using Agiso.Utils;
using AliwwClient.Cache;
using ICSharpCode.SharpZipLib.Zip;

namespace AliwwClient.Manager
{
	// Token: 0x020000B5 RID: 181
	public class RepairQnAliresManager
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x00040684 File Offset: 0x0003E884
		public static void DownAef(bool isShowDialog = false)
		{
			try
			{
				RepairQnAliresManager.a a = new RepairQnAliresManager.a();
				if (RepairQnAliresManager.a())
				{
					RepairQnAliresManager.a("修复文件已存在，无需下载！");
				}
				else if (RepairQnAliresManager.b)
				{
					RepairQnAliresManager.a("修复文件已经在下载中，请稍后...");
				}
				else
				{
					a.a = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\aef", "aef.zip");
					string text = "http://download.agiso.com/aef/aef.zip";
					DownFile downFile = new DownFile("下载修复文件", "", text, a.a);
					if (!downFile.NeedToDown())
					{
						Util.UnZipFiles(a.a, Path.GetDirectoryName(a.a), null);
					}
					else if (isShowDialog)
					{
						RepairQnAliresManager.b = true;
						DialogResult dialogResult = downFile.ShowDialog();
						RepairQnAliresManager.b = false;
						if (dialogResult == DialogResult.OK)
						{
							Util.UnZipFiles(a.a, Path.GetDirectoryName(a.a), null);
						}
					}
					else
					{
						DownFile downFile2 = downFile;
						downFile2.Callback = (Action<bool>)Delegate.Combine(downFile2.Callback, new Action<bool>(a.b));
						RepairQnAliresManager.b = true;
						downFile.Start();
					}
				}
			}
			catch (Exception ex)
			{
				RepairQnAliresManager.a(ex.ToString());
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000407C4 File Offset: 0x0003E9C4
		private static bool a()
		{
			return File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\aef", "aef.dll"));
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000407F8 File Offset: 0x0003E9F8
		private static bool c(Dictionary<string, List<ProcessInfo>> A_0)
		{
			bool flag;
			if (AppConfig.AllowAutoLogin)
			{
				flag = false;
			}
			else
			{
				try
				{
					List<string> list = new List<string>();
					foreach (KeyValuePair<string, List<ProcessInfo>> keyValuePair in A_0)
					{
						string key = keyValuePair.Key;
						foreach (ProcessInfo processInfo in keyValuePair.Value)
						{
							if (processInfo.MainWindowHandle != IntPtr.Zero)
							{
								string text = Path.Combine(Path.GetDirectoryName(processInfo.MainModuleFileName), key);
								if (!string.IsNullOrEmpty(text) && !list.Contains(text))
								{
									list.Add(text);
								}
							}
							Win32Extend.KillProcessById(processInfo.Id, null);
						}
					}
					int num = 0;
					int num2 = 0;
					foreach (string text2 in list)
					{
						if (RepairQnAliresManager.b(text2))
						{
							num++;
						}
						else
						{
							num2++;
						}
					}
					flag = num > 0 && num2 == 0;
				}
				catch (Exception ex)
				{
					RepairQnAliresManager.a(ex.ToString());
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000409AC File Offset: 0x0003EBAC
		private static bool a(Dictionary<string, string> A_0)
		{
			int num = 0;
			int num2 = 0;
			foreach (KeyValuePair<string, string> keyValuePair in A_0)
			{
				string text = Path.Combine(keyValuePair.Value, keyValuePair.Key);
				if (RepairQnAliresManager.b(text))
				{
					num++;
				}
				else
				{
					num2++;
				}
			}
			return num > 0 && num2 == 0;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00040A2C File Offset: 0x0003EC2C
		private static bool b(string A_0)
		{
			RepairQnAliresManager.b b = new RepairQnAliresManager.b();
			b.b = A_0;
			b.a = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\aef", "aef.dll");
			b.c = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\aef", "aef.pak");
			return Util.CheckWait(5000, new Func<bool>(b.d), 300);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00040AB0 File Offset: 0x0003ECB0
		public static void RepairAef()
		{
			if (!AppConfig.AllowAutoLogin)
			{
				Dictionary<string, List<ProcessInfo>> processInfoDictByProcess = Util.GetProcessInfoDictByProcess();
				Dictionary<string, List<ProcessInfo>> dictionary = new Dictionary<string, List<ProcessInfo>>();
				if (processInfoDictByProcess != null && processInfoDictByProcess.Count > 0)
				{
					dictionary = RepairQnAliresManager.b(processInfoDictByProcess);
				}
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
				string text;
				List<string> list = Util.GetVersionListByRegedit(out text);
				if (list == null || list.Count <= 0)
				{
					MessageBox.Show("未知千牛版本号，可能还未运行千牛", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					list = RepairQnAliresManager.a(list);
					foreach (string text2 in list)
					{
						dictionary2.Add(text2, text);
					}
					if (dictionary.Count == 0 && dictionary2.Count == 0)
					{
						MessageBox.Show("您的千牛版本暂时不需要修复。请当千牛升级到 7.02.05N 或更高版本时，再执行该修复操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else if (RepairQnAliresManager.a)
					{
						MessageBox.Show("前一次修复仍在进行中，请稍后再试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						DialogResult dialogResult = MessageBox.Show("您好，该操作用于自助解决“无法与千牛建立连接”的问题，确定继续？（注：修复时，会自动关闭所有千牛）", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
						if (dialogResult == DialogResult.OK)
						{
							try
							{
								RepairQnAliresManager.a = true;
								RepairQnAliresManager.DownAef(true);
								if (!RepairQnAliresManager.a())
								{
									MessageBox.Show("修复失败，文件不存在，请稍后再试。如果一直重复出现，请联系旺旺在线客服“agiso”。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								}
								else if (false | RepairQnAliresManager.c(dictionary) | RepairQnAliresManager.a(dictionary2))
								{
									MessageBox.Show("修复完成，请重新登录千牛！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								}
								else
								{
									MessageBox.Show("修复失败，操作文件失败，请稍后再试。如果一直重复出现，请联系旺旺在线客服“agiso”。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show("修复失败，" + ex.Message + "，请稍后再试。如果一直重复出现，请联系旺旺在线客服“agiso”。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
							finally
							{
								RepairQnAliresManager.a = false;
							}
						}
					}
				}
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00040CA8 File Offset: 0x0003EEA8
		public static Dictionary<int, string> DictAlires
		{
			get
			{
				if (RepairQnAliresManager.c == null)
				{
					RepairQnAliresManager.c = new Dictionary<int, string>();
					RepairQnAliresManager.c[50900] = "<script src='http://wwmsg.agiso.com/im' id='_agi'></script>";
					RepairQnAliresManager.c[50904] = "<script src='http://wwmsg.agiso.com/in' id='_agi'></script>";
				}
				return RepairQnAliresManager.c;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00040CFC File Offset: 0x0003EEFC
		public static string AliresReplaceJsUrl
		{
			get
			{
				string text;
				if (AppConfig.CurrentAliwwClientVersion >= 50904)
				{
					text = RepairQnAliresManager.DictAlires[50904];
				}
				else
				{
					text = RepairQnAliresManager.DictAlires[50900];
				}
				return text;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00003D2E File Offset: 0x00001F2E
		public static string AgentAliresReplaceJsUrl { get; } = "<script src='http://127.0.0.1:17532/in' id='_agi'></script>";

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00003D35 File Offset: 0x00001F35
		public static string AliresReplaceJsUrl2 { get; } = "<script src='http://wwmsg.agiso.com/in' id='_agi' name='wwmsgagiso'></script>";

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00003D3C File Offset: 0x00001F3C
		public static string AliresReplaceJsUrlHttps { get; } = "<script src='https://wwmsg.agiso.com/in' id='_ag'></script>";

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00003D43 File Offset: 0x00001F43
		public static string AgentAliresReplaceJsUrl2 { get; } = "<script src='http://127.0.0.1:17532/in' id='_agi' name='wwmsgagiso'></script>";

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00003D4A File Offset: 0x00001F4A
		public static string DefaultSourceJsUrl
		{
			get
			{
				return "<script src=\"https://iseiya.taobao.com/imsupport\"></script>";
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00003D52 File Offset: 0x00001F52
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x00003D59 File Offset: 0x00001F59
		public static bool NeedToRestart { get; set; } = false;

		// Token: 0x06000546 RID: 1350 RVA: 0x00040D40 File Offset: 0x0003EF40
		private static Dictionary<string, List<ProcessInfo>> b(Dictionary<string, List<ProcessInfo>> A_0)
		{
			Dictionary<string, List<ProcessInfo>> dictionary = new Dictionary<string, List<ProcessInfo>>();
			foreach (KeyValuePair<string, List<ProcessInfo>> keyValuePair in A_0)
			{
				string text = keyValuePair.Key;
				if (!string.IsNullOrEmpty(text))
				{
					text = text.Replace("N", "").Trim();
					if (Util.VersionCompare(text, "7.02.05") >= 0)
					{
						dictionary[keyValuePair.Key] = keyValuePair.Value;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00040DE8 File Offset: 0x0003EFE8
		private static List<string> a(IEnumerable<string> A_0)
		{
			List<string> list = new List<string>();
			List<string> list2;
			if (A_0 == null)
			{
				list2 = list;
			}
			else
			{
				foreach (string text in A_0)
				{
					if (!string.IsNullOrEmpty(text))
					{
						string text2 = text.Replace("N", "").Trim();
						if (Util.VersionCompare(text2, "7.02.05") >= 0)
						{
							list.Add(text);
						}
					}
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00040E80 File Offset: 0x0003F080
		public static void RepairAlires()
		{
			try
			{
				Dictionary<string, List<ProcessInfo>> processInfoDictByProcess = Util.GetProcessInfoDictByProcess();
				if (!Util.IsEmptyList<KeyValuePair<string, List<ProcessInfo>>>(processInfoDictByProcess))
				{
					RepairQnAliresManager.a(processInfoDictByProcess);
				}
				else if ((DateTime.Now - RepairQnAliresManager.d).TotalMinutes >= 3.0)
				{
					RepairQnAliresManager.d = DateTime.Now;
					string text;
					List<string> versionListByRegedit = Util.GetVersionListByRegedit(out text);
					RepairQnAliresManager.a(versionListByRegedit, text);
				}
			}
			catch (Exception ex)
			{
				RepairQnAliresManager.a(ex.ToString());
				k.a().WriteLine("通信时异常，请尝试退出所有千牛后，再重新登录千牛");
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00040F1C File Offset: 0x0003F11C
		private static void a(Dictionary<string, List<ProcessInfo>> A_0)
		{
			Dictionary<string, List<ProcessInfo>> dictionary = RepairQnAliresManager.b(A_0);
			if (dictionary.Count > 0)
			{
				Dictionary<string, List<ProcessInfo>> dictionary2 = new Dictionary<string, List<ProcessInfo>>();
				foreach (KeyValuePair<string, List<ProcessInfo>> keyValuePair in dictionary)
				{
					string key = keyValuePair.Key;
					foreach (ProcessInfo processInfo in keyValuePair.Value)
					{
						string text = Path.Combine(Path.GetDirectoryName(processInfo.MainModuleFileName), key);
						if (!dictionary2.ContainsKey(text))
						{
							dictionary2[text] = new List<ProcessInfo>();
						}
						dictionary2[text].Add(processInfo);
					}
				}
				if (dictionary2.Count > 0)
				{
					foreach (KeyValuePair<string, List<ProcessInfo>> keyValuePair2 in dictionary2)
					{
						bool flag = false;
						bool flag2 = false;
						bool flag3 = false;
						bool flag4 = false;
						string text2 = keyValuePair2.Value[0].Version.Replace("N", "").Trim();
						if (Util.VersionCompare(text2, "9.17.00") < 0)
						{
							RepairQnAliresManager.a(keyValuePair2.Key + "\\uiresources\\default\\resdb.alires", out flag);
							RepairQnAliresManager.a(keyValuePair2.Key + "\\webui\\web.resdb.alires", out flag2);
						}
						else if (Util.VersionCompare(text2, "9.17.05") <= 0)
						{
							RepairQnAliresManager.a(keyValuePair2.Key + "\\Resources\\newWebui\\webui.zip", "<script src=\"https://iseiya.taobao.com/imsupport\"></script>", RepairQnAliresManager.g, out flag3);
						}
						else
						{
							RepairQnAliresManager.a(keyValuePair2.Key + "\\Resources\\newWebui\\webui.zip", "https://iseiya.taobao.com/imsupport", "https://wwmsg.agiso.com/in         ", out flag4);
						}
						if (flag || flag2 || flag3 || flag4)
						{
							RepairQnAliresManager.NeedToRestart = true;
						}
					}
				}
			}
			if ((DateTime.Now - RepairQnAliresManager.d).TotalMinutes >= 3.0)
			{
				RepairQnAliresManager.d = DateTime.Now;
				List<string> list = new List<string>();
				foreach (KeyValuePair<string, List<ProcessInfo>> keyValuePair3 in A_0)
				{
					foreach (ProcessInfo processInfo2 in keyValuePair3.Value)
					{
						string directoryName = Path.GetDirectoryName(processInfo2.MainModuleFileName);
						if (!list.Contains(directoryName))
						{
							list.Add(directoryName);
						}
					}
				}
				foreach (string text3 in list)
				{
					List<string> versionListUnderPath = Util.GetVersionListUnderPath(text3);
					RepairQnAliresManager.a(versionListUnderPath, text3);
				}
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000412B4 File Offset: 0x0003F4B4
		private static void a(List<string> A_0, string A_1)
		{
			if (A_0.Count > 0)
			{
				List<string> list = RepairQnAliresManager.a(A_0);
				foreach (string text in list)
				{
					string text2 = text.Replace("N", "").Trim();
					if (Util.VersionCompare(text2, "9.17.00") < 0)
					{
						bool flag;
						RepairQnAliresManager.a(Path.Combine(A_1, text) + "\\uiresources\\default\\resdb.alires", out flag);
						RepairQnAliresManager.a(Path.Combine(A_1, text) + "\\webui\\web.resdb.alires", out flag);
					}
					else if (Util.VersionCompare(text2, "9.17.05") <= 0)
					{
						bool flag;
						RepairQnAliresManager.a(Path.Combine(A_1, text) + "\\Resources\\newWebui\\webui.zip", "<script src=\"https://iseiya.taobao.com/imsupport\"></script>", RepairQnAliresManager.g, out flag);
					}
					else
					{
						bool flag;
						RepairQnAliresManager.a(Path.Combine(A_1, text) + "\\Resources\\newWebui\\webui.zip", "https://iseiya.taobao.com/imsupport", "https://wwmsg.agiso.com/in         ", out flag);
					}
				}
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000413D8 File Offset: 0x0003F5D8
		private static void a(string A_0, out bool A_1)
		{
			A_1 = false;
			FileInfo fileInfo = new FileInfo(A_0);
			if (fileInfo.Exists)
			{
				try
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(A_0));
					FileInfo[] files = directoryInfo.GetFiles();
					if (files != null && files.Length != 0)
					{
						foreach (FileInfo fileInfo2 in files)
						{
							if (fileInfo2.Name.Contains("resdb.alires_bak_"))
							{
								File.Delete(fileInfo2.FullName);
							}
							if (fileInfo2.Name == "sign.json")
							{
								File.Delete(fileInfo2.FullName);
							}
						}
					}
				}
				catch
				{
				}
				DateTime dateTime = fileInfo.LastWriteTime;
				LogQnAliresRecode log = LogQnAliresRecodeCache.GetLog(A_0);
				string text = (AppConfig.AllowAutoLogin ? RepairQnAliresManager.e : RepairQnAliresManager.AliresReplaceJsUrl);
				string text2 = (AppConfig.AllowAutoLogin ? RepairQnAliresManager.h : RepairQnAliresManager.f);
				bool flag = false;
				if (log == null || !(log.FileModifyTime.ToString("yyyy-MM-dd HH:mm:ss") == dateTime.ToString("yyyy-MM-dd HH:mm:ss")) || !text.Equals(log.ModifyJsUrl) || !text2.Equals(log.ModifyJsUrl2))
				{
					for (int j = 0; j < 3; j++)
					{
						using (FileBinaryEditor fileBinaryEditor = new FileBinaryEditor(A_0))
						{
							try
							{
								int num = fileBinaryEditor.FileIndexOf("<script src=\"https://iseiya.taobao.com/imsupport\"></script>", 0);
								string text3 = "<script src=\"https://iseiya.taobao.com/imsupport\"></script>";
								if (num < 0)
								{
									List<string> list = new List<string>();
									list.AddRange(RepairQnAliresManager.DictAlires.Values);
									list.Add(RepairQnAliresManager.e);
									foreach (string text4 in list)
									{
										if (!(text4 == text))
										{
											num = fileBinaryEditor.FileIndexOf(text4, 0);
											if (num > 0)
											{
												text3 = text4;
												break;
											}
										}
									}
								}
								if (num < 0)
								{
									break;
								}
								if (!fileBinaryEditor.FileReplaceStr(text3, text, num, true))
								{
									flag = true;
									break;
								}
								fileInfo.Refresh();
								dateTime = fileInfo.LastWriteTime;
							}
							catch (Exception ex)
							{
								k.a().WriteLine("通信修复异常");
								RepairQnAliresManager.a(string.Format("替换文件时异常，{0}", ex));
								flag = true;
								break;
							}
						}
					}
					using (FileBinaryEditor fileBinaryEditor2 = new FileBinaryEditor(A_0))
					{
						try
						{
							int num2 = fileBinaryEditor2.FileIndexOf("<script src=\"https://g.alicdn.com/bshop2/im_support/0.7.0/index.js\"></script>", 0);
							string text5 = "<script src=\"https://g.alicdn.com/bshop2/im_support/0.7.0/index.js\"></script>";
							if (num2 < 0)
							{
								List<string> list2 = new List<string>
								{
									RepairQnAliresManager.f,
									RepairQnAliresManager.h
								};
								foreach (string text6 in list2)
								{
									if (!(text6 == text2))
									{
										num2 = fileBinaryEditor2.FileIndexOf(text6, 0);
										if (num2 > 0)
										{
											text5 = text6;
											break;
										}
									}
								}
							}
							if (num2 >= 0)
							{
								if (fileBinaryEditor2.FileReplaceStr(text5, text2, num2, true))
								{
									fileInfo.Refresh();
									dateTime = fileInfo.LastWriteTime;
								}
								else
								{
									flag = true;
								}
							}
						}
						catch (Exception ex2)
						{
							k.a().WriteLine("通信修复异常2");
							RepairQnAliresManager.a(string.Format("替换文件时异常2，{0}", ex2));
							flag = true;
						}
					}
					if (!flag)
					{
						A_1 = true;
						if (log != null)
						{
							LogQnAliresRecodeCache.Update(A_0, dateTime, AppConfig.CurrentAliwwClientVersion, text, text2);
						}
						else
						{
							LogQnAliresRecodeCache.Insert(A_0, dateTime, AppConfig.CurrentAliwwClientVersion, text, text2);
						}
					}
				}
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000417B0 File Offset: 0x0003F9B0
		private static void a(string A_0, string A_1, string A_2, out bool A_3)
		{
			FileInfo fileInfo = new FileInfo(A_0);
			if (!fileInfo.Exists)
			{
				A_3 = false;
			}
			else
			{
				LogQnAliresRecode log = LogQnAliresRecodeCache.GetLog(A_0);
				if (log != null && log.FileModifyTime.ToString("yyyy-MM-dd HH:mm:ss") == fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss") && log.ModifyJsUrl.TrimEnd(new char[0]) == A_2.TrimEnd(new char[0]))
				{
					A_3 = false;
				}
				else
				{
					try
					{
						RepairQnAliresManager.a(A_0, "web_chat-packer/recent.html", A_1, A_2);
						string text = Path.Combine(fileInfo.DirectoryName, "sign.json");
						if (File.Exists(text))
						{
							FileInfo fileInfo2 = new FileInfo(text);
							if (fileInfo2.Length > 0L)
							{
								string text2 = Path.Combine(fileInfo.DirectoryName, "sign.json.back");
								fileInfo2.CopyTo(text2);
								fileInfo2.Delete();
								using (File.Create(text))
								{
								}
								File.Delete(text2);
							}
						}
					}
					catch (Exception ex)
					{
						RepairQnAliresManager.a(string.Format("替换异常，{0}", ex));
						A_3 = false;
						return;
					}
					A_3 = true;
					DateTime lastWriteTime = new FileInfo(A_0).LastWriteTime;
					if (log != null)
					{
						LogQnAliresRecodeCache.Update(A_0, lastWriteTime, AppConfig.CurrentAliwwClientVersion, A_2, "");
					}
					else
					{
						LogQnAliresRecodeCache.Insert(A_0, lastWriteTime, AppConfig.CurrentAliwwClientVersion, A_2, "");
					}
				}
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00041940 File Offset: 0x0003FB40
		private static void a(string A_0, string A_1, string A_2, string A_3)
		{
			using (ZipFile zipFile = new ZipFile(A_0))
			{
				if (zipFile.FindEntry(A_1, false) > 0)
				{
					ZipEntry entry = zipFile.GetEntry(A_1);
					string text = "";
					using (Stream inputStream = zipFile.GetInputStream(entry))
					{
						using (StreamReader streamReader = new StreamReader(inputStream))
						{
							text = streamReader.ReadToEnd();
							if (text.IndexOf(A_2) < 0)
							{
								return;
							}
							text = text.Replace(A_2, A_3);
						}
					}
					zipFile.BeginUpdate();
					zipFile.Add(new j(text), A_1);
					zipFile.CommitUpdate();
				}
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00003D61 File Offset: 0x00001F61
		private static void a(string A_0)
		{
			LogWriter.WriteLog(A_0, 1);
		}

		// Token: 0x040003E9 RID: 1001
		private static bool a = false;

		// Token: 0x040003EA RID: 1002
		private static bool b = false;

		// Token: 0x040003EB RID: 1003
		private static Dictionary<int, string> c;

		// Token: 0x040003EC RID: 1004
		private static DateTime d = DateTime.MinValue;

		// Token: 0x040003ED RID: 1005
		[CompilerGenerated]
		private static readonly string e;

		// Token: 0x040003EE RID: 1006
		[CompilerGenerated]
		private static readonly string f;

		// Token: 0x040003EF RID: 1007
		[CompilerGenerated]
		private static readonly string g;

		// Token: 0x040003F0 RID: 1008
		[CompilerGenerated]
		private static readonly string h;

		// Token: 0x040003F1 RID: 1009
		[CompilerGenerated]
		private static bool i;

		// Token: 0x020000B6 RID: 182
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x06000552 RID: 1362 RVA: 0x00041A64 File Offset: 0x0003FC64
			internal void b(bool A_0)
			{
				RepairQnAliresManager.b = false;
				if (A_0)
				{
					Util.UnZipFiles(this.a, Path.GetDirectoryName(this.a), null);
				}
			}

			// Token: 0x040003F2 RID: 1010
			public string a;
		}

		// Token: 0x020000B7 RID: 183
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x06000554 RID: 1364 RVA: 0x00041A94 File Offset: 0x0003FC94
			internal bool d()
			{
				bool flag;
				try
				{
					File.Copy(this.a, Path.Combine(this.b, "aef.dll"), true);
					File.Copy(this.c, Path.Combine(this.b, "aef.pak"), true);
					flag = true;
				}
				catch (Exception ex)
				{
					RepairQnAliresManager.a(ex.ToString());
					flag = false;
				}
				return flag;
			}

			// Token: 0x040003F3 RID: 1011
			public string a;

			// Token: 0x040003F4 RID: 1012
			public string b;

			// Token: 0x040003F5 RID: 1013
			public string c;
		}
	}
}
