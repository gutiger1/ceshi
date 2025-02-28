namespace AliwwClient
{
	// Token: 0x0200005E RID: 94
	[global::System.Runtime.InteropServices.ComVisible(true)]
	[global::System.Security.Permissions.PermissionSet(global::System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
	public partial class Form2 : global::AliwwClient.BaseForm
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00033E74 File Offset: 0x00032074
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.a != null)
			{
				this.a.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x040002B9 RID: 697
		private global::System.ComponentModel.IContainer a = null;
	}
}
