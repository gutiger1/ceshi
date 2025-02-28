using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Agiso;
using AliwwClient.Properties;

namespace AliwwClient
{
	// Token: 0x02000035 RID: 53
	public partial class FormTip : Form
	{
		// Token: 0x06000153 RID: 339 RVA: 0x0001F63C File Offset: 0x0001D83C
		public FormTip()
		{
			this.a();
			base.Icon = Resources.Icon1;
			this.c.Text = "您好，以下账号千牛通信失败。可尝试以下方法修复：\r\n\r\n 1：重启千牛；2：重启助手；3：联系在线客服寻求帮助（旺旺在线客服：" + AppConfig.GetContactWay().Wangwang + "）\r\n \r\n如果想忽略这个帐号的提示，可在帐号管理界面关闭智能答复和发送开关";
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0001F690 File Offset: 0x0001D890
		public void Toggle(List<string> unConnectUserNicks)
		{
			FormTip.a a = new FormTip.a();
			a.a = unConnectUserNicks;
			a.b = this;
			this.d.Items.Clear();
			if (a.a == null || a.a.Count <= 0)
			{
				base.Hide();
			}
			else
			{
				base.Show();
				if (base.InvokeRequired)
				{
					Action action = new Action(a.c);
					base.Invoke(action);
				}
				else
				{
					foreach (string text in a.a)
					{
						this.d.Items.Add(text);
					}
				}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0001F764 File Offset: 0x0001D964
		private void a(object sender, EventArgs e)
		{
			base.Location = new Point(Screen.GetWorkingArea(this).Right - base.Width, Screen.GetWorkingArea(this).Bottom - base.Height);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0001F7D8 File Offset: 0x0001D9D8
		private void a()
		{
			this.b = new Panel();
			this.d = new ListBox();
			this.c = new Label();
			this.b.SuspendLayout();
			base.SuspendLayout();
			this.b.Controls.Add(this.d);
			this.b.Controls.Add(this.c);
			this.b.Dock = DockStyle.Top;
			this.b.Location = new Point(0, 0);
			this.b.Name = "panel1";
			this.b.Size = new Size(420, 218);
			this.b.TabIndex = 1;
			this.d.FormattingEnabled = true;
			this.d.ItemHeight = 12;
			this.d.Location = new Point(14, 99);
			this.d.Name = "listBox1";
			this.d.Size = new Size(386, 100);
			this.d.TabIndex = 4;
			this.c.Location = new Point(12, 9);
			this.c.Name = "label1";
			this.c.Size = new Size(405, 72);
			this.c.TabIndex = 1;
			this.c.Text = "您好，以下账号千牛通信失败。可尝试以下方法修复：\r\n\r\n 1：重启千牛；2：重启助手；3：联系在线客服寻求帮助（旺旺在线客服：{AppConfig.GetContactWay().Wangwang}）\r\n \r\n如果想忽略这个帐号的提示，可在帐号管理界面关闭智能答复和发送开关";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(420, 225);
			base.Controls.Add(this.b);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormTip";
			this.Text = "Agiso旺旺发送助手提示";
			base.Load += this.a;
			this.b.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0400016B RID: 363
		private Panel b;

		// Token: 0x0400016C RID: 364
		private Label c;

		// Token: 0x0400016D RID: 365
		private ListBox d;

		// Token: 0x02000036 RID: 54
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x06000159 RID: 345 RVA: 0x0001F9E0 File Offset: 0x0001DBE0
			internal void c()
			{
				foreach (string text in this.a)
				{
					this.b.d.Items.Add(text);
				}
			}

			// Token: 0x0400016E RID: 366
			public List<string> a;

			// Token: 0x0400016F RID: 367
			public FormTip b;
		}
	}
}
