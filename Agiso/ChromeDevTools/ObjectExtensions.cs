using System;

namespace Agiso.ChromeDevTools
{
	// Token: 0x02000118 RID: 280
	public static class ObjectExtensions
	{
		// Token: 0x06000896 RID: 2198 RVA: 0x000526A8 File Offset: 0x000508A8
		public static string GetMethod(this object obj)
		{
			string text;
			if (obj == null)
			{
				text = null;
			}
			else
			{
				text = obj.GetType().GetMethod();
			}
			return text;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000526CC File Offset: 0x000508CC
		public static string GetMethod(this Type type)
		{
			string text;
			if (null == type)
			{
				text = null;
			}
			else
			{
				EventAttribute eventAttribute = type.GetCustomAttributes(typeof(EventAttribute), true).FirstOrDefault() as EventAttribute;
				if (eventAttribute != null)
				{
					text = eventAttribute.MethodName;
				}
				else
				{
					CommandAttribute commandAttribute = type.GetCustomAttributes(typeof(CommandAttribute), true).FirstOrDefault() as CommandAttribute;
					if (commandAttribute != null)
					{
						text = commandAttribute.MethodName;
					}
					else
					{
						CommandResponseAttribute commandResponseAttribute = type.GetCustomAttributes(typeof(CommandResponseAttribute), true).FirstOrDefault() as CommandResponseAttribute;
						if (commandResponseAttribute != null)
						{
							text = commandResponseAttribute.MethodName;
						}
						else
						{
							if (!type.IsGenericType)
							{
								throw new Exception("Could not determine the method type for " + ((type != null) ? type.ToString() : null));
							}
							object[] genericArguments = type.GetGenericArguments();
							text = genericArguments.FirstOrDefault().GetMethod();
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000527B4 File Offset: 0x000509B4
		public static object FirstOrDefault(this object[] objs)
		{
			object obj;
			if (objs == null || objs.Length < 1)
			{
				obj = null;
			}
			else
			{
				obj = objs[0];
			}
			return obj;
		}
	}
}
