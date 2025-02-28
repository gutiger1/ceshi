using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Timers;
using Agiso.AliwwApi.Qn;
using Agiso.Object;
using Agiso.Utils;

namespace AliwwClient.Manager
{
	// Token: 0x020000A8 RID: 168
	public class ImKillManager
	{
		// Token: 0x060004F0 RID: 1264 RVA: 0x0003DB6C File Offset: 0x0003BD6C
		private static bool a(string A_0, string A_1)
		{
			return ImKillManager.a.TryAdd(A_0, A_1);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0003DB90 File Offset: 0x0003BD90
		public static bool Kill(string userNick, string reason, bool ignoreAmiThreadProcessing = false)
		{
			if (!ignoreAmiThreadProcessing)
			{
				AliwwMessageInfo amiThreadProcessing = Form1.AmiThreadProcessing;
				string text = ((amiThreadProcessing != null) ? amiThreadProcessing.UserNick : null);
				AliwwMessageInfo amiThreadProcessing2 = Form1.AmiThreadProcessing;
				string text2 = ((amiThreadProcessing2 != null) ? amiThreadProcessing2.SellerNick : null);
				if (userNick == text || Util.GetMasterNick(userNick) == text2)
				{
					return ImKillManager.a(userNick, reason);
				}
			}
			if (ImKillManager.a(userNick))
			{
				LogWriter.WriteLog("千牛：" + userNick + "，被杀掉了，原因：" + reason, 1);
			}
			return true;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0003DC14 File Offset: 0x0003BE14
		public static void Clear(string userNick)
		{
			string text;
			ImKillManager.a.TryRemove(userNick, out text);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0003DC30 File Offset: 0x0003BE30
		static ImKillManager()
		{
			ImKillManager.b.Elapsed += ImKillManager.<>c.<>9.a;
			ImKillManager.b.Start();
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0003DC80 File Offset: 0x0003BE80
		private static bool a(string A_0)
		{
			AliwwTalkWindowQn aliwwTalkWindowQn = AliwwTalkWindowQn.Get(A_0);
			bool flag;
			if (aliwwTalkWindowQn != null)
			{
				aliwwTalkWindowQn.KillProcess();
				flag = true;
			}
			else
			{
				AliwwWorkBenchQn aliwwWorkBenchQn = AliwwWorkBenchQn.Get(A_0);
				if (aliwwWorkBenchQn != null)
				{
					aliwwWorkBenchQn.KillProcess();
				}
				flag = aliwwWorkBenchQn != null;
			}
			return flag;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0003DCBC File Offset: 0x0003BEBC
		~ImKillManager()
		{
			ImKillManager.a.Clear();
			ImKillManager.b.Dispose();
		}

		// Token: 0x040003D2 RID: 978
		[CompilerGenerated]
		private static readonly ConcurrentDictionary<string, string> a = new ConcurrentDictionary<string, string>();

		// Token: 0x040003D3 RID: 979
		private static Timer b = new Timer(2000.0);
	}
}
