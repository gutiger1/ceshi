using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

// Token: 0x0200076F RID: 1903
internal class Class3
{
	// Token: 0x0600262A RID: 9770 RVA: 0x0006FAF4 File Offset: 0x0006DCF4
	static Class3()
	{
		try
		{
			RSACryptoServiceProvider.UseMachineKeyStore = true;
		}
		catch
		{
		}
	}

	// Token: 0x0600262B RID: 9771 RVA: 0x000022DD File Offset: 0x000004DD
	private void deZB7v6mRchPO()
	{
	}

	// Token: 0x0600262C RID: 9772 RVA: 0x0006FC28 File Offset: 0x0006DE28
	internal static byte[] creoiNvd7(byte[] byte_4)
	{
		uint[] array = new uint[16];
		int num = 448 - byte_4.Length * 8 % 512;
		uint num2 = (uint)((num + 512) % 512);
		if (num2 == 0U)
		{
			num2 = 512U;
		}
		uint num3 = (uint)((long)byte_4.Length + (long)((ulong)(num2 / 8U)) + 8L);
		ulong num4 = (ulong)((long)byte_4.Length * 8L);
		byte[] array2 = new byte[num3];
		for (int i = 0; i < byte_4.Length; i++)
		{
			array2[i] = byte_4[i];
		}
		byte[] array3 = array2;
		int num5 = byte_4.Length;
		array3[num5] |= 128;
		for (int j = 8; j > 0; j--)
		{
			array2[(int)(checked((IntPtr)(unchecked((ulong)num3 - (ulong)((long)j)))))] = (byte)((num4 >> (8 - j) * 8) & 255UL);
		}
		uint num6 = (uint)(array2.Length * 8 / 32);
		uint num7 = 1732584193U;
		uint num8 = 4023233417U;
		uint num9 = 2562383102U;
		uint num10 = 271733878U;
		for (uint num11 = 0U; num11 < num6 / 16U; num11 += 1U)
		{
			uint num12 = num11 << 6;
			for (uint num13 = 0U; num13 < 61U; num13 += 4U)
			{
				array[(int)((UIntPtr)(num13 >> 2))] = (uint)(((int)array2[(int)((UIntPtr)(num12 + (num13 + 3U)))] << 24) | ((int)array2[(int)((UIntPtr)(num12 + (num13 + 2U)))] << 16) | ((int)array2[(int)((UIntPtr)(num12 + (num13 + 1U)))] << 8) | (int)array2[(int)((UIntPtr)(num12 + num13))]);
			}
			uint num14 = num7;
			uint num15 = num8;
			uint num16 = num9;
			uint num17 = num10;
			Class3.smethod_0(ref num7, num8, num9, num10, 0U, 7, 1U, array);
			Class3.smethod_0(ref num10, num7, num8, num9, 1U, 12, 2U, array);
			Class3.smethod_0(ref num9, num10, num7, num8, 2U, 17, 3U, array);
			Class3.smethod_0(ref num8, num9, num10, num7, 3U, 22, 4U, array);
			Class3.smethod_0(ref num7, num8, num9, num10, 4U, 7, 5U, array);
			Class3.smethod_0(ref num10, num7, num8, num9, 5U, 12, 6U, array);
			Class3.smethod_0(ref num9, num10, num7, num8, 6U, 17, 7U, array);
			Class3.smethod_0(ref num8, num9, num10, num7, 7U, 22, 8U, array);
			Class3.smethod_0(ref num7, num8, num9, num10, 8U, 7, 9U, array);
			Class3.smethod_0(ref num10, num7, num8, num9, 9U, 12, 10U, array);
			Class3.smethod_0(ref num9, num10, num7, num8, 10U, 17, 11U, array);
			Class3.smethod_0(ref num8, num9, num10, num7, 11U, 22, 12U, array);
			Class3.smethod_0(ref num7, num8, num9, num10, 12U, 7, 13U, array);
			Class3.smethod_0(ref num10, num7, num8, num9, 13U, 12, 14U, array);
			Class3.smethod_0(ref num9, num10, num7, num8, 14U, 17, 15U, array);
			Class3.smethod_0(ref num8, num9, num10, num7, 15U, 22, 16U, array);
			Class3.smethod_1(ref num7, num8, num9, num10, 1U, 5, 17U, array);
			Class3.smethod_1(ref num10, num7, num8, num9, 6U, 9, 18U, array);
			Class3.smethod_1(ref num9, num10, num7, num8, 11U, 14, 19U, array);
			Class3.smethod_1(ref num8, num9, num10, num7, 0U, 20, 20U, array);
			Class3.smethod_1(ref num7, num8, num9, num10, 5U, 5, 21U, array);
			Class3.smethod_1(ref num10, num7, num8, num9, 10U, 9, 22U, array);
			Class3.smethod_1(ref num9, num10, num7, num8, 15U, 14, 23U, array);
			Class3.smethod_1(ref num8, num9, num10, num7, 4U, 20, 24U, array);
			Class3.smethod_1(ref num7, num8, num9, num10, 9U, 5, 25U, array);
			Class3.smethod_1(ref num10, num7, num8, num9, 14U, 9, 26U, array);
			Class3.smethod_1(ref num9, num10, num7, num8, 3U, 14, 27U, array);
			Class3.smethod_1(ref num8, num9, num10, num7, 8U, 20, 28U, array);
			Class3.smethod_1(ref num7, num8, num9, num10, 13U, 5, 29U, array);
			Class3.smethod_1(ref num10, num7, num8, num9, 2U, 9, 30U, array);
			Class3.smethod_1(ref num9, num10, num7, num8, 7U, 14, 31U, array);
			Class3.smethod_1(ref num8, num9, num10, num7, 12U, 20, 32U, array);
			Class3.smethod_2(ref num7, num8, num9, num10, 5U, 4, 33U, array);
			Class3.smethod_2(ref num10, num7, num8, num9, 8U, 11, 34U, array);
			Class3.smethod_2(ref num9, num10, num7, num8, 11U, 16, 35U, array);
			Class3.smethod_2(ref num8, num9, num10, num7, 14U, 23, 36U, array);
			Class3.smethod_2(ref num7, num8, num9, num10, 1U, 4, 37U, array);
			Class3.smethod_2(ref num10, num7, num8, num9, 4U, 11, 38U, array);
			Class3.smethod_2(ref num9, num10, num7, num8, 7U, 16, 39U, array);
			Class3.smethod_2(ref num8, num9, num10, num7, 10U, 23, 40U, array);
			Class3.smethod_2(ref num7, num8, num9, num10, 13U, 4, 41U, array);
			Class3.smethod_2(ref num10, num7, num8, num9, 0U, 11, 42U, array);
			Class3.smethod_2(ref num9, num10, num7, num8, 3U, 16, 43U, array);
			Class3.smethod_2(ref num8, num9, num10, num7, 6U, 23, 44U, array);
			Class3.smethod_2(ref num7, num8, num9, num10, 9U, 4, 45U, array);
			Class3.smethod_2(ref num10, num7, num8, num9, 12U, 11, 46U, array);
			Class3.smethod_2(ref num9, num10, num7, num8, 15U, 16, 47U, array);
			Class3.smethod_2(ref num8, num9, num10, num7, 2U, 23, 48U, array);
			Class3.smethod_3(ref num7, num8, num9, num10, 0U, 6, 49U, array);
			Class3.smethod_3(ref num10, num7, num8, num9, 7U, 10, 50U, array);
			Class3.smethod_3(ref num9, num10, num7, num8, 14U, 15, 51U, array);
			Class3.smethod_3(ref num8, num9, num10, num7, 5U, 21, 52U, array);
			Class3.smethod_3(ref num7, num8, num9, num10, 12U, 6, 53U, array);
			Class3.smethod_3(ref num10, num7, num8, num9, 3U, 10, 54U, array);
			Class3.smethod_3(ref num9, num10, num7, num8, 10U, 15, 55U, array);
			Class3.smethod_3(ref num8, num9, num10, num7, 1U, 21, 56U, array);
			Class3.smethod_3(ref num7, num8, num9, num10, 8U, 6, 57U, array);
			Class3.smethod_3(ref num10, num7, num8, num9, 15U, 10, 58U, array);
			Class3.smethod_3(ref num9, num10, num7, num8, 6U, 15, 59U, array);
			Class3.smethod_3(ref num8, num9, num10, num7, 13U, 21, 60U, array);
			Class3.smethod_3(ref num7, num8, num9, num10, 4U, 6, 61U, array);
			Class3.smethod_3(ref num10, num7, num8, num9, 11U, 10, 62U, array);
			Class3.smethod_3(ref num9, num10, num7, num8, 2U, 15, 63U, array);
			Class3.smethod_3(ref num8, num9, num10, num7, 9U, 21, 64U, array);
			num7 += num14;
			num8 += num15;
			num9 += num16;
			num10 += num17;
		}
		byte[] array4 = new byte[16];
		Array.Copy(BitConverter.GetBytes(num7), 0, array4, 0, 4);
		Array.Copy(BitConverter.GetBytes(num8), 0, array4, 4, 4);
		Array.Copy(BitConverter.GetBytes(num9), 0, array4, 8, 4);
		Array.Copy(BitConverter.GetBytes(num10), 0, array4, 12, 4);
		return array4;
	}

