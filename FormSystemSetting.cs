using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using Agiso;
using Agiso.Utils;
using Microsoft.Win32;

namespace AliwwClient
{
	// Token: 0x02000034 RID: 52
	public partial class FormSystemSetting : BaseForm
	{
		// Token: 0x0600014A RID: 330 RVA: 0x0001E580 File Offset: 0x0001C780
		public FormSystemSetting()
		{
			this.a();
			if (AppConfig.AllowAutoLogin)
			{
				base.StartPosition = FormStartPosition.Manual;
				base.Location = new Point(Screen.PrimaryScreen.Bounds.Width - base.Width, Screen.PrimaryScreen.Bounds.Height - base.Height - 40);
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0001E5F0 File Offset: 0x0001C7F0
		private static bool b()
		{
			bool flag;
			try
			{
				if (Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true).GetValue(Application.ProductName) != null)
				{
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(ex.ToString(), 1);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0001E64C File Offset: 0x0001C84C
		private static bool a(bool A_0)
		{
			if (A_0)
			{
				try
				{
					string text = Application.StartupPath + "\\" + Application.ProductName + ".exe";
					string productName = Application.ProductName;
					RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
					if (registryKey == null)
					{
						registryKey = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
					}
					registryKey.SetValue(productName, text);
					return true;
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(ex.ToString(), 1);
					return false;
				}
			}
			bool flag = false;
			bool flag2 = false;
			try
			{
				Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true).DeleteValue(Application.ProductName, false);
				flag = true;
			}
			catch (Exception ex2)
			{
				LogWriter.WriteLog(ex2.ToString(), 1);
			}
			try
			{
				Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true).DeleteValue(Application.ProductName, false);
				flag2 = true;
			}
			catch (Exception ex3)
			{
				LogWriter.WriteLog(ex3.ToString(), 1);
			}
			return flag || flag2;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0001E770 File Offset: 0x0001C970
		private void d(object sender, EventArgs e)
		{
			bool flag = FormSystemSetting.b();
			if (this.l.Checked != flag)
			{
				this.l.Checked = flag;
			}
			this.e.Checked = AppConfig.CurrentSystemSettingInfo.CloseWindowWhenSendedBool;
			this.j.Checked = !AppConfig.CurrentSystemSettingInfo.DisableCloseWindowWhenAutoReply;
			this.k.Checked = AppConfig.CurrentSystemSettingInfo.AllowSendExpression;
			this.c.Checked = AppConfig.CurrentSystemSettingInfo.AllowSetMinimizeCustomerBenchWindowWhileSendSuccess;
			this.d.Checked = AppConfig.CurrentSystemSettingInfo.DisableAutoFitEnterOrCtrlEnter;
			this.s.Checked = AppConfig.CurrentSystemSettingInfo.AutoSendBeforeMsg;
			this.o.Text = AppConfig.CurrentSystemSettingInfo.AliwwMessageLengthMax.ToString();
			this.n.Checked = AppConfig.CurrentSystemSettingInfo.AllowGetMsgByWebSocket;
			string sendMessageHotKey = AppConfig.CurrentSystemSettingInfo.SendMessageHotKey;
			string text = sendMessageHotKey;
			if (!(text == "^{ENTER}"))
			{
				if (text == "{ENTER}")
				{
				}
				this.f.Checked = true;
			}
			else
			{
				this.h.Checked = true;
			}
			this.g.Text = AppConfig.CurrentSystemSettingInfo.SendInterval.ToString();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0001E8B8 File Offset: 0x0001CAB8
		private void c(object sender, EventArgs e)
		{
			if (!FormSystemSetting.a(this.l.Checked))
			{
				MessageBox.Show("设置失败，一般是因为没有权限修改注册表，也可能是被安全软件拦截。\r\n如果是win7或win8的用户，请用管理员身份运行。\r\n具体方法：\r\n\r\n按住Shift键-->右键桌面的图标-->以管理员身份运行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				if (this.l.Checked)
				{
					this.l.Checked = false;
				}
				else
				{
					this.l.Checked = true;
				}
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0001E918 File Offset: 0x0001CB18
		private void b(object sender, EventArgs e)
		{
			if (this.k.Checked)
			{
				MessageBox.Show("启用该选项后，发送瞬间你在使用的粘贴板会被更改。\r\n原始粘贴板将在发送结束后自动还原！\r\n另外，发送的内容中请勿以表情开头，否则将发送失败。\r\n可将表情放在内容中间或结尾。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0001E948 File Offset: 0x0001CB48
		private void a(object sender, EventArgs e)
		{
			AppConfig.CurrentSystemSettingInfo.SendMessageHotKey = (this.f.Checked ? "{ENTER}" : "^{ENTER}");
			AppConfig.CurrentSystemSettingInfo.CloseWindowWhenSendedBool = this.e.Checked;
			AppConfig.CurrentSystemSettingInfo.CloseWindowBeforeSendBool = false;
			AppConfig.CurrentSystemSettingInfo.DisableCloseWindowWhenAutoReply = !this.j.Checked;
			AppConfig.CurrentSystemSettingInfo.AllowSendExpression = this.k.Checked;
			AppConfig.CurrentSystemSettingInfo.AllowSetMinimizeCustomerBenchWindowWhileSendSuccess = this.c.Checked;
			AppConfig.CurrentSystemSettingInfo.DisableAutoFitEnterOrCtrlEnter = this.d.Checked;
			AppConfig.CurrentSystemSettingInfo.AutoSendBeforeMsg = this.s.Checked;
			int num = Util.ToInt(this.g.Text);
			if (num <= 500)
			{
				num = 500;
			}
			else if (num > 5000)
			{
				num = 5000;
			}
			AppConfig.CurrentSystemSettingInfo.SendInterval = num;
			AppConfig.CurrentSystemSettingInfo.AllowGetMsgByWebSocket = this.n.Checked;
			int num2 = Util.ToInt(this.o.Text);
			if (num2 < 400)
			{
				num2 = 400;
			}
			else if (num2 > 2000)
			{
				num2 = 2000;
			}
			AppConfig.CurrentSystemSettingInfo.AliwwMessageLengthMax = num2;
			try
			{
				if (AppConfig.SaveConfig())
				{
					if (this.n.Checked)
					{
						global::k.a().OpenWwMsgWebSocket();
					}
					else
					{
						global::k.a().CloseWwMsgWebSocket();
					}
				}
				base.Close();
			}
			catch (ConfigurationErrorsException)
			{
				global::k.a().ShowPermitMessageBox();
			}
			catch
			{
				MessageBox.Show("保存失败！", "失败提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0001EB40 File Offset: 0x0001CD40
		private void a()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormSystemSetting));
			this.r = new Label();
			this.q = new Label();
			this.n = new CheckBox();
			this.o = new TextBox();
			this.p = new Label();
			this.c = new CheckBox();
			this.d = new CheckBox();
			this.e = new CheckBox();
			this.f = new RadioButton();
			this.g = new TextBox();
			this.h = new RadioButton();
			this.i = new Label();
			this.j = new CheckBox();
			this.k = new CheckBox();
			this.l = new CheckBox();
			this.m = new Button();
			this.s = new CheckBox();
			base.SuspendLayout();
			this.r.AutoSize = true;
			this.r.Location = new Point(213, 207);
			this.r.Name = "label2";
			this.r.Size = new Size(53, 12);
			this.r.TabIndex = 256;
			this.r.Text = "500-5000";
			this.q.AutoSize = true;
			this.q.Location = new Point(213, 230);
			this.q.Name = "label1";
			this.q.Size = new Size(53, 12);
			this.q.TabIndex = 255;
			this.q.Text = "400-2000";
			this.n.AutoSize = true;
			this.n.Location = new Point(33, 160);
			this.n.Name = "cbAllowGetMsgByWebSocket";
			this.n.Size = new Size(120, 16);
			this.n.TabIndex = 254;
			this.n.Text = "启用实时获取消息";
			this.n.UseVisualStyleBackColor = true;
			this.o.Location = new Point(145, 227);
			this.o.Name = "txtAliwwMessageLengthMax";
			this.o.Size = new Size(62, 21);
			this.o.TabIndex = 253;
			this.o.Text = "0";
			this.p.AutoSize = true;
			this.p.Location = new Point(29, 230);
			this.p.Name = "label6";
			this.p.Size = new Size(113, 12);
			this.p.TabIndex = 252;
			this.p.Text = "消息分段最大长度：";
			this.c.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.c.AutoSize = true;
			this.c.Location = new Point(32, 113);
			this.c.Name = "CbMinWinWhileSent";
			this.c.Size = new Size(180, 16);
			this.c.TabIndex = 246;
			this.c.Text = "发送成功后最小化客服工作台";
			this.c.UseVisualStyleBackColor = true;
			this.d.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.d.AutoSize = true;
			this.d.Location = new Point(32, 49);
			this.d.Name = "CbEnterAutoFit";
			this.d.Size = new Size(186, 16);
			this.d.TabIndex = 251;
			this.d.Text = "禁用Enter、Ctrl+Enter自适应";
			this.d.UseVisualStyleBackColor = true;
			this.e.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.e.AutoSize = true;
			this.e.Location = new Point(32, 92);
			this.e.Name = "CbCloseWhileSent";
			this.e.Size = new Size(156, 16);
			this.e.TabIndex = 241;
			this.e.Text = "发送成功后关闭聊天窗口";
			this.e.UseVisualStyleBackColor = true;
			this.f.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.f.AutoSize = true;
			this.f.Checked = true;
			this.f.Location = new Point(31, 27);
			this.f.Name = "RbEnter1";
			this.f.Size = new Size(77, 16);
			this.f.TabIndex = 239;
			this.f.TabStop = true;
			this.f.Text = "Enter发送";
			this.f.UseVisualStyleBackColor = true;
			this.g.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.g.Location = new Point(145, 204);
			this.g.Name = "TxtSendInterval";
			this.g.Size = new Size(62, 21);
			this.g.TabIndex = 248;
			this.h.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.h.AutoSize = true;
			this.h.Location = new Point(110, 27);
			this.h.Name = "RbEnter2";
			this.h.Size = new Size(107, 16);
			this.h.TabIndex = 240;
			this.h.Text = "Ctrl+Enter发送";
			this.h.UseVisualStyleBackColor = true;
			this.i.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.i.AutoSize = true;
			this.i.Location = new Point(29, 207);
			this.i.Name = "label3";
			this.i.Size = new Size(113, 12);
			this.i.TabIndex = 247;
			this.i.Text = "发送间隔多少毫秒：";
			this.j.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.j.AutoSize = true;
			this.j.Checked = true;
			this.j.CheckState = CheckState.Checked;
			this.j.Location = new Point(32, 70);
			this.j.Name = "CbCloseAfterAutoReply";
			this.j.Size = new Size(156, 16);
			this.j.TabIndex = 244;
			this.j.Text = "智能答复后关闭聊天窗口";
			this.j.UseVisualStyleBackColor = true;
			this.k.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.k.AutoSize = true;
			this.k.Location = new Point(110, 137);
			this.k.Name = "CbAllowEmoticon";
			this.k.Size = new Size(96, 16);
			this.k.TabIndex = 245;
			this.k.Text = "允许发送表情";
			this.k.UseVisualStyleBackColor = true;
			this.k.Click += this.b;
			this.l.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.l.AutoSize = true;
			this.l.Location = new Point(32, 137);
			this.l.Name = "CbAutoRun";
			this.l.Size = new Size(72, 16);
			this.l.TabIndex = 242;
			this.l.Text = "开机启动";
			this.l.UseVisualStyleBackColor = true;
			this.l.CheckedChanged += this.c;
			this.m.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.m.Location = new Point(181, 256);
			this.m.Name = "BtnSaveConfig";
			this.m.Size = new Size(85, 26);
			this.m.TabIndex = 243;
			this.m.Text = "保存设置(&S)";
			this.m.UseVisualStyleBackColor = true;
			this.m.Click += this.a;
			this.s.AutoSize = true;
			this.s.Location = new Point(33, 182);
			this.s.Name = "cbAutoSendBeforeMsg";
			this.s.Size = new Size(210, 16);
			this.s.TabIndex = 257;
			this.s.Text = "自动发送5小时内未发送的订单消息";
			this.s.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(291, 313);
			base.Controls.Add(this.s);
			base.Controls.Add(this.r);
			base.Controls.Add(this.q);
			base.Controls.Add(this.n);
			base.Controls.Add(this.o);
			base.Controls.Add(this.p);
			base.Controls.Add(this.c);
			base.Controls.Add(this.d);
			base.Controls.Add(this.e);
			base.Controls.Add(this.f);
			base.Controls.Add(this.g);
			base.Controls.Add(this.h);
			base.Controls.Add(this.i);
			base.Controls.Add(this.j);
			base.Controls.Add(this.k);
			base.Controls.Add(this.l);
			base.Controls.Add(this.m);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormSystemSetting";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "发送选项";
			base.Load += this.d;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000159 RID: 345
		private CheckBox c;

		// Token: 0x0400015A RID: 346
		private CheckBox d;

		// Token: 0x0400015B RID: 347
		private CheckBox e;

		// Token: 0x0400015C RID: 348
		private RadioButton f;

		// Token: 0x0400015D RID: 349
		private TextBox g;

		// Token: 0x0400015E RID: 350
		private RadioButton h;

		// Token: 0x0400015F RID: 351
		private Label i;

		// Token: 0x04000160 RID: 352
		private CheckBox j;

		// Token: 0x04000161 RID: 353
		private CheckBox k;

		// Token: 0x04000162 RID: 354
		private CheckBox l;

		// Token: 0x04000163 RID: 355
		private Button m;

		// Token: 0x04000164 RID: 356
		private CheckBox n;

		// Token: 0x04000165 RID: 357
		private TextBox o;

		// Token: 0x04000166 RID: 358
		private Label p;

		// Token: 0x04000167 RID: 359
		private Label q;

		// Token: 0x04000168 RID: 360
		private Label r;

		// Token: 0x04000169 RID: 361
		private CheckBox s;
	}
}
