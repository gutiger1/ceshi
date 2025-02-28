using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

// Token: 0x02000778 RID: 1912
internal class Class7
{
	// Token: 0x06002659 RID: 9817 RVA: 0x000708A8 File Offset: 0x0006EAA8
	private static Assembly smethod_0(object object_0, ResolveEventArgs resolveEventArgs_0)
	{
		Assembly assembly3;
		lock (Class7.hashtable_0)
		{
			string text = resolveEventArgs_0.Name.Trim();
			object obj = Class7.hashtable_0[text];
			if (obj == null)
			{
				try
				{
					string text2 = Class7.smethod_1(text);
					Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
					foreach (Assembly assembly in assemblies)
					{
						if (assembly.GetName().Name.ToUpper() == text2.ToUpper())
						{
							return assembly;
						}
					}
				}
				catch
				{
				}
			}
			if (obj == null)
			{
				try
				{
					RSACryptoServiceProvider.UseMachineKeyStore = true;
					string text3 = Class7.smethod_1(text);
					byte[] bytes = Encoding.Unicode.GetBytes(text3);
					string text4 = "b0494a1f-4bd3-" + Convert.ToBase64String(Class3.BjkXsyRir(bytes));
					Stream manifestResourceStream = typeof(Class7).Assembly.GetManifestResourceStream(text4);
					if (manifestResourceStream != null)
					{
						try
						{
							BinaryReader binaryReader = new BinaryReader(manifestResourceStream);
							binaryReader.BaseStream.Position = 0L;
							byte[] array2 = new byte[manifestResourceStream.Length];
							binaryReader.Read(array2, 0, array2.Length);
							binaryReader.Close();
							bool flag2 = false;
							Assembly assembly2 = null;
							try
							{
								assembly2 = Assembly.Load(array2);
							}
							catch (FileLoadException)
							{
								flag2 = true;
							}
							catch (BadImageFormatException)
							{
								flag2 = true;
							}
							if (flag2)
							{
								string text5 = Path.Combine(Path.GetTempPath(), text4);
								string text6 = Path.Combine(text5, text3 + ".dll");
								if (File.Exists(text6))
								{
									if (Class7.hashtable_1.ContainsKey(text6))
									{
										goto IL_01D1;
									}
								}
								try
								{
									Class7.hashtable_1[text6] = null;
									if (!Directory.Exists(Path.GetDirectoryName(text6)))
									{
										Directory.CreateDirectory(Path.GetDirectoryName(text6));
									}
									FileStream fileStream = new FileStream(text6, FileMode.Create, FileAccess.Write);
									fileStream.Write(array2, 0, array2.Length);
									fileStream.Close();
								}
								catch
								{
								}
								IL_01D1:
								assembly2 = Assembly.LoadFile(text6);
								Class7.hashtable_0.Add(text, assembly2);
							}
							else
							{
								Class7.hashtable_0.Add(text, assembly2);
							}
							return assembly2;
						}
						catch (Exception)
						{
						}
					}
				}
				catch (Exception)
				{
				}
				assembly3 = null;
			}
			else
			{
				assembly3 = (Assembly)obj;
			}
		}
		return assembly3;
	}

	// Token: 0x0600265A RID: 9818 RVA: 0x00070B84 File Offset: 0x0006ED84
	private static string smethod_1(string string_0)
	{
		string text = string_0.Trim();
		int num = text.IndexOf(',');
		if (num >= 0)
		{
			text = text.Substring(0, num);
		}
		return text;
	}

	// Token: 0x04001F42 RID: 8002
	private static Hashtable hashtable_0 = new Hashtable();

	// Token: 0x04001F43 RID: 8003
	private static Hashtable hashtable_1 = new Hashtable();

	// Token: 0x04001F44 RID: 8004
	private static bool bool_0 = false;
}
