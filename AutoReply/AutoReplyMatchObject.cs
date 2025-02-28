using System;
using System.Runtime.CompilerServices;
using Agiso.Object;

namespace AliwwClient.AutoReply
{
	// Token: 0x0200006A RID: 106
	public class AutoReplyMatchObject : ICloneable
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000314 RID: 788 RVA: 0x000031A9 File Offset: 0x000013A9
		// (set) Token: 0x06000315 RID: 789 RVA: 0x000031B1 File Offset: 0x000013B1
		public EnumArType MatchType { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000316 RID: 790 RVA: 0x000031BA File Offset: 0x000013BA
		// (set) Token: 0x06000317 RID: 791 RVA: 0x000031C2 File Offset: 0x000013C2
		public string KeyWord { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000318 RID: 792 RVA: 0x000031CB File Offset: 0x000013CB
		// (set) Token: 0x06000319 RID: 793 RVA: 0x000031D3 File Offset: 0x000013D3
		public string ReplyWord { get; set; }

		// Token: 0x0600031A RID: 794 RVA: 0x00034F20 File Offset: 0x00033120
		public object Clone()
		{
			return new AutoReplyMatchObject
			{
				MatchType = this.MatchType,
				KeyWord = this.KeyWord,
				ReplyWord = this.ReplyWord
			};
		}

		// Token: 0x040002E7 RID: 743
		[CompilerGenerated]
		private EnumArType a;

		// Token: 0x040002E8 RID: 744
		[CompilerGenerated]
		private string b;

		// Token: 0x040002E9 RID: 745
		[CompilerGenerated]
		private string c;
	}
}
