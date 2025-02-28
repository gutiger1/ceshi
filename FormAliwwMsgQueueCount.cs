using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Agiso;
using Agiso.Object;
using Agiso.Utils;
using AliwwClient.Cache;
using AliwwClient.Manager;

namespace AliwwClient
{
	// Token: 0x02000027 RID: 39
	public partial class FormAliwwMsgQueueCount : BaseForm
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x000165C4 File Offset: 0x000147C4
		public FormAliwwMsgQueueCount()
		{
			this.a();
			if (AppConfig.AllowAutoLogin)
			{
				base.StartPosition = FormStartPosition.Manual;
				base.Location = new Point(Screen.PrimaryScreen.Bounds.Width - base.Width, Screen.PrimaryScreen.Bounds.Height - base.Height - 40);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00016634 File Offset: 0x00014834
		public static FormAliwwMsgQueueCount FormAliwwMsgQueueCountInstance
		{
			get
			{
				if (FormAliwwMsgQueueCount.a == null || FormAliwwMsgQueueCount.a.IsDisposed)
				{
					FormAliwwMsgQueueCount.a = new FormAliwwMsgQueueCount();
				}
				return FormAliwwMsgQueueCount.a;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00016668 File Offset: 0x00014868
		private void g(object sender, EventArgs e)
		{
			this.c.Columns.Clear();
			this.c.Columns.Add("卖家", 300, HorizontalAlignment.Left);
			this.c.Columns.Add("数量", 50, HorizontalAlignment.Center);
			this.h.Checked = AppConfig.AllowAutoLogin;
			if (AppConfig.AllowAutoLogin)
			{
				this.i.Visible = true;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000166E8 File Offset: 0x000148E8
		public void BindListView(List<KeyValuePair<string, int>> list)
		{
			this.c.Items.Clear();
			if (list != null && list.Count != 0)
			{
				int num = 0;
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].Value > 0)
					{
						num += list[i].Value;
						ListViewItem listViewItem = new ListViewItem(new string[]
						{
							list[i].Key,
							list[i].Value.ToString()
						});
						this.c.Items.Add(listViewItem);
					}
				}
				this.Text = string.Format("消息队列数量:{0}", num);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000167C0 File Offset: 0x000149C0
		private void f(object sender, EventArgs e)
		{
			if (base.Visible)
			{
				this.d.Enabled = true;
			}
			else
			{
				this.d.Enabled = false;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000167F0 File Offset: 0x000149F0
		private void e(object sender, EventArgs e)
		{
			try
			{
				if (Form1.AmiThreadProcessing != null)
				{
					this.Text = string.Format("当前：{0}({1})", Form1.AmiThreadProcessing.SellerNick, Form1.AmiThreadProcessing.MsgId);
				}
				else
				{
					this.Text = "消息队列数量（无处理中消息）";
				}
			}
			catch
			{
				this.Text = "消息队列数量（无处理中消息）";
			}
			List<KeyValuePair<string, int>> msgQueueCount = AppConfig.GetMsgQueueCount();
			if (msgQueueCount != null)
			{
				this.BindListView(msgQueueCount);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00016874 File Offset: 0x00014A74
		private void d(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("该操作将重启消息发送的线程，是否继续？", "消息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
			if (dialogResult == DialogResult.OK)
			{
				k.a().RestartSendAndAutoReplyThread();
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000168A8 File Offset: 0x00014AA8
		private void c(object sender, EventArgs e)
		{
			string text = this.g.Text;
			if (AppConfig.DictSellerExecuteCache.ContainsKey(text))
			{
				SellerCache sellerCache = AppConfig.DictSellerExecuteCache[text];
				if (sellerCache != null)
				{
					sellerCache.RoolbackMsgs(this.h.Checked);
				}
			}
			this.g.Text = "";
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00016904 File Offset: 0x00014B04
		private void b(object sender, EventArgs e)
		{
			if (this.c.SelectedItems != null && this.c.SelectedItems.Count > 0)
			{
				this.g.Text = this.c.SelectedItems[0].SubItems[0].Text;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00016964 File Offset: 0x00014B64
		private void a(object sender, EventArgs e)
		{
			if (AppConfig.AllowAutoLogin)
			{
				DialogResult dialogResult = MessageBox.Show(((Form1.AmiThreadProcessing == null) ? "暂无消息" : JSON.Encode(Form1.AmiThreadProcessing)) + "\r\n点击确定，关闭当前消息对应的千牛", "当前消息", MessageBoxButtons.OKCancel);
				if (dialogResult == DialogResult.OK && Form1.AmiThreadProcessing != null)
				{
					AldsAccountInfo agentAccountInfo = AppConfig.GetAgentAccountInfo(Form1.AmiThreadProcessing.SellerNick);
					if (agentAccountInfo != null)
					{
						ImKillManager.Kill(agentAccountInfo.UserNick, "关闭当前消息对应的千牛", true);
					}
				}
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00016A18 File Offset: 0x00014C18
		private void a()
		{
			this.b = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormAliwwMsgQueueCount));
			this.d = new Timer(this.b);
			this.e = new Button();
			this.c = new ListView();
			this.f = new Button();
			this.g = new TextBox();
			this.h = new CheckBox();
			this.i = new Button();
			base.SuspendLayout();
			this.d.Interval = 1000;
			this.d.Tick += this.e;
			this.e.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.e.Location = new Point(346, 371);
			this.e.Name = "BtnRestartThread";
			this.e.Size = new Size(75, 23);
			this.e.TabIndex = 1;
			this.e.Text = "重启线程";
			this.e.UseVisualStyleBackColor = true;
			this.e.Click += this.d;
			this.c.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.c.FullRowSelect = true;
			this.c.HideSelection = false;
			this.c.Location = new Point(12, 34);
			this.c.Name = "lvQueueCount";
			this.c.Size = new Size(409, 331);
			this.c.TabIndex = 0;
			this.c.UseCompatibleStateImageBehavior = false;
			this.c.View = View.Details;
			this.c.SelectedIndexChanged += this.b;
			this.f.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.f.Location = new Point(330, 4);
			this.f.Name = "btnStopSend";
			this.f.Size = new Size(91, 23);
			this.f.TabIndex = 2;
			this.f.Text = "停止消息发送";
			this.f.UseVisualStyleBackColor = true;
			this.f.Click += this.c;
			this.g.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.g.Location = new Point(12, 6);
			this.g.Name = "txtSellerNick";
			this.g.Size = new Size(229, 21);
			this.g.TabIndex = 3;
			this.h.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.h.AutoSize = true;
			this.h.Location = new Point(247, 8);
			this.h.Name = "cbReportToServer";
			this.h.Size = new Size(84, 16);
			this.h.TabIndex = 4;
			this.h.Text = "上报服务器";
			this.h.UseVisualStyleBackColor = true;
			this.i.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.i.Location = new Point(247, 371);
			this.i.Name = "btnSeeMsg";
			this.i.Size = new Size(93, 23);
			this.i.TabIndex = 5;
			this.i.Text = "查看当前消息";
			this.i.UseVisualStyleBackColor = true;
			this.i.Visible = false;
			this.i.Click += this.a;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(433, 399);
			base.Controls.Add(this.i);
			base.Controls.Add(this.h);
			base.Controls.Add(this.g);
			base.Controls.Add(this.f);
			base.Controls.Add(this.e);
			base.Controls.Add(this.c);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormAliwwMsgQueueCount";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "消息队列数量";
			base.Load += this.g;
			base.VisibleChanged += this.f;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000BD RID: 189
		private static FormAliwwMsgQueueCount a;

		// Token: 0x040000BF RID: 191
		private ListView c;

		// Token: 0x040000C0 RID: 192
		private Timer d;

		// Token: 0x040000C1 RID: 193
		private Button e;

		// Token: 0x040000C2 RID: 194
		private Button f;

		// Token: 0x040000C3 RID: 195
		private TextBox g;

		// Token: 0x040000C4 RID: 196
		private CheckBox h;

		// Token: 0x040000C5 RID: 197
		private Button i;
	}
}
