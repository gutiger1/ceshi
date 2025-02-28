using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace AliwwClient.Manager
{
	// Token: 0x020000B8 RID: 184
	public class EmotionItem
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00003D6B File Offset: 0x00001F6B
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00003D73 File Offset: 0x00001F73
		[JsonProperty("id")]
		public string Id { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00003D7C File Offset: 0x00001F7C
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x00003D84 File Offset: 0x00001F84
		[JsonProperty("shortCut")]
		public string ShortCut { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00003D8D File Offset: 0x00001F8D
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00003D95 File Offset: 0x00001F95
		[JsonProperty("originalFile")]
		public string OriginalFile { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00003D9E File Offset: 0x00001F9E
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00003DA6 File Offset: 0x00001FA6
		[JsonProperty("fixedFile")]
		public string FixedFile { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00003DAF File Offset: 0x00001FAF
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00003DB7 File Offset: 0x00001FB7
		[JsonIgnore]
		public string BigFixedFile { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00003DC0 File Offset: 0x00001FC0
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00003DC8 File Offset: 0x00001FC8
		[JsonIgnore]
		public string MD5 { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00003DD1 File Offset: 0x00001FD1
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00003DD9 File Offset: 0x00001FD9
		[JsonProperty("meaning")]
		public string Meaning { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00003DE2 File Offset: 0x00001FE2
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00003DEA File Offset: 0x00001FEA
		[JsonProperty("groupName")]
		public string GroupName { get; set; } = "MyEmotion";

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00003DF3 File Offset: 0x00001FF3
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00003DFB File Offset: 0x00001FFB
		[JsonProperty("type")]
		public int Type { get; set; } = 2;

		// Token: 0x040003F6 RID: 1014
		[CompilerGenerated]
		private string a;

		// Token: 0x040003F7 RID: 1015
		[CompilerGenerated]
		private string b;

		// Token: 0x040003F8 RID: 1016
		[CompilerGenerated]
		private string c;

		// Token: 0x040003F9 RID: 1017
		[CompilerGenerated]
		private string d;

		// Token: 0x040003FA RID: 1018
		[CompilerGenerated]
		private string e;

		// Token: 0x040003FB RID: 1019
		[CompilerGenerated]
		private string f;

		// Token: 0x040003FC RID: 1020
		[CompilerGenerated]
		private string g;

		// Token: 0x040003FD RID: 1021
		[CompilerGenerated]
		private string h;

		// Token: 0x040003FE RID: 1022
		[CompilerGenerated]
		private int i;
	}
}
