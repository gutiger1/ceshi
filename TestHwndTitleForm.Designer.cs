// Token: 0x02000005 RID: 5
public partial class TestHwndTitleForm : global::System.Windows.Forms.Form
{
	// Token: 0x0600000D RID: 13 RVA: 0x0000F918 File Offset: 0x0000DB18
	protected override void Dispose(bool disposing)
	{
		if (disposing && this.d != null)
		{
			this.d.Dispose();
		}
		base.Dispose(disposing);
	}

	// Token: 0x04000006 RID: 6
	private global::System.ComponentModel.IContainer d = null;
}
