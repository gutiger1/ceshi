using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools.Protocol.Chrome.CSS
{
	// Token: 0x020005F1 RID: 1521
	[SupportedBy("Chrome")]
	public class CSSMedia
	{
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x0000CD00 File Offset: 0x0000AF00
		public string Text { get; set; }

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x0000CD09 File Offset: 0x0000AF09
		// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x0000CD11 File Offset: 0x0000AF11
		public string Source { get; set; }

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0000CD1A File Offset: 0x0000AF1A
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x0000CD22 File Offset: 0x0000AF22
		public string SourceURL { get; set; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x0000CD2B File Offset: 0x0000AF2B
		// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x0000CD33 File Offset: 0x0000AF33
		public SourceRange Range { get; set; }

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		// (set) Token: 0x06001CBB RID: 7355 RVA: 0x0000CD44 File Offset: 0x0000AF44
		public string ParentStyleSheetId { get; set; }

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0000CD4D File Offset: 0x0000AF4D
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x0000CD55 File Offset: 0x0000AF55
		public MediaQuery[] MediaList { get; set; }

		// Token: 0x04000FF0 RID: 4080
		[CompilerGenerated]
		private string a;

		// Token: 0x04000FF1 RID: 4081
		[CompilerGenerated]
		private string b;

		// Token: 0x04000FF2 RID: 4082
		[CompilerGenerated]
		private string c;

		// Token: 0x04000FF3 RID: 4083
		[CompilerGenerated]
		private SourceRange d;

		// Token: 0x04000FF4 RID: 4084
		[CompilerGenerated]
		private string e;

		// Token: 0x04000FF5 RID: 4085
		[CompilerGenerated]
		private MediaQuery[] f;
	}
}
