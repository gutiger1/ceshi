using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Agiso;
using Agiso.Object;

namespace AliwwClient
{
	// Token: 0x02000030 RID: 48
	[ComVisible(true)]
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	public partial class FormLogin : BaseForm
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00002917 File Offset: 0x00000B17
		public FormLogin(Action<string, string> action)
		{
			this.a();
			this.a = action;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0001D680 File Offset: 0x0001B880
		private void a(object sender, EventArgs e)
		{
			WebBrowser webBrowser = this.c;
			string text = "https://oauth.taobao.com/authorize?response_type=code&client_id=12292026&redirect_uri=https%3A%2F%2Falds.agiso.com%2FLogin.aspx&state=RedirectUrl%3Ahttp%3A%2F%2Falds.agiso.com%3A80%2FLoginAliwwClient.aspx&from_site=fuwu&rdm=";
			Random random = new Random();
			webBrowser.Navigate(text + ((random != null) ? random.ToString() : null));
			this.c.ScriptErrorsSuppressed = true;
			this.c.ObjectForScripting = this;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000022DD File Offset: 0x000004DD
		private void a(object sender, WebBrowserNavigatedEventArgs e)
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00002934 File Offset: 0x00000B34
		public void ShowMessage(string msg)
		{
			MessageBox.Show(msg);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0001D6CC File Offset: 0x0001B8CC
		public string GetAccountList()
		{
			ConcurrentDictionary<string, AldsAccountInfo> userDict = AppConfig.UserDict;
			StringBuilder stringBuilder = new StringBuilder();
			if (userDict != null && userDict.Count > 0)
			{
				foreach (KeyValuePair<string, AldsAccountInfo> keyValuePair in userDict)
				{
					stringBuilder.Append("," + keyValuePair.Key);
				}
			}
			string text;
			if (stringBuilder.Length > 0)
			{
				text = stringBuilder.ToString().Substring(1);
			}
			else
			{
				text = "";
			}
			return text;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0001D76C File Offset: 0x0001B96C
		public void AddAccount(string userName, string aliwwClientPassword)
		{
			userName = HttpUtility.UrlDecode(userName);
			aliwwClientPassword = HttpUtility.UrlDecode(aliwwClientPassword);
			if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(aliwwClientPassword))
			{
				MessageBox.Show("登录失败，未查找到该账号的信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				this.a(userName.Trim(), aliwwClientPassword.Trim());
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0001D7FC File Offset: 0x0001B9FC
		private void a()
		{
			this.c = new WebBrowser();
			base.SuspendLayout();
			this.c.Dock = DockStyle.Fill;
			this.c.Location = new Point(0, 0);
			this.c.MinimumSize = new Size(20, 20);
			this.c.Name = "webBrowser1";
			this.c.Size = new Size(999, 450);
			this.c.TabIndex = 0;
			this.c.Navigated += this.a;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(999, 450);
			base.Controls.Add(this.c);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormLogin";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "淘宝账号密码登录";
			base.Load += this.a;
			base.ResumeLayout(false);
		}

		// Token: 0x04000146 RID: 326
		private Action<string, string> a;

		// Token: 0x04000148 RID: 328
		private WebBrowser c;
	}
}