	// Token: 0x0600262D RID: 9773 RVA: 0x0000F51E File Offset: 0x0000D71E
	private static void smethod_0(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, uint[] uint_7)
	{
		uint_1 = uint_2 + Class3.smethod_4(uint_1 + ((uint_2 & uint_3) | (~uint_2 & uint_4)) + uint_7[(int)((UIntPtr)uint_5)] + Class3.uint_0[(int)((UIntPtr)(uint_6 - 1U))], ushort_0);
	}

	// Token: 0x0600262E RID: 9774 RVA: 0x0000F549 File Offset: 0x0000D749
	private static void smethod_1(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, uint[] uint_7)
	{
		uint_1 = uint_2 + Class3.smethod_4(uint_1 + ((uint_2 & uint_4) | (uint_3 & ~uint_4)) + uint_7[(int)((UIntPtr)uint_5)] + Class3.uint_0[(int)((UIntPtr)(uint_6 - 1U))], ushort_0);
	}

	// Token: 0x0600262F RID: 9775 RVA: 0x0000F574 File Offset: 0x0000D774
	private static void smethod_2(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, uint[] uint_7)
	{
		uint_1 = uint_2 + Class3.smethod_4(uint_1 + (uint_2 ^ uint_3 ^ uint_4) + uint_7[(int)((UIntPtr)uint_5)] + Class3.uint_0[(int)((UIntPtr)(uint_6 - 1U))], ushort_0);
	}

