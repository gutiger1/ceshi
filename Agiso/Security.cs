using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Agiso
{
	// Token: 0x020000D7 RID: 215
	public abstract class Security
	{
		// Token: 0x0600066A RID: 1642 RVA: 0x000470CC File Offset: 0x000452CC
		public static string Decrypt(string pToDecrypt, string sKey = "By Agiso")
		{
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			byte[] array = new byte[pToDecrypt.Length / 2];
			for (int i = 0; i < pToDecrypt.Length / 2; i++)
			{
				int num = Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 16);
				array[i] = (byte)num;
			}
			descryptoServiceProvider.Key = Encoding.ASCII.GetBytes(sKey);
			descryptoServiceProvider.IV = Encoding.ASCII.GetBytes(sKey);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(array, 0, array.Length);
			cryptoStream.FlushFinalBlock();
			new StringBuilder();
			return Encoding.Default.GetString(memoryStream.ToArray());
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00047184 File Offset: 0x00045384
		public static string GetKeyString(string cookieStr)
		{
			string text = "V";
			string text2 = "s";
			string text3 = "L";
			string text4 = "9";
			string text5 = "u";
			string text6 = "7";
			string text7 = "T";
			string text8 = "0";
			string text9 = string.Concat(new string[] { text, text2, text3, text4, text5, text6, text7, text8 });
			if (!string.IsNullOrEmpty(cookieStr))
			{
				string[] array = cookieStr.Split(new char[] { ';' });
				foreach (string text10 in array)
				{
					string[] array3 = text10.Split(new char[] { '=' });
					if (array3.Length > 1 && array3[0].Trim().ToLower() == "existshop")
					{
						if (array3[1].Trim().Length < 8)
						{
							array3[1] = array3[1].Trim() + text9;
						}
						return array3[1].Trim().Substring(0, 8);
					}
				}
			}
			return text9;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000472BC File Offset: 0x000454BC
		public static bool IsAdministrator()
		{
			WindowsIdentity current = WindowsIdentity.GetCurrent();
			WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
			return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
		}
	}
}
