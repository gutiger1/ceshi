using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Agiso
{
	// Token: 0x020000DA RID: 218
	public class BuyerInfoCache
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x0004785C File Offset: 0x00045A5C
		public void UpdateAldsOpenUid(string buyerNick, string aldsOpenUid)
		{
			BuyerInfoCache.a a = new BuyerInfoCache.a();
			a.a = buyerNick;
			if (!string.IsNullOrEmpty(a.a) && !a.a.Contains("**") && !a.a.Contains(":"))
			{
				object obj = BuyerInfoCache.e;
				lock (obj)
				{
					BuyerInfo buyerInfo = BuyerInfoCache.a.FirstOrDefault(new Func<BuyerInfo, bool>(a.b));
					if (buyerInfo == null)
					{
						buyerInfo = new BuyerInfo();
						buyerInfo.BuyerNick = a.a;
						buyerInfo.AldsOpenUid = aldsOpenUid;
						BuyerInfoCache.a.Add(buyerInfo);
					}
					else
					{
						buyerInfo.AldsOpenUid = aldsOpenUid;
					}
				}
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0004792C File Offset: 0x00045B2C
		public void UpdateRecentOpenUid(string buyerNick, string recentOpenUid)
		{
			BuyerInfoCache.b b = new BuyerInfoCache.b();
			b.a = buyerNick;
			if (!string.IsNullOrEmpty(b.a) && !b.a.Contains("**") && !b.a.Contains(":"))
			{
				object obj = BuyerInfoCache.e;
				lock (obj)
				{
					BuyerInfo buyerInfo = BuyerInfoCache.a.FirstOrDefault(new Func<BuyerInfo, bool>(b.b));
					if (buyerInfo == null)
					{
						buyerInfo = new BuyerInfo();
						buyerInfo.BuyerNick = b.a;
						buyerInfo.RecentOpenUid = recentOpenUid;
						BuyerInfoCache.a.Add(buyerInfo);
					}
					else
					{
						buyerInfo.RecentOpenUid = recentOpenUid;
					}
				}
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000479FC File Offset: 0x00045BFC
		public string GetBuyerNickByAldsOpenUid(string aldsOpenUid)
		{
			BuyerInfoCache.c c = new BuyerInfoCache.c();
			c.a = aldsOpenUid;
			string text;
			if (string.IsNullOrEmpty(c.a))
			{
				text = "";
			}
			else if (BuyerInfoCache.d.ContainsKey(c.a))
			{
				text = BuyerInfoCache.d[c.a];
			}
			else
			{
				BuyerInfo buyerInfo = BuyerInfoCache.a.FirstOrDefault(new Func<BuyerInfo, bool>(c.b));
				if (string.IsNullOrEmpty((buyerInfo != null) ? buyerInfo.BuyerNick : null))
				{
					text = "";
				}
				else
				{
					BuyerInfoCache.d.TryAdd(c.a, buyerInfo.BuyerNick);
					text = buyerInfo.BuyerNick;
				}
			}
			return text;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00047AA4 File Offset: 0x00045CA4
		public string GetRecentOpenUidByAldsOpenUid(string aldsOpenUid)
		{
			BuyerInfoCache.d d = new BuyerInfoCache.d();
			d.a = aldsOpenUid;
			string text;
			if (string.IsNullOrEmpty(d.a))
			{
				text = "";
			}
			else if (BuyerInfoCache.b.ContainsKey(d.a))
			{
				text = BuyerInfoCache.b[d.a];
			}
			else
			{
				BuyerInfo buyerInfo = BuyerInfoCache.a.FirstOrDefault(new Func<BuyerInfo, bool>(d.b));
				if (string.IsNullOrEmpty((buyerInfo != null) ? buyerInfo.RecentOpenUid : null))
				{
					text = "";
				}
				else
				{
					BuyerInfoCache.b.TryAdd(d.a, buyerInfo.RecentOpenUid);
					text = buyerInfo.RecentOpenUid;
				}
			}
			return text;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00047B4C File Offset: 0x00045D4C
		public string GetAldsOpenUidByRecentOpenUid(string recentOpenUid)
		{
			BuyerInfoCache.e e = new BuyerInfoCache.e();
			e.a = recentOpenUid;
			string text;
			if (string.IsNullOrEmpty(e.a))
			{
				text = "";
			}
			else if (BuyerInfoCache.c.ContainsKey(e.a))
			{
				text = BuyerInfoCache.c[e.a];
			}
			else
			{
				BuyerInfo buyerInfo = BuyerInfoCache.a.FirstOrDefault(new Func<BuyerInfo, bool>(e.b));
				if (string.IsNullOrEmpty((buyerInfo != null) ? buyerInfo.RecentOpenUid : null))
				{
					text = "";
				}
				else
				{
					BuyerInfoCache.c.TryAdd(e.a, buyerInfo.AldsOpenUid);
					text = buyerInfo.AldsOpenUid;
				}
			}
			return text;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00047BF4 File Offset: 0x00045DF4
		public void Add(string buyerNick, string aldsOpenUid, string recentOpenUid)
		{
			BuyerInfoCache.f f = new BuyerInfoCache.f();
			f.a = buyerNick;
			if (!string.IsNullOrEmpty(f.a) && !f.a.Contains("**") && !f.a.Contains(":"))
			{
				object obj = BuyerInfoCache.e;
				lock (obj)
				{
					BuyerInfo buyerInfo = BuyerInfoCache.a.FirstOrDefault(new Func<BuyerInfo, bool>(f.b));
					if (buyerInfo != null)
					{
						buyerInfo.BuyerNick = f.a;
						buyerInfo.AldsOpenUid = aldsOpenUid;
						buyerInfo.RecentOpenUid = recentOpenUid;
					}
					else
					{
						buyerInfo = new BuyerInfo
						{
							BuyerNick = f.a,
							AldsOpenUid = aldsOpenUid,
							RecentOpenUid = recentOpenUid
						};
						BuyerInfoCache.a.Add(buyerInfo);
					}
				}
			}
		}

		// Token: 0x040004AC RID: 1196
		private static List<BuyerInfo> a = new List<BuyerInfo>();

		// Token: 0x040004AD RID: 1197
		[CompilerGenerated]
		private static readonly ConcurrentDictionary<string, string> b = new ConcurrentDictionary<string, string>();

		// Token: 0x040004AE RID: 1198
		[CompilerGenerated]
		private static readonly ConcurrentDictionary<string, string> c = new ConcurrentDictionary<string, string>();

		// Token: 0x040004AF RID: 1199
		[CompilerGenerated]
		private static readonly ConcurrentDictionary<string, string> d = new ConcurrentDictionary<string, string>();

		// Token: 0x040004B0 RID: 1200
		private static object e = new object();

		// Token: 0x020000DB RID: 219
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x0600067D RID: 1661 RVA: 0x0000425E File Offset: 0x0000245E
			internal bool b(BuyerInfo A_0)
			{
				return A_0.BuyerNick == this.a;
			}

			// Token: 0x040004B1 RID: 1201
			public string a;
		}

		// Token: 0x020000DC RID: 220
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x0600067F RID: 1663 RVA: 0x00004271 File Offset: 0x00002471
			internal bool b(BuyerInfo A_0)
			{
				return A_0.BuyerNick == this.a;
			}

			// Token: 0x040004B2 RID: 1202
			public string a;
		}

		// Token: 0x020000DD RID: 221
		[CompilerGenerated]
		private sealed class c
		{
			// Token: 0x06000681 RID: 1665 RVA: 0x00004284 File Offset: 0x00002484
			internal bool b(BuyerInfo A_0)
			{
				return A_0.AldsOpenUid == this.a;
			}

			// Token: 0x040004B3 RID: 1203
			public string a;
		}

		// Token: 0x020000DE RID: 222
		[CompilerGenerated]
		private sealed class d
		{
			// Token: 0x06000683 RID: 1667 RVA: 0x00004297 File Offset: 0x00002497
			internal bool b(BuyerInfo A_0)
			{
				return A_0.AldsOpenUid == this.a;
			}

			// Token: 0x040004B4 RID: 1204
			public string a;
		}

		// Token: 0x020000DF RID: 223
		[CompilerGenerated]
		private sealed class e
		{
			// Token: 0x06000685 RID: 1669 RVA: 0x000042AA File Offset: 0x000024AA
			internal bool b(BuyerInfo A_0)
			{
				return A_0.RecentOpenUid == this.a;
			}

			// Token: 0x040004B5 RID: 1205
			public string a;
		}

		// Token: 0x020000E0 RID: 224
		[CompilerGenerated]
		private sealed class f
		{
			// Token: 0x06000687 RID: 1671 RVA: 0x000042BD File Offset: 0x000024BD
			internal bool b(BuyerInfo A_0)
			{
				return A_0.BuyerNick == this.a;
			}

			// Token: 0x040004B6 RID: 1206
			public string a;
		}
	}
}
