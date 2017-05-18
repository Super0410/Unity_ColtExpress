# Unity_ColtExpress
帮朋友写的课设，仿制柯尔特列车

1、Start Quit
	Start：
		player：
			playerCount playerName playerCharacter
		Scene：
			出牌数 明暗明明

	GamePlay：
		1、 洗牌
		2、 出牌
		3、 结算

2、BasicClasses
	车厢：
		上、车厢
		所有地图五个车厢

	Card：
		Type：9
			上下左右
			警长（结算时出牌人选择左右）
			捡钱（谁先出谁先选）
			打拳（同上下、掉钱包）
			打枪
				下方左右1车厢 上方所有人
				若左右车厢空 则空枪 废牌
				若有人 则选人 必中
			子弹 （开枪后从牌库取子弹牌 至无效牌 给被击中者）

	Character： 
		AllCharacter：
			Properties
				Health 3
				钱袋：300

			Card：
				子弹  6

		警长
			1、同车厢得到子弹牌
			2、同车厢到车顶

	可拾取品：
		钱袋：
		手提包：
			车头 和警长在一起 钱多
		Demond

3、GamePlay
	随机n场景：
		每个场景一回合
		每回合6张牌重新洗牌
