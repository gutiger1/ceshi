using System;
using System.Runtime.CompilerServices;

namespace Agiso.ChromeDevTools
{
	// Token: 0x02000113 RID: 275
	public class ErrorResponse : IErrorResponse, ICommandResponse
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0000489F File Offset: 0x00002A9F
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x000048A7 File Offset: 0x00002AA7
		public Error Error { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x000048B0 File Offset: 0x00002AB0
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x000048B8 File Offset: 0x00002AB8
		public long Id { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x000048C1 File Offset: 0x00002AC1
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x000048C9 File Offset: 0x00002AC9
		public string Method { get; set; }

		// Token: 0x06000889 RID: 2185 RVA: 0x00052650 File Offset: 0x00050850
		public override string ToString()
		{
			return string.Format("Id:{0}, Method:{1}, ErrorCode:{2}, ErrorMessage:{3}", new object[]
			{
				this.Id,
				this.Method,
				this.Error.Code,
				this.Error.Message
			});
		}

		// Token: 0x0400052A RID: 1322
		[CompilerGenerated]
		private Error a;

		// Token: 0x0400052B RID: 1323
		[CompilerGenerated]
		private long b;

		// Token: 0x0400052C RID: 1324
		[CompilerGenerated]
		private string c;
	}
}
