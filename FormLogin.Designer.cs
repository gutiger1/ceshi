namespace AliwwClient
{
	// Token: 0x02000030 RID: 48
	[global::System.Runtime.InteropServices.ComVisible(true)]
	[global::System.Security.Permissions.PermissionSet(global::System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
	public partial class FormLogin : global::AliwwClient.BaseForm
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0001D7CC File Offset: 0x0001B9CC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.b != null)
			{
				this.b.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000147 RID: 327
		private global::System.ComponentModel.IContainer b = null;
	}
}
