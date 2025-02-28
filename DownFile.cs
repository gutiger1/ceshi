using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Agiso;
using Agiso.Utils;
using AliwwClient.Properties;

namespace AliwwClient
{
	// Token: 0x02000021 RID: 33
	public partial class DownFile : Form
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00002710 File Offset: 0x00000910
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00002718 File Offset: 0x00000918
		public Action<bool> Callback { get; set; }

		// Token: 0x060000B0 RID: 176 RVA: 0x00012AF0 File Offset: 0x00010CF0
		public DownFile(string title, string descript, string remoteUrl, string savePath)
		{
			this.a();
			if (AppConfig.AllowAutoLogin)
			{
				base.StartPosition = FormStartPosition.Manual;
				base.Location = new Point(Screen.PrimaryScreen.Bounds.Width - base.Width, Screen.PrimaryScreen.Bounds.Height - base.Height - 40);
			}
			base.Name = title;
			this.f.Text = descript;
			this.a = remoteUrl;
			this.b = savePath;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002721 File Offset: 0x00000921
		private void c(object sender, EventArgs e)
		{
			this.c = new Thread(new ThreadStart(this.b));
			this.c.Start();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002745 File Offset: 0x00000945
		public void Start()
		{
			this.c(null, null);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00012B84 File Offset: 0x00010D84
		private void b()
		{
			FileStream fileStream = null;
			try
			{
				DownFile.a a = new DownFile.a();
				a.c = this;
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.a);
				a.b = (HttpWebResponse)httpWebRequest.GetResponse();
				Stream responseStream = a.b.GetResponseStream();
				byte[] array = new byte[1000];
				a.a = 0L;
				int i = responseStream.Read(array, 0, 1000);
				string directoryName = Path.GetDirectoryName(this.b);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				if (File.Exists(this.b))
				{
					File.Delete(this.b);
				}
				fileStream = new FileStream(this.b, FileMode.Create, FileAccess.Write);
				while (i > 0)
				{
					fileStream.Write(array, 0, i);
					a.a += (long)i;
					if (base.IsHandleCreated)
					{
						base.Invoke(new EventHandler(a.d));
					}
					i = responseStream.Read(array, 0, 1000);
				}
				responseStream.Close();
				fileStream.Dispose();
				if (base.IsHandleCreated)
				{
					base.Invoke(new EventHandler(this.b));
				}
				Action<bool> callback = this.Callback;
				if (callback != null)
				{
					callback(true);
				}
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog("URL:" + this.a + ", " + ex.ToString(), 1);
				try
				{
					if (File.Exists(this.b))
					{
						File.Delete(this.b);
					}
				}
				catch
				{
				}
				try
				{
					if (base.IsHandleCreated)
					{
						base.Invoke(new EventHandler(this.a));
					}
				}
				catch
				{
				}
				Action<bool> callback2 = this.Callback;
				if (callback2 != null)
				{
					callback2(false);
				}
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00012DBC File Offset: 0x00010FBC
		private void a(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == DialogResult.Cancel && this.c.IsAlive)
			{
				if (MessageBox.Show("文件正在下载中，关闭窗口文件将中止下载，确定要关闭吗？", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
				{
					e.Cancel = true;
				}
				else if (this.c.IsAlive)
				{
					this.c.Abort();
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00012E20 File Offset: 0x00011020
		public bool NeedToDown()
		{
			bool flag;
			if (!File.Exists(this.b))
			{
				flag = true;
			}
			else
			{
				try
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.a);
					HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
					long contentLength = httpWebResponse.ContentLength;
					string text = Util.ComputeHashMd5(httpWebResponse.GetResponseStream());
					using (FileStream fileStream = new FileStream(this.b, FileMode.Open))
					{
						if (fileStream.Length != contentLength)
						{
							return true;
						}
						if (Util.ComputeHashMd5(fileStream) != text)
						{
							return true;
						}
					}
					flag = false;
				}
				catch (Exception ex)
				{
					LogWriter.WriteLog(ex.ToString(), 1);
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00012F20 File Offset: 0x00011120
		private void a()
		{
			base.Icon = Resources.Icon1;
			this.f = new Label();
			this.g = new ProgressBar();
			base.SuspendLayout();
			this.f.AutoSize = true;
			this.f.Location = new Point(12, 20);
			this.f.Name = "lb_descript";
			this.f.Size = new Size(53, 12);
			this.f.TabIndex = 0;
			this.f.Text = "下载描述";
			this.g.Location = new Point(12, 46);
			this.g.Name = "progressBar1";
			this.g.Size = new Size(361, 23);
			this.g.TabIndex = 1;
			this.g.Value = 5;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(385, 81);
			base.Controls.Add(this.g);
			base.Controls.Add(this.f);
			base.Name = "DownFile";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "DownFile";
			base.FormClosing += this.a;
			base.Load += this.c;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000274F File Offset: 0x0000094F
		[CompilerGenerated]
		private void b(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002758 File Offset: 0x00000958
		[CompilerGenerated]
		private void a(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Abort;
		}

		// Token: 0x0400006F RID: 111
		private string a;

		// Token: 0x04000070 RID: 112
		private string b;

		// Token: 0x04000071 RID: 113
		private Thread c;

		// Token: 0x04000072 RID: 114
		[CompilerGenerated]
		private Action<bool> d;

		// Token: 0x04000074 RID: 116
		private Label f;

		// Token: 0x04000075 RID: 117
		private ProgressBar g;

		// Token: 0x02000022 RID: 34
		[CompilerGenerated]
		private sealed class a
		{
			// Token: 0x060000BB RID: 187 RVA: 0x00002761 File Offset: 0x00000961
			internal void d(object sender, EventArgs e)
			{
				this.c.g.Value = (int)((double)this.a * 100.0 / (double)this.b.ContentLength);
			}

			// Token: 0x04000076 RID: 118
			public long a;

			// Token: 0x04000077 RID: 119
			public HttpWebResponse b;

			// Token: 0x04000078 RID: 120
			public DownFile c;
		}
	}
}