	// Token: 0x06002630 RID: 9776 RVA: 0x0000F59C File Offset: 0x0000D79C
	private static void smethod_3(ref uint uint_1, uint uint_2, uint uint_3, uint uint_4, uint uint_5, ushort ushort_0, uint uint_6, uint[] uint_7)
	{
		uint_1 = uint_2 + Class3.smethod_4(uint_1 + (uint_3 ^ (uint_2 | ~uint_4)) + uint_7[(int)((UIntPtr)uint_5)] + Class3.uint_0[(int)((UIntPtr)(uint_6 - 1U))], ushort_0);
	}

	// Token: 0x06002631 RID: 9777 RVA: 0x0000F5C5 File Offset: 0x0000D7C5
	private static uint smethod_4(uint uint_1, ushort ushort_0)
	{
		return (uint_1 >> (int)(32 - ushort_0)) | (uint_1 << (int)ushort_0);
	}

	// Token: 0x06002632 RID: 9778 RVA: 0x0000F5D7 File Offset: 0x0000D7D7
	internal static bool smethod_5()
	{
		if (!Class3.bool_4)
		{
			Class3.smethod_6();
			Class3.bool_4 = true;
		}
		return Class3.bool_1;
	}

	// Token: 0x06002633 RID: 9779 RVA: 0x000702CC File Offset: 0x0006E4CC
	internal static SymmetricAlgorithm SuhhReBcy()
	{
		SymmetricAlgorithm symmetricAlgorithm = null;
		if (Class3.smethod_5())
		{
			symmetricAlgorithm = new AesCryptoServiceProvider();
		}
		else
		{
			try
			{
				symmetricAlgorithm = new RijndaelManaged();
			}
			catch
			{
				symmetricAlgorithm = (SymmetricAlgorithm)Activator.CreateInstance("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", "System.Security.Cryptography.AesCryptoServiceProvider").Unwrap();
			}
		}
		return symmetricAlgorithm;
	}

