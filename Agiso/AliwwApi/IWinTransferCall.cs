using System;
using Agiso.Object;

namespace Agiso.AliwwApi
{
	// Token: 0x0200071F RID: 1823
	public interface IWinTransferCall
	{
		// Token: 0x0600243A RID: 9274
		ErrCodeInfo TransferCallToGroup(int idx = 1);

		// Token: 0x0600243B RID: 9275
		ErrCodeInfo TransferCall(string buyerNick);
	}
}
