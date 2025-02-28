using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Agiso.Utils;

namespace Agiso.Extensions
{
	// Token: 0x020006FE RID: 1790
	public static class ObjectExtension
	{
		// Token: 0x06002337 RID: 9015 RVA: 0x00059FAC File Offset: 0x000581AC
		public static string smethod_0(this string data, string key, string iv)
		{
			string text;
			try
			{
				if (!string.IsNullOrEmpty(data))
				{
					byte[] bytes = Encoding.ASCII.GetBytes(key);
					byte[] bytes2 = Encoding.ASCII.GetBytes(iv);
					using (DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider())
					{
						int keySize = descryptoServiceProvider.KeySize;
						using (MemoryStream memoryStream = new MemoryStream())
						{
							using (CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateEncryptor(bytes, bytes2), CryptoStreamMode.Write))
							{
								using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
								{
									streamWriter.Write(data);
									streamWriter.Flush();
									cryptoStream.FlushFinalBlock();
									streamWriter.Flush();
									return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
								}
							}
						}
					}
				}
				text = "";
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
				text = data;
			}
			return text;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x0005A0D4 File Offset: 0x000582D4
		public static string smethod_1(this string data, string key, string iv)
		{
			string text;
			try
			{
				if (!string.IsNullOrEmpty(data))
				{
					byte[] bytes = Encoding.ASCII.GetBytes(key);
					byte[] bytes2 = Encoding.ASCII.GetBytes(iv);
					byte[] array;
					try
					{
						array = Convert.FromBase64String(data);
					}
					catch
					{
						return null;
					}
					using (DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider())
					{
						using (MemoryStream memoryStream = new MemoryStream(array))
						{
							using (CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateDecryptor(bytes, bytes2), CryptoStreamMode.Read))
							{
								using (StreamReader streamReader = new StreamReader(cryptoStream))
								{
									return streamReader.ReadToEnd();
								}
							}
						}
					}
				}
				text = "";
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
				text = data;
			}
			return text;
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x0005A1EC File Offset: 0x000583EC
		public static bool IsActivateMsg(this string data)
		{
			return !string.IsNullOrWhiteSpace(data) && data.StartsWith("AGISO#QN_AGENT_ACTIVATE_TYPE");
		}
	}
}
