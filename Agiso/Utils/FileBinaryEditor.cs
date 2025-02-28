using System;
using System.IO;
using System.Text;

namespace Agiso.Utils
{
	// Token: 0x020000E4 RID: 228
	public class FileBinaryEditor : IDisposable
	{
		// Token: 0x060006B4 RID: 1716 RVA: 0x00048B38 File Offset: 0x00046D38
		private byte[] a()
		{
			if (!this.b)
			{
				using (FileStream fileStream = new FileStream(this.a, FileMode.Open, FileAccess.Read))
				{
					using (BinaryReader binaryReader = new BinaryReader(fileStream))
					{
						this.c = binaryReader.ReadBytes((int)fileStream.Length);
					}
				}
				this.b = true;
			}
			return this.c;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00004399 File Offset: 0x00002599
		public FileBinaryEditor(string fileName)
		{
			this.a = fileName;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00048BBC File Offset: 0x00046DBC
		public bool FileReplaceStr(string searchStr, string newReplaceStr, int offset = 0, bool autoBackup = true)
		{
			bool flag;
			if (searchStr == newReplaceStr)
			{
				flag = true;
			}
			else
			{
				byte[] bytes = Encoding.UTF8.GetBytes(searchStr);
				byte[] bytes2 = Encoding.UTF8.GetBytes(newReplaceStr);
				byte[] array;
				try
				{
					array = this.a();
				}
				catch (Exception ex)
				{
					throw new Exception("打开原文件时异常", ex);
				}
				byte[] array2;
				if (!FileBinaryEditor.a(array, bytes, bytes2, out array2, offset))
				{
					flag = false;
				}
				else
				{
					try
					{
						using (new FileStream(this.a, FileMode.Truncate, FileAccess.ReadWrite))
						{
						}
						using (FileStream fileStream2 = new FileStream(this.a, FileMode.OpenOrCreate, FileAccess.ReadWrite))
						{
							fileStream2.Write(array2, 0, array2.Length);
						}
					}
					catch (Exception ex2)
					{
						throw new Exception("载入新文件时异常", ex2);
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00048CB4 File Offset: 0x00046EB4
		public int FileIndexOf(string searchStr, int offset = 0)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(searchStr);
			byte[] array = this.a();
			return FileBinaryEditor.a(array, bytes, offset);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00048CE0 File Offset: 0x00046EE0
		internal static int a(byte[] A_0, byte[] A_1, int A_2 = 0)
		{
			int num;
			if (A_2 == -1)
			{
				num = -1;
			}
			else if (A_0 == null)
			{
				num = -1;
			}
			else if (A_1 == null)
			{
				num = -1;
			}
			else if (A_0.Length == 0)
			{
				num = -1;
			}
			else if (A_1.Length == 0)
			{
				num = -1;
			}
			else if (A_0.Length < A_1.Length)
			{
				num = -1;
			}
			else
			{
				for (int i = A_2; i < A_0.Length - A_1.Length; i++)
				{
					if (A_0[i] == A_1[0])
					{
						if (A_1.Length != 1)
						{
							bool flag = true;
							int j = 1;
							while (j < A_1.Length)
							{
								if (A_0[i + j] == A_1[j])
								{
									j++;
								}
								else
								{
									flag = false;
									IL_0097:
									if (!flag)
									{
										goto IL_009B;
									}
									return i;
								}
							}
							goto IL_0097;
						}
						return i;
					}
					IL_009B:;
				}
				num = -1;
			}
			return num;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00048DA8 File Offset: 0x00046FA8
		internal static bool a(byte[] A_0, byte[] A_1, byte[] A_2, out byte[] A_3, int A_4 = 0)
		{
			if (A_1.Length != A_2.Length)
			{
				throw new Exception("替换的前后字节长度必须一致！");
			}
			int num = FileBinaryEditor.a(A_0, A_1, A_4);
			bool flag;
			if (num < 0)
			{
				A_3 = A_0;
				flag = false;
			}
			else
			{
				int num2 = A_2.Length - A_1.Length;
				A_3 = new byte[A_0.Length + num2];
				for (int i = 0; i < num; i++)
				{
					A_3[i] = A_0[i];
				}
				for (int j = 0; j < A_2.Length; j++)
				{
					A_3[j + num] = A_2[j];
				}
				for (int k = num + A_2.Length; k < A_3.Length; k++)
				{
					A_3[k] = A_0[k - num2];
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000043B0 File Offset: 0x000025B0
		public void Dispose()
		{
			this.c = null;
			this.b = false;
			GC.Collect();
		}

		// Token: 0x040004CD RID: 1229
		private string a;

		// Token: 0x040004CE RID: 1230
		private bool b = false;

		// Token: 0x040004CF RID: 1231
		private byte[] c;
	}
}
