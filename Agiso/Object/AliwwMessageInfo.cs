using System;
using System.Data;
using Agiso.DBAccess;

namespace Agiso.Object
{
	// Token: 0x020000A1 RID: 161
	[Serializable]
	public class AliwwMessageInfo : ICloneable
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000038C2 File Offset: 0x00001AC2
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x000038CA File Offset: 0x00001ACA
		public long IdNo { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000038D3 File Offset: 0x00001AD3
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x000038DB File Offset: 0x00001ADB
		public long MsgId { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x000038E4 File Offset: 0x00001AE4
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x000038EC File Offset: 0x00001AEC
		public long Tid { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x000038F5 File Offset: 0x00001AF5
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x000038FD File Offset: 0x00001AFD
		public string SellerNick { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00003906 File Offset: 0x00001B06
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000390E File Offset: 0x00001B0E
		public string UserNick { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00003917 File Offset: 0x00001B17
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000391F File Offset: 0x00001B1F
		public string BuyerNick { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00003928 File Offset: 0x00001B28
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x00003930 File Offset: 0x00001B30
		public string BuyerOpenUid { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00003939 File Offset: 0x00001B39
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00003941 File Offset: 0x00001B41
		public string MessageTitle { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000394A File Offset: 0x00001B4A
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x00003952 File Offset: 0x00001B52
		public string MessageBody { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000395B File Offset: 0x00001B5B
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x00003963 File Offset: 0x00001B63
		public DateTime ModifyTime { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000396C File Offset: 0x00001B6C
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x00003974 File Offset: 0x00001B74
		public DateTime CreateTime { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000397D File Offset: 0x00001B7D
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x00003985 File Offset: 0x00001B85
		public DateTime CreateTimeLocal { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000398E File Offset: 0x00001B8E
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00003996 File Offset: 0x00001B96
		public DateTime EnqueueTime { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000399F File Offset: 0x00001B9F
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x000039A7 File Offset: 0x00001BA7
		public DateTime DequeueTime { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x000039B0 File Offset: 0x00001BB0
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x000039B8 File Offset: 0x00001BB8
		public bool IsComplete { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000039C1 File Offset: 0x00001BC1
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x000039C9 File Offset: 0x00001BC9
		public EnumAliwwMessageSource AliwwMessageSourceType { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000039D2 File Offset: 0x00001BD2
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x000039DA File Offset: 0x00001BDA
		public MsgSendSoftware SendSoftware { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000039E3 File Offset: 0x00001BE3
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x000039EB File Offset: 0x00001BEB
		public bool Stop { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x000039F4 File Offset: 0x00001BF4
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x000039FC File Offset: 0x00001BFC
		public bool IsNeedScreenshot { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00003A05 File Offset: 0x00001C05
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00003A0D File Offset: 0x00001C0D
		public string FileName { get; set; }

		// Token: 0x0600049D RID: 1181 RVA: 0x0003D4AC File Offset: 0x0003B6AC
		public static AliwwMessageInfo Map(DataRow row)
		{
			AliwwMessageInfo aliwwMessageInfo;
			if (row == null)
			{
				aliwwMessageInfo = null;
			}
			else
			{
				AliwwMessageInfo aliwwMessageInfo2 = new AliwwMessageInfo();
				aliwwMessageInfo2.IdNo = DbUtil.TrimLongNull(row["IdNo"]);
				aliwwMessageInfo2.MsgId = DbUtil.TrimLongNull(row["MsgId"]);
				aliwwMessageInfo2.Tid = DbUtil.TrimLongNull(row["Tid"]);
				aliwwMessageInfo2.SellerNick = DbUtil.TrimNull(row["SellerNick"]);
				aliwwMessageInfo2.BuyerNick = DbUtil.TrimNull(row["BuyerNick"]);
				if (row.Table.Columns.Contains("BuyerOpenUid"))
				{
					aliwwMessageInfo2.BuyerOpenUid = DbUtil.TrimNull(row["BuyerOpenUid"]);
				}
				aliwwMessageInfo2.MessageTitle = DbUtil.TrimNull(row["MessageTitle"]);
				aliwwMessageInfo2.MessageBody = DbUtil.TrimNull(row["MessageBody"]);
				aliwwMessageInfo2.ModifyTime = DbUtil.TrimDateNull(row["ModifyTime"]);
				aliwwMessageInfo2.CreateTime = DbUtil.TrimDateNull(row["CreateTime"]);
				aliwwMessageInfo2.CreateTimeLocal = DbUtil.TrimDateNull(row["CreateTimeLocal"]);
				aliwwMessageInfo = aliwwMessageInfo2;
			}
			return aliwwMessageInfo;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0003D5E4 File Offset: 0x0003B7E4
		public object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