	// Token: 0x06002634 RID: 9780 RVA: 0x00070320 File Offset: 0x0006E520
	internal static void smethod_6()
	{
		try
		{
			Class3.bool_1 = CryptoConfig.AllowOnlyFipsAlgorithms;
		}
		catch
		{
		}
	}

	// Token: 0x06002635 RID: 9781 RVA: 0x0000F5F0 File Offset: 0x0000D7F0
	internal static byte[] BjkXsyRir(byte[] byte_4)
	{
		if (!Class3.smethod_5())
		{
			return new MD5CryptoServiceProvider().ComputeHash(byte_4);
		}
		return Class3.creoiNvd7(byte_4);
	}

	// Token: 0x06002636 RID: 9782 RVA: 0x0000F60B File Offset: 0x0000D80B
	private static uint smethod_7(uint uint_1)
	{
		return (uint)"{11111-22222-10009-11112}".Length;
	}

	// Token: 0x06002637 RID: 9783 RVA: 0x00070350 File Offset: 0x0006E550
	[Class3.Attribute0(typeof(Class3.Attribute0.Class4<object>[]))]
	internal static string smethod_8(string string_1)
	{
		"{11111-22222-50001-00000}".Trim();
		byte[] array = Convert.FromBase64String(string_1);
		return Encoding.Unicode.GetString(array, 0, array.Length);
	}

	// Token: 0x06002638 RID: 9784 RVA: 0x0000F617 File Offset: 0x0000D817
	private static int smethod_9()
	{
		return 5;
	}

	// Token: 0x06002639 RID: 9785 RVA: 0x00070380 File Offset: 0x0006E580
	private static void smethod_10()
	{
		try
		{
			RSACryptoServiceProvider.UseMachineKeyStore = true;
		}
		catch
		{
		}
	}

	// Token: 0x0600263A RID: 9786 RVA: 0x000703AC File Offset: 0x0006E5AC
	private static Delegate smethod_11(IntPtr intptr_3, Type type_0)
	{
		return (Delegate)typeof(Marshal).GetMethod("GetDelegateForFunctionPointer", new Type[]
		{
			typeof(IntPtr),
			typeof(Type)
		}).Invoke(null, new object[] { intptr_3, type_0 });
	}

	// Token: 0x0600263B RID: 9787 RVA: 0x00070410 File Offset: 0x0006E610
	internal static object smethod_12(Assembly assembly_1)
	{
		try
		{
			if (File.Exists(((Assembly)assembly_1).Location))
			{
				return ((Assembly)assembly_1).Location;
			}
		}
		catch
		{
		}
		try
		{
			if (File.Exists(((Assembly)assembly_1).GetName().CodeBase.ToString().Replace("file:///", "")))
			{
				return ((Assembly)assembly_1).GetName().CodeBase.ToString().Replace("file:///", "");
			}
		}
		catch
		{
		}
		try
		{
			if (File.Exists(assembly_1.GetType().GetProperty("Location").GetValue(assembly_1, new object[0])
				.ToString()))
			{
				return assembly_1.GetType().GetProperty("Location").GetValue(assembly_1, new object[0])
					.ToString();
			}
		}
		catch
		{
		}
		return "";
	}

