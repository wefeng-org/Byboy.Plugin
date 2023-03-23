using Byboy.SignPlugin.DbUtils;
using Plugin;
using Plugin.DbEntitys;
using Plugin.EveEntitys;
using SuperWx;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using WeFeng.Db;

namespace Byboy.SignPlugin
{
    public class Plugin : IPlugin
    {
        public override string Name => "签到插件";

        public override string Version => "v1.0";

        public override string Author => "Byboy";

        public override string Description => "用户签到";

        public override BaseConfig Config { get; set; }
        public override bool IsOpen { get; set; }
        public Random r = new();

        public override void Initialize()
        {
            // 初始化数据库
            try {
                DbUtil.Db.DbMaintenance.CreateDatabase();
                // 初始化表
                DbUtil.Db.CodeFirst.InitTables(typeof(ClusterSign),typeof(ClusterConfig));
                DbUtil.SaveClusterConfig(new ClusterConfig() { Id = 1, GroupUsername = "默认配置",ConfigObj = new ConfigObj { Status = false } });
                DbUtil.SaveClusterConfig(new ClusterConfig() { Id = 2, GroupUsername = "全局配置",ConfigObj = new ConfigObj { Status = false } });
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }

            DbUtil.configs = DbUtil.Db.Queryable<ClusterConfig>().ToList();
            // 订阅收到群消息事件
            ReceiveGroupMessage += Plugin_ReceiveGroupMessage;
            ReceiveFriendMessage += Plugin_ReceiveFriendMessage;

            Eve.OnLog(this,"开启成功");
        }


        private async Task Plugin_ReceiveFriendMessage(TLS_BFClent sender, ReceiveFriendMessageArgs e)
        {
            if (e.Content.MsgType == MessageType.NORMALIM)
            {
                var result = onReceiveFriendMessage(e.Username, e.Content.Text);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    await SendTextMsg(sender.WX.UserLogin.OriginalId, new() { new() { Username = e.Username, Type = 1, Content = result } });
                }
            }
        }

        private bool IsRobotAdmins(string username)
        {
            return StaticData.RobotConfig.AdminUsername.Split(",").Contains(username);
        }

