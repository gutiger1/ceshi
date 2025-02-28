using System;
using System.Collections;
using Agiso.Utils;

namespace Agiso.AliwwApi
{
	// Token: 0x02000708 RID: 1800
	public static class QnApiConsts
	{
		// Token: 0x06002391 RID: 9105 RVA: 0x0005D5B0 File Offset: 0x0005B7B0
		public static string GetJsForSetMsg(string contactNick, string contactSiteId, string contactOpenUid, string msgContent, int type = 0)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["uid"] = contactSiteId + contactNick;
			hashtable["text"] = msgContent;
			hashtable["type"] = type;
			Hashtable hashtable2 = new Hashtable();
			hashtable2["cmd"] = "insertText2Inputbox";
			hashtable2["param"] = hashtable;
			return string.Format(Security.Decrypt("CE630816FED0DBBD7E6D368A7CC1B5098D28B2EE4438CF33985885184783A273", "By Agiso"), JSON.Encode(hashtable2));
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x0005D63C File Offset: 0x0005B83C
		public static string GetJsForSendMsg(string targetNick, string targetSiteId, string msgContent, int type = 1)
		{
			string text = targetSiteId + targetNick;
			return Security.Decrypt("", "By Agiso").Replace("{$J_targetID}", text).Replace("{$J_msgContent}", msgContent)
				.Replace("{$J_type}", type.ToString());
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x0005D690 File Offset: 0x0005B890
		public static string GetJsForSendMsg(string targetNick, string msgContent, int type = 1)
		{
			return QnApiConsts.GetJsForSendMsg(targetNick, "cntaobao", msgContent, type);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x0005D6B0 File Offset: 0x0005B8B0
		public static string GetJsForTransferContact(string contactNick, string contactSiteId, string targetNick, string targetSiteId)
		{
			string text = contactSiteId + contactNick;
			string text2 = targetSiteId + targetNick;
			return Security.Decrypt("B7699A2D1D2703F8656DB01E1057097556CB983C84554F6BF37F14DFA5CBBDE62D6615801178AB739E2EEF6454A8E3E23488C3958D4CFD5AA86C0407DAC7327F089BF6BFD5B0F7E3BAA79AF4170BB14C41DDE6B8937E603D7353FE36D76DE80DAF6971274D798FE87606046276B3BAB8750CF55F65842324835235F3713648D1E5EC0CD78609E444A481C0014CE1AFE6F016D7A67816BEB17A46E94AC8175D0A", "By Agiso").Replace("{$J_contactID}", text).Replace("{$J_targetID}", text2);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x0005D6F8 File Offset: 0x0005B8F8
		public static string GetJsForIncludeJs(string jsSrcUrl)
		{
			return "; $(function(){ $('body').append('<script src=\"" + jsSrcUrl + "\"></script>'); });";
		}

		// Token: 0x04001DB3 RID: 7603
		public const string WIN_CHROME_QN_INDEX_URL_CONTAIN = "/g.alicdn.com/qn-enterprise/home/";

		// Token: 0x04001DB4 RID: 7604
		public const string WIN_CHROME_QN_INDEX_URL_CONTAIN_6_3 = "h5.m.taobao.com/qn/pc/shop/index";

		// Token: 0x04001DB5 RID: 7605
		public const string WIN_CHROME_QN_RECENT_URL_CONTAIN = "/recent.html";

		// Token: 0x04001DB6 RID: 7606
		public const string WIN_CHROME_QN_RECENT_URL_CONTAIN606 = "/recent606.html";

		// Token: 0x04001DB7 RID: 7607
		public const string WIN_CHROME_QN_RECENT_URL_NOT_CONTAIN = "type=1&";

		// Token: 0x04001DB8 RID: 7608
		public const string WIN_CHROME_TB_21600714 = "appkey=21600714&";

		// Token: 0x04001DB9 RID: 7609
		public const string WIN_CHROME_TB_21600715 = "appkey=21600715&";

		// Token: 0x04001DBA RID: 7610
		public const string WIN_CHROME_TB_21790264 = "appkey=21790264&";

		// Token: 0x04001DBB RID: 7611
		public const string WIN_CHROME_TB_21619184 = "appkey=21619184&";

		// Token: 0x04001DBC RID: 7612
		public const string WIN_CHROME_ZZGDAPP = "zzgdapp.com/";

		// Token: 0x04001DBD RID: 7613
		public const string WIN_CHROME_ALDS_PC_URL_CONTAIN = "//alds.agiso.com/";

		// Token: 0x04001DBE RID: 7614
		public const string WIN_CHROME_ALDS_PCWW_URL_CONTAIN = "//aldsqn.agiso.com/";

		// Token: 0x04001DBF RID: 7615
		public const string WIN_CHORME_TB_21600712 = "appkey=21600712&";

		// Token: 0x04001DC0 RID: 7616
		public const string WIN_CHORME_TB_23436601 = "appkey=23436601&";

		// Token: 0x04001DC1 RID: 7617
		public const string WIN_CHORME_AIYONG = "q.aiyongbao.com/";

		// Token: 0x04001DC2 RID: 7618
		public const string WIN_CHORME_TB_23433600 = "appkey=23433600&";

		// Token: 0x04001DC3 RID: 7619
		public const string JS_FORMAT_SET_MSG = "CE630816FED0DBBD7E6D368A7CC1B5098D28B2EE4438CF33985885184783A273";
	}
}
