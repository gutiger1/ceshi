using System;
using System.Data;
using Agiso.DBAccess;
using Agiso.Extensions;
using Agiso.Utils;

namespace Agiso
{
	// Token: 0x020000D3 RID: 211
	public class DbHelper
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x00046850 File Offset: 0x00044A50
		private static string[] a()
		{
			return new string[] { "\r\nPRAGMA foreign_keys = OFF;\r\n", "\r\nCREATE TABLE IF NOT EXISTS 'AldsAccount' (\r\n'IdNo'  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n'UserNick'  TEXT(32) NOT NULL,\r\n'Password'  TEXT(32) NOT NULL,\r\n'Idx'  INTEGER,\r\n'Option1'  INTEGER,\r\n'ManualNick'  TEXT,\r\n'ModifyTime'  TEXT,\r\n'CreateTime'  TEXT\r\n);\r\n\r\nCREATE UNIQUE INDEX IF NOT EXISTS 'Idx_Account_UserNick'\r\nON 'AldsAccount' ('UserNick' ASC);\r\n\r\nCREATE TABLE IF NOT EXISTS CustomerServiceMould (\r\nIdNo  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\nSellerNick  TEXT,\r\nTitle  TEXT,\r\nModifyTime TEXT,\r\nCreateTime TEXT\r\n);\r\n\r\nCREATE UNIQUE INDEX IF NOT EXISTS Idx_CustomerServiceMould_SellerNick_Title\r\nON CustomerServiceMould (SellerNick ASC, Title ASC);\r\n\r\nCREATE TABLE IF NOT EXISTS CustomerServiceWorksheet (\r\nIdNo  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\nMouldId  INTEGER NOT NULL,\r\nSellerNick TEXT,\r\nUserNick  TEXT,\r\nWorkTimeJson TEXT,\r\nModifyTime TEXT,\r\nCreateTime TEXT\r\n);\r\n\r\nCREATE TABLE IF NOT EXISTS 'AliwwMessage' (\r\n'IdNo'  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n'MsgId'  INTEGER NOT NULL,\r\n'Tid'  INTEGER NOT NULL,\r\n'SellerNick'  TEXT(32) NOT NULL,\r\n'BuyerNick'  TEXT(32) NOT NULL,\r\n'MessageTitle'  TEXT(500),\r\n'MessageBody'  TEXT(5000),\r\n'ModifyTime'  TEXT,\r\n'CreateTime'  TEXT,\r\n'CreateTimeLocal'  TEXT\r\n);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_AM_Tid'\r\nON 'AliwwMessage' ('Tid' ASC);\r\n\r\nCREATE UNIQUE INDEX IF NOT EXISTS 'Idx_AM_MsgId'\r\nON 'AliwwMessage' ('MsgId' ASC);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_AM_SellerNick_CreateTime'\r\nON 'AliwwMessage' ('SellerNick' ASC, 'CreateTime' ASC);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_AM_CreateTime'\r\nON 'AliwwMessage' ('CreateTime' ASC);\r\n\r\n\r\nCREATE TABLE IF NOT EXISTS 'ErrorLog' (\r\n'IdNo'  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n'LogContent'  TEXT(2000),\r\n'CreateTime'  TEXT\r\n);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_ErrorLog_CreateTime'\r\nON 'ErrorLog' ('CreateTime' ASC);\r\n\r\nCREATE TABLE IF NOT EXISTS 'LogSendResult' (\r\n'IdNo'  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n'MsgId'  INTEGER NOT NULL,\r\n'UserNick'       TEXT(50),\r\n'SendResultCode' INTEGER,\r\n'SendResultMsg'  TEXT(200),\r\n'SendSoftware' INTEGER DEFAULT 0,\r\n'CreateTimeLocal'  TEXT\r\n);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_LogSR_CreateTimeLocal'\r\nON 'LogSendResult' ('CreateTimeLocal' ASC);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_LogSR_MsgId_SendResultCode'\r\nON 'LogSendResult' ('MsgId' ASC, 'SendResultCode' ASC);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_LogSR_SendResultCode_CreateTimeLocal'\r\nON 'LogSendResult' ('SendResultCode' ASC, 'CreateTimeLocal' ASC);\r\n\r\n\r\nCREATE TABLE IF NOT EXISTS 'SystemSettings' (\r\n'IdNo'  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n'SendMessageHotKey'  TEXT(10) NOT NULL,\r\n'Option1'  INTEGER NOT NULL,\r\n'SendInterval'  INTEGER,\r\n'CloseWindowWhenSended'  INTEGER NOT NULL,\r\n'CloseWindowBeforeSend'  INTEGER NOT NULL,\r\n'AliwwMessageLengthMax'  INTEGER,\r\n'ManualNick'  TEXT,\r\n'MsgSendFirstTime'  TEXT,\r\n'CurrentVersion' INTEGER,\r\n'ModifyTime'  TEXT,\r\n'CreateTime'  TEXT\r\n);\r\n\r\nCREATE TABLE IF NOT EXISTS 'AutoReply' (\r\n'IdNo'  INTEGER NOT NULL,\r\n'Idx'  INTEGER NOT NULL,\r\n'Enabled'   INTEGER,\r\n'Type'  INTEGER NOT NULL,\r\n'KeyWord'  TEXT NOT NULL,\r\n'ReplyWord'  TEXT NOT NULL,\r\n'SellerNick'  TEXT NOT NULL,\r\n'Option1'  INTEGER,\r\n'ModifyTime'  TEXT NOT NULL,\r\n'CreateTime'  TEXT NOT NULL,\r\nPRIMARY KEY ('IdNo' ASC)\r\n);\r\n\r\nCREATE UNIQUE INDEX IF NOT EXISTS 'Idx_AR_SellerNick_KeyWord'\r\nON 'AutoReply' ('SellerNick' ASC, 'KeyWord' ASC);\r\n\r\nCREATE TABLE IF NOT EXISTS 'LogAutoReply' (\r\n'IdNo'  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n'SellerNick'  TEXT(32) NOT NULL,\r\n'SenderNick'  TEXT(32) NOT NULL,\r\n'SenderId'  TEXT(32),\r\n'ConsultWord'  TEXT,\r\n'ConsultTime'  TEXT,\r\n'MatchType'  INTEGER,\r\n'KeyWord'  TEXT,\r\n'ReplyWord'  TEXT,\r\n'DutyManualNick' TEXT,\r\n'FromType' INTEGER,\r\n'CreateTime'  TEXT NOT NULL\r\n);\r\n\r\nCREATE TABLE IF NOT EXISTS 'LogFirstReply' (\r\n'IdNo'  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n'SellerNick'  TEXT(32) NOT NULL,\r\n'SenderNick'  TEXT(32) NOT NULL,\r\n'SenderId'  TEXT(32),\r\n'ConsultWord'  TEXT,\r\n'ConsultTime'  TEXT,\r\n'ReplyWord'  TEXT,\r\n'CreateTime'  TEXT NOT NULL\r\n);\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_LogFirstReply_SellerNick_SenderNick_CreateTime'\r\nON 'LogFirstReply' ('SellerNick' ASC, 'SenderNick' ASC, 'CreateTime' ASC);\r\n\r\nCREATE TABLE IF NOT EXISTS  AliwwTalkClickPoint (\r\n     UserNick TEXT(32) PRIMARY KEY NOT NULL,\r\n     PointX INTEGER DEFAULT NULL,\r\n     PointY INTEGER DEFAULT NULL,\r\n     CreateTime TEXT NOT NULL\r\n);\r\n\r\nCREATE TABLE IF NOT EXISTS LogSyncEmotion(\r\n    'SellerNick' TEXT PRIMARY KEY,\r\n    'LastUserNick' TEXT,\r\n    'LastSyncTime' TEXT\r\n);\r\n\r\nCREATE TABLE IF NOT EXISTS 'AliwwUserInfo' (\r\n    IdNo INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n    UserNick TEXT(64) NOT NULL,\r\n    UserId INTEGER NOT NULL,\r\n    CreateTime TEXT\r\n);\r\n\r\nCREATE UNIQUE INDEX IF NOT EXISTS Idx_AliwwUserInfo_UserNick ON AliwwUserInfo ( UserNick ASC );\r\n" };
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0004687C File Offset: 0x00044A7C
		public static string[] GetInitAutoReplyData()
		{
			return new string[] { "\r\nINSERT OR IGNORE INTO 'AutoReply' (IdNo,Idx,Enabled,Type,KeyWord,ReplyWord,SellerNick,Option1,ModifyTime,CreateTime)\r\nSELECT 4, 1, null, 100, 1, '{@提取}只查询今天发货的订单，超过的请复制以下网址到浏览器提取：\r\n{$提取链接}', '【适用所有已添加的旺旺】', 0, '2001-01-01 00:00:00', '2001-01-01 00:00:00'\r\nUNION SELECT 5, 1, null, 100, 2, '邮箱发货一般发到您购买时填写在“给卖家的留言”的邮箱，或支付宝邮箱，或注册淘宝时的邮箱。\r\n旺旺发送如果您没收到，可以复制以下链接到浏览器提取：\r\n{$提取链接}', '【适用所有已添加的旺旺】', 0, '2001-01-01 00:00:00', '2001-01-01 00:00:00'\r\nUNION SELECT 6, 1, null, 100, 3, '{@转接}', '【适用所有已添加的旺旺】', 0, '2001-01-01 00:00:00', '2001-01-01 00:00:00'\r\nUNION SELECT 8, 1, null, 300, '提取,发货,提货,发卡,怎么不发', '{@提取}只查询今天发货的订单，超过的请提供订单编号，或自行复制以下网址到浏览器提取：\r\n{$提取链接}', '【适用所有已添加的旺旺】', 0, '2001-01-01 00:00:00', '2001-01-01 00:00:00'\r\nUNION SELECT 9, 1, null, 300, '在不在,在吗,在嘛,在么,有没有人,有人吗,(您好,你好)+(在不,zai),在不+(？,?)', '您好，在的。请说。  /:^_^', '【适用所有已添加的旺旺】', 0, '2001-01-01 00:00:00', '2001-01-01 00:00:00'\r\nUNION SELECT 3, 1, null, 500, '【Agiso首次智能答复Agiso】', '回复 1、提货\r\n回复 2、了解提货方法\r\n回复 3、找店主', '【适用所有已添加的旺旺】', 0, '2001-01-01 00:00:00', '2001-01-01 00:00:00';\r\n" };
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0004689C File Offset: 0x00044A9C
		public static bool InitDb()
		{
			return DbAccessDAL.InitDb(DbHelper.a());
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000468B4 File Offset: 0x00044AB4
		public static void UpdateDb()
		{
			if (AppConfig.CurrentSystemSettingInfo != null)
			{
				IDbAccess dbAccess = DbAccessDAL.CreateDbAccess();
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 50608)
				{
					string text = "\r\nALTER TABLE AldsAccount ADD COLUMN DefaultMouldId INTEGER DEFAULT 0;\r\nALTER TABLE LogAutoReply RENAME TO LogAutoReply_bak50608;\r\n\r\nCREATE TABLE IF NOT EXISTS 'LogAutoReply' (\r\n'IdNo'  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n'SellerNick'  TEXT(32) NOT NULL,\r\n'SenderNick'  TEXT(32) NOT NULL,\r\n'SenderId'  TEXT(32),\r\n'ConsultWord'  TEXT,\r\n'ConsultTime'  TEXT,\r\n'MatchType'  INTEGER,\r\n'KeyWord'  TEXT,\r\n'ReplyWord'  TEXT,\r\n'DutyManualNick' TEXT,\r\n'FromType' INTEGER,\r\n'CreateTime'  TEXT NOT NULL\r\n); ";
					dbAccess.ExecuteNonQuery(text);
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 50609)
				{
					string text2 = "ALTER TABLE AldsAccount ADD TransferNickIfNotDuty TEXT";
					dbAccess.ExecuteNonQuery(text2);
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion >= 50611)
				{
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 50612)
				{
					string text3 = "\r\nDROP INDEX IF EXISTS 'Idx_LogAutoReply_KeyWord';\r\nDROP INDEX IF EXISTS 'Idx_LogAutoReply_SellerNick_SenderNick_CreateTime'; \r\nCREATE INDEX IF NOT EXISTS 'Idx_LogAutoReply_KeyWord'\r\nON 'LogAutoReply'('KeyWord' ASC);\r\nCREATE INDEX IF NOT EXISTS 'Idx_LogAutoReply_SellerNick_SenderNick_CreateTime'\r\nON 'LogAutoReply'('SellerNick' ASC, 'SenderNick' ASC, 'CreateTime' ASC);\r\nDROP INDEX IF EXISTS 'Idx_CustomerServiceWorksheet_MouldId_UserNick';\r\nCREATE UNIQUE INDEX IF NOT EXISTS Idx_CustomerServiceWorksheet_MouldId_UserNick\r\nON CustomerServiceWorksheet(MouldId ASC, UserNick ASC);\r\n";
					dbAccess.ExecuteNonQuery(text3);
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 50615)
				{
					string text4 = "ALTER TABLE LogAutoReply ADD IsTransferFail INTEGER";
					dbAccess.ExecuteNonQuery(text4);
					text4 = "ALTER TABLE LogAutoReply ADD TransferFailMsg TEXT";
					dbAccess.ExecuteNonQuery(text4);
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 50701)
				{
					string text5 = "INSERT OR IGNORE INTO 'AutoReply'(IdNo, Idx, Enabled, Type, KeyWord, ReplyWord, SellerNick, Option1, ModifyTime, CreateTime)\r\nSELECT 3, 1, null, 500, '【Agiso首次智能答复Agiso】', " + DbUtil.ToSqlString(AppConfig.CurrentSystemSettingInfo.MsgSendFirstTime) + ", '【适用所有已添加的旺旺】', 0, '2001-01-01 00:00:00', '2001-01-01 00:00:00'";
					dbAccess.ExecuteNonQuery(text5);
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 50704)
				{
					AppConfig.CurrentSystemSettingInfo.AliwwMessageLengthMax = AppConfig.AliwwMessageLengthMax;
					AppConfig.CurrentSystemSettingInfo.AutoReplyBySellerNick = AppConfig.IsAutoReplyBySellerNick;
					AppConfig.CurrentSystemSettingInfo.AllowGetMsgByWebSocket = AppConfig.AllowGetMsgByWebSocket;
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 50800)
				{
					AppConfig.CurrentSystemSettingInfo.AllowSendMsgByWebSocket = true;
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 50807)
				{
					string text6 = "\r\nCREATE TABLE IF NOT EXISTS  LogQnAliresRecode (\r\n  FileName TEXT(255) PRIMARY KEY NOT NULL,\r\n  FileModifyTime TEXT,\r\n  CreateTime TEXT\r\n);\r\n";
					dbAccess.ExecuteNonQuery(text6);
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 50904)
				{
					try
					{
						string text7 = "\r\nALTER TABLE LogQnAliresRecode ADD COLUMN ModifyVersion INTEGER DEFAULT 50900";
						dbAccess.ExecuteNonQuery(text7);
						text7 = "\r\nALTER TABLE LogQnAliresRecode ADD COLUMN ModifyJsUrl TEXT DEFAULT '<script src=''http://wwmsg.agiso.com/im'' id=''_agi''></script>'";
						dbAccess.ExecuteNonQuery(text7);
					}
					catch (Exception ex)
					{
						LogWriter.WriteLog(ex.ToString(), 1);
					}
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 60101 && AppConfig.CurrentSystemSettingInfo.CurrentVersion != 51001)
				{
					string text8 = "\r\nALTER TABLE SystemSettings ADD COLUMN InsertMsgSuccInterval INTEGER DEFAULT 1000";
					dbAccess.ExecuteNonQuery(text8);
					AppConfig.CurrentSystemSettingInfo.InsertMsgSuccInterval = 1000;
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 60104)
				{
					string text9 = "ALTER TABLE AldsAccount ADD COLUMN NotDutyNickReplyMsg TEXT";
					dbAccess.ExecuteNonQuery(text9);
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 60107)
				{
					string text10 = "ALTER TABLE SystemSettings ADD COLUMN SameQueryReplyInterval INTEGER DEFAULT 60";
					dbAccess.ExecuteNonQuery(text10);
					text10 = "ALTER TABLE SystemSettings ADD COLUMN RecvMsgReplyInterval INTEGER DEFAULT 0";
					dbAccess.ExecuteNonQuery(text10);
					text10 = "ALTER TABLE SystemSettings ADD COLUMN NoMatchReplyInterval INTEGER DEFAULT 0";
					dbAccess.ExecuteNonQuery(text10);
					text10 = "ALTER TABLE SystemSettings ADD COLUMN TransferInterval INTEGER DEFAULT 0";
					dbAccess.ExecuteNonQuery(text10);
					text10 = "ALTER TABLE SystemSettings ADD COLUMN FirstReplyInterval INTEGER DEFAULT 30";
					dbAccess.ExecuteNonQuery(text10);
					AppConfig.CurrentSystemSettingInfo.SameQueryReplyInterval = 60;
					AppConfig.CurrentSystemSettingInfo.FirstReplyInterval = 30;
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 60209)
				{
					string text11 = "ALTER TABLE LogQnAliresRecode ADD COLUMN ModifyJsUrl2 TEXT DEFAULT ''";
					dbAccess.ExecuteNonQuery(text11);
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 60303)
				{
					DataTable dataTable = dbAccess.ExecuteTable("*", "AldsAccount", "1=1");
					if (dataTable != null && dataTable.Rows.Count > 0)
					{
						foreach (object obj in dataTable.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							object obj2 = dataRow["Password"];
							string text12 = ((obj2 != null) ? obj2.ToString().smethod_0("782ki934", "8ht9zh90") : null);
							string text13 = string.Format("Update AldsAccount Set Password={0} Where IdNo = {1}", DbUtil.ToSqlString(text12), Util.ToLong(dataRow["IdNo"]));
							dbAccess.ExecuteNonQuery(text13);
						}
					}
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 60316)
				{
					string text14 = "ALTER TABLE AutoReply ADD COLUMN Valid INTEGER DEFAULT 1";
					dbAccess.ExecuteNonQuery(text14);
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 60334)
				{
					string text15 = "ALTER TABLE AliwwMessage ADD COLUMN BuyerOpenUid TEXT(64) DEFAULT null";
					dbAccess.ExecuteNonQuery(text15);
					text15 = "CREATE INDEX IF NOT EXISTS 'Idx_AM_SellerNick_BuyerOpenUid_CreateTime'\r\nON 'AliwwMessage'('SellerNick' ASC, 'BuyerOpenUid' ASC, 'CreateTime' ASC)";
					dbAccess.ExecuteNonQuery(text15);
					text15 = "ALTER TABLE LogAutoReply ADD COLUMN SenderOpenUid TEXT(64) DEFAULT null";
					dbAccess.ExecuteNonQuery(text15);
					text15 = "ALTER TABLE LogFirstReply ADD COLUMN SenderOpenUid TEXT(64) DEFAULT null";
					dbAccess.ExecuteNonQuery(text15);
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 60501)
				{
					string text16 = "\r\nCREATE INDEX IF NOT EXISTS 'Idx_AM_SellerNick_BuyerOpenUid_CreateTime'\r\nON 'AliwwMessage' ('SellerNick' ASC, 'BuyerOpenUid' ASC, 'CreateTime' ASC);\r\nCREATE INDEX IF NOT EXISTS 'Idx_LogFirstReply_SellerNick_SenderOpenUid_CreateTime'\r\nON 'LogFirstReply' ('SellerNick' ASC, 'SenderOpenUid' ASC, 'CreateTime' ASC);\r\nCREATE INDEX IF NOT EXISTS 'Idx_LogAutoReply_SellerNick_SenderOpenUid_CreateTime'\r\nON 'LogAutoReply'('SellerNick' ASC, 'SenderOpenUid' ASC, 'CreateTime' ASC);";
					dbAccess.ExecuteNonQuery(text16);
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 60514)
				{
					string text17 = "\r\nCREATE TABLE IF NOT EXISTS 'AgentAldsPlugNotice' (\r\n'UserNick'  TEXT(32) PRIMARY KEY NOT NULL ,\r\n'NoticeTime'  TEXT\r\n);";
					dbAccess.ExecuteNonQuery(text17);
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 60616)
				{
					string text18 = "\r\nDROP INDEX Idx_AR_SellerNick_KeyWord;\r\n\r\nCREATE INDEX IF NOT EXISTS 'Idx_AR_SellerNick_KeyWord'\r\nON 'AutoReply' ('SellerNick' ASC, 'KeyWord' ASC);\r\n";
					dbAccess.ExecuteNonQuery(text18);
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < 60618)
				{
					string text19 = "ALTER TABLE AutoReply ADD COLUMN Grade INTEGER NOT NULL DEFAULT 100";
					dbAccess.ExecuteNonQuery(text19);
					text19 = "ALTER TABLE AutoReply ADD COLUMN ArStartTime TEXT";
					dbAccess.ExecuteNonQuery(text19);
					text19 = "ALTER TABLE AutoReply ADD COLUMN ArEndTime TEXT";
					dbAccess.ExecuteNonQuery(text19);
					if (!AppConfig.CurrentSystemSettingInfo.OnlyFirstReply)
					{
						AppConfig.CurrentSystemSettingInfo.FirstReplyContinueMatch = true;
						AppConfig.CurrentSystemSettingInfo.FirstReplyContinueNoMatch = true;
					}
				}
				if (AppConfig.CurrentSystemSettingInfo.CurrentVersion < AppConfig.CurrentAliwwClientVersion)
				{
					AppConfig.CurrentSystemSettingInfo.CurrentVersion = AppConfig.CurrentAliwwClientVersion;
					AppConfig.SaveConfig();
				}
			}
		}
	}
}
