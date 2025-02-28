using System;
using System.Diagnostics;
using System.Windows.Forms;
using Agiso.Utils;

namespace Agiso.Handler
{
	// Token: 0x020000F8 RID: 248
	public class SendKeysExtend
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x0004F5A8 File Offset: 0x0004D7A8
		public static void Send(string keys)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			SendKeys.Send(keys);
			stopwatch.Stop();
			if (stopwatch.Elapsed.TotalSeconds >= 3.0)
			{
				LogWriter.WriteLog(string.Format("SendKeysExtend.Send--插入“{0}”，用时：{1}", keys, stopwatch.Elapsed.TotalSeconds), 1);
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0004F614 File Offset: 0x0004D814
		public static void SendWait(string keys)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			SendKeys.SendWait(keys);
			stopwatch.Stop();
			if (stopwatch.Elapsed.TotalSeconds >= 3.0)
			{
				LogWriter.WriteLog(string.Format("SendKeysExtend.SendWait--插入“{0}”，用时：{1}", keys, stopwatch.Elapsed.TotalSeconds), 1);
			}
		}
	}
}
