using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Agiso;
using Agiso.DbManager;
using Agiso.Object;
using Agiso.Utils;
using Agiso.WwService.Sdk.Domain;
using Agiso.WwService.Sdk.Response;
using AliwwClient.Properties;
using AliwwClient.WebSocketServer;

namespace AliwwClient
{
	// Token: 0x02000031 RID: 49
	public partial class FormPrivateKeyLogin : BaseForm
	{
		// Token: 0x0600013C RID: 316 RVA: 0x0000293D File Offset: 0x00000B3D
		public FormPrivateKeyLogin()
		{
			this.a();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0001D92C File Offset: 0x0001BB2C
		private void b(object sender, EventArgs e)
		{
			FormPrivateKeyLogin.a a = new FormPrivateKeyLogin.a();
			string text = this.c.Text.Trim();
			if (text.Contains("："))
			{
				DialogResult dialogResult = MessageBox.Show("输入的帐号中带有全角冒号“：”，猜测可能您是要用子帐号登录，是否自动替换为半角冒号“:”！", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
				DialogResult dialogResult2 = dialogResult;
				DialogResult dialogResult3 = dialogResult2;
				if (dialogResult3 == DialogResult.Cancel)
				{
					return;
				}
				if (dialogResult3 == DialogResult.Yes)
				{
					this.c.Text = text.Replace("：", ":");
				}
			}
			string text2 = this.c.Text.Trim();
			string text3 = this.h.Text.Trim();
			if (string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text3))
			{
				MessageBox.Show("请填写要添加的旺旺号和私钥！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				this.c.Focus();
			}
			else if (text2.ToCharArray().Length < 2)
			{
				MessageBox.Show("旺旺号填写不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				this.c.Focus();
			}
			else
			{
				text2 = text2.ToLower();
				if (AppConfig.UserDict.ContainsKey(text2))
				{
					MessageBox.Show("旺旺号已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.c.Focus();
				}
				else
				{
					LoginResponse loginResponse;
					try
					{
						loginResponse = AppConfig.WwServiceClient.Login(text2, text3);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						this.c.Focus();
						return;
					}
					if (loginResponse.IsError)
					{
						if (loginResponse.ErrMsg.Contains("私钥错误"))
						{
							DialogResult dialogResult4 = MessageBox.Show(loginResponse.ErrMsg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
							if (dialogResult4 == DialogResult.Yes)
							{
								Util.OpenLink("https://alds.agiso.com/aldsTb/#/Autologistics/AldsAgentSetting?showAliwwClient=true&key=2");
							}
						}
						else
						{
							MessageBox.Show(loginResponse.ErrMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						this.c.Focus();
					}
					else
					{
						AliwwClientAccount account = loginResponse.Account;
						if (!AppConfig.AllowAutoLogin)
						{
							new FormAlert().ShowDialog(this);
						}
						a.a = new AldsAccountInfo();
						a.a.Map(account);
						a.a.Password = text3;
						if (a.a.IsValid)
						{
							a.a.AutoSendOnOff = true;
						}
						if (!a.a.IsValid && a.a.EnableAutoReply)
						{
							a.a.AutoReplyOnOff = true;
						}
						a.a.QnConnectionStatus = (AppConfig.GetUserCacheOrCreate(text2).IsSessionNull ? "-" : "√");
						if (AppConfig.UserDict.TryAdd(text2, a.a))
						{
							AldsAccountManager.Insert(a.a);
							global::k.a().Invoke(new Action(a.b));
							AppConfig.WwWebSocketClient.AddOnlineUser(text2, text3, AppConfig.ApplicationUuid);
						}
						AldsBehavior aldsSession = AppConfig.GetUserCacheOrCreate(text2).AldsSession;
						if (aldsSession != null)
						{
							aldsSession.ChangeLoginState(true);
						}
						base.Close();
					}
				}
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0001DC38 File Offset: 0x0001BE38
		private void b(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.g.PerformClick();
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0001DC38 File Offset: 0x0001BE38
		private void a(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.g.PerformClick();
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00002953 File Offset: 0x00000B53
		private void b(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Util.OpenLink("https://alds.agiso.com/aldsTb/#/Autologistics/AldsAgentSetting?showAliwwClient=true&key=2&ver=" + AppConfig.GetCurrentApplicationVersion());
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0001DC5C File Offset: 0x0001BE5C
		private void a(object sender, EventArgs e)
		{
			FormPrivateKeyLogin.b b = new FormPrivateKeyLogin.b();
			b.a = null;
			b.b = null;
			Action<string, string> action = new Action<string, string>(b.d);
			FormLogin formLogin = new FormLogin(action);
			formLogin.ShowDialog();
			WebBrowser webBrowser = formLogin.Controls["webBrowser1"] as WebBrowser;
			if (webBrowser != null)
			{
				HtmlDocument document = webBrowser.Document;
				if (document != null)
				{
					document.ExecCommand("ClearAuthenticationCache", false, null);
				}
			}
			if (!string.IsNullOrEmpty(b.a) && !string.IsNullOrEmpty(b.b))
			{
				if (b.a.ToCharArray().Length < 2)
				{
					MessageBox.Show("旺旺号填写不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					b.a = b.a.ToLower();
					if (AppConfig.UserDict.ContainsKey(b.a))
					{
						MessageBox.Show("旺旺号已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						LoginResponse loginResponse = null;
						try
						{
							loginResponse = AppConfig.WwServiceClient.Login(b.a, b.b);
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							return;
						}
						if (loginResponse.IsError)
						{
							if (loginResponse.ErrMsg.Contains("私钥错误"))
							{
								DialogResult dialogResult = MessageBox.Show(loginResponse.ErrMsg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
								if (dialogResult == DialogResult.Yes)
								{
									Util.OpenLink("https://alds.agiso.com/aldsTb/#/Autologistics/AldsAgentSetting?showAliwwClient=true&key=2");
								}
							}
							else
							{
								MessageBox.Show(loginResponse.ErrMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
						}
						else
						{
							AliwwClientAccount account = loginResponse.Account;
							if (!AppConfig.AllowAutoLogin)
							{
								new FormAlert().ShowDialog(this);
							}
							b.c = new AldsAccountInfo();
							b.c.Map(account);
							b.c.Password = b.b;
							b.c.AutoSendOnOff = true;
							if (AppConfig.UserDict.TryAdd(b.a, b.c))
							{
								AldsAccountManager.Insert(b.c);
								global::k.a().Invoke(new Action(b.d));
								AppConfig.WwWebSocketClient.AddOnlineUser(b.a, b.b, AppConfig.ApplicationUuid);
							}
							AldsBehavior aldsSession = AppConfig.GetUserCacheOrCreate(b.a).AldsSession;
							if (aldsSession != null)
							{
								aldsSession.ChangeLoginState(true);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000296A File Offset: 0x00000B6A
		private void a(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Util.OpenLink("https://www.yuque.com/agiso/aldstb/eerb64");
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0001DF08 File Offset: 0x0001C108
		private void a()
		{
			this.b = new Panel();
			this.j = new LinkLabel();
			this.i = new Label();
			this.c = new TextBox();
			this.d = new Label();
			this.e = new LinkLabel();
			this.f = new Label();
			this.g = new Button();
			this.h = new TextBox();
			this.k = new Label();
			this.b.SuspendLayout();
			base.SuspendLayout();
			this.b.Controls.Add(this.k);
			this.b.Controls.Add(this.j);
			this.b.Controls.Add(this.i);
			this.b.Controls.Add(this.c);
			this.b.Controls.Add(this.d);
			this.b.Controls.Add(this.e);
			this.b.Controls.Add(this.f);
			this.b.Controls.Add(this.g);
			this.b.Controls.Add(this.h);
			this.b.Dock = DockStyle.Fill;
			this.b.Location = new Point(0, 0);
			this.b.Name = "panel1";
			this.b.Size = new Size(378, 217);
			this.b.TabIndex = 0;
			this.j.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.j.AutoSize = true;
			this.j.Location = new Point(158, 178);
			this.j.Name = "linkLabel1";
			this.j.Size = new Size(125, 12);
			this.j.TabIndex = 215;
			this.j.TabStop = true;
			this.j.Text = "添加自动发货插件教程";
			this.j.LinkClicked += this.a;
			this.i.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.i.AutoSize = true;
			this.i.Location = new Point(21, 154);
			this.i.Name = "label1";
			this.i.Size = new Size(329, 36);
			this.i.TabIndex = 214;
			this.i.Text = "如果登录千牛，可使用千牛自动发货插件“助手一键登录”，\r\n\r\n可省去账号私钥的输入。";
			this.c.Location = new Point(189, 26);
			this.c.MaxLength = 32;
			this.c.Name = "TxtLoginUserNick";
			this.c.Size = new Size(170, 21);
			this.c.TabIndex = 209;
			this.c.KeyDown += this.b;
			this.d.AutoSize = true;
			this.d.Location = new Point(22, 29);
			this.d.Name = "label14";
			this.d.Size = new Size(161, 12);
			this.d.TabIndex = 208;
			this.d.Text = "旺旺昵称（淘宝帐户昵称）：";
			this.e.AutoSize = true;
			this.e.Location = new Point(101, 68);
			this.e.Name = "LinkLabelSetPwd";
			this.e.Size = new Size(53, 12);
			this.e.TabIndex = 213;
			this.e.TabStop = true;
			this.e.Text = "查看私钥";
			this.e.LinkClicked += this.b;
			this.f.AutoSize = true;
			this.f.Location = new Point(16, 68);
			this.f.Name = "label13";
			this.f.Size = new Size(89, 12);
			this.f.TabIndex = 207;
			this.f.Text = "旺旺助手私钥（";
			this.g.Location = new Point(189, 102);
			this.g.Name = "BtnLoginAdd";
			this.g.Size = new Size(170, 25);
			this.g.TabIndex = 211;
			this.g.Text = "添加(&A)";
			this.g.UseVisualStyleBackColor = true;
			this.g.Click += this.b;
			this.h.Location = new Point(189, 64);
			this.h.Name = "TxtLoginPwd";
			this.h.PasswordChar = '*';
			this.h.Size = new Size(170, 21);
			this.h.TabIndex = 210;
			this.h.KeyDown += this.a;
			this.k.AutoSize = true;
			this.k.Location = new Point(153, 68);
			this.k.Name = "label2";
			this.k.Size = new Size(29, 12);
			this.k.TabIndex = 216;
			this.k.Text = "）：";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(378, 217);
			base.Controls.Add(this.b);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = Resources.Icon1;
			base.Name = "FormPrivateKeyLogin";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "账号私钥登陆";
			this.b.ResumeLayout(false);
			this.b.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0400014A RID: 330
		private Panel b;

		// Token: 0x0400014B RID: 331
		private TextBox c;

		// Token: 0x0400014C RID: 332
		private Label d;

		// Token: 0x0400014D RID: 333
		private LinkLabel e;

		// Token: 0x0400014E RID: 334
		private Label f;

		// Token: 0x0400014F RID: 335
		private Button g;

		// Token: 0x04000150 RID: 336
		private TextBox h;

		// Token: 0x04000151 RID: 337
		private Label i;

		// Token: 0x04000152 RID: 338
		private LinkLabel j;

		// Token: 0x04000153 RID: 339
		private Label k;

		// Token: 0x02000032 RID: 50
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x06000146 RID: 326 RVA: 0x00002977 File Offset: 0x00000B77
			internal void b()
			{
				AppConfig.UserList.Add(this.a);
			}

			// Token: 0x04000154 RID: 340
			public AldsAccountInfo a;
		}

		// Token: 0x02000033 RID: 51
		[CompilerGenerated]
		private sealed class b
		{
			// Token: 0x06000148 RID: 328 RVA: 0x00002989 File Offset: 0x00000B89
			internal void d(string A_0, string A_1)
			{
				this.a = A_0;
				this.b = A_1;
			}

			// Token: 0x06000149 RID: 329 RVA: 0x00002999 File Offset: 0x00000B99
			internal void d()
			{
				AppConfig.UserList.Add(this.c);
			}

			// Token: 0x04000155 RID: 341
			public string a;

			// Token: 0x04000156 RID: 342
			public string b;

			// Token: 0x04000157 RID: 343
			public AldsAccountInfo c;
		}
	}
}
