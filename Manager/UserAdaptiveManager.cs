using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Agiso.Handler;
using Agiso.Utils;
using WebSocketSharp.Server;

namespace AliwwClient.Manager
{
	// Token: 0x020000C0 RID: 192
	public class UserAdaptiveManager
	{
		// Token: 0x06000596 RID: 1430 RVA: 0x00003F49 File Offset: 0x00002149
		public UserAdaptiveManager(WebSocketBehavior wsBehavior)
		{
			this._wsBehavior = wsBehavior;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000428A0 File Offset: 0x00040AA0
		private int a()
		{
			int num;
			if (!this._wsBehavior.Context.IsLocal)
			{
				num = 0;
			}
			else
			{
				int port = this._wsBehavior.Context.UserEndPoint.Port;
				int port2 = this._wsBehavior.Context.RequestUri.Port;
				Process process = new Process();
				process.StartInfo.FileName = "cmd.exe";
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardInput = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.CreateNoWindow = true;
				process.Start();
				process.StandardInput.WriteLine("netstat -anto");
				process.StandardInput.WriteLine("exit");
				Regex regex = new Regex("\\s+", RegexOptions.Compiled);
				string text;
				while ((text = process.StandardOutput.ReadLine()) != null)
				{
					text = text.Trim();
					if (text.StartsWith("TCP", StringComparison.OrdinalIgnoreCase))
					{
						text = regex.Replace(text, ",");
						string[] array = text.Split(new char[] { ',' });
						if (array.Length >= 5 && array[1] == string.Format("127.0.0.1:{0}", port) && array[2] == string.Format("127.0.0.1:{0}", port2))
						{
							return Util.ToInt(array[4]);
						}
					}
				}
				process.Close();
				num = 0;
			}
			return num;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00042A34 File Offset: 0x00040C34
		public int GetAliWorkbenchProcessId()
		{
			int num2;
			try
			{
				int num = this.a();
				if (num <= 0)
				{
					num2 = 0;
				}
				else
				{
					int num3 = 0;
					using (Process processById = Process.GetProcessById(num))
					{
						if ("Aliapp".Equals(processById.ProcessName, StringComparison.OrdinalIgnoreCase) || "AliRender".Equals(processById.ProcessName, StringComparison.OrdinalIgnoreCase))
						{
							int num4 = 10;
							int num5 = num;
							do
							{
								num3 = Win32Extend.GetParentProcessId(num5);
								using (Process processById2 = Process.GetProcessById(num3))
								{
									if ("AliWorkbench".Equals(processById2.ProcessName, StringComparison.OrdinalIgnoreCase))
									{
										return num3;
									}
									num5 = num3;
								}
								num4--;
							}
							while (num3 > 0 && num4 > 0);
						}
					}
					num2 = num3;
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
				num2 = 0;
			}
			return num2;
		}

		// Token: 0x04000413 RID: 1043
		public WebSocketBehavior _wsBehavior;
	}
}