        private string onReceiveFriendMessage(string username,string Message)
        {
            string result = null;
            Match m = null;
            try {
                m = Regex.Match(Message,@"^(\S+)\s+(?<chatroom>(\d{1,12})@chatroom)(.*?)$",RegexOptions.Singleline);
                if (!m.Success)
                    return null;
                string ExternalId = m.Groups["chatroom"].Value;
                ClusterConfig cc = ExternalId.GetClusterConfig();
                if (cc == null)
                    return null;

                //if (!StaticData.RobotConfig.AdminUsername.Split(",").Contains(username)) {
                //    if (ExternalId < 10000)
                //        return null;

                //    var cluster = this.User.Clusters.FirstOrDefault(t => t.ExternalId == ExternalId);
                //    if (cluster == null)
                //        return null;
                //    var member = cluster.members.FirstOrDefault(t => t.QQ == QQ);
                //    if (member == null || (cluster.Creator != member.QQ && !member.IsAdmin()))
                //        return null;
                //}

                var config = cc.ConfigObj;
                switch (m.Groups[1].Value) {
                    case "开启签到": {
                        if (config.Status)
                            result = @"签到已处于开启状态！";
                        else {
                            config.Status = true;
                            cc.SaveClusterConfig();
                            result = @"签到开启成功！";
                        }
                    }
                    break;
                    case "关闭签到": {
                        config.Status = false;
                        cc.SaveClusterConfig();
                        result = @"签到关闭成功！";
                    }
                    break;
                    case "查看签到设置": {
                        StringBuilder sb = new(string.Format(@"签到设置 {0}
",ExternalId));
                        sb.AppendFormat("自动签到={0}\r\n",config.FirstSign ? "开" : "关");
                        sb.AppendFormat("签到指令={0}\r\n",config.Cmd);
                        sb.AppendFormat("签到天数分级={0}\r\n",string.Join(",",config.Days));
                        sb.AppendFormat("签到称号分级={0}\r\n",string.Join(",",config.Levels));
                        sb.AppendFormat("首次聊天欢迎词={0}\r\n",string.Join("\r\n",config.Hellos));
                        sb.AppendFormat("签到时段={0}\r\n",string.Join(",",config.SignTime));
                        sb.AppendFormat("签到积分类型={0}\r\n",StaticData.RobotConfig.JfNames.ExtcreditsName(config.Type));
                        sb.AppendFormat("签到积分范围={0}-{1}\r\n",config.Min,config.Max);
                        sb.AppendFormat("连续签到奖励={0}\r\n",config.Add);
                        sb.AppendFormat("前n名={0}\r\n",config.Begin);
                        sb.AppendFormat("前n名奖励={0}\r\n",config.BeginExtCredits);
                        sb.AppendFormat("重复签到扣分={0}-{1}\r\n",config.RepeatMin,config.RepeatMax);
                        sb.AppendFormat("随机奖励概率={0}\r\n",config.Random);
                        sb.AppendFormat("随机奖励积分类型={0}\r\n",StaticData.RobotConfig.JfNames.ExtcreditsName(config.RndType));
                        sb.AppendFormat("随机奖励数量={0}-{1}\r\n",config.RndMin,config.RndMax);
                        sb.AppendFormat("排行榜数量={0}\r\n",config.Top);
                        sb.AppendFormat("签到时间错误提示={0}\r\n",config.Lang1);
                        sb.AppendFormat("重复签到扣分提示={0}\r\n",config.Lang2);
                        sb.AppendFormat("签到成功提示={0}\r\n",config.Lang3);
                        sb.AppendFormat("重复签到不扣分提示={0}\r\n",config.Lang4);
                        sb.AppendFormat("签到帮助指令={0}\r\n",config.Lang5);
                        sb.AppendFormat("签到帮助内容={0}\r\n",config.Lang6);
                        sb.AppendFormat("签到等级指令={0}\r\n",config.Lang7);
                        sb.AppendFormat("签到等级内容={0}\r\n",config.Lang8);
                        sb.AppendFormat("签到总榜指令={0}\r\n",config.Lang9);
                        sb.AppendFormat("签到总榜内容={0}\r\n",config.Lang10);
                        sb.AppendFormat("签到月榜指令={0}\r\n",config.Lang11);
                        sb.AppendFormat("签到月榜内容={0}\r\n",config.Lang12);
                        sb.AppendFormat("今日签到指令={0}\r\n",config.Lang13);
                        sb.AppendFormat("今日签到内容={0}\r\n",config.Lang14);
                        sb.AppendFormat("发言总榜指令={0}\r\n",config.Lang15);
                        sb.AppendFormat("发言总榜内容={0}\r\n",config.Lang16);
                        sb.AppendFormat("发言月榜指令={0}\r\n",config.Lang17);
                        sb.AppendFormat("发言月榜内容={0}\r\n",config.Lang18);
                        sb.AppendFormat("签到信息指令={0}\r\n",config.Lang19);
                        sb.AppendFormat("签到信息内容={0}\r\n",config.Lang20);
                        sb.AppendFormat("随机奖励提示={0}\r\n",string.Join("\r\n",config.Lang21));

                        sb.AppendFormat("请复制以上内容（不包括本句），修改后回复即可完成设置。");
                        result = sb.ToString();
                    }
                    break;
                    case "签到设置": {
                        m = Regex.Match(m.Groups[3].Value,@"(\S{1,20})\=([\s\S]*?)(?=(\r\n?\S{1,20}\=)|$)");
                        while (m.Success) {
                            switch (m.Groups[1].Value) {
                                case "签到指令":
                                    config.Cmd = m.Groups[2].Value.Trim();
                                    break;
                                case "签到天数分级":
                                    config.Days = (m.Groups[2].Value.Trim()).Split(",").Select(t => int.Parse(t)).ToList();
                                    break;
                                case "签到称号分级":
                                    config.Levels = m.Groups[2].Value.Trim().Split(",".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).ToList();
                                    break;
                                case "首次聊天欢迎词":
                                    config.Hellos = m.Groups[2].Value.Trim().Split("\r\n".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).ToList();
                                    break;
                                case "签到时段":
                                    config.SignTime = (m.Groups[2].Value.Trim()).Split(",").Select(t => int.Parse(t)).ToList();
                                    break;
                                case "签到积分类型": {
                                    config.Type = StaticData.RobotConfig.JfNames.ExtcreditsType(m.Groups[2].Value.Trim());
                                }
                                break;
                                case "签到积分范围": {
                                    var t1 = m.Groups[2].Value.Trim().Split('-');
                                    config.Min = int.Parse(t1[0]);
                                    config.Max = int.Parse(t1[1]);
                                }
                                break;
                                case "连续签到奖励": {
                                    config.Add = int.Parse(m.Groups[2].Value.Trim());
                                }
                                break;
                                case "前n名": {
                                    config.Begin = int.Parse(m.Groups[2].Value.Trim());
                                }
                                break;
                                case "前n名奖励": {
                                    config.BeginExtCredits = int.Parse(m.Groups[2].Value.Trim());
                                }
                                break;
                                case "重复签到扣分": {
                                    var t1 = m.Groups[2].Value.Trim().Split('-');
                                    config.RepeatMin = int.Parse(t1[0]);
                                    config.RepeatMax = int.Parse(t1[1]);
                                }
                                break;
                                case "随机奖励积分类型": {
                                    config.RndType = StaticData.RobotConfig.JfNames.ExtcreditsType(m.Groups[2].Value.Trim());
                                }
                                break;
                                case "随机奖励概率": {
                                    if (!StaticData.RobotConfig.AdminUsername.Split(",").Contains(username))
                                        break;
                                    config.Random = int.Parse(m.Groups[2].Value.Trim());
                                }
                                break;
                                case "随机奖励数量": {
                                    var t1 = m.Groups[2].Value.Trim().Split('-');
                                    config.RndMin = int.Parse(t1[0]);
                                    config.RndMax = int.Parse(t1[1]);
                                }
                                break;
                                case "排行榜数量":
                                    config.Top = int.Parse(m.Groups[2].Value.Trim());
                                    break;
                                case "签到时间错误提示":
                                    config.Lang1 = m.Groups[2].Value.Trim();
                                    break;
                                case "重复签到扣分提示":
                                    config.Lang2 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到成功提示":
                                    config.Lang3 = m.Groups[2].Value.Trim();
                                    break;
                                case "重复签到不扣分提示":
                                    config.Lang4 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到帮助指令":
                                    config.Lang5 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到帮助内容":
                                    config.Lang6 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到等级指令":
                                    config.Lang7 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到等级内容":
                                    config.Lang8 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到总榜指令":
                                    config.Lang9 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到总榜内容":
                                    config.Lang10 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到月榜指令":
                                    config.Lang11 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到月榜内容":
                                    config.Lang12 = m.Groups[2].Value.Trim();
                                    break;
                                case "今日签到指令":
                                    config.Lang13 = m.Groups[2].Value.Trim();
                                    break;
                                case "今日签到内容":
                                    config.Lang14 = m.Groups[2].Value.Trim();
                                    break;
                                case "发言总榜指令":
                                    config.Lang15 = m.Groups[2].Value.Trim();
                                    break;
                                case "发言总榜内容":
                                    config.Lang16 = m.Groups[2].Value.Trim();
                                    break;
                                case "发言月榜指令":
                                    config.Lang17 = m.Groups[2].Value.Trim();
                                    break;
                                case "发言月榜内容":
                                    config.Lang18 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到信息指令":
                                    config.Lang19 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到信息内容":
                                    config.Lang20 = m.Groups[2].Value.Trim();
                                    break;
                                case "随机奖励提示":
                                    config.Lang21 = m.Groups[2].Value.Trim().Split("\r\n".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).ToList();
                                    break;
                                default:
                                    return "存在未知指令，请先发送【查看签到设置】";
                            }
                            m = m.NextMatch();
                        }
                        cc.SaveClusterConfig();
                        result = "设置成功";
                    }
                    break;
                    default:
                        break;
                }
            } catch (Exception ex) {
                Eve.OnLog(this,$"指令处理错误1：{ex.Message}\r\n{ex.StackTrace}");
            }
            return result;
        }

        private async Task Plugin_ReceiveGroupMessage(TLS_BFClent? bfClient, global::Plugin.EveEntitys.ReceiveGroupMessageArgs e)
        {
            var cc = e.GroupUsername.GetClusterConfig();
            if (cc == null)
                return;
            var config = cc.ConfigObj;
            var username = e.Username;

            if (e.Content.Text == "开启签到") {
                if (IsRobotAdmins(username) || e.Group.OwnerUsername == username || e.Group.IsAdmin) {
                    string result = null;
                    if (cc.Id == 2 && !IsRobotAdmins(username))
                        result = $@"@{e.Sender.Nickname}管理员禁止修改签到设置";
                    else if (config.Status)
                        result = $@"@{e.Sender.Nickname}签到已处于开启状态！请回复以下指令：
关闭签到
查看签到设置
清理未发言+天数，例如：清理未发言30，表示清理30天内未发言记录
清理未签到+天数，例如：清理未签到30，表示清理30天内未签到记录";
                    else {
                        config.Status = true;
                        cc.SaveClusterConfig();
                        result = $@"@{e.Sender.Nickname}签到开启成功！请回复以下指令：
关闭签到
查看签到设置
清理未发言+天数，例如：清理未发言30，表示清理30天内未发言记录
清理未签到+天数，例如：清理未签到30，表示清理30天内未签到记录";
                    }
                    await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = result } });
                    return;
                }
            } else if (e.Content.Text == "关闭签到") {
                if (IsRobotAdmins(username) || e.Group.OwnerUsername == username || e.Group.IsAdmin) {
                    string result = null;
                    if (cc.Id == 2 && !IsRobotAdmins(username))
                        result = $@"@{e.Sender.Nickname}管理员禁止修改签到设置";
                    else {
                        config.Status = false;
                        cc.SaveClusterConfig();
                        result = $@"@{e.Sender.Nickname}签到关闭成功！
如需开启请回复：开启签到";
                    }
                    await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = result } });
                    return;
                }
            } else if (e.Content.Text.StartsWith("清理未发言")) {
                if (IsRobotAdmins(username) || e.Group.OwnerUsername == username || e.Group.IsAdmin) {
                    string result = null;
                    int count = DbUtil.DeleteSignByLastSentTime(e.GroupUsername,DateTime.Now.AddDays(-1 * int.Parse(e.Content.Text[6..].Trim())));
                    result = string.Format($@"@{e.Sender.Nickname}成功删除{0}条数据",count);
                    await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = result } });
                    return;
                }
            } else if (e.Content.Text.StartsWith("清理未签到")) {
                if (IsRobotAdmins(username) || e.Group.OwnerUsername == username || e.Group.IsAdmin) {
                    string result = null;
                    var count = DbUtil.DeleteSignByLastSignTime(e.GroupUsername,DateTime.Now.AddDays(-1 * int.Parse(e.Content.Text[6..].Trim())));
                    result = string.Format($@"@{e.Sender.Nickname}成功删除{0}条数据",count);
                    await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = result } });
                    return;
                }
            } else if (e.Content.Text.Trim() == "查看签到设置") {
                if (IsRobotAdmins(username) || e.Group.OwnerUsername == username || e.Group.IsAdmin) {
                    string result = null;
                    if (cc.Id == 2 && !IsRobotAdmins(username))
                        result = $@"@{e.Sender.Nickname}管理员禁止修改签到设置";
                    else {
                        StringBuilder sb = new StringBuilder($@"@{e.Sender.Nickname}签到设置
");
                        sb.AppendFormat("签到指令={0}\r\n",config.Cmd);
                        sb.AppendFormat("签到天数分级={0}\r\n",string.Join(",",config.Days));
                        sb.AppendFormat("签到称号分级={0}\r\n",string.Join(",",config.Levels));
                        sb.AppendFormat("首次聊天欢迎词={0}\r\n",string.Join("\r\n",config.Hellos));
                        sb.AppendFormat("签到时段={0}\r\n",string.Join(",",config.SignTime));
                        sb.AppendFormat("签到积分类型={0}\r\n",StaticData.RobotConfig.JfNames.ExtcreditsName(config.Type));
                        sb.AppendFormat("签到积分范围={0}-{1}\r\n",config.Min,config.Max);
                        sb.AppendFormat("连续签到奖励={0}\r\n",config.Add);
                        sb.AppendFormat("前n名={0}\r\n",config.Begin);
                        sb.AppendFormat("前n名奖励={0}\r\n",config.BeginExtCredits);
                        sb.AppendFormat("重复签到扣分={0}-{1}\r\n",config.RepeatMin,config.RepeatMax);
                        sb.AppendFormat("排行榜数量={0}\r\n",config.Top);
                        sb.AppendFormat("随机奖励积分类型={0}\r\n",StaticData.RobotConfig.JfNames.ExtcreditsName(config.RndType));
                        sb.AppendFormat("随机奖励概率={0}\r\n",config.Random);
                        sb.AppendFormat("随机奖励数量={0}-{1}\r\n",config.RndMin,config.RndMax);
                        sb.AppendFormat("签到时间错误提示={0}\r\n",config.Lang1);
                        sb.AppendFormat("重复签到扣分提示={0}\r\n",config.Lang2);
                        sb.AppendFormat("签到成功提示={0}\r\n",config.Lang3);
                        sb.AppendFormat("重复签到不扣分提示={0}\r\n",config.Lang4);
                        sb.AppendFormat("签到帮助指令={0}\r\n",config.Lang5);
                        sb.AppendFormat("签到帮助内容={0}\r\n",config.Lang6);
                        sb.AppendFormat("签到等级指令={0}\r\n",config.Lang7);
                        sb.AppendFormat("签到等级内容={0}\r\n",config.Lang8);
                        sb.AppendFormat("签到总榜指令={0}\r\n",config.Lang9);
                        sb.AppendFormat("签到总榜内容={0}\r\n",config.Lang10);
                        sb.AppendFormat("签到月榜指令={0}\r\n",config.Lang11);
                        sb.AppendFormat("签到月榜内容={0}\r\n",config.Lang12);
                        sb.AppendFormat("今日签到指令={0}\r\n",config.Lang13);
                        sb.AppendFormat("今日签到内容={0}\r\n",config.Lang14);
                        sb.AppendFormat("发言总榜指令={0}\r\n",config.Lang15);
                        sb.AppendFormat("发言总榜内容={0}\r\n",config.Lang16);
                        sb.AppendFormat("发言月榜指令={0}\r\n",config.Lang17);
                        sb.AppendFormat("发言月榜内容={0}\r\n",config.Lang18);
                        sb.AppendFormat("签到信息指令={0}\r\n",config.Lang19);
                        sb.AppendFormat("签到信息内容={0}\r\n",config.Lang20);
                        sb.AppendFormat("随机奖励提示={0}\r\n",string.Join("\r\n",config.Lang21));

                        sb.AppendFormat("请复制以上内容（不包括本句），修改后回复即可完成设置。");
                        result = sb.ToString();
                    }
                    await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = result } });
                    return;
                }
            } else if (e.Content.Text.Trim().StartsWith("签到设置")) {
                if (IsRobotAdmins(username) || e.Group.OwnerUsername == username || e.Group.IsAdmin) {
                    string result = null;
                    if (cc.Id == 2 && !IsRobotAdmins(username))
                        result = $@"@{e.Sender.Nickname}管理员禁止修改签到设置";
                    else {
                        var msg1 = e.Content.Text.Trim().Substring(5).Trim();
                        var m = Regex.Match(msg1,@"(\S{1,20})\=([\s\S]*?)(?=(\r\n?\S{1,20}\=)|$)");
                        while (m.Success) {
                            switch (m.Groups[1].Value) {
                                case "自动签到":
                                    config.FirstSign = (m.Groups[2].Value.Trim() == "开");
                                    break;
                                case "签到指令":
                                    config.Cmd = m.Groups[2].Value.Trim();
                                    break;
                                case "签到天数分级":
                                    config.Days = (m.Groups[2].Value.Trim()).Split(",").Select(t => int.Parse(t)).ToList();
                                    break;
                                case "签到称号分级":
                                    config.Levels = m.Groups[2].Value.Trim().Split(",".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).ToList();
                                    break;
                                case "首次聊天欢迎词":
                                    config.Hellos = m.Groups[2].Value.Trim().Split("\r\n".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).ToList();
                                    break;
                                case "签到时段":
                                    config.SignTime = (m.Groups[2].Value.Trim()).Split(",").Select(t => int.Parse(t)).ToList();
                                    break;
                                case "签到积分类型": {
                                    config.Type = StaticData.RobotConfig.JfNames.ExtcreditsType(m.Groups[2].Value.Trim());
                                }
                                break;
                                case "签到积分范围": {
                                    var t1 = m.Groups[2].Value.Trim().Split('-');
                                    config.Min = int.Parse(t1[0]);
                                    config.Max = int.Parse(t1[1]);
                                }
                                break;
                                case "连续签到奖励": {
                                    config.Add = int.Parse(m.Groups[2].Value.Trim());
                                }
                                break;
                                case "前n名": {
                                    config.Begin = int.Parse(m.Groups[2].Value.Trim());
                                }
                                break;
                                case "前n名奖励": {
                                    config.BeginExtCredits = int.Parse(m.Groups[2].Value.Trim());
                                }
                                break;
                                case "重复签到扣分": {
                                    var t1 = m.Groups[2].Value.Trim().Split('-');
                                    config.RepeatMin = int.Parse(t1[0]);
                                    config.RepeatMax = int.Parse(t1[1]);
                                }
                                break;
                                case "随机奖励积分类型": {
                                    config.RndType = StaticData.RobotConfig.JfNames.ExtcreditsType(m.Groups[2].Value.Trim());
                                }
                                break;
                                case "随机奖励概率": {
                                    config.Random = int.Parse(m.Groups[2].Value.Trim());
                                }
                                break;
                                case "随机奖励数量": {
                                    var t1 = m.Groups[2].Value.Trim().Split('-');
                                    config.RndMin = int.Parse(t1[0]);
                                    config.RndMax = int.Parse(t1[1]);
                                }
                                break;
                                case "排行榜数量":
                                    config.Top = int.Parse(m.Groups[2].Value.Trim());
                                    break;
                                case "签到时间错误提示":
                                    config.Lang1 = m.Groups[2].Value.Trim();
                                    break;
                                case "重复签到扣分提示":
                                    config.Lang2 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到成功提示":
                                    config.Lang3 = m.Groups[2].Value.Trim();
                                    break;
                                case "重复签到不扣分提示":
                                    config.Lang4 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到帮助指令":
                                    config.Lang5 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到帮助内容":
                                    config.Lang6 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到等级指令":
                                    config.Lang7 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到等级内容":
                                    config.Lang8 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到总榜指令":
                                    config.Lang9 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到总榜内容":
                                    config.Lang10 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到月榜指令":
                                    config.Lang11 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到月榜内容":
                                    config.Lang12 = m.Groups[2].Value.Trim();
                                    break;
                                case "今日签到指令":
                                    config.Lang13 = m.Groups[2].Value.Trim();
                                    break;
                                case "今日签到内容":
                                    config.Lang14 = m.Groups[2].Value.Trim();
                                    break;
                                case "发言总榜指令":
                                    config.Lang15 = m.Groups[2].Value.Trim();
                                    break;
                                case "发言总榜内容":
                                    config.Lang16 = m.Groups[2].Value.Trim();
                                    break;
                                case "发言月榜指令":
                                    config.Lang17 = m.Groups[2].Value.Trim();
                                    break;
                                case "发言月榜内容":
                                    config.Lang18 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到信息指令":
                                    config.Lang19 = m.Groups[2].Value.Trim();
                                    break;
                                case "签到信息内容":
                                    config.Lang20 = m.Groups[2].Value.Trim();
                                    break;
                                case "随机奖励提示":
                                    config.Lang21 = m.Groups[2].Value.Trim().Split("\r\n".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).ToList();
                                    break;
                                default: {
                                    result = $@"@{e.Sender.Nickname}存在未知指令，请先发送【查看签到设置】";
                                    await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = result } });
                                }
                                return;
                            }
                            m = m.NextMatch();
                        }
                        DbUtil.SaveClusterConfig(cc);
                        result = $@"@{e.Sender.Nickname}设置成功";
                    }
                    await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = result } });
                    return;
                }
            }

            if (!config.Status)
                return;

            DateTime now = DateTime.Now;
            DateTime today = new(now.Year,now.Month,now.Day);
            ClusterSign sign = DbUtil.GetClusterSign(e.GroupUsername,e.Username);
            bool first = false;//首次发言自动签到
            if (sign == null) {
                sign = new ClusterSign() { GroupUsername = e.GroupUsername,Nickname = e.Sender.Nickname,Username = e.Username,AddTime = now,LastSentTime = now,SentCount = 1,MonthSentCount = 1 };
                first = true;
            } else {
                if (sign.LastSentTime.Year != now.Year || sign.LastSentTime.Month != now.Month || sign.LastSentTime.Day != now.Day)//每天第一次发消息
                    first = true;

                if (sign.LastSentTime.Year == now.Year && sign.LastSentTime.Month == now.Month)//月发送
                    sign.MonthSentCount += 1;
                else
                    sign.MonthSentCount = 1;

                sign.SentCount += 1;
                sign.LastSentTime = now;
            }

            if (first)//每天第一次发消息
            {
                int level = GetLevel(sign,config);
                if (config.Hellos.Count > level && !config.Hellos[level].Equals("null"))//每日首次发消息欢迎
                {
                    var result = ReplaceParams(config.Hellos[level],config,sign,null);
                    await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + result } });
                }
            }


            if (config.FirstSign && first) {
                var result = Sign(e,config,now,today,sign);
                await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + result } });
            } else if (Regex.IsMatch(e.Content.Text.Trim(),config.Cmd,RegexOptions.IgnoreCase))//签到
              {
                sign.Nickname = e.Sender.Nickname;
                if (!config.SignTime.Contains(now.Hour))//签到时间检查
                {
                    var msg = ReplaceParams(config.Lang1,config,sign,null);
                    await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + msg } });
                } else if (sign.LastSignTime.HasValue &&//重复签到检查
                      sign.LastSignTime.Value.Year == now.Year &&
                      sign.LastSignTime.Value.Month == now.Month &&
                      sign.LastSignTime.Value.Day == now.Day) {
                    if (config.RepeatMax > 0 || config.RepeatMin > 0) {
                        int iExt = r.Next(config.RepeatMin,config.RepeatMax);//随机生成积分
                        DbUtilsHelpers.UpdateExtcredits(e.GroupUsername,e.Username,config.Type,-1 * iExt);//扣分
                        string sExt = iExt.ToString() + StaticData.RobotConfig.JfNames.ExtcreditsName(config.Type);//查询积分类型名称
                        var msg = ReplaceParams(config.Lang2,config,sign);//替换群名变量
                        msg = msg.Replace("[扣除积分]",sExt);//替换[扣除积分]变量
                        await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + msg } });
                    } else {
                        var msg = ReplaceParams(config.Lang4,config,sign);
                        await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + msg } });
                    }
                } else//开始签到了
                  {
                    var result = Sign(e,config,now,today,sign);
                    await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + result } });
                }

            }
            sign.SaveClusterSign();

            if (e.Content.Text.Trim().Equals(config.Lang5))//签到帮助
            {
                var msg = ReplaceParams(config.Lang6,config,sign);
                await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + msg } });
            } else if (e.Content.Text.Trim().Equals(config.Lang7))//签到等级
              {
                StringBuilder sb = new StringBuilder();
                var template = config.Lang8.Split(new string[] { "[for]","[/for]" },StringSplitOptions.RemoveEmptyEntries);
                if (template != null) {
                    sb.Append(template[0]);
                    for (int i = 0; i < config.Days.Count; i++) {
                        sb.AppendFormat(template[1],i,config.Days[i],config.Levels[i]);
                    }
                } else
                    sb.AppendFormat("模板错误，请联系群主、管理员修改");
                var msg = ReplaceParams(sb.ToString(),config,sign);
                await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + msg } });
            } else if (e.Content.Text.Trim().Equals(config.Lang9))//签到总榜
              {
                var result = ReplaceFor(config.Lang10,DbUtil.GetSignBySignCount(e.GroupUsername,config.Top),config);
                await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + result } });
            } else if (e.Content.Text.Trim().Equals(config.Lang11))//签到月榜
              {
                var result = ReplaceFor(config.Lang12,DbUtil.GetSignByMonthSignCount(e.GroupUsername,config.Top,new DateTime(now.Year,now.Month,1)),config);
                await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + result } });
            } else if (e.Content.Text.Trim().Equals(config.Lang13))//今日签到
              {
                var result = ReplaceFor(config.Lang14,DbUtil.GetSignByLastSignTime(e.GroupUsername,config.Top,today),config);
                await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + result } });
            } else if (e.Content.Text.Trim().Equals(config.Lang15))//发言总榜
              {
                var result = ReplaceFor(config.Lang16,DbUtil.GetSignBySentCount(e.GroupUsername,config.Top),config);
                await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + result } });
            } else if (e.Content.Text.Trim().Equals(config.Lang17))//发言月榜
              {
                var result = ReplaceFor(config.Lang18,DbUtil.GetSignByMonthSentCount(e.GroupUsername,config.Top,new DateTime(now.Year,now.Month,1)),config);
                await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + result } });
            } else if (e.Content.Text.Trim().Equals(config.Lang19))//签到信息
              {
                var msg = ReplaceParams(config.Lang20,config,sign);
                await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + msg } });
            }

            if (config.Random > 0 && r.Next(1000) < config.Random)//中奖了。。。
            {
                int lExt = r.Next(config.RndMin,config.RndMax);
                DbUtilsHelpers.UpdateExtcredits(e.GroupUsername,e.Username,config.RndType,lExt);
                string msg = config.Lang21[lExt % config.Lang21.Count].Replace("[基本积分]",lExt.ToString() + StaticData.RobotConfig.JfNames.ExtcreditsName(config.RndType));
                msg = ReplaceParams(msg,config,sign);
                await SendTextMsg(bfClient.WX.UserLogin.OriginalId,new() { new() { AtUsernames = e.Username,Username = e.GroupUsername,Type = 1,Content = $" @{e.Sender.Nickname}" + msg } });
            }
        }

        private string ReplaceFor(string Template,List<ClusterSign> signs,ConfigObj config)
        {
            StringBuilder sb = new StringBuilder();
            Template = Template.Replace("[时间]",DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"));
            var template = Template.Split(new string[] { "[for]","[/for]" },StringSplitOptions.RemoveEmptyEntries);
            if (template != null && template.Length == 3) {
                sb.Append(template[0]);
                signs.ForEach(cs => {
                    sb.Append(ReplaceParams(template[1],config,cs));
                }
                );
                sb.Append(template[2]);
            } else
                sb.Append("模板错误，请联系群主、管理员修改");

            return sb.ToString();
        }

        private string Sign(ReceiveGroupMessageArgs e,ConfigObj config,DateTime now,DateTime today,ClusterSign sign)
        {
            int iExt = r.Next(config.Min,config.Max);//★基本积分

            double iContinue = 1;//★连续签到加成
            if (sign.LastSignTime.HasValue && new DateTime(sign.LastSignTime.Value.Year,sign.LastSignTime.Value.Month,sign.LastSignTime.Value.Day) == today.AddDays(-1)) {
                iContinue = Math.Pow(1 + config.Add * 1.0 / 100,sign.Continue);
                sign.Continue += 1;
            } else
                sign.Continue = 1;

            double Begin = 1;//★前n个签到加成                    
            long todayCount = DbUtil.GetSignCount(e.GroupUsername,today);//★今天已经签到的人数
            if (todayCount < config.Begin)
                Begin = (1 + config.BeginExtCredits * 1.0 / 100);

            double dExt = iExt;//计算最后得分
            dExt *= iContinue * Begin;

            int lExt = (int)dExt;//计算最后得分
            long value = DbUtilsHelpers.UpdateExtcredits(e.GroupUsername,e.Username,config.Type,lExt);//修改积分

            if (sign.LastSignTime.HasValue && sign.LastSignTime.Value.Year == now.Year && sign.LastSignTime.Value.Month == now.Month)
                sign.MonthSignCount += 1;
            else
                sign.MonthSignCount = 1;
            sign.SignCount += 1;//签到次数

            sign.Extcredits = lExt;//本次签到积分
            sign.LastSignTime = now;//签到时间
            sign.Level = GetLevel(sign,config);//签到等级

            var msg = ReplaceParams(config.Lang3,config,sign,e.Group);
            msg = ReplaceExtcredits(msg,config,value);
            msg = msg
                .Replace("[当前指令]",e.Content.Text)
                .Replace("[今日排名]",(todayCount + 1).ToString())
                .Replace("[基本积分]",iExt.ToString() + StaticData.RobotConfig.JfNames.ExtcreditsName(config.Type))
                .Replace("[今日连续加成]",iContinue.ToString("f2"))
                .Replace("[今日排名加成]",Begin.ToString("f2"));
            return msg;
        }

        private string ReplaceExtcredits(string Message,ConfigObj configObj,long value)
        {
            if (string.IsNullOrWhiteSpace(Message))
                return string.Empty;
            return Message.Replace("[总积分]",value + StaticData.RobotConfig.JfNames.ExtcreditsName(configObj.Type));
        }

        private string ReplaceParams(string Message,ConfigObj configObj,ClusterSign sign,GroupArgs e = null)
        {
            try {
                if (string.IsNullOrWhiteSpace(Message))
                    return string.Empty;

                Message = Message
                            .Replace("[签到指令]",configObj.Cmd)
                            .Replace("[签到时段]",string.Join(",",configObj.SignTime))
                            .Replace("[奖励积分]",$"{configObj.Min}-{configObj.Max}{StaticData.RobotConfig.JfNames.ExtcreditsName(configObj.Type)}")
                            .Replace("[连续加成]",configObj.Add.ToString())
                            .Replace("[前n名]",configObj.Begin.ToString())
                            .Replace("[前n名奖励]",configObj.BeginExtCredits.ToString())
                            .Replace("[重复签到]",(configObj.RepeatMin > 0 || configObj.RepeatMax > 0) ? "重复签到扣分" : "重复签到不扣分")
                            .Replace("[重复签到扣分]",$"{configObj.RepeatMin}-{configObj.RepeatMax}{StaticData.RobotConfig.JfNames.ExtcreditsName(configObj.Type)}")
                            .Replace("[Username]",sign.Username)
                            .Replace("[昵称]",sign.Nickname)
                            .Replace("[累计签到]",sign.SignCount.ToString())
                            .Replace("[本月签到]",sign.MonthSignCount.ToString())
                            .Replace("[连续签到]",sign.Continue.ToString())
                            .Replace("[上次签到]",sign.LastSignTime.HasValue ? sign.LastSignTime.Value.ToString() : "无")
                            .Replace("[当前等级]",configObj.Levels[sign.Level])
                            .Replace("[本次积分]",sign.Extcredits.ToString() + StaticData.RobotConfig.JfNames.ExtcreditsName(configObj.Type))
                            .Replace("[首次发言]",sign.AddTime.ToString())
                            .Replace("[最后发言]",sign.LastSentTime.ToString())
                            .Replace("[累计发言]",sign.SentCount.ToString())
                            .Replace("[本月发言]",sign.MonthSentCount.ToString())
                            .Replace("[签到时间]",string.Join(",",configObj.SignTime));

                if (sign.Level + 1 < configObj.Levels.Count) {
                    Message = Message.Replace("[再签到]",(configObj.Days[sign.Level + 1] - sign.SignCount).ToString())
                             .Replace("[下一等级]",configObj.Levels[sign.Level + 1])
                             .Replace("[下一级天数]",configObj.Days[sign.Level + 1].ToString());
                }
                return Message;
            } catch (Exception ex) {
                Eve.OnLog(this,$"替换变量失败：{ex.Message}\n{ex.StackTrace}");
            }
            return null!;
        }

        private int GetLevel(ClusterSign sign,ConfigObj configObj)
        {
            for (int i = configObj.Days.Count - 1; i >= 0; i--) {
                if (sign.SignCount >= configObj.Days[i])
                    return i;
            }

            return 0;
        }

        public override void Install()
        {
            Eve.OnLog(this,"初始化成功");
        }
    }
}