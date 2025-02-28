using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Agiso;
using Agiso.AliwwApi;
using Agiso.DBAccess;
using Agiso.DbManager;
using Agiso.Object;
using Agiso.Utils;
using AliwwClient.Manager;
using AliwwClient.Object;
using AliwwClient.Properties;
using AliwwClient.Server;
using AliwwClient.WebSocketServer;
using AliwwClient.WebSocketServer.Extensions;
using WebSocketSharp.Server;

namespace AliwwClient
{
	// Token: 0x0200002C RID: 44
	public partial class FormErrorLog : BaseForm
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00019D54 File Offset: 0x00017F54
		public FormErrorLog()
		{
			this.a();
			if (AppConfig.AllowAutoLogin)
			{
				base.StartPosition = FormStartPosition.Manual;
				base.Location = new Point(Screen.PrimaryScreen.Bounds.Width - base.Width, Screen.PrimaryScreen.Bounds.Height - base.Height - 40);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00019DC4 File Offset: 0x00017FC4
		public static FormErrorLog FormErrorLogInstance
		{
			get
			{
				if (FormErrorLog.a == null || FormErrorLog.a.IsDisposed)
				{
					FormErrorLog.a = new FormErrorLog();
				}
				return FormErrorLog.a;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000289D File Offset: 0x00000A9D
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000028A4 File Offset: 0x00000AA4
		public static HttpServerBase LocalHttpServer { get; set; }

		// Token: 0x06000114 RID: 276 RVA: 0x00019DF8 File Offset: 0x00017FF8
		private void n(object sender, EventArgs e)
		{
			this.Text = this.Text + "【" + HardwareInfo.Uuid.Substring(16) + "】";
			this.aa.Checked = AppConfig.CurrentSystemSettingInfo.AutoReplyBySellerNick;
			this.aa.CheckedChanged += this.j;
			this.x.Checked = AppConfig.CurrentSystemSettingInfo.AllowKillAliApp;
			Control control = this.aq;
			HttpServerBase httpServerBase = FormErrorLog.b;
			control.Text = ((httpServerBase != null) ? httpServerBase.HttpPort : 32100).ToString();
			this.b();
			this.m(null, null);
			Dictionary<long, string> enumValAndDescList = EnumTool.GetEnumValAndDescList<LogType>();
			foreach (KeyValuePair<long, string> keyValuePair in enumValAndDescList)
			{
				this.z.Items.Add(keyValuePair, (keyValuePair.Key & AppConfig.EnabledLogOption) > 0L);
			}
			this.av.Text = AppConfig.CurrentSystemSettingInfo.InsertMsgSuccInterval.ToString();
			this.az.Value = DateTime.Now.AddDays(-31.0);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00019F54 File Offset: 0x00018154
		private void m(object sender, EventArgs e)
		{
			int num;
			DataTable dataTable = ErrorLogManager.Get(this.j.Value, out num, 0, 0);
			this.a(dataTable);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00019F80 File Offset: 0x00018180
		private void a(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (!e.IsSelected)
			{
				this.l.Text = "";
			}
			else
			{
				this.l.Text = e.Item.SubItems[1].Text;
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000028AC File Offset: 0x00000AAC
		private void b()
		{
			this.g.Columns.Add("时间", 70, HorizontalAlignment.Center);
			this.g.Columns.Add("日志内容", 700, HorizontalAlignment.Left);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00019FCC File Offset: 0x000181CC
		private void a(DataTable A_0)
		{
			this.g.Items.Clear();
			if (A_0 != null && A_0.Rows.Count != 0)
			{
				for (int i = 0; i < A_0.Rows.Count; i++)
				{
					DataRow dataRow = A_0.Rows[i];
					ListViewItem listViewItem = new ListViewItem(new string[]
					{
						DbUtil.TrimDateNull(dataRow["CreateTime"]).ToString("HH:mm:ss"),
						dataRow["LogContent"].ToString()
					});
					this.g.Items.Add(listViewItem);
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0001A080 File Offset: 0x00018280
		private void l(object sender, EventArgs e)
		{
			float dpiX = base.CreateGraphics().DpiX;
			this.v.Text = "Dpi: " + dpiX.ToString("0.##");
			string text = this.p.Text;
			int num = Util.ToInt(this.u.Text);
			int num2 = Util.ToInt(this.t.Text);
			if (string.IsNullOrEmpty(text))
			{
				MessageBox.Show("Nick is required!");
			}
			else
			{
				Aliww aliww = new Aliww(this.p.Text);
				AliwwTalkWindow customerBenchWindow = aliww.GetCustomerBenchWindow(false);
				customerBenchWindow.Click(num, num2, true);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0001A128 File Offset: 0x00018328
		private void k(object sender, EventArgs e)
		{
			long num = 0L;
			foreach (object obj in this.z.CheckedItems)
			{
				num += ((KeyValuePair<long, string>)obj).Key;
			}
			AppConfig.EnabledLogOption = num;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0001A1A0 File Offset: 0x000183A0
		private void j(object sender, EventArgs e)
		{
			bool autoReplyBySellerNick = AppConfig.CurrentSystemSettingInfo.AutoReplyBySellerNick;
			try
			{
				DialogResult dialogResult = MessageBox.Show("\"智能答复时按主号答复\"改变之后，会导致原先的智能答复不能使用，需重新设置智能答复，是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
				if (dialogResult == DialogResult.OK)
				{
					AppConfig.CurrentSystemSettingInfo.AutoReplyBySellerNick = this.aa.Checked;
					AppConfig.SaveConfig();
				}
				else
				{
					this.aa.CheckedChanged -= this.j;
					this.aa.Checked = !this.aa.Checked;
					this.aa.CheckedChanged += this.j;
				}
			}
			catch (Exception ex)
			{
				AppConfig.CurrentSystemSettingInfo.AutoReplyBySellerNick = autoReplyBySellerNick;
				MessageBox.Show("保存失败，失败原因：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0001A278 File Offset: 0x00018478
		private void i(object sender, EventArgs e)
		{
			this.ae.Text = ((AppConfig.AliwwWebScoketServer == null) ? "未开启" : (AppConfig.AliwwWebScoketServer.IsListening ? "已启动" : "未开启"));
			Control control = this.ao;
			string text;
			if (AppConfig.AliwwWebScoketServer != null)
			{
				text = string.Join(",", AppConfig.AliwwWebScoketServer.PostList.Select(new Func<int, string>(FormErrorLog.<>c.<>9.a)).ToArray<string>());
			}
			else
			{
				text = "0";
			}
			control.Text = text;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0001A314 File Offset: 0x00018514
		private void h(object sender, EventArgs e)
		{
			this.ac.Items.Clear();
			if (AppConfig.AliwwSocketSecret != null)
			{
				List<IWebSocketSession> list = new List<IWebSocketSession>();
				List<IWebSocketSession> list2 = new List<IWebSocketSession>();
				List<IWebSocketSession> list3 = new List<IWebSocketSession>();
				foreach (AgisoWebSocketServer agisoWebSocketServer in AppConfig.AliwwWebScoketServer.ServerList)
				{
					if (agisoWebSocketServer.AliwwSessionManager.Count > 0)
					{
						list = list.Concat(agisoWebSocketServer.AliwwSessionManager.Sessions).ToList<IWebSocketSession>();
					}
					if (agisoWebSocketServer.AldsSessionManager.Count > 0)
					{
						list2 = list2.Concat(agisoWebSocketServer.AldsSessionManager.Sessions).ToList<IWebSocketSession>();
					}
					if (AppConfig.AllowAutoLogin && agisoWebSocketServer.LoginSessionManager.Count > 0)
					{
						list3 = agisoWebSocketServer.LoginSessionManager.Sessions.ToList<IWebSocketSession>();
					}
				}
				this.ac.Items.Add(new ListItem("--（支持发货、答复、转接）--", "-1"));
				foreach (IWebSocketSession webSocketSession in list)
				{
					BehaviorBase behaviorBase = (BehaviorBase)webSocketSession;
					this.ac.Items.Add(new ListItem(behaviorBase.UserNick, behaviorBase.ID.ToString()));
				}
				this.ac.Items.Add(new ListItem("--（支持发货、千牛7.07及以上转接）--", "-2"));
				foreach (IWebSocketSession webSocketSession2 in list2)
				{
					BehaviorBase behaviorBase2 = (BehaviorBase)webSocketSession2;
					this.ac.Items.Add(new ListItem(behaviorBase2.UserNick, behaviorBase2.ID.ToString()));
				}
				if (list3.Count > 0)
				{
					this.ac.Items.Add(new ListItem("--（代理连接）--", "-3"));
					foreach (IWebSocketSession webSocketSession3 in list3)
					{
						LoginBehavior loginBehavior = (LoginBehavior)webSocketSession3;
						this.ac.Items.Add(new ListItem(loginBehavior.CurrUrl, loginBehavior.ID.ToString()));
					}
				}
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0001A5C4 File Offset: 0x000187C4
		private void g(object sender, EventArgs e)
		{
			string text = this.ak.Text.Trim();
			string text2 = this.ai.Text.Trim();
			string text3 = this.a7.Text.Trim();
			if (!string.IsNullOrEmpty(text) && (!string.IsNullOrEmpty(text2) || !string.IsNullOrEmpty(text3)))
			{
				if (this.a4.Checked)
				{
					if (this.ba.Checked)
					{
						RecentBehavior recentSession = AppConfig.GetUserCacheOrCreate(text).RecentSession;
						if (recentSession != null)
						{
							recentSession.OpenChat(text2, text3, "cntaobao");
						}
					}
					else
					{
						RecentBehavior recentSession2 = AppConfig.GetUserCacheOrCreate(text).RecentSession;
						if (recentSession2 != null)
						{
							recentSession2.smethod_0(text2, text3, " ", 0, "cntaobao");
						}
					}
				}
				else if (this.a5.Checked)
				{
					if (this.ba.Checked)
					{
						AldsBehavior aldsSession = AppConfig.GetUserCacheOrCreate(text).AldsSession;
						if (aldsSession != null)
						{
							aldsSession.OpenChat(text2, text3, "cntaobao");
						}
					}
					else
					{
						AldsBehavior aldsSession2 = AppConfig.GetUserCacheOrCreate(text).AldsSession;
						if (aldsSession2 != null)
						{
							aldsSession2.smethod_0(text2, text3, " ", 0, "cntaobao");
						}
					}
				}
				else
				{
					AppConfig.ProcessStartQn(text, text2, text3, "cntaobao");
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0001A708 File Offset: 0x00018908
		private void f(object sender, EventArgs e)
		{
			string text = this.ak.Text.Trim();
			string text2 = this.ai.Text.Trim();
			string text3 = this.a7.Text.Trim();
			string text4 = this.a2.Text.Trim();
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text4) && (!string.IsNullOrEmpty(text2) || !string.IsNullOrEmpty(text3)))
			{
				if (this.a4.Checked)
				{
					RecentBehavior recentSession = AppConfig.GetUserCacheOrCreate(text).RecentSession;
					if (recentSession != null)
					{
						recentSession.TransferContact(text2, "cntaobao", text3, text4, "cntaobao");
					}
				}
				else
				{
					AldsBehavior aldsSession = AppConfig.GetUserCacheOrCreate(text).AldsSession;
					if (aldsSession != null)
					{
						aldsSession.TransferContact(text2, "cntaobao", text3, text4, "cntaobao");
					}
				}
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000028E5 File Offset: 0x00000AE5
		private void a(object sender, LinkLabelLinkClickedEventArgs e)
		{
			RepairQnAliresManager.RepairAef();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0001A7DC File Offset: 0x000189DC
		private void e(object sender, EventArgs e)
		{
			HttpServerBase httpServerBase = FormErrorLog.b;
			if (((httpServerBase != null) ? new bool?(httpServerBase.IsRuning) : null).GetValueOrDefault())
			{
				FormErrorLog.b.Stop();
			}
			int num = Util.ToInt(this.aq.Text);
			FormErrorLog.LocalHttpServer = new HttpServerBase(num);
			FormErrorLog.b.Start();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0001A844 File Offset: 0x00018A44
		private void d(object sender, EventArgs e)
		{
			HttpServerBase httpServerBase = FormErrorLog.b;
			if (((httpServerBase != null) ? new bool?(httpServerBase.IsRuning) : null).GetValueOrDefault())
			{
				FormErrorLog.b.Stop();
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0001A884 File Offset: 0x00018A84
		private void c(object sender, EventArgs e)
		{
			string text = Util.Trim(this.ao.Text);
			DialogResult dialogResult = MessageBox.Show("是否设端口号为：" + text, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.OK)
			{
				try
				{
					try
					{
						AppConfig.AliwwWebScoketServer.Stop();
					}
					catch
					{
					}
					List<int> list = text.Split(new char[] { ',', '，' }).Where(new Func<string, bool>(FormErrorLog.<>c.<>9.b)).Select(new Func<string, int>(FormErrorLog.<>c.<>9.a))
						.ToList<int>();
					if (list.Count <= 0)
					{
						MessageBox.Show("输入端口有误，多个端口逗号分隔", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
					}
					if (list.Count > 5)
					{
						MessageBox.Show("封顶5个端口", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
					}
					AppConfig.AliwwWebScoketServer = new WebSocketServerIns(list);
					AppConfig.AliwwWebScoketServer.Start();
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(ex.ToString(), 1);
				}
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0001A9D8 File Offset: 0x00018BD8
		private void b(object sender, EventArgs e)
		{
			if (!(this.av.Text == ""))
			{
				int num = Util.ToInt(this.av.Text);
				if (num > 5000 || num < 500)
				{
					MessageBox.Show("间隔只能在500-5000之间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					AppConfig.CurrentSystemSettingInfo.AllowKillAliApp = this.x.Checked;
					AppConfig.CurrentSystemSettingInfo.InsertMsgSuccInterval = num;
					AppConfig.SaveConfig();
					MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0001AA78 File Offset: 0x00018C78
		private void a(object sender, EventArgs e)
		{
			FormErrorLog.a a = new FormErrorLog.a();
			a.a = this.az.Value.Date;
			Task.Run(new Action(a.b));
			MessageBox.Show("已开始删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0001AAFC File Offset: 0x00018CFC
		private void a()
		{
			this.d = new TabControl();
			this.e = new TabPage();
			this.f = new SplitContainer();
			this.g = new ListView();
			this.h = new global::System.Windows.Forms.Panel();
			this.i = new global::System.Windows.Forms.Label();
			this.j = new DateTimePicker();
			this.k = new global::System.Windows.Forms.Button();
			this.l = new global::System.Windows.Forms.TextBox();
			this.m = new TabPage();
			this.ay = new GroupBox();
			this.az = new DateTimePicker();
			this.a0 = new global::System.Windows.Forms.Button();
			this.ar = new GroupBox();
			this.@as = new global::System.Windows.Forms.Button();
			this.aq = new global::System.Windows.Forms.TextBox();
			this.ap = new global::System.Windows.Forms.Button();
			this.an = new GroupBox();
			this.am = new LinkLabel();
			this.ag = new GroupBox();
			this.a7 = new global::System.Windows.Forms.TextBox();
			this.a8 = new global::System.Windows.Forms.Label();
			this.a6 = new global::System.Windows.Forms.Panel();
			this.a9 = new global::System.Windows.Forms.RadioButton();
			this.a4 = new global::System.Windows.Forms.RadioButton();
			this.a5 = new global::System.Windows.Forms.RadioButton();
			this.a2 = new global::System.Windows.Forms.TextBox();
			this.a3 = new global::System.Windows.Forms.Label();
			this.a1 = new global::System.Windows.Forms.Button();
			this.ah = new global::System.Windows.Forms.Button();
			this.ai = new global::System.Windows.Forms.TextBox();
			this.aj = new global::System.Windows.Forms.Label();
			this.ak = new global::System.Windows.Forms.TextBox();
			this.al = new global::System.Windows.Forms.Label();
			this.ab = new GroupBox();
			this.at = new global::System.Windows.Forms.Button();
			this.ao = new global::System.Windows.Forms.TextBox();
			this.ac = new ComboBox();
			this.ad = new global::System.Windows.Forms.Button();
			this.ae = new global::System.Windows.Forms.TextBox();
			this.af = new global::System.Windows.Forms.Button();
			this.y = new GroupBox();
			this.z = new CheckedListBox();
			this.w = new GroupBox();
			this.aa = new global::System.Windows.Forms.CheckBox();
			this.aw = new global::System.Windows.Forms.Label();
			this.ax = new LinkLabel();
			this.x = new global::System.Windows.Forms.CheckBox();
			this.av = new global::System.Windows.Forms.TextBox();
			this.au = new global::System.Windows.Forms.Button();
			this.n = new GroupBox();
			this.v = new global::System.Windows.Forms.Label();
			this.o = new global::System.Windows.Forms.Label();
			this.p = new global::System.Windows.Forms.TextBox();
			this.q = new global::System.Windows.Forms.Label();
			this.r = new global::System.Windows.Forms.Label();
			this.s = new global::System.Windows.Forms.Button();
			this.t = new global::System.Windows.Forms.TextBox();
			this.u = new global::System.Windows.Forms.TextBox();
			this.ba = new global::System.Windows.Forms.CheckBox();
			this.d.SuspendLayout();
			this.e.SuspendLayout();
			((ISupportInitialize)this.f).BeginInit();
			this.f.Panel1.SuspendLayout();
			this.f.Panel2.SuspendLayout();
			this.f.SuspendLayout();
			this.h.SuspendLayout();
			this.m.SuspendLayout();
			this.ay.SuspendLayout();
			this.ar.SuspendLayout();
			this.an.SuspendLayout();
			this.ag.SuspendLayout();
			this.a6.SuspendLayout();
			this.ab.SuspendLayout();
			this.y.SuspendLayout();
			this.w.SuspendLayout();
			this.n.SuspendLayout();
			base.SuspendLayout();
			this.d.Controls.Add(this.e);
			this.d.Controls.Add(this.m);
			this.d.Dock = DockStyle.Fill;
			this.d.Location = new Point(0, 0);
			this.d.Margin = new Padding(4, 2, 4, 2);
			this.d.Name = "tabControl1";
			this.d.SelectedIndex = 0;
			this.d.Size = new Size(701, 448);
			this.d.TabIndex = 0;
			this.e.Controls.Add(this.f);
			this.e.Location = new Point(4, 22);
			this.e.Margin = new Padding(4, 2, 4, 2);
			this.e.Name = "tabPage1";
			this.e.Padding = new Padding(4, 2, 4, 2);
			this.e.Size = new Size(693, 422);
			this.e.TabIndex = 0;
			this.e.Text = "ErrorLog";
			this.e.UseVisualStyleBackColor = true;
			this.f.Dock = DockStyle.Fill;
			this.f.Location = new Point(4, 2);
			this.f.Margin = new Padding(4, 2, 4, 2);
			this.f.Name = "splitContainer1";
			this.f.Panel1.Controls.Add(this.g);
			this.f.Panel1.Controls.Add(this.h);
			this.f.Panel2.Controls.Add(this.l);
			this.f.Panel2.RightToLeft = RightToLeft.No;
			this.f.Size = new Size(685, 418);
			this.f.SplitterDistance = 242;
			this.f.TabIndex = 1;
			this.g.Dock = DockStyle.Fill;
			this.g.FullRowSelect = true;
			this.g.HideSelection = false;
			this.g.Location = new Point(0, 22);
			this.g.Margin = new Padding(4, 2, 4, 2);
			this.g.MultiSelect = false;
			this.g.Name = "LvErrorLog";
			this.g.Size = new Size(242, 396);
			this.g.TabIndex = 8;
			this.g.UseCompatibleStateImageBehavior = false;
			this.g.View = global::System.Windows.Forms.View.Details;
			this.g.ItemSelectionChanged += this.a;
			this.h.Controls.Add(this.i);
			this.h.Controls.Add(this.j);
			this.h.Controls.Add(this.k);
			this.h.Dock = DockStyle.Top;
			this.h.Location = new Point(0, 0);
			this.h.Margin = new Padding(4, 2, 4, 2);
			this.h.Name = "panel1";
			this.h.Size = new Size(242, 22);
			this.h.TabIndex = 3;
			this.i.AutoSize = true;
			this.i.Location = new Point(4, 4);
			this.i.Margin = new Padding(4, 0, 4, 0);
			this.i.Name = "label1";
			this.i.Size = new Size(35, 12);
			this.i.TabIndex = 5;
			this.i.Text = "Date:";
			this.j.CustomFormat = "yyyy-MM-dd";
			this.j.Format = DateTimePickerFormat.Custom;
			this.j.Location = new Point(46, 0);
			this.j.Margin = new Padding(4, 2, 4, 2);
			this.j.Name = "DtpErrorLog";
			this.j.Size = new Size(100, 21);
			this.j.TabIndex = 4;
			this.k.Location = new Point(153, 0);
			this.k.Margin = new Padding(4, 2, 4, 2);
			this.k.Name = "BtnErrorLogSearch";
			this.k.Size = new Size(86, 22);
			this.k.TabIndex = 2;
			this.k.Text = "Search(&S)";
			this.k.UseVisualStyleBackColor = true;
			this.k.Click += this.m;
			this.l.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.l.Location = new Point(0, 0);
			this.l.Margin = new Padding(4, 2, 4, 2);
			this.l.Multiline = true;
			this.l.Name = "TxtErrorLog";
			this.l.ReadOnly = true;
			this.l.RightToLeft = RightToLeft.No;
			this.l.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.l.Size = new Size(441, 420);
			this.l.TabIndex = 0;
			this.m.Controls.Add(this.ay);
			this.m.Controls.Add(this.ar);
			this.m.Controls.Add(this.an);
			this.m.Controls.Add(this.ag);
			this.m.Controls.Add(this.ab);
			this.m.Controls.Add(this.y);
			this.m.Controls.Add(this.w);
			this.m.Controls.Add(this.n);
			this.m.Location = new Point(4, 22);
			this.m.Margin = new Padding(4, 2, 4, 2);
			this.m.Name = "tabPage2";
			this.m.Padding = new Padding(4, 2, 4, 2);
			this.m.Size = new Size(693, 422);
			this.m.TabIndex = 1;
			this.m.Text = "Tools";
			this.m.UseVisualStyleBackColor = true;
			this.ay.Controls.Add(this.az);
			this.ay.Controls.Add(this.a0);
			this.ay.Location = new Point(8, 362);
			this.ay.Margin = new Padding(4, 2, 4, 2);
			this.ay.Name = "groupBox8";
			this.ay.Padding = new Padding(4, 2, 4, 2);
			this.ay.Size = new Size(248, 54);
			this.ay.TabIndex = 238;
			this.ay.TabStop = false;
			this.ay.Text = "清理记录";
			this.az.CustomFormat = "yyyy-MM-dd";
			this.az.Format = DateTimePickerFormat.Custom;
			this.az.Location = new Point(6, 20);
			this.az.Margin = new Padding(4, 2, 4, 2);
			this.az.Name = "dtpLogTime";
			this.az.Size = new Size(80, 21);
			this.az.TabIndex = 238;
			this.a0.Location = new Point(94, 20);
			this.a0.Margin = new Padding(4, 2, 4, 2);
			this.a0.Name = "btnClearLog";
			this.a0.Size = new Size(116, 22);
			this.a0.TabIndex = 237;
			this.a0.Text = "清理之前记录";
			this.a0.UseVisualStyleBackColor = true;
			this.a0.Click += this.a;
			this.ar.Controls.Add(this.@as);
			this.ar.Controls.Add(this.aq);
			this.ar.Controls.Add(this.ap);
			this.ar.Location = new Point(590, 8);
			this.ar.Margin = new Padding(4, 2, 4, 2);
			this.ar.Name = "groupBox7";
			this.ar.Padding = new Padding(4, 2, 4, 2);
			this.ar.Size = new Size(94, 100);
			this.ar.TabIndex = 237;
			this.ar.TabStop = false;
			this.ar.Text = "测试http";
			this.@as.Location = new Point(56, 50);
			this.@as.Margin = new Padding(4, 2, 4, 2);
			this.@as.Name = "button2";
			this.@as.Size = new Size(32, 22);
			this.@as.TabIndex = 237;
			this.@as.Text = "停";
			this.@as.UseVisualStyleBackColor = true;
			this.@as.Click += this.d;
			this.aq.Location = new Point(6, 18);
			this.aq.Margin = new Padding(4, 2, 4, 2);
			this.aq.Name = "txtHttpPortTest";
			this.aq.Size = new Size(80, 21);
			this.aq.TabIndex = 236;
			this.ap.Location = new Point(6, 50);
			this.ap.Margin = new Padding(4, 2, 4, 2);
			this.ap.Name = "button1";
			this.ap.Size = new Size(28, 22);
			this.ap.TabIndex = 235;
			this.ap.Text = "启";
			this.ap.UseVisualStyleBackColor = true;
			this.ap.Click += this.e;
			this.an.Controls.Add(this.am);
			this.an.Location = new Point(590, 128);
			this.an.Margin = new Padding(4, 2, 4, 2);
			this.an.Name = "groupBox6";
			this.an.Padding = new Padding(4, 2, 4, 2);
			this.an.Size = new Size(94, 78);
			this.an.TabIndex = 234;
			this.an.TabStop = false;
			this.an.Text = "系统工具";
			this.am.AutoSize = true;
			this.am.Location = new Point(10, 30);
			this.am.Margin = new Padding(4, 0, 4, 0);
			this.am.Name = "linkLabelRepair";
			this.am.Size = new Size(53, 12);
			this.am.TabIndex = 233;
			this.am.TabStop = true;
			this.am.Text = "助手修复";
			this.am.LinkClicked += this.a;
			this.ag.Controls.Add(this.ba);
			this.ag.Controls.Add(this.a7);
			this.ag.Controls.Add(this.a8);
			this.ag.Controls.Add(this.a6);
			this.ag.Controls.Add(this.a2);
			this.ag.Controls.Add(this.a3);
			this.ag.Controls.Add(this.a1);
			this.ag.Controls.Add(this.ah);
			this.ag.Controls.Add(this.ai);
			this.ag.Controls.Add(this.aj);
			this.ag.Controls.Add(this.ak);
			this.ag.Controls.Add(this.al);
			this.ag.Location = new Point(278, 263);
			this.ag.Margin = new Padding(4, 2, 4, 2);
			this.ag.Name = "groupBox5";
			this.ag.Padding = new Padding(4, 2, 4, 2);
			this.ag.Size = new Size(398, 153);
			this.ag.TabIndex = 0;
			this.ag.TabStop = false;
			this.ag.Text = "aliim测试";
			this.a7.Location = new Point(98, 73);
			this.a7.Margin = new Padding(4, 2, 4, 2);
			this.a7.Name = "txtBuyerOpenId";
			this.a7.Size = new Size(162, 21);
			this.a7.TabIndex = 12;
			this.a8.AutoSize = true;
			this.a8.Location = new Point(38, 76);
			this.a8.Margin = new Padding(4, 0, 4, 0);
			this.a8.Name = "label9";
			this.a8.Size = new Size(53, 12);
			this.a8.TabIndex = 11;
			this.a8.Text = "BuyerId:";
			this.a6.Controls.Add(this.a9);
			this.a6.Controls.Add(this.a4);
			this.a6.Controls.Add(this.a5);
			this.a6.Location = new Point(88, 122);
			this.a6.Name = "panel2";
			this.a6.Size = new Size(184, 22);
			this.a6.TabIndex = 10;
			this.a9.AutoSize = true;
			this.a9.Location = new Point(127, 3);
			this.a9.Name = "cbOther";
			this.a9.Size = new Size(53, 16);
			this.a9.TabIndex = 10;
			this.a9.Text = "other";
			this.a9.UseVisualStyleBackColor = true;
			this.a4.AutoSize = true;
			this.a4.Checked = true;
			this.a4.Location = new Point(10, 3);
			this.a4.Name = "rbRecent";
			this.a4.Size = new Size(59, 16);
			this.a4.TabIndex = 8;
			this.a4.TabStop = true;
			this.a4.Text = "recent";
			this.a4.UseVisualStyleBackColor = true;
			this.a5.AutoSize = true;
			this.a5.Location = new Point(75, 3);
			this.a5.Name = "rbAlds";
			this.a5.Size = new Size(47, 16);
			this.a5.TabIndex = 9;
			this.a5.Text = "alds";
			this.a5.UseVisualStyleBackColor = true;
			this.a2.Location = new Point(98, 99);
			this.a2.Margin = new Padding(4, 2, 4, 2);
			this.a2.Name = "txtTranferNick";
			this.a2.Size = new Size(162, 21);
			this.a2.TabIndex = 7;
			this.a3.AutoSize = true;
			this.a3.Location = new Point(18, 103);
			this.a3.Margin = new Padding(4, 0, 4, 0);
			this.a3.Name = "label5";
			this.a3.Size = new Size(77, 12);
			this.a3.TabIndex = 6;
			this.a3.Text = "tranferNick:";
			this.a1.Location = new Point(270, 101);
			this.a1.Margin = new Padding(4, 2, 4, 2);
			this.a1.Name = "btnTranfer";
			this.a1.Size = new Size(76, 22);
			this.a1.TabIndex = 5;
			this.a1.Text = "转接";
			this.a1.UseVisualStyleBackColor = true;
			this.a1.Click += this.f;
			this.ah.Location = new Point(270, 75);
			this.ah.Margin = new Padding(4, 2, 4, 2);
			this.ah.Name = "btnAliimTest";
			this.ah.Size = new Size(76, 22);
			this.ah.TabIndex = 4;
			this.ah.Text = "呼叫聊天人";
			this.ah.UseVisualStyleBackColor = true;
			this.ah.Click += this.g;
			this.ai.Location = new Point(98, 46);
			this.ai.Margin = new Padding(4, 2, 4, 2);
			this.ai.Name = "txtBuyerNick";
			this.ai.Size = new Size(162, 21);
			this.ai.TabIndex = 3;
			this.aj.AutoSize = true;
			this.aj.Location = new Point(28, 49);
			this.aj.Margin = new Padding(4, 0, 4, 0);
			this.aj.Name = "label7";
			this.aj.Size = new Size(65, 12);
			this.aj.TabIndex = 2;
			this.aj.Text = "BuyerNick:";
			this.ak.Location = new Point(98, 20);
			this.ak.Margin = new Padding(4, 2, 4, 2);
			this.ak.Name = "txtUserNick";
			this.ak.Size = new Size(162, 21);
			this.ak.TabIndex = 1;
			this.al.AutoSize = true;
			this.al.Location = new Point(36, 23);
			this.al.Margin = new Padding(4, 0, 4, 0);
			this.al.Name = "label6";
			this.al.Size = new Size(59, 12);
			this.al.TabIndex = 0;
			this.al.Text = "UserNick:";
			this.ab.Controls.Add(this.at);
			this.ab.Controls.Add(this.ao);
			this.ab.Controls.Add(this.ac);
			this.ab.Controls.Add(this.ad);
			this.ab.Controls.Add(this.ae);
			this.ab.Controls.Add(this.af);
			this.ab.Location = new Point(278, 178);
			this.ab.Margin = new Padding(4, 2, 4, 2);
			this.ab.Name = "groupBox4";
			this.ab.Padding = new Padding(4, 2, 4, 2);
			this.ab.Size = new Size(292, 81);
			this.ab.TabIndex = 232;
			this.ab.TabStop = false;
			this.ab.Text = "连接";
			this.at.Location = new Point(264, 22);
			this.at.Margin = new Padding(4, 2, 4, 2);
			this.at.Name = "button3";
			this.at.Size = new Size(28, 22);
			this.at.TabIndex = 239;
			this.at.Text = "设";
			this.at.UseVisualStyleBackColor = true;
			this.at.Click += this.c;
			this.ao.Location = new Point(144, 22);
			this.ao.Margin = new Padding(4, 2, 4, 2);
			this.ao.Name = "txtWsPort";
			this.ao.Size = new Size(116, 21);
			this.ao.TabIndex = 238;
			this.ac.DropDownStyle = ComboBoxStyle.DropDownList;
			this.ac.FormattingEnabled = true;
			this.ac.Location = new Point(6, 50);
			this.ac.Margin = new Padding(4, 2, 4, 2);
			this.ac.Name = "cbSession";
			this.ac.Size = new Size(196, 20);
			this.ac.TabIndex = 235;
			this.ad.Location = new Point(212, 50);
			this.ad.Margin = new Padding(4, 2, 4, 2);
			this.ad.Name = "btnRefresh";
			this.ad.Size = new Size(76, 22);
			this.ad.TabIndex = 234;
			this.ad.Text = "刷新";
			this.ad.UseVisualStyleBackColor = true;
			this.ad.Click += this.h;
			this.ae.Location = new Point(88, 22);
			this.ae.Margin = new Padding(4, 2, 4, 2);
			this.ae.Name = "textBox1";
			this.ae.Size = new Size(56, 21);
			this.ae.TabIndex = 233;
			this.af.Location = new Point(6, 20);
			this.af.Margin = new Padding(4, 2, 4, 2);
			this.af.Name = "btnServerCheck";
			this.af.Size = new Size(76, 22);
			this.af.TabIndex = 232;
			this.af.Text = "服务端状态";
			this.af.UseVisualStyleBackColor = true;
			this.af.Click += this.i;
			this.y.Controls.Add(this.z);
			this.y.Location = new Point(8, 142);
			this.y.Margin = new Padding(4, 2, 4, 2);
			this.y.Name = "groupBox3";
			this.y.Padding = new Padding(4, 2, 4, 2);
			this.y.Size = new Size(248, 210);
			this.y.TabIndex = 227;
			this.y.TabStop = false;
			this.y.Text = "日志选项";
			this.z.CheckOnClick = true;
			this.z.Dock = DockStyle.Fill;
			this.z.FormattingEnabled = true;
			this.z.Location = new Point(4, 16);
			this.z.Margin = new Padding(4, 2, 4, 2);
			this.z.Name = "cblLogType";
			this.z.Size = new Size(240, 192);
			this.z.TabIndex = 0;
			this.z.SelectedValueChanged += this.k;
			this.w.Controls.Add(this.aa);
			this.w.Controls.Add(this.aw);
			this.w.Controls.Add(this.ax);
			this.w.Controls.Add(this.x);
			this.w.Controls.Add(this.av);
			this.w.Controls.Add(this.au);
			this.w.Location = new Point(278, 6);
			this.w.Margin = new Padding(4, 2, 4, 2);
			this.w.Name = "groupBox2";
			this.w.Padding = new Padding(4, 2, 4, 2);
			this.w.Size = new Size(292, 164);
			this.w.TabIndex = 226;
			this.w.TabStop = false;
			this.w.Text = "选项";
			this.aa.AutoSize = true;
			this.aa.Location = new Point(20, 141);
			this.aa.Margin = new Padding(4, 2, 4, 2);
			this.aa.Name = "cbAutoReplyBySellerNick";
			this.aa.Size = new Size(144, 16);
			this.aa.TabIndex = 234;
			this.aa.Text = "智能答复时按主号答复";
			this.aa.UseVisualStyleBackColor = true;
			this.aw.AutoSize = true;
			this.aw.Location = new Point(16, 64);
			this.aw.Margin = new Padding(4, 0, 4, 0);
			this.aw.Name = "label8";
			this.aw.Size = new Size(77, 12);
			this.aw.TabIndex = 0;
			this.aw.Text = "检测消息间隔";
			this.ax.AutoSize = true;
			this.ax.Location = new Point(144, 66);
			this.ax.Margin = new Padding(4, 0, 4, 0);
			this.ax.Name = "linkLabel1";
			this.ax.Size = new Size(53, 12);
			this.ax.TabIndex = 237;
			this.ax.TabStop = true;
			this.ax.Text = "500-5000";
			this.x.AutoSize = true;
			this.x.Location = new Point(20, 40);
			this.x.Margin = new Padding(4, 2, 4, 2);
			this.x.Name = "cbAllowKillAliApp";
			this.x.Size = new Size(252, 16);
			this.x.TabIndex = 226;
			this.x.Text = "自动杀掉AliApp进程（当进程超过120M时）";
			this.x.UseVisualStyleBackColor = true;
			this.av.Location = new Point(98, 62);
			this.av.Margin = new Padding(4, 2, 4, 2);
			this.av.Name = "txtInsertMsgSuccInterval";
			this.av.Size = new Size(42, 21);
			this.av.TabIndex = 230;
			this.av.Text = "1000";
			this.av.TextAlign = HorizontalAlignment.Center;
			this.au.Location = new Point(208, 104);
			this.au.Margin = new Padding(4, 2, 4, 2);
			this.au.Name = "button5";
			this.au.Size = new Size(52, 22);
			this.au.TabIndex = 236;
			this.au.Text = "保存";
			this.au.UseVisualStyleBackColor = true;
			this.au.Click += this.b;
			this.n.Controls.Add(this.v);
			this.n.Controls.Add(this.o);
			this.n.Controls.Add(this.p);
			this.n.Controls.Add(this.q);
			this.n.Controls.Add(this.r);
			this.n.Controls.Add(this.s);
			this.n.Controls.Add(this.t);
			this.n.Controls.Add(this.u);
			this.n.Location = new Point(8, 6);
			this.n.Margin = new Padding(4, 2, 4, 2);
			this.n.Name = "groupBox1";
			this.n.Padding = new Padding(4, 2, 4, 2);
			this.n.Size = new Size(248, 126);
			this.n.TabIndex = 0;
			this.n.TabStop = false;
			this.n.Text = "test click";
			this.v.AutoSize = true;
			this.v.Location = new Point(10, 76);
			this.v.Margin = new Padding(4, 0, 4, 0);
			this.v.Name = "LblDpi";
			this.v.Size = new Size(0, 12);
			this.v.TabIndex = 6;
			this.o.Location = new Point(10, 20);
			this.o.Margin = new Padding(4, 0, 4, 0);
			this.o.Name = "label4";
			this.o.Size = new Size(40, 12);
			this.o.TabIndex = 0;
			this.o.Text = "Nick:";
			this.p.Location = new Point(56, 18);
			this.p.Margin = new Padding(4, 2, 4, 2);
			this.p.Name = "textBoxForClickNick";
			this.p.Size = new Size(128, 21);
			this.p.TabIndex = 5;
			this.q.AutoSize = true;
			this.q.Location = new Point(112, 46);
			this.q.Margin = new Padding(4, 0, 4, 0);
			this.q.Name = "label3";
			this.q.Size = new Size(17, 12);
			this.q.TabIndex = 4;
			this.q.Text = "Y:";
			this.r.AutoSize = true;
			this.r.Location = new Point(32, 46);
			this.r.Margin = new Padding(4, 0, 4, 0);
			this.r.Name = "label2";
			this.r.Size = new Size(17, 12);
			this.r.TabIndex = 3;
			this.r.Text = "X:";
			this.s.Location = new Point(110, 70);
			this.s.Margin = new Padding(4, 2, 4, 2);
			this.s.Name = "btnTestClick";
			this.s.Size = new Size(76, 22);
			this.s.TabIndex = 2;
			this.s.Text = "Test(&T)";
			this.s.UseVisualStyleBackColor = true;
			this.s.Click += this.l;
			this.t.Location = new Point(134, 44);
			this.t.Margin = new Padding(4, 2, 4, 2);
			this.t.Name = "textBoxForClickY";
			this.t.Size = new Size(50, 21);
			this.t.TabIndex = 1;
			this.t.Text = "105";
			this.u.Location = new Point(56, 44);
			this.u.Margin = new Padding(4, 2, 4, 2);
			this.u.Name = "textBoxForClickX";
			this.u.Size = new Size(50, 21);
			this.u.TabIndex = 0;
			this.u.Text = "270";
			this.ba.AutoSize = true;
			this.ba.Location = new Point(279, 127);
			this.ba.Name = "cbOpenchat";
			this.ba.Size = new Size(72, 16);
			this.ba.TabIndex = 13;
			this.ba.Text = "OpenChat";
			this.ba.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(701, 448);
			base.Controls.Add(this.d);
			base.Icon = Resources.Icon1;
			base.Margin = new Padding(4, 2, 4, 2);
			base.Name = "FormErrorLog";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "ErrorLog";
			base.Load += this.n;
			this.d.ResumeLayout(false);
			this.e.ResumeLayout(false);
			this.f.Panel1.ResumeLayout(false);
			this.f.Panel2.ResumeLayout(false);
			this.f.Panel2.PerformLayout();
			((ISupportInitialize)this.f).EndInit();
			this.f.ResumeLayout(false);
			this.h.ResumeLayout(false);
			this.h.PerformLayout();
			this.m.ResumeLayout(false);
			this.ay.ResumeLayout(false);
			this.ar.ResumeLayout(false);
			this.ar.PerformLayout();
			this.an.ResumeLayout(false);
			this.an.PerformLayout();
			this.ag.ResumeLayout(false);
			this.ag.PerformLayout();
			this.a6.ResumeLayout(false);
			this.a6.PerformLayout();
			this.ab.ResumeLayout(false);
			this.ab.PerformLayout();
			this.y.ResumeLayout(false);
			this.w.ResumeLayout(false);
			this.w.PerformLayout();
			this.n.ResumeLayout(false);
			this.n.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040000FE RID: 254
		private static FormErrorLog a;

		// Token: 0x040000FF RID: 255
		[CompilerGenerated]
		private static HttpServerBase b;

		// Token: 0x04000101 RID: 257
		private TabControl d;

		// Token: 0x04000102 RID: 258
		private TabPage e;

		// Token: 0x04000103 RID: 259
		private SplitContainer f;

		// Token: 0x04000104 RID: 260
		private ListView g;

		// Token: 0x04000105 RID: 261
		private global::System.Windows.Forms.Panel h;

		// Token: 0x04000106 RID: 262
		private global::System.Windows.Forms.Label i;

		// Token: 0x04000107 RID: 263
		private DateTimePicker j;

		// Token: 0x04000108 RID: 264
		private global::System.Windows.Forms.Button k;

		// Token: 0x04000109 RID: 265
		private global::System.Windows.Forms.TextBox l;

		// Token: 0x0400010A RID: 266
		private TabPage m;

		// Token: 0x0400010B RID: 267
		private GroupBox n;

		// Token: 0x0400010C RID: 268
		private global::System.Windows.Forms.Label o;

		// Token: 0x0400010D RID: 269
		private global::System.Windows.Forms.TextBox p;

		// Token: 0x0400010E RID: 270
		private global::System.Windows.Forms.Label q;

		// Token: 0x0400010F RID: 271
		private global::System.Windows.Forms.Label r;

		// Token: 0x04000110 RID: 272
		private global::System.Windows.Forms.Button s;

		// Token: 0x04000111 RID: 273
		private global::System.Windows.Forms.TextBox t;

		// Token: 0x04000112 RID: 274
		private global::System.Windows.Forms.TextBox u;

		// Token: 0x04000113 RID: 275
		private global::System.Windows.Forms.Label v;

		// Token: 0x04000114 RID: 276
		private GroupBox w;

		// Token: 0x04000115 RID: 277
		private global::System.Windows.Forms.CheckBox x;

		// Token: 0x04000116 RID: 278
		private GroupBox y;

		// Token: 0x04000117 RID: 279
		private CheckedListBox z;

		// Token: 0x04000118 RID: 280
		private global::System.Windows.Forms.CheckBox aa;

		// Token: 0x04000119 RID: 281
		private GroupBox ab;

		// Token: 0x0400011A RID: 282
		private ComboBox ac;

		// Token: 0x0400011B RID: 283
		private global::System.Windows.Forms.Button ad;

		// Token: 0x0400011C RID: 284
		private global::System.Windows.Forms.TextBox ae;

		// Token: 0x0400011D RID: 285
		private global::System.Windows.Forms.Button af;

		// Token: 0x0400011E RID: 286
		private GroupBox ag;

		// Token: 0x0400011F RID: 287
		private global::System.Windows.Forms.Button ah;

		// Token: 0x04000120 RID: 288
		private global::System.Windows.Forms.TextBox ai;

		// Token: 0x04000121 RID: 289
		private global::System.Windows.Forms.Label aj;

		// Token: 0x04000122 RID: 290
		private global::System.Windows.Forms.TextBox ak;

		// Token: 0x04000123 RID: 291
		private global::System.Windows.Forms.Label al;

		// Token: 0x04000124 RID: 292
		private LinkLabel am;

		// Token: 0x04000125 RID: 293
		private GroupBox an;

		// Token: 0x04000126 RID: 294
		private global::System.Windows.Forms.TextBox ao;

		// Token: 0x04000127 RID: 295
		private global::System.Windows.Forms.Button ap;

		// Token: 0x04000128 RID: 296
		private global::System.Windows.Forms.TextBox aq;

		// Token: 0x04000129 RID: 297
		private GroupBox ar;

		// Token: 0x0400012A RID: 298
		private global::System.Windows.Forms.Button @as;

		// Token: 0x0400012B RID: 299
		private global::System.Windows.Forms.Button at;

		// Token: 0x0400012C RID: 300
		private global::System.Windows.Forms.Button au;

		// Token: 0x0400012D RID: 301
		private global::System.Windows.Forms.TextBox av;

		// Token: 0x0400012E RID: 302
		private global::System.Windows.Forms.Label aw;

		// Token: 0x0400012F RID: 303
		private LinkLabel ax;

		// Token: 0x04000130 RID: 304
		private GroupBox ay;

		// Token: 0x04000131 RID: 305
		private DateTimePicker az;

		// Token: 0x04000132 RID: 306
		private global::System.Windows.Forms.Button a0;

		// Token: 0x04000133 RID: 307
		private global::System.Windows.Forms.Button a1;

		// Token: 0x04000134 RID: 308
		private global::System.Windows.Forms.TextBox a2;

		// Token: 0x04000135 RID: 309
		private global::System.Windows.Forms.Label a3;

		// Token: 0x04000136 RID: 310
		private global::System.Windows.Forms.RadioButton a4;

		// Token: 0x04000137 RID: 311
		private global::System.Windows.Forms.RadioButton a5;

		// Token: 0x04000138 RID: 312
		private global::System.Windows.Forms.Panel a6;

		// Token: 0x04000139 RID: 313
		private global::System.Windows.Forms.TextBox a7;

		// Token: 0x0400013A RID: 314
		private global::System.Windows.Forms.Label a8;

		// Token: 0x0400013B RID: 315
		private global::System.Windows.Forms.RadioButton a9;

		// Token: 0x0400013C RID: 316
		private global::System.Windows.Forms.CheckBox ba;

		// Token: 0x0200002E RID: 46
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x0600012E RID: 302 RVA: 0x0001D35C File Offset: 0x0001B55C
			internal void b()
			{
				try
				{
					AliwwMessageManager.Delete(this.a);
					LogSendResultManager.Delete(this.a);
					ErrorLogManager.Delete(this.a);
					MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog("删除失败，出现异常了" + Environment.NewLine + ex.ToString(), 1);
				}
			}

			// Token: 0x04000141 RID: 321
			public DateTime a;
		}
	}
}
