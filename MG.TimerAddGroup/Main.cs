using MG.TimerAddGroup.DbUtils;
using Out.YunFaDan.Model;
using Plugin;
using System.Net.Http.Json;

namespace MG.TimerAddGroup
{
    public class Main : IPlugin
    {
        public override string Author => "美逛科技";
        public override string Description => "他会定时的加群";
        public override string Name => "定时加群";
        public override string Version => "v1.0";
        private DateTime LastGetList { get; set; } = DateTime.MinValue;
        private List<AddGroupAfter> addGroupAfters = new List<AddGroupAfter>();
        private List<AddGroup> addGroups = new List<AddGroup>();

        private Timer timer;
        private Timer timer1;
        public override void Initialize()
        {

            Init();
            timer1 = new Timer(TimerQuic,null!,60 * 1000,-1);
            //timer = new Timer(TimerEnc,null!,1000,-1);
            base.Initialize();
        }
        void Init()
        {
            var t = DbUtil.Db.Queryable<AddGroupAfter>().ToList();
            addGroupAfters.AddRange(t);
            var t1 = DbUtil.Db.Queryable<AddGroup>().ToList();
            addGroups.AddRange(t1);
        }

        public override void Install()
        {
            //初始化数据库
            DbUtil.Db.DbMaintenance.CreateDatabase();
            // 初始化表
            DbUtil.Db.CodeFirst.InitTables(typeof(AddGroup),typeof(AddGroupAfter));
            //Test();
            base.Install();
        }




        //static bool isFrist = true;
        // 设置17个线程信号量
        static SemaphoreSlim GetContactSemaphoreSlim = new SemaphoreSlim(17);
        static SemaphoreSlim GetQrCodeSemaphoreSlim = new SemaphoreSlim(17);
        internal static List<string> q = new();
        private void TimerQuic(object o)
        {
            timer1.Change(-1,-1);
            OnLog("开始");
            try {
                var list = PgSqlDbUtil.Db.Queryable<TbGroupInfo>().Where(t => t.OldUsername != null && t.GroupCountStatus != -2).OrderBy(t => t.GroupCountCallTime).GroupBy(t => t.OldUsername).ToList();
                foreach (var item in list) {
                    Task.Run(() => {

                    });

                }


                //var list = DbUtil.Db.Queryable<AddGroup>().Where(t => !string.IsNullOrWhiteSpace(t.GroupUsername)).ToList();
                //var users = StaticData.users.Where(t => t.IsLogin).ToList();
                //foreach (var user in users) {
                //    if (user != null && user.IsLogin) {
                //        var addGroups = DbUtil.Db.Queryable<AddGroup>().Where(t => t.AddUsername == user.Username && t.LastQrCode.AddDays(7) < DateTime.Now).First();

                //        OnLog($"群信息:{JsonSerializer.Serialize(addGroups)}");
                //        Task.Run(async () => {
                //            try {
                //                GetContactSemaphoreSlim.Wait();
                //                var getContactResponse = await GetContact(user.OriginalId,new() { addGroups.GroupUsername! });
                //                if (getContactResponse == null &&getContactResponse.BaseResponse.Ret == 0 && getContactResponse.ContactCount > 0) {
                //                    addGroups.LastQrCode = DateTime.Now;
                //                    DbUtil.Db.Updateable(addGroups).ExecuteCommand();
                //                    OnLog($"自己获取的群信息:{JsonSerializer.Serialize(getContactResponse)}");

                //                    var contact = getContactResponse.ContactList.FirstOrDefault();
                //                    var tbGroupInfo = PgSqlDbUtil.Db.Queryable<TbGroupInfo>().Where(t => t.GroupName == contact!.NickName.String_t).First();
                //                    tbGroupInfo ??= new() {
                //                        GroupHead = contact!.BigHeadImgUrl,
                //                        CreateTime = DateTime.Now,
                //                        UpdateTime = DateTime.Now,
                //                        GroupCount = (int)contact.NewChatroomData.MemberCount,
                //                        GroupId = contact.UserName.String_t,
                //                        GroupName = contact.NickName.String_t,
                //                    };
                //                    tbGroupInfo.GroupCountTime = DateTime.Now;
                //                    tbGroupInfo.GroupCountCallTime = DateTime.Now;
                //                    tbGroupInfo.GroupCountStatus = 0;
                //                    tbGroupInfo.GroupCountSuccessTime = DateTime.Now;
                //                    PgSqlDbUtil.Db.Storageable(tbGroupInfo).WhereColumns(t => t.GroupName).ExecuteCommand();
                //                } else {
                //                    q.Add(addGroups.GroupName);
                //                    OnLog($"获取群信息失败{JsonSerializer.Serialize(getContactResponse)}");
                //                }
                //            } finally {
                //                GetContactSemaphoreSlim.Release();
                //            }
                //        });
                //        Task.Run(async () => {
                //            try {
                //                GetQrCodeSemaphoreSlim.Wait();
                //                var qrCode = await GetQRCode(user.OriginalId,addGroups.GroupUsername!);
                //                if (qrCode != null && qrCode.BaseResponse.Ret == 0) {
                //                    var inviteLink = ZXingUtils.DecodeQRCodeFromImageBytes(qrCode.Qrcode.Buffer);
                //                    OnLog($"自己获取的qrcode:{inviteLink}");
                //                    var tbGroupInfo = PgSqlDbUtil.Db.Queryable<TbGroupInfo>().Where(t => t.GroupName == addGroups.GroupName).First();
                //                    tbGroupInfo ??= new() {
                //                        CreateTime = DateTime.Now,
                //                        UpdateTime = DateTime.Now,
                //                        GroupId = addGroups.GroupUsername,
                //                        GroupName = addGroups.GroupName,
                //                    };
                //                    tbGroupInfo.GroupQrcode = inviteLink;
                //                    tbGroupInfo.GroupQrcodeStatus = 0;
                //                    tbGroupInfo.GroupNativeQrcodeTime = DateTime.Now;
                //                    PgSqlDbUtil.Db.Storageable(tbGroupInfo).WhereColumns(t => t.GroupName).ExecuteCommand();
                //                } else {
                //                    OnLog($"获取QrCode失败{JsonSerializer.Serialize(qrCode)}");
                //                }
                //            } finally {
                //                GetQrCodeSemaphoreSlim.Release();
                //            }
                //        });
                //    }

                //}
            } catch (Exception ex) {
                Console.WriteLine(ex);
            } finally {
                timer1.Change(60 * 1000,60 * 1000);
            }
        }



