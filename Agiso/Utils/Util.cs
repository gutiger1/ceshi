using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using Agiso.AcExpression;
using Agiso.DBAccess;
using Agiso.Handler;
using Agiso.Object;
using Agiso.Windows;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.VisualBasic;
using Microsoft.Win32;

namespace Agiso.Utils
{
	// Token: 0x020000E8 RID: 232
	public class Util
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x000494A8 File Offset: 0x000476A8
		public static bool IsDate(string date)
		{
			bool flag;
			try
			{
				DateTime.Parse(date);
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000494D8 File Offset: 0x000476D8
		public static bool IsZipCode(string zip)
		{
			return Util.IsNaturalNumber(zip) && zip.Length == 6;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00049504 File Offset: 0x00047704
		public static string UrlEncode(string str, Encoding encoding = null)
		{
			if (encoding == null)
			{
				encoding = Encoding.Default;
			}
			return HttpUtility.UrlEncode(str, encoding);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00049528 File Offset: 0x00047728
		public static string SafeXss(string strInput)
		{
			string text;
			if (string.IsNullOrEmpty(strInput))
			{
				text = "";
			}
			else
			{
				strInput = strInput.Trim();
				strInput = strInput.Replace("'", "");
				strInput = strInput.Replace(">", "");
				strInput = strInput.Replace("<", "");
				strInput = strInput.Replace(";", "");
				strInput = strInput.Replace("/", "");
				strInput = strInput.Replace("\\", "");
				strInput = strInput.Replace("\"", "");
				strInput = strInput.Replace("&", "");
				text = strInput;
			}
			return text;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000495EC File Offset: 0x000477EC
		public static string SafeHtml(string strInput)
		{
			string text;
			if (string.IsNullOrEmpty(strInput))
			{
				text = "";
			}
			else
			{
				strInput = strInput.Trim();
				strInput = strInput.Replace("'", "");
				strInput = strInput.Replace(">", "&gt;");
				strInput = strInput.Replace("<", "&lt;");
				strInput = strInput.Replace(";", "");
				strInput = strInput.Replace("/", "");
				strInput = strInput.Replace("\\", "");
				strInput = strInput.Replace("\"", "");
				strInput = strInput.Replace("&", "");
				text = strInput;
			}
			return text;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000496B4 File Offset: 0x000478B4
		public static string TextToHtml(object input)
		{
			string text;
			if (input == null)
			{
				text = "";
			}
			else
			{
				string text2 = input.ToString().Trim();
				text2 = Util.ConvertUrlToLink(text2);
				text2 = text2.Replace("\n", "<br>");
				text2 = text2.Replace(" ", "&nbsp;");
				text = text2;
			}
			return text;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0004970C File Offset: 0x0004790C
		public static bool IsMoney(string money)
		{
			bool flag;
			try
			{
				decimal.Parse(money);
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0004973C File Offset: 0x0004793C
		public static string GenerateRandomString(int Length)
		{
			char[] array = new char[]
			{
				'2', '3', '4', '9', 'A', 'B', 'C', '8', 'D', 'E',
				'F', 'G', 'H', '5', 'K', 'M', 'N', 'P', '7', 'R',
				'S', 'T', 'U', '6', 'V', 'W', 'X', 'Y', 'Z'
			};
			string text = "";
			int num = array.Length;
			Random random = new Random(~(int)DateTime.Now.Ticks);
			for (int i = 0; i < Length; i++)
			{
				int num2 = random.Next(0, num);
				text += array[num2].ToString();
			}
			return text;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000497B4 File Offset: 0x000479B4
		private static string e(string A_0)
		{
			byte[] array = Encoding.Default.GetBytes(A_0);
			array = new MD5CryptoServiceProvider().ComputeHash(array);
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i].ToString("x").PadLeft(2, '0');
			}
			return text;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00049818 File Offset: 0x00047A18
		public static string UserMd5(string str)
		{
			string text = "key_agiso_secret";
			str + text;
			string text2 = "";
			MD5 md = MD5.Create();
			byte[] array = md.ComputeHash(Encoding.UTF8.GetBytes(str));
			for (int i = 0; i < array.Length; i++)
			{
				text2 += array[i].ToString();
			}
			return text2;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00049884 File Offset: 0x00047A84
		public static string ComputeHashMd5(string str)
		{
			return Util.ComputeHashMd5(str, Encoding.UTF8);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000498A0 File Offset: 0x00047AA0
		public static string ComputeHashMd5(string str, Encoding encode)
		{
			MD5 md = MD5.Create();
			byte[] array = md.ComputeHash(encode.GetBytes(str));
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000498FC File Offset: 0x00047AFC
		public static string ComputeHashMd5(Stream stream)
		{
			MD5 md = MD5.Create();
			byte[] array = md.ComputeHash(stream);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00049950 File Offset: 0x00047B50
		public static string ComputeHashMd5(Image image)
		{
			string text;
			if (image == null)
			{
				text = "";
			}
			else
			{
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Bmp);
				byte[] buffer = memoryStream.GetBuffer();
				memoryStream.Close();
				MD5 md = MD5.Create();
				byte[] array = md.ComputeHash(buffer);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("X2"));
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000499DC File Offset: 0x00047BDC
		public static string IntToStringSign(int num)
		{
			return ((num >= 0) ? "+" : "-") + Math.Abs(num).ToString();
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00049A10 File Offset: 0x00047C10
		public static string LongToStringSign(long num)
		{
			return ((num >= 0L) ? "+" : "-") + Math.Abs(num).ToString();
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00049A4C File Offset: 0x00047C4C
		public static bool IsNaturalNumber(string strNumber)
		{
			bool flag;
			if (string.IsNullOrEmpty(strNumber))
			{
				flag = false;
			}
			else
			{
				Regex regex = new Regex("[^0-9]");
				Regex regex2 = new Regex("0*[1-9][0-9]*");
				flag = !regex.IsMatch(strNumber) && regex2.IsMatch(strNumber);
			}
			return flag;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00049A94 File Offset: 0x00047C94
		public static bool IsWholeNumber(string strNumber)
		{
			bool flag;
			if (string.IsNullOrEmpty(strNumber))
			{
				flag = false;
			}
			else
			{
				Regex regex = new Regex("[^0-9]");
				flag = !regex.IsMatch(strNumber);
			}
			return flag;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00049AC8 File Offset: 0x00047CC8
		public static bool IsInteger(string strNumber)
		{
			bool flag;
			if (string.IsNullOrEmpty(strNumber))
			{
				flag = false;
			}
			else
			{
				Regex regex = new Regex("[^0-9-]");
				Regex regex2 = new Regex("^-[0-9]+$|^[0-9]+$");
				flag = !regex.IsMatch(strNumber) && regex2.IsMatch(strNumber);
			}
			return flag;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00049B10 File Offset: 0x00047D10
		public static bool IsPositiveNumber(string strNumber)
		{
			bool flag;
			if (string.IsNullOrEmpty(strNumber))
			{
				flag = false;
			}
			else
			{
				Regex regex = new Regex("[^0-9.]");
				Regex regex2 = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
				Regex regex3 = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
				flag = !regex.IsMatch(strNumber) && regex2.IsMatch(strNumber) && !regex3.IsMatch(strNumber);
			}
			return flag;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00049B74 File Offset: 0x00047D74
		public static bool IsNumber(string strNumber)
		{
			bool flag;
			if (string.IsNullOrEmpty(strNumber))
			{
				flag = false;
			}
			else
			{
				Regex regex = new Regex("[^0-9.-]");
				Regex regex2 = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
				Regex regex3 = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
				string text = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
				string text2 = "^([-]|[0-9])[0-9]*$";
				Regex regex4 = new Regex(string.Concat(new string[] { "(", text, ")|(", text2, ")" }));
				flag = !regex.IsMatch(strNumber) && !regex2.IsMatch(strNumber) && !regex3.IsMatch(strNumber) && regex4.IsMatch(strNumber);
			}
			return flag;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00049C24 File Offset: 0x00047E24
		public static bool IsAlpha(string strToCheck)
		{
			bool flag;
			if (string.IsNullOrEmpty(strToCheck))
			{
				flag = false;
			}
			else
			{
				Regex regex = new Regex("[^a-zA-Z]");
				flag = !regex.IsMatch(strToCheck);
			}
			return flag;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00049C58 File Offset: 0x00047E58
		public static bool IsAlphaNumeric(string strToCheck)
		{
			bool flag;
			if (string.IsNullOrEmpty(strToCheck))
			{
				flag = false;
			}
			else
			{
				Regex regex = new Regex("[^a-zA-Z0-9]");
				flag = !regex.IsMatch(strToCheck);
			}
			return flag;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00049C8C File Offset: 0x00047E8C
		public static bool IsEmailAddress(string strEmailAddress)
		{
			bool flag;
			if (string.IsNullOrEmpty(strEmailAddress))
			{
				flag = false;
			}
			else
			{
				Regex regex = new Regex("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
				flag = regex.IsMatch(strEmailAddress);
			}
			return flag;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00049CBC File Offset: 0x00047EBC
		public static string MatchEmailAddress(string source)
		{
			string text;
			if (string.IsNullOrEmpty(source))
			{
				text = "";
			}
			else
			{
				string text2 = "";
				Regex regex = new Regex("(?<email>[\\da-zA-Z_]+@([-\\dA-Za-z]+\\.)+[a-zA-Z]{2,})");
				foreach (object obj in regex.Matches(source))
				{
					Match match = (Match)obj;
					text2 = text2 + "," + match.Groups["email"].Value;
				}
				if (text2.Length == 0)
				{
					text = "";
				}
				else
				{
					text = text2.Substring(1);
				}
			}
			return text;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00049D80 File Offset: 0x00047F80
		public static List<string> FindUrlsFrom(string content)
		{
			List<string> list = new List<string>();
			Regex regex = Util.a();
			MatchCollection matchCollection = regex.Matches(content);
			if (matchCollection != null)
			{
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					list.Add(match.Value);
				}
			}
			return list;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00049E04 File Offset: 0x00048004
		private static Regex a()
		{
			string text = "(http|ftp|https):\\/\\/[\\w\\-_]+(\\.[\\w\\-_]+)+([\\w\\-\\.!,@?^=%&amp;:/~\\+#]*[\\w\\-\\@?^=%&amp;/~\\+#])?";
			return new Regex(text);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00049E20 File Offset: 0x00048020
		public static bool IsContainUrl(object contentObj)
		{
			bool flag;
			if (contentObj == null)
			{
				flag = false;
			}
			else
			{
				string text = contentObj.ToString().Trim();
				Regex regex = Util.a();
				flag = regex.IsMatch(text);
			}
			return flag;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00049E54 File Offset: 0x00048054
		public static string ConvertUrlToLink(object contentObj)
		{
			string text;
			if (contentObj == null)
			{
				text = "";
			}
			else
			{
				string text2 = contentObj.ToString().Trim();
				Regex regex = Util.a();
				text2 = regex.Replace(text2, new MatchEvaluator(Util.a));
				text = text2;
			}
			return text;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00049E98 File Offset: 0x00048098
		private static string a(Match A_0)
		{
			string text = "<a href='{0}' target='_blank'>{0}</a>";
			return string.Format(text, A_0.Value);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00049EBC File Offset: 0x000480BC
		public static decimal ToMoney(string moneyStr)
		{
			decimal num;
			if (string.IsNullOrEmpty(moneyStr) || moneyStr.Trim().Length == 0)
			{
				num = 0m;
			}
			else
			{
				try
				{
					num = decimal.Round(decimal.Parse(moneyStr), 2);
				}
				catch
				{
					num = 0m;
				}
			}
			return num;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00049F18 File Offset: 0x00048118
		public static decimal ToMoney(decimal moneyValue)
		{
			return decimal.Round(moneyValue, 2);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00049F30 File Offset: 0x00048130
		public static long ToLong(object sourceObj)
		{
			long num;
			if (sourceObj == null)
			{
				num = 0L;
			}
			else
			{
				string text = sourceObj.ToString();
				try
				{
					if (string.IsNullOrEmpty(text))
					{
						num = 0L;
					}
					else
					{
						num = long.Parse(text);
					}
				}
				catch
				{
					num = -1L;
				}
			}
			return num;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00049F94 File Offset: 0x00048194
		public static int ToInt(object sourceObj)
		{
			int num;
			if (sourceObj == null)
			{
				num = 0;
			}
			else
			{
				string text = sourceObj.ToString();
				try
				{
					if (string.IsNullOrEmpty(text))
					{
						num = 0;
					}
					else
					{
						num = int.Parse(text);
					}
				}
				catch
				{
					num = -1;
				}
			}
			return num;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00049FE0 File Offset: 0x000481E0
		public static DateTime? ToDateTime(object datetimeObj)
		{
			DateTime? dateTime;
			if (datetimeObj == null)
			{
				dateTime = null;
			}
			else
			{
				string text = datetimeObj.ToString();
				try
				{
					if (string.IsNullOrEmpty(text))
					{
						dateTime = null;
					}
					else
					{
						dateTime = new DateTime?(DateTime.Parse(text));
					}
				}
				catch
				{
					dateTime = null;
				}
			}
			return dateTime;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0004A048 File Offset: 0x00048248
		public static bool ToBoolean(object sourceObj)
		{
			bool flag;
			if (sourceObj == null)
			{
				flag = false;
			}
			else
			{
				string text = sourceObj.ToString();
				try
				{
					if (string.IsNullOrEmpty(text))
					{
						flag = false;
					}
					else
					{
						flag = bool.Parse(text);
					}
				}
				catch
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0004A094 File Offset: 0x00048294
		public static double ToDouble(object sourceObj)
		{
			double num;
			if (sourceObj == null)
			{
				num = 0.0;
			}
			else
			{
				string text = sourceObj.ToString();
				try
				{
					if (string.IsNullOrEmpty(text))
					{
						num = 0.0;
					}
					else
					{
						num = double.Parse(text);
					}
				}
				catch
				{
					num = 0.0;
				}
			}
			return num;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0004A0F8 File Offset: 0x000482F8
		public static decimal TruncMoney(decimal moneyValue)
		{
			int num = Convert.ToInt32(moneyValue * 100m) % 10;
			moneyValue = (moneyValue * 100m - num) / 100m;
			return moneyValue;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0004A144 File Offset: 0x00048344
		public static bool IsCellNumber(string cell)
		{
			bool flag;
			if (cell.Length < 11 || cell.Length > 12)
			{
				flag = false;
			}
			else
			{
				try
				{
					long num = Convert.ToInt64(cell);
					if (num < 13000000000L)
					{
						return false;
					}
					if (num >= 19000000000L)
					{
						return false;
					}
				}
				catch
				{
					return false;
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0004A1B8 File Offset: 0x000483B8
		public static string GetChineseMoney(decimal moneyValue)
		{
			if (moneyValue < 0m)
			{
				moneyValue *= -1m;
			}
			string text = Convert.ToInt32(Util.TruncMoney(moneyValue) * 100m).ToString();
			int length = text.Length;
			StringBuilder stringBuilder = new StringBuilder(100);
			if (length > 14)
			{
				throw new Exception("Money Value Is Too Large");
			}
			if (length > 10 && length <= 14)
			{
				stringBuilder.Append(Util.d(text.Substring(0, text.Length - 10)));
				text = text.Substring(text.Length - 10, 10);
				stringBuilder.Append("亿");
			}
			if (length > 6)
			{
				stringBuilder.Append(Util.d(text.Substring(0, text.Length - 6)));
				text = text.Substring(text.Length - 6, 6);
				stringBuilder.Append("万");
			}
			if (length > 2)
			{
				stringBuilder.Append(Util.d(text.Substring(0, text.Length - 2)));
				text = text.Substring(text.Length - 2, 2);
				stringBuilder.Append("元");
			}
			if (Convert.ToInt32(text) == 0)
			{
				stringBuilder.Append("整");
			}
			else
			{
				int num = Convert.ToInt32(text.Substring(0, 1));
				stringBuilder.Append(Util.a[num]);
				if (num != 0)
				{
					stringBuilder.Append("角");
				}
				int num2 = Convert.ToInt32(text.Substring(1, 1));
				if (num2 != 0)
				{
					stringBuilder.Append(Util.a[num2]);
					stringBuilder.Append("分");
				}
			}
			string text2 = stringBuilder.ToString();
			while (text2.IndexOf("零零") != -1)
			{
				stringBuilder.Replace("零零", "零");
				text2 = stringBuilder.ToString();
			}
			stringBuilder.Replace("零亿", "亿");
			stringBuilder.Replace("零万", "万");
			stringBuilder.Replace("亿万", "亿");
			return stringBuilder.ToString();
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0004A3E8 File Offset: 0x000485E8
		private static string d(string A_0)
		{
			int num = Convert.ToInt32(A_0);
			string text;
			if (num == 0)
			{
				text = "";
			}
			else
			{
				string text2 = num.ToString();
				StringBuilder stringBuilder = new StringBuilder(10);
				if (text2.Length == 4)
				{
					int num2 = Convert.ToInt32(text2.Substring(0, 1));
					text2 = text2.Substring(1, text2.Length - 1);
					stringBuilder.Append(Util.a[num2]);
					if (num2 != 0)
					{
						stringBuilder.Append("仟");
					}
				}
				if (text2.Length == 3)
				{
					int num2 = Convert.ToInt32(text2.Substring(0, 1));
					text2 = text2.Substring(1, text2.Length - 1);
					stringBuilder.Append(Util.a[num2]);
					if (num2 != 0)
					{
						stringBuilder.Append("佰");
					}
				}
				if (text2.Length == 2)
				{
					int num2 = Convert.ToInt32(text2.Substring(0, 1));
					text2 = text2.Substring(1, text2.Length - 1);
					stringBuilder.Append(Util.a[num2]);
					if (num2 != 0)
					{
						stringBuilder.Append("拾");
					}
				}
				if (text2.Length == 1)
				{
					int num2 = Convert.ToInt32(text2);
					stringBuilder.Append(Util.a[num2]);
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0004A528 File Offset: 0x00048728
		public static string RemoveHtmlTag(string str)
		{
			Regex regex = new Regex("<\\/*[^<>]*>");
			return regex.Replace(str, "");
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0004A550 File Offset: 0x00048750
		public static int DateDiff(DateTime DateTime1, DateTime DateTime2)
		{
			DateTime1 = Convert.ToDateTime(DateTime1.ToString("yyyy-MM-dd"));
			DateTime2 = Convert.ToDateTime(DateTime2.ToString("yyyy-MM-dd"));
			TimeSpan timeSpan = new TimeSpan(DateTime1.Ticks);
			TimeSpan timeSpan2 = new TimeSpan(DateTime2.Ticks);
			return timeSpan.Subtract(timeSpan2).Duration().Days;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0004A5C0 File Offset: 0x000487C0
		public static DateTime TimeSpanToDateTime(string timespanStr)
		{
			double num = (double)Util.ToLong(timespanStr);
			return Util.TimeSpanToDateTime(num);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0004A5E0 File Offset: 0x000487E0
		public static DateTime TimeSpanToDateTime(double timestamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			DateTime dateTime2;
			if (timestamp > 10000000000.0)
			{
				dateTime2 = dateTime.AddMilliseconds(timestamp);
			}
			else
			{
				dateTime2 = dateTime.AddSeconds(timestamp);
			}
			return dateTime2.ToLocalTime();
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0004A62C File Offset: 0x0004882C
		public static string ToTimeSpanString(DateTime dt)
		{
			return Convert.ToInt64((dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString();
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0004A66C File Offset: 0x0004886C
		public static int CheckEmail(string mailAddress)
		{
			Regex regex = new Regex("^[a-zA-Z0-9_-]+@([a-zA-Z0-9-]+\\.){1,}(com|net|edu|miz|biz|cn|cc)$");
			int num;
			if (!regex.IsMatch(mailAddress))
			{
				num = 405;
			}
			else
			{
				string text = Util.c(mailAddress);
				if (text == null)
				{
					num = 404;
				}
				else
				{
					TcpClient tcpClient = new TcpClient();
					tcpClient.NoDelay = true;
					tcpClient.ReceiveTimeout = 3000;
					tcpClient.SendTimeout = 3000;
					try
					{
						tcpClient.Connect(text, 25);
						NetworkStream stream = tcpClient.GetStream();
						StreamReader streamReader = new StreamReader(stream, Encoding.Default);
						StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default);
						string text2 = "service@justonline.cn.cn";
						streamWriter.WriteLine("helo " + text);
						streamWriter.WriteLine("mail from:<" + mailAddress + ">");
						streamWriter.WriteLine("rcpt to:<" + text2 + ">");
						string text3 = streamReader.ReadLine();
						if (!text3.StartsWith("2"))
						{
							num = 403;
						}
						else
						{
							streamWriter.WriteLine("quit");
							num = 200;
						}
					}
					catch
					{
						num = 403;
					}
				}
			}
			return num;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0004A7B4 File Offset: 0x000489B4
		private static string c(string A_0)
		{
			string text = A_0.Split(new char[] { '@' })[1];
			Process process = Process.Start(new ProcessStartInfo
			{
				UseShellExecute = false,
				RedirectStandardInput = true,
				RedirectStandardOutput = true,
				FileName = "nslookup",
				CreateNoWindow = true,
				Arguments = "-type=mx " + text
			});
			StreamReader standardOutput = process.StandardOutput;
			Regex regex = new Regex("mail exchanger = (?<mailServer>[^\\s]+)");
			string text2;
			while ((text2 = standardOutput.ReadLine()) != null)
			{
				Match match = regex.Match(text2);
				if (regex.Match(text2).Success)
				{
					return match.Groups["mailServer"].Value;
				}
			}
			return null;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0004A884 File Offset: 0x00048A84
		public static string GetWeekName(int id)
		{
			string text = "";
			switch (id)
			{
			case 1:
				text = "星期一";
				break;
			case 2:
				text = "星期二";
				break;
			case 3:
				text = "星期三";
				break;
			case 4:
				text = "星期四";
				break;
			case 5:
				text = "星期五";
				break;
			case 6:
				text = "星期六";
				break;
			case 7:
				text = "星期日";
				break;
			}
			return text;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0004A904 File Offset: 0x00048B04
		public static int GetWeekID(DayOfWeek week)
		{
			int num = 0;
			switch (week)
			{
			case DayOfWeek.Sunday:
				num = 7;
				break;
			case DayOfWeek.Monday:
				num = 1;
				break;
			case DayOfWeek.Tuesday:
				num = 2;
				break;
			case DayOfWeek.Wednesday:
				num = 3;
				break;
			case DayOfWeek.Thursday:
				num = 4;
				break;
			case DayOfWeek.Friday:
				num = 5;
				break;
			case DayOfWeek.Saturday:
				num = 6;
				break;
			}
			return num;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0004A958 File Offset: 0x00048B58
		public static string GetAbsoluteFilePath(string filePath)
		{
			string text = filePath;
			if (!filePath.Substring(1, 1).Equals(":") && !filePath.StartsWith("\\"))
			{
				text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
			}
			return text.Replace("/", "\\");
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0004A9B8 File Offset: 0x00048BB8
		public static string FilterCompetitorKeyword(string input)
		{
			return input.Replace("京东", " xx ").Replace("新蛋", " xx ").Replace("某东", " xx ")
				.Replace("某蛋", " xx ")
				.Replace("*东", " xx ")
				.Replace("*蛋", " xx ");
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0004AA30 File Offset: 0x00048C30
		public decimal Round(decimal x, int len)
		{
			return decimal.Round(x + 0.000001m, len);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0004AA58 File Offset: 0x00048C58
		public static int DecimalToU_Int32(decimal argument)
		{
			try
			{
				object obj = (int)argument;
			}
			catch (Exception ex)
			{
				Util.a(ex);
			}
			object obj2;
			try
			{
				obj2 = (uint)argument;
			}
			catch (Exception ex2)
			{
				obj2 = Util.a(ex2);
			}
			return int.Parse(obj2.ToString());
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0004AAC4 File Offset: 0x00048CC4
		private static string a(Exception A_0)
		{
			string text = A_0.GetType().ToString();
			return text.Substring(text.LastIndexOf('.') + 1);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0004AAF0 File Offset: 0x00048CF0
		public static string TrimString(string s, int len)
		{
			string text;
			if (s.Length > len)
			{
				text = s.Substring(0, len - 1) + "...";
			}
			else
			{
				text = s;
			}
			return text;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0004AB28 File Offset: 0x00048D28
		public static string TrimNullRow(string input)
		{
			string text;
			if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
			{
				text = "";
			}
			else
			{
				Regex regex = new Regex("([\\r]+|[\\n]+)[\\s\\t\\r\\n]*([\\r]+|[\\n]+)");
				text = regex.Replace(input, "\n");
			}
			return text;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0004AB74 File Offset: 0x00048D74
		public static string TrimEveryRow(string input)
		{
			string text;
			if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
			{
				text = "";
			}
			else
			{
				Regex regex = new Regex("[\\s\\t]+[\\r\\n]");
				Regex regex2 = new Regex("[\\r\\n][\\s\\t]+");
				text = regex2.Replace(regex.Replace(input.Trim(), "\n"), "\n");
			}
			return text;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0004ABDC File Offset: 0x00048DDC
		public static bool IsDecimalNoLessThanZero(string str)
		{
			bool flag;
			if (string.IsNullOrEmpty(str))
			{
				flag = false;
			}
			else
			{
				try
				{
					decimal num = decimal.Parse(str);
					if (num >= 0m)
					{
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				catch
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0004AC28 File Offset: 0x00048E28
		public static bool IsDecimalGreaterThanZero(string str)
		{
			bool flag;
			if (string.IsNullOrEmpty(str))
			{
				flag = false;
			}
			else
			{
				try
				{
					decimal num = decimal.Parse(str);
					if (num > 0m)
					{
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				catch
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0004AC74 File Offset: 0x00048E74
		public static bool smethod_0(string Id)
		{
			bool flag;
			if (Id.Length == 18)
			{
				flag = Util.b(Id);
			}
			else
			{
				flag = Id.Length == 15 && Util.a(Id);
			}
			return flag;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0004ACB0 File Offset: 0x00048EB0
		private static bool b(string A_0)
		{
			long num = 0L;
			bool flag;
			if (!long.TryParse(A_0.Remove(17), out num) || (double)num < Math.Pow(10.0, 16.0) || !long.TryParse(A_0.Replace('x', '0').Replace('X', '0'), out num))
			{
				flag = false;
			}
			else
			{
				string text = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
				if (text.IndexOf(A_0.Remove(2)) == -1)
				{
					flag = false;
				}
				else
				{
					string text2 = A_0.Substring(6, 8).Insert(6, "-").Insert(4, "-");
					DateTime dateTime = default(DateTime);
					if (!DateTime.TryParse(text2, out dateTime))
					{
						flag = false;
					}
					else
					{
						string[] array = "1,0,x,9,8,7,6,5,4,3,2".Split(new char[] { ',' });
						string[] array2 = "7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2".Split(new char[] { ',' });
						char[] array3 = A_0.Remove(17).ToCharArray();
						int num2 = 0;
						for (int i = 0; i < 17; i++)
						{
							num2 += int.Parse(array2[i]) * int.Parse(array3[i].ToString());
						}
						int num3 = -1;
						Math.DivRem(num2, 11, out num3);
						flag = !(array[num3] != A_0.Substring(17, 1).ToLower());
					}
				}
			}
			return flag;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0004AE28 File Offset: 0x00049028
		private static bool a(string A_0)
		{
			long num = 0L;
			bool flag;
			if (!long.TryParse(A_0, out num) || (double)num < Math.Pow(10.0, 14.0))
			{
				flag = false;
			}
			else
			{
				string text = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
				if (text.IndexOf(A_0.Remove(2)) == -1)
				{
					flag = false;
				}
				else
				{
					string text2 = A_0.Substring(6, 6).Insert(4, "-").Insert(2, "-");
					DateTime dateTime = default(DateTime);
					flag = DateTime.TryParse(text2, out dateTime);
				}
			}
			return flag;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0004AECC File Offset: 0x000490CC
		public static bool IsIntGreaterThanZero(string str)
		{
			bool flag;
			if (str == string.Empty)
			{
				flag = false;
			}
			else
			{
				try
				{
					int num = Convert.ToInt32(str);
					if (num > 0)
					{
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				catch
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0004AF18 File Offset: 0x00049118
		public static string ReadFileStream(string fileName)
		{
			return Util.ReadFileStream(fileName, Encoding.UTF8);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0004AF34 File Offset: 0x00049134
		public static string ReadFileStream(string fileName, Encoding encoding)
		{
			StreamReader streamReader = new StreamReader(fileName, encoding, false);
			string text = streamReader.ReadToEnd().ToString();
			streamReader.Close();
			return text;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0004AF60 File Offset: 0x00049160
		public static string[] GetHtmlImageUrlList(string sHtmlText, bool filterA)
		{
			if (filterA)
			{
				Regex regex = new Regex("(?is)<a[^>]*?href=(['\"]?)(?<url>[^'\"\\s>]+)\\1[^>]*>(?<text>(?:(?!</?a\\b).)*)</a>", RegexOptions.IgnoreCase);
				sHtmlText = regex.Replace(sHtmlText, "");
			}
			Regex regex2 = new Regex("<img\\b[^<>]*?\\bsrc[\\s\\t\\r\\n]*=[\\s\\t\\r\\n]*[\"']?[\\s\\t\\r\\n]*(?<imgUrl>[^\\s\\t\\r\\n\"'<>]*)[^<>]*?/?[\\s\\t\\r\\n]*>", RegexOptions.IgnoreCase);
			MatchCollection matchCollection = regex2.Matches(sHtmlText);
			int num = 0;
			string[] array = new string[matchCollection.Count];
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				array[num++] = match.Groups["imgUrl"].Value;
			}
			return array;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0004B01C File Offset: 0x0004921C
		public static long GetDirSize(string path)
		{
			long num = 0L;
			string[] files = Directory.GetFiles(path);
			for (int i = 0; i < files.Length; i++)
			{
				FileStream fileStream = new FileStream(files[i], FileMode.Open);
				num += fileStream.Length;
				fileStream.Close();
			}
			return num;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0004B068 File Offset: 0x00049268
		public static void DeleteFiles(string dir)
		{
			if (Directory.Exists(dir))
			{
				string[] files = Directory.GetFiles(dir);
				for (int i = 0; i < files.Length; i++)
				{
					if (File.Exists(files[i]))
					{
						File.Delete(files[i]);
					}
				}
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0004B0AC File Offset: 0x000492AC
		public static void SetFileReadAccess(string fileName, bool setReadOnly)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			fileInfo.IsReadOnly = setReadOnly;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0004B0C8 File Offset: 0x000492C8
		public static void ReWriteFile(string fileName, string text)
		{
			bool flag = false;
			FileInfo fileInfo = new FileInfo(fileName);
			if (fileInfo.IsReadOnly)
			{
				flag = true;
				fileInfo.IsReadOnly = false;
			}
			using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
			{
				fileStream.SetLength(0L);
				using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default))
				{
					streamWriter.WriteLine(text);
					streamWriter.Close();
				}
				fileStream.Close();
			}
			if (flag)
			{
				fileInfo.IsReadOnly = true;
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0004B16C File Offset: 0x0004936C
		public static string RequestHtmlByUrl(string url)
		{
			WebRequest webRequest = WebRequest.Create(url);
			WebResponse response = webRequest.GetResponse();
			Stream responseStream = response.GetResponseStream();
			Encoding utf = Encoding.UTF8;
			Encoding @default = Encoding.Default;
			StreamReader streamReader = new StreamReader(responseStream, utf);
			string text = streamReader.ReadToEnd();
			streamReader.Close();
			streamReader.Dispose();
			responseStream.Close();
			responseStream.Dispose();
			return text;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0004B1CC File Offset: 0x000493CC
		public static string[] ShortUrl(string url)
		{
			string[] array = new string[]
			{
				"a", "b", "c", "d", "e", "f", "g", "h", "i", "j",
				"k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
				"u", "v", "w", "x", "y", "z", "0", "1", "2", "3",
				"4", "5", "6", "7", "8", "9", "A", "B", "C", "D",
				"E", "F", "G", "H", "I", "J", "K", "L", "M", "N",
				"O", "P", "Q", "R", "S", "T", "U", "V", "W", "X",
				"Y", "Z"
			};
			string text = Util.UserMd5(url);
			string[] array2 = new string[text.Length / 8];
			for (int i = 0; i < text.Length / 8; i++)
			{
				string text2 = text.Substring(i * 8, 8);
				long num = 1073741823L & long.Parse(text2);
				string text3 = "";
				for (int j = 0; j < 6; j++)
				{
					long num2 = 61L & num;
					text3 += array[(int)num2];
					num >>= 5;
				}
				array2[i] = text3;
			}
			return array2;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0004B4C4 File Offset: 0x000496C4
		public static string GetMasterNick(string userNick)
		{
			string text;
			if (string.IsNullOrEmpty(userNick))
			{
				text = "";
			}
			else
			{
				int num = userNick.IndexOf(':');
				if (num >= 0)
				{
					text = userNick.Substring(0, num);
				}
				else
				{
					text = userNick;
				}
			}
			return text;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0004B504 File Offset: 0x00049704
		public static void OpenLink(string url)
		{
			try
			{
				Process.Start(url);
			}
			catch
			{
				MessageBox.Show("未设置默认浏览器，无法打开网页：" + url, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0004B54C File Offset: 0x0004974C
		public static string GetTidsFromString(string source)
		{
			string text;
			if (string.IsNullOrEmpty(source))
			{
				text = "";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				Regex regex = new Regex("(?<tid>\\d{15,})");
				foreach (object obj in regex.Matches(source))
				{
					Match match = (Match)obj;
					string value = match.Groups["tid"].Value;
					long num = Util.ToLong(value);
					if (num >= 10000000L)
					{
						stringBuilder.Append("," + value);
					}
				}
				string text2 = stringBuilder.ToString();
				if (text2 == null || text2.Length == 0)
				{
					text = "";
				}
				else
				{
					text = text2.Substring(1);
				}
			}
			return text;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0004B640 File Offset: 0x00049840
		public static int VersionCompare(string version1, string version2)
		{
			int num;
			if (string.IsNullOrEmpty(version2))
			{
				num = 1;
			}
			else if (string.IsNullOrEmpty(version1))
			{
				num = -1;
			}
			else
			{
				version1 = version1.Replace("N", "");
				version2 = version2.Replace("N", "");
				string[] array = version1.Split(new char[] { '.' });
				string[] array2 = version2.Split(new char[] { '.' });
				int num2 = array.Length;
				int num3 = array2.Length;
				int num4 = ((num2 > num3) ? num2 : num3);
				for (int i = 0; i < num4; i++)
				{
					if (i >= num2)
					{
						return -1;
					}
					if (i >= num3)
					{
						return 1;
					}
					int num5 = Util.ToInt(array[i]);
					int num6 = Util.ToInt(array2[i]);
					if (num5 > num6)
					{
						return 1;
					}
					if (num5 < num6)
					{
						return -1;
					}
				}
				num = 0;
			}
			return num;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0004B730 File Offset: 0x00049930
		public static DateTime UnixTimestampToDateTime(long timestamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
			return dateTime.AddSeconds((double)timestamp);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0004B75C File Offset: 0x0004995C
		public static void UnZipFiles(string zipedFileName, string unZipDirectory, string password)
		{
			new FastZip().ExtractZip(zipedFileName, unZipDirectory, 2, null, "", "", true, false);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0004B784 File Offset: 0x00049984
		public static string MixString(string source, int startLength, int endLength, string mixString = "****")
		{
			string text;
			if (string.IsNullOrEmpty(source))
			{
				text = "";
			}
			else
			{
				source = source.Trim();
				int length = source.Length;
				if (length == 0)
				{
					text = "";
				}
				else
				{
					if (endLength < 0)
					{
						endLength = 0;
					}
					if (startLength > length)
					{
						startLength = length;
					}
					if (endLength > length)
					{
						endLength = length;
					}
					string text2 = "";
					if (startLength > 0)
					{
						text2 = source.Substring(0, startLength);
					}
					text2 += mixString;
					if (endLength > 0)
					{
						text2 += source.Substring(length - endLength);
					}
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0004B810 File Offset: 0x00049A10
		public static void MixField(ref DataTable tableSource, string field, int startLength, int endLength, string mixString = "****")
		{
			if (tableSource != null && tableSource.Rows.Count != 0)
			{
				DataRow[] array = tableSource.Select("1=1");
				Util.MixField(ref array, field, startLength, endLength, mixString);
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0004B850 File Offset: 0x00049A50
		public static void MixField(ref DataRow[] rowArray, string field, int startLength, int endLength, string mixString = "****")
		{
			if (rowArray != null && rowArray.Length != 0)
			{
				foreach (DataRow dataRow in rowArray)
				{
					string text = DbUtil.TrimNull(dataRow[field]);
					dataRow[field] = Util.MixString(text, startLength, endLength, mixString);
				}
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0004B8A4 File Offset: 0x00049AA4
		public static double GetHardDiskSpace(string str_HardDiskName)
		{
			double num = 0.0;
			str_HardDiskName += ":\\";
			DriveInfo[] drives = DriveInfo.GetDrives();
			foreach (DriveInfo driveInfo in drives)
			{
				if (driveInfo.Name == str_HardDiskName)
				{
					num = (double)driveInfo.TotalSize / 1073741824.0;
				}
			}
			return num;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0004B90C File Offset: 0x00049B0C
		public static double GetHardDiskFreeSpace(string str_HardDiskName)
		{
			double num = 0.0;
			str_HardDiskName += ":\\";
			DriveInfo[] drives = DriveInfo.GetDrives();
			foreach (DriveInfo driveInfo in drives)
			{
				if (driveInfo.Name == str_HardDiskName)
				{
					num = (double)driveInfo.TotalFreeSpace / 1073741824.0;
				}
			}
			return num;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0004B974 File Offset: 0x00049B74
		public static byte[] smethod_1(Bitmap bitmap, ImageFormat format)
		{
			byte[] array2;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				bitmap.Save(memoryStream, format);
				byte[] array = new byte[memoryStream.Length];
				memoryStream.Seek(0L, SeekOrigin.Begin);
				memoryStream.Read(array, 0, Convert.ToInt32(memoryStream.Length));
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0004B9E0 File Offset: 0x00049BE0
		public static bool CheckWait(int timeOut, Func<bool> check, int sleepTime = 100)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			while (!check())
			{
				if (sleepTime > 0)
				{
					Thread.Sleep(sleepTime);
				}
				if (stopwatch.Elapsed.TotalMilliseconds >= (double)timeOut)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0004BA2C File Offset: 0x00049C2C
		public static bool? CheckWaitTrueOrNull(int timeOut, Func<bool?> check, int sleepTime = 100)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			for (;;)
			{
				bool? flag = check();
				if (flag == null)
				{
					break;
				}
				if (flag.Value)
				{
					goto IL_0062;
				}
				if (sleepTime > 0)
				{
					Thread.Sleep(sleepTime);
				}
				if (stopwatch.Elapsed.TotalMilliseconds >= (double)timeOut)
				{
					goto IL_0058;
				}
			}
			return null;
			IL_0058:
			return new bool?(false);
			IL_0062:
			return new bool?(true);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0004BAA8 File Offset: 0x00049CA8
		public static bool CheckWait(int timeOut, Func<bool> check, CancellationToken token, int sleepTime = 100)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			while (!check())
			{
				if (sleepTime > 0)
				{
					Thread.Sleep(sleepTime);
				}
				if (stopwatch.Elapsed.TotalMilliseconds >= (double)timeOut || token.IsCancellationRequested)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0004BB00 File Offset: 0x00049D00
		public static bool Retry(int retryTimes, Func<bool> check, int sleepTime = 100)
		{
			while (!check())
			{
				if (sleepTime > 0)
				{
					Thread.Sleep(sleepTime);
				}
				if (--retryTimes <= 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0004BB38 File Offset: 0x00049D38
		public static bool Retry(int retryTimes, Func<bool> check, CancellationToken token, int sleepTime = 100)
		{
			while (!check())
			{
				if (sleepTime > 0)
				{
					Thread.Sleep(sleepTime);
				}
				if (--retryTimes <= 0 || token.IsCancellationRequested)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0004BB7C File Offset: 0x00049D7C
		public static string GetEnumDescription(Enum en)
		{
			Type type = en.GetType();
			MemberInfo[] member = type.GetMember(en.ToString());
			if (member != null && member.Length != 0)
			{
				object[] customAttributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					return ((DescriptionAttribute)customAttributes[0]).Description;
				}
			}
			return en.ToString();
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0004BBE8 File Offset: 0x00049DE8
		public static bool AcExpressionExec(string originalSet, string source)
		{
			OrCollection orCollection = new OrCollection();
			OrCollection.Parse(ref originalSet, ref orCollection);
			return orCollection.ExecAll(source);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0004BC0C File Offset: 0x00049E0C
		public static bool AcExpressionValid(string originalSet, out string validResult, out int index)
		{
			return OrCollection.Valid(originalSet, out validResult, out index);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0004BC24 File Offset: 0x00049E24
		public static bool AcExpressionContainsFullWidthKeyChar(string originalSet, out int index)
		{
			return OrCollection.ContainsFullWidthKeyChar(originalSet, out index);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0004BC38 File Offset: 0x00049E38
		public static string AcExpressionReplaceFullWidthKeyChar(string originalSet)
		{
			return OrCollection.ReplaceFullWidthKeyChar(originalSet);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0004BC50 File Offset: 0x00049E50
		public static HttpWebRequest GetWebRequest(string url, string method)
		{
			HttpWebRequest httpWebRequest;
			if (url.Contains("https://"))
			{
				ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(Util.a);
				httpWebRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
			}
			else
			{
				httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			}
			httpWebRequest.ServicePoint.Expect100Continue = false;
			httpWebRequest.Method = method;
			httpWebRequest.KeepAlive = true;
			return httpWebRequest;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0003A434 File Offset: 0x00038634
		private static bool a(object A_0, X509Certificate A_1, X509Chain A_2, SslPolicyErrors A_3)
		{
			return true;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0004BCBC File Offset: 0x00049EBC
		public static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
		{
			Stream stream = null;
			StreamReader streamReader = null;
			string text;
			try
			{
				stream = rsp.GetResponseStream();
				if ("gzip".Equals(rsp.ContentEncoding, StringComparison.OrdinalIgnoreCase))
				{
					stream = new GZipStream(stream, CompressionMode.Decompress);
				}
				streamReader = new StreamReader(stream, encoding);
				text = streamReader.ReadToEnd();
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
				}
				if (stream != null)
				{
					stream.Close();
				}
				if (rsp != null)
				{
					rsp.Close();
				}
			}
			return text;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0004BD38 File Offset: 0x00049F38
		public static T GetRandomValue<T>(List<T> argList)
		{
			T t;
			if (argList == null || argList.Count == 0)
			{
				t = default(T);
			}
			else
			{
				Random random = new Random();
				int num = random.Next(0, argList.Count);
				T t2 = argList[num];
				t = t2;
			}
			return t;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0004BD88 File Offset: 0x00049F88
		public static decimal ToDecimal(object sourceObj)
		{
			decimal num;
			if (sourceObj == null)
			{
				num = 0m;
			}
			else
			{
				string text = sourceObj.ToString();
				if (text.Trim().Length == 0)
				{
					num = 0m;
				}
				else
				{
					try
					{
						num = decimal.Parse(text);
					}
					catch
					{
						num = 0m;
					}
				}
			}
			return num;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0004BDE8 File Offset: 0x00049FE8
		public static string Trim(object input)
		{
			string text = Convert.ToString(input);
			return string.IsNullOrEmpty(text) ? "" : text.Trim();
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0004BE14 File Offset: 0x0004A014
		public static bool TryGetSoftwarePath(string softName, out string path)
		{
			string text = string.Empty;
			string text2 = "path";
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			try
			{
				registryKey = Registry.LocalMachine;
				registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + softName.ToString() + ".exe", false);
				object obj = ((registryKey2 != null) ? registryKey2.GetValue(text2) : null);
				if (obj == null)
				{
					path = "";
					return false;
				}
				RegistryValueKind valueKind = registryKey2.GetValueKind(text2);
				if (valueKind == RegistryValueKind.String)
				{
					text = obj.ToString();
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
					registryKey = null;
				}
				if (registryKey2 != null)
				{
					registryKey2.Close();
					registryKey2 = null;
				}
			}
			bool flag;
			if (!string.IsNullOrEmpty(text))
			{
				path = text;
				flag = true;
			}
			else
			{
				path = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0004BF08 File Offset: 0x0004A108
		public static bool IsPortOccuped(int port)
		{
			bool flag = false;
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo = new ProcessStartInfo("netstat", "-an -p TCP");
					process.StartInfo.CreateNoWindow = true;
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
					process.StartInfo.RedirectStandardOutput = true;
					process.Start();
					string text = process.StandardOutput.ReadToEnd().ToLower();
					string text2 = "127.0.0.1";
					string text3 = "0.0.0.0";
					IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
					List<string> list = new List<string>();
					list.Add(text2);
					list.Add(text3);
					for (int i = 0; i < addressList.Length; i++)
					{
						list.Add(addressList[i].ToString());
					}
					for (int j = 0; j < list.Count; j++)
					{
						if (text.IndexOf("tcp") >= 0 && text.IndexOf(list[j] + ":" + port.ToString()) >= 0)
						{
							flag = true;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
			}
			return flag;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0004C088 File Offset: 0x0004A288
		public static List<int> SelectPortNotOccuped(List<int> ports)
		{
			List<int> list = new List<int>();
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo = new ProcessStartInfo("netstat", "-an -p TCP");
					process.StartInfo.CreateNoWindow = true;
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
					process.StartInfo.RedirectStandardOutput = true;
					process.Start();
					string text = process.StandardOutput.ReadToEnd().ToLower();
					string text2 = "127.0.0.1";
					string text3 = "0.0.0.0";
					IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
					List<string> list2 = new List<string>();
					list2.Add(text2);
					list2.Add(text3);
					for (int i = 0; i < addressList.Length; i++)
					{
						list2.Add(addressList[i].ToString());
					}
					using (List<int>.Enumerator enumerator = ports.GetEnumerator())
					{
						IL_0147:
						while (enumerator.MoveNext())
						{
							int num = enumerator.Current;
							bool flag = false;
							int j = 0;
							while (j < list2.Count)
							{
								if (text.IndexOf("tcp") < 0 || text.IndexOf(list2[j] + ":" + num.ToString()) < 0)
								{
									j++;
								}
								else
								{
									flag = true;
									IL_0138:
									if (!flag)
									{
										list.Add(num);
										goto IL_0147;
									}
									goto IL_0147;
								}
							}
							goto IL_0138;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
			}
			return list;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0004C26C File Offset: 0x0004A46C
		public static List<int> GetProcessIdByPorts(params int[] ports)
		{
			List<int> list = new List<int>();
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo = new ProcessStartInfo
					{
						Arguments = "-a -n -o",
						FileName = "netstat.exe",
						UseShellExecute = false,
						WindowStyle = ProcessWindowStyle.Hidden,
						RedirectStandardInput = true,
						RedirectStandardOutput = true,
						RedirectStandardError = true
					};
					process.Start();
					StreamReader standardOutput = process.StandardOutput;
					StreamReader standardError = process.StandardError;
					string text = standardOutput.ReadToEnd() + standardError.ReadToEnd();
					string text2 = process.ExitCode.ToString();
					if (!(text2 != "0"))
					{
					}
					string[] array = Regex.Split(text, "\r\n");
					foreach (string text3 in array)
					{
						string[] array3 = Regex.Split(text3.Trim(), "\\s+");
						if (array3.Length > 4 && (array3[0].Equals("UDP") || array3[0].Equals("TCP")) && "LISTENING".Equals(array3[3], StringComparison.OrdinalIgnoreCase))
						{
							int num = Util.ToInt(Regex.Replace(array3[1], "(\\d{1,3}\\.){3}\\d{1,3}:", ""));
							if (num > 0 && ports.Contains(num))
							{
								int num2 = Util.ToInt(array3[4]);
								if (num2 > 0)
								{
									list.Add(num2);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog("获取端口被占用的进程Id列表失败，" + ex.ToString(), 1);
			}
			return list.Distinct<int>().ToList<int>();
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0004C454 File Offset: 0x0004A654
		public static string StrConvSimple(string initValue)
		{
			string text;
			if (string.IsNullOrEmpty(initValue))
			{
				text = "";
			}
			else
			{
				text = Strings.StrConv(initValue, VbStrConv.SimplifiedChinese, 0).Replace("説", "说").Replace("綉", "绣")
					.Replace("濜", "浕");
			}
			return text;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0004C4B4 File Offset: 0x0004A6B4
		public static string StrConvTraditional(string initValue)
		{
			string text;
			if (string.IsNullOrEmpty(initValue))
			{
				text = "";
			}
			else
			{
				text = Strings.StrConv(initValue, VbStrConv.TraditionalChinese, 0);
			}
			return text;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0004C4E0 File Offset: 0x0004A6E0
		public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
		{
			Image image = Image.FromFile(originalImagePath);
			int num = width;
			int num2 = height;
			int num3 = 0;
			int num4 = 0;
			int num5 = image.Width;
			int num6 = image.Height;
			if (!(mode == "HW"))
			{
				if (!(mode == "W"))
				{
					if (!(mode == "H"))
					{
						if (mode == "Cut")
						{
							if ((double)image.Width / (double)image.Height > (double)num / (double)num2)
							{
								num6 = image.Height;
								num5 = image.Height * num / num2;
								num4 = 0;
								num3 = (image.Width - num5) / 2;
							}
							else
							{
								num5 = image.Width;
								num6 = image.Width * height / num;
								num3 = 0;
								num4 = (image.Height - num6) / 2;
							}
						}
					}
					else
					{
						num = image.Width * height / image.Height;
					}
				}
				else
				{
					num2 = image.Height * width / image.Width;
				}
			}
			Image image2 = new Bitmap(num, num2);
			Graphics graphics = Graphics.FromImage(image2);
			graphics.InterpolationMode = InterpolationMode.High;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.Clear(Color.Transparent);
			graphics.DrawImage(image, new global::System.Drawing.Rectangle(0, 0, num, num2), new global::System.Drawing.Rectangle(num3, num4, num5, num6), GraphicsUnit.Pixel);
			try
			{
				image2.Save(thumbnailPath, ImageFormat.Jpeg);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				image.Dispose();
				image2.Dispose();
				graphics.Dispose();
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000043ED File Offset: 0x000025ED
		public static bool IsEmptyList<T>(IEnumerable<T> list)
		{
			return list == null || list.Count<T>() <= 0;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0004C674 File Offset: 0x0004A874
		public static SortedDictionary<ArActionType, List<object>> Merge(params SortedDictionary<ArActionType, List<object>>[] args)
		{
			SortedDictionary<ArActionType, List<object>> sortedDictionary;
			if (args == null)
			{
				sortedDictionary = null;
			}
			else
			{
				SortedDictionary<ArActionType, List<object>> sortedDictionary2 = null;
				foreach (SortedDictionary<ArActionType, List<object>> sortedDictionary3 in args)
				{
					if (sortedDictionary3 != null)
					{
						if (sortedDictionary2 == null)
						{
							sortedDictionary2 = sortedDictionary3;
						}
						else
						{
							foreach (KeyValuePair<ArActionType, List<object>> keyValuePair in sortedDictionary3)
							{
								if (!sortedDictionary2.ContainsKey(keyValuePair.Key))
								{
									sortedDictionary2.Add(keyValuePair.Key, keyValuePair.Value);
								}
								else
								{
									ArActionType key = keyValuePair.Key;
									ArActionType arActionType = key;
									if (arActionType <= ArActionType.Tiqu)
									{
										if (arActionType != ArActionType.ReplyMsg)
										{
											if (arActionType != ArActionType.Tiqu)
											{
											}
										}
										else if (keyValuePair.Value != null)
										{
											sortedDictionary2[keyValuePair.Key].AddRange(keyValuePair.Value);
										}
										else
										{
											sortedDictionary2[keyValuePair.Key] = new List<object> { keyValuePair.Value };
										}
									}
									else if (arActionType == ArActionType.AppointTransferCall || arActionType != ArActionType.TransferCall)
									{
									}
								}
							}
						}
					}
				}
				sortedDictionary = sortedDictionary2;
			}
			return sortedDictionary;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0004C7B4 File Offset: 0x0004A9B4
		public static void CollectPicMd5(Bitmap screenshot, string fileNamePrefix = "")
		{
			if (screenshot != null)
			{
				string text = Util.ComputeHashMd5(screenshot);
				if (!Util.b.Contains(text))
				{
					try
					{
						if (!Directory.Exists(Util.c))
						{
							Directory.CreateDirectory(Util.c);
						}
						string text2 = Util.c + fileNamePrefix + text + ".png";
						if (!File.Exists(text2))
						{
							screenshot.Save(text2, ImageFormat.Png);
						}
						Util.b.Add(text);
						if (!string.IsNullOrEmpty(fileNamePrefix) && fileNamePrefix.StartsWith("黑名单_"))
						{
							LogWriter.WriteLog("黑名单买家，" + text, 1);
						}
					}
					catch (Exception ex)
					{
						LogWriter.WriteLog("保存图片出错" + ex.ToString(), 1);
					}
				}
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0004C88C File Offset: 0x0004AA8C
		public static void CollecPicMd5(WindowInfo win, string fileNamePrefix = "")
		{
			using (Bitmap bitmapFromDC = win.GetBitmapFromDC(0))
			{
				Util.CollectPicMd5(bitmapFromDC, fileNamePrefix);
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0004C8C4 File Offset: 0x0004AAC4
		public static Dictionary<string, List<Agiso.Utils.ProcessInfo>> GetProcessInfoDictByProcess()
		{
			Dictionary<string, List<Agiso.Utils.ProcessInfo>> dictionary = new Dictionary<string, List<Agiso.Utils.ProcessInfo>>();
			List<Process> list = Process.GetProcessesByName("AliWorkbench").ToList<Process>();
			foreach (Process process in list)
			{
				using (process)
				{
					if (process != null && process.MainWindowHandle != IntPtr.Zero)
					{
						string text = Path.Combine(Path.GetDirectoryName(process.MainModule.FileName), "AliWorkbench.ini");
						string versionByIniFileName = Util.GetVersionByIniFileName(text);
						if (!dictionary.ContainsKey(versionByIniFileName))
						{
							dictionary[versionByIniFileName] = new List<Agiso.Utils.ProcessInfo>();
						}
						dictionary[versionByIniFileName].Add(new Agiso.Utils.ProcessInfo
						{
							Id = process.Id,
							MainModuleFileName = process.MainModule.FileName,
							MainWindowHandle = process.MainWindowHandle,
							Version = versionByIniFileName
						});
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0004C9EC File Offset: 0x0004ABEC
		public static List<string> GetVersionListByRegedit(out string path)
		{
			List<string> list = new List<string>();
			if (Util.TryGetSoftwarePath("AliWorkbench", out path))
			{
				list = Util.GetVersionListUnderPath(path);
			}
			return list;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0004CA18 File Offset: 0x0004AC18
		public static List<string> GetVersionListUnderPath(string path)
		{
			List<string> list = new List<string>();
			if (Directory.Exists(path))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				DirectoryInfo[] directories = directoryInfo.GetDirectories();
				if (directories.Length != 0)
				{
					Regex regex = new Regex("\\d+?\\.\\d+?\\.\\d+?N");
					foreach (DirectoryInfo directoryInfo2 in directories)
					{
						if (regex.IsMatch(directoryInfo2.Name))
						{
							list.Add(directoryInfo2.Name);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0004CA94 File Offset: 0x0004AC94
		public static string GetVersion()
		{
			string text = null;
			if (AppConfig.AllowAutoLogin)
			{
				text = AppConfig.AgentQnSetupIniFileName;
			}
			string text2;
			if (text == null)
			{
				List<Process> list = Process.GetProcessesByName("AliWorkbench").ToList<Process>();
				foreach (Process process in list)
				{
					using (process)
					{
						if (process != null && process.MainWindowHandle != IntPtr.Zero && string.IsNullOrEmpty(text))
						{
							text = Path.Combine(Path.GetDirectoryName(process.MainModule.FileName), "AliWorkbench.ini");
						}
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					text2 = Util.GetVersionByIniFileName(text);
				}
				else
				{
					text2 = "";
				}
			}
			else
			{
				text2 = Util.GetVersionByIniFileName(text);
			}
			return text2;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0004CB8C File Offset: 0x0004AD8C
		public static string GetVersionByIniFileName(string iniFileName)
		{
			string text;
			if (string.IsNullOrEmpty(iniFileName))
			{
				text = "";
			}
			else if (!File.Exists(iniFileName))
			{
				text = "";
			}
			else
			{
				try
				{
					using (StreamReader streamReader = new StreamReader(iniFileName))
					{
						while (!streamReader.EndOfStream)
						{
							string text2 = streamReader.ReadLine();
							if (text2.ToLower().Trim().StartsWith("version", StringComparison.OrdinalIgnoreCase))
							{
								string[] array = text2.Split(new char[] { '=' });
								if (array.Length == 2)
								{
									return array[1].Trim();
								}
							}
						}
					}
				}
				catch
				{
				}
				text = "";
			}
			return text;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0004CC54 File Offset: 0x0004AE54
		public static List<T> Sort<T>(List<T> list, string sortName, string sortType)
		{
			List<T> list2;
			if (list == null || list.Count == 1)
			{
				list2 = list;
			}
			else
			{
				Type typeFromHandle = typeof(T);
				PropertyInfo property = typeFromHandle.GetProperty(sortName);
				if (property == null)
				{
					list2 = list;
				}
				else if (!(property.PropertyType.GetInterface("IComparable") != null))
				{
					list2 = list;
				}
				else
				{
					MethodInfo method = property.PropertyType.GetMethod("CompareTo", new Type[] { typeof(object) });
					List<T> list3 = list.Select(new Func<T, T>(Util.<>c__120<T>.<>9.a)).ToList<T>();
					for (int i = 0; i < list3.Count - 1; i++)
					{
						T t = list3[i];
						object value = property.GetValue(list3[i]);
						int num = i;
						for (int j = i + 1; j < list3.Count; j++)
						{
							object value2 = property.GetValue(list3[j]);
							if (sortType.Equals("desc", StringComparison.OrdinalIgnoreCase))
							{
								if ((int)method.Invoke(value, new object[] { value2 }) < 0)
								{
									t = list3[j];
									num = j;
								}
							}
							else if ((int)method.Invoke(value, new object[] { value2 }) > 0)
							{
								t = list3[j];
								num = j;
							}
						}
						list3[num] = list3[i];
						list3[i] = t;
					}
					list2 = list3;
				}
			}
			return list2;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0004CE1C File Offset: 0x0004B01C
		public static bool IsBlankImg(Image img)
		{
			bool flag;
			if (img == null)
			{
				flag = false;
			}
			else
			{
				using (Bitmap bitmap = new Bitmap(img))
				{
					int num = img.Height * img.Width;
					Color pixel = bitmap.GetPixel(0, 0);
					for (int i = 0; i < img.Height; i++)
					{
						for (int j = 0; j < img.Width; j++)
						{
							Color pixel2 = bitmap.GetPixel(j, i);
							if (pixel2.A != pixel.A || pixel2.R != pixel.R || pixel2.G != pixel.G || pixel2.B != pixel.B)
							{
								return false;
							}
						}
					}
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0004CEFC File Offset: 0x0004B0FC
		public static bool UseCmd(string strCMD)
		{
			string text;
			return Util.UseCmd(strCMD, false, out text);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0004CF14 File Offset: 0x0004B114
		public static bool UseCmd(string strCMD, out string outputStr)
		{
			return Util.UseCmd(strCMD, true, out outputStr);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0004CF2C File Offset: 0x0004B12C
		public static bool UseCmd(string strCMD, bool readOupStr, out string outputStr)
		{
			outputStr = "";
			bool flag;
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo.FileName = "cmd.exe";
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.RedirectStandardInput = true;
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.CreateNoWindow = true;
					process.Start();
					process.StandardInput.WriteLine(strCMD);
					process.StandardInput.Flush();
					process.StandardInput.Close();
					if (readOupStr)
					{
						outputStr = process.StandardOutput.ReadToEnd();
					}
					if (!process.WaitForExit(2000))
					{
						Win32Extend.KillProcess(process);
					}
				}
				flag = true;
			}
			catch (Win32Exception ex)
			{
				if (ex.Message.Contains("页面文件太小，无法完成操作"))
				{
					AppConfig.ClearMemorySilent("页面文件太小，清理下内存");
					Thread.Sleep(1000);
				}
				LogWriter.WriteLog(ex.ToString(), 1);
				flag = false;
			}
			catch (Exception ex2)
			{
				LogWriter.WriteLog(ex2.ToString(), 1);
				flag = false;
			}
			return flag;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00004401 File Offset: 0x00002601
		public static Regex NumRegex { get; } = new Regex("^\\d+$");

		// Token: 0x06000747 RID: 1863 RVA: 0x0004D060 File Offset: 0x0004B260
		public static bool IsNum(string value)
		{
			return !string.IsNullOrEmpty(value) && Util.d.IsMatch(value);
		}

		// Token: 0x040004D2 RID: 1234
		private static string[] a = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };

		// Token: 0x040004D3 RID: 1235
		private static List<string> b = new List<string>();

		// Token: 0x040004D4 RID: 1236
		private static string c = AppDomain.CurrentDomain.BaseDirectory + "\\ScreenshotMd5\\";

		// Token: 0x040004D5 RID: 1237
		[CompilerGenerated]
		private static readonly Regex d;
	}
}
