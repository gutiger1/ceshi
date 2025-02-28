using System;
using System.IO;
using System.Windows.Forms;
using Agiso.DBAccess;

namespace Agiso.Utils
{
	// Token: 0x020000E7 RID: 231
	public static class LogWriter
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00049194 File Offset: 0x00047394
		private static StreamWriter Writer
		{
			get
			{
				if (LogWriter.a == null)
				{
					int num = 1;
					for (;;)
					{
						try
						{
							LogWriter.a = new StreamWriter(string.Format("LOG\\{0}{1}.log", DateTime.Today.ToString("yyyyMMdd"), (num == 1) ? "" : ("_" + num.ToString().Trim())), true);
							break;
						}
						catch
						{
							num++;
						}
					}
				}
				return LogWriter.a;
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00049220 File Offset: 0x00047420
		public static bool LogToTxt(string textLine, bool autoFlush, bool autoPrintDateTime)
		{
			if (autoPrintDateTime)
			{
				textLine = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + "\t" + textLine;
			}
			if (!Directory.Exists("LOG"))
			{
				LogWriter.a("LOG");
			}
			LogWriter.Writer.AutoFlush = autoFlush;
			TextWriter @out = Console.Out;
			bool flag = false;
			try
			{
				Console.SetOut(LogWriter.Writer);
				Console.WriteLine(textLine);
				flag = true;
			}
			catch (Exception ex)
			{
				TextWriter error = Console.Error;
				error.WriteLine(ex.Message);
				flag = false;
			}
			finally
			{
				Console.SetOut(@out);
			}
			return flag;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000492D4 File Offset: 0x000474D4
		private static bool a(string A_0, bool A_1)
		{
			if (A_1)
			{
				A_0 = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss.fff]") + "\t" + A_0;
			}
			bool flag = false;
			FileStream fileStream = new FileStream(Util.GetAbsoluteFilePath(string.Format("ErrorLog_{0}.log", DateTime.Today.ToString("yyyyMMdd"))), FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter streamWriter = new StreamWriter(fileStream);
			try
			{
				streamWriter.BaseStream.Seek(0L, SeekOrigin.End);
				streamWriter.WriteLine(A_0);
				flag = true;
			}
			catch
			{
			}
			finally
			{
				streamWriter.Flush();
				streamWriter.Close();
				streamWriter.Dispose();
				fileStream.Close();
				fileStream.Dispose();
			}
			return flag;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000493A0 File Offset: 0x000475A0
		private static int b(string A_0)
		{
			int num;
			try
			{
				num = DbAccessDAL.CreateDbAccess().ExecuteNonQuery(string.Format("INSERT INTO ErrorLog(LogContent, CreateTime) VALUES ({0},{1})", DbUtil.ToSqlString(A_0), DbUtil.ToSqlString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))));
			}
			catch (Exception ex)
			{
				LogWriter.WriteLog(string.Format("插入ErrorLog数据库时错误。错觉误原因：｛{1}｝。原内容：｛{0}｝", A_0, ex.ToString()), 2);
				num = 0;
			}
			return num;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00049414 File Offset: 0x00047614
		public static int WriteLog(string strLog, int level, bool addDatetimed)
		{
			int num = 0;
			if ((level & 1) > 0)
			{
				if (LogWriter.b(strLog) > 0)
				{
					num |= 1;
				}
				else
				{
					level |= 2;
				}
			}
			if ((level & 2) > 0 && LogWriter.LogToTxt(strLog, true, addDatetimed))
			{
				num |= 2;
			}
			if ((level & 4) > 0)
			{
				if (addDatetimed)
				{
					strLog = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss.fff]") + "\t" + strLog;
				}
				Console.WriteLine(strLog);
			}
			return num;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00049490 File Offset: 0x00047690
		public static int WriteLog(string strLog, int level)
		{
			return LogWriter.WriteLog(strLog, level, true);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000043D3 File Offset: 0x000025D3
		private static void a(string A_0)
		{
			FolderAuthorize.SetFolderACL(Application.StartupPath, "everyone");
			Directory.CreateDirectory(A_0);
		}

		// Token: 0x040004D1 RID: 1233
		private static StreamWriter a;
	}
}