        private async void TimerEnc(object o)
        {
            timer.Change(-1,-1);
            //            if (isFrist) {
            //                isFrist = false;
            //#if DEBUG
            //                await Task.Delay(10 * 1000);
            //#else
            //                await Task.Delay(100 * 1000);
            //#endif
            //            }
            //            //判断当前时间是否在早上8点到晚上8点之间,如果不在就不执行
            //            if (DateTime.Now.Hour < 8 || DateTime.Now.Hour > 20) {
            //                timer.Change(1000,-1);
            //                groupList = null!;
            //                return;
            //            }
            //            try {
            //                if (groupList == null || LastGetList.Date < DateTime.Now.Date) {
            //                    groupList = HttpUtil.Get<List<Group>>("http://120.26.62.16:7050/api/ELE/GetMTQRCode");
            //                    LastGetList = DateTime.Now;
            //                    OnLog($"共获取了{groupList.Count}个群");
            //                }
            //                //请求http://120.26.62.16:7050/api/ELE/GetMTQRCode接口获取所有群的信息
            //                foreach (var group in groupList) {
            //                    var count = addGroups.Where(t => t.GroupCode == group.groupqrcodevalue).Count();
            //                    if (count != 0) {
            //                        //跳过这次循环
            //                        continue;
            //                    }
            //                    //查询当前群是否已经添加过
            //                    OnLog($"即将查询{group.groupname}是否加过");
            //                    var addGroup = DbUtil.Db.Queryable<AddGroup>()
            //                        .Where(it => it.GroupCode == group.groupqrcodevalue)
            //                        .Count();
            //                    //如果没有添加过群
            //                    if (addGroup == 0) {
            //                        var random = new Random();
            //                        var waitTime = random.Next(30,60);
            //                        //添加群
            //                        OnLog($"等待{waitTime}秒");
            //                        await Task.Delay(waitTime * 1000);
            //                        foreach (var user in StaticData.users) {
            //                            if (user.IsLogin == false) {
            //                                continue;
            //                            }
            //                            //查询当前人最近加群的时间,如果大于1小时就可以添加群
            //                            var userAddGroup = DbUtil.Db.Queryable<AddGroup>()
            //                                .Where(it => it.AddUsername == user.Username).OrderBy(it => it.AddTime,OrderByType.Desc)
            //                                .First();

            //                            if (userAddGroup == null || (DateTime.Now - userAddGroup.AddTime).TotalHours > 1) {
            //                                OnLog($"即将使用{user.OriginalId}加群");
            //                                var t = await GetA8Key(user.OriginalId,group.groupqrcodevalue,4);
            //                                if (t == null) {
            //                                    OnLog($"添加群失败,原因:获取A8Key失败");
            //                                    continue;
            //                                }
            //                                //如果t.BaseResponse.Ret不等于0,则添加群失败
            //                                if (t.BaseResponse.Ret != 0) {
            //                                    OnLog($"添加群失败,原因:{t.BaseResponse.ErrMsg.String_t}");
            //                                    continue;
            //                                }
            //                                OnLog($"加群的url为{t.FullUrl}");
            //                                var html = HttpUtil.Get(t.FullUrl);
            //                                if (html.IndexOf("群聊人数超过200人，只可通过群成员邀请进群") != -1) {
            //                                    var addg1 = new AddGroup() {
            //                                        GroupA8keyUrl = t.FullUrl,
            //                                        AddUsername = "人数超过200",
            //                                        GroupCode = group.groupqrcodevalue,
            //                                        GroupName = group.groupname,
            //                                        AddTime = DateTime.Now,
            //                                        IsAdd = false,
            //                                        AddGroupText = html
            //                                    };
            //                                    DbUtil.Db.Insertable(addg1).ExecuteCommand();
            //                                    addGroups.Add(addg1);
            //                                    return;
            //                                }

            //                                //post请求t.FullUrl的值
            //                                var t1 = HttpUtil.Post(t.FullUrl);
            //                                OnLog("第一次返回" + t1);
            //                                await Task.Delay(5 * 1000);
            //                                if (string.IsNullOrEmpty(t1)) {
            //                                    continue;
            //                                }
            //                                var t2 = HttpUtil.Post(t.FullUrl);
            //                                OnLog("第二次返回" + t2);
            //                                if (t2.IndexOf("weixin://jump/mainframe/") != -1) {
            //                                    var headIndex = t2.IndexOf("weixin://jump/mainframe/");
            //                                    if (headIndex == -1) { OnLog("添加群聊失败,原因:无返回群号"); continue; }
            //                                    var head = t2[(headIndex + 24)..];
            //                                    var breadIndex = head.IndexOf("\"");
            //                                    var groupUsername = head[..breadIndex];
            //                                    //添加记录
            //                                    var addg = new AddGroup() {
            //                                        GroupA8keyUrl = t.FullUrl,
            //                                        AddUsername = user.Username,
            //                                        GroupCode = group.groupqrcodevalue,
            //                                        GroupName = group.groupname,
            //                                        AddTime = DateTime.Now,
            //                                        AgreeTime = DateTime.Now,
            //                                        IsAdd = true,
            //                                        GroupUsername = groupUsername,
            //                                        AddGroupText = html
            //                                    };
            //                                    DbUtil.Db.Insertable(addg).ExecuteCommand();
            //                                    addGroups.Add(addg);
            //                                    return;
            //                                } 
            //                            }
            //                        }
            //                        Thread.Sleep(1000);
            //                        continue;
            //                    }
            //                }
            //            } catch (Exception ex) {
            //                OnLog(ex.ToString());
            //            } finally {
            //                try {
            //                    timer.Change(1000,-1);
            //                } catch (global::System.Exception ex) {
            //                    OnLog($"定时器已经释放{ex}");
            //                }
            //            }
        }

        public override void Terminate()
        {
            timer.Dispose();
            timer1.Dispose();
            base.Terminate();
        }
    }

    public class GroupAdd
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Remark { get; set; }
        public string GroupUsername { get; set; }
        public string GroupName { get; set; }
        public int GroupMemberCount { get; set; }
        public string InviteLink { get; set; }
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
        public static async void Post<T>(string url,T obj)
        {
            var con = await http.PostAsJsonAsync(url,obj);
            var t = await con.Content.ReadAsStringAsync();
            Console.WriteLine(t);
        }
    }

    public class Group
    {
        public string groupqrcode { get; set; }
        public string groupname { get; set; }
        public string groupqrcodevalue { get; set; }
    }

}
