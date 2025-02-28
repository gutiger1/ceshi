using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Agiso.Utils;

namespace Agiso.Object
{
	// Token: 0x02000696 RID: 1686
	public class ErrCodeInfo
	{
		// Token: 0x06002092 RID: 8338 RVA: 0x0000E758 File Offset: 0x0000C958
		public ErrCodeInfo()
		{
			this.InnerErrCodes = new List<ErrCodeInfo>();
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x0000E76C File Offset: 0x0000C96C
		public ErrCodeInfo(ErrCodeType errCode)
			: this()
		{
			this.b = errCode;
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x0000E77C File Offset: 0x0000C97C
		public ErrCodeInfo(ErrCodeType errCode, string errorMsg)
			: this(errCode)
		{
			this.a = errorMsg;
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x0000E78D File Offset: 0x0000C98D
		public ErrCodeInfo(ErrCodeType errCode, params ErrCodeInfo[] innerErrCodes)
			: this(errCode)
		{
			this.InnerErrCodes.AddRange(innerErrCodes);
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x0000E7A3 File Offset: 0x0000C9A3
		public ErrCodeInfo(ErrCodeType errCode, List<ErrCodeInfo> innerErrCodeList)
			: this(errCode, innerErrCodeList.ToArray())
		{
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x0000E7B3 File Offset: 0x0000C9B3
		public ErrCodeType ErrCode
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x0000E7BB File Offset: 0x0000C9BB
		// (set) Token: 0x06002099 RID: 8345 RVA: 0x0000E7C3 File Offset: 0x0000C9C3
		public List<ErrCodeInfo> InnerErrCodes { get; set; }

		// Token: 0x0600209A RID: 8346 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		public void SetErrorMsg(string errorMsg)
		{
			this.a = errorMsg;
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x00053E38 File Offset: 0x00052038
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.a(this.ErrCode));
			if (!string.IsNullOrEmpty(this.a))
			{
				stringBuilder.Append("=>");
				stringBuilder.Append(this.a);
			}
			foreach (ErrCodeInfo errCodeInfo in this.InnerErrCodes)
			{
				stringBuilder.Append("-->");
				stringBuilder.Append(this.a(errCodeInfo.ErrCode));
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00053EF8 File Offset: 0x000520F8
		private string a(ErrCodeType A_0)
		{
			string text = Util.GetEnumDescription(A_0);
			if (string.IsNullOrEmpty(text))
			{
				text = string.Format("错误代码：{0}", A_0);
			}
			return text;
		}

		// Token: 0x04001266 RID: 4710
		private string a;

		// Token: 0x04001267 RID: 4711
		private ErrCodeType b;

		// Token: 0x04001268 RID: 4712
		[CompilerGenerated]
		private List<ErrCodeInfo> c;
	}
}
