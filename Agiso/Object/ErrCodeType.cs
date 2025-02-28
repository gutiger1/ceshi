using System;
using System.ComponentModel;

namespace Agiso.Object
{
	// Token: 0x0200067F RID: 1663
	public enum ErrCodeType
	{
		// Token: 0x04001144 RID: 4420
		[Description("未定义的")]
		Undefined,
		// Token: 0x04001145 RID: 4421
		[Description("呼叫成功旺旺版")]
		CallSuccWw = 101,
		// Token: 0x04001146 RID: 4422
		[Description("呼叫成功千牛版")]
		CallSuccQn = 151,
		// Token: 0x04001147 RID: 4423
		[Description("发送成功")]
		SendSucc = 201,
		// Token: 0x04001148 RID: 4424
		[Description("发送Piece消息成功")]
		SendSuccPiece = 205,
		// Token: 0x04001149 RID: 4425
		[Description("转接完成")]
		TransferCallFinish = 1000,
		// Token: 0x0400114A RID: 4426
		[Description("发送人、接收人、或消息内容未填写")]
		UserNickOrMsgBodyIsNull = -1,
		// Token: 0x0400114B RID: 4427
		[Description("千牛未登录或者千牛已掉线")]
		MainWindowNotFound = -101,
		// Token: 0x0400114C RID: 4428
		[Description("呼叫失败，找不到查找好友控件，联系旺旺 agiso ！")]
		CallFailFindFriendControlNotFound = -104,
		// Token: 0x0400114D RID: 4429
		[Description("呼叫失败，找不到指定发送窗口！")]
		CallFailSpecifySendWindowNotFound = -107,
		// Token: 0x0400114E RID: 4430
		[Description("呼叫失败，设置指定发送窗口的发送对象时失败！")]
		CallFailSpecifySendWindowSetTextFail = -108,
		// Token: 0x0400114F RID: 4431
		[Description("呼叫失败，调用旺旺命令时异常！请尝试重新安装千牛。")]
		CallFailWwcmdException = -153,
		// Token: 0x04001150 RID: 4432
		[Description("呼叫失败，呼叫对象为空！")]
		CallFailTargetNickIsNull = -154,
		// Token: 0x04001151 RID: 4433
		[Description("呼叫失败，买家已被加入千牛黑名单中或者会员不存在")]
		CallFailTargetNickInBlackListOrNotExists = -155,
		// Token: 0x04001152 RID: 4434
		[Description("呼叫失败，买家拒绝任何人加好友，无法发送！")]
		CallFailTargetNickRefuseToAddFriend = -160,
		// Token: 0x04001153 RID: 4435
		[Description("对方设置不接收陌生人消息，无法发送！")]
		CallFailTargetNickReceiveFriendOnly = -161,
		// Token: 0x04001154 RID: 4436
		[Description("检验多次失败，请尝试：千牛聊天窗口不要关闭，千牛右侧自动发货插件设置在第一位，设置方法：https://www.yuque.com/agiso/aldstb/eerb64")]
		CallFailCheckTargetNickUnEquals = -162,
		// Token: 0x04001155 RID: 4437
		[Description("千牛接待中心聊天窗口请设置“自动发货”插件并放在第一个，以便验证当前联系人是否与目标匹配。参考设置方法：https://www.yuque.com/agiso/aldstb/eerb64")]
		CallFailCheckTargetNickUnEqualsBecauseNotAddPlugin = -163,
		// Token: 0x04001156 RID: 4438
		[Description("呼叫失败，找不到主窗口或旺旺、千牛版本不支持！")]
		CallFailMainWindowNotFoundOrVersionNotSupport = -193,
		// Token: 0x04001157 RID: 4439
		[Description("呼叫成功，却找不到聊天窗口！有几种可能：1、发送帐号的千牛或旺旺未登录在该电脑上；2、可尝试重新登录千牛，Win7/8/10系统请使用管理员身份运行千牛后登录；3、重新安装千牛！")]
		CallFailChatWindowNotFound = -194,
		// Token: 0x04001158 RID: 4440
		[Description("启动登录窗成功，但是登录窗无效")]
		CallSucQnButLoginIsInvalidate = -195,
		// Token: 0x04001159 RID: 4441
		[Description("发送全部失败！")]
		SendFailAll = -201,
		// Token: 0x0400115A RID: 4442
		[Description("发送失败，分段发送时部份失败！")]
		SendFailPiece = -202,
		// Token: 0x0400115B RID: 4443
		[Description("找不到聊天记录的IE框！")]
		SendFailChatHistoryIeControlNotFound = -203,
		// Token: 0x0400115C RID: 4444
		[Description("找不到消息输入框！")]
		SendFailMsgInputControlNotFound = -204,
		// Token: 0x0400115D RID: 4445
		[Description("发送Piece消息失败！")]
		SendFailWhileSendPiece = -205,
		// Token: 0x0400115E RID: 4446
		[Description("消息重复输入！")]
		SendFailSetTextIntoMsgRepeat = -206,
		// Token: 0x0400115F RID: 4447
		[Description("填写发送内容到消息框时失败！")]
		SendFailSetTextIntoMsgInputFail = -207,
		// Token: 0x04001160 RID: 4448
		[Description("填写内容后，检查目标发送对象时失败！")]
		SendFailCheckTargetNickFailAfterSetText = -208,
		// Token: 0x04001161 RID: 4449
		[Description("发送失败，未检测到“读”标志")]
		SendFailNotFindRead = -209,
		// Token: 0x04001162 RID: 4450
		[Description("发送失败，检测到小红点")]
		SendFailLittleRedDot = -210,
		// Token: 0x04001163 RID: 4451
		[Description("取原消息记录时异常")]
		SendFailGetOldTextFail = -211,
		// Token: 0x04001164 RID: 4452
		[Description("未检测到千牛与助手的链接")]
		SendFailSessionIsNull = -212,
		// Token: 0x04001165 RID: 4453
		[Description("发送Piece消息失败，填写到消息框的内容长度超过限制！")]
		SendFailMsgTooLong = -219,
		// Token: 0x04001166 RID: 4454
		[Obsolete]
		[Description("发送Piece消息失败，填写消息到输入框失败！")]
		SendFailWhileSendPieceAndWrite = -220,
		// Token: 0x04001167 RID: 4455
		[Description("发送Piece消息失败，检测到消息未发送出去！")]
		SendFailClickSendFail = -221,
		// Token: 0x04001168 RID: 4456
		[Description("发送Piece消息失败，清空聊天窗口失败！")]
		SendFailClearTextFail = -222,
		// Token: 0x04001169 RID: 4457
		[Description("发送消息失败，聊天窗口不可用，有可能是弹窗导致的！")]
		SendFailAliTalkWinDisable = -223,
		// Token: 0x0400116A RID: 4458
		[Description("发送消息失败，发现服务提醒窗口！")]
		SendFailHasFuwuTipWindow = -224,
		// Token: 0x0400116B RID: 4459
		[Description("买家30天内主动和您沟通或下单后，您才能给买家发消息")]
		SendFail30DaysNotContact = -225,
		// Token: 0x0400116C RID: 4460
		[Description("转接时转接菜单窗口未找到！")]
		TransferCallWinCoolMenuNotFind = -1008,
		// Token: 0x0400116D RID: 4461
		[Description("未设置人工客服无法转接！")]
		TransferCallManualNickIsNull = -1009,
		// Token: 0x0400116E RID: 4462
		[Description("转接时查找控件未找到！")]
		TransferCallFindInputNotFind = -1010,
		// Token: 0x0400116F RID: 4463
		[Description("转接失败")]
		TransferCallFailed = -1000,
		// Token: 0x04001170 RID: 4464
		[Description("非客服工作台不支持转接！")]
		TransferCallFailedBecauseOfChatType = -1020,
		// Token: 0x04001171 RID: 4465
		[Description("同一店铺子帐号间相互联系时不支持转接！")]
		TransferCallFailedBecauseOfSameAccount = -1030,
		// Token: 0x04001172 RID: 4466
		[Description("转接开关未开启！")]
		TransferCallIsOff = -1040,
		// Token: 0x04001173 RID: 4467
		[Description("消息发送失败，您的账号被禁言")]
		SendFailAccountIsBanned = -1050,
		// Token: 0x04001174 RID: 4468
		[Description("您发送的消息中可能包含了恶意网址、违规广告及其他类关键词，请停止发送类似的消息")]
		SendFailIllegalKeywords = -1060,
		// Token: 0x04001175 RID: 4469
		[Description("相同咨询者短期内有过相同咨询和相同答复，不处理答复")]
		AutoReplyHasSameRecently = -2010,
		// Token: 0x04001176 RID: 4470
		[Description("未登录千牛，且未开启自动登录")]
		const_50 = -3010,
		// Token: 0x04001177 RID: 4471
		[Description("设置为自动登录千牛，但未设置千牛密码")]
		SendFailQn5NoPwd = -3020,
		// Token: 0x04001178 RID: 4472
		[Description("尝试千牛自动登录时，尝试太久了")]
		SendFailQn5TryToLoginTooManySeconds = -3022,
		// Token: 0x04001179 RID: 4473
		[Description("尝试旺旺自动登录时，尝试太久了")]
		SendFailWwTryToLoginTooManySeconds = -3023,
		// Token: 0x0400117A RID: 4474
		[Description("登录时，找不到千牛登录窗")]
		SendFailQn5LoginWinNotFoundWhileAutoLogin = -3030,
		// Token: 0x0400117B RID: 4475
		[Description("登录窗口疑似被人为关闭")]
		SendFailQn5LoginWinHasBeenCloseByManual = -3031,
		// Token: 0x0400117C RID: 4476
		[Description("登录千牛时，提示错误太多次")]
		SendFailQn5LoginErrorComponentShowTimesTooMany = -3032,
		// Token: 0x0400117D RID: 4477
		[Description("该用户已在本台电脑登录")]
		SendFailQn5LoginErrorUserHasLoginOnThisMachine = -3033,
		// Token: 0x0400117E RID: 4478
		[Description("短信验证超时")]
		SendFailQn5LoginFailMobileValidTimeout = -3039,
		// Token: 0x0400117F RID: 4479
		[Description("该帐号登录需要手机验证码")]
		SendFailQn5LoginFailMobileValidCodeRequired = -3040,
		// Token: 0x04001180 RID: 4480
		[Description("获取不到短信验证码，请检查手机接收短信验证码是否可靠")]
		SendFailQn5LoginFailGetSmsValidCodeNull = -3041,
		// Token: 0x04001181 RID: 4481
		[Description("所用的所有短信验证码都是无效的。")]
		SendFailQn5LoginFailAllSmsCodeInvalid = -3042,
		// Token: 0x04001182 RID: 4482
		[Description("短信验证码输入框未找到")]
		SendFailQn5LoginFailMobileValidCodeInputNotFound = -3043,
		// Token: 0x04001183 RID: 4483
		[Description("登录时需要图形验证码")]
		SendFailQn5LoginFailPicValidCodeRequired = -3045,
		// Token: 0x04001184 RID: 4484
		[Description("图形验证码的网址未找到")]
		SendFailQn5LoginFailPicValidCodeRequiredPathNotFound = -3046,
		// Token: 0x04001185 RID: 4485
		[Description("请求图形验证码失败")]
		SendFailQn5LoginFailPicValidCodeRequiredGetPicCodeError = -3047,
		// Token: 0x04001186 RID: 4486
		[Description("图形验证码的输入框、或者按钮未找到")]
		SendFailQn5LoginFailPicValidCodeRequiredInputOrButtonNotFound = -3048,
		// Token: 0x04001187 RID: 4487
		[Description("尝试图形验证码太多次仍然失败")]
		SendFailQn5LoginFailPicValidCodeRequiredTryTooManyTimes = -3049,
		// Token: 0x04001188 RID: 4488
		[Description("登录时需要选择帐号类型为：淘宝卖家、门店、服务商")]
		SendFailQn5LoginFailNeedToSelectSellerType = -3050,
		// Token: 0x04001189 RID: 4489
		[Description("登录时提示，您的账号还未进行实人认证，已被限制登录！完成认证后才能进入！")]
		SendFailQn5LoginAuthInterceptNeedToScanQr = -3051,
		// Token: 0x0400118A RID: 4490
		[Description("登录时需要滑动框验证")]
		SendFailQn5LoginFailNeedToSlide = -3052,
		// Token: 0x0400118B RID: 4491
		[Description("登录时被限制登录，需要联系官方客服")]
		SendFailQn5LoginFailNeedToConcatTb = -3053,
		// Token: 0x0400118C RID: 4492
		[Description("账号或者密码为空，请重新输入")]
		SendFailQn5LoginAccountOrPwdIsNull = -3059,
		// Token: 0x0400118D RID: 4493
		[Description("登录旺旺发现错误")]
		LoginAliwwFindError = -3060,
		// Token: 0x0400118E RID: 4494
		[Description("登录旺旺超时")]
		LoginAliwwTimeout = -3061,
		// Token: 0x0400118F RID: 4495
		[Description("重置旺旺登录表单失败")]
		ResetLoginFormFail = -3062,
		// Token: 0x04001190 RID: 4496
		[Description("密码错误")]
		PasswordError = -3063,
		// Token: 0x04001191 RID: 4497
		[Description("被限制登录")]
		LimitLogin = -3064,
		// Token: 0x04001192 RID: 4498
		[Description("已经登录(可能是旺旺bug导致的错误提示)")]
		WarnLogined = -3065,
		// Token: 0x04001193 RID: 4499
		[Description("绑定的手机号不是指定的手机号")]
		BindingIncorrectMobile = -3066,
		// Token: 0x04001194 RID: 4500
		[Description("查找不到登录窗口")]
		LoginWinNotFound = -3067,
		// Token: 0x04001195 RID: 4501
		[Description("启动旺旺超时")]
		StartWwTimeout = -3068,
		// Token: 0x04001196 RID: 4502
		[Description("获取手机验证码超时")]
		GetMobileCaptchaTimeout = -3069,
		// Token: 0x04001197 RID: 4503
		[Description("加载手机验证码窗口超时")]
		LoadMobileCaptchaWinTimeOut = -3070,
		// Token: 0x04001198 RID: 4504
		[Description("请求受限")]
		RequestLimit = -3071,
		// Token: 0x04001199 RID: 4505
		[Description("安全校验多次失败")]
		SecurityCheckFail = -3072,
		// Token: 0x0400119A RID: 4506
		[Description("安全校验出错")]
		SecurityCheckError = -3073,
		// Token: 0x0400119B RID: 4507
		[Description("仍然在登录界面处")]
		StillOnLoginWinArea = -3074,
		// Token: 0x0400119C RID: 4508
		[Description("登录千牛超时")]
		LoginQnTimeout = -3075,
		// Token: 0x0400119D RID: 4509
		[Description("启动千牛超时")]
		StartQnTimeout = -3076,
		// Token: 0x0400119E RID: 4510
		[Description("登录时提示千牛开小差，重启千牛")]
		LoginQnErrorNeedToRestart = -3077,
		// Token: 0x0400119F RID: 4511
		[Description("特价版千牛不能登录淘宝站点")]
		SpecialEditionLoginError = -3078,
		// Token: 0x040011A0 RID: 4512
		[Description("没有可用的验证方式，您可以联系主账号为您绑定手机号进行验证")]
		MobilePhoneAuthenticationIsNotBound = -3079,
		// Token: 0x040011A1 RID: 4513
		[Description("登录成功，但显示Nick是Undefined")]
		LoginAliwwSuccessButNickIsUndefined = -3080,
		// Token: 0x040011A2 RID: 4514
		[Description("自动登录时，找不到旺旺登录窗")]
		SendFailWwLoginWinNotFoundWhileAutoLogin = -3081,
		// Token: 0x040011A3 RID: 4515
		[Description("登录旺旺时，提示错误太多次")]
		SendFailWwLoginErrorComponentShowTimesTooMany = -3082,
		// Token: 0x040011A4 RID: 4516
		[Description("登录旺旺时，网络错误")]
		SendFailWwLoginErrorNetworkError = -3083,
		// Token: 0x040011A5 RID: 4517
		[Description("消息发送过程被停止发送")]
		MsgWasStoppedWhileSending = -3084,
		// Token: 0x040011A6 RID: 4518
		[Description("登录窗口没有响应")]
		LoginQnNotResponsing = -3085,
		// Token: 0x040011A7 RID: 4519
		[Description("需主号手机验证码，请本地登录一下代挂子号，验证后再重新激活")]
		LoginLimitNeedMasterAccountSmsCode = -3086,
		// Token: 0x040011A8 RID: 4520
		[Description("在登录中的界面上")]
		QnLogining = -3087,
		// Token: 0x040011A9 RID: 4521
		[Description("千牛登录窗口被关闭了")]
		QnLoginWinBeClosed = -3088,
		// Token: 0x040011AA RID: 4522
		[Description("千牛工作台被关闭了")]
		QnWorkbenchWinBeClosed = -3089,
		// Token: 0x040011AB RID: 4523
		[Description("千牛聊天窗口被关闭了")]
		QnAliTalkWinBeClosed = -3090,
		// Token: 0x040011AC RID: 4524
		[Description("登录失败，需要身份证验证")]
		NeedIdVerification = -3091,
		// Token: 0x040011AD RID: 4525
		[Description("需要发送短信验证")]
		NeedSendSmsToValidate = -3092,
		// Token: 0x040011AE RID: 4526
		[Description("呼叫聊天窗口超时")]
		CallTalkWinTimeOutInTalkWin = -3100,
		// Token: 0x040011AF RID: 4527
		[Description("在聊天窗口发送消息超时")]
		SendMsgTimeOutInTalkWin = -3200,
		// Token: 0x040011B0 RID: 4528
		[Description("分段消息太多导致消息发送超时")]
		SendMsgTimeOutInTalkWinOfTooManyMsg = -3210,
		// Token: 0x040011B1 RID: 4529
		[Description("登录错账号了")]
		LoginErrorAccount = -3500,
		// Token: 0x040011B2 RID: 4530
		[Description("登录成功后，获取旺旺工作台昵称失败，可能是旺旺工作台一直没有显示出来")]
		SendFailWwGetBenchWinAccountNull = -3510,
		// Token: 0x040011B3 RID: 4531
		[Description("未检测到自动发货插件")]
		AldsPlugNotFound = -3520,
		// Token: 0x040011B4 RID: 4532
		[Description("只是闲鱼用户，非淘宝用户，无法发送")]
		SendFailOnlyIdleBuyerNotTbBuyer = -3530,
		// Token: 0x040011B5 RID: 4533
		[Description("发消息时失败，消息包含团队管理禁用词")]
		SendFailTeamForbid = -4000,
		// Token: 0x040011B6 RID: 4534
		[Description("发消息时失败，买家已开启消息拒收，无法再发送消息，买家回复或下单后可继续发送")]
		SendFailBuyerRejectMessage = -4010,
		// Token: 0x040011B7 RID: 4535
		[Description("淘宝拦截，敷衍消息已拦截下发")]
		SendFailTbIntercept = -4020,
		// Token: 0x040011B8 RID: 4536
		[Description("由于您短时间内多次发送相同内容，容易给消费者造成困扰，最新的消息已被系统拦截，请重新组织话术")]
		SendSameMsgTooManySoInterceptByTb = -4030,
		// Token: 0x040011B9 RID: 4537
		[Description("您发送的消息中包含不安全的链接，消息发送失败")]
		SendFailContainsUnsafeLink = -4040,
		// Token: 0x040011BA RID: 4538
		[Description("登录成功后找到了工作台窗口，但未找到聊天窗口")]
		SendFailChatWinNotFoundAndBenchWinHasFound = -4080,
		// Token: 0x040011BB RID: 4539
		[Description("登录成功后找到了工作台窗口，但疑似崩溃了")]
		SendFailBenchWinHasFoundButCrash = -4081,
		// Token: 0x040011BC RID: 4540
		[Description("千牛或旺旺崩溃，发现错误报告程序窗口")]
		SendFailAliwwErrorReport = -4082,
		// Token: 0x040011BD RID: 4541
		[Description("发送时异常")]
		const_121 = -8000,
		// Token: 0x040011BE RID: 4542
		[Description("发送时异常，获取chorme的json串出错了")]
		SendFailQn5GetChormeJsonException = -8500,
		// Token: 0x040011BF RID: 4543
		[Description("找到聊天窗口，但未找到连接")]
		FindTalkWindowButRecentIsNull = -8600,
		// Token: 0x040011C0 RID: 4544
		[Description("账号为空")]
		AccountIsNull = -9000,
		// Token: 0x040011C1 RID: 4545
		[Description("千牛协议呼叫失败多次，千牛可能已经掉线了")]
		MaybeQnOffline = -9500,
		// Token: 0x040011C2 RID: 4546
		[Description("千牛与服务器连接中断")]
		QnLinkageInterrupt = -9510,
		// Token: 0x040011C3 RID: 4547
		[Description("千牛没有响应")]
		QnNoResponse = -9520,
		// Token: 0x040011C4 RID: 4548
		[Description("获取买家昵称失败")]
		GetBuyerNickFail = -9600,
		// Token: 0x040011C5 RID: 4549
		[Description("获取代挂表情出错了")]
		GetAgentEmotionsFail = -9700
	}
}
