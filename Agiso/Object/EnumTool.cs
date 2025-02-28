using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Agiso.Object
{
	// Token: 0x02000688 RID: 1672
	public class EnumTool
	{
		// Token: 0x06001FD4 RID: 8148 RVA: 0x00052C48 File Offset: 0x00050E48
		public static Dictionary<long, string> GetEnumValAndDescList<T>()
		{
			Dictionary<long, string> dictionary = new Dictionary<long, string>();
			T t = default(T);
			if (t != null && t.GetType().IsEnum)
			{
				foreach (object obj in Enum.GetValues(typeof(T)))
				{
					int num = (int)obj;
					string name = Enum.GetName(typeof(T), num);
					string text = "";
					MemberInfo[] member = typeof(T).GetMember(name);
					if (member != null && member.Length != 0)
					{
						object[] customAttributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							text = ((DescriptionAttribute)customAttributes[0]).Description;
						}
					}
					dictionary[(long)num] = text;
				}
			}
			return dictionary;
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x00052D64 File Offset: 0x00050F64
		public static T TryParse<T>(string enumName)
		{
			T t;
			try
			{
				t = (T)((object)Enum.Parse(typeof(T), (string)enumName, true));
			}
			catch
			{
				t = default(T);
			}
			return t;
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x00052DB0 File Offset: 0x00050FB0
		public static T TryParse<T>(string enumName, bool ignoreCase)
		{
			T t;
			try
			{
				t = (T)((object)Enum.Parse(typeof(T), enumName, ignoreCase));
			}
			catch
			{
				t = default(T);
			}
			return t;
		}
	}
}
