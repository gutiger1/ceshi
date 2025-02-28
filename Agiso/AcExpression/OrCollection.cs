using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Agiso.AcExpression
{
	// Token: 0x020000EF RID: 239
	public class OrCollection : List<OrInfo>
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0004D394 File Offset: 0x0004B594
		public OrInfo LastOr
		{
			get
			{
				if (base.Count == 0)
				{
					base.Add(new OrInfo());
				}
				return base[base.Count - 1];
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0004D3C8 File Offset: 0x0004B5C8
		public bool ExecAll(string source)
		{
			foreach (OrInfo orInfo in this)
			{
				if (orInfo.Exec(source))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0004D420 File Offset: 0x0004B620
		public string WriteTreeNode(string deep = "")
		{
			StringBuilder stringBuilder = new StringBuilder();
			deep += ">>";
			foreach (OrInfo orInfo in this)
			{
				foreach (AndInfo andInfo in orInfo.Child)
				{
					stringBuilder.AppendLine(deep + andInfo.Child.WriteTreeNode(deep));
					stringBuilder.AppendLine(andInfo.ContentB.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0004D4F0 File Offset: 0x0004B6F0
		public static void Parse(ref string originalSet, ref OrCollection list)
		{
			while (originalSet.Length > 0)
			{
				char c = originalSet[0];
				originalSet = originalSet.Substring(1);
				switch (c)
				{
				case '(':
					OrCollection.Parse(ref originalSet, ref list.LastOr.Child.LastAnd.Child);
					continue;
				case ')':
					return;
				case '+':
					list.LastOr.Child.Add(new AndInfo());
					continue;
				case ',':
					list.Add(new OrInfo());
					continue;
				}
				list.LastOr.LastChild.ContentB.Append(c);
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0004D5A4 File Offset: 0x0004B7A4
		public static bool Valid(string originalSet, out string validResult, out int index)
		{
			index = -1;
			bool flag;
			if (string.IsNullOrEmpty(originalSet))
			{
				validResult = string.Concat(new string[] { "空的公式" });
				flag = false;
			}
			else if (originalSet.StartsWith(")") || originalSet.StartsWith("+") || originalSet.StartsWith(","))
			{
				index = 0;
				validResult = string.Concat(new string[] { "开头出现无符字符。位置：1" });
				flag = false;
			}
			else if (originalSet.EndsWith("(") || originalSet.EndsWith("+") || originalSet.EndsWith(","))
			{
				index = originalSet.Length - 1;
				validResult = "结尾出现无符字符。位置：" + originalSet.Length;
				flag = false;
			}
			else
			{
				int num = originalSet.IndexOf("()");
				if (num >= 0)
				{
					index = num;
					validResult = "出现空的“( )”号，里面应该放点东西。位置：" + (num + 1);
					flag = false;
				}
				else
				{
					int num2 = originalSet.IndexOf(")(");
					if (num2 >= 0)
					{
						index = num2;
						validResult = "两对( )号之间，缺少应有的连接字符。位置：" + (num2 + 1);
						flag = false;
					}
					else
					{
						int num3 = originalSet.IndexOf(",,");
						if (num3 >= 0)
						{
							index = num3;
							validResult = "连续出来两个连接号。位置：" + (num3 + 1);
							flag = false;
						}
						else
						{
							int num4 = originalSet.IndexOf("++");
							if (num4 >= 0)
							{
								index = num4;
								validResult = "连续出来两个连接号。位置：" + (num4 + 1);
								flag = false;
							}
							else
							{
								int num5 = originalSet.IndexOf(",+");
								if (num5 >= 0)
								{
									index = num5;
									validResult = "连续出来两个连接号。位置：" + (num5 + 1);
									flag = false;
								}
								else
								{
									int num6 = originalSet.IndexOf("+,");
									if (num6 >= 0)
									{
										index = num6;
										validResult = "连续出来两个连接号。位置：" + (num6 + 1);
										flag = false;
									}
									else
									{
										int num7 = originalSet.Length - originalSet.Replace("(", "").Length;
										int num8 = originalSet.Length - originalSet.Replace(")", "").Length;
										if (num7 < num8)
										{
											validResult = string.Concat(new string[] { "“( )”没有成对出现，缺少“(”号" });
											flag = false;
										}
										else if (num7 > num8)
										{
											validResult = string.Concat(new string[] { "“( )”没有成对出现，缺少“)”号" });
											flag = false;
										}
										else
										{
											Regex regex = new Regex("[^\\(\\+,]\\(");
											Match match = regex.Match(originalSet);
											if (match != null && match.Success)
											{
												index = match.Index;
												validResult = "“(”号连接处，缺少“,+”连接号，位置：" + (match.Index + 1);
												flag = false;
											}
											else
											{
												Regex regex2 = new Regex("\\)[^\\)\\+,]");
												Match match2 = regex2.Match(originalSet);
												if (match2 != null && match2.Success)
												{
													index = match2.Index;
													validResult = "“)”号连接处，缺少“,+”连接号，位置：" + (match2.Index + 1);
													flag = false;
												}
												else
												{
													validResult = "";
													flag = true;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0004D904 File Offset: 0x0004BB04
		public static bool ContainsFullWidthKeyChar(string originalSet, out int index)
		{
			index = -1;
			bool flag;
			if (string.IsNullOrEmpty(originalSet))
			{
				flag = false;
			}
			else
			{
				index = originalSet.IndexOf("（");
				if (index >= 0)
				{
					flag = true;
				}
				else
				{
					index = originalSet.IndexOf("）");
					if (index >= 0)
					{
						flag = true;
					}
					else
					{
						index = originalSet.IndexOf("，");
						if (index >= 0)
						{
							flag = true;
						}
						else
						{
							index = originalSet.IndexOf("＋");
							flag = index >= 0;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0004D998 File Offset: 0x0004BB98
		public static string ReplaceFullWidthKeyChar(string originalSet)
		{
			string text;
			if (string.IsNullOrEmpty(originalSet))
			{
				text = originalSet;
			}
			else
			{
				text = originalSet.Replace('（', '(').Replace('）', ')').Replace('，', ',')
					.Replace('＋', '+');
			}
			return text;
		}
	}
}
