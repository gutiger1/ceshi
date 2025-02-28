using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace AliwwClient
{
	// Token: 0x0200005E RID: 94
	[ComVisible(true)]
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	public partial class Form2 : BaseForm
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x000030A3 File Offset: 0x000012A3
		public Form2()
		{
			this.a();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00033DA4 File Offset: 0x00031FA4
		private void a(object sender, EventArgs e)
		{
			base.Location = new Point(Screen.GetWorkingArea(this).Right - base.Width, Screen.GetWorkingArea(this).Bottom - base.Height);
			this.b.ObjectForScripting = this;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000030B9 File Offset: 0x000012B9
		public void JsUpgrade()
		{
			k.a().AliwwClient_OnUpgrade(false);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00033DF4 File Offset: 0x00031FF4
		public void ShowInfo(string title, string content, InfoBoxContentType ibct)
		{
			if (string.IsNullOrEmpty(title))
			{
				title = "系统通知";
			}
			this.Text = title;
			switch (ibct)
			{
			case InfoBoxContentType.Text:
				this.b.DocumentText = "<html><header><style>body{font-size:12px; font-family:微软雅黑 宋体 arial;}</style></header><body>" + content + "</body></html>";
				break;
			case InfoBoxContentType.Html:
				this.b.DocumentText = content;
				break;
			case InfoBoxContentType.Url:
				this.b.Url = new Uri(content);
				break;
			}
			base.Show();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00033EA4 File Offset: 0x000320A4
		private void a()
		{
			this.b = new WebBrowser();
			base.SuspendLayout();
			this.b.AllowNavigation = false;
			this.b.AllowWebBrowserDrop = false;
			this.b.Dock = DockStyle.Fill;
			this.b.IsWebBrowserContextMenuEnabled = false;
			this.b.Location = new Point(0, 0);
			this.b.MinimumSize = new Size(20, 20);
			this.b.Name = "webBrowser1";
			this.b.ScrollBarsEnabled = false;
			this.b.Size = new Size(244, 161);
			this.b.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(244, 161);
			base.Controls.Add(this.b);
			base.MaximizeBox = false;
			this.MaximumSize = new Size(260, 200);
			base.MinimizeBox = false;
			this.MinimumSize = new Size(260, 200);
			base.Name = "Form2";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			this.Text = "提示";
			base.TopMost = true;
			base.Load += this.a;
			base.ResumeLayout(false);
		}

		// Token: 0x040002BA RID: 698
		private WebBrowser b;
	}
}
