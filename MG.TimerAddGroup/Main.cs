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
        public DateTime LastGetList { get; set; } = DateTime.MinValue;
        public List<AddGroupAfter> addGroupAfters = new List<AddGroupAfter>();
        public List<AddGroup> addGroups = new List<AddGroup>();

        private Timer timer;
        public override void Initialize()
        {

            Init();
            timer = new Timer(TimerEnc,null!,1000,-1);
            ReceiveSystemMessage += Main_ReceiveSystemMessage;
            ReceiveGroupMessage += Main_ReceiveGroupMessage;
            base.Initialize();
        }
        void Init()
        {
            var t = DbUtil.Db.Queryable<AddGroupAfter>().ToList();
            addGroupAfters.AddRange(t);
            var t1 = DbUtil.Db.Queryable<AddGroup>().ToList();
            addGroups.AddRange(t1);
        }
        private async Task Main_ReceiveGroupMessage(SuperWx.WXUserLogin sender,Plugin.EveEntitys.ReceiveGroupMessageArgs e)
        {
            if (addGroupAfters.Where(t => t.GroupUsername == e.GroupUsername).Count() != 1) {
                var t = await DbUtil.Db.Queryable<AddGroupAfter>().Where(it => it.GroupUsername == e.GroupUsername).FirstAsync();
                if (t == null) {
                    await ModChatRoomNotify(sender.OriginalId,e.GroupUsername,0);
                    await ModChatRoomMsgBox(sender.OriginalId,e.GroupUsername,0);
                    t = new AddGroupAfter { GroupUsername = e.GroupUsername,IsNoRemind = true };
                    await DbUtil.Db.Insertable(t).ExecuteCommandIdentityIntoEntityAsync();
                    addGroupAfters.Add(t);
                } else {
                    addGroupAfters.Add(t);
                }
            }
            if (addGroups.Where(t => t.GroupUsername == e.GroupUsername).Count() != 1) {
                var t1 = addGroups.FirstOrDefault(t => t.GroupName.Contains(e.Group.Name));
                if (t1 == null) {
                    t1 = await DbUtil.Db.Queryable<AddGroup>().Where(t => t.GroupName.Contains(e.Group.Name)).FirstAsync();
                }
                t1.IsAdd = true;
                t1.GroupUsername = e.GroupUsername;
                await DbUtil.Db.Updateable(t1).ExecuteCommandHasChangeAsync();
                addGroups.Add(t1);
            }

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
            //初始化数据库
            DbUtil.Db.DbMaintenance.CreateDatabase();
            // 初始化表
            DbUtil.Db.CodeFirst.InitTables(typeof(AddGroup),typeof(AddGroupAfter));
            base.Install();
        }
        List<Group> groupList = HttpUtil.Get<List<Group>>("http://120.26.62.16:7050/api/ELE/GetMTQRCode");
        static bool isFrist = true;

        private async void TimerEnc(object o)
        {
            timer.Change(-1,-1);
            if (isFrist) {
                isFrist = false;
                await Task.Delay(100 * 1000);
            }
            //判断当前时间是否在早上8点到晚上8点之间,如果不在就不执行
            if (DateTime.Now.Hour < 8 || DateTime.Now.Hour > 20) {
                timer.Change(1000,-1);
                groupList = null;
                return;
            }
            try {
                if (groupList == null || LastGetList.Date < DateTime.Now.Date) {
                    groupList = HttpUtil.Get<List<Group>>("http://120.26.62.16:7050/api/ELE/GetMTQRCode");
                    LastGetList = DateTime.Now;
                }
                //请求http://120.26.62.16:7050/api/ELE/GetMTQRCode接口获取所有群的信息
                foreach (var group in groupList) {
                    //查询当前群是否已经添加过
                    OnLog($"即将查询{group.groupname}是否加过");
                    var addGroup = DbUtil.Db.Queryable<AddGroup>()
                        .Where(it => it.GroupCode == group.groupqrcodevalue)
                        .Count();
                    //如果没有添加过群
                    if (addGroup == 0) {
                        var random = new Random();
                        var waitTime = random.Next(30,60);
                        //添加群
                        foreach (var user in StaticData.users) {
                            if (user.IsLogin == false) {
                                break;
                            }
                            //查询当前人最近加群的时间,如果大于1小时就可以添加群
                            var userAddGroup = DbUtil.Db.Queryable<AddGroup>()
                                .Where(it => it.AddUsername == user.Username).OrderBy(it => it.AddTime,OrderByType.Desc)
                                .First();

                            if (userAddGroup == null || (DateTime.Now - userAddGroup.AddTime).TotalHours > 1) {
                                OnLog($"等待{waitTime}秒");
                                await Task.Delay(waitTime * 1000);
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
                                OnLog($"加群的url为{t.FullUrl}");
                                var html = HttpUtil.Get(t.FullUrl);

                                //post请求t.FullUrl的值
                                var t1 = HttpUtil.Post(t.FullUrl);
                                OnLog("第一次返回" + t1);
                                await Task.Delay(5 * 1000);
                                var t2 = HttpUtil.Post(t.FullUrl);
                                OnLog("第二次返回" + t2);
                                var headIndex = t2.IndexOf("weixin://jump/mainframe/");
                                if (headIndex == -1) { OnLog("添加群聊失败,原因:无返回群号"); continue; }
                                var head = t2[(headIndex + 24)..];
                                var breadIndex = head.IndexOf("\"");
                                var groupUsername = head[..breadIndex];
                                //添加记录
                                DbUtil.Db.Insertable(new AddGroup() {
                                    GroupA8keyUrl = t.FullUrl,
                                    AddUsername = user.Username,
                                    GroupCode = group.groupqrcodevalue,
                                    GroupName = group.groupname,
                                    AddTime = DateTime.Now,
                                    IsAdd = true,
                                    GroupUsername = groupUsername,
                                    AddGroupText = html
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
                } catch (global::System.Exception ex) {
                    OnLog($"定时器已经释放{ex}");
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
        /// 发送get请求,返回string类型
        /// </summary>
        public static string Get(this string url)
        {
            var result = http.GetStringAsync(url).Result;
            return result;
        }
        /// <summary>
        /// 发送post请求,不需要参数和返回值
        /// </summary>
        public static string Post(this string url)
        {
            return http.PostAsync(url,null).Result.Content.ReadAsStringAsync().Result;
        }
    }

    public class Group
    {
        public string groupqrcode { get; set; }
        public string groupname { get; set; }
        public string groupqrcodevalue { get; set; }
    }

}
