using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Agiso;
using Agiso.DbManager;
using Agiso.Object;
using Agiso.Utils;

namespace AliwwClient
{
	// Token: 0x02000028 RID: 40
	public partial class FormAutoReplyEdit : BaseForm
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00016F00 File Offset: 0x00015100
		public AutoReplyInfo ResultAutoReply
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00016F18 File Offset: 0x00015118
		public FormAutoReplyEdit(AutoReplyInfo autoReply, bool isAdd)
		{
			this.a = autoReply;
			this.b = isAdd;
			this.a();
			if (AppConfig.AllowAutoLogin)
			{
				base.StartPosition = FormStartPosition.Manual;
				base.Location = new Point(Screen.PrimaryScreen.Bounds.Width - base.Width, Screen.PrimaryScreen.Bounds.Height - base.Height - 40);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00016F98 File Offset: 0x00015198
		private void e(object sender, EventArgs e)
		{
			this.b();
			this.i.ValueMember = "ArTypeValue";
			this.i.DisplayMember = "ArTypeText";
			this.i.DataSource = AutoReplyManager.ArTypeList;
			this.i.SelectedValue = this.a.Type.GetHashCode();
			this.n.Text = ((this.a.Type == EnumArType.FirstReply) ? "" : this.a.KeyWord);
			if (string.IsNullOrEmpty(this.a.SellerNick))
			{
				this.j.SelectedItem = "【适用所有已添加的旺旺】";
			}
			else
			{
				this.j.SelectedItem = this.a.SellerNick;
			}
			this.l.Text = this.a.ReplyWord;
			this.l.Text = this.l.Text.Replace("\r\n", "\n").Replace("\n", "\r\n");
			this.aa.Value = ((this.a.ArStartTime == DateTime.MinValue || this.a.ArStartTime == null) ? DateTime.Now.Date : this.a.ArStartTime.Value);
			this.ad.Value = ((this.a.ArEndTime == DateTime.MinValue || this.a.ArEndTime == null) ? DateTime.Now.AddDays(1.0).Date.AddMinutes(-1.0) : this.a.ArEndTime.Value);
			this.ab.Text = ((this.a.IdNo == 0L) ? "100" : this.a.Grade.ToString());
			if (this.b)
			{
				this.p.Visible = false;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000282A File Offset: 0x00000A2A
		private void a(object sender, HelpEventArgs e)
		{
			Util.OpenLink("https://www.yuque.com/agiso/aldstb/wn7qdv");
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00002837 File Offset: 0x00000A37
		private void a(object sender, CancelEventArgs e)
		{
			this.a(null, null);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00002837 File Offset: 0x00000A37
		private void c(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.a(null, null);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00017208 File Offset: 0x00015408
		private void b(object sender, LinkLabelLinkClickedEventArgs e)
		{
			LinkLabel linkLabel = (LinkLabel)sender;
			this.a(linkLabel.Text);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00017228 File Offset: 0x00015428
		private void a(string A_0)
		{
			if (!string.IsNullOrEmpty(A_0))
			{
				int selectionStart = this.l.SelectionStart;
				this.l.Text = this.l.Text.Insert(selectionStart, A_0);
				this.l.Focus();
				this.l.SelectionStart = selectionStart;
				this.l.SelectionLength = A_0.Length;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002841 File Offset: 0x00000A41
		private void d(object sender, EventArgs e)
		{
			this.a.IdNo = 0L;
			this.c(sender, e);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00017290 File Offset: 0x00015490
		private void c(object sender, EventArgs e)
		{
			if (this.j.SelectedItem == null)
			{
				MessageBox.Show("未选店铺！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				this.j.Focus();
			}
			else
			{
				EnumArType enumArType = ((this.i.SelectedValue == null) ? EnumArType.Undefine : ((EnumArType)this.i.SelectedValue));
				string text = this.n.Text.Trim();
				string text2 = this.j.SelectedItem.ToString();
				string text3 = this.l.Text.Trim();
				if (enumArType == EnumArType.FirstReply)
				{
					text = "【Agiso首次智能答复Agiso】";
				}
				if (enumArType != EnumArType.NoMatching && string.IsNullOrEmpty(text))
				{
					MessageBox.Show("关键词必填写！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.n.Focus();
				}
				else if (string.IsNullOrEmpty(text2))
				{
					MessageBox.Show("店铺选择错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.j.Focus();
				}
				else if (enumArType == EnumArType.Undefine)
				{
					MessageBox.Show("类型选择错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.i.Focus();
				}
				else
				{
					if (text3.Contains("{@转接("))
					{
						Regex regex = new Regex("{@转接\\((.*?)\\)}");
						MatchCollection matchCollection = regex.Matches(text3);
						if (matchCollection.Count > 0)
						{
							if (matchCollection.Count > 1)
							{
								MessageBox.Show("指定转接客服时，只能指定一个转接客服，不能指定多个客服", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								this.j.Focus();
								return;
							}
							foreach (object obj in matchCollection)
							{
								Match match = (Match)obj;
								string text4 = (match.Groups[1].Value ?? "").Trim();
								if (!string.IsNullOrEmpty(text4) && !text2.Equals(Util.GetMasterNick(text4)))
								{
									MessageBox.Show("指定转接客服时，请选择子帐号相对应的店铺", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
									this.j.Focus();
									return;
								}
							}
						}
					}
					if (enumArType == EnumArType.Contains || enumArType == EnumArType.AcExpression)
					{
						int num;
						if (Util.AcExpressionContainsFullWidthKeyChar(text, out num))
						{
							DialogResult dialogResult = MessageBox.Show("检测到关键词中包含全角的关键字符“（）＋，”，是否自动替换为相应的半角签字“()+,”？", "关键词包含全角字符", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
							if (dialogResult == DialogResult.Yes)
							{
								text = Util.AcExpressionReplaceFullWidthKeyChar(text);
								this.n.Text = text;
							}
							else if (dialogResult == DialogResult.Cancel)
							{
								this.n.Focus();
								if (num >= 0)
								{
									this.n.Select(num, 1);
									return;
								}
								return;
							}
						}
						string text5;
						int num2;
						if (!Util.AcExpressionValid(text, out text5, out num2))
						{
							MessageBox.Show("AC表达式错了，" + text5, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							this.n.Focus();
							if (num2 >= 0)
							{
								this.n.Select(num2, 1);
								return;
							}
							return;
						}
					}
					else if (enumArType == EnumArType.Expression)
					{
						try
						{
							Assembly assembly = null;
							try
							{
								assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "\\Agiso.dll");
							}
							catch (FileNotFoundException ex)
							{
								LogWriter.WriteLog("FileNotFoundException" + ex.ToString(), 1);
								DialogResult dialogResult2 = MessageBox.Show("未找到Agiso.dll，是否前往下载？\r\n下载完之后放在助手目录下，有疑问联系在线客服。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
								if (dialogResult2 == DialogResult.OK)
								{
									Process.Start("http://download.agiso.com/AliwwClient/Agiso.dll");
								}
								return;
							}
							catch (Exception ex2)
							{
								LogWriter.WriteLog("Exception:" + ex2.ToString(), 1);
								MessageBox.Show("调用Agiso.dll出现异常，异常原因：" + ex2.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}
							Type type = assembly.GetType("Agiso.Dynamic.StringX");
							object obj2 = assembly.CreateInstance("Agiso.Dynamic.StringX", true, BindingFlags.Default, null, new object[] { "" }, CultureInfo.CurrentCulture, null);
							MethodInfo method = type.GetMethod("Execute");
							method.Invoke(obj2, new object[] { text });
						}
						catch (Exception ex3)
						{
							MessageBox.Show("公式错了，" + ex3.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							this.n.Focus();
							return;
						}
					}
					this.a.Type = enumArType;
					this.a.KeyWord = text;
					this.a.SellerNick = text2;
					this.a.ReplyWord = text3;
					this.a.ArStartTime = new DateTime?(this.aa.Value);
					this.a.ArEndTime = new DateTime?(this.ad.Value);
					if (!string.IsNullOrWhiteSpace(this.ab.Text))
					{
						try
						{
							this.a.Grade = Convert.ToInt64(this.ab.Text);
							goto IL_053D;
						}
						catch (Exception ex4)
						{
							MessageBox.Show("优先值必须为数字，" + ex4.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							this.ab.Focus();
							return;
						}
					}
					this.a.Grade = 100L;
					IL_053D:
					base.DialogResult = DialogResult.OK;
				}
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000285F File Offset: 0x00000A5F
		private void b(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00017860 File Offset: 0x00015A60
		private void a(object sender, EventArgs e)
		{
			EnumArType enumArType = ((this.i.SelectedValue == null) ? EnumArType.Undefine : ((EnumArType)this.i.SelectedValue));
			if (enumArType == EnumArType.NoMatching || enumArType == EnumArType.FirstReply)
			{
				this.n.Text = "";
				this.n.Enabled = false;
			}
			else
			{
				this.n.Enabled = true;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000178D0 File Offset: 0x00015AD0
		private void b()
		{
			this.j.Items.Clear();
			this.j.Items.Add("【适用所有已添加的旺旺】");
			foreach (AldsAccountInfo aldsAccountInfo in AppConfig.UserList)
			{
				if (aldsAccountInfo.EnableAutoReply)
				{
					if (AppConfig.CurrentSystemSettingInfo.AutoReplyBySellerNick)
					{
						string masterNick = Util.GetMasterNick(aldsAccountInfo.UserNick);
						if (!this.j.Items.Contains(masterNick))
						{
							this.j.Items.Add(masterNick);
						}
					}
					else
					{
						this.j.Items.Add(aldsAccountInfo.UserNick);
					}
				}
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000179A4 File Offset: 0x00015BA4
		private void a(object sender, LinkLabelLinkClickedEventArgs e)
		{
			using (FormTransferNick formTransferNick = new FormTransferNick())
			{
				formTransferNick.ShowDialog();
				if (!string.IsNullOrEmpty(formTransferNick.TransferNick))
				{
					string masterNick = Util.GetMasterNick(formTransferNick.TransferNick);
					this.j.SelectedItem = masterNick;
					this.a("{@转接(" + formTransferNick.TransferNick + ")}");
				}
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00017A50 File Offset: 0x00015C50
		private void a()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormAutoReplyEdit));
			this.d = new Panel();
			this.p = new Button();
			this.e = new Button();
			this.f = new Button();
			this.g = new GroupBox();
			this.ae = new Label();
			this.af = new Label();
			this.ac = new Label();
			this.ad = new DateTimePicker();
			this.aa = new DateTimePicker();
			this.ab = new TextBox();
			this.z = new Label();
			this.y = new Label();
			this.x = new LinkLabel();
			this.s = new LinkLabel();
			this.t = new LinkLabel();
			this.u = new LinkLabel();
			this.v = new LinkLabel();
			this.w = new LinkLabel();
			this.q = new Label();
			this.r = new Label();
			this.h = new LinkLabel();
			this.i = new ComboBox();
			this.j = new ComboBox();
			this.k = new Label();
			this.l = new TextBox();
			this.m = new Label();
			this.n = new TextBox();
			this.o = new Label();
			this.ah = new Label();
			this.ag = new Label();
			this.d.SuspendLayout();
			this.g.SuspendLayout();
			base.SuspendLayout();
			this.d.Controls.Add(this.p);
			this.d.Controls.Add(this.e);
			this.d.Controls.Add(this.f);
			this.d.Dock = DockStyle.Bottom;
			this.d.Location = new Point(0, 395);
			this.d.Name = "panel1";
			this.d.Size = new Size(630, 48);
			this.d.TabIndex = 20;
			this.p.Location = new Point(378, 13);
			this.p.Name = "BtnSaveAs";
			this.p.Size = new Size(75, 23);
			this.p.TabIndex = 510;
			this.p.Text = "另存为(&A)";
			this.p.UseVisualStyleBackColor = true;
			this.p.Click += this.d;
			this.e.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.e.Location = new Point(543, 13);
			this.e.Name = "BtnCancel";
			this.e.Size = new Size(75, 23);
			this.e.TabIndex = 1000;
			this.e.Text = "取消(&C)";
			this.e.UseVisualStyleBackColor = true;
			this.e.Click += this.b;
			this.f.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.f.Location = new Point(462, 13);
			this.f.Name = "BtnSave";
			this.f.Size = new Size(75, 23);
			this.f.TabIndex = 500;
			this.f.Text = "保存(&S)";
			this.f.UseVisualStyleBackColor = true;
			this.f.Click += this.c;
			this.g.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.g.Controls.Add(this.ag);
			this.g.Controls.Add(this.ah);
			this.g.Controls.Add(this.ae);
			this.g.Controls.Add(this.af);
			this.g.Controls.Add(this.ac);
			this.g.Controls.Add(this.ad);
			this.g.Controls.Add(this.aa);
			this.g.Controls.Add(this.ab);
			this.g.Controls.Add(this.z);
			this.g.Controls.Add(this.y);
			this.g.Controls.Add(this.x);
			this.g.Controls.Add(this.s);
			this.g.Controls.Add(this.t);
			this.g.Controls.Add(this.u);
			this.g.Controls.Add(this.v);
			this.g.Controls.Add(this.w);
			this.g.Controls.Add(this.q);
			this.g.Controls.Add(this.r);
			this.g.Controls.Add(this.h);
			this.g.Controls.Add(this.i);
			this.g.Controls.Add(this.j);
			this.g.Controls.Add(this.k);
			this.g.Controls.Add(this.l);
			this.g.Controls.Add(this.m);
			this.g.Controls.Add(this.n);
			this.g.Controls.Add(this.o);
			this.g.Location = new Point(12, 12);
			this.g.Name = "groupBox1";
			this.g.Size = new Size(606, 377);
			this.g.TabIndex = 10;
			this.g.TabStop = false;
			this.g.Text = "正在编辑智能答复语";
			this.ae.AutoSize = true;
			this.ae.Location = new Point(210, 346);
			this.ae.Name = "label10";
			this.ae.Size = new Size(149, 12);
			this.ae.TabIndex = 619;
			this.ae.Text = "（数据越小，优先级越高）";
			this.af.AutoSize = true;
			this.af.Location = new Point(210, 272);
			this.af.Name = "label8";
			this.af.Size = new Size(257, 12);
			this.af.TabIndex = 618;
			this.af.Text = "（每天这个时间段才会答复，其余时间不做处理";
			this.ac.AutoSize = true;
			this.ac.Location = new Point(130, 277);
			this.ac.Name = "label7";
			this.ac.Size = new Size(11, 12);
			this.ac.TabIndex = 617;
			this.ac.Text = "-";
			this.ad.CustomFormat = "HH:mm";
			this.ad.Format = DateTimePickerFormat.Custom;
			this.ad.Location = new Point(147, 273);
			this.ad.Name = "DateArEnd";
			this.ad.ShowUpDown = true;
			this.ad.Size = new Size(53, 21);
			this.ad.TabIndex = 616;
			this.ad.Value = new DateTime(2023, 6, 26, 23, 59, 0, 0);
			this.aa.CustomFormat = "HH:mm";
			this.aa.Format = DateTimePickerFormat.Custom;
			this.aa.Location = new Point(71, 273);
			this.aa.Name = "DateArStart";
			this.aa.ShowUpDown = true;
			this.aa.Size = new Size(53, 21);
			this.aa.TabIndex = 615;
			this.aa.Value = new DateTime(2023, 6, 26, 0, 0, 0, 0);
			this.ab.Location = new Point(71, 341);
			this.ab.Name = "TxtGrade";
			this.ab.Size = new Size(121, 21);
			this.ab.TabIndex = 614;
			this.z.AutoSize = true;
			this.z.Location = new Point(24, 346);
			this.z.Name = "label5";
			this.z.Size = new Size(41, 12);
			this.z.TabIndex = 611;
			this.z.Text = "优先级";
			this.y.AutoSize = true;
			this.y.Location = new Point(12, 279);
			this.y.Name = "label4";
			this.y.Size = new Size(53, 12);
			this.y.TabIndex = 610;
			this.y.Text = "答复时间";
			this.x.AutoSize = true;
			this.x.Location = new Point(263, 245);
			this.x.Name = "linkLabel6";
			this.x.Size = new Size(107, 12);
			this.x.TabIndex = 608;
			this.x.TabStop = true;
			this.x.Text = "{@转接(指定客服)}";
			this.x.LinkClicked += this.a;
			this.s.AutoSize = true;
			this.s.Location = new Point(311, 224);
			this.s.Name = "linkLabel5";
			this.s.Size = new Size(83, 12);
			this.s.TabIndex = 607;
			this.s.TabStop = true;
			this.s.Text = "{$旺旺分段符}";
			this.s.LinkClicked += this.b;
			this.t.AutoSize = true;
			this.t.Location = new Point(157, 245);
			this.t.Name = "linkLabel4";
			this.t.Size = new Size(47, 12);
			this.t.TabIndex = 606;
			this.t.TabStop = true;
			this.t.Text = "{@提取}";
			this.t.LinkClicked += this.b;
			this.u.AutoSize = true;
			this.u.Location = new Point(210, 245);
			this.u.Name = "linkLabel3";
			this.u.Size = new Size(47, 12);
			this.u.TabIndex = 605;
			this.u.TabStop = true;
			this.u.Text = "{@转接}";
			this.u.LinkClicked += this.b;
			this.v.AutoSize = true;
			this.v.Location = new Point(157, 224);
			this.v.Name = "linkLabel2";
			this.v.Size = new Size(71, 12);
			this.v.TabIndex = 604;
			this.v.TabStop = true;
			this.v.Text = "{$提取链接}";
			this.v.LinkClicked += this.b;
			this.w.AutoSize = true;
			this.w.Location = new Point(234, 224);
			this.w.Name = "linkLabel1";
			this.w.Size = new Size(71, 12);
			this.w.TabIndex = 603;
			this.w.TabStop = true;
			this.w.Text = "{$人工客服}";
			this.w.LinkClicked += this.b;
			this.q.AutoSize = true;
			this.q.Location = new Point(69, 245);
			this.q.Name = "label2";
			this.q.Size = new Size(89, 12);
			this.q.TabIndex = 602;
			this.q.Text = "点击插入动作：";
			this.r.AutoSize = true;
			this.r.Location = new Point(69, 224);
			this.r.Name = "label1";
			this.r.Size = new Size(89, 12);
			this.r.TabIndex = 601;
			this.r.Text = "点击插入变量：";
			this.h.AutoSize = true;
			this.h.Location = new Point(568, 34);
			this.h.Name = "LinkHelp";
			this.h.Size = new Size(29, 12);
			this.h.TabIndex = 600;
			this.h.TabStop = true;
			this.h.Tag = "智能答复说明";
			this.h.Text = "帮助";
			this.h.LinkClicked += this.c;
			this.i.DropDownStyle = ComboBoxStyle.DropDownList;
			this.i.FormattingEnabled = true;
			this.i.Location = new Point(71, 31);
			this.i.Name = "CbArType";
			this.i.Size = new Size(82, 20);
			this.i.TabIndex = 100;
			this.i.SelectedIndexChanged += this.a;
			this.j.DisplayMember = "UserNick";
			this.j.DropDownStyle = ComboBoxStyle.DropDownList;
			this.j.FormattingEnabled = true;
			this.j.Location = new Point(71, 58);
			this.j.Name = "CbArSellerNick";
			this.j.Size = new Size(187, 20);
			this.j.TabIndex = 300;
			this.j.ValueMember = "UserNick";
			this.k.AutoSize = true;
			this.k.Location = new Point(24, 84);
			this.k.Name = "label17";
			this.k.Size = new Size(41, 12);
			this.k.TabIndex = 15;
			this.k.Text = "回复语";
			this.l.Location = new Point(71, 84);
			this.l.Multiline = true;
			this.l.Name = "TxtArReplyWord";
			this.l.ScrollBars = ScrollBars.Vertical;
			this.l.Size = new Size(508, 137);
			this.l.TabIndex = 400;
			this.m.AutoSize = true;
			this.m.Location = new Point(24, 61);
			this.m.Name = "label16";
			this.m.Size = new Size(41, 12);
			this.m.TabIndex = 13;
			this.m.Text = "关联号";
			this.n.Location = new Point(159, 31);
			this.n.Name = "TxtArKeyword";
			this.n.Size = new Size(403, 21);
			this.n.TabIndex = 200;
			this.o.AutoSize = true;
			this.o.Location = new Point(24, 34);
			this.o.Name = "label9";
			this.o.Size = new Size(41, 12);
			this.o.TabIndex = 11;
			this.o.Text = "关键词";
			this.ah.AutoSize = true;
			this.ah.Location = new Point(221, 294);
			this.ah.Name = "label3";
			this.ah.Size = new Size(233, 12);
			this.ah.TabIndex = 620;
			this.ah.Text = "当结束时间小于起始时间时，代表可以跨天";
			this.ag.AutoSize = true;
			this.ag.Location = new Point(221, 317);
			this.ag.Name = "label6";
			this.ag.Size = new Size(293, 12);
			this.ag.TabIndex = 621;
			this.ag.Text = "即 起始时间-24：00，24：00-结束时间 为答复时间）";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(630, 443);
			base.Controls.Add(this.g);
			base.Controls.Add(this.d);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.HelpButton = true;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormAutoReplyEdit";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "智能答复语设置";
			base.HelpButtonClicked += this.a;
			base.Load += this.e;
			base.HelpRequested += this.a;
			this.d.ResumeLayout(false);
			this.g.ResumeLayout(false);
			this.g.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040000C6 RID: 198
		private AutoReplyInfo a;

		// Token: 0x040000C7 RID: 199
		private bool b;

		// Token: 0x040000C9 RID: 201
		private Panel d;

		// Token: 0x040000CA RID: 202
		private Button e;

		// Token: 0x040000CB RID: 203
		private Button f;

		// Token: 0x040000CC RID: 204
		private GroupBox g;

		// Token: 0x040000CD RID: 205
		private LinkLabel h;

		// Token: 0x040000CE RID: 206
		private ComboBox i;

		// Token: 0x040000CF RID: 207
		private ComboBox j;

		// Token: 0x040000D0 RID: 208
		private Label k;

		// Token: 0x040000D1 RID: 209
		private TextBox l;

		// Token: 0x040000D2 RID: 210
		private Label m;

		// Token: 0x040000D3 RID: 211
		private TextBox n;

		// Token: 0x040000D4 RID: 212
		private Label o;

		// Token: 0x040000D5 RID: 213
		private Button p;

		// Token: 0x040000D6 RID: 214
		private Label q;

		// Token: 0x040000D7 RID: 215
		private Label r;

		// Token: 0x040000D8 RID: 216
		private LinkLabel s;

		// Token: 0x040000D9 RID: 217
		private LinkLabel t;

		// Token: 0x040000DA RID: 218
		private LinkLabel u;

		// Token: 0x040000DB RID: 219
		private LinkLabel v;

		// Token: 0x040000DC RID: 220
		private LinkLabel w;

		// Token: 0x040000DD RID: 221
		private LinkLabel x;

		// Token: 0x040000DE RID: 222
		private Label y;

		// Token: 0x040000DF RID: 223
		private Label z;

		// Token: 0x040000E0 RID: 224
		private DateTimePicker aa;

		// Token: 0x040000E1 RID: 225
		private TextBox ab;

		// Token: 0x040000E2 RID: 226
		private Label ac;

		// Token: 0x040000E3 RID: 227
		private DateTimePicker ad;

		// Token: 0x040000E4 RID: 228
		private Label ae;

		// Token: 0x040000E5 RID: 229
		private Label af;

		// Token: 0x040000E6 RID: 230
		private Label ag;

		// Token: 0x040000E7 RID: 231
		private Label ah;
	}
}
