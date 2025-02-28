using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Agiso;
using Agiso.AliwwApi.Qn;
using Agiso.Handler;
using Agiso.Object;
using Agiso.Utils;
using Agiso.Windows;
using AliwwClient;

// Token: 0x02000005 RID: 5
public partial class TestHwndTitleForm : Form
{
	// Token: 0x06000005 RID: 5 RVA: 0x000022BC File Offset: 0x000004BC
	public TestHwndTitleForm()
	{
		this.a();
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000006 RID: 6 RVA: 0x0000F678 File Offset: 0x0000D878
	protected FormErrorLog FormErrorLogInstance
	{
		get
		{
			if (this.a == null || this.a.IsDisposed)
			{
				this.a = new FormErrorLog();
			}
			return this.a;
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000F6B0 File Offset: 0x0000D8B0
	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if ((msg.Msg == 256) | (msg.Msg == 260))
		{
			if (keyData != Keys.F12)
			{
				return base.ProcessCmdKey(ref msg, keyData);
			}
			if (this.FormErrorLogInstance.Visible)
			{
				this.FormErrorLogInstance.Visible = false;
			}
			this.FormErrorLogInstance.Show(this);
		}
		return false;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000F71C File Offset: 0x0000D91C
	private void d(object sender, EventArgs e)
	{
		AliwwQn aliwwQn = new AliwwQn(new AldsAccountInfo
		{
			UserNick = "168商务休闲馆:bruce",
			Password = "168bruce1"
		});
		aliwwQn.SendMsg(AppConfig.RobotUserNick, AppConfig.RobotOpenUid, "你好", "cntaobao");
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000F76C File Offset: 0x0000D96C
	private void c(object sender, EventArgs e)
	{
		Process[] processesByName = Process.GetProcessesByName("AliApp");
		foreach (Process process in processesByName)
		{
			using (process)
			{
				Process process3 = process.Parent();
				if (process3 == null || WindowInfo.GetWindowFromHandler(process3.MainWindowHandle) == null)
				{
					process.Kill();
				}
			}
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0000F7EC File Offset: 0x0000D9EC
	private void b()
	{
		JSON.Encode(new DemoClass
		{
			DemoProt = 9104889119441736L
		});
		JSON.Decode<DemoClass>("{\"Tid\":\"9104889119441736\"}");
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000022DD File Offset: 0x000004DD
	private void b(object sender, EventArgs e)
	{
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000F824 File Offset: 0x0000DA24
	private void a(object sender, EventArgs e)
	{
		Application.DoEvents();
		List<WindowInfo> windowListByClassAndName = Win32Extend.GetWindowListByClassAndName(new WindowStruct
		{
			ClassName = "StandardWindow",
			WindowName = null
		}, this.b.Id, false);
		foreach (WindowInfo windowInfo in windowListByClassAndName)
		{
			if (this.b.Id == windowInfo.ProcessId)
			{
				string text = "_cn_" + windowInfo.Info.ClassName + "_wn_" + windowInfo.Info.WindowName;
				if (!this.c.Contains(text))
				{
					this.c.Add(text);
					Console.WriteLine(text);
				}
			}
		}
		if (windowListByClassAndName.Count > 0)
		{
			Console.WriteLine("----------------------------------");
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000F948 File Offset: 0x0000DB48
	private void a()
	{
		this.e = new TextBox();
		this.f = new Button();
		this.g = new TextBox();
		this.h = new Label();
		this.i = new Label();
		this.j = new Button();
		this.k = new TextBox();
		this.l = new PictureBox();
		((ISupportInitialize)this.l).BeginInit();
		base.SuspendLayout();
		this.e.Location = new global::System.Drawing.Point(119, 14);
		this.e.Name = "textBox1";
		this.e.Size = new Size(201, 21);
		this.e.TabIndex = 0;
		this.f.Location = new global::System.Drawing.Point(326, 12);
		this.f.Name = "button1";
		this.f.Size = new Size(75, 23);
		this.f.TabIndex = 1;
		this.f.Text = "开始查找";
		this.f.UseVisualStyleBackColor = true;
		this.f.Click += this.d;
		this.g.Location = new global::System.Drawing.Point(26, 68);
		this.g.Multiline = true;
		this.g.Name = "textBox2";
		this.g.Size = new Size(375, 160);
		this.g.TabIndex = 2;
		this.h.AutoSize = true;
		this.h.Location = new global::System.Drawing.Point(24, 17);
		this.h.Name = "label1";
		this.h.Size = new Size(89, 12);
		this.h.TabIndex = 3;
		this.h.Text = "查找标题包含：";
		this.i.AutoSize = true;
		this.i.Location = new global::System.Drawing.Point(24, 44);
		this.i.Name = "label2";
		this.i.Size = new Size(89, 12);
		this.i.TabIndex = 6;
		this.i.Text = "查找句柄结点：";
		this.j.Location = new global::System.Drawing.Point(326, 39);
		this.j.Name = "button2";
		this.j.Size = new Size(75, 23);
		this.j.TabIndex = 5;
		this.j.Text = "开始查找";
		this.j.UseVisualStyleBackColor = true;
		this.j.Click += this.c;
		this.k.Location = new global::System.Drawing.Point(119, 41);
		this.k.Name = "textBox3";
		this.k.Size = new Size(201, 21);
		this.k.TabIndex = 4;
		this.l.Location = new global::System.Drawing.Point(162, 125);
		this.l.Name = "pictureBox1";
		this.l.Size = new Size(100, 50);
		this.l.TabIndex = 7;
		this.l.TabStop = false;
		base.AutoScaleDimensions = new SizeF(6f, 12f);
		base.AutoScaleMode = AutoScaleMode.Font;
		base.ClientSize = new Size(428, 248);
		base.Controls.Add(this.l);
		base.Controls.Add(this.i);
		base.Controls.Add(this.j);
		base.Controls.Add(this.k);
		base.Controls.Add(this.h);
		base.Controls.Add(this.g);
		base.Controls.Add(this.f);
		base.Controls.Add(this.e);
		base.Name = "TestHwndTitleForm";
		this.Text = "TestHwndTitle";
		base.Load += this.b;
		((ISupportInitialize)this.l).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	// Token: 0x04000003 RID: 3
	private FormErrorLog a;

	// Token: 0x04000004 RID: 4
	private Process b;

	// Token: 0x04000005 RID: 5
	private List<string> c = new List<string>();

	// Token: 0x04000007 RID: 7
	private TextBox e;

	// Token: 0x04000008 RID: 8
	private Button f;

	// Token: 0x04000009 RID: 9
	private TextBox g;

	// Token: 0x0400000A RID: 10
	private Label h;

	// Token: 0x0400000B RID: 11
	private Label i;

	// Token: 0x0400000C RID: 12
	private Button j;

	// Token: 0x0400000D RID: 13
	private TextBox k;

	// Token: 0x0400000E RID: 14
	private PictureBox l;
}
