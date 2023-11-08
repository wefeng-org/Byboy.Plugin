using Byboy.CocopilotGroup.DbUtils;
using MG.WeCode.Entitys;
using Plugin;
using Plugin.EveEntitys;
using SuperWx;
using System.Text.Json;

namespace Byboy.CocopilotGroup
{
    public class Plugin : IPlugin
    {
        public override string Author => "Byboy";
        public override string Name => "COCOPLIOT群专用插件";
        public override string Version => "v1.0";
        public override string Description => "COCOPLIOT群专用插件";
        public override BaseConfig Config { get; set; }
        public override bool IsOpen { get; set; }
        public override void Initialize()
        {
            // 初始化数据库
            try {
                DbUtil.Db.DbMaintenance.CreateDatabase();
                // 初始化表
                DbUtil.Db.CodeFirst.InitTables(typeof(TBGroupDetail));
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }

            // 订阅收到群消息事件
            ReceiveGroupMessage += Plugin_ReceiveGroupMessage;
            // 订阅系统消息事件
            ReceiveSystemMessage += Plugin_ReceiveSystemMessage;
            //订阅群组成员变动事件
            ReceiveGroupMemberEnter += Plugin_ReceiveGroupMemberEnter;
            //订阅好友消息事件
            ReceiveFriendMessage += Plugin_ReceiveFriendMessage;
            //订阅好友添加事件
            ReceiveNewFriend += Plugin_ReceiveNewFriend;
            Eve.OnLog(this,"开启成功");
        }

        private async Task Plugin_ReceiveNewFriend(WXUserLogin sender,ReceiveNewFriendArgs e)
        {
            OnLog($"触发了加好友{JsonSerializer.Serialize(e)}");
            await Task.Delay(1000);
            var tt = await VerifyUser(sender.OriginalId,e.EncryptUsername,e.Ticket,e.Ticket,"",3,17);
            OnLog(JsonSerializer.Serialize(tt));
        }

        private async Task Plugin_ReceiveFriendMessage(WXUserLogin sender,ReceiveFriendMessageArgs e)
        {
            //if (e.Content.Text.Contains("564312978")) {
            //    var group = await DbUtil.Db.Queryable<TBGroupDetail>().Where(t => t.GroupCount < 500).OrderBy(t => t.Id).FirstAsync();
            //    OnLog(JsonSerializer.Serialize(group));
            //    await SendTextMsg(sender.OriginalId,new List<MultiMessageArgs> { new() { Username = e.Username,Type = 0,Content = $"欢迎你,{e.Sender.Nickname},马上邀请你进群" } });
            //    await Task.Delay(1000 * 3);
            //    var tt = await InviteGroupMemberRequest(sender.OriginalId,group.GroupUsername,new List<string> { e.Username });
            //    OnLog(JsonSerializer.Serialize(tt));
            //}

            if (IsRobotAdmins(e.Username) || e.Username == "wxid_nrp71pjkl4ws22") {
                if (e.Content.Text.Contains("开车")) {
                    var catName = e.Content.Text;
                    catName = catName.Replace("开车","");
                    catName = catName.Replace(" ","");
                    //判断是要开的车是否在数据库中
                    var car = await DbUtil.Db.Queryable<TBCar>().Where(t => t.CatName == catName).FirstAsync();
                    //如果不在,则添加到数据库
                    if (car == null) {
                        car = new TBCar() { CatName = catName, CatId = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString(), IsOpen = false, Uuid = new() };
                        await DbUtil.Db.Insertable(car).ExecuteCommandAsync();
                        //发送消息这个人,回复开车成功
                        await SendTextMsg(sender.OriginalId,new List<MultiMessageArgs> { new() { Username = e.Username,Type = 0,Content = $"开车成功,车名:{catName}" } });
                        //获取数据库中的群组
                        var groups = await DbUtil.Db.Queryable<TBGroupDetail>().ToListAsync();
                        foreach (var item in groups) {
                            //发送消息到群组,开车了
                            await SendTextMsg(sender.OriginalId,new List<MultiMessageArgs> { new() { Username = item.GroupUsername,Type = 0,Content = $"@全体人员 开车了,车名{catName}",AtUsernames = "notify@all" } });
                        }
                    } else {
                        //给这个人发送消息,车已在数据库了
                        await SendTextMsg(sender.OriginalId,new List<MultiMessageArgs> { new() { Username = e.Username,Type = 0,Content = $"车已在数据库了,车名:{catName}" } });
                    }
                }
            }
        }

        private async Task Plugin_ReceiveGroupMemberEnter(WXUserLogin sender,ReceiveGroupMemberEnterArgs e)
        {
            await Task.Delay(5 * 1000);
            //查看群组是否存在,如果存在,则更新群组人数
            var group = await DbUtil.Db.Queryable<TBGroupDetail>().Where(t => t.GroupUsername == e.GroupUsername).FirstAsync();
            if (group != null) {
                group.GroupCount = e.Group.GroupMembers.Count;
                await DbUtil.Db.Updateable(group).ExecuteCommandAsync();
            }
        }

        private async Task Plugin_ReceiveSystemMessage(WXUserLogin sender,ReceiveSystemMessageArgs e)
        {
            if (e.Content.Contains("现在可以开始聊天了")) {
                //获取人数不足500人的群id序号最小的群
                var group = await DbUtil.Db.Queryable<TBGroupDetail>().Where(t => t.GroupCount < 500).OrderBy(t => t.Id).FirstAsync();
                //给用户发消息,欢迎你,马上邀请你进群
                await SendTextMsg(sender.OriginalId,new List<MultiMessageArgs> { new() { Username = e.FromeUsername,Type = 0,Content = $"欢迎你,{e.FromeUsername},马上邀请你进群" } });
                await Task.Delay(1000 * 3);
                //邀请用户进入群组
                await InviteGroupMemberRequest(sender.OriginalId,group.GroupUsername,new List<string> { e.FromeUsername });
            }
        }



        private async Task Plugin_ReceiveGroupMessage(WXUserLogin sender,ReceiveGroupMessageArgs e)
        {
            if (IsRobotAdmins(e.Username) && e.Content.Text.Contains("添加此群")) {
                var text = $"添加{e.Group.Name}({e.GroupUsername})到数据库";
                var count = await DbUtil.Db.Queryable<TBGroupDetail>().Where(t => t.GroupUsername == e.GroupUsername).CountAsync();
                if (count == 0) {
                    text += "成功!";
                    await DbUtil.Db.Insertable(new TBGroupDetail() { GroupName = e.Group.Name,GroupUsername = e.GroupUsername,GroupCount = e.Group.GroupMembers.Count }).ExecuteCommandAsync();
                } else {
                    text += "失败!";
                }
                await SendTextMsg(sender.OriginalId,new List<MultiMessageArgs> { new() { Username = e.GroupUsername,Type = 0,Content = text } });
            }



            if (e.Content.MsgType == MessageType.NORMALIM && (e.Content.Text.Contains("帮助") || e.Content.Text.Contains("怎么办") || e.Content.Text.Contains("啥原因"))) {
                await SendTextMsg(sender.OriginalId,new List<MultiMessageArgs> { new() { Username = e.GroupUsername,Type = 0,Content = "帮助文档:\r\nhttps://www.yuque.com/ningmengguorou/lll50a/ti3yq1v4khsrsyfd" } });
            }
        }
        private bool IsRobotAdmins(string username)
        {
            return StaticData.RobotConfig.AdminUsername.Split(",").Contains(username);
        }
    }
}
