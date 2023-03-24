using Plugin;
using SuperWx;

namespace Byboy.SingleFriend
{
    /// <summary>
    /// 黑粉检测插件
    /// </summary>
    public class MailPlugin : IPlugin
    {
        /// <summary>
        /// 作者
        /// </summary>
        public override string Author => "Byboy";
        /// <summary>
        /// 插件描述
        /// </summary>
        public override string Description => "用户检查用户是否为单项好友";
        /// <summary>
        /// 插件名称
        /// </summary>
        public override string Name => "黑粉检查";
        /// <summary>
        /// 插件版本
        /// </summary>
        public override string Version => "v1.0.0.0";
        /// <summary>
        /// 插件初始化
        /// </summary>
        public override void Initialize()
        {
            ReceiveFriendMessage += MailPlugin_ReceiveFriendMessage;
        }
        List<string> hfList = new();

        private async Task MailPlugin_ReceiveFriendMessage(WXUserLogin sender,Plugin.EveEntitys.ReceiveFriendMessageArgs e)
        {
            var orid = sender.OriginalId;
            //判断发消息的人是否为管理员
            if (StaticData.RobotConfig.AdminUsername.Split(",").Contains(e.Username)) {
                //如果是管理员则执行
                // 判断消息是否为指定的消息“查询黑粉”
                if (e.Content.MsgType == Plugin.EveEntitys.MessageType.NORMALIM) {
                    if (e.Content.Text.Equals("查询黑粉")) {
                        List<string> usernameList = new();
                        int currentWxcontactSeq = 0;
                        WeChat.Pb.Entites.InitContactResp t = null;
                        //拿到所有的好友列表
                        do {
                            t = await InitContact(orid,currentWxcontactSeq,int.MaxValue);
                            currentWxcontactSeq = t.CurrentWxcontactSeq;
                            usernameList.AddRange(t.ContactUsernameList);
                        } while (t.ContactUsernameList.Length != 0);
                        //查询好友信息

                        int i = 0;
                        do {
                            var GetUserList = new List<string>();
                            if (usernameList.Count - i < 20) {
                                GetUserList = usernameList.ToArray()[i..].ToList();
                            } else {
                                GetUserList = usernameList.ToArray()[i..(i + 20)].ToList();
                            }
                            WeChat.Pb.Entites.GetContactResponse getContactResponse = await GetContact(orid,GetUserList);
                            hfList.AddRange(getContactResponse.Ticket.Select(t => t.Username));
                            i += 20;
                        } while (i > usernameList.Count);

                        //创建一个标签
                        var listResp = await CreateLabels(orid,new List<string> { "黑粉" });
                        var labelId = listResp.LabelPairList[0].LabelId;
                        hfList.ForEach(async t => {
                            await UpdataFriendLabels(orid,t,new List<int> { (int)labelId });
                        });

                        await SendTextMsg(orid,e.Username,$"共检查到{hfList.Count}个黑粉，已经放入黑粉标签中，请查看，如需删除，请发送指令“删除黑粉”");
                        e.Cancel = true;

                    } else if (e.Content.Text.Equals("删除黑粉")) {
                        // 需要删除好友接口
                        await SendTextMsg(orid,e.Username,$"共检删除了{hfList.Count}个黑粉");
                        e.Cancel = true;
                    }
                }
                //非指定命令无视命令
            }
            //非管理员无视命令
        }
    }
}
