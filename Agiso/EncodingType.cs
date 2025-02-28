using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Agiso
{
	// Token: 0x020000D4 RID: 212
	public class EncodingType
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x00046E30 File Offset: 0x00045030
		public static Encoding GetType(string FILE_NAME)
		{
			FileStream fileStream = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read);
			Encoding type = EncodingType.GetType(fileStream);
			fileStream.Close();
			return type;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00046E58 File Offset: 0x00045058
		public static Encoding GetType(FileStream fs)
		{
			RuntimeHelpers.InitializeArray(new byte[3], fieldof(p.b).FieldHandle);
			byte[] array = new byte[3];
			array[0] = 254;
			array[1] = byte.MaxValue;
			RuntimeHelpers.InitializeArray(new byte[3], fieldof(p.c).FieldHandle);
			Encoding encoding = Encoding.Default;
			BinaryReader binaryReader = new BinaryReader(fs, Encoding.Default);
			int num;
			int.TryParse(fs.Length.ToString(), out num);
			byte[] array2 = binaryReader.ReadBytes(num);
			if (EncodingType.a(array2) || (array2[0] == 239 && array2[1] == 187 && array2[2] == 191))
			{
				encoding = Encoding.UTF8;
			}
			else if (array2[0] == 254 && array2[1] == 255 && array2[2] == 0)
			{
				encoding = Encoding.BigEndianUnicode;
			}
			else if (array2[0] == 255 && array2[1] == 254 && array2[2] == 65)
			{
				encoding = Encoding.Unicode;
			}
			binaryReader.Close();
			return encoding;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00046F70 File Offset: 0x00045170
		private static bool a(byte[] A_0)
		{
			int num = 1;
			foreach (byte b in A_0)
			{
				if (num == 1)
				{
					if (b >= 128)
					{
						while (((b = (byte)(b << 1)) & 128) > 0)
						{
							num++;
						}
						if (num == 1 || num > 6)
						{
							return false;
						}
					}
				}
				else
				{
					if ((b & 192) != 128)
					{
						return false;
					}
					num--;
				}
			}
			if (num > 1)
			{
				throw new Exception("非预期的byte格式");
			}
			return true;
		}
	}
}
