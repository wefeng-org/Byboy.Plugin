using Byboy.SignPlugin.DbUtils;
using MG.WeCode.Entitys;
using MG.WeCode.WeClients;
using Plugin;
using Plugin.DbEntitys;
using SuperWx;
using WeFeng.Db;

namespace Byboy.SignPlugin
{
    public class Plugin : IPlugin
    {
        public string Name => "签到插件";

        public string Version => "v1.0";

        public string Author => "Byboy";

        public string Description => "用户签到";

        public BaseConfig Config { get; set; }
        public bool IsOpen { get; set; }
        public Random r = new();

        public void Initialize()
        {
            // 初始化数据库
            DbUtil.Db.DbMaintenance.CreateDatabase();
            // 初始化表
            DbUtil.Db.CodeFirst.InitTables(typeof(Sign));
            // 订阅收到群消息事件
            Eve.ReceiveGroupMessage += Eve_ReceiveGroupMessage;

            Eve.OnLog(this,"开启成功");
        }

        private async void Eve_ReceiveGroupMessage(object? sender,global::Plugin.EveEntitys.ReceiveGroupMessageArgs e)
        {
            try {
                var ttttt = r.Next(10,100);
                var bfClient = sender as TLS_BFClent;
                // 判断是否为签到指令
                if (e.Content.Text.Contains("签到") && (e.Content.AtUsernameList != null && e.Content.AtUsernameList.Contains(bfClient.WX.UserLogin.Username))) {
                    // 查询是否已经签到
                    var sign = DbUtil.Db.Queryable<Sign>().Where(it => it.Username == e.Username && it.GroupUsername == e.GroupUsername).First();
                    if (sign == null) {
                        // 未签到
                        // 插入签到记录
                        await DbUtil.Db.Insertable(new Sign() {
                            Username = e.Username,
                            GroupUsername = e.GroupUsername,
                            SignTime = DateTime.Now,
                            SignDays = 1
                        }).ExecuteCommandAsync();

                        //添加积分,随机积分10~100
                        var interal = DbUtilsHelpers.GetIntegral(e.GroupUsername,e.Username);
                        interal.Jf0 += ttttt;
                        interal.AddOrUpdateIntegralList();

                        // 回复签到成功
                        await WeClient.Messages.SendTextMsg(bfClient.WX.UserLogin.OriginalId,new List<MultiMessageArgs> { new MultiMessageArgs { Username = e.GroupUsername,Type = 1,Content = $"@{e.Sender.Nickname},签到成功,这是你连续签到的第1天,奖励{StaticData.RobotConfig.JfNames.Jf0Name}:{ttttt},剩余{StaticData.RobotConfig.JfNames.Jf0Name}:{ttttt}",AtUsernames = e.Sender.Username }, });
                    } else {
                        // 已签到
                        // 判断是否为当天签到
                        if (sign.SignTime.Date == DateTime.Now.Date) {
                            // 已签到
                            await WeClient.Messages.SendTextMsg(bfClient.WX.UserLogin.OriginalId,new List<MultiMessageArgs> { new MultiMessageArgs { Username = e.GroupUsername,Type = 1,Content = $"@{e.Sender.Nickname},你今天已经签到过了哟~",AtUsernames = e.Sender.Username } });
                        } else {

                            // 未签到
                            // 判断是否为连续签到
                            if (sign.SignTime.Date == DateTime.Now.Date.AddDays(-1)) {
                                // 连续签到
                                // 更新签到记录
                                await DbUtil.Db.Updateable(new Sign() {
                                    Id = sign.Id,
                                    Username = e.Username,
                                    GroupUsername = e.GroupUsername,
                                    SignTime = DateTime.Now,
                                    SignDays = sign.SignDays + 1
                                }).ExecuteCommandAsync();
                                // 更新签到记录
                                await DbUtil.Db.Updateable(new Sign() {
                                    Id = sign.Id,
                                    Username = e.Username,
                                    GroupUsername = e.GroupUsername,
                                    SignTime = DateTime.Now,
                                    SignDays = 1
                                }).ExecuteCommandAsync();
                                //添加积分,随机积分10~100
                                var interal = DbUtilsHelpers.GetIntegral(e.GroupUsername,e.Username);
                                interal.Jf0 += ttttt;
                                interal.AddOrUpdateIntegralList();
                                // 回复签到成功
                                await WeClient.Messages.SendTextMsg(bfClient.WX.UserLogin.OriginalId,new List<MultiMessageArgs> { new MultiMessageArgs { Username = e.GroupUsername,Type = 1,Content = $"@{e.Sender.Nickname},签到成功,这是你连续签到的第{sign.SignDays + 1}天,奖励{StaticData.RobotConfig.JfNames.Jf0Name}:{ttttt},剩余{StaticData.RobotConfig.JfNames.Jf0Name}:{ttttt}",AtUsernames = e.Sender.Username } });
                            } else {
                                // 未连续签到
                                // 更新签到记录
                                await DbUtil.Db.Updateable(new Sign() {
                                    Id = sign.Id,
                                    Username = e.Username,
                                    GroupUsername = e.GroupUsername,
                                    SignTime = DateTime.Now,
                                    SignDays = 1
                                }).ExecuteCommandAsync();
                                //添加积分,随机积分10~100
                                var interal = DbUtilsHelpers.GetIntegral(e.GroupUsername,e.Username);
                                interal.Jf0 += ttttt;
                                interal.AddOrUpdateIntegralList();
                                // 回复签到成功
                                await WeClient.Messages.SendTextMsg(bfClient.WX.UserLogin.OriginalId,new List<MultiMessageArgs> { new MultiMessageArgs { Username = e.GroupUsername,Type = 1,Content = $"@{e.Sender.Nickname},签到成功,这是你连续签到的第{1}天,奖励{StaticData.RobotConfig.JfNames.Jf0Name}:{ttttt},剩余{StaticData.RobotConfig.JfNames.Jf0Name}:{ttttt}",AtUsernames = e.Sender.Username } });
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                Eve.OnLog(this,$"出错了{ex}");
            }
        }

        public void Install()
        {
            Eve.OnLog(this,"初始化成功");
        }

        public void Terminate()
        {
            Eve.ReceiveGroupMessage -= Eve_ReceiveGroupMessage;
        }
    }
}