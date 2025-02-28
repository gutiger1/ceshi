using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AliwwClient.Properties
{
	// Token: 0x02000069 RID: 105
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00034EEC File Offset: 0x000330EC
		public static Settings Default
		{
			get
			{
				return Settings.a;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00034F00 File Offset: 0x00033100
		[DebuggerNonUserCode]
		[SpecialSetting(SpecialSetting.ConnectionString)]
		[ApplicationScopedSetting]
		[DefaultSettingValue("Data Source=|DataDirectory|\\AldsDb.sdf")]
		public string AldsDbConnectionString
		{
			get
			{
				return (string)this["AldsDbConnectionString"];
			}
		}

		// Token: 0x040002E6 RID: 742
		private static Settings a = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
