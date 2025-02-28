using System;

namespace System.Diagnostics
{
	// Token: 0x0200001C RID: 28
	public static class ProcessExtensions
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00011394 File Offset: 0x0000F594
		private static string a(Process A_0)
		{
			int id = A_0.Id;
			string processName = A_0.ProcessName;
			Process[] processesByName = Process.GetProcessesByName(processName);
			string text = null;
			for (int i = 0; i < processesByName.Length; i++)
			{
				text = ((i == 0) ? processName : (processName + "#" + i.ToString()));
				using (PerformanceCounter performanceCounter = new PerformanceCounter("Process", "ID Process", text))
				{
					if ((int)performanceCounter.NextValue() == id)
					{
						return text;
					}
				}
			}
			return text;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00011430 File Offset: 0x0000F630
		private static Process a(string A_0)
		{
			Process process;
			using (PerformanceCounter performanceCounter = new PerformanceCounter("Process", "Creating Process ID", A_0))
			{
				try
				{
					process = Process.GetProcessById((int)performanceCounter.NextValue());
				}
				catch (Exception ex)
				{
					if (!(ex is ArgumentException))
					{
						throw ex;
					}
					process = null;
				}
			}
			return process;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0001149C File Offset: 0x0000F69C
		public static Process Parent(this Process process)
		{
			string text = ProcessExtensions.a(process);
			return ProcessExtensions.a(text);
		}
	}
}
