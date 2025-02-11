using SqlSugar;

namespace Byboy.SignPlugin.DbUtils
{

    /// <summary>
    /// 签到记录
    /// </summary>
    public class ClusterSign
    {

        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 群号码
        /// </summary>
        public string GroupUsername { get; set; } = string.Empty;

        /// <summary>
        /// username
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// nickname
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Nickname { get; set; } = string.Empty;
        /// <summary>
        /// 总签到次数
        /// </summary>
        public long SignCount { get; set; }
        /// <summary>
        /// 月签到次数
        /// </summary>
        public long MonthSignCount { get; set; }

        /// <summary>
        /// 连续签到天数
        /// </summary>
        public long Continue { get; set; }

        /// <summary>
        /// 上次签到时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? LastSignTime { get; set; }

        /// <summary>
        /// 本次签到积分
        /// </summary>
        public long Extcredits { get; set; }

        /// <summary>
        /// 本次签到后等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 首次发言时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 最后发言时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime LastSentTime { get; set; }

        /// <summary>
        /// 发言次数
        /// </summary>
        public long SentCount { get; set; }

        /// <summary>
        /// 月发言次数
        /// </summary>
        public long MonthSentCount { get; set; }

    }


    /// <summary>
    /// 每个群的签到配置文件
    /// </summary>
    public class ConfigObj
    {
        public ConfigObj()
        {
            this.FirstSign = true;
            this.Status = true;
            this.Cmd = "^(签到|打卡)$";
            this.Days = ("0,1,3,7,15,30,60,90,120,240,365,750").Split(",").Select(t => int.Parse(t)).ToList();
            this.Levels = "无名小卒,初来乍到,偶尔看看★,偶尔看看★★,偶尔看看★★★,常住居民★,常住居民★★,常住居民★★★,以群为家★,以群为家★★,以群为家★★★,伴群终老".Split(",".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).ToList();
            this.Hellos = @"欢迎您回来看看，您的等级是【[当前等级]】，再签到[再签到]天可升级为【[下一等级]】
您的签到等级是【[当前等级]】，再签到[再签到]天可升级为【[下一等级]】
您的签到等级是【[当前等级]】，再签到[再签到]天可升级为【[下一等级]】
您的签到等级是【[当前等级]】，再签到[再签到]天可升级为【[下一等级]】
您的签到等级是【[当前等级]】，再签到[再签到]天可升级为【[下一等级]】
您的签到等级是【[当前等级]】，再签到[再签到]天可升级为【[下一等级]】
您的签到等级是【[当前等级]】，再签到[再签到]天可升级为【[下一等级]】
您的签到等级是【[当前等级]】，再签到[再签到]天可升级为【[下一等级]】
您的签到等级是【[当前等级]】，再签到[再签到]天可升级为【[下一等级]】
您的签到等级是【[当前等级]】，再签到[再签到]天可升级为【[下一等级]】
您的签到等级是【[当前等级]】，再签到[再签到]天可升级为【[下一等级]】
您的签到等级是【[当前等级]】，再签到[再签到]天可升级为【[下一等级]】".Split("\r\n".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).ToList();
            this.Type = 1;
            this.Min = 1;
            this.Max = 10;
            this.Add = 5;
            this.Begin = 5;
            this.BeginExtCredits = 10;
            this.RepeatMin = 1;
            this.RepeatMax = 2;
            this.SignTime = ("0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23").Split(",").Select(t => int.Parse(t)).ToList();
            this.Top = 10;
            this.Random = 10;
            this.RndMin = 10;
            this.RndMax = 30;
            this.RndType = 0;//经验


            this.Lang1 = "还没有到签到时间或者签到时间已过，签到时间：[签到时间]时";
            this.Lang2 = "今天您已经签到过了，请勿重复签到，本次签到扣[扣除积分]";
            this.Lang3 = @"第[今日排名]个签到
本次签到得：
[基本积分]*[今日排名加成]*[今日连续加成]=[本次积分]
本月：[本月签到]天 连续：[连续签到]天
累计：[累计签到]天/[下一级天数]天
当前：[当前等级]
再签[再签到]天到：[下一等级]
";
            this.Lang4 = "您已经签到过了，请勿重复签到！";
            this.Lang5 = "签到帮助";
            this.Lang6 = @"可用指令如下：
[签到指令]
签到等级
签到总榜
签到月榜
今日签到
发言总榜
发言月榜
签到信息

奖励积分：[奖励积分]
连续签到奖励[连续加成]点，每日前[前n名]名签到奖励[前n名奖励]点
重复签到扣分：[重复签到扣分]，签到时间段：[签到时段]
";
            this.Lang7 = "签到等级";
            this.Lang8 = @"签到等级如下：
[for]{0,3}级 签到{1,3}天 称号：{2}
[/for]";
            this.Lang9 = "签到总榜";
            this.Lang10 = @"签到总榜如下：
[for][昵称] [累计签到]天 称号：[当前等级] 最后签到：[上次签到]
[/for]
截止：[时间]";
            this.Lang11 = "签到月榜";
            this.Lang12 = @"签到月榜如下：
[for][昵称] [本月签到]天 称号：[当前等级] 最后签到：[上次签到]
[/for]
截止：[时间]";
            this.Lang13 = "今日签到";
            this.Lang14 = @"今日签到排行如下：
[for][昵称]时间：[上次签到] 连续：[连续签到] 奖励：[本次积分] 当前：[当前等级]
[/for]
截止：[时间]";
            this.Lang15 = "发言总榜";
            this.Lang16 = @"本群发言总排行如下：
[for][昵称] [首次发言]至[最后发言]发言：[累计发言]条，本月发言：[本月发言]条
[/for]
截止：[时间]";
            this.Lang17 = "发言月榜";
            this.Lang18 = @"本群发言月排行如下：
[for][昵称] [首次发言]至[最后发言]发言：[累计发言]条，本月发言：[本月发言]条
[/for]
截止：[时间]";
            this.Lang19 = "签到信息";
            this.Lang20 = @"首次发言：[首次发言]
本月发言：[本月发言]
累计发言：[累计发言]
累计签到：[累计签到]
本月签到：[本月签到]
连续签到：[连续签到]
上次签到：[上次签到]
上次领取：[本次积分]
当前等级：[当前等级]
截止：[时间]";

            this.Lang21 = new List<string>() { "今天运气真好，捡到：[基本积分]" };
        }

