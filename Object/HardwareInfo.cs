using System;
using System.Management;
using System.Net;
using Agiso.Utils;

namespace AliwwClient.Object
{
	// Token: 0x0200009A RID: 154
	public class HardwareInfo
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x0003CF7C File Offset: 0x0003B17C
		public static string GetHostName()
		{
			return Dns.GetHostName();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0003CF90 File Offset: 0x0003B190
		public static string GetCpuID()
		{
			string text2;
			try
			{
				ManagementClass managementClass = new ManagementClass("Win32_Processor");
				ManagementObjectCollection instances = managementClass.GetInstances();
				string text = null;
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = managementObject.Properties["ProcessorId"].Value.ToString();
					}
				}
				text2 = text;
			}
			catch
			{
				text2 = "";
			}
			return text2;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0003D028 File Offset: 0x0003B228
		public static string GetHardDiskID()
		{
			string text2;
			try
			{
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
				string text = null;
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = managementObject["SerialNumber"].ToString().Trim();
					}
				}
				text2 = text;
			}
			catch
			{
				text2 = "";
			}
			return text2;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0003D0B4 File Offset: 0x0003B2B4
		public static string Uuid
		{
			get
			{
				if (string.IsNullOrEmpty(HardwareInfo.a))
				{
					string text = HardwareInfo.GetHostName() + HardwareInfo.GetCpuID() + HardwareInfo.GetHardDiskID();
					HardwareInfo.a = Util.ComputeHashMd5(text);
				}
				return HardwareInfo.a;
			}
		}

		// Token: 0x04000377 RID: 887
		private static string a;
	}
}
