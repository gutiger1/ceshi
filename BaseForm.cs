using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Agiso;
using AliwwClient.Properties;

namespace AliwwClient
{
	// Token: 0x0200001E RID: 30
	[ComVisible(true)]
	public partial class BaseForm : Form
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00002699 File Offset: 0x00000899
		public BaseForm()
		{
			base.Load += this.a;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000026B4 File Offset: 0x000008B4
		private void a(object sender, EventArgs e)
		{
			base.Icon = Resources.Icon1;
			AppConfig.SetAllTextBoxSelectAllSupport(this);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00011864 File Offset: 0x0000FA64
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if ((msg.Msg == 256) | (msg.Msg == 260))
			{
				if (keyData == Keys.Escape)
				{
					base.DialogResult = DialogResult.Cancel;
					base.Close();
				}
			}
			return false;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000118A8 File Offset: 0x0000FAA8
		private void a()
		{
			new ComponentResourceManager(typeof(BaseForm));
			base.SuspendLayout();
			base.ClientSize = new Size(284, 262);
			base.Icon = Resources.Icon1;
			base.Name = "BaseForm";
			base.ResumeLayout(false);
		}
	}
}