	// Token: 0x0600263C RID: 9788 RVA: 0x00070520 File Offset: 0x0006E720
	[Class3.Attribute0(typeof(Class3.Attribute0.Class4<object>[]))]
	private static byte[] smethod_13(string string_1)
	{
		byte[] array;
		using (FileStream fileStream = new FileStream(string_1, FileMode.Open, FileAccess.Read, FileShare.Read))
		{
			int num = 0;
			long length = fileStream.Length;
			int i = (int)length;
			array = new byte[i];
			while (i > 0)
			{
				int num2 = fileStream.Read(array, num, i);
				num += num2;
				i -= num2;
			}
		}
		return array;
	}

	// Token: 0x0600263D RID: 9789 RVA: 0x00070588 File Offset: 0x0006E788
	[Class3.Attribute0(typeof(Class3.Attribute0.Class4<object>[]))]
	private static byte[] smethod_14(byte[] byte_4)
	{
		MemoryStream memoryStream = new MemoryStream();
		SymmetricAlgorithm symmetricAlgorithm = Class3.SuhhReBcy();
		symmetricAlgorithm.Key = new byte[]
		{
			219, 251, 8, 163, 108, 221, 128, 146, 208, 181,
			245, 235, 145, 94, 92, 213, 172, 22, 190, 42,
			215, 154, 184, 147, 2, 211, 106, 0, 175, 162,
			252, 235
		};
		symmetricAlgorithm.IV = new byte[]
		{
			208, 203, 157, 20, 175, 118, 62, 211, 250, 162,
			41, 43, 214, 165, 158, 142
		};
		CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Write);
		cryptoStream.Write(byte_4, 0, byte_4.Length);
		cryptoStream.Close();
		return memoryStream.ToArray();
	}

	// Token: 0x0600263E RID: 9790 RVA: 0x000705F8 File Offset: 0x0006E7F8
	private byte[] method_0()
	{
		return null;
	}

	// Token: 0x0600263F RID: 9791 RVA: 0x00070608 File Offset: 0x0006E808
	private byte[] method_1()
	{
		return null;
	}

	// Token: 0x06002640 RID: 9792 RVA: 0x00070618 File Offset: 0x0006E818
	private byte[] method_2()
	{
		string text = "{11111-22222-20001-00001}";
		if (text.Length > 0)
		{
			return new byte[] { 1, 2 };
		}
		return new byte[] { 1, 2 };
	}

	// Token: 0x06002641 RID: 9793 RVA: 0x00070658 File Offset: 0x0006E858
	private byte[] method_3()
	{
		string text = "{11111-22222-20001-00002}";
		if (text.Length > 0)
		{
			return new byte[] { 1, 2 };
		}
		return new byte[] { 1, 2 };
	}

	// Token: 0x06002642 RID: 9794 RVA: 0x00070698 File Offset: 0x0006E898
	private byte[] method_4()
	{
		string text = "{11111-22222-30001-00001}";
		if (text.Length > 0)
		{
			return new byte[] { 1, 2 };
		}
		return new byte[] { 1, 2 };
	}

	// Token: 0x06002643 RID: 9795 RVA: 0x000706D8 File Offset: 0x0006E8D8
	private byte[] method_5()
	{
		string text = "{11111-22222-30001-00002}";
		if (text.Length > 0)
		{
			return new byte[] { 1, 2 };
		}
		return new byte[] { 1, 2 };
	}

	// Token: 0x06002644 RID: 9796 RVA: 0x00070718 File Offset: 0x0006E918
	internal byte[] method_6()
	{
		string text = "{11111-22222-40001-00001}";
		if (text.Length > 0)
		{
			return new byte[] { 1, 2 };
		}
		return new byte[] { 1, 2 };
	}

	// Token: 0x06002645 RID: 9797 RVA: 0x00070758 File Offset: 0x0006E958
	internal byte[] method_7()
	{
		string text = "{11111-22222-40001-00002}";
		if (text.Length > 0)
		{
			return new byte[] { 1, 2 };
		}
		return new byte[] { 1, 2 };
	}

	// Token: 0x06002646 RID: 9798 RVA: 0x00070798 File Offset: 0x0006E998
	internal byte[] method_8()
	{
		string text = "{11111-22222-50001-00001}";
		if (text.Length > 0)
		{
			return new byte[] { 1, 2 };
		}
		return new byte[] { 1, 2 };
	}

	// Token: 0x06002647 RID: 9799 RVA: 0x000707D8 File Offset: 0x0006E9D8
	internal byte[] udfDaXdkp()
	{
		string text = "{11111-22222-50001-00002}";
		if (text.Length > 0)
		{
			return new byte[] { 1, 2 };
		}
		return new byte[] { 1, 2 };
	}

	// Token: 0x06002649 RID: 9801 RVA: 0x0000410B File Offset: 0x0000230B
	internal static bool smethod_15()
	{
		return true;
	}

	// Token: 0x0600264A RID: 9802 RVA: 0x0000F622 File Offset: 0x0000D822
	internal static bool smethod_16()
	{
		return false;
	}

	// Token: 0x04001F22 RID: 7970
	private static bool bool_0 = false;

	// Token: 0x04001F23 RID: 7971
	private static bool bool_1 = false;

	// Token: 0x04001F24 RID: 7972
	private static byte[] byte_0 = new byte[0];

	// Token: 0x04001F25 RID: 7973
	private static IntPtr intptr_0 = IntPtr.Zero;

	// Token: 0x04001F26 RID: 7974
	private static bool bool_2 = false;

	// Token: 0x04001F27 RID: 7975
	private static int int_0 = 0;

	// Token: 0x04001F28 RID: 7976
	private static long long_0 = 0L;

	// Token: 0x04001F29 RID: 7977
	internal static Class3.Delegate1 delegate1_0 = null;

	// Token: 0x04001F2A RID: 7978
	private static bool bool_3 = false;

	// Token: 0x04001F2B RID: 7979
	private static IntPtr intptr_1 = IntPtr.Zero;

	// Token: 0x04001F2C RID: 7980
	private static Assembly assembly_0 = typeof(Class3).Assembly;

	// Token: 0x04001F2D RID: 7981
	private static int int_1 = 0;

	// Token: 0x04001F2E RID: 7982
	private static bool bool_4 = false;

	// Token: 0x04001F2F RID: 7983
	private static int[] int_2 = new int[0];

	// Token: 0x04001F30 RID: 7984
	private static string[] string_0 = new string[0];

	// Token: 0x04001F31 RID: 7985
	private static byte[] byte_1 = new byte[0];

	// Token: 0x04001F32 RID: 7986
	private static byte[] byte_2 = new byte[0];

	// Token: 0x04001F33 RID: 7987
	[Class3.Attribute0(typeof(Class3.Attribute0.Class4<object>[]))]
	private static bool firstrundone = false;

	// Token: 0x04001F34 RID: 7988
	private static SortedList sortedList_0 = new SortedList();

	// Token: 0x04001F35 RID: 7989
	private static int int_3 = 1;

	// Token: 0x04001F36 RID: 7990
	private static byte[] byte_3 = new byte[0];

	// Token: 0x04001F37 RID: 7991
	internal static Hashtable hashtable_0 = new Hashtable();

	// Token: 0x04001F38 RID: 7992
	private static int int_4 = 0;

	// Token: 0x04001F39 RID: 7993
	internal static Class3.Delegate1 delegate1_1 = null;

	// Token: 0x04001F3A RID: 7994
	private static long long_1 = 0L;

	// Token: 0x04001F3B RID: 7995
	private static bool bool_5 = false;

	// Token: 0x04001F3C RID: 7996
	private static uint[] uint_0 = new uint[]
	{
		3614090360U, 3905402710U, 606105819U, 3250441966U, 4118548399U, 1200080426U, 2821735955U, 4249261313U, 1770035416U, 2336552879U,
		4294925233U, 2304563134U, 1804603682U, 4254626195U, 2792965006U, 1236535329U, 4129170786U, 3225465664U, 643717713U, 3921069994U,
		3593408605U, 38016083U, 3634488961U, 3889429448U, 568446438U, 3275163606U, 4107603335U, 1163531501U, 2850285829U, 4243563512U,
		1735328473U, 2368359562U, 4294588738U, 2272392833U, 1839030562U, 4259657740U, 2763975236U, 1272893353U, 4139469664U, 3200236656U,
		681279174U, 3936430074U, 3572445317U, 76029189U, 3654602809U, 3873151461U, 530742520U, 3299628645U, 4096336452U, 1126891415U,
		2878612391U, 4237533241U, 1700485571U, 2399980690U, 4293915773U, 2240044497U, 1873313359U, 4264355552U, 2734768916U, 1309151649U,
		4149444226U, 3174756917U, 718787259U, 3951481745U
	};

	// Token: 0x04001F3D RID: 7997
	private static IntPtr intptr_2 = IntPtr.Zero;

	// Token: 0x02000770 RID: 1904
	internal class Attribute0 : Attribute
	{
		// Token: 0x0600264B RID: 9803 RVA: 0x0000228C File Offset: 0x0000048C
		[Class3.Attribute0(typeof(Class3.Attribute0.Class4<object>[]))]
		public Attribute0(object object_0)
		{
		}

		// Token: 0x02000771 RID: 1905
		internal class Class4<T>
		{
		}
	}

	// Token: 0x02000772 RID: 1906
	internal class Class5
	{
		// Token: 0x0600264D RID: 9805 RVA: 0x0000F625 File Offset: 0x0000D825
		[Class3.Attribute0(typeof(Class3.Attribute0.Class4<object>[]))]
		internal static void ce4DmfsmSrOT856tDgfrkMb()
		{
			if (!(Class3.Class5.smethod_0(Convert.ToBase64String(Class3.assembly_0.GetName().GetPublicKeyToken()), " ") != "  "))
			{
				return;
			}
			for (;;)
			{
				Class3.Class5.ce4DmfsmSrOT856tDgfrkMb();
			}
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x00070818 File Offset: 0x0006EA18
		[Class3.Attribute0(typeof(Class3.Attribute0.Class4<object>[]))]
		internal static string smethod_0(string string_0, string string_1)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(string_0);
			byte[] array = bytes;
			byte[] array2 = new byte[]
			{
				82, 102, 104, 110, 32, 77, 24, 34, 118, 181,
				51, 17, 18, 51, 12, 109, 10, 32, 77, 24,
				34, 158, 161, 41, 97, 28, 118, 181, 5, 25,
				1, 88
			};
			byte[] array3 = Class3.BjkXsyRir(Encoding.Unicode.GetBytes(string_1));
			MemoryStream memoryStream = new MemoryStream();
			SymmetricAlgorithm symmetricAlgorithm = Class3.SuhhReBcy();
			symmetricAlgorithm.Key = array2;
			symmetricAlgorithm.IV = array3;
			CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(array, 0, array.Length);
			cryptoStream.Close();
			return Convert.ToBase64String(memoryStream.ToArray());
		}
	}

	// Token: 0x02000773 RID: 1907
	// (Invoke) Token: 0x06002651 RID: 9809
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal delegate uint Delegate1(IntPtr classthis, IntPtr comp, IntPtr info, [MarshalAs(UnmanagedType.U4)] uint flags, IntPtr nativeEntry, ref uint nativeSizeOfCode);

	// Token: 0x02000774 RID: 1908
	// (Invoke) Token: 0x06002655 RID: 9813
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	private delegate IntPtr Delegate2();

	// Token: 0x02000775 RID: 1909
	internal struct Struct0
	{
		// Token: 0x04001F3E RID: 7998
		internal bool bool_0;

		// Token: 0x04001F3F RID: 7999
		internal byte[] byte_0;
	}

	// Token: 0x02000776 RID: 1910
	[Flags]
	private enum Enum0
	{

	}
}
