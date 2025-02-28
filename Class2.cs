using System;
using System.Reflection;

// Token: 0x0200076D RID: 1901
internal class Class2
{
	// Token: 0x06002623 RID: 9763 RVA: 0x0006FA88 File Offset: 0x0006DC88
	internal static void KW6B7v663AeaZ(int typemdt)
	{
		Type type = Class2.module_0.ResolveType(33554432 + typemdt);
		foreach (FieldInfo fieldInfo in type.GetFields())
		{
			MethodInfo methodInfo = (MethodInfo)Class2.module_0.ResolveMethod(fieldInfo.MetadataToken + 100663296);
			fieldInfo.SetValue(null, (MulticastDelegate)Delegate.CreateDelegate(type, methodInfo));
		}
	}

	// Token: 0x04001F21 RID: 7969
	internal static Module module_0 = typeof(Class2).Assembly.ManifestModule;

	// Token: 0x0200076E RID: 1902
	// (Invoke) Token: 0x06002627 RID: 9767
	internal delegate void Delegate0(object o);
}
