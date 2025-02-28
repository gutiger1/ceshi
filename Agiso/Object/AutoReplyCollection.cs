using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Agiso.Utils;

namespace Agiso.Object
{
	// Token: 0x02000692 RID: 1682
	public class AutoReplyCollection : List<AutoReplyInfo>
	{
		// Token: 0x06002082 RID: 8322 RVA: 0x00053B70 File Offset: 0x00051D70
		public static AutoReplyCollection Load(List<AutoReplyInfo> list, string userNick, bool isAutoReplyBySellerNick, string autoReplyAllSellerFit)
		{
			AutoReplyCollection.a a = new AutoReplyCollection.a();
			a.a = autoReplyAllSellerFit;
			a.b = userNick;
			AutoReplyCollection autoReplyCollection;
			if (list == null)
			{
				autoReplyCollection = null;
			}
			else
			{
				AutoReplyCollection autoReplyCollection2 = new AutoReplyCollection();
				List<AutoReplyInfo> list2;
				if (isAutoReplyBySellerNick && a.b.Contains(":"))
				{
					AutoReplyCollection.b b = new AutoReplyCollection.b();
					b.b = a;
					b.a = Util.GetMasterNick(b.b.b);
					list2 = list.Where(new Func<AutoReplyInfo, bool>(b.c)).OrderBy(new Func<AutoReplyInfo, EnumArType>(AutoReplyCollection.<>c.<>9.e)).ToList<AutoReplyInfo>();
				}
				else
				{
					list2 = list.Where(new Func<AutoReplyInfo, bool>(a.c)).OrderBy(new Func<AutoReplyInfo, EnumArType>(AutoReplyCollection.<>c.<>9.d)).ToList<AutoReplyInfo>();
				}
				if (list2 != null)
				{
					foreach (AutoReplyInfo autoReplyInfo in list2)
					{
						autoReplyCollection2.Add(autoReplyInfo);
					}
				}
				autoReplyCollection = autoReplyCollection2;
			}
			return autoReplyCollection;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x00053CB4 File Offset: 0x00051EB4
		public AutoReplyInfo Match(string newMsg)
		{
			newMsg = (newMsg ?? "").Trim();
			List<AutoReplyInfo> list = new List<AutoReplyInfo>();
			foreach (AutoReplyInfo autoReplyInfo in this)
			{
				if (autoReplyInfo.Match(newMsg) && !string.IsNullOrEmpty(autoReplyInfo.ReplyWord))
				{
					list.Add(autoReplyInfo);
				}
			}
			IGrouping<EnumArType, AutoReplyInfo> grouping = list.Where(new Func<AutoReplyInfo, bool>(AutoReplyCollection.<>c.<>9.c)).GroupBy(new Func<AutoReplyInfo, EnumArType>(AutoReplyCollection.<>c.<>9.b)).OrderBy(new Func<IGrouping<EnumArType, AutoReplyInfo>, EnumArType>(AutoReplyCollection.<>c.<>9.a))
				.FirstOrDefault<IGrouping<EnumArType, AutoReplyInfo>>();
			if (grouping != null)
			{
				IGrouping<long, AutoReplyInfo> grouping2 = grouping.GroupBy(new Func<AutoReplyInfo, long>(AutoReplyCollection.<>c.<>9.a)).OrderBy(new Func<IGrouping<long, AutoReplyInfo>, long>(AutoReplyCollection.<>c.<>9.a)).FirstOrDefault<IGrouping<long, AutoReplyInfo>>();
				if (grouping2 != null)
				{
					List<AutoReplyInfo> list2 = grouping2.ToList<AutoReplyInfo>();
					Random random = new Random();
					int num = random.Next(0, list2.Count);
					return list2[num];
				}
			}
			return null;
		}

		// Token: 0x02000694 RID: 1684
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x0600208F RID: 8335 RVA: 0x0000E6EB File Offset: 0x0000C8EB
			internal bool c(AutoReplyInfo A_0)
			{
				return A_0.Enabled && (A_0.SellerNick == this.b || A_0.SellerNick == this.a);
			}

			// Token: 0x04001262 RID: 4706
			public string a;

			// Token: 0x04001263 RID: 4707
			public string b;
		}

		// Token: 0x02000695 RID: 1685
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x06002091 RID: 8337 RVA: 0x0000E71F File Offset: 0x0000C91F
			internal bool c(AutoReplyInfo A_0)
			{
				return A_0.Enabled && (A_0.SellerNick == this.a || A_0.SellerNick == this.b.a);
			}

			// Token: 0x04001264 RID: 4708
			public string a;

			// Token: 0x04001265 RID: 4709
			public AutoReplyCollection.a b;
		}
	}
}
