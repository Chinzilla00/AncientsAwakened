using AAMod.Backgrounds;
using AAMod.Globals;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Utilities;
using Terraria.Localization;

namespace AAMod
{
    public class Lang
    {
        public static string Worldtext(String WorldInfo)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(WorldInfo)
                    {
                        case "YttriumInfo":
                        return "你的世界获得了钇土!";
                        case "UraniumInfo":
                        return "你的世界获得了铀绿!";
                        case "TechneciumInfo":
                        return "你的世界获得了闪锝!";
                        case "downedEquinoxInfo":
                        return "上神之礼在天空中闪耀...";
                        case "downedMoonlordInfo1":
                        return "真正的远古觉醒!";
                        case "downedMoonlordInfo2":
                        return "月球领主的精华在洞穴中闪耀...";
                        case "downedMechBossAnyInfo":
                        return "洞穴中有光闪过...";
                        case "downedSistersInfo":
                        return "混沌能量从地下最深出开始涌出. ";
                        case "downedBoss2Info":
                        return "打败强大的恶魔之后你听见泰拉心球中的靡靡之音...";
                        case "downedBoss3Info1":
                        return "远古之骨已经因能量而爆裂!";
                        case "downedBoss3Info2":
                        return "沙漠之风开始酝酿...";
                        case "downedBoss3Info3":
                        return "凛冬之山隆隆作响...";
                        case "downedPlantBossInfo1":
                        return "泰拉心球中的和谐之音回响";
                        case "downedPlantBossInfo2":
                        return "虚空枯萎机器重新运转";
                        case "downedPlantBossInfo3":
                        return "地下的恶魔开始密谋";
                        case "downedPlantBossInfo4":
                        return "嘿, 小子, 是我, 阿努比斯. 回城找我帮我个忙, 我想和你说点事.";
                        case "downedStormAnyInfo":
                        return "山洞里一道霹雳轰鸣...";
                        case "hardModeInfo":
                        return "愤怒和怨恨的灵魂被释放了!";
                        case "downedAllAncientsInfo":
                        return "真正的冥昧之息在大气中翻涌...";
                    }
                }
            else
                {
                    switch(WorldInfo)
                    {
                        case "YttriumInfo":
                        return "Your world bursts with Yttrium!";
                        case "UraniumInfo":
                        return "Your world bursts with Uranium!";
                        case "TechneciumInfo":
                        return "Your world bursts with Technecium!";
                        case "downedEquinoxInfo":
                        return "The gift of the celestials sparkle in the atmosphere...";
                        case "downedMoonlordInfo1":
                        return "The Ancients have Awakened!";
                        case "downedMoonlordInfo2":
                        return "The Essence of the Moon Lord sparkles in the caves below...";
                        case "downedMechBossAnyInfo":
                        return "The caves shine with light for a brief moment...";
                        case "downedSistersInfo":
                        return "Chaotic energy grows in the deepest parts of the world.";
                        case "downedBoss2Info":
                        return "You hear a hum of harmony from the Terrarium after the defeat of a great evil...";
                        case "downedBoss3Info1":
                        return "Bones of the ancient past burst with energy!";
                        case "downedBoss3Info2":
                        return "The desert winds stir...";
                        case "downedBoss3Info3":
                        return "The winter hills rumble...";
                        case "downedPlantBossInfo1":
                        return "The choirs of unity hum from the terrarium.";
                        case "downedPlantBossInfo2":
                        return "The withered machines of the emptiness reactivate.";
                        case "downedPlantBossInfo3":
                        return "Devils in the underworld begin to plot.";
                        case "downedPlantBossInfo4":
                        return "Hey kid, it's me, Anubis. Do me a favor and meet me back in town, I wanna talk to ya about somethin'.";
                        case "downedStormAnyInfo":
                        return "The clap of a thunderbolt roars in the caverns...";
                        case "hardModeInfo":
                        return "The Souls of Fury and Wrath are unleashed upon the world!";
                        case "downedAllAncientsInfo":
                        return "Chaos begins to stir in the atmosphere...";
                    }
                }
            return "";
        }
        public static string AAPlayerChat(String PlayerInfo)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(PlayerInfo)
                    {
                        case "UnstableSoulInfo":
                        return "你的灵魂激荡着...";
                        case "InfinityGauntletInfo":
                        return "完美的平衡, 万物本该如此...";
                        case "WorldgenReminderInfo1":
                        return "嘿, 呃…小子？ 如果我哪里错了, 请纠正我, 但我认为你的世界不是由远古觉醒的内容而生成的。 如果我是你, 我会完成一个新的世界。 ";
                        case "WorldgenReminderInfo2":
                        return "你这个笨蛋！你没有生成远古觉醒的内容！快创造一个新世界！呃啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊";
                        case "WorldgenReminderInfo3":
                        return "错误。 世界并没有包含aamod. tmod的内容。 请创造一个新世界。 ";
                        case "WorldgenReminderInfo4":
                        return "嘿！你没有在这个世界上创造出远古觉醒的东西！在我把你炸到火星之前赶紧创造一个新世界！";
                        case "WorldgenReminderInfo5":
                        return "嘿, 呃……这个世界里我看不到任何远古觉醒的内容。 聪明点赶紧创一个新世界。 ";
                        case "WorldgenReminderInfo6":
                        return "嘿。 说你呢。 另一个次元的人。 你可能在下载了mod之后忘记了创造一个新的世界。 如果你想要所有的mod的内容, 创造一个新的世界。 ";
                        case "WorldgenReminderInfo7":
                        return "创造……新世界……否则疯狂的蘑菇人……会压扁……你这个小泰拉人……";
                        case "WorldgenReminderInfo8":
                        return "…凡人。 如果我没有老眼昏花的话, 你的世界就没有远古觉醒的内容。 创造一个新的世界最吼的。 ";
                    }
                }
            else
                {
                    switch(PlayerInfo)
                    {
                        case "UnstableSoulInfo":
                        return "Your soul ripples...";
                        case "InfinityGauntletInfo":
                        return "Perfectly Balanced, as all things should be...";
                        case "WorldgenReminderInfo1":
                        return "Hey uh...kid? Correct me if I'm wrong, but I think your world didn't generate with Ancients Awakened stuff in it. I'd make a new one if I were you.";
                        case "WorldgenReminderInfo2":
                        return "YOU IMBECILE! YOU DIDN'T GENERATE ANCIENTS AWAKENED CONTENT! MAKE A NEW WORLD NOW! REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE";
                        case "WorldgenReminderInfo3":
                        return "ERR0R. W0RLD D0ES N0T APPEAR T0 C0NTAIN AAM0D.TM0D C0NTENT. PLEASE GENERATE A NEW W0RLD.";
                        case "WorldgenReminderInfo4":
                        return "HEY! You didn't generate Ancients Awakened stuff in this world! Generate a new world before I blast you to mars!";
                        case "WorldgenReminderInfo5":
                        return "Hey, uh...I don't see any Ancients Awakened content in this world. Might be smart to make a new world or whatever...";
                        case "WorldgenReminderInfo6":
                        return "Hey. You. Interdimensional being. You might have forgotten to make a new world after downloading the mod. Make a new world if you want all the mod's content.";
                        case "WorldgenReminderInfo7":
                        return "Make...new world....or mushmad...will squish...little terrarian...";
                        case "WorldgenReminderInfo8":
                        return "...Mortal. Your world doesn't have Ancients Awakened content if my old eyes do not deceive me. Generating a new world would be optimal.";
                    }
                }
            return"";
        }
        public static string Newtext(String Newtext)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                }
            else
                {
                }
            return"";
        }
        public static string GlobalNPCSInfo(String NPCsummon)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(NPCsummon)
                    {
                        case "NPCarrive":
                        return " 到了!";
                    }
                }
            else
                {
                    switch(NPCsummon)
                    {
                        case "NPCarrive":
                        return " have awoken!";
                    }
                }
            return"";
        }
        public static string TownNPCStanLee(String StanLee)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(StanLee)
                    {
                        case "StanLeeName":
                        return "插画绘师";
                        case "StanLeeChat1":
                        return "你知道, 我想一个人可以有所作为. -牛津纳菲尔德学院如是说";
                        case "StanLeeChat2":
                        return "我觉得你高了";
                        case "StanLeeChat3":
                        return "你看见我的鞋了吗? ";
                        case "StanLeeChat4":
                        return "曾经我也是保安， 但是， 呃……后来我被解雇了……";
                        case "StanLeeChat5":
                        return "精益求精. ";
                        case "StanLeeChat6":
                        return "嘿， 如果你有报纸的话， 我可以借体育栏目看看吗? ";
                        case "StanLeeChat7":
                        return "我记得当我是一个邮车司机的时候， 我要送一些邮件给名人. 我想， 他叫托尼…史塔克? ";
                        case "StanLeeChat8":
                        return "哈……!太搞笑了!";
                        case "StanLeeChat9":
                        return "喔!很合身";
                        case "StanLeeChat10":
                        return "别逼我用鞭子抽你， 你这个小混蛋. ";
                        case "StanLeeChat11":
                        return "你有毛病吗， 你以前从没见过宇宙飞船? ";
                    }
                }
            else
                {
                    switch(StanLee)
                    {
                        case "StanLeeName":
                        return "Illustrator";
                        case "StanLeeChat1":
                        return "You know, I guess one person can make a difference. 'Nuff said.";
                        case "StanLeeChat2":
                        return "I thought you'd be taller.";
                        case "StanLeeChat3":
                        return "Have you seen my shoe?";
                        case "StanLeeChat4":
                        return "And then there was this one time I was a security guard, but uh...then I got fired...";
                        case "StanLeeChat5":
                        return "Excelsior.";
                        case "StanLeeChat6":
                        return "Hey, if you got a newspaper, could I borrow the sports section?";
                        case "StanLeeChat7":
                        return "I remember when I was a mail truck driver, got to deliver some mail to some famous guy. I think his name was Tony...Stank?";
                        case "StanLeeChat8":
                        return "HAH..! THAT'S HILARIOUS!";
                        case "StanLeeChat9":
                        return "Wow, nice suit.";
                        case "StanLeeChat10":
                        return "Don't make me whip ya, you little punk.";
                        case "StanLeeChat11":
                        return "Whats'a matter with you, ya never seen a spaceship before?";
                    }
                }
            return"";
        }
        public static string TownNPCAnubis(String Anubis)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(Anubis)
                    {
                        case "AnubisName":
                        return "传说书记员";
                        case "SetChatButtons1":
                        return "切换信息";
                        case "SetChatButtons2":
                        return "我要做什么? ";
                        case "SetChatButtons3":
                        return "蘑菇. 我是认真的. ";
                        case "SetChatButtons4":
                        return "更多的蘑菇. ";
                        case "SetChatButtons5":
                        return "淡定";
                        case "SetChatButtons6":
                        return "死胖子龙";
                        case "SetChatButtons7":
                        return "三头怪胎";
                        case "SetChatButtons8":
                        return "三个愿望";
                        case "SetChatButtons9":
                        return "蛇. 怎么总是蛇? ";
                        case "SetChatButtons10":
                        return "了解";
                        case "SetChatButtons11":
                        return "机械质料";
                        case "SetChatButtons12":
                        return "两个头;没脑子";
                        case "SetChatButtons13":
                        return "更多蠕虫boss, 该死";
                        case "SetChatButtons14":
                        return "远古裁决";
                        case "SetChatButtons15":
                        return "麻烦双胞胎";
                        case "SetChatButtons16":
                        return "远古之怒";
                        case "SetChatButtons17":
                        return "远古之怨";
                        case "SetChatButtons18":
                        return "远古劫难";
                        case "SetChatButtons19":
                        return "冥昧末日预言者";
                        case "SetChatButtons20":
                        return "宝石持有者";
                        case "downedMonarchY":
                        return "…就这样? ";
                        case "downedMonarchN":
                        return "嘿, 你知道这些到处都是的小蘑菇吗？ 我听说, 如果你把他们放在一块, 挥一下, 他们的国王或什么东西就会来试图把你赶走。 我很想看一看。 ";
                        case "downedFungusY":
                        return "干得好。 老实说, 你现在有多高？ ";
                        case "downedFungusN":
                        return "绚烂的蘑菇洞总让我觉得有点毛骨悚然。 无论如何, 你想要更好的魔法能力？ 有一个巨大的蘑菇怪物, 身体内充满了强大的魔法能力。 只不过你在它下面的时候需要堵住你的鼻子。 ";
                        case "downedGripsY":
                        return "把那些大爪子打下来很不错。 也许小东西们能让我好好歇一歇。 ";
                        case "downedGripsN":
                        return "那些飞着的爪子在晚上简直就是一场让我害怕的噩梦。 我旅行中, 遇到过两个大大的。 也许如果你杀了他们, 小东西们就会被赶走。 也许你杀一点小爪子, 就能显示召唤出他们的方法。 ";
                        case "downedBroodY":
                        return "那是我有生以来见过的最胖的龙。 ";
                        case "downedBroodN":
                        return "喜欢龙吗？ 不？ 那可不好。 想要更好的装备？ 你得去杀死这条熔岩 “巨”龙。 她又大又吓人, 太太太太太太太胖了飞不高, 但她还是会飞。 ";
                        case "downedHydraY":
                        return "总算解脱了。 那只九头渊蛇似乎并不会善罢甘休。 至少她女儿更成熟点……嗯？ 谁？ 我以后再解释, 总之干得好。 ";
                        case "downedHydraN":
                        return "潭渊一直是所有令人作呕的蜥蜴聚集地。 不过那有个相当大的, 三个头。 她总是很不高兴, 每次我想进她的窝, 她都想吃掉我！";
                        case "downedDjinnY":
                        return "哈！你这个沙子废物, 看看现在谁强！";
                        case "downedDjinnN":
                        return "那 个 狗 娘 养--哦, 嗨。 对不起, 我只是有点生气, 因为我和沙漠巨灵发生了一场小争执。 你能去教教那个会魔法的傻瓜和他的蠢货打手们别对着我伸肌肉了吗？ ";
                        case "downedSerpentY":
                        return "希望你没有被“冻伤”！*砰-咚-锵*（译者注：打击乐中用来活跃气氛的节拍伴奏）...我知道那得一瘸一拐。 ";
                        case "downedSerpentN":
                        return "蛇, 为什么总是蛇？ 我讨厌他们！不管怎样, 最近在苔原上, 有这么多雪蛇还真是不让我孤单。 你能当一次除害人, 看看他们在干什么吗？ ";
                        case "downedRetrieverY":
                        return "你有没有机会找回我第三版的《神奇狗阿努比斯的生命传奇历险记》？ ";
                        case "downedRetrieverN":
                        return "还记得混乱之爪？ 那些恶心的脏手？ 现在有个机器版的还老偷我东西。 你能帮我个忙, 教训一下它什么的吗？ ";
                        case "downedRaiderY":
                        return "所以它只是个大机器人？ 好吧, 有时候只是人们想多了。 那东西确实和育母炎龙一样胖。 ";
                        case "downedRaiderN":
                        return "人们说有一天晚上他们看到了奇怪的阴影。 它太大了甚至还把月亮遮住了一会儿。 你应该发现这个秘密并且做点什么。 ";
                        case "downedOrthrusY":
                        return "我猜双头狗现在正在吃自己。 ";
                        case "downedOrthrusN":
                        return "记得九头渊蛇？ 外面有一个更大的。 是个机器人。 能发闪电。 所以呃…祝你好运！";
                        case "downedEquinoxY":
                        return "除掉昼夜虫干得不错。 不过我可以告诉你现在好像过了一个星期。 希望我没有错过护士的预约…";
                        case "downedEquinoxN":
                        return "喜欢虫子？ 我也不喜欢, 你猜怎么着？ 有两个虫子控制日夜的流动, 而且很强力。 祝你好运。 ";
                        case "AnubisB":
                        return "我听说有个万人迷家伙帅的一批, 而且所有的女人都爱他, 因为他有惊人的灵魂判断力。 好家伙！";
                        case "downedSistersY":
                        return "漂亮！你给那俩宠坏的小孩好好上了一课！那俩没看见这个！";
                        case "downedSistersN":
                        return "还记得育母炎龙和九头渊蛇？ 好, 那俩有女儿。 而且她们讨厌“男 人”。 。 ！每次我去混沌之地的时候, 这俩都会等在那毁了我的一天！你去给她们俩点教训？ ";
                        case "downedAkumaY":
                        return "邪鬼巨龙觉得他很激昂。 对我来说, 他只是过于想冷静然后失败了。 不管怎么说, 想要给你头发上倒点水, 你头上有点烧焦了。 ";
                        case "downedAkumaN":
                        return "为什么有人会把一只太阳蛇称为恶魔？ 我个人不知道...不过邪鬼巨龙必须得走。 他老用火焰一样的呼吸觊觎我的沙漠, 这很让我恼火。 ";
                        case "downedYamataY":
                        return "感谢你让那个七头娘娘腔闭嘴。 他让我想把我的毛撕下来。 ";
                        case "downedYamataN":
                        return "八歧大蛇, 牢骚鬼！他什么都抱怨, 而且 不 会 闭 嘴！说 真 的, 你可以去试试对付七个不停互相交谈的吵闹的头。 ";
                        case "downedZeroY":
                        return "…老实说。 我不喜欢那东西死后说的话。 ";
                        case "downedZeroN":
                        return "你知道虚空吗？ 东边那些恐怖的浮岛？ 那里有一个 大 的可怕的机器总是漂浮在那。 不论怎样, 你击杀月球领主之后, 我听到一股巨大的冲击波从虚空涌出, 你能帮我查一查吗？ ";
                        case "downedShenY":
                        return "天哪——我知道你有这种感觉, 伙计！太棒了！尽管……你打他时他看起来很生气……几乎和他被打时一样——呃, 别再生气了。  ";
                        case "downedShenN":
                        return "邪鬼巨龙和八歧大蛇...你知道吗, 他们两个曾经是一个生物。 很糟糕的是, 它很强。 他曾经一度把两个文明世界合二为一。 不说了, 你需要啥？ ";
                        case "Stones":
                        return "你知道……在你与上神应龙交战之后, 我感觉到一些……非常古老的魔法在激活。 也许你应该去拜访一下你遇到的那些强硬的BOSS？ 顺便问一下, 你最近见过哥布林召唤师吗？ 天哪, 她变得很强。 不知道她会掉落什么东西。 ";
                        case "else":
                        return "我不知道？ 你可以随便问我你目前能打的任何强力生物。  ";
                        case "AkumaGuideChat":
                        return "如果我是你, 我会在白天离潭渊远点. 雾大的要死, 你都看不见什么鬼东西蹲在里面. 如果我记得没错的话, 你可以带一个用龙爪做的灯笼, 就能看的清楚了. ";
                        case "YamataGuideChat":
                        return "因为某种原因, 燎狱里的火山在晚上活动频繁. 白天就会比较安静. 如果你能用那些九头蛇之爪做个掩护物品, 也许你能走过去？ 他们似乎不受那灰烬的影响…";
                        case "JungleGuideChat":
                        return "我注意到丛林的生物会掉一些奇怪的植物。 它们硬得像钉子， 也许可以用它们来打造装备。 ";
                        case "BroodMotherGuideChat":
                        return "你知道燎狱火山底部的那些蛋吗？ 是啊， 如果我是你， 我可不会碰那些， 除非你想对付一条非常愤怒的龙。 ";
                        case "HydraGuideChat":
                        return "在潭渊的湖底有一些……的蒴荚。 我真的不想知道那个九头蛇怪吃什么。 我也不想破坏它们， 那只蜥蜴可能会因为食物不好而变得暴躁。 ";
                        case "VoidGuideChat":
                        return "那些东边的浮岛把我吓坏了。 他们只是……在那里。 我想他们上面有些小隔间， 但我不会去那里。 ";
                        case "HardModeGuideChat1":
                        return "嘿， 如果你去地下混沌之地的话， 我想你可以得到一些魂， 就像你在邪恶环境中得到的那样。 这些应该有用。 ";
                        case "HardModeGuideChat2":
                        return "嘿， 你知道潭渊里的蟾蜍吗？ 我听说他们囤积了很多硬币。 快去打那些恶心的怪物， 然后砰的一声！都是现金。 ";
                        case "PlantBossGuideChat":
                        return "你听到泰拉心球里传来的音乐了吗？ 我觉得下面有些新东西。 看看你能不能弄到他们持有的绿色棱镜。 我想你可以用它建造一个牛逼的制作台。 ";
                        case "EquinoxBossGuideChat":
                        return "你知道天空中那些发光的球吗？ 如果我没记错， 它们会随时间而变化。 如果你一天不同的时间去， 也许你能得到不同的东西？ ";
                        case "AnubisChat1":
                        return "你不会恰巧擅长揉肚子吧? ";
                        case "AnubisChat2":
                        return "你知道去海滩看到沙滩上有沙子多可怕吗? 想象一下一直都有. 这就是我的生活. ";
                        case "AnubisChat3":
                        return "第一千次, 别 来 摸 我 的 毛!";
                        case "AnubisChat4":
                        return "嘿, 我背后很痒. 能(嘶)帮我挠挠吗? ";
                        case "AnubisChat5":
                        return "别, 我不带跳骚项圈. \n \n*挠 挠*";
                        case "AnubisChat6":
                        return "每个人都问我谁是一个好孩子, 但我很沮丧, 因为他们从来没有告诉我什么是好孩子. ";
                        case "AnubisChat7":
                        return "你见过我的尾巴吗? 我需要好好教教它. ";
                        case "AnubisChat8":
                        return "沙漠巨灵被打败了但是它没从我这得到任何东西! 核实一下!";
                        case "AnubisChat9":
                        return "顺便感谢你让我在这落脚. 在沙漠里走了几千年的滋味真不好受. ";
                        case "AnubisChat10":
                        return "对了, 我写了泰拉史记. 但是我也写了另外一本伟大的书, 《阿努比斯生命史诗历险记》! 要看看? ";
                        case "AnubisChat11":
                        return "你不觉得讨厌吗, 当";
                        case "AnubisChat12":
                        return "红色的血垃圾";
                        case "AnubisChat13":
                        return "紫色的脏东西";
                        case "AnubisChat14":
                        return "占领了你的世界的时候? 想想就真tm恶心. ";
                        case "AnubisChat15":
                        return "我最讨厌什么生物? 很简单, 史莱姆之王. 如果它刚好掉在你身上, 祝你能不用喷灯把你身上的凝胶洗掉. ";
                        case "AnubisChat16":
                        return "天哪, 我之前试过和他们搭讪, ";
                        case "AnubisChat17":
                        return "不想让我加入他们的聊天. 我想知道, 这些女人和血月是怎么回事? ";
                        case "AnubisChat18":
                        return "我试着和";
                        case "AnubisChat19":
                        return "搭讪, 她对我很有礼貌. 很...奇怪...尤其是在血月的时候. ";
                        case "AnubisChat20":
                        return "嗨, 不错的衣服. ";
                        case "AnubisChat21":
                        return "嗨, 喜欢我飘起来的派对帽吗? 魔法真好玩. ";
                        case "AnubisChat22":
                        return "你这些泰拉瑞亚的朋友很喜欢迪斯科吗? 我会跳布吉舞. ";
                        case "AnubisChat23":
                        return "嘿嗯...如果有人问, ";
                        case "AnubisChat24":
                        return "是从我这获得他的所有信息, 懂? ";
                        case "AnubisChat25":
                        return "那个";
                        case "AnubisChat26":
                        return "太可怕了. 知道很多boss的消息...实际上, 我只希望他不会跟踪我或是怎么样. . ";
                        case "AnubisChat27":
                        return "我认为, ";
                        case "AnubisChat28":
                        return "不会让人知道他到底有多聪明. 我是说, 看看那张脸. 又纯真, 又原始, 又天才. ";
                        case "AnubisChat29":
                        return "一直对我大喊大叫. 只是因为我把他做的鞋子都吃光了. 这不是我的错, 谁让他用上好的皮革做的. ";
                        case "AnubisChat30":
                        return "别问";
                        case "AnubisChat31":
                        return ", 他的商品哪来的. 很讨厌. ";
                    }
                }
            else
                {
                    switch(Anubis)
                    {
                        case "AnubisName":
                        return "Legendscribe";
                        case "SetChatButtons1":
                        return "Switch Info";
                        case "SetChatButtons2":
                        return "What Do I do now?";
                        case "SetChatButtons3":
                        return "Mushrooms. I'm serious.";
                        case "SetChatButtons4":
                        return "More Mushrooms.";
                        case "SetChatButtons5":
                        return "Get a Grip";
                        case "SetChatButtons6":
                        return "Deadweight Dragon";
                        case "SetChatButtons7":
                        return "Three-Headed Freak";
                        case "SetChatButtons8":
                        return "3 Wishes";
                        case "SetChatButtons9":
                        return "Snakes. Why is it always snakes?";
                        case "SetChatButtons10":
                        return "Gotcha'";
                        case "SetChatButtons11":
                        return "Mechanical Mass";
                        case "SetChatButtons12":
                        return "Two heads; zero brains";
                        case "SetChatButtons13":
                        return "More worm bosses god dammit.";
                        case "SetChatButtons14":
                        return "Ancient of Judgement";
                        case "SetChatButtons15":
                        return "Terrible Twins";
                        case "SetChatButtons16":
                        return "Ancient of Fury";
                        case "SetChatButtons17":
                        return "Ancient of Wrath";
                        case "SetChatButtons18":
                        return "Ancient of Doom";
                        case "SetChatButtons19":
                        return "Discordian Doomsayer";
                        case "SetChatButtons20":
                        return "The Stonekeepers";
                        case "downedMonarchY":
                        return "...that was it?";
                        case "downedMonarchN":
                        return "Hey, you know all these little red mushrooms growing everywhere? I hear if you squish a bunch of them together and wave them around, their king or something will come and attempt to run you down. I gotta see that.";
                        case "downedFungusY":
                        return "Nice work. Now be honest, how high are you right now?";
                        case "downedFungusN":
                        return "The glowing mushroom caves always make me feel loopy for some reason. Anyways, you want better magic abilities? There's a big mushroom monster that has some great magic abilities infused into it. Just plug your nose while your down there.";
                        case "downedGripsY":
                        return "Nice job taking down those giant hands. Maybe the little ones will finally leave me alone for once.";
                        case "downedGripsN":
                        return "Those flying claws at night are a nightmare to deal with, and they freak me out. In my travels, I've come across these two REEEEEEEEALLY big ones. Maybe if you kill them, the little ones will bugger off. Maybe killing a few of them and showing that you have in some way will call them down.";
                        case "downedBroodY":
                        return "That was the fattest dragon I've ever seen in my life.";
                        case "downedBroodN":
                        return "Like dragons? No? Too bad. You want better gear? You gotta go kill of this HUGE dragon made of lava. She's big, scary, and WAAAAAAAAAAAY too fat to fly, but she does anyways.";
                        case "downedHydraY":
                        return "Good riddance. That hydra can't seem to lay off. At least her daughter is a bit more mellow...huh? Who? I'll explain later, good job.";
                        case "downedHydraN":
                        return "The Mire has always been a gathering spot for all the nastiest lizards, but there's a really big one there, and it's got 3 heads. She's really grouchy all the time, and any time I try to go into her den, she tries to EAT me!";
                        case "downedDjinnY":
                        return "Hah! Who's tough now you sandy sadsack!";
                        case "downedDjinnN":
                        return "THAT SON OF A-- Oh hi. Sorry, I was just a bit angry about a little tussle I had with desert djinn. That magical meathead and his goons to stop flexing their muscles on me. Could you go teach em' a thing or two?";
                        case "downedSerpentY":
                        return "Hope you didn't get any 'FROSTBITES'! *buh-dum-tish* ...yeah I know that was lame.";
                        case "downedSerpentN":
                        return "Snakes, why does it always have to be snakes? I hate 'em! Whatever, in the tundra recently, there have been these snow snerpents that won't leave me alone. Could ya play exterminator and find out what they're doing?";
                        case "downedRetrieverY":
                        return "Did you get my 3rd edition of 'The Life and Epic Adventures of Anubis the Wonder Dog!' back by any chance?";
                        case "downedRetrieverN":
                        return "Remember the Grips of Chaos? Those nasty grabby hands? There's a robotic one and it keeps stealing my stuff. Can you do me a favor and go throw a wrench at it or something?";
                        case "downedRaiderY":
                        return "So it was a giant robot after all? Well, sometimes people just overthinking the problem. That thing was almost as fat as the Broodmother.";
                        case "downedRaiderN":
                        return "People said that they saw strange shade in one of the nights. It was so big that it even closed the moon for a moment. You should discover this mystery and do something.";
                        case "downedOrthrusY":
                        return "I guess orthrus is what it eats, now.";
                        case "downedOrthrusN":
                        return "Remeber the Hydra? There's a bigger one out there. And it's a robot. And it uh...shoots lightning. So uh...good luck!";
                        case "downedEquinoxY":
                        return "Nice job taking out the Equinox worms. I could tell you did because it's like a week later now. I hope I didn't miss my nurse's appointment...";
                        case "downedEquinoxN":
                        return "Like worms? Me neither, but guess what? There are 2 big ones that control the flow of day and night, and they're tough buggers. Good luck.";
                        case "AnubisB":
                        return "I hear there’s this lorekeeper guy that’s really jacked and handsome, and all the ladies love him for his amazing soul-judging abilities. What a guy.";
                        case "downedSistersY":
                        return "Nice, you taught those two spoiled brats a lesson! Those two didn't see it coming!";
                        case "downedSistersN":
                        return "Remember ol' Brood and Hydra? Well, those two have daughters. And MAN they're annoying..! Every time I go into the chaos biomes, those two are just waiting to ruin my day! Can you go give em' the ol' one-two?";
                        case "downedAkumaY":
                        return "Akuma thinks he's edgy. To me, he just comes across as trying to be way too cool and failing. Anyways, might wanna run some water through your hair. You got a little singed up there.";
                        case "downedAkumaN":
                        return "Why would anyone call a sun serpent a demon? I have no idea personally...but Akuma has got to go. He always glasses my deserts with his flame breath and it pisses me off.";
                        case "downedYamataY":
                        return "Thanks for shutting up that 7-headed sissy. He makes me want to tear my fur out.";
                        case "downedYamataN":
                        return "Yamata, the whiny nit! He complains about everything, and he WONT SHUT UP! LIKE SERIOUSLY, YOU try and deal with seven obnoxiously loud dragon heads that chatter constantly and talk over eachother!";
                        case "downedZeroY":
                        return "...I'll be honest. I don't like what that thing said after it died one bit.";
                        case "downedZeroN":
                        return "You know the void? Those spooky floating islands to the east? There's a BIG scary machine there that's always just floating there. Anyways, after you slammed the moon lord, I heard a massive shockwave come from the void. Could you check it out for me?";
                        case "downedShenY":
                        return "Holy-- I knew you had it in you, man! Awesome job! Although...he seemed pretty angry when you beat him...almost as angry as when he got beat by-- er, nevermind that.";
                        case "downedShenN":
                        return "Akuma and Yamata...you know, those two were once one being. And hot dang, that guy was powerful. He leveled 2 civilizations one time. Anyways, so what was it that you needed?";
                        case "Stones":
                        return "You know...after you whooped ol' Shen, I felt some...very old magic activate. Maybe you should pay a visit to some of the tougher bosses you've come across? By the way, have you seen the Goblin Summoner recently? Jeeze she got tough. Wonder what her deal is.";
                        case "else":
                        return "I dunno? Ask me about any strong creatures you can fight currently.";
                        case "AkumaGuideChat":
                        return "If I were you, I'd stay out of the Mire during the day. It gets really foggy and you can't see jack squat in there. I think you can make a lantern out of blaze claws if I remember correctly to help see through it...";
                        case "YamataGuideChat":
                        return "The Volcano in the inferno gets pretty active at night for some reason. During the day, it seems to calm down. Maybe you could get through if you made some kind of cover out of those Hydra claws? They seem to not be affected by that ash...";
                        case "JungleGuideChat":
                        return "Creatures in the jungle drop these really weird leaves I've noticed. They're tough as nails, though, so making gear out of it could be a pretty interesting idea...";
                        case "BroodMotherGuideChat":
                        return "You know those eggs at the bottom of the inferno volcano? Yeah uh, I wouldn't touch those if I were you, unless you want to deal with a very angry dragon.";
                        case "HydraGuideChat":
                        return "Underneath the lake in the mire are some pods of...something. I don't really want to know what that Hydra thing eats. I wouldn't break them either, that lizard gets feisty when something messes with her food.";
                        case "VoidGuideChat":
                        return "Those floating islands to the east freak me out. They're just...there. I think there's some houses on them, but I'm not going up there.";
                        case "HardModeGuideChat1":
                        return "Hey, if you go to the underground chaos biomes, I think you can get some souls like the ones you get in the evil biomes. Those should be useful.";
                        case "HardModeGuideChat2":
                        return "Hey you know those frogs in the mire? I hear they hoard tons of coins. Just go beat the bajeezus out of them and BAM! Cash money.";
                        case "PlantBossGuideChat":
                        return "Did you hear that music coming from the Terrarium? I think there's something new down there. See if you can get your hands on one of those green prisms they have. I think you can make a crazy new crafting station with it.";
                        case "EquinoxBossGuideChat":
                        return "You know those glowing spheres in the sky? If I remember correctly, they change depending on the time of day. Maybe you can get something different from them if you go at different points in the day?";
                        case "AnubisChat1":
                        return "You wouldn’t happen to be good at belly rubs would you?";
                        case "AnubisChat2":
                        return "You know that awful feeling of getting sand in your swim trunks after going to the beach? Imagine having that all the time. Welcome to my life.";
                        case "AnubisChat3":
                        return "For the thousandth time, I AM NOT A FURRY!";
                        case "AnubisChat4":
                        return "Hey, I got this really bad itch on my back. Could ya get it for me?";
                        case "AnubisChat5":
                        return @"No. I won’t wear a flea collar.
 
*Scratch Scratch*";
                        case "AnubisChat6":
                        return "Everyone asks me who's a good boy, but I'm upset because they never tell me who it is.";
                        case "AnubisChat7":
                        return "Have you seen my tail? I need to teach it a thing or two.";
                        case "AnubisChat8":
                        return "The Desert Djinn may be ripped but he's got nothing on me! Check it!";
                        case "AnubisChat9":
                        return "Thanks for letting me crash here by the way. Walking around the desert for a couple thousand years really tuckers ya out.";
                        case "AnubisChat10":
                        return "I wrote the Terraria Historia, yes. But I also wrote another great book. 'The Life and Epic Adventures of Anubis the Wonder Dog!' Want a copy?";
                        case "AnubisChat11":
                        return "Don't you hate it when ";
                        case "AnubisChat12":
                        return "red fleshy crap";
                        case "AnubisChat13":
                        return "purple muggy crap";
                        case "AnubisChat14":
                        return " takes over your biome? it's disgusting.";
                        case "AnubisChat15":
                        return "What creature do I hate most? Oh that's easy, King Slime. If that thing lands on you, good luck washing the slime out of your clothes or fur without a blowtorch.";
                        case "AnubisChat16":
                        return "Geeze, I tried hitting on ";
                        case "AnubisChat17":
                        return " earlier and they kicked the bajeezus out of me. What is it with these ladies and blood moons?";
                        case "AnubisChat18":
                        return "I tried hitting on ";
                        case "AnubisChat19":
                        return " and she was totally polite to me. That's...odd...especially during a blood moon.";
                        case "AnubisChat20":
                        return "Hey, nice outfit.";
                        case "AnubisChat21":
                        return "Hey, like my floating party hat? Magic is fun.";
                        case "AnubisChat22":
                        return "Disco is still popular with you terrarians, right? I can do a mean boogie.";
                        case "AnubisChat23":
                        return "Hey uh...if anyone asks, ";
                        case "AnubisChat24":
                        return " got all of his info from me, got it?";
                        case "AnubisChat25":
                        return "That ";
                        case "AnubisChat26":
                        return " is pretty chill. Knows a lot about bosses too...actually I hope he isn't stalking me or anything.";
                        case "AnubisChat27":
                        return "I think ";
                        case "AnubisChat28":
                        return " isn't letting in on how smart he actually is. I mean look at that face. Pure. Raw. Genius.";
                        case "AnubisChat29":
                        return " keeps yelling at me for eating all the shoes he makes. It's not my fault he makes them with premium lether.";
                        case "AnubisChat30":
                        return "Don't ask ";
                        case "AnubisChat31":
                        return " where he gets his merch. It's nasty.";
                    }
                }
            return"";
        }
        public static string TownNPCGoblinSlayer(String GoblinSlayer)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(GoblinSlayer)
                    {
                        case "GoblinSlayerName":
                        return "哥布林杀手";
                        case "GoblinSlayerChat1":
                        return "我不相信";
                        case "GoblinSlayerChat2":
                        return ". 他身上有些我不喜欢的东西...";
                        case "GoblinSlayerChat3":
                        return "告诉我他们从哪来的, 他们杀哥布林杀的真多. 我希望有时间去看看那个地方. 听起来很荣幸. ";
                        case "GoblinSlayerChat4":
                        return "我不喜欢哥布林. ";
                        case "GoblinSlayerChat5":
                        return "看到什么我能杀了的哥布林吗? ";
                        case "GoblinSlayerChat6":
                        return "哥布林是这片土地上的祸害. ";
                        case "GoblinSlayerChat7":
                        return "突击几个哥布林的聚集地? ";
                        case "GoblinSlayerChat8":
                        return "为什么我讨厌哥布林? 因为他们是哥布林. ";
                        case "GoblinSlayerChat9":
                        return "嘿, 你在外面能帮我杀些哥布林吗? 把他们的灵魂给我, 我给你我额外的哥布林杀手装备. ";
                    }
                }
            else
                {
                    switch(GoblinSlayer)
                    {
                        case "GoblinSlayerName":
                        return "Goblin Slayer";
                        case "GoblinSlayerChat1":
                        return "I don't trust ";
                        case "GoblinSlayerChat2":
                        return ". There's just something about him that I don't like...";
                        case "GoblinSlayerChat3":
                        return " tells me that where he's from, they kill goblins a lot. I wish to visit this place sometime. Sounds glorious.";
                        case "GoblinSlayerChat4":
                        return "I don't like goblins.";
                        case "GoblinSlayerChat5":
                        return "Seen any goblins I can kill?";
                        case "GoblinSlayerChat6":
                        return "Goblins are a scourge on this earth.";
                        case "GoblinSlayerChat7":
                        return "Find any good goblin dens to raid?";
                        case "GoblinSlayerChat8":
                        return "Why do I hate goblins? Because they're goblins.";
                        case "GoblinSlayerChat9":
                        return "Hey, while you're out there, can you kill some goblins for me? Give me their souls and I'll trade you for some of my extra goblin slaying gear.";
                    }
                }
            return"";
        }
        public static string TownNPCLovecraftian(String Lovecraftian)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(Lovecraftian)
                    {
                        case "button1":
                        return "商店";
                        case "button2":
                        return "提供材料";
                        case "LovecraftianChat1":
                        return "你知道我从哪来, 我就是你的世界所称的“顶尖工匠”";
                        case "LovecraftianChat2":
                        return "我不是唯一来这里的人. 我的世界出现时空裂缝时, 一大堆其他的东西都跟着来了. 但是, 像克苏鲁之眼和克苏鲁之脑早已经在这了, 不知道从哪里来的. ";
                        case "LovecraftianChat3":
                        return "...你看什么呢? 好像你以前没见过乌贼人一样. ";
                        case "LovecraftianChat4":
                        return "没错, 我是个女人. 那又怎么样? 是触须让你远离我? ";
                        case "LovecraftianChat5":
                        return "如果你有自我保护的意识, 我会远离海岸边那艘沉船. 我脖子上的可怕东西就在那儿, 尤其是…啊, 别在意. ";
                        case "LovecraftianChat6":
                        return "有没有在你的触须里发现它是怎么长出来的? 你没有? 只有我有? ";
                        case "LovecraftianChat7":
                        return "嘿, 你的世界很有趣. 你能给我带些不同生物群落的样本来研究吗? 如果你愿意的话, 我可以做些东西来和你交易. ";
                        case "LovecraftianChat8":
                        return "哦!这可真尴尬!不幸的 ";
                        case "LovecraftianChat9":
                        return ". 他的船是我从裂缝里掉出来时损坏的那艘. ";
                        case "LovecraftianChat10":
                        return "那个";
                        case "LovecraftianChat11":
                        return "是瞎扯. 克苏鲁在他说“*你*”(脏话:cnm)之前就会把他给压扁";
                        case "LovecraftianChat12":
                        return "那个晃晃悠悠的死人吓死我了, 说什么我就是个行走的恐怖故事. 我不知道, 我觉得他知道的太多了...";
                        case "LovecraftianChat13":
                        return "有趣的事实：月亮领主和克苏鲁是兄弟. 至少有一次我遇到的粉红小仙女是这么说的";
                        case "PurityFlaskChat":
                        return "哦!这些就是纯净碎片吗? 完美!拿着这个. 你可以用我做的这个特殊的瓶子来净化大多数生物圈. ";
                        case "AshJarChat":
                        return "龙, 啊哈? 我见过更可怕的. 不管怎样, 这是一个新烧瓶. 小心, 很烫. ";
                        case "DarkwaterFlaskChat":
                        return "这是什么. . ? 它实际上是一个……东西的球. 我会调查的. 给, 新烧瓶. 去吧. ";
                        case "CorruptionFlaskChat":
                        return "你捂着嘴干什么? 有些东西闻起来比这更难闻. 只不过是腐肉在分解而已. ";
                        case "CrimsonFlaskChat":
                        return "嗯…骨头? 我以前见过这样的. 我来的地方有类似的. ";
                        case "MeanGreenStewChat":
                        return "这是什么东西? 它……闪闪发光……? 不管怎样, 我会做更多的研究. 这是一个新烧瓶. ";
                        case "VoidFlaskChat":
                        return "哇, 这太重了!这是啥? 我以前从未见过这种金属. 哦, 对了. 新烧瓶. 在这里. ";
                        case "FungicideChat":
                        return "嗯…发光的孢子? 除了发光的蘑菇, 我从没见过这样的东西. 说到蘑菇, 我在这里找到了一些非常好的杀蘑菇配方. 我把它做成了一个烧瓶. ";
                        case "SporeSacChat1":
                        return "这是什么东西? 太黏了……我想我能让它起到点作用. 顺便说一下, ";
                        case "SporeSacChat2":
                        return "教了我如何制作蘑菇孢子. 你可以随意使用它. ";
                        case "GlowingSporeSacChat1":
                        return "嗯, 这东西有点炫酷. *嗅* 妈呀, 这东西真刺鼻. 不管怎么说, 我做了一些";
                        case "GlowingSporeSacChat2":
                        return "的发光孢子. ";
                        case "JungleFlaskChat":
                        return "哦, 非常感谢!这些刺针非常适合做一些临时注射器. 这, 是我开发的一套全新的将森林变成丛林的方案. 漂亮吗, 嗯? ";
                        case "IceFlaskChat":
                        return "现在这东西就很有用了. 感谢. 嘿, 说到冰, 看看这个. 造雪和除雪烧瓶? 喜欢吗? 两个换一个!";
                        case "ForestFlaskChat":
                        return "我真希望这些东西能跟我回到我来的地方. 超可爱~!哦, 但如果我是你, 我会小心对待这些小家伙的. 树精告诉我有一个巨大的怪物保护他们……不管怎样, 当你在外面的时候, 我做了一个新的烧瓶. 它能把丛林变成森林. 小心点. ";
                        case "SquidListChat":
                        return "这是我研究需要的物品清单. 如果你丢了, 我很乐意为你写一篇新的. ";
                        case "NothingChat":
                        return "嗯……没东西? 我需要一些研究的东西. 我想要一些来自各种环境的重要材料. 怪物碎片、植物等. ";
                    }
                }
                else
                {
                    switch(Lovecraftian)
                    {
                        case "button1":
                        return "Shop";
                        case "button2":
                        return "Supply Ingredients";
                        case "LovecraftianChat1":
                        return "You know, where I’m from, I’m what your world would call ‘hot stuff.’";
                        case "LovecraftianChat2":
                        return "I wasn’t the only thing that came here. A whole bunch of other stuff came through with me when a spacial rift opened up in my world. Stuff like the Eye of Cthulhu and the Brain of Cthulhu were already here though. No clue where those two came from.";
                        case "LovecraftianChat3":
                        return "...What are you looking at? You act like you've never seen a squid-person before.";
                        case "LovecraftianChat4":
                        return "Yes I’m a woman. What about it? Is it the tentacle beard that threw you off?";
                        case "LovecraftianChat5":
                        return "If you have any sense of self preservation, I’d avoid that sunken ship in the ocean just off the coast. Scary things from my neck of the woods hang out there, especially... nevermind.";
                        case "LovecraftianChat6":
                        return "Ever just find things in your tentacles that you don’t know how they got there? No? Just me?";
                        case "LovecraftianChat7":
                        return "Hey, your world is pretty interesting. Could you bring me some samples from different biomes for me to study ? If you do, I can make some neat stuff to trade with you.";
                        case "LovecraftianChat8":
                        return "Oh. This is awkward. Poor ";
                        case "LovecraftianChat9":
                        return ". His ship was the one that got destroyed when I fell out of that rift.";
                        case "LovecraftianChat10":
                        return "That ";
                        case "LovecraftianChat11":
                        return " is talking out of his ass. Cthulhu would most likely squash him before he could even say *ech*.";
                        case "LovecraftianChat12":
                        return "That dead guy shambling around freaks me out, and that’s saying something considering I’m a walking horror story. I don’t know, I just feel like he knows too much...";
                        case "LovecraftianChat13":
                        return "Fun fact; The Moon Lord and Cthulhu are brothers. At least that’s what some pink pixie lady I met one time told me.";
                        case "PurityFlaskChat":
                        return "Oh! Are those purity shards? Perfect! Here, take this. You can purify most biomes with this special flask I made.";
                        case "AshJarChat":
                        return "Dragons, eh? I've seen scarier. Anyways, here's a new flask. Careful, it's hot.";
                        case "DarkwaterFlaskChat":
                        return "What is this..? It's literally a ball of...something. I'm gonna look into it. Here, new flask. Go crazy.";
                        case "CorruptionFlaskChat":
                        return "Why are you gagging? There are things that smell way worse than this. It's only decomposing flesh.";
                        case "CrimsonFlaskChat":
                        return "Hm...bones? I've seen ones like these before. Similar to ones from where I came.";
                        case "MeanGreenStewChat":
                        return "What is this stuff? It's...sparkly..? Whatever, I'll research it a bit more. Here's a new flask.";
                        case "VoidFlaskChat":
                        return "Wow this is heavy! What is this? I've never seen this kind of metal before. Oh right. New flask. Here.";
                        case "FungicideChat":
                        return "Hmm...glowing spores? I've never seen something like this aside from glowing mushrooms. Speaking of mushrooms, here, I found a recipe for some really good fungicide. I made it into a flask.";
                        case "SporeSacChat1":
                        return "What is this stuff? It's so squishy...I'll make it work I guess. Oh by the way, ";
                        case "SporeSacChat2":
                        return " showed me how to make mushroom spores. Feel free to use it how you see fit.";
                        case "GlowingSporeSacChat1":
                        return "Hm, this stuff is pretty glowy. *Sniff* Yowza that burns my nostrils. Anyways, here, I made a glowing version of  ";
                        case "GlowingSporeSacChat2":
                        return "'s spores.";
                        case "JungleFlaskChat":
                        return "Oh thank you so much! These stingers will work nicely for some makeshift syringes. Here, I've developed a brand new solution that changes forest into jungle. Nifty, huh?";
                        case "IceFlaskChat":
                        return "Now THIS will come in handy. Thank you. Hey, speaking of ice, check this out. Snow creation AND removal flasks? You like it? Two for the price of one!";
                        case "ForestFlaskChat":
                        return "I wish we had these back where I came from. They're adorable~! Oh, but I'd be careful with these little guys if I were you. The dryad told me there's some giant monster that protects them...anyways, while you were out, I made a new flask. It turns Jungle into forest. Careful with it.";
                        case "SquidListChat":
                        return "Here's a list of some things I need for my research. If you lose it, I'll happily write up a new one for you";
                        case "NothingChat":
                        return "Hmm...nothing? I need stuff to study. I'd like some important materials from biomes. Monster pieces, plants, etc.";
                    }
                }
            return"";
        }
        public static string TownNPCMushman(String Mushman)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(Mushman)
                    {
                        case "button1":
                        return "商店";
                        case "button2":
                        return "诡异植物";
                        case "MushmanChat1":
                        return "那些荧光松露看着真让人难受. ";
                        case "MushmanChat2":
                        return "提议让我用一次他的热水澡缸, 我拒绝了, 因为我有更好的事情要做. ";
                        case "MushmanChat3":
                        return "你知道, 赤孢皇并不是看起来那样. ";
                        case "MushmanChat4":
                        return "别问我从哪儿买的蘑菇炼药. ";
                        case "MushmanChat5":
                        return "我有药, 你有钱, 交易? ";
                        case "MushmanChat6":
                        return "有一次有人问我红松露的味道是否和蓝松露一样好. 当然不是. 蓝松露要咸得多. ";
                        case "NoMushroomChat1":
                        return "我需要一些奇怪的植物. 给我拿些来, 我给你一些特殊的炼金蘑菇. 很适合做药水. ";
                        case "NoMushroomChat2":
                        return "...没有? ";
                        case "NoMushroomChat3":
                        return "请给我植物. 没有植物我没法给你药水. ";
                        case "SpecialChat1":
                        return "癫狂蘑菇? 太好了!这是一种特殊的蘑菇. 这个真的很有用. 只是…不要直接吃. ";
                        case "SpecialChat2":
                        return "哦, 一只癫狂蘑菇!这些我很喜欢, 因为它们有特殊作用. 这里有一些彩虹炼药菇. ";
                        case "SpecialChat3":
                        return "你应该知道, 在两个蘑菇地里都可以找到这些东西. 以防万一, 一定要仔细检查. 它们真的很有用. ";
                        case "MushroomChat1":
                        return "感谢. 这些蘑菇比毫无价值的染料更有用, 对吗? ";
                        case "MushroomChat2":
                        return "这里. 更多的彩色蘑菇满足你的炼药需要. 只是…不要吃它们. ";
                        case "MushroomChat3":
                        return "我要这些染料做什么用? 呃…麻烦. 离我远点, 我有事情要做!";
                    }
                }
            else
                {
                    switch(Mushman)
                    {
                        case "button1":
                        return "Shop";
                        case "button2":
                        return "Strange Plants";
                        case "MushmanChat1":
                        return "Those glowing truffles are all just such downers.";
                        case "MushmanChat2":
                        return " offered to let me get in his hot tub one time. I denied because I had better things to do";
                        case "MushmanChat3":
                        return "The Mushroom Monarch isn't all he seems, you know.";
                        case "MushmanChat4":
                        return "Don't ask where I get the mushrooms for my potions.";
                        case "MushmanChat5":
                        return "I got potions, you got money. Wanna trade?";
                        case "MushmanChat6":
                        return " asked me one time if red truffles tasted as good as blue ones. Obviously not. Blue truffles are way saltier.";
                        case "NoMushroomChat1":
                        return "I need strange plants for something. Bring me some and I'll give you some special alchemical mushrooms. Good for making potions.";
                        case "NoMushroomChat2":
                        return "...no plants?";
                        case "NoMushroomChat3":
                        return "Plants please. I won't give you mushrooms without them.";
                        case "SpecialChat1":
                        return "A Madness Mushroom? Sweet! Here's a special kind mushroom for payment. This one is really useful. Just...don't eat it directly.";
                        case "SpecialChat2":
                        return "Oh, a Madness Mushroom! These ones I like a lot because of their special properties. Here, have a few rainbow shrooms.";
                        case "SpecialChat3":
                        return "You can find these in both mushroom biomes, you know. Make sure to check both of them just in case. They're really useful.";
                        case "MushroomChat1":
                        return "Thank you. These mushrooms are way more useful than worthless dyes, am I right?";
                        case "MushroomChat2":
                        return "Here. More colored mushrooms for all your brewing needs. Just...don't eat them.";
                        case "MushroomChat3":
                        return "What do I use these dye materials for? Uh...things. Now leave me be, I have stuff to do!";

                    }
                }
            return"";
        }
        public static string TownNPCSamurai(String Samurai)
        {
            if(Language.ActiveCulture == GameCulture.English)
                {
                    switch(Samurai)
                    {
                        case "SamuraiChat1":
                        return "I've known ";
                        case "SamuraiChat2":
                        return " for a while. He's quite the man of culture.";
                        case "SamuraiChat3":
                        return "I'm not really a fan of ";
                        case "SamuraiChat4":
                        return "'s ale. It's a bit strong for my taste. I prefer Sake to be entirely honest.";
                        case "SamuraiChat5":
                        return "The chaos biomes weren't always so.... err.... chaotic.";
                        case "SamuraiChat6":
                        return "Have you seen my sword? I can't seem to find it anywhere.";
                        case "SamuraiChat7":
                        return "We used to be the most powerful nation in all of terraria... then... HE came...";
                        case "SamuraiChat8":
                        return "I remember my old master giving me wise words such as: 'YOU ARE TRYING TO VIEW FLASH CONTENT BUT YOU DO NOT HAVE A FLASH PLAYER INSTALLED'... I’m still trying to figure that one out.";
                        case "SamuraiChat9":
                        return "I'll be honest, I get half my lines from fortune cookies. Want one?";
                        case "SamuraiChat10":
                        return "If you refuse to accept anything but the best, you very often get it.";
                        case "SamuraiChat11":
                        return "Change can hurt, but it leads a path to something better.";
                        case "SamuraiChat12":
                        return "You cannot love life until you live the life you love.";
                        case "SamuraiChat13":
                        return "Land is always on the mind of a flying bird.";
                        case "SamuraiChat14":
                        return "Our deeds determine us, as much as we determine our deeds.";
                        case "SamuraiChat15":
                        return "Never give up. You're not a failure if you don't give up.";
                        case "SamuraiChat16":
                        return "You already know the answer to the questions lingering inside your head.";
                        case "SamuraiChat17":
                        return "It is now, and in this world, that we must live.";
                        case "SamuraiChat18":
                        return "You can make your own happiness.";
                        case "SamuraiChat19":
                        return "If winter comes, can spring be far behind?";
                        case "SamuraiChat20":
                        return "A stranger is a friend you have not spoken to yet.";
                        case "SamuraiChat21":
                        return "Your shoes will make you happy today.";
                    }
                }
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(Samurai)
                    {
                        case "SamuraiChat1":
                        return "我认识";
                        case "SamuraiChat2":
                        return "有段时间了. 他很有文化素养. ";
                        case "SamuraiChat3":
                        return "我不太喜欢";
                        case "SamuraiChat4":
                        return "的麦酒. 太浓了. 诚实的说, 我喜欢清酒. ";
                        case "SamuraiChat5":
                        return "混沌之地并不总是这么.... emmm.... 混乱. ";
                        case "SamuraiChat6":
                        return "你见过我的剑吗? 我哪儿也找不到. ";
                        case "SamuraiChat7":
                        return "我们曾经是泰拉瑞亚最强大的民族 ...直到...他 来了...";
                        case "SamuraiChat8":
                        return "我记得我的老主人留给了我一些明智的话, 例如：“你在看Flash, 然而你没有安装Flash播放器”...我依旧在试图弄懂. ";
                        case "SamuraiChat9":
                        return "老实说, 我有一半的台词来自于幸运饼干. 来一块? ";
                        case "SamuraiChat10":
                        return "如果你只接受最好的, 你经常会得到最好的. \n(译者：William Somerset Maugham 威廉·萨默塞特·毛姆)";
                        case "SamuraiChat11":
                        return "改变可能会受伤, 但会带我们走向更好的道路. ";
                        case "SamuraiChat12":
                        return "只有体验过不幸, 你才会热爱生活. ";
                        case "SamuraiChat13":
                        return "大地永远是飞鸟的怀抱. ";
                        case "SamuraiChat14":
                        return "什么样的人便决定了做什么样的事. 同样, 做什么样的事也决定了是什么样的人. \n(译者：George Eliot 乔治·艾略特)";
                        case "SamuraiChat15":
                        return "别放弃. 还未放弃就不曾失败. ";
                        case "SamuraiChat16":
                        return "你已经知道在萦绕在你脑海中的问题答案了. ";
                        case "SamuraiChat17":
                        return "当下世界中最重要的是， 我们还活着. ";
                        case "SamuraiChat18":
                        return "你可以创造自己的幸福. ";
                        case "SamuraiChat19":
                        return "冬天来了, 春天还会远吗? \n(译者：Percy Bysshe Shelley 珀西·比希·雪莱)";
                        case "SamuraiChat20":
                        return "陌生人是你还没有和之交谈过的朋友. ";
                        case "SamuraiChat21":
                        return "今天你的鞋子让你快乐. ";
                    }
                }
            return"";
        }
        public static string BossChat(String BossInfo)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(BossInfo)
                    {
                        case "male":
                        return "他";
                        case "fimale":
                        return "她";
                        case "boy":
                        return ", 少年";
                        case "girl":
                        return ", 少女";
                        case "AHDeath1":
                        return "啊啊啊啊啊! 别又这样了!!!";
                        case "AHDeath2":
                        return "艾希你看? 我告诉你这主意很蠢, 但是你就是不听...";
                        case "AHDeath3":
                        return "我为什么要跟你一块. . ? 我真应该让你自己和他们打. ";
                        case "AHDeath4":
                        return "闭嘴吧! 我觉得我们合力攻击";
                        case "AHDeath5":
                        return ", 我们应该可以把他打趴下!";
                        case "AHDeath6":
                        return "啊. . ! 闭嘴. . !";
                        case "AHDeath7":
                        return "无论怎样...我要回去了. 必须有人告诉父亲这臭小子的事. ";
                        case "AHDeath8":
                        return "好好好. . ! 好吧! 从这边走! 我要回燎狱!";
                        case "AHSpawn1":
                        return "好的, 你好, 在这看见你真惊喜~!";
                        case "AHSpawn2":
                        return "啊对, 我听说过你的一些事, 小子...你就是那个把我母亲痛打的常温动物. . !";
                        case "AHSpawn3":
                        return "啊对, 我听说过你的一些事, 小子...你在这些地方可搞了不少的麻烦...";
                        case "AHSpawn4":
                        return "以及我的...";
                        case "AHSpawn5":
                        return "...你还打伤了我妈...";
                        case "AHSpawn6":
                        return "你应该知道, 你就是个非常讨厌的家伙...";
                        case "AHSpawn7":
                        return "所以现在. . !";
                        case "AHSpawn8":
                        return "我们决定打的你嗷嗷叫. . ! 来吧, 遥酱, 让我们来教训教训这个常温东西~!";
                        case "AHSpawn9":
                        return "别再叫我遥酱了...从现在起. ";
                        case "AsheDowned":
                        return "哦啊. . ! 很疼, 你知道吗!";
                        case "HarukaDowned":
                        return "啊. . ! 哦啊...";
                        case "Akuma1":
                        return "水? ! 卧槽. . ! 我没法呼吸了!";
                        case "Akuma2":
                        return "呀呀呀呀呀呀. 我累了, 小子, 咱俩的比试要推迟了. 明天再来吧. ";
                        case "Akuma3":
                        return "我以为你们这些泰拉人更喜欢打架. 看来猜错了. ";
                        case "Akuma4":
                        return "嗨小子!天塌了, 注意点!";
                        case "Akuma5":
                        return "烈火与愤怒从天而降!";
                        case "Akuma6":
                        return "燎狱火山之灵啊!助我压扁这小子!";
                        case "Akuma7":
                        return "嘿小子!小心!";
                        case "Akuma8":
                        return "我来了!";
                        case "Akuma9":
                        return "小子, 阳光普照之处没有阴凉可乘!";
                        case "Akuma10":
                        return "热起来了, 对吗?";
                        case "Akuma11":
                        return "燎狱的火山终于平息了...";
                        case "Akuma12":
                        return "哈...你小子还不错, 但还不够强. 等你再强一点再回来找我. ";
                        case "AkumaA1":
                        return "艾希? 再来帮你一次亲爱的老爸料理这小子!";
                        case "AkumaA2":
                        return "懂了, 爸爸. . !";
                        case "AkumaA3":
                        return "嘿! 放开我爸!";
                        case "AkumaA4":
                        return "艾女...!";
                        case "AkumaA5":
                        return "明白吗? 你有火焰般炽热的精神! 小子, 我很喜欢这样!";
                        case "AkumaA6":
                        return "什么? !你怎么坚持这么长时间的? !为什么你这个小...我拒绝再被一个泰拉人打败! 攻击!";
                        case "AkumaA7":
                        return "啊啊. . ! 水! 我讨厌水!!!";
                        case "AkumaA8":
                        return "这次, 晚上可救不了你, 小子!已经是新的一天了! ";
                        case "AkumaA9":
                        return "你烧伤了, 小子";
                        case "AkumaA10":
                        return "嘿, 小子这次很光明磊落. 我印象深刻. 这儿. 拿走你的战利品. ";
                        case "AkumaA11":
                        return "啊…!怎么回事!我怎么会输给一个普通的泰拉人? !嗯……好小子, 你赢了, 公平公正. 这是你的奖励. ";
                        case "AkumaA12":
                        return "好好好. 你作弊. 你应该像一个真正的男子汉一样来专家模式挑战我. ";
                        case "AkumaA13":
                        return "天又要塌了! 集中注意!";
                        case "AkumaA14":
                        return "愤怒之火再次从天而降!";
                        case "AkumaA15":
                        return "你低估了龙的火力, 小子!";
                        case "AkumaA16":
                        return "烈火永不熄灭, 小子!";
                        case "AkumaA17":
                        return "抬头! 火山喷发了, 小子!";
                        case "AkumaA18":
                        return "来了!";
                        case "AkumaA19":
                        return "嘿小子? 喜欢烟花吗? 不? 真糟糕!";
                        case "AkumaA20":
                        return "该完美收场了, 小子!";
                        case "AkumaA21":
                        return "白昼不息烈日不止, 小子!";
                        case "AkumaA22":
                        return "面对烈日的怒火吧!";

                        case "AkumaAAshe1":
                        return "爸, 不! 啊! 你! 下次我们再见的时候, 我要把你活烤了. . !";
                        case "AkumaAAshe2":
                        return "哦啊， 混蛋...! 我先撤了!";
                        case "AkumaTransition1":
                        return "呵呵...";
                        case "AkumaTransition2":
                        return "你知道的, 小子...";
                        case "AkumaTransition3":
                        return "扇风可不能灭火...";
                        case "AkumaTransition4":
                        return "邪鬼巨龙已经觉醒!";
                        case "AkumaTransition5":
                        return "只会让他们更猛烈!";
                        case "AkumaTransition6":
                        return "你周围的空气开始升温...";

                        case "Athena1":
                        return "哈..!";
                        case "Athena2":
                        return "们";
                        case "Athena3":
                        return "你! 地上的凡人";
                        case "Athena4":
                        return "我的六翼小天使们告诉我你一直在攻击他们! 为什么?!";
                        case "Athena5":
                        return "那我要给你好好上一课, 你这个不懂规矩的小混蛋!";
                        case "Athena6":
                        return "准备开战!";
                        case "Athena7":
                        return "呼...";
                        case "Athena8":
                        return "咱们快点搞完. 我整天没这么多闲空.";
                        case "Athena9":
                        return "那就滚远点...白痴.";
                        case "Athena10":
                        return "不跟你闹着玩了, 我所呼唤的风暴即将冲你而来!";
                        case "Athena11":
                        return "哦! 好, 好..! 我不打扰你了! 看在上帝的份上, 你给我小心着点, 懂吗?";
                        case "Athena12":
                        return "...那么. 你来啦.";
                        case "Athena13":
                        return "是时候夺回我的荣耀了..!";
                        case "Athena14":
                        return "准备开战!";

                        case "AthenaA1":
                        return "那就滚远点...白痴.";
                        case "AthenaA2":
                        return "哦! 好, 好..! 我不打扰你了! 看在上帝的份上, 你给我小心着点, 懂吗?";

                        case "AthenaDefeat1":
                        return "...呼...呼...";
                        case "AthenaDefeat2":
                        return "...我还是输了.";
                        case "AthenaDefeat3":
                        return "不.";
                        case "AthenaDefeat4":
                        return "我不会这样轻易的放弃.";
                        case "AthenaDefeat5":
                        return "有一句我的人民赖以为生的名言, 地上的凡人.";
                        case "AthenaDefeat6":
                        return "最耀眼的黎明...";
                        case "AthenaDefeat7":
                        return "最漆黑的深夜...";
                        case "AthenaDefeat8":
                        return "即便失败...";
                        case "AthenaDefeat9":
                        return "战 士 不 会 在 最 后 一 战 前 倒 下!!!";

                        case "Athena2Defeat1":
                        return "...为什么?!";
                        case "Athena2Defeat2":
                        return "...我怎么就是赢不了?!";
                        case "Athena2Defeat3":
                        return "...这事咱俩还没完, 地上的凡人";
                        case "Athena2Defeat4":
                        return "们";
                        case "Athena2Defeat5":
                        return "我最终会夺回属于我的荣耀...";
                        case "Athena2Defeat6":
                        return "...那时, 你给我小心着点.";
                        case "Athena2Defeat7":
                        return "黑暗, 混沌的力量在最近苏醒.";
                        case "Athena2Defeat8":
                        return "希望“他”不会回来...";
                        case "Athena2Defeat9":
                        return "一切平安.";

                        case "SeraphHerald1":
                        return "嘿! 地 上 的 凡 人! 对 你, 你 这 个 臭 傻 X!";
                        case "SeraphHerald2":
                        return "雅典娜女王要求你立即再次去天穹卫城觐见!";
                        case "SeraphHerald3":
                        return "她要求重比一次, 而且这次, 她不会让你这么轻易把她打败!";
                        case "SeraphHerald4":
                        return "我也许会说祝你好运, 如果你能出现的话!";
                        case "SeraphHerald5":
                        return "不见不散你个傻X!";
                        case "SeraphHerald6":
                        return "啊对了, 呃, 那个喜欢偷别人东西的恶心虫子也想跟你再打一架.";

                        case "GreedFalse1":
                        return "呃呃呃呃呃呃 这 是 地 表 的 光! 太 亮 了! 太 刺 眼 了!";
                        case "GreedFalse2":
                        return "离 我 琳 琅 满 目 的 财 宝 远 点 你 这 个 臭 小 偷!";

                        case "Greed1":
                        return "谁在打扰我数钱?! 我忙的要死--";
                        case "Greed2":
                        return "...哦哦哦哦哦哦...那是...?";
                        case "Greed3":
                        return "你把我最喜欢吃的带来了..!";
                        case "Greed4":
                        return "金色大餐...";
                        case "Greed5":
                        return "以 及 一 个 泰 拉 人!!!";
                        case "GreedName":
                        return "金食饕餮";

                        case "GreedTransition1":
                        return "你..! 你 这 个 小--";
                        case "GreedTransition2":
                        return "好 了, 我 受 够 了!";
                        case "GreedTransition3":
                        return "你 想 偷 我 的 财 宝, 那 现 在...!";
                        case "GreedTransition4":
                        return "我 要 你 的 命! 呵 呵 呵 呵 呵!!!";
                        case "GreedAName":
                        return "觉醒金食饕餮";

                        case "Rajah1":
                        return "正 义 不 会 被 欺 骗";
                        case "Rajah2":
                        return "正义得到伸张……";
                        case "Rajah3":
                        return "懦夫. ";
                        case "Rajah4":
                        return "你这次赢了, 杀人犯……但我会为那些被你残忍杀害的生物报仇的……";
                        case "Rajah5":
                        return "这 事 没 完, ";
                        case "Rajah6":
                        return "!我 会 和 你 打 到 最 后!";
                        case "Rajah7":
                        return "给我趴下. ";
                        case "Rajah8":
                        return "打得不错, ";
                        case "Rajah9":
                        return ". 拿着你的奖励. ";
                        case "SupremeRajahDefeat1":
                        return "呼...";
                        case "SupremeRajahDefeat2":
                        return "...那么...";
                        case "SupremeRajahDefeat3":
                        return "甚至， 每次当我达到最强状态时...";
                        case "SupremeRajahDefeat4":
                        return "...我也无法打败你. ";
                        case "SupremeRajahDefeat5":
                        return "...泰拉人...也许...";
                        case "SupremeRajahDefeat6":
                        return "也许这就是一个信号...或许我作为保护者的时代...";
                        case "SupremeRajahDefeat7":
                        return "...已经结束了. 该交出我的位置了. ";
                        case "SupremeRajahDefeat8":
                        return "...我代表你所有杀过的兔子原谅你, 但是作为交换...我希望你能代替我的位置...";
                        case "SupremeRajahDefeat9":
                        return "...成为它们的捍卫者. 它们的保护者. ";
                        case "SupremeRajahDefeat10":
                        return "我只需要世界上最强的生物. . 只需要如果你比我强大...";
                        case "SupremeRajahDefeat11":
                        return "谁会比你更适合代替我的位置呢, ";
                        case "SupremeRajahDefeat12":
                        return "成为一个在无辜者需要时能保护它们的人. ";
                        case "SupremeRajahDefeat13":
                        return "考虑一下. ";
                        case "SupremeRajahDefeat14":
                        return "如果你还想和我过两招...用一下那个特殊的萝卜. 我会很高兴夺回我的荣耀. ";
                        case "SupremeRajahDefeat15":
                        return "...看你了, 年轻人. ";
                        case "SupremeRajahDefeat16":
                        return "王公兔的讲话温暖了你的内心. 你决定从此不再伤害兔子. 成为他的骄傲. ";
                        case "YamataAHead":
                        return "哦呃!!!";
                        case "Yamata1":
                        return "哈! 我对你很宽容! 当你真正强大的时候再来, 我们来真正的比试!";
                        case "Yamata2":
                        return "八歧大蛇被打败, 潭渊中的雾气消散";
                        case "Yamata3":
                        return "潭渊里没有太阳!!! 呵呵呵呵呵呵!!!";
                        case "Yamata4":
                        return "非非非非常讨厌!!! 这太阳! 我走了!";
                        case "Yamata5":
                        return "你 觉 我 已 经 不 行 了 对 吗? ! 我 可 不 这 么 认 为!!!";
                        case "Yamata6":
                        return "哦可别想飞! 我自己就是巨大的引力, 会吸引一切包括它自己! 噫哈哈哈哈哈哈哈哈!!!";
                        case "Yamata7":
                        return "潭渊里无处可逃!";
                        case "Yamata8":
                        return "想走? ! 没这么容易！";
                        case "Yamata9":
                        return "噫哈哈哈哈哈哈哈哈. . ! 别回来了!";
                        case "Yamata10":
                        return "这点防御在这可救不了你! 别当个小淘气鬼了, 让我毁了你!";
                        case "Yamata11":
                        return "别来回跑了, 让我宰了你!!!";
                        case "Yamata12":
                        return "呵呵呵呵呵, 你真的很烦你知道吗. . !";
                        case "Yamata13":
                        return "我不懂你为什么要一直和我打! 我各方面都比你强!";
                        case "Yamata14":
                        return "我又有点沮丧!";
                        case "Yamata15":
                        return "我讨厌和你打! 非常非常非常讨厌!!!";
                        case "YamataHead":
                        return "哦啊!!!";
                        case "YamataTransition1":
                        return "呀哈哈哈哈哈哈哈哈哈~";
                        case "YamataTransition2":
                        return "你觉得我已经完了...? !";
                        case "YamataTransition3":
                        return "哈!说的和真的一样!";
                        case "YamataTransition4":
                        return "深渊蠢动着...";
                        case "YamataTransition5":
                        return "八歧大蛇已经觉醒!";
                        case "YamataTransition6":
                        return "现在有七个头了！啊哈哈哈哈哈哈哈哈哈!!!!!";
                        case "YamataTransition7":
                        return "你开始感到来自灵魂的压力...";
                        case "YamataA1":
                        return "呃啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊";
                        case "YamataA2":
                        return "不? 不可能的!即使在我清醒的时候? !你一定是作弊了!呃啊……!好吧!拿走你的战利品!我要走了……!";
                        case "YamataA3":
                        return "八歧大蛇被打败, 潭渊的雾气消散. ";
                        case "YamataA4":
                        return "不————!!! 你这个臭小子!!! 这次我差点就碰到你了!!! 好吧, 拿走你的东西, 反正我也不在乎!";
                        case "YamataA5":
                        return "哈! 得了吧! 当你不用作弊的时候再回到专家模式来打败我! 你所有的战利品都是我的!";
                        case "YamataA6":
                        return "你还能打? ! 你有毛病吗? !";
                        case "YamataA7":
                        return "我受够了你的恶作剧!!!吃我毒液你这个小混蛋!!!";
                        case "YamataA8":
                        return "停停停停停!!!我不会让你赢的!!!";
                        case "YamataA9":
                        return "你知道吗, 你就像个讨厌的小臭虫!";
                        case "YamataA10":
                        return "死!你怎么还不死? !";
                        case "YamataA11":
                        return "停!!! 我可不想再输了!!!";
                        case "YamataA12":
                        return "什-什么? !死!去死吧, 你这个小东西!死死死死死死死!!!!";
                        case "YamataA13":
                        return "不不不!!! 别了!!! 这次我要把你踩在地上!!!";
                        case "YamataA14":
                        return "爸爸, 看来我得再来救你一次. ";
                        case "YamataA15":
                        return "我的好闺女. . !";
                        case "YamataA16":
                        return "哦, 亲爱的. . ! 想帮爸爸打这个小虫子吗? ";
                        case "YamataA17":
                        return "哎...是的爸爸";
                        case "HarukaY1":
                        return "爸爸, 你这个白痴……不论怎样, 我不能说我没预料到. ";
                        case "HarukaY2":
                        return "就这样吧. 我输了, 你来对付他们, 爸爸. ";
                        case "YamataHead1":
                        return "尝尝酸液的滋味, 你这讨厌的蛆虫!!!!";
                        case "YamataHead2":
                        return "别动, 让我把你融化了!!!!";
                        case "YamataHead3":
                        return "毒液往下流呀往下流!!!!什么时候停止? 谁知道? !咿哈哈哈哈哈哈哈哈哈!!!!";
                        case "YamataHead4":
                        return "死吧死吧死吧死吧死死死死死死吧!!!";
                        case "YamataHead5":
                        return "啪!砰!我要送你上西天!!!";
                        case "YamataHead6":
                        return "咿哈哈哈哈哈哈哈哈哈!!!";
                        case "YamataHead7":
                        return "抓住他!吃掉他!让";
                        case "YamataHead8":
                        return "从我眼前消失!!!";
                        case "YamataHead9":
                        return"我吃过比你更吓人的兔子";
                        case "YamataHead10":
                        return "希望你带了雨伞!因为雨下得疼!!!!咿哈哈哈哈哈哈哈哈!!!!";
                        case "YamataHead11":
                        return "毒液降下!!!咿哈哈哈哈哈哈哈哈!";
                        case "YamataHead12":
                        return "尝尝幽魂的厉害吧你个小混蛋";
                        case "YamataHead13":
                        return "咿哈哈哈哈哈哈!!!";
                        case "YamataHead14":
                        return "哎呀!流下的酸液!希望你不会被溶解…. ";
                        case "YamataHead15":
                        return "哎呀!又是流下的酸液!咿哈哈哈哈哈哈哈哈";
                        case "YamataHead16":
                        return "哇啊啊啊啊!你没法活下来!";
                        case "YamataHead17":
                        return "快 点!!!!站 着 别 动, 好 让 我 送 你 上 火 星!";
                        case "YamataHead18":
                        return "呀啊啊啊啊啊啊啊啊啊啊啊啊啊啊 别动啊啊啊啊啊啊啊!!";
                        case "YamataHead19":
                        return "有酸液的正常味道!";
                        case "YamataHead20":
                        return "我要把你撕成碎片!!!!你个小混蛋!";
                        case "YamataHead21":
                        return "噫噫噫呃呃呃呃呃呃呃呃呃呃呃呃呃呃呃呃呃呃呃!!!!";
                        case "Sagittarius1":
                        return "目 标 已 被 消 灭. 返 回 隐 形 模 式. ";
                        case "Sagittarius2":
                        return "目 标 丢 失. 返 回 隐 形 模 式. ";
                        case "SagittariusFree1":
                        return "目 标 已 被 消 灭. 返 回 隐 形 模 式. ";
                        case "SagittariusFree2":
                        return "目 标 丢 失. 返 回 隐 形 模 式. ";
                        case "SagittariusFree3":
                        return "自我维护程序初始化";
                        case "ZeroBoss1":
                        return "零 械 物 理 单 元 处 于 临 界 状 态. 调 用 并 执 行 末 日 注 册 协 议. ";
                        case "ZeroBoss2":
                        return "末 日 注 册 协 议 报 错. MAIN. EXPERT M0DE = FALSE. （主程序未检测到专家模式）";
                        case "ZeroBoss3":
                        return "厄劫石停止发光. 现在你可以挖掘了. ";
                        case "ZeroBoss4":
                        return "初 始 化 备 用 武 器 协 议. ";
                        case "ZeroBoss5":
                        return "关 键 错 误: 未 找 到 手 臂 单 元. 重 新 调 用 资 源 链 接 到 攻 击 协 议. 护 罩 降 级. ";
                        case "ZeroBoss6":
                        return "目 标 被 毁 灭. 返 回 初 始 轨 道. ";
                        case "ZeroBoss7":
                        return "目 标 丢 失. 返 回 初 始 轨 道. ";
                        case "ZeroBoss8":
                        return "初 始 化 探 针 创 建 系 统. ";
                        case "ZeroBoss9":
                        return "旋 转 协 议 加 载. ";
                        case "ZeroBoss10":
                        return "武 器 单 元 重 新 加 载";
                        case "ZeroBoss11":
                        return "重 载 武 器 单 元";
                        case "ZeroAwakened1":
                        return "厄劫石停止发光. 现在你可以挖掘了. ";
                        case "ZeroAwakened2":
                        return "警 告. 检 测 到 严 重 损 坏, 任 务 即 将 失 败. 启 动 全 面 抹 杀 协 议";
                        case "ZeroAwakened3":
                        return "警 告. 检 测 到 严 重 损 坏, 再 次 任 务 即 将 失 败. 启 动 终 极 全 面 抹 杀 协 议";
                        case "ZeroAwakened4":
                        return "作 弊 警 告!作 弊 警 告! 释 放 4 个 零 单 位";
                        case "ZeroAwakened5":
                        return "你 的 作 弊 工 具 无 法 拯 救 你";
                        case "ZeroAwakened6":
                        return "目 标 被 毁 灭. 返 回 初 始 轨 道. ";
                        case "ZeroAwakened7":
                        return "目 标 丢 失. 返 回 初 始 轨 道. ";
                        case "ZeroDeath1":
                        return "任 务 失 败. 向 基 地 发 送 遇 难 信 号. ";
                        case "ZeroDeath2":
                        return "任 务 失 败. 再 次 尝 试 发 送 遇 难 信 号. ";
                        case "ZeroDeath3":
                        return "信 号 发 送 中...";
                        case "ZeroDeath4":
                        return "收 到 遇 难 信 号. ";
                        case "FuryAshe1":
                        return "爸, 不! 你要为此付出代价, ";
                        case "FuryAshe2":
                        return "啊! 对不起, 爸. . ! 我必须离开了!";
                        case "WrathHaruka1":
                        return "父亲! 呃啊. . ! 下次再见的时候, 我要让你命丧于此!";
                        case "WrathHaruka2":
                        return "呃...对不起, 父亲...我坚持不住了...";
                        case "ShenA1":
                        return "我不得不说, 孩子. 你让我印象深刻. ";
                        case "ShenA2":
                        return "直面它吧, 孩子! 你永远也无法打败本身就是混沌的存在!";
                        case "ShenA3":
                        return "即使面临困难， 你依旧在战斗";
                        case "ShenA4":
                        return "你执意要这样? 真有意思...";
                        case "ShenA5":
                        return "老实说， 你让我想起了我自己...";
                        case "ShenA6":
                        return "你知道难逃一死之时， 你就与之抗争...";
                        case "ShenA7":
                        return "也许有一天， 你会成为你自己领地的统治者...";
                        case "ShenA8":
                        return "现在别再这样了!站住， 像个男人一样!";
                        case "ShenA9":
                        return "然而如今， 我们对立而战!现在我看看你有什么能耐!";
                        case "ShenA10":
                        return "微 不 足 道 的 一 只 蝼 蚁! 你 已 经 死 了!";
                        case "ShenA11":
                        return "继续来? 真令我印象深刻. 给我看看你真正的实力!";
                        case "ShenA12":
                        return "什 么? ! 你 怎 么 - 够 了! 你 很 快 就 会 直 到 永 劫 的 混 沌 意 味 着 什 么!";
                        case "ShenA13": 
                        return "来! 继续努力!";
                        case "ShenA14":
                        return "不! 我 怎 么 会! 输 给 你!";
                        case "ShenA15":
                        return "给 我 看 看! 给 我 看 看 你 有 什 么 真 本 事!";
                        case "ShenA16":
                        return "哦啊啊啊啊啊!!!";
                        case "ShenDeath1":
                        return "重新分离...";
                        case "ShenDeath2":
                        return "这是你的错， 你这个傲慢的虫子. . !我知道我们应该更加大力度的攻击， 但是...不不不不. . !你说我们可以不费吹灰之力就把它们压扁!";
                        case "ShenDeath3":
                        return "勇者";
                        case "ShenDeath4":
                        return ", 总有一天你会再次面对我们的怒火...等我们再次准备好充足的精力...";
                        case "ShenDeath5":
                        return "...或者你决定再用一次印迹. . !";
                        case "ShenDeath6":
                        return "你自己选择, 孩子. ";
                        case "ShenDeath7":
                        return "你这个笨蛋!我们又双叒叕!输了!!";
                        case "ShenDeath8":
                        return "啊, 我的头...";
                        case "ShenDeath9":
                        return "跳 梁 小 丑";
                        case "ShenDeath10":
                        return "那么你, ";
                        case "ShenDeath11":
                        return "! 下 次 我 会 把 你 的 头 拧 下 来!!!";
                        case "ShenDeath12":
                        return "相信我们, 小子. ";
                        case "ShenDeath13":
                        return "总会有下次的. ";
                        case "ShenDoragon1":
                        return "泰 拉 魔 法? ! 不 会! 我 一 度 认 为 它 已 经 从 这 片 土 地 上 消 失 了!";
                        case "ShenDoragon2":
                        return "上神之爪! 助我!";
                        case "ShenDoragon3":
                        return "艾希? 遥香? 我再次需要你们帮忙. . !";
                        case "ShenDoragon4":
                        return "收到, 爸爸~!";
                        case "ShenDoragon5":
                        return "又来. . ? ";
                        case "ShenDoragon6":
                        return "女儿们. . ? 帮助你父亲对付这个蝼蚁. ";
                        case "ShenDoragon7":
                        return "很乐意, 爸爸~!";
                        case "ShenDoragon8":
                        return "好, 父亲. ";
                        case "ShenDoragon9":
                        return "孩子， 你很固执. 我喜欢. ";
                        case "ShenDoragon10":
                        return "这什么? ...能力? 我从来没想过...";
                        case "ShenDoragon11":
                        return "真的勇士从不仁慈！我不会， 我认为你也不会……";
                        case "ShenDoragon12":
                        return "放弃吧, 孩子. 世界终将归于混沌!";
                        case "ShenDoragon13":
                        return "毫 不 留 情!";
                        case "ShenDoragon14":
                        return "什么? 你还能打? 为什么? !";
                        case "ShenDoragon15":
                        return "一个超远古之神的失败给予了宝石持有者们新的力量";
                        case "ShenDoragon16":
                        return "呵呵, 好吧. 我想我会让你歇息一会. 但是如果你回来变得更强, 我会给你展示展示永劫混沌的真实实力...";
                        case "ShenDoragon17":
                        return "一个超远古之神的失败给予了宝石持有者们新的力量";
                        case "ShenDoragon18":
                        return "一场好戏, 孩子, 真是一场好戏. 你的战斗力仍然让我印象深刻!也许有一天我会给你看看我的真正实力. ";
                        case "ShenSpawn1":
                        return "又看到我们了是不是很惊讶, 小子? ";
                        case "ShenSpawn2":
                        return "噫哈哈哈哈. . ! 对. . ! 这里看见我们一定惊得说不出话了. . ! 但这次， 我们还藏了一手. . !";
                        case "ShenSpawn3":
                        return "你刚才使用的那个印迹使我们恢复了全部的力量， 这将使我们达到真正的强大形态. . !";
                        case "ShenSpawn4":
                        return "我们曾是同一个生物. . ! 但是后来有一个像你一样的泰拉混蛋将我们的灵魂一分为二. . ! 但是现在...呵呵呵呵呵...";
                        case "ShenSpawn5":
                        return "我 们 合 二 为 一";
                        case "ShenSpawn6":
                        return "呵呵.... 哈哈哈哈哈哈...";
                        case "ShenSpawn7":
                        return "你的致命失误, 小子...";
                        case "ShenSpawn8":
                        return "正如你所见.... ";
                        case "ShenSpawn9":
                        return "吾 乃 上 神 应 龙, 混 乱 与 动 荡 的 原 初 之 皇!";
                        case "ShenSpawn10":
                        return "而你, 我的孩子, 将会面临混沌的恐惧和怒火. . !";
                        case "ShenSpawn11":
                        return "只有一个字 死!!!";
                        case "ShenTransition1":
                        return "呵呵...";
                        case "ShenTransition2":
                        return "呵呵呵呵...";
                        case "ShenTransition3":
                        return "哈哈哈哈哈哈哈!!!";
                        case "ShenTransition4":
                        return "你是不是已经忘了我们最后一次战斗...? ";
                        case "ShenTransition5":
                        return "总会还有一次战斗, 小子...";
                        case "ShenTransition6":
                        return "我只是用了一部分我的能力...而现在...呵呵呵呵...";
                        case "ShenTransition7":
                        return "上神应龙觉醒了!";
                        case "ShenTransition8":
                        return "你 将 在 混 律 业 火 中 焚 烧 殆 尽!!!";
                    }
                }
            else
                {
                    switch(BossInfo)
                    {
                        case "male":
                        return "him";
                        case "fimale":
                        return "her";
                        case "boy":
                        return ", BOY";
                        case "girl":
                        return ", GIRL";

                        case "AHDeath1":
                        return "RRRRRRRRRGH! NOT AGAIN!!!";
                        case "AHDeath2":
                        return "You see Ashe? I told you this was a stupid idea, but YOU didn't listen...";
                        case "AHDeath3":
                        return "Why do I keep going with you..? I should honestly just let you fight them yourself.";
                        case "AHDeath4":
                        return "Shut up! I thought if we ganged up on ";
                        case "AHDeath5":
                        return ", we could just beat the daylights out of 'em!";
                        case "AHDeath6":
                        return "Rgh..! Shut up..!";
                        case "AHDeath7":
                        return "Whatever...I'm going back home. SOMEONE has to tell dad about this kid.";
                        case "AHDeath8":
                        return "Hmpf..! Fine! Be that way! I'm going back to the inferno!";

                        case "AHSpawn1":
                        return "Well hello there, what a surprise to see YOU here~!";
                        case "AHSpawn2":
                        return "Oh yes, I've heard PLENTY about you, kid...you're the little warm-blood who thrashed my mother..!";
                        case "AHSpawn3":
                        return "Oh yes, I've heard PLENTY about you, kid...you've been stirring up quite a bit of trouble in these parts...";
                        case "AHSpawn4":
                        return "And mine...";
                        case "AHSpawn5":
                        return "...and you also hurt my mom...";
                        case "AHSpawn6":
                        return "You're a pretty annoying wretch, you know...";
                        case "AHSpawn7":
                        return "So now..! Heh...";
                        case "AHSpawn8":
                        return "We're gonna give you something to absolutely SCREAM about..! Come on, Hakie, let's torch this little warm-blood~!";
                        case "AHSpawn9":
                        return "Please don't call me Hakie again...ever.";

                        case "AsheDowned":
                        return "OW..! THAT HURT, YOU KNOW!";
                        case "HarukaDowned":
                        return "Rgh..! Ow...";

                        case "Akuma1":
                        return "Water?! ACK..! I CAN'T BREATHE!";
                        case "Akuma2":
                        return "Yaaaaaaaaawn. I'm bushed kid, I'm gonna have to take a rain check. Come back tomorrow.";
                        case "Akuma3":
                        return "I thought you terrarians put up more of a fight. Guess not.";
                        case "Akuma4":
                        return "Hey kid! Sky's fallin', watch out!";
                        case "Akuma5":
                        return "Down comes fire and fury!";
                        case "Akuma6":
                        return "Spirits of the volcano! help me crush this kid!";
                        case "Akuma7":
                        return "Hey kid! Watch out!";
                        case "Akuma8":
                        return "Incoming!";
                        case "Akuma9":
                        return "Sun's shining, and there's no shade to be seen, kid!";
                        case "Akuma10":
                        return "Getting hotter, ain't it?";
                        case "Akuma11":
                        return "The volcanoes of the inferno are finally quelled...";
                        case "Akuma12":
                        return "Hmpf...you’re pretty good kid, but not good enough. Come back once you’ve gotten a bit better.";

                        case "AkumaA1":
                        return "Ashe? Help your dear old dad with this kid again!";
                        case "AkumaA2":
                        return "You got it, daddy..!";
                        case "AkumaA3":
                        return "Hey! Hands off my papa!";
                        case "AkumaA4":
                        return "Atta-girl..!";
                        case "AkumaA5":
                        return "Still got it, do you? Ya got fire in your spirit! I like that about you, kid!";
                        case "AkumaA6":
                        return "What?! How have you lasted this long?! Why you little... I refuse to be bested by a terrarian again! Have at it!";
                        case "AkumaA7":
                        return "ACK..! WATER! I LOATHE WATER!!!";
                        case "AkumaA8":
                        return "Nighttime won't save you from me this time, kid! The day is born anew!";
                        case "AkumaA9":
                        return "You just got burned, kid.";
                        case "AkumaA10":
                        return "Heh, not too shabby this time kid. I'm impressed. Here. Take your prize.";
                        case "AkumaA11":
                        return "GRAH..! HOW!? HOW COULD I LOSE TO A MERE MORTAL TERRARIAN?! Hmpf...fine kid, you win, fair and square. Here's your reward.";
                        case "AkumaA12":
                        return "Nice. You cheated. Now come fight me in expert mode like a real man.";
                        case "AkumaA13":
                        return "Sky's fallin' again! On your toes!";
                        case "AkumaA14":
                        return "Down comes the flames of fury again!";
                        case "AkumaA15":
                        return "You underestimate the artillery of a dragon, kid!";
                        case "AkumaA16":
                        return "Flames don't give in till the end, kid!";
                        case "AkumaA17":
                        return "Heads up! Volcano's eruptin' kid!";
                        case "AkumaA18":
                        return "INCOMING!";
                        case "AkumaA19":
                        return "Hey Kid? Like Fireworks? No? Too Bad!";
                        case "AkumaA20":
                        return "Here comes the grand finale, kid!";
                        case "AkumaA21":
                        return "The Sun won't quit 'til the day is done, kid!";
                        case "AkumaA22":
                        return "Face the fury of the sun!";

                        case "AkumaAAshe1":
                        return "Papa, NO! HEY! YOU! I'm gonna bake you alive next time we meet..!";
                        case "AkumaAAshe2":
                        return "OW, you Jerk..! I'm out!";

                        case "AkumaTransition1":
                        return "Heh...";
                        case "AkumaTransition2":
                        return "You know, kid...";
                        case "AkumaTransition3":
                        return "fanning the flames doesn't put them out...";
                        case "AkumaTransition4":
                        return "Akuma has been Awakened!";
                        case "AkumaTransition5":
                        return "IT ONLY MAKES THEM STRONGER!";
                        case "AkumaTransition6":
                        return "The air around you begins to heat up...";

                        case "Athena1":
                        return "Hmpf..!";
                        case "Athena2":
                        return "s";
                        case "Athena3":
                        return "You! Earthwalker";
                        case "Athena4":
                        return "My seraphs tell me you've been attacking them! Why?!";
                        case "Athena5":
                        return "I'm gonna teach you a lesson, you little brat!";
                        case "Athena6":
                        return "En Garde!";
                        case "Athena7":
                        return "Sigh...";
                        case "Athena8":
                        return "Lets just get this overwith. I don't have all day.";
                        case "Athena9":
                        return "And stay away...idiot.";
                        case "Athena10":
                        return "No more kidding around, the storms are calling, and they're coming for you!";
                        case "Athena11":
                        return "OW! Fine, fine..! I'll leave you alone! Geez, you don't let up, do you.";
                        case "Athena12":
                        return "...So. You came.";
                        case "Athena13":
                        return "It's high time I won my honor back..!";
                        case "Athena14":
                        return "En Garde!";

                        case "AthenaA1":
                        return "And stay away...idiot.";
                        case "AthenaA2":
                        return "OW! Fine, fine..! I'll leave you alone! Geez, you don't let up, do you.";

                        case "AthenaDefeat1":
                        return "...hah...hah...";
                        case "AthenaDefeat2":
                        return "...I still lost.";
                        case "AthenaDefeat3":
                        return "No.";
                        case "AthenaDefeat4":
                        return "I'm not giving up that easilly.";
                        case "AthenaDefeat5":
                        return "There's a phrase my people live by, earthwalker.";
                        case "AthenaDefeat6":
                        return "Brightest of dawn...";
                        case "AthenaDefeat7":
                        return "Darkest of night...";
                        case "AthenaDefeat8":
                        return "Even in defeat...";
                        case "AthenaDefeat9":
                        return "A VARIAN ALWAYS PUTS UP ONE LAST FIGHT!!!";

                        case "Athena2Defeat1":
                        return "...WHY?!";
                        case "Athena2Defeat2":
                        return "...Why can't I win?!";
                        case "Athena2Defeat3":
                        return "...You haven't heard the end of me, earthwalker ";
                        case "Athena2Defeat4":
                        return "s";
                        case "Athena2Defeat5":
                        return "I will win back my honor eventually...";
                        case "Athena2Defeat6":
                        return "...until then, watch your back.";
                        case "Athena2Defeat7":
                        return "Dark, chaotic forces are waking up as of late.";
                        case "Athena2Defeat8":
                        return "hopefully HE doesn't come back...";
                        case "Athena2Defeat9":
                        return "Stay safe.";

                        case "SeraphHerald1":
                        return "HEY! EARTHWALKER! YEAH YOU, YA STUPID APE!";
                        case "SeraphHerald2":
                        return "Queen Athena requests your presence at the acropolis again immediately!";
                        case "SeraphHerald3":
                        return "She demands a rematch, and this time, she won't let you tear her down so easily!";
                        case "SeraphHerald4":
                        return "I would say break a leg, but we can do that ourselves when you show up!";
                        case "SeraphHerald5":
                        return "See ya twerp!";
                        case "SeraphHerald6":
                        return "Oh yeah, and uh, that obnoxious kleptomaniac worm wants to fight you again too or something.";

                        case "GreedFalse1":
                        return "EEEEEEEEEEEEEEEGH THE LIGHT OF THE SURFACE! TOO BRIGHT! TOO BRIGHT!";
                        case "GreedFalse2":
                        return "AND STAY AWAY FROM MY GLORIOUS RICHES YOU LITTLE THIEF!";

                        case "Greed1":
                        return "Who disturbs me from my coin-counting?! I'm busy--";
                        case "Greed2":
                        return "...Oooooh...is that...?";
                        case "Greed3":
                        return "You brought me my favorite dish..!";
                        case "Greed4":
                        return "Golden Grub...";
                        case "Greed5":
                        return "WITH A SIDE OF TERRARIAN!!!";
                        case "GreedName":
                        return "Greed";

                        case "GreedTransition1":
                        return "YOU..! YOU LITTLE--";
                        case "GreedTransition2":
                        return "THATS IT, I'VE HAD IT!";
                        case "GreedTransition3":
                        return "YOU TRY TO STEAL MY SHINIES, AND NOW...!";
                        case "GreedTransition4":
                        return "I WILL STEAL YOUR LIFE! HEEHEEHEEHEEHEEHEEHEEHEEHEEHEE!!!";
                        case "GreedAName":
                        return "Greed";

                        case "Rajah1":
                        return "JUSTICE CANNOT BE CHEATED";
                        case "Rajah2":
                        return "Justice has been served...";
                        case "Rajah3":
                        return "Coward.";
                        case "Rajah4":
                        return "You win this time, murderer...but I will avenge those you've mercilicely slain...";
                        case "Rajah5":
                        return "THIS ISN'T THE END, ";
                        case "Rajah6":
                        return "! RIVALS CLASH TILL THE VERY END!";
                        case "Rajah7":
                        return "And stay down.";
                        case "Rajah8":
                        return "Well fought, ";
                        case "Rajah9":
                        return ". Take your reward.";

                        case "SupremeRajahDefeat1":
                        return "Rgh...";
                        case "SupremeRajahDefeat2":
                        return "...so...";
                        case "SupremeRajahDefeat3":
                        return "Even when I'm at my most powerful...";
                        case "SupremeRajahDefeat4":
                        return "...I still can't beat you.";
                        case "SupremeRajahDefeat5":
                        return "...Terrarian...maybe...";
                        case "SupremeRajahDefeat6":
                        return "Perhaps this is all just a sign that...maybe my time as protector...";
                        case "SupremeRajahDefeat7":
                        return "...is finally up. It might be time to pass on the baton.";
                        case "SupremeRajahDefeat8":
                        return "...I forgive you for every rabbit you've killed, but in return...I want you to take my place...";
                        case "SupremeRajahDefeat9":
                        return "...as their champion. Their protector.";
                        case "SupremeRajahDefeat10":
                        return "I only want the best for the creatures of this world...and if you're stronger than I am...";
                        case "SupremeRajahDefeat11":
                        return "Who better to take my place than you, ";
                        case "SupremeRajahDefeat12":
                        return "Be the one the innocent can look to in their time of need.";
                        case "SupremeRajahDefeat13":
                        return "Think about it.";
                        case "SupremeRajahDefeat14":
                        return "And if you ever want to spar...use one of those special carrots. I'd be glad to earn my honor back.";
                        case "SupremeRajahDefeat15":
                        return "...See ya, kiddo.";
                        case "SupremeRajahDefeat16":
                        return "Rajah Rabbit's speech warms your heart. You no longer have the will to harm rabbits. Do him proud.";

                        case "YamataAHead":
                        return "OWIE!!!";
                        case "Yamata1":
                        return "HAH! I went easy on ya! Come back when you're actually good and we can have a real fight!";
                        case "Yamata2":
                        return "The defeat of Yamata causes the fog in the mire to lift.";
                        case "Yamata3":
                        return "THE SUN DOESN'T SHINE IN THE DEPTHS!!! NYEHEHEHEHEHEHEHEH!!!";
                        case "Yamata4":
                        return "HISSSSSSSSSSSSSSS!!! THE SUNNNNNNNNNN! I'M OUT!";
                        case "Yamata5":
                        return "YOU THINK I'M DONE YET?! I DON'T THINK SO!!!!";
                        case "Yamata6":
                        return "Oh and don't even think about flying! My ego is so massive it has a gravitational pull all of it's own! NYEHEHEHEHEHEHEHEHEHEHEHEH!!!";
                        case "Yamata7":
                        return "THERE IS NO ESCAPE FROM THE ABYSS!";
                        case "Yamata8":
                        return "Running away?! I DON'T THINK SO!";
                        case "Yamata9":
                        return "NYEHEHEHEHEHEHEH..! And don�t come back!";
                        case "Yamata10":
                        return "Resistance isn't gonna save you here! Now stop being a little brat and let me destroy you!";
                        case "Yamata11":
                        return "STOP SQUIRMING AND LET ME SQUASH YOU!!!";
                        case "Yamata12":
                        return "NGAAAAAAAAAAAAAH YOU'RE REALLY ANNOYING YOU KNOW..!";
                        case "Yamata13":
                        return "I don't understand why you keep fighting me! I'm superior to you in every single way!";
                        case "Yamata14":
                        return "I'M GETTING FRUSTRATED AGAIN!";
                        case "Yamata15":
                        return "I HATE FIGHTING YOU! I HATE IT I HATE IT I HATE IT!!!";

                        case "YamataHead":
                        return "OWIE!!!";
                        case "YamataTransition1":
                        return "NYEHEHEHEHEHEHEHEH~!";
                        case "YamataTransition2":
                        return "You thought I was DONE..?!";
                        case "YamataTransition3":
                        return "HAH! AS IF!";
                        case "YamataTransition4":
                        return "The abyss hungers...";
                        case "YamataTransition5":
                        return "Yamata has been Awakened!";
                        case "YamataTransition6":
                        return "AND IT'S GOT 7 HEADS! NYEHEHEHEHEHEHEHEHEHEHEHEH!!!";
                        case "YamataTransition7":
                        return "You begin to feel as if your soul is weighing you down...";

                        case "YamataA1":
                        return "REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE";
                        case "YamataA2":
                        return "NO...! IMPOSSIBLE! EVEN IN MY AWAKENED FORM?! YOU MUST HAVE CHEATED! GYAAAAAAH..! FINE! TAKE YOUR LOOT! I'M OUTTA HERE..!";
                        case "YamataA3":
                        return "The defeat of Yamata causes the fog in the mire to lift.";
                        case "YamataA4":
                        return "NOOOOOOOOOOOOOO!!! YOU LITTLE BRAT!!! I ALMOST HAD YOU THIS TIME!!! FINE, take your stuff, See if I care!";
                        case "YamataA5":
                        return "HAH! Nice try! Come back in Expert mode when you don�t have to cheat to beat me! All your loot is MINE still!";
                        case "YamataA6":
                        return "YOU'RE STILL FIGHTING?! WHAT THE HELL IS WRONG WITH YOU?!";
                        case "YamataA7":
                        return "I'VE HAD IT UP TO HERE WITH YOUR SHENANIGANS!!! EAT VENOM YOU LITTLE HELLSPAWN!!!";
                        case "YamataA8":
                        return "STOP IT STOP IT STOP IT!!! I'M NOT GONNA LET YOU WIN!!!";
                        case "YamataA9":
                        return "You're an annoying little bugger, you know!";
                        case "YamataA10":
                        return "DIE! WHY WON'T YOU JUST DIE ALREADY?!";
                        case "YamataA11":
                        return "STOP IT!!! I'M NOT GONNA LOSE AGAIN!!!";
                        case "YamataA12":
                        return "Wh-WHA?! DIE! DIE YOU LITTLE TWERP! DIEDIEDIEDIEDIEDIEDIE!!!!";
                        case "YamataA13":
                        return "NO NO NO!!! NOT AGAIN!!! THIS TIME IMMA STOMP YOU RIGHT INTO THE GROUND!!!";
                        case "YamataA14":
                        return "Looks like I gotta come in and save your rear end again, dad.";
                        case "YamataA15":
                        return "That's my girl..!";
                        case "YamataA16":
                        return "Oh, sweetie..! Care to help daddy thrash this little worm?!";
                        case "YamataA17":
                        return "Sigh...yes dad.";

                        case "HarukaY1":
                        return "Dad, you moron..! Whatever, Can't really say I didn't see it coming.";
                        case "HarukaY2":
                        return "That's it. I'm done, YOU deal with them, dad.";

                        case "YamataHead1":
                        return "TASTE ACID YOU UNBEARABLE MAGGOT!!!";
                        case "YamataHead2":
                        return "STOP MOVING AND LET ME MELT YOU!!!";
                        case "YamataHead3":
                        return "Down Down DOWN THE VENOM GOES!!! When it will it stop? WHO KNOWS?! NYEHEHEHEHEHEH!!!";
                        case "YamataHead4":
                        return "DIEDIEDIEDIEDIEDIEDIEDIIIIIIIIIIIIIIIE!!!";
                        case "YamataHead5":
                        return "BAM! BOOM! I'LL BLOW YOU INTO NEXT SUNDAY!!!";
                        case "YamataHead6":
                        return "NGAAAAAAAAAAAAAAAAAH!!!";
                        case "YamataHead7":
                        return "GET THEM! EAT THEM! JUST GET ";
                        case "YamataHead8":
                        return " OUT OF MY FACE!!!";
                        case "YamataHead9":
                        return "I�VE EATEN RABBITS MORE INTIMIDATING THAN YOU!";
                        case "YamataHead10":
                        return "HOPE YOU BROUGHT YOUR UMBRELLA! BECAUSE IT�S RAINING PAIN!!! NYEHEHEHEHEHEHEHEH!!!";
                        case "YamataHead11":
                        return "DOWN COMES THE VENOM!!!NYEHEHEHEHEHEHEHEH!";
                        case "YamataHead12":
                        return "EAT ECTOPLASM YOU LITTLE WRETCH";
                        case "YamataHead13":
                        return "NYAAAAAAAAAAAH!!!";
                        case "YamataHead14":
                        return "WHOOPS! DROPPED ACID! Hope you're not degradable..!";
                        case "YamataHead15":
                        return "WHOOPS! DROPPED ACID AGAIN! NYEHEHEHEHEHEHEHEHEHEHEHEH";
                        case "YamataHead16":
                        return "NYAAAAAAAH! YOU WON�T LIVE THROUGH THIS ONE!";
                        case "YamataHead17":
                        return "COME ON!!! STAND STILL SO I CAN BLOW YOU TO MARS!";
                        case "YamataHead18":
                        return "NGAAAAAAAAAAAAAH STAAAAAAAHP MOOOOOOOOVIIIIIIING!!!!!";
                        case "YamataHead19":
                        return "HAVE A HEALTHY TASTE OF ACID!";
                        case "YamataHead20":
                        return "I'M GONNA RIP YOU TO PIECES YOU LITTLE WRETCH!!!";
                        case "YamataHead21":
                        return "REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE!!!";

                        case "Sagittarius1":
                        return "target(s) neutralized. returning to stealth mode.";
                        case "Sagittarius2":
                        return "target(s) lost. returning to stealth mode.";
                        case "SagittariusFree1":
                        return "target(s) neutralized. returning to stealth mode.";
                        case "SagittariusFree2":
                        return "target(s) lost. returning to stealth mode.";
                        case "SagittariusFree3":
                        return "initializing repair program.";

                        case "ZeroBoss1":
                        return "PHYSICAL ZER0 UNIT IN CRITICAL C0NDITI0N. DISCARDING AND ENGAGING D00MSDAY PR0T0C0L.";
                        case "ZeroBoss2":
                        return "D00MSDAY PR0T0CALL MALFUNCTI0N. MAIN.EXPERT M0DE = FALSE.";
                        case "ZeroBoss3":
                        return "Doomstone stops glowing. You can now mine it.";
                        case "ZeroBoss4":
                        return "INITIALIZING BACKUP WEAPON PR0T0C0L.";
                        case "ZeroBoss5":
                        return "CRITICAL ERR0R: ARM UNITS NOT FOUND. RER0UTING RES0URCES TO OFFENSIVE PR0T0C0LS. SHIELD L0WERED.";
                        case "ZeroBoss6":
                        return "TARGET NEUTRALIZED. RETURNING T0 0RBIT.";
                        case "ZeroBoss7":
                        return "TARGET L0ST. RETURNING T0 0RBIT.";
                        case "ZeroBoss8":
                        return "INITIATING PR0BE CREATI0N SYSTEM.";
                        case "ZeroBoss9":
                        return "CYCLONE PROTOCOL ENGAGED";
                        case "ZeroBoss10":
                        return "RE-ESTABLISHING WEAP0N UNITS";
                        case "ZeroBoss11":
                        return "RE-wiogn WEAP0N UNITS";

                        case "ZeroAwakened1":
                        return "Doomstone stops glowing. You can now mine it.";
                        case "ZeroAwakened2":
                        return "WARNING. DRASTIC DAMAGE DETECTED, FAILURE IMMINENT. ENGAGE T0TAL 0FFENCE PR0T0C0L";
                        case "ZeroAwakened3":
                        return "WARNING. DRASTIC DAMAGE DETECTED, FAILURE IMMINENT AGAIN. ENGAGE T0TAL 0FFENCE PR0T0C0L 0MEGA";
                        case "ZeroAwakened4":
                        return "CHEATER ALERT CHEATER ALERT. N0 DR0PS 4 U";
                        case "ZeroAwakened5":
                        return "Y0UR CHEAT SHEET BUTCHER T00L WILL N0T SAVE Y0U HERE";
                        case "ZeroAwakened6":
                        return "TARGET NEUTRALIZED. RETURNING T0 0RBIT.";
                        case "ZeroAwakened7":
                        return "TARGET L0ST. RETURNING T0 0RBIT.";
                        case "ZeroDeath1":
                        return "MISSI0N FAILED. SENDING DISTRESS SIGNAL T0 H0ME BASE.";
                        case "ZeroDeath2":
                        return "MISSI0N FAILED. ATTEMPTING DISTRESS SIGNAL AGAIN.";
                        case "ZeroDeath3":
                        return "SENDING...";
                        case "ZeroDeath4":
                        return "DISTRESS SIGNAL RECIEVED.";
                        case "FuryAshe1":
                        return "Papa, NO! You'll PAY for this, ";
                        case "FuryAshe2":
                        return "AGH! Sorry papa..! I gotta bail!";
                        case "WrathHaruka1":
                        return "Father! Rrgh..! Next time we meet, I'll strike you down!";
                        case "WrathHaruka2":
                        return "Ngh...sorry father...I can't carry on...";
                        case "ShenA1":
                        return "I must say, child. You impress me.";
                        case "ShenA2":
                        return "Face it, child! You’ll never defeat the living embodiment of disarray itself!";
                        case "ShenA3":
                        return "You fight, even when the odds are stacked against you.";
                        case "ShenA4":
                        return "You’re still going? How amusing...";
                        case "ShenA5":
                        return "You remind me of myself quite a bit, to be honest...";
                        case "ShenA6":
                        return "Putting up a fight when you know Death is inevitable...";
                        case "ShenA7":
                        return "Maybe some day, you'll have your own realm to rule over...";
                        case "ShenA8":
                        return "Now stop making this hard! Stand still and take it like a man!";
                        case "ShenA9":
                        return "But today, we clash! Now show me what you got!";
                        case "ShenA10":
                        return "DIE ALREADY YOU INSIGNIFICANT LITTLE WORM!!";
                        case "ShenA11":
                        return "Still got it? I'm impressed. Show me your true power!";
                        case "ShenA12":
                        return "WHAT?! HOW HAVE YOU- ENOUGH! YOU WILL KNOW WHAT IT MEANS TO FEEL UNYIELDING CHAOS!";
                        case "ShenA13":
                        return "Come on! KEEP PUSHING!";
                        case "ShenA14":
                        return "NO! I WILL NOT LOSE! NOT TO YOU!";
                        case "ShenA15":
                        return "SHOW ME! SHOW ME THE TRUE POWER YOU HOLD!";
                        case "ShenA16":
                        return "GRAAAAAAAAAH!!!";
                        case "ShenDeath1":
                        return "Split again...";
                        case "ShenDeath2":
                        return "This is YOUR fault you insolent worm..! I knew we should have been more aggressive but NOOOOOOOOO..! YOU said we could squash them without even trying!";
                        case "ShenDeath3":
                        return "Warriors";
                        case "ShenDeath4":
                        return ", you will face our fury again one day...either when we gain enough power again...";
                        case "ShenDeath5":
                        return "...or you decide to use that Sigil again..!";
                        case "ShenDeath6":
                        return "Your choice, child.";
                        case "ShenDeath7":
                        return "YOU IMBECILE! WE LOST! AGAAAAAAAAAAAAIN!!!";
                        case "ShenDeath8":
                        return "Rgh, my head...";
                        case "ShenDeath9":
                        return "BUNCH OF CLOWNS";
                        case "ShenDeath10":
                        return "And YOU, ";
                        case "ShenDeath11":
                        return "! NEXT TIME I'M GONNA TEAR YOUR HEADS OFF!!!";
                        case "ShenDeath12":
                        return "And trust us, kid.";
                        case "ShenDeath13":
                        return "There's always a next time.";
                        case "ShenDoragon1":
                        return "TERRA MAGIC?! NO! I THOUGHT IT WAS WIPED FROM THE EARTH!";
                        case "ShenDoragon2":
                        return "Grips! Assist me!";
                        case "ShenDoragon3":
                        return "Ashe? Haruka? I need your assistance again..!";
                        case "ShenDoragon4":
                        return "On it, Papa~!";
                        case "ShenDoragon5":
                        return "Again..?";
                        case "ShenDoragon6":
                        return "Girls..? Help your father with this insignificant mortal.";
                        case "ShenDoragon7":
                        return "With pleasure, Papa~!";
                        case "ShenDoragon8":
                        return "Yes, father.";
                        case "ShenDoragon9":
                        return "You are quite persistent, Child. I like that.";
                        case "ShenDoragon10":
                        return "What's this? Competence? I would have never expected...";
                        case "ShenDoragon11":
                        return "True warriors don't show mercy! I won't and I doubt you will either..!";
                        case "ShenDoragon12":
                        return "Give up, child. The world will always fall into chaos!";
                        case "ShenDoragon13":
                        return "SHOW NO MERCY!";
                        case "ShenDoragon14":
                        return "What? You're still fighting? Why?!";
                        case "ShenDoragon15":
                        return "The defeat of a superancient empowers the stonekeepers.";
                        case "ShenDoragon16":
                        return "Heh, alright. I’ll leave you alone I guess. But if you come back stronger, I’ll show you the power of true unyielding chaos...";
                        case "ShenDoragon17":
                        return "The defeat of a superancient empowers the stonekeepers.";
                        case "ShenDoragon18":
                        return "Good show, child, good show. Your combat prowess still impresses me! Maybe some day I'll show you my true power.";
                        case "ShenSpawn1":
                        return "Surprised to see us again, Kid?";
                        case "ShenSpawn2":
                        return "NYEHEHEHEHEH..! Yes..! Must be shocking to see us here..! But this time, we have a little tricksie up our sleeves..!";
                        case "ShenSpawn3":
                        return "That Sigil you just used gave us back our full power, which will let us reach our true, powerful form..!";
                        case "ShenSpawn4":
                        return "We used to be the same being..! But then a Terrarian wretch like you split our soul in half..! But now...heheheh...";
                        case "ShenSpawn5":
                        return "WE ARE COMPLETE AGAIN";
                        case "ShenSpawn6":
                        return "Heh....heheh...";
                        case "ShenSpawn7":
                        return "You've made a grave mistake, child...";
                        case "ShenSpawn8":
                        return "For you see....";
                        case "ShenSpawn9":
                        return "I AM SHEN DORAGON, EMPEROR OF CHAOS AND ANARCHY!";
                        case "ShenSpawn10":
                        return "And you, my child, will face the wrath and fury of chaos itself..!";
                        case "ShenSpawn11":
                        return "DIE!!!";
                        case "ShenTransition1":
                        return "Heh...";
                        case "ShenTransition2":
                        return "Heheheh...";
                        case "ShenTransition3":
                        return "HAHAHAHAHAHAHA!!!";
                        case "ShenTransition4":
                        return "Have you forgotten about our last battles...?";
                        case "ShenTransition5":
                        return "There's always a last stand, kiddo...";
                        case "ShenTransition6":
                        return "I have only been using a fraction of my true power...and now...heheheh...";
                        case "ShenTransition7":
                        return "Shen Doragon has been Awakened!";
                        case "ShenTransition8":
                        return "YOU WILL BURN IN THE FLAMES OF DISCORDIAN HELL!!!";
                    }
                }
            return"";
        }
        public static string BossSummonsInfo(String BossName)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                   switch(BossName)
                    {
                        case "BossAwoken":
                        return "已经苏醒!";
                        case "EquinoxWormawoken":
                        return "昼夜双虫已经苏醒!";
                        case "ChaosRuneYamataFalse":
                        return "一幅凶煞邪魔的图像在你脑海中一闪而过...";
                        case "ChaosRuneAkumaFalse":
                        return "一种七个头的恐惧在你脑海中一闪而过...";
                        case "ChaosRuneFalse":
                        return "哈!我也希望有两个我能把你干翻!";
                        case "ChaosRuneAncientsFalse":
                        return "这个符文没用...";
                        case "ChaosRuneTrue1":
                        return "上神应龙已经苏醒!";
                        case "ChaosRuneTrue2":
                        return "跳过乐趣时间? 懂了, 我欣赏你, 小子";
                        case "ChaosSigilFalse1":
                        return "只有蓝色印迹在发光...";
                        case "ChaosSigilFalse2":
                        return "只有红色印迹在发光...";
                        case "ChaosSigilFalse3":
                        return "哈!我也希望有两个我能把你干翻!";
                        case "ChaosSigilFalse4":
                        return "混沌印迹发着光， 混沌底座的图像在你的脑海中闪现";
                        case "ChaosSigilFalse5":
                        return "这个印迹没用...";
                        case "ChaosSigilTrue1":
                        return "大错特错, 小子...";
                        case "ChaosSigilTrue2":
                        return "哈? ...又来. . ? 好, 让我们把这事安排的明明白白";
                        case "ConfusingMushroomFalse":
                        return "蘑菇发着光, 它的气味让你感到毛骨悚然. ";
                        case "ConfusingMushroomTrue":
                        return "真菌帝准备攻击你. ";
                        case "CuriousClawTrue":
                        return "混沌双爪已经到来!";
                        case "CuriousClawFalse":
                        return "爪子在你手中无力的伸开. 恶心的要死. ";
                        case "CyberneticBellFalse":
                        return "铃铛在响, 什么也没发生. ";
                        case "CyberneticBellTrue":
                        return "侵入者听到了铃声, 即将开始杀戮. ";
                        case "CyberneticClawFalse":
                        return "爪子在你手中无力的伸开. ";
                        case "CyberneticClawTrue":
                        return "捕猎者正准备来狩猎你. ";
                        case "CyberneticShroomFalse":
                        return "别再像个精神病一样挥舞着那个金属蘑菇了. ";
                        case "CyberneticShroomTrue":
                        return "电子松露出现. ";
                        case "DiamondCarrotRajahText":
                        return "致 命 错 误, 泰 拉 人!";
                        case "DiamondCarrotRajahText2":
                        return "给我 康 康 你拿了什么, ";
                        case "DjinnLampDayTimeFalse":
                        return "魔灯在月光下闪烁, 什么也没发生. ";
                        case "DjinnLampDesertFalse":
                        return "你擦灯的时候, 它吐着沙子";
                        case "DjinnLampFalse":
                        return "再怎么擦也没用";
                        case "DraconianDayTimeFalse":
                        return "臭小子. 龙不睡觉吗？ 明天再来。 ";
                        case "DraconianRuneFalse1":
                        return "一幅燎狱顶端焦塔的图像在你脑海一闪而过. ";
                        case "DraconianRuneFalse2":
                        return "嘿, 小子, 你应该知道, 那符文只需要用一次就行. ";
                        case "DraconianRuneInfernoFalse":
                        return "你只能在燎狱使用那个印迹, 小子. ";
                        case "DraconianRuneInfernoFalse2":
                        return "小子， 这个符文必须用在酷煞之日祭坛上。 在燎狱的中央。 ";
                        case "DraconianRuneTrue1":
                        return "你周围的空气开始升温!";
                        case "DraconianRuneTrue2":
                        return "不浪费时间切中要害...? 好吧, 那你准备下地狱吧...!";
                        case "DraconianSigilFalse":
                        return "嘿, 小子, 你应该知道, 那印迹只需要用一次就行. ";
                        case "DraconianSigilInfernoFalse":
                        return "你只能在燎狱使用那个印迹, 小子. ";
                        case "DraconianSignalTrue1":
                        return "呵呵, 我希望你已经准备好感受烈日的愤火了, 小子. ";
                        case "DraconianSignalTrue2":
                        return "又来, 小子？ 你没有更好的事情要做吗？ 你已经打败过我一次了. 好吧, 但我可不会对你心慈手软. ";
                        case "DragonBellDayTimeFalse":
                        return "龙现在已经睡了, 听不见铃铛响. ";
                        case "DragonBellTrue":
                        return "育母炎龙已经被呼唤";
                        case "DragonBellFalse":
                        return "龙不在这, 听不见铃铛响. ";
                        case "DreadRuneTrue1":
                        return "八歧大蛇已经觉醒!";
                        case "DreadRuneTrue2":
                        return "是的, 是的, 我明白了, 我第一阶段很讨厌. 让我们用……来解决这个问题!";
                        case "DreadTimeFalse":
                        return "不！我现在不想战斗！我需要舒服的睡眠！晚上来！";
                        case "DreadFalse1":
                        return "一幅潭渊中心怪树的图像在你脑海一闪而过";
                        case "DreadFalse2":
                        return "你究竟在tm干什么? 我已经在这了!!!";
                        case "DreadMireFalse":
                        return "嘿, 小傻子！潭渊那边走！";
                        case "DreadMireFalse2":
                        return "你必须在潭渊中心的祭坛上使用! 相信我, 不会出事的!";
                        case "DreadSigilTrue1":
                        return "你 胆 敢 进入我的地盘, 泰拉人? !哦呵呵呵呵呵…!大错特错!";
                        case "DreadSigilTrue2":
                        return "再来要更多战利品……? !这一次你可没那么幸运, 你这个小鬼……!";
                        case "SistersInfo1":
                        return "又是你...!? 上次还没学乖? 哦好, 我会用一个球把你炸成碎片!";
                        case "SistersInfo2":
                        return "不管怎样, 让我们结束这一切吧. ";
                        case "SistersDownedInfo1":
                        return "咳……再来一次……";
                        case "SistersDownedInfo2":
                        return "这 次 我 不 会 输!你会感觉到失败的味道, 你这可恶的常温动物!";
                        case "HydraChowTimeFalse":
                        return "什么也没发生. 潭渊的生物在睡觉. ";
                        case "HydraChowTrue":
                        return "九头蛇想吃那个食物. ";
                        case "HydraChowFalse":
                        return "什么也没发生. 一个拿着又臭又黏糊的球的你宛如一个智障. ";
                        case "InterestingClawTrue":
                        return "混沌双爪已经到来!";
                        case "InterestingClawFalse":
                        return "爪子在你手中无力的伸开. 恶心的要死. ";
                        case "IntimidatingMushroomFalse":
                        return "蘑菇和你大眼瞪小眼, 让你浑身发冷. ";
                        case "IntimidatingMushroomTrue":
                        return "赤孢皇正准备踩扁你";
                        case "LifescannerFalse":
                        return "生命扫描仪没起作用. ";
                        case "ScrapHeapFalse":
                        return "你用这个东西会有静电刺激感. 大概它在发信号? ";
                        case "ScrapHeapTrue":
                        return "双头狗要把这东西和你都吃了";
                        case "SubzeroCrystalTimeFalse":
                        return "水晶呆在那, 正在阳光下融化";
                        case "SubzeroCrystalSnowZoneFalse":
                        return "水晶在它里面显示出一幅附近冰地生物群的图像";
                        case "SubzeroCrystalTrue":
                        return "绝零冰蛇继续攻击";
                        case "ToadstoolFalse":
                        return "蟾蜍臭菇呱呱叫...? ";
                        case "ToadstoolTrue":
                        return "松露蟾蜍呱呱叫";
                        case "ZeroFalse":
                        return "错 误。 零 单 元 已 经 激 活. 请 稍 后 重 新 尝 试. ";
                        case "ZeroVoidZoneFalse":
                        return "错 误。 读 取 代 码PLAYER. GETM0DPLAYER<AAPLAYER>(M0D). Z0NEV0ID == FALSE. 请 稍 后 重 新 尝 试. ";
                        case "ZeroRuneTrue":
                        return "末 日 协 议 手 动 激 活. 终 结 系 统 全 功 率 充 能. ";
                        case "ZeroTesseractTrue":
                        return "零 单 元 启 动. 加 载 毁 灭 者 协 议. ";
                        case "ZeroTesseractDownedTrue":
                        return "目 标 锁 定. 本 次 终 结 你 失 败 的 几 率 为 零, 泰 拉 人. ";
                        case "RajahGlobalInfo1":
                        return "杀害无辜者必须受到惩罚!";
                        case "RajahGlobalInfo2":
                        return "你犯了不可饶恕的罪!我要把你从这个凡人的国度里抹去!准备好真正的痛苦和惩罚吧, ";
                        case "RajahGlobalInfo3":
                        return "一个愤怒的生物的眼睛注视着你…";
                        case "RoyalRabbit1":
                        return "杀害无辜者必须受到惩罚!";
                        case "RoyalRabbit2":
                        return "你要为你的罪过付出代价, ";
                    }
                }
            else
                {
                    switch(BossName)
                    {
                        case "BossAwoken":
                        return " have awoken!";
                        case "EquinoxWormawoken":
                        return "The Equinox Worms have awoken!";
                        case "ChaosRuneYamataFalse":
                        return "The imagery of a blazing demon flashes through your mind...";
                        case "ChaosRuneAkumaFalse":
                        return "The imagery of a 7 headed terror flashes through your mind...";
                        case "ChaosRuneFalse":
                        return "HAH! I WISH there were two of me to smash you into the ground!";
                        case "ChaosRuneAncientsFalse":
                        return "The Rune does nothing...";
                        case "ChaosRuneTrue1":
                        return "Shen Doragon has been Awakened!";
                        case "ChaosRuneTrue2":
                        return "Skipping to the fun part, I see? I like you, child.";
                        case "ChaosSigilFalse1":
                        return "Only the blue half of the sigil is lit up...";
                        case "ChaosSigilFalse2":
                        return "Only the red half of the sigil is lit up...";
                        case "ChaosSigilFalse3":
                        return "HAH! I WISH there were two of me to smash you into the ground!";
                        case "ChaosSigilFalse4":
                        return "The Chaos Sigil glows, and imagery of the chaos pedestals flash through your mind";
                        case "ChaosSigilFalse5":
                        return "The sigil does nothing...";
                        case "ChaosSigilTrue1":
                        return "Big mistake, child...";
                        case "ChaosSigilTrue2":
                        return "Hmpf...again..? Alright, let's just get this done and overwith.";
                        case "ConfusingMushroomFalse":
                        return "The mushroom glows, and the smell of it makes you feel loopy.";
                        case "ConfusingMushroomTrue":
                        return "The Feudal Fungus keeps trying to attack you";
                        case "CuriousClawTrue":
                        return "The Grips of Chaos are already here!";
                        case "CuriousClawFalse":
                        return "The claw lays limp in your hand. Nasty.";
                        case "CyberneticBellFalse":
                        return "The bell rings, but nothing happens.";
                        case "CyberneticBellTrue":
                        return "The Raider hears the bell and keeps attempting to kill you";
                        case "CyberneticClawFalse":
                        return "The claw just lays limp in your hand.";
                        case "CyberneticClawTrue":
                        return "The Retriever is still trying to grab you";
                        case "CyberneticShroomFalse":
                        return "Stop waving that metal mushroom around like a psychopath.";
                        case "CyberneticShroomTrue":
                        return "The Techno Truffle exists.";
                        case "DiamondCarrotRajahText":
                        return "GRAVE MISTAKE, TERRARIAN!";
                        case "DiamondCarrotRajahText2":
                        return "Show me what you got, ";
                        case "DjinnLampDayTimeFalse":
                        return "The lamp shimmers in the moonlight, yet does nothing";
                        case "DjinnLampDesertFalse":
                        return "The lamp spits out sand as you rub it";
                        case "DjinnLampTrue":
                        return "No ammount of rubbing the lamp will save you here";
                        case "DraconianDayTimeFalse":
                        return "Geez, kid. Can't a dragon get a little shut-eye? Come back in the morning.";
                        case "DraconianRuneFalse1":
                        return "An image of the scorched tower at the peak of the inferno flashes through your mind";
                        case "DraconianRuneFalse2":
                        return "Hey kid, that rune only works once, ya know.";
                        case "DraconianRuneInfernoFalse":
                        return "You can only use that rune in the Inferno, kid.";
                        case "DraconianRuneInfernoFalse2":
                        return "That sigil has to be used at the Altar of the Draconian Sun, kid. It's in the middle of the inferno.";
                        case "DraconianRuneTrue1":
                        return "The air around you begins to heat up...";
                        case "DraconianRuneTrue2":
                        return "Cutting right to the chase I see..? Alright then, prepare for hell..!";
                        case "DraconianSigilFalse":
                        return "Hey kid, that Sigil only works once, ya know.";
                        case "DraconianSigilInfernoFalse":
                        return "You can only use that Sigil in the Inferno, kid.";
                        case "DraconianSignalTrue1":
                        return "Heh, I hope you’re ready to feel the fury of the blazing sun kid.";
                        case "DraconianSignalTrue2":
                        return "Back for more, kid? Don’t you have better things to do? You already beat me once.  Alright, but I won’t go easy on you.";
                        case "DragonBellDayTimeFalse":
                        return "The bell rings on deaf ears. The dragons are asleep now.";
                        case "DragonBellTrue":
                        return "The Broodmother has already been called";
                        case "DragonBellFalse":
                        return "The bell rings on deaf ears. The dragons are not here.";
                        case "DreadRuneTrue1":
                        return "Yamata has been Awakened!";
                        case "DreadRuneTrue2":
                        return "Yeah, yeah I get it, my first phase is obnoxious. Let�s just get this over with..!";
                        case "DreadTimeFalse":
                        return "NO! I DON'T WANNA FIGHT NOW! I NEED MY BEAUTY SLEEP! COME BACK AT NIGHT!";
                        case "DreadFalse1":
                        return "An image of the strange tree at the heart of the mire flashes through your mind";
                        case "DreadFalse2":
                        return "WHAT THE HELL ARE YOU DOING?! I'M ALREADY HERE!!!";
                        case "DreadMireFalse":
                        return "Hey Dumbo! Mire is that way!";
                        case "DreadMireFalse2":
                        return "You NEED to use that sigil on the altar at the center of the mire! Trust me, nothing bad will happen!";
                        case "DreadSigilTrue1":
                        return "You DARE enter my territory, Terrarian?! NYEHEHEHEHEH..! Big mistake..!";
                        case "DreadSigilTrue2":
                        return "Back for more..?! This time you won�t be so lucky you little whelp..!";
                        case "SistersDownedInfo1":
                        return "Again..!? Didin't you learn from last time? Oh well, I'm gonna have a ball blasting you to shreds!";
                        case "SistersDownedInfo2":
                        return "Whatever, let's just get this over with.";
                        case "SistersInfo1":
                        return "Sigh... Here we go again...";
                        case "SistersInfo2":
                        return "THIS TIME I'M NOT LOSING! You're gonna feel the taste of defeat you disgusting warm-blood!";
                        case "HydraChowTimeFalse":
                        return "Nothing is coming. The creatures of the Mire sleep.";
                        case "HydraChowTrue":
                        return "The Hydra wants that food.";
                        case "HydraChowFalse":
                        return "Nothing is coming. Now you look dumb holding out this smelly ball of gunk.";
                        case "InterestingClawTrue":
                        return "The Grips of Chaos are already here!";
                        case "InterestingClawFalse":
                        return "The claw lays limp in your hand. Nasty.";
                        case "IntimidatingMushroomFalse":
                        return "The mushroom just glares at you and gives you chills just looking at it.";
                        case "IntimidatingMushroomTrue":
                        return "The mushroom Monarch keeps trying to stomp you";
                        case "LifescannerFalse":
                        return "The Lifescanner doesn't do anything.";
                        case "ScrapHeapFalse":
                        return "You feel a static shock from using this. Maybe it's trying to send a signal?";
                        case "ScrapHeapTrue":
                        return "Orthrus wants to eat that AND you";
                        case "SubzeroCrystalTimeFalse":
                        return "The crystal just sits there, melting in the sun";
                        case "SubzeroCrystalSnowZoneFalse":
                        return "The crystal shows an image of the nearby ice biome inside of it";
                        case "SubzeroCrystalTrue":
                        return "The Subzero Serpent continues to attack";
                        case "ToadstoolFalse":
                        return "The toadstool croaks..?";
                        case "ToadstoolTrue":
                        return "The Truffle Toad croaks";
                        case "ZeroFalse":
                        return "ERR0R. ZER0 UNIT ALREADY ACTIVE. PLEASE TRY AGAIN LATER.";
                        case "ZeroVoidZoneFalse":
                        return "ERR0R. PLAYER.GETM0DPLAYER<AAPLAYER>(M0D).Z0NEV0ID == FALSE. PLEASE TRY AGAIN LATER.";
                        case "ZeroRuneTrue":
                        return "D00MSDAY PR0T0CALL ACTIVATED MANUALLY. TERMINATI0N SYSTEMS AT FULL P0WER";
                        case "ZeroTesseractTrue":
                        return "ZER0 UNIT ACTIVATED. ENGAGE D00MBRINGER PR0T0C0L.";
                        case "ZeroTesseractDownedTrue":
                        return "TARGET L0CKED. FAILURE T0 TERMINATE Y0U IS N0T A P0SSIBILITY THIS TIME, TERRARIAN.";
                        case "RajahGlobalInfo1":
                        return "Those who slaughter the innocent must be PUNISHED!";
                        case "RajahGlobalInfo2":
                        return "YOU HAVE COMMITTED AN UNFORGIVABLE SIN! I SHALL WIPE YOU FROM THIS MORTAL REALM! PREPARE FOR TRUE PAIN AND PUNISHMENT, ";
                        case "RajahGlobalInfo3":
                        return "The eyes of a wrathful creature gaze upon you...";
                        case "RoyalRabbit1":
                        return "Those who slaughter the innocent must be PUNISHED!";
                        case "RoyalRabbit2":
                        return "YOU WILL PAY FOR YOUR SINS, ";
                    }
                }
            return"";
        }
        public static string BossCheck(String Boss)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(Boss)
                    {
                        case "Usean":
                        return "使用 ";
                        case "Usea":
                        return "使用 ";
                        case "Usethe":
                        return "使用 ";
                        case "or":
                        return " 或者 ";
                        case "atnight":
                        return " 在晚上召唤";
                        case "MushroomMonarch":
                        return "赤孢皇";
                        case "MushroomMonarchInfo":
                        return " 在白天, 或者地表蘑菇地召唤";
                        case "FeudalFungus":
                        return "真菌帝";
                        case "FeudalFungusInfo":
                        return " 在荧光蘑菇地, 或者在晚上召唤";
                        case "GripsofChaos":
                        return "混沌双爪";
                        case "Broodmother":
                        return "育母炎龙";
                        case "BroodmotherInfo":
                        return " 白天期间在燎狱召唤, 或者破坏火山下3个龙蛋";
                        case "Hydra":
                        return "九头渊蛇";
                        case "HydraInfo":
                        return " 夜晚在潭渊召唤, 或者破坏潭渊下3个蒴荚";
                        case "SubzeroSerpent":
                        return "绝零冰蛇";
                        case "SubzeroSerpentInfo":
                        return " 在夜晚雪地召唤";
                        case "DesertDjinn":
                        return "沙漠巨灵";
                        case "DesertDjinnInfo":
                        return " 白天在沙漠召唤";
                        case "Sagittarius":
                        return "虚空人马";
                        case "SagittariusInfo":
                        return " 在虚空浮岛召唤";
                        case "TruffleToad":
                        return "松露蟾蜍";
                        case "TruffleToadInfo":
                        return " 在荧光蘑菇环境召唤";
                        case "Greed":
                        return "金食饕餮";
                        case "GreedInfo":
                        return "在欲望金窟的原欲祭坛处召唤.";
                        case "Athena":
                        return "天城鸟妖神";
                        case "AthenaInfo":
                        return " 在天穹卫城的天鸮祭坛上召唤.";
                        case "AthenaA":
                        return "鸟妖神的复仇";
                        case "AthenaAInfo":
                        return " 在天穹卫城的天鸮祭坛上召唤, 月球领主后";
                        case "Retriever":
                        return "捕猎者-电子猎犬爪";
                        case "TechnoTruffle":
                        return "电子化-机械松露怪";
                        case "RaiderUltima":
                        return "侵入者-创世哺育之母";
                        case "OrthrusX":
                        return "食腐者-双头狗俄耳托斯X型";
                        case "RajahRabbit":
                        return "巨兔王公";
                        case "RajahRabbitInfo":
                        return " 或者像一个无耻混蛋 一样杀100只兔子";
                        case "NightcrawlerDaybringer":
                        return "昼夜双虫";
                        case "SistersofDiscord":
                        return "(远古者)混沌姐妹";
                        case "Yamata":
                        return "(远古者)八歧大蛇";
                        case "YamataInfo":
                        return " 在夜晚潭渊召唤";
                        case "Akuma":
                        return "(远古者)邪鬼巨龙";
                        case "AkumaInfo":
                        return " 在白天燎狱召唤";
                        case "Zero":
                        return "(远古者)零械单元";
                        case "ZeroInfo":
                        return " 在浮岛虚空召唤";
                        case "ShenDoragon":
                        return "(原古者)上神应龙";
                        case "RajahRabbitRevenge":
                        return "(原古者)至尊巨兔王公";
                        case "RajahRabbitRevengeInfo":
                        return " 或者总计杀死1000只 兔子后, 每杀死100只兔子";
                    }
                }
            else
                {
                    switch(Boss)
                    {
                        case "Usean":
                        return "Use an ";
                        case "Usea":
                        return "Use a ";
                        case "Usethe":
                        return "Use the ";
                        case "or":
                        return " or ";
                        case "atnight":
                        return " at night";
                        case "MushroomMonarch":
                        return "The Mushroom Monarch";
                        case "MushroomMonarchInfo":
                        return " during the day in the Surface Mushroom Biome";
                        case "FeudalFungus":
                        return "The Feudal Fungus";
                        case "FeudalFungusInfo":
                        return " in a Glowing Mushroom Biome or at night";
                        case "GripsofChaos":
                        return "Grips of Chaos";
                        case "Broodmother":
                        return "The Broodmother";
                        case "BroodmotherInfo":
                        return " in the Inferno during the day";
                        case "Hydra":
                        return "Hydra";
                        case "HydraInfo":
                        return " in the Mire at night";
                        case "SubzeroSerpent":
                        return "Subzero Serpent";
                        case "SubzeroSerpentInfo":
                        return " in the Snow biome at night";
                        case "DesertDjinn":
                        return "Desert Djinn";
                        case "DesertDjinnInfo":
                        return " in the Desert during the day";
                        case "Sagittarius":
                        return "Sagittarius";
                        case "SagittariusInfo":
                        return " in the Void";
                        case "TruffleToad":
                        return "Truffle Toad";
                        case "TruffleToadInfo":
                        return " in a glowing mushroom biome";
                        case "Athena":
                        return "Athena";
                        case "Greed":
                        return "Greed";
                        case "GreedInfo":
                        return " at the Altar of Desire in the Hoard.";
                        case "AthenaInfo":
                        return " at the owl altar in the Acropolis.";
                        case "AthenaA":
                        return "Athena Rematch";
                        case "AthenaAInfo":
                        return " at the owl altar in the Acropolis Post-moon lord.";
                        case "Retriever":
                        return "Retriever";
                        case "TechnoTruffle":
                        return "Techno Truffle";
                        case "RaiderUltima":
                        return "Raider Ultima";
                        case "OrthrusX":
                        return "Orthrus X";
                        case "RajahRabbit":
                        return "Rajah Rabbit";
                        case "RajahRabbitInfo":
                        return " or kill 100 Rabbits like a jerk.";
                        case "NightcrawlerDaybringer":
                        return "Nightcrawler & Daybringer";
                        case "SistersofDiscord":
                        return "Sisters of Discord";
                        case "Yamata":
                        return "Yamata";
                        case "YamataInfo":
                        return " in the Mire at night";
                        case "Akuma":
                        return "Akuma";
                        case "AkumaInfo":
                        return " in the Inferno during the day";
                        case "Zero":
                        return "Zero";
                        case "ZeroInfo":
                        return " in the Void";
                        case "ShenDoragon":
                        return "Shen Doragon";
                        case "RajahRabbitRevenge":
                        return "Rajah Rabbit's Revenge";
                        case "RajahRabbitRevengeInfo":
                        return " or every 100 rabbit kills after 1000.";
                    }
                }
            return"";
        }
        public static string ItemsInfo(String Itemsname)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(Itemsname)
                    {
                        case "GoblinDollInfo":
                        return "灵魂宝石在你手中具象化";
                        case "GoblinSoulBuyprice":
                        return "购买价格:";
                        case "GoblinSoul":
                        return "哥布林亡魂";
                        case "GoblinSouls":
                        return "哥布林亡魂";
                        case "DemiseEXInfo":
                        return "被击中的敌人灵魂爆裂";
                    }
                }
            else
                {
                    switch(Itemsname)
                    {
                        case "GoblinDollInfo":
                        return "The soul stone materializes in your hand";
                        case "GoblinSoulBuyprice":
                        return "Buy price:";
                        case "GoblinSoul":
                        return "Goblin Soul";
                        case "GoblinSouls":
                        return "Goblin Souls";
                        case "DemiseEXInfo":
                        return "The struck enemy's soul bursts";
                    }
                }
            return"";
        }
        public static string RajahSPTooltip(String RajahSash)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(RajahSash)
                    {
                        case "Melee":
                        return "近战";
                        case "Ranged":
                        return "远程";
                        case "Magic":
                        return "魔法";
                        case "Summoning":
                        return "召唤";
                        case "Throwing":
                        return "投掷";
                        case "DamageType":
                        return "伤害类型";
                        case "CurrentDamageBoost:+":
                        return "当前伤害提升: +";
                        case "CurrentSpeedBoost:":
                        return "当前速度增加: ";
                        case "CurrentDamageResistance:":
                        return "当前伤害减免增加: ";
                        case "Damage":
                        return "伤害";
                    }
                }
            else
                {
                    switch(RajahSash)
                    {
                        case "Melee":
                        return "Melee";
                        case "Ranged":
                        return "Ranged";
                        case "Magic":
                        return "Magic";
                        case "Summoning":
                        return "Summoning";
                        case "Throwing":
                        return "Throwing";
                        case "DamageType":
                        return "Damage Type";
                        case "CurrentDamageBoost:+":
                        return "Current Damage Boost: +";
                        case "CurrentSpeedBoost:":
                        return "Current Speed Boost: ";
                        case "CurrentDamageResistance:":
                        return "Current Damage Resistance: ";
                        case "Damage":
                        return " Damage";
                    }
                }
            return"";
        }
        public static string GreedTooltip(String Greed)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(Greed)
                    {
                        case "CurrentDamageBoost:+":
                        return "当前伤害提升: +";
                    }
                }
            else
                {
                    switch(Greed)
                    {
                        case "CurrentDamageBoost:+":
                        return "Current Damage Boost: +";
                    }
                }
        }
        public static string questFish(String questFishtext)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(questFishtext)
                    {
                        case "Fishmother":
                        return "好, 那么, 我正穿过燎狱寻找一些热的东西来搞我的传奇恶作剧, 恰巧我看到了这个水下大胖鱼把它周围的水都煮沸了. 我感觉 '完美!' 但是我没法抓住它因为我没带鱼竿. 去抓住它, 我的仆人.";
                        case "FishmotherLocation":
                        return "燎狱的任何位置";
                        case "TriHeadedKoi":
                        return "我讨厌潭渊. 在我睡觉时间以外什么也看不见, 但是我知道那有一条杀人鱼, 那东西有, 三 个 头. 你能去给我抓来吗?";
                        case "TriHeadedKoiLocation":
                        return "潭渊的任何位置";
                    }
                }
            else
                {
                    switch(questFishtext)
                    {
                        case "Fishmother":
                        return "Okay so, I was walking through the Inferno looking for something hot for one of my epic pranks, when I saw this fat fish underwater, boiling the water around it. I thought 'PERFECT!' but I couldn't catch it because I didn't have my rod on me. Go get it, slave.";
                        case "FishmotherLocation":
                        return "Caught anywhere in the Inferno";
                        case "TriHeadedKoi":
                        return "I hate the Mire. I can't see a thing in there unless it's past my bedtime, but I know there's a killer fish in there that I want that has, get this, THREE HEADS. Can you go get it for me?";
                        case "TriHeadedKoiLocation":
                        return "Caught anywhere in the Mire";
                    }
                }
            return"";
        }
        public static string TilesInfo(String Tiles)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch(Tiles)
                    {
                        case "Spawnpointremoved":
                        return "出生点移除!";
                        case "Spawnpointset":
                        return "出生点设置!";
                        case "DracoAltar1":
                        return "臭小子. 龙不睡觉吗? 明天再来. ";
                        case "DracoAltar2":
                        return "嘿, 小子, 你应该知道, 那印迹只用一次. ";
                        case "DracoAltar3":
                        return "呵呵, 我希望你准备好感受烈日的愤火, 小子";
                        case "DracoAltar4":
                        return "又来, 小子? 你没有更好的事情要做吗? 你已经打败过我一次了. 好吧, 但我可不会对你心慈手软. ";
                        case "DreadAltar1":
                        return "不！我现在不想战斗！我需要舒服的睡眠！晚上来！";
                        case "DreadAltar2":
                        return "你究竟在tm干什么? 我已经在这了!!!";
                        case "DreadAltar3":
                        return "你 胆 敢 进入我的地盘, 泰拉人? !哦呵呵呵呵呵…!大错特错!";
                        case "DreadAltar4":
                        return "再来要更多战利品……? !这一次你可没那么幸运, 你这个小鬼……!";
                        case "DragonEgg1":
                        return "龙蛋破碎的声音在火山中回响… ";
                        case "DragonEgg2":
                        return "你听到远处的咆哮…";
                        case "HydraPod1":
                        return "有东西从破碎的蒴荚中溢出. 真恶心. ";
                        case "HydraPod2":
                        return "你听到附近传来嘶嘶声";
                    }
                }
            else
                {
                    switch(Tiles)
                    {
                        case "Spawnpointremoved":
                        return "Spawn point removed!";
                        case "Spawnpointset":
                        return "Spawn point set!";
                        case "DracoAltar1":
                        return "Geez, kid. Can't a dragon get a little shut-eye? Come back in the morning.";
                        case "DracoAltar2":
                        return "Hey kid, that Sigil only works once, ya know.";
                        case "DracoAltar3":
                        return "Heh, I hope you�re ready to feel the fury of the blazing sun kid.";
                        case "DracoAltar4":
                        return "Back for more, kid? Don�t you have better things to do? You already beat me once.  Alright, but I won�t go easy on you.";
                        case "DreadAltar1":
                        return "NO! I DON'T WANNA FIGHT NOW! I NEED MY BEAUTY SLEEP! COME BACK AT NIGHT!";
                        case "DreadAltar2":
                        return "WHAT THE HELL ARE YOU DOING?! I'M ALREADY HERE!!!";
                        case "DreadAltar3":
                        return "You DARE enter my territory, Terrarian?! NYEHEHEHEHEH..! Big mistake..!";
                        case "DreadAltar4":
                        return "Back for more..?! This time you won�t be so lucky you little whelp..!";
                        case "DragonEgg1":
                        return "The sound of the egg breaking echoes through the volcano...";
                        case "DragonEgg2":
                        return "You hear a distant roar...";
                        case "HydraPod1":
                        return "The contents spill from the broken pod. Nasty.";
                        case "HydraPod2":
                        return "You hear hissing somewhere nearby";

                    }
                }
            return"";
        }
        public static string ArmorBonus(String Armorname)
        {
            if (Language.ActiveCulture == GameCulture.Chinese)
                {
                    switch (Armorname)
                    {
                        case "AbyssalBonus":
                        return @"你的远程攻击具有九头蛇毒的毒性
减少20%弹药消耗
敌人减少瞄准你的可能性";
                        case "AssassinBonus":
                        return  @"悄声走近, 突然刺杀
增加18%远程/近战伤害
减少25%弹药消耗
双击下键进入隐形状态
隐形状态下移动不受阻碍
隐形状态下近战和远程伤害增加";
                        case "AtlanteanBonus":
                        return @"完全处于液体中时, 你的魔法能力和防御力提升";
                        case "BlazingBonus":
                        return @"增加5%伤害减免
敌人更容易瞄准你
你挥动的武器对敌人造成燃烧";
                        case "ChaosFukumenBonus":
                        return "你的远程攻击混合燎狱龙炎和九头蛇毒\n增加25%远程伤害\n敌人更容易瞄准你\n夜视和狩猎效果\n减少25%弹药消耗";
                        case "ChaosHelmBonus":
                        return "你免疫大多数负面效果\n站立不动时快速回复生命值\n你免疫击退";
                        case "ChaosKabutoBonus":
                        return "增加10%近战速度\n敌人更容易瞄准你\n攻击你的敌人受到灼伤\n你挥动武器造成燎狱龙炎和九头蛇毒";
                        case "ChaosMaskBonus":
                        return "完全浸入水中时， 你的魔法能力极大提升\n你可以畅通无阻的游泳";
                        case "ChaosSkullBonus":
                        return "你的远程攻击混合燎狱龙炎和九头蛇毒\n45%几率不消耗弹药\n增加20%远程暴击率\n按下能力键向光标进行一次混沌射击";
                        case "ChaosVisorBonus":
                        return "+4召唤上限\n一个虚空猎手为你而战";
                        case "PerfectChaosHoodBonus":
                        return "“混沌赐予你力量”\n随着你的生命减少， 你的魔法伤害增加， 魔力消耗降低\n你的魔法攻击对敌人造成冥昧业火";
                        case "PerfectChaosKabutoBonus":
                        return "“混沌赐予你力量”\n随着你的生命减少， 你的近战伤害和防御力增加\n你的近战攻击对敌人造成冥昧业火";
                        case "PerfectChaosMaskBonus":
                        return "“混沌赐予你力量”\n随着你的生命减少， 你的召唤物伤害增加\n你的召唤物攻击对敌人造成冥昧业火";
                        case "PerfectChaosVisorBonus":
                        return "“混沌赐予你力量”\n随着你的生命减少， 你的远程伤害和暴击率增加\n你的远程攻击对敌人造成冥昧业火";
                        case "DarkmatterHeaddressBonus":
                        return @"增加6召唤上限
你的召唤物会电击敌人
夜晚增加8%伤害减免";
                        case "DarkmatterHelmBonus":
                        return "增加30%投掷速度和暴击率\r\n你的投掷武器会电击敌人\r\n夜晚增加8%伤害减免";
                        case "DarkmatterHelmetBonus":
                        return "增加15%近战暴击率和近战速度\r\n你的近战武器会电击敌人\r\n夜晚增加8%伤害减免";
                        case "DarkmatterMaskBonus":
                        return "增加200魔力上限\r\n减少20%魔力消耗\r\n你的魔法攻击会电击敌人\r\n夜晚增加8%伤害减免";
                        case "DarkmatterVisorBonus":
                        return "减少25%弹药消耗\r\n增加20%远程暴击率\r\n你的远程武器会电击敌人\r\n夜晚增加8%伤害减免";
                        case "DeathlySkullBonus":
                        return "如死般默然, 使得怪物更少瞄准你\r\n减少20%弹药消耗";
                        case "DemonHoodBonus":
                        return "你的召唤物会让敌人燃烧\r\n你周围总会有一个小恶魔\r\n小恶魔不占用你的召唤上限";
                        case "DepthFukumenBonus":
                        return "怪物减少攻击你的可能性\r\n减少20%弹药消耗\r\n夜视";
                        case "FishDiverMaskABonus":
                        return "“冲洗时间”\r\n你的所有攻击对除了boss以外的敌人造成潮湿\r\n在液体中可以畅快的运动\r\n无限手套可以发挥最大潜力了\r\n你获得了一个“鱼”伙伴";
                        case "SFishDiverMaskABonus":
                        return "“冲洗时间”\r\n你的所有攻击对除了boss以外的敌人造成潮湿\r\n在液体中可以畅快的运动\r\n无限手套可以发挥最大潜力了\r\n你获得了一个“鱼”伙伴";
                        case "MoonHoodABonus":
                        return "“真是讽刺, 不是吗? ”\r\n你如天上明月一般发光\r\n魔法攻击对目标造成蚀月\r\n你有了一个月亮伙伴";
                        case "TiedsMaskABonus":
                        return "“苏联人对我一无所知”\n你所有的近战攻击都会造成燎狱咒焰\n一个闹灵伙伴为你照明";
                        case "DoomiteVisorBonus":
                        return "+1召唤上限\r\n一个虚空搜索者为你而战";
                        case "DoomiteUHelmBonus":
                        return "增加召唤物击退";
                        case "DoomsdayMaskBonus":
                        return "生命终结系统启动\r\n你可以探测到周围所有的敌对生物\r\n在黑暗中更容易看到\r\n你的召唤物对敌人造成一段时间的防御力削弱\r\n+5召唤上限";
                        case "DoomsdayHelmetBonus":
                        return "生命终结系统启动\r\n你可以探测到周围所有的敌对生物\r\n在黑暗中更容易看到\r\n你的魔法攻击对敌人造成一段时间的防御力削弱\r\n减少30%魔力消耗";
                        case "DracoHelmBonus":
                        return "“狂焰之怒充满了你的身体”\r\n你免疫所有冰系debuff\r\n你的灵魂灼烧如同狂焰之怒\r\n你的近战攻击对目标造成破晓";
                        case "DreadHelmBonus":
                        return "“沉渊之怒让你大发雷霆”\r\n你免疫所有火系debuff\r\n你如天上恐惧之月般发光\r\n你的远程伤害对目标造成蚀月";
                        case "DynaskullBonus":
                        return "你的远程弹药相当强大, 震荡力使得目标混乱";
                        case "FleshrendHelmBonus":
                        return "受到敌人攻击会产生血泉喷裂对周围敌人造成伤害\r\n提高生命回复速度";
                        case "FulguriteHelmBonus":
                        return "受到攻击时从你的身体中爆发一阵闪电, 击退敌人\r\n减少20%魔法消耗";
                        case "FulguriteHelmetBonus":
                        return "受到攻击时从你的身体中爆发一阵闪电, 击退敌人\r\n增加20%近战速度和移动速度";
                        case "FulguriteVisorBonus":
                        return "受到攻击时从你的身体中爆发一阵闪电, 击退敌人\r\n25%几率不消耗弹药";
                        case "ShroomHatBonus":
                        return "你对法力病免疫";
                        case "GoblinSlayerHelmBonus":
                        return "对哥布林增加500%伤害和击退\r\n增加80%对哥布林的伤害减免\r\n“我只想猎杀哥布林”";
                        case "HoodlumHoodBonus":
                        return "生命值低于一半时， 移动速度、近战伤害和召唤伤害将增加50%";
                        case "HeartstoneHelmetBonus":
                        return "生命值低于40时, 伤害加倍";
                        case "ImpHoodBonus":
                        return "你的召唤物对敌人造成燃烧";
                        case "LeviathanHelmBonus":
                        return "生命值低于一半时， 移动速度、远程战伤害和魔法伤害将增加50%";
                        case "KindledKabutoBonus":
                        return "增加5%伤害减免\r\n你挥动的武器对敌人造成灼伤";
                        case "MadnessVisorBonus":
                        return "增加5%伤害";
                        case "MadTitanChestplateBonus":
                        return "无限手套现在正是最强之时\r\n“疯狂泰坦的力量就在你的指尖”\r\n(译者注：你也是灭霸? )";
                        case "MadTitanVestBonus":
                        return "无限手套现在正是最强之时\r\n“疯狂泰坦的力量就在你的指尖”\r\n(译者注：你也是灭霸? )";
                        case "MechHelmBonus":
                        return "增加10%伤害减免";
                        case "MushiumHatBonus":
                        return "药水病的时间减少25%";
                        case "NightmareHelmetBonus":
                        return "增加10%移动速度";
                        case "NightsHelmBonus":
                        return "受到敌人攻击让你获得速度狂潮, 短时间内提高移动速度\r\n增加22%移动速度";
                        case "OceanHelmBonus":
                        return "完全处于水中时增加20%魔法伤害并且减少15%魔力消耗";
                        case "OroborosHelmetBonus":
                        return "+3防御";
                        case "AdamantitePaintBonus":
                        return "+4召唤上限";
                        case "ChlorophytePaintBonus":
                        return "+6召唤上限\r\n向周围的敌人发射水晶叶";
                        case "CobaltPaintBonus":
                        return "+2召唤上限";
                        case "FulguritePaintBonus":
                        return "受到攻击时从你的身体中爆发一阵闪电, 击退敌人\r\n+5召唤上限";
                        case "HallowedPaintBonus":
                        return "+6召唤上限";
                        case "MythrilPaintBonus":
                        return "+3召唤上限";
                        case "OrichalcumPaintBonus":
                        return "花瓣会落在你的目标身上, 造成额外伤害\r\n+2召唤上限";
                        case "PalladiumPaintBonus":
                        return "+1召唤上限\r\n击中敌人后, 极大增加生命回复";
                        case "TechneciumPaintBonus":
                        return "+4召唤伤害\r\n攻击敌人使你进行静电蓄能\n当你充能达到4级或者你受到攻击时蓄能将会释放";
                        case "TerraPaintBonus":
                        return "+7召唤上限";
                        case "TitaniumPaintBonus":
                        return "+4召唤上限\r\n你的召唤物攻击敌人后会短暂无敌";
                        case "UraniumPaintBonus":
                        return "+3召唤上限\r\n靠近你的敌人受到你盔甲的辐射灼伤";
                        case "YttriumPaintBonus":
                        return "+2召唤上限\r\n你可以进行闪电般的冲刺";
                        case "Paladin_HelmetBonus":
                        return "+100生命上限\r\n获得圣骑士盾效果";
                        case "RadiumHatBonus":
                        return "+7召唤上限\r\n受到攻击会有星星从天堂坠落并增加你的移动速度\r\n白天增加30%移动速度";
                        case "RadiumHeadgearBonus":
                        return "减少25%弹药消耗\r\n增加20%远程暴击率\r\n受到攻击会有星星从天堂坠落并增加你的移动速度";
                        case "RadiumHelmBonus":
                        return "增加30%投掷暴击率和速度\r\n受到攻击会有星星从天堂坠落并增加你的移动速度\r\n白天增加30%移动速度";
                        case "RadiumHelmetBonus":
                        return "增加15%近战暴击率和速度\r\n受到攻击会有星星从天堂坠落并增加你的移动速度\r\n白天增加30%移动速度";
                        case "RadiumMaskBonus":
                        return "+200魔力上限\r\n减少20%魔力消耗\r\n受到攻击会有星星从天堂坠落并增加你的移动速度\r\n白天增加30%移动速度";
                        case "RaiderChestBonus":
                        return "你不会被击退\r\n随着你的生命值降低, 你的伤害减免将提高";
                        case "StarcloudHeadgearBonus":
                        return "生命值低于60时, 增加100%召唤物和魔法伤害";
                        case "StarcrystalHelmBonus":
                        return "魔法低于20%最大值时, 伤害变成2倍";
                        case "TechneciumHelmetBonus":
                        return "攻击敌人使你进行静电蓄能\n当你充能达到4级或者你受到攻击时蓄能将会释放";
                        case "TechneciumMaskBonus":
                        return "攻击敌人使你进行静电蓄能\n当你充能达到4级或者你受到攻击时蓄能将会释放";
                        case "TechneciumVisorBonus":
                        return "攻击敌人使你进行静电蓄能\n当你充能达到4级或者你受到攻击时蓄能将会释放";
                        case "TribalHatBonus":
                        return "你有幸获得大自然的馈赠, 让你自动使用魔力药剂\r\n减少30%魔法消耗";
                        case "TrueAbyssalFukumenBonus":
                        return "你的远程攻击具有九头蛇毒的毒性\r\n敌人减少瞄准你的可能性\r\n减少25%弹药消耗\r\n藏于阴影中, 你进入隐身状态";
                        case "TrueAtlanteanHelmBonus":
                        return "完全处于液体中时, 玩家魔法能力大幅度提升\n你可以在水中畅通无阻的游泳";
                        case "TrueBlazingKabutoBonus":
                        return "增加5%伤害减免\r\n敌人更容易瞄准你\r\n攻击你的敌人获得燃烧\r\n你挥动的武器对敌人造成燃烧";
                        case "TrueCopperHelmBonus":
                        return "“你周围的大地魔力赐予你能量”\r\n受到攻击时释放出铜之魂攻击敌人";
                        case "TrueDeathlySkullBonus":
                        return "如死般默然, 使得怪物更少瞄准你\r\n减少25%弹药消耗\r\n死亡是你的灵魂物质化, 复活\r\n重生时, 你获得更多的无敌帧, 但是你的防御降低10\r\n(译者注：原句While Etheral, you have more invincibility frames, but your\r\n defense is reduced by 10, 这里Etheral意味不明)";
                        case "TrueDemonHoodBonus":
                        return "你的召唤物对敌人造成影炎\r\n你周围总会有一个小恶魔\r\n小恶魔不占用你的召唤上限";
                        case "TrueDoomiteVisorBonus":
                        return "+4召唤上限\r\n虚空搜索者为你而战";
                        case "TrueDynaskullBonus":
                        return "你的远程弹药相当强大, 震荡力使得目标混乱\r\n有45%几率不消耗弹药\r\n按下Ability能力键朝你的光标进行一次王骨射击";
                        case "TrueFleshrendHelmBonus":
                        return "受到敌人攻击会产生血泉喷裂对周围敌人造成伤害\r\n极大提高生命回复\r\n近战暴击会吸取敌人的生命治疗自己\r\n你的所有近战攻击产生脓血效果";
                        case "TrueHallowedHelmBonus":
                        return "20%不消耗弹药, 减少魔法消耗, 增加近战和移动速度";
                        case "TrueNightsHelmBonus":
                        return "受到敌人攻击让你获得速度狂潮, 短时间内提高移动速度\r\n增加44%移动速度\r\n杀死敌人有25％的几率使得它们产生诅咒爆发, 对周围的敌人造成伤害\r\n你的近战攻击造成咒焰";
                        case "TrueRaiderPlateBonus":
                        return "你免疫寒冷, 冻结和霜冻debuff\r\n静止时快速回复生命\r\n免疫击退";
                        case "TrueTribalHatBonus":
                        return "你有幸获得大自然的馈赠, 让你自动使用魔力药剂\r\n减少40%魔力消耗\r\n丛林保护着你, 不断在周围产生孢子";
                        case "UraniumHeadgearBonus":
                        return "靠近你的敌人受到你盔甲的辐射灼伤";
                        case "UraniumHoodBonus":
                        return "靠近你的敌人受到你盔甲的辐射灼伤";
                        case "UraniumVisorBonus":
                        return "靠近你的敌人受到你盔甲的辐射灼伤";
                        case "VikingPlateBonus":
                        return "增加4%伤害减免";
                        case "WitchHoodBonus":
                        return "灼热并且燃烧\r\n增加20%魔法/召唤伤害\r\n+4召唤上限\r\n一个狱焰之灵保护着你\r\n越少的法力, 使得狱焰之灵更强大";
                        case "YtriumHeadgearBonus":
                        return "你可以进行闪电般的冲刺";
                        case "YtriumHelmBonus":
                        return "你可以进行闪电般的冲刺";
                        case "YtriumMaskBonus":
                        return "你可以进行闪电般的冲刺";
                    }
                }
            else
                {
                    switch (Armorname)
                    {
                        case "AbyssalBonus":
                        return @"Your ranged attacks are imbued with the poisonous properties of hydra venom
20% decreased ammo consumption
Enemies are less likely to target you";
                        case "AssassinBonus":
                        return  @"Slash and Stalk.
18% increased Melee and Ranged damage
25% decreased ammo consumption
Enemies are less likely to target you
you can do a swift dash";
                        case "AtlanteanBonus":
                        return @"While submerged in liquids, your magic abilities and defense are enhanced";
                        case "BlazingBonus":
                        return @"5% increased damage resistance
Enemies are more likely to target you
Your Swung weapons set your enemies ablaze";
                        case "ChaosFukumenBonus":
                        return @"Your ranged attacks are imbued with the chaos of Dragonfire and Bogtoxin
25% increased ranged damage
Enemies are less likely to target you
Night Vision and hunter effects
25% reduced ammo consumption";
                        case "ChaosHelmBonus":
                        return @"You are immune to most debuffs
You quickly regenerate your HP while standing still
You are immune to knockback";
                        case "ChaosKabutoBonus":
                        return @"10% increased melee speed
Enemies are more likely to target you
Enemies that strike you are set ablaze
Your Swung weapons inflicts them with Bogtoxin and Dragonflame";
                        case "ChaosMaskBonus":
                        return @"While submerged in liquids, your magic abilities are increased drastically
You can swim and liquid does not affect your movement";
                        case "ChaosSkullBonus":
                        return @"Your ranged attacks are imbued with the chaos of Dragonfire and Bogtoxin
45% chance to not consume ammo
20% increased ranged critical chance
Pressing the Ability hotkey fires off a chaos shot towards your cursor";
                        case "ChaosVisorBonus":
                        return @"+4 Minion slots
A void scout hunts down your foes for you";
                        case "PerfectChaosHoodBonus":
                        return @"'Chaos empowers you'
As your health decreases, your magic damage increases and your mana consumption decreases
Your magic attacks raze your oponents with the flames of discordian hell";
                        case "PerfectChaosKabutoBonus":
                        return @"'Chaos empowers you'
As your health decreases, your melee damage and defense increase
Your attacks raze your oponents with the flames of Chaos";
                        case "PerfectChaosMaskBonus":
                        return @"'Chaos empowers you'
As your health decreases, your minion damage increases
Your minions raze your oponents with the flames of discordian hell";
                        case "PerfectChaosVisorBonus":
                        return @"'Chaos empowers you'
As your health decreases, your ranged damage and critical chance increases
Your ranged attacks raze your oponents with the flames of discordian hell";
                        case "DarkmatterHeaddressBonus":
                        return @"Increases max number of minions by 6
Your minions electrocute enemies
8% increased damage resistance at night";
                        case "DarkmatterHelmBonus":
                        return @"30% throwing crit chance and velocity
Your thrown weapons electrocute enemies
8% increased damage resistance at night";
                        case "DarkmatterHelmetBonus":
                        return @"15% increased melee critical chance and speed
Your melee weapons electrocute enemies
8% increased damage resistance at night";
                        case "DarkmatterMaskBonus":
                        return  @"200 increased maximum mana
20% reduced mana usage
Your Magic spells electrocute enemies
8% increased damage resistance at night";
                        case "DarkmatterVisorBonus":
                        return @"25% decreased ammo consumption
20% increased ranged critical chance
Your ranged ammunition electrocute enemies
8% increased damage resistance at night";
                        case "DeathlySkullBonus":
                        return @"You are as quiet as death itself, making enemies less likely to target you
20% Reduced Ammo Consumption";
                        case "DemonHoodBonus":
                        return @"Your minions set enemies ablaze
You Always have a small Imp servant by your side
Imp isn't counted in your max minion count";
                        case "DepthFukumenBonus":
                        return @"Enemies are less likely to target you
20% decreased ammo consumption
Nightvision";
                        case "FishDiverMaskABonus":
                        return @"'Hosing time.'
All of your attacks inflict wet to non-boss enemies
Grants uninhibited liquid movement
The Infinity Gauntlet is now at its max potential
You gain a fishy companion";
                        case "SFishDiverMaskABonus":
                        return @"'Hosing time.'
All of your attacks inflict wet to non-boss enemies
Grants uninhibited liquid movement
The Infinity Gauntlet is now at its max potential
You gain a fishy companion";
                        case "MoonHoodABonus":
                        return @"'Stings, doesn't it?'
You glow like the moon in the sky
Magic attacks inflict Moonraze on your target
You have a lunar friend to assist you";
                        case "TiedsMaskABonus":
                        return @"'The soviets have got nothing on me'
All of your melee attacks inflict cursed inferno
A spooky friend lights your way";
                        case "DoomiteVisorBonus":
                        return @"+1 Max Minion
A void searcher fights by your side";
                        case "DoomiteUHelmBonus":
                        return  @"Increases max amount of minions by 2
Increases your minions knockback";
                        case "DoomsdayMaskBonus":
                        return @"Life termination systems activated
You detect all hostile life around you
You can see in the dark much more easily
Your minion's attacks are strong enough to weaken your enemies defense for a time
+5 Minion slots";
                        case "DoomsdayHelmetBonus":
                        return @"Life termination systems activated
You detect all hostile life around you
You can see in the dark much more easily
Your magic attacks are strong enough to weaken your enemies defense for a time
Mana usage reduced by 30%";
                        case "DracoHelmBonus":
                        return @"'Blazing fury consumes you'
You are immune to all ice-related debuffs
You glow like the blazing fire in your soul
Your Melee attacks inflict Daybreak on your target";
                        case "DreadHelmBonus":
                        return @"'Abyssal Wrath enrages you'
You are immune to all fire-related debuffs
You glow like the dread moon in the sky
Your ranged attacks inflict Moonraze on your target";
                        case "DynaskullBonus":
                        return @"Your ranged projectiles have so much power behind them, they confuse the target due to concussive force";
                        case "FleshrendHelmBonus":
                        return @"Being hit by enemies triggers a bloodsplosion that damages all enemies around you
Increased life regen";
                        case "FulguriteHelmBonus":
                        return @"Being struck causes a burst of lightning to erupt from your body, knocking back enemies
-20% Mana Usage";
                        case "FulguriteHelmetBonus":
                        return @"Being struck causes a burst of lightning to erupt from your body, knocking back enemies
+20% increased melee and movement speed";
                        case "FulguriteVisorBonus":
                        return @"Being struck causes a burst of lightning to erupt from your body, knocking back enemies
25% chance to not consume ammo weapons";
                        case "ShroomHatBonus":
                        return @"You are immune to Mana Sickness";
                        case "GoblinSlayerHelmBonus":
                        return @"500% increased damage and knockback to goblins
80% damage resistance from goblins
'I hunt goblins or nothing.'";
                        case "HoodlumHoodBonus":
                        return @"Reaching below half heath multiplies your movement speed, melee damage, and minion damage by 50%";
                        case "HeartstoneHelmetBonus":
                        return "Doubles damage when health is below 40";
                        case "ImpHoodBonus":
                        return "Your minions set enemies ablaze";
                        case "LeviathanHelmBonus":
                        return @"Reaching below half heath multiplies your movement speed, ranged damage, and magic damage by 50%";
                        case "KindledKabutoBonus":
                        return @"5% increased damage resistance
Your Swung weapons set your enemies ablaze";
                        case "MadnessVisorBonus":
                        return "+5% damage";
                        case "MadTitanChestplateBonus":
                        return @"The infinity gauntlet is now at it's most powerful
'The power of a mad titan is now at your fingertips'";
                        case "MadTitanVestBonus":
                        return @"The infinity gauntlet is now at it's most powerful
'The power of a mad titan is now at your fingertips'";
                        case "MechHelmBonus":
                        return "+10% Damage Resistance";
                        case "MushiumHatBonus":
                        return @"Potion Sickness time is cut by 25%";
                        case "NightmareHelmetBonus":
                        return "+10% movement speed";
                        case "NightsHelmBonus":
                        return @"Being hit by enemies Sends you into a speed frenzy, increasing movement speed for a short time
22% Increased Movement speed";
                        case "OceanHelmBonus":
                        return @"20% increased magic damage and 15% mana use reduction while submerged in water";
                        case "OroborosHelmetBonus":
                        return @"+3 Defence";
                        case "AdamantitePaintBonus":
                        return @"+4 Minion slots";
                        case "ChlorophytePaintBonus":
                        return @"+6 Minion slots
Shoots crystal leaves at nearby enemies";
                        case "CobaltPaintBonus":
                        return @"+2 Minion slots";
                        case "FulguritePaintBonus":
                        return @"Being struck causes a burst of lightning to erupt from your body, knocking back enemies
+5 Minion slots";
                        case "HallowedPaintBonus":
                        return @"+6 Minion slots";
                        case "MythrilPaintBonus":
                        return @"+3 Minion slots";
                        case "OrichalcumPaintBonus":
                        return @"Flower petals will fall on your target for extra damage
+2 Minion slots";
                        case "PalladiumPaintBonus":
                        return @"+1 Minion Slot
Greatly increases life regeneration after striking an enemy";
                        case "TechneciumPaintBonus":
                        return @"+4 Minion slots
Hitting enemies causes you to build up a static charge
Charge is released once you reach a charge level of 4 or you are hit";
                        case "TerraPaintBonus":
                        return @"+7 Minion slots";
                        case "TitaniumPaintBonus":
                        return @"+4 Minion slots
Briefly become invulnerable after your minions strike an enemy";
                        case "UraniumPaintBonus":
                        return @"+3 Minion slots
Enemies near you are burned by radiation emitted by your armor";
                        case "YttriumPaintBonus":
                        return @"+2 Minion slots
You can do a lightning-quick dash";
                        case "Paladin_HelmetBonus":
                        return @"+100 Health
Effects of the Paladin's Shield";
                        case "RadiumHatBonus":
                        return @"Increases max number of minions by 7
Being hit causes stars from the heavans to fall around you and increases your movement speed
30% increased movement speed during the day";
                        case "RadiumHeadgearBonus":
                        return @"25% decreased ammo consumption
20% increased ranged critical chance
Being hit causes stars from the heavans to fall around you and increases your movement speed";
                        case "RadiumHelmBonus":
                        return @"30% throwing crit chance and velocity
Being hit causes stars from the heavans to fall around you and increases your movement speed
30% increased movement speed during the day";
                        case "RadiumHelmetBonus":
                        return @"15% increased melee critical chance and speed
Being hit causes stars from the heavans to fall around you and increases your movement speed
30% increased movement speed during the day";
                        case "RadiumMaskBonus":
                        return @"200 increased maximum mana
20% reduced mana usage
Being hit causes stars from the heavans to fall around you and increases your movement speed
30% increased movement speed during the day";
                        case "RaiderChestBonus":
                        return @"You cannot be knocked back
As your health goes down, your damage resistance goes up";
                        case "StarcloudHeadgearBonus":
                        return "+ 100% minion and magic damage if health is below 60.";
                        case "StarcrystalHelmBonus":
                        return "Doubles damage when mana is below 20% maximum capacity";
                        case "TechneciumHelmetBonus":
                        return @"Hitting enemies causes you to build up a static charge
Charge is released once you reach a charge level of 4 or you are hit";
                        case "TechneciumMaskBonus":
                        return @"Hitting enemies causes you to build up a static charge
Charge is released once you reach a charge level of 4 or you are hit";
                        case "TechneciumVisorBonus":
                        return @"Hitting enemies causes you to build up a static charge
Charge is released once you reach a charge level of 4 or you are hit";
                        case "TribalHatBonus":
                        return @"You are blessed with a gift of nature, allowing you to autodrink mana potions
Mana usage lowered by 30%";
                        case "TrueAbyssalFukumenBonus":
                        return @"Your ranged attacks are imbued with the venomous properties of bogtoxin
Enemies are less likely to target you
Night Vision and hunter effects
25% reduced ammo consumption";
                        case "TrueAtlanteanHelmBonus":
                        return @"While submerged in water, your magic abilities are increased drastically
You can swim and water does not affect your movement";
                        case "TrueBlazingKabutoBonus":
                        return @"5% increased damage resistance
Enemies are more likely to target you
Enemies that strike you are set ablaze
Your Swung weapons set your enemies ablaze";
                        case "TrueCopperHelmBonus":
                        return @"'You are empowered by the magic of the earth around you'
Being struck releases a copper soul that strikes your enemies";
                        case "TrueDeathlySkullBonus":
                        return @"You are as quiet as death itself, making enemies less likely to target you
25% Reduced Ammo Consumption
Being killed causes your spirit to materialize, reviving you
While Etheral, you have more invincibility frames, but your defense is reduced by 10";
                        case "TrueDemonHoodBonus":
                        return @"Your minions bathe your enemies in shadowflame
You Always have an Imp army by your side
Imp army doesn't affect minion count";
                        case "TrueDoomiteVisorBonus":
                        return @"+4 Minion slots
A void scout hunts down your foes for you";
                        case "TrueDynaskullBonus":
                        return @"Your ranged projectiles have so much power behind them, they confuse the target due to concussive force
45% chance to not consume ammo
Pressing the Ability hotkey fires off a dynaskull shot towards your cursor";
                        case "TrueFleshrendHelmBonus":
                        return @"Being hit by enemies triggers a bloodsplosion that damages all enemies around you
Massively increased life regen
Strong Melee hits leech life from enemies to heal you
You inflict Ichor with all of your melee attacks";
                        case "TrueHallowedHelmBonus":
                        return @"20% Chance not to Consume Ammo, reduced mana usage, increased melee, and movement speed";
                        case "TrueNightsHelmBonus":
                        return @"Being hit by enemies Sends you into a speed frenzy, increasing movement speed for a short time
44% Increased Movement speed
Killing enemies has a 25% chance to cause them to erupt into a cursed explosion that damages enemies around it
Your melee attacks inflict cursed inferno";
                        case "TrueRaiderPlateBonus":
                        return @"You are immune to all cold-related debuffs
You quickly regenerate your HP while standing still
You are immune to knockback";
                        case "TrueTribalHatBonus":
                        return @"You are blessed with a gift of nature, allowing you to autodrink mana potions
Mana usage lowered by 40%
The jungle gives you protection, causing deadly spores to spawn around you";
                        case "UraniumHeadgearBonus":
                        return @"Enemies near you are burned by radiation emitted by your armor";
                        case "UraniumHoodBonus":
                        return @"Enemies near you are burned by radiation emitted by your armor";
                        case "UraniumVisorBonus":
                        return @"Enemies near you are burned by radiation emitted by your armor";
                        case "VikingPlateBonus":
                        return @"4% Increased damage resistance";
                        case "WitchHoodBonus":
                        return @"Scorch and Burn.
20% Increased Magic and Minion damage
+4 max minions
A Fire spirit protects you
The Fire spirit becomes more powerful the less mana you have";
                        case "YtriumHeadgearBonus":
                        return @"You can do a lightning-quick dash.";
                        case "YtriumHelmBonus":
                        return @"You can do a lightning-quick dash.";
                        case "YtriumMaskBonus":
                        return @"You can do a lightning-quick dash.";
                    }
                }
            return "";
        }
        public static string WorldBuild(String Info)
        {
            if(Language.ActiveCulture == GameCulture.Chinese)
            {
                switch(Info)
                {
                    case "Info1":
                    return "安置混沌祭坛";
                    case "Info2":
                    return "混沌蔓延";
                    case "Info3":
                    return "把土壤焦化成燎狱";
                    case "Info4":
                    return "给潭渊蓄水";
                    case "Info5":
                    return "建筑泰拉心球";
                }
            }
            else
            {
                switch(Info)
                {
                    case "Info1":
                    return "Placing Chaos Altars";
                    case "Info2":
                    return "Spreading Chaos";
                    case "Info3":
                    return "Scorching the Inferno";
                    case "Info4":
                    return "Flooding the Mire";
                    case "Info5":
                    return "Constructing the Terrarium";
                }
            }
            return"";
        }
    }
}