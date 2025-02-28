using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Agiso.AcExpression;
using Agiso.Utils;

namespace Agiso.Object
{
	// Token: 0x02000691 RID: 1681
	public class AutoReplyInfo
	{
		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		// (set) Token: 0x0600205D RID: 8285 RVA: 0x0000E5AC File Offset: 0x0000C7AC
		public long IdNo { get; set; }

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x0000E5B5 File Offset: 0x0000C7B5
		// (set) Token: 0x0600205F RID: 8287 RVA: 0x0000E5BD File Offset: 0x0000C7BD
		public long Idx { get; set; }

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x0000E5C6 File Offset: 0x0000C7C6
		// (set) Token: 0x06002061 RID: 8289 RVA: 0x0000E5CE File Offset: 0x0000C7CE
		public bool Enabled { get; set; }

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x0000E5D7 File Offset: 0x0000C7D7
		// (set) Token: 0x06002063 RID: 8291 RVA: 0x0000E5DF File Offset: 0x0000C7DF
		public bool Valid { get; set; }

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x0000E5E8 File Offset: 0x0000C7E8
		// (set) Token: 0x06002065 RID: 8293 RVA: 0x0000E5F0 File Offset: 0x0000C7F0
		public EnumArType Type { get; set; }

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x0000E5F9 File Offset: 0x0000C7F9
		// (set) Token: 0x06002067 RID: 8295 RVA: 0x0000E601 File Offset: 0x0000C801
		public string TypeText { get; set; }

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x0000E60A File Offset: 0x0000C80A
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x0000E612 File Offset: 0x0000C812
		public string KeyWord { get; set; }

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x0000E61B File Offset: 0x0000C81B
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x0000E623 File Offset: 0x0000C823
		public string ReplyWord { get; set; }

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x0000E62C File Offset: 0x0000C82C
		// (set) Token: 0x0600206D RID: 8301 RVA: 0x0000E634 File Offset: 0x0000C834
		public string SellerNick { get; set; }

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x0000E63D File Offset: 0x0000C83D
		// (set) Token: 0x0600206F RID: 8303 RVA: 0x0000E645 File Offset: 0x0000C845
		public long Grade { get; set; }

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x0000E64E File Offset: 0x0000C84E
		// (set) Token: 0x06002071 RID: 8305 RVA: 0x0000E656 File Offset: 0x0000C856
		public DateTime? ArStartTime { get; set; }

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x0000E65F File Offset: 0x0000C85F
		// (set) Token: 0x06002073 RID: 8307 RVA: 0x0000E667 File Offset: 0x0000C867
		public DateTime? ArEndTime { get; set; }

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x0000E670 File Offset: 0x0000C870
		// (set) Token: 0x06002075 RID: 8309 RVA: 0x0000E678 File Offset: 0x0000C878
		public long Option1 { get; set; }

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x0000E681 File Offset: 0x0000C881
		// (set) Token: 0x06002077 RID: 8311 RVA: 0x0000E689 File Offset: 0x0000C889
		public DateTime ModifyTime { get; set; }

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x000536E8 File Offset: 0x000518E8
		public string ModifyTimeStr
		{
			get
			{
				return this.ModifyTime.ToString("yyyy-MM-dd HH:mm:ss");
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x0000E692 File Offset: 0x0000C892
		// (set) Token: 0x0600207A RID: 8314 RVA: 0x0000E69A File Offset: 0x0000C89A
		public DateTime CreateTime { get; set; }

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x0005370C File Offset: 0x0005190C
		public string CreateTimeStr
		{
			get
			{
				return this.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
			}
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x00053730 File Offset: 0x00051930
		private OrCollection a()
		{
			if (this.p == null)
			{
				try
				{
					string text = this.KeyWord.ToLower();
					this.p = new OrCollection();
					OrCollection.Parse(ref text, ref this.p);
				}
				catch
				{
				}
			}
			return this.p;
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x0005378C File Offset: 0x0005198C
		public bool CheckTime()
		{
			if (this.ArStartTime != null && this.ArEndTime != null)
			{
				long num = Convert.ToInt64(this.ArStartTime.Value.ToString("HHmm"));
				long num2 = Convert.ToInt64(this.ArEndTime.Value.ToString("HHmm"));
				long num3 = Convert.ToInt64(DateTime.Now.ToString("HHmm"));
				if (num != num2 && (num != 0L || num2 != 2359L))
				{
					if (num < num2)
					{
						if (num3 < num || num3 > num2)
						{
							return false;
						}
					}
					else if (num3 < num && num3 > num2)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x00053870 File Offset: 0x00051A70
		public bool Match(string newMsg)
		{
			bool flag;
			if (!this.CheckTime())
			{
				flag = false;
			}
			else
			{
				EnumArType type = this.Type;
				EnumArType enumArType = type;
				if (enumArType <= EnumArType.Contains)
				{
					if (enumArType != EnumArType.EqualsWith)
					{
						if (enumArType != EnumArType.StartWith)
						{
							if (enumArType != EnumArType.Contains)
							{
								goto IL_0167;
							}
						}
						else
						{
							if (newMsg.ToLower().StartsWith(this.KeyWord.ToLower()))
							{
								return true;
							}
							goto IL_0167;
						}
					}
					else
					{
						if (newMsg.ToLower().Equals(this.KeyWord.ToLower()))
						{
							return true;
						}
						goto IL_0167;
					}
				}
				else if (enumArType != EnumArType.AcExpression)
				{
					if (enumArType != EnumArType.Expression)
					{
						if (enumArType != EnumArType.NoMatching)
						{
							goto IL_0167;
						}
						return true;
					}
					else
					{
						try
						{
							Assembly assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "\\Agiso.dll");
							Type type2 = assembly.GetType("Agiso.Dynamic.StringX");
							object obj = assembly.CreateInstance("Agiso.Dynamic.StringX", true, BindingFlags.Default, null, new object[] { newMsg }, CultureInfo.CurrentCulture, null);
							MethodInfo method = type2.GetMethod("Execute");
							if ((bool)method.Invoke(obj, new object[] { this.KeyWord }))
							{
								return true;
							}
							goto IL_0167;
						}
						catch (Exception ex)
						{
							LogWriter.WriteLog(ex.ToString(), 1);
							goto IL_0167;
						}
					}
				}
				try
				{
					if (this.a() != null && this.a().ExecAll(newMsg.ToLower()))
					{
						return true;
					}
				}
				catch
				{
				}
				IL_0167:
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x00053A04 File Offset: 0x00051C04
		public SortedDictionary<ArActionType, List<object>> Explain(AldsAccountInfo account)
		{
			return AutoReplyInfo.ExplainReplyWord(this.ReplyWord, account);
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x00053A20 File Offset: 0x00051C20
		public static SortedDictionary<ArActionType, List<object>> ExplainReplyWord(string replyWord, AldsAccountInfo account)
		{
			SortedDictionary<ArActionType, List<object>> sortedDictionary = new SortedDictionary<ArActionType, List<object>>();
			SortedDictionary<ArActionType, List<object>> sortedDictionary2;
			if (string.IsNullOrEmpty(replyWord))
			{
				sortedDictionary2 = sortedDictionary;
			}
			else
			{
				string text = replyWord;
				if (text.Contains("{@提取}"))
				{
					sortedDictionary.Add(ArActionType.Tiqu, null);
					text = text.Replace("{@提取}", "");
				}
				bool flag = false;
				if (text.Contains("{@转接("))
				{
					Regex regex = new Regex("{@转接\\((.*?)\\)}");
					Match match = regex.Match(text);
					if (match.Success)
					{
						flag = true;
						string text2 = match.Groups[1].Value.Trim();
						sortedDictionary.Add(ArActionType.AppointTransferCall, new List<object> { text2 });
						text = text.Replace(match.Groups[0].Value, "");
					}
				}
				if (text.Contains("{@转接}"))
				{
					if (!flag)
					{
						sortedDictionary.Add(ArActionType.TransferCall, null);
					}
					text = text.Replace("{@转接}", "");
				}
				text = text.Replace("{$提取链接}", account.TiquShortUrl).Replace("{$人工客服}", AppConfig.GetDutyManualNick(account));
				if (!string.IsNullOrEmpty(text))
				{
					sortedDictionary.Add(ArActionType.ReplyMsg, new List<object> { text });
				}
				sortedDictionary2 = sortedDictionary;
			}
			return sortedDictionary2;
		}

		// Token: 0x0400124A RID: 4682
		[CompilerGenerated]
		private long a;

		// Token: 0x0400124B RID: 4683
		[CompilerGenerated]
		private long b;

		// Token: 0x0400124C RID: 4684
		[CompilerGenerated]
		private bool c;

		// Token: 0x0400124D RID: 4685
		[CompilerGenerated]
		private bool d;

		// Token: 0x0400124E RID: 4686
		[CompilerGenerated]
		private EnumArType e;

		// Token: 0x0400124F RID: 4687
		[CompilerGenerated]
		private string f;

		// Token: 0x04001250 RID: 4688
		[CompilerGenerated]
		private string g;

		// Token: 0x04001251 RID: 4689
		[CompilerGenerated]
		private string h;

		// Token: 0x04001252 RID: 4690
		[CompilerGenerated]
		private string i;

		// Token: 0x04001253 RID: 4691
		[CompilerGenerated]
		private long j;

		// Token: 0x04001254 RID: 4692
		[CompilerGenerated]
		private DateTime? k;

		// Token: 0x04001255 RID: 4693
		[CompilerGenerated]
		private DateTime? l;

		// Token: 0x04001256 RID: 4694
		[CompilerGenerated]
		private long m;

		// Token: 0x04001257 RID: 4695
		[CompilerGenerated]
		private DateTime n;

		// Token: 0x04001258 RID: 4696
		[CompilerGenerated]
		private DateTime o;

		// Token: 0x04001259 RID: 4697
		private OrCollection p;
	}
}
