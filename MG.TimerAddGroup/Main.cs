using MG.TimerAddGroup.DbUtils;
using Plugin;
using SqlSugar;
using System.Net.Http.Json;

namespace MG.TimerAddGroup
{
    public class Main : IPlugin
    {
        public override string Author => "美逛科技";
        public override string Description => "他会定时的加群";
        public override string Name => "定时加群";
        public override string Version => "v1.0";

        private Timer timer;
        public override void Initialize()
        {
            DbUtil.Db.DbMaintenance.CreateDatabase();
            // 初始化表
            DbUtil.Db.CodeFirst.InitTables(typeof(AddGroup));
            timer = new Timer(TimerEnc,null!,1000,-1);
            ReceiveSystemMessage += Main_ReceiveSystemMessage;
             
            
            base.Initialize();
        }

        private async Task Main_ReceiveSystemMessage(SuperWx.WXUserLogin sender,Plugin.EveEntitys.ReceiveSystemMessageArgs e)
        {

            if (e.Content.Contains("你通过扫描二维码加入群聊")) {
                var groupUsername = e.FromeUsername;
                var modContact = await GetContact(sender.OriginalId,new List<string> { groupUsername });
                var mod = modContact.ContactList[0];
                var t = await DbUtil.Db.Queryable<AddGroup>().Where(t => t.GroupName.Contains(mod.NickName.String_t) || mod.NickName.String_t.Contains(t.GroupName)).FirstAsync();
                t.AgreeTime = DateTime.Now;
                t.IsAdd = true;
                t.GroupUsername = groupUsername;
                await DbUtil.Db.Updateable(t).ExecuteCommandHasChangeAsync();
            }
        }

        public override void Install()
        {
            base.Install();
        }
        List<Group> groupList = HttpUtil.Get<List<Group>>("http://120.26.62.16:7050/api/ELE/GetMTQRCode");

        private async void TimerEnc(object o)
        {
            timer.Change(-1,-1);
            //判断当前时间是否在早上8点到晚上8点之间,如果不在就不执行
            if (DateTime.Now.Hour < 8 || DateTime.Now.Hour > 20) {
                timer.Change(1000,-1);
                groupList = null;
                return;
            }
            try {
                if (groupList == null) {
                    groupList = HttpUtil.Get<List<Group>>("http://120.26.62.16:7050/api/ELE/GetMTQRCode");
                }
                //请求http://120.26.62.16:7050/api/ELE/GetMTQRCode接口获取所有群的信息
                foreach (var group in groupList) {
                    //查询当前群是否已经添加过
                    var addGroup = DbUtil.Db.Queryable<AddGroup>()
                        .Where(it => it.GroupCode == group.groupqrcodevalue)
                        .Count();
                    //如果没有添加过群
                    if (addGroup == 0) {
                        //添加群
                        foreach (var user in StaticData.users) {
                            if (user.IsLogin == false) {
                                break;
                            }
                            //查询当前人最近加群的时间,如果大于1小时就可以添加群
                            var userAddGroup = DbUtil.Db.Queryable<AddGroup>()
                                .Where(it => it.AddUsername == user.Username)
                                .OrderBy(it => it.AddTime,OrderByType.Desc)
                                .First();
                            if (userAddGroup == null || (DateTime.Now - userAddGroup.AddTime).TotalHours > 1) {
                                //随机等待10~20分钟,如果是DEBUG则不等待
                                var random = new Random();
                                var waitTime = random.Next(10,20);
                                OnLog($"等待{waitTime}分钟");
                                Thread.Sleep(waitTime * 1000);
                                OnLog($"即将使用{user.OriginalId}加群");
                                var t = await GetA8Key(user.OriginalId,group.groupqrcodevalue,4);
                                if (t == null) {
                                    OnLog($"添加群失败,原因:获取A8Key失败");
                                    continue;
                                }
                                //如果t.BaseResponse.Ret不等于0,则添加群失败
                                if (t.BaseResponse.Ret != 0) {
                                    OnLog($"添加群失败,原因:{t.BaseResponse.ErrMsg.String_t}");
                                    continue;
                                }
                                //post请求t.FullUrl的值
                                HttpUtil.Post(t.FullUrl);
                                //添加记录
                                DbUtil.Db.Insertable(new AddGroup() {
                                    AddUsername = user.Username,
                                    GroupCode = group.groupqrcodevalue,
                                    GroupName = group.groupname,
                                    AddTime = DateTime.Now
                                }).ExecuteCommand();
                                return;
                            }
                        }
                        return;
                    }
                }
            } catch (Exception ex) {
                OnLog(ex.ToString());
            } finally {
                try {
                    timer.Change(1000,-1);
                } catch (global::System.Exception) {

                }
            }
        }

        public override void Terminate()
        {
            timer.Dispose();
            base.Terminate();
        }
    }

    internal static class HttpUtil
    {
        /// <summary>
        /// 创建一个HttpClient对象
        /// </summary>
        private static readonly HttpClient http = new HttpClient();
        /// <summary>
        /// 发送get请求
        /// </summary>
        public static T Get<T>(this string url)
        {
            var result = http.GetFromJsonAsync<T>(url).Result;
            return result;
        }
        /// <summary>
        /// 发送post请求,不需要参数和返回值
        /// </summary>
        public static void Post(this string url)
        {
            http.PostAsync(url,null).Wait();
        }
    }

    public class Group
    {
        public string groupqrcode { get; set; }
        public string groupname { get; set; }
        public string groupqrcodevalue { get; set; }
    }

}