        /// <summary>
        /// 状态（true，允许签到）
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 允许签到的群
        /// </summary>
        public List<string> ExternalIds { get; set; } = new List<string>();

        /// <summary>
        /// 签到天数
        /// </summary>
        public List<int> Days { get; set; }

        /// <summary>
        /// 签到等级
        /// </summary>
        public List<string> Levels { get; set; }

        /// <summary>
        /// 欢迎词
        /// </summary>
        public List<string> Hellos { get; set; }

        /// <summary>
        /// 签到积分类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 奖励最少值
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// 奖励最大值
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// 连续签到加成
        /// </summary>
        public int Add { get; set; }

        /// <summary>
        /// 额外奖励前n人
        /// </summary>
        public int Begin { get; set; }

        /// <summary>
        /// 额外奖励点数
        /// </summary>
        public int BeginExtCredits { get; set; }

        /// <summary>
        /// 扣除最小值
        /// </summary>
        public int RepeatMin { get; set; }

        /// <summary>
        /// 扣除最大值
        /// </summary>
        public int RepeatMax { get; set; }

        /// <summary>
        /// 签到时间段限制
        /// </summary>
        public List<int> SignTime { get; set; }

        /// <summary>
        /// 签到指令
        /// </summary>
        public string Cmd { get; set; }

        /// <summary>
        /// 随机概率奖励
        /// </summary>
        public int Random { get; set; }

        /// <summary>
        /// 随机奖励积分类型
        /// </summary>
        public int RndType { get; set; }

        /// <summary>
        /// 随机奖励数量
        /// </summary>
        public int RndMin { get; set; }

        /// <summary>
        /// 随机奖励数量
        /// </summary>
        public int RndMax { get; set; }

        /// <summary>
        /// 排行榜数量
        /// </summary>
        public int Top { get; set; }

        public string Lang1 { get; set; }

        public string Lang2 { get; set; }

        public string Lang3 { get; set; }

        public string Lang4 { get; set; }

        public string Lang5 { get; set; }

        public string Lang6 { get; set; }

        public string Lang7 { get; set; }

        public string Lang8 { get; set; }

        public string Lang9 { get; set; }

        public string Lang10 { get; set; }

        public string Lang11 { get; set; }

        public string Lang12 { get; set; }

        public string Lang13 { get; set; }

        public string Lang14 { get; set; }

        public string Lang15 { get; set; }

        public string Lang16 { get; set; }

        public string Lang17 { get; set; }

        public string Lang18 { get; set; }

        public string Lang19 { get; set; }

        public string Lang20 { get; set; }

        public List<string> Lang21 { get; set; }
        /// <summary>
        /// 首次发言自动签到
        /// </summary>
        public bool FirstSign { get; set; }
    }

    /// <summary>
    /// 数据库存储
    /// </summary>
    [SugarIndex("unique_codetable1_CreateTime",nameof(ClusterConfig.GroupUsername),OrderByType.Desc,true)]
    public class ClusterConfig
    {
        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 群号码
        /// </summary>
        public string GroupUsername { get; set; }


        [SugarColumn(IsJson = true,IsNullable = true)]
        /// <summary>
        /// 结构化config
        /// </summary>
        public ConfigObj ConfigObj { get; set; }
        /// <summary>
        /// 群昵称
        /// </summary>
        public string GroupNickName { get; set; } = string.Empty;
        /// <summary>
        /// 群成员数量
        /// </summary>
        public int MemberCount { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; } = string.Empty;

        public ClusterConfig()
        {
            ConfigObj = new ConfigObj();
        }
    }
}
