using SqlSugar;
using System;

namespace Byboy.SignPlugin.DbUtils
{
    public static class DbUtil
    {
         public static SqlSugarScope Db = new(new ConnectionConfig() {
            ConnectionString = "Data Source=Db/Byboy.SignPlugin.db;",//连接符字串
            DbType = DbType.Sqlite,//数据库类型
            InitKeyType = InitKeyType.Attribute,
            IsAutoCloseConnection = true //不设成true要手动close
        });
        /// <summary>
        /// 清理发言信息
        /// </summary>
        /// <param name="ExternalId"></param>
        /// <param name="LastSentTime"></param>
        /// <returns></returns>
        static List<ClusterSign> signs = new();
        /// <summary>
        /// 配置缓存
        /// </summary>
        public static List<ClusterConfig> configs = new List<ClusterConfig>();

        internal static int DeleteSignByLastSentTime(string groupUsername,DateTime dateTime)
        {
            try {
               
                return Db.Deleteable<ClusterSign>().Where(t => t.GroupUsername == groupUsername && t.LastSentTime < dateTime).ExecuteCommand();
            } catch (Exception) {
            }
            return 0;
        }
        /// <summary>
        /// 清理签到信息
        /// </summary>
        /// <param name="ExternalId"></param>
        /// <param name="LastSignTime"></param>
        /// <returns></returns>
        internal static int DeleteSignByLastSignTime(string groupUsername,DateTime dateTime)
        {
            try {
                return Db.Deleteable<ClusterSign>().Where(t => t.GroupUsername == groupUsername && t.LastSignTime < dateTime).ExecuteCommand();
            } catch (Exception) {
            }
            return 0;
        }
        /// <summary>
        /// 从缓存取出配置
        /// </summary>
        /// <param name="ExternalId"></param>
        /// <returns></returns>
        internal static ClusterConfig GetClusterConfig(this string chatroom)
        {
            try {
                var cc = configs.FirstOrDefault(t => t.Id == 2);
                if (cc != null && cc.ConfigObj.Status)
                    return cc;
                cc = configs.FirstOrDefault(t => t.GroupUsername == chatroom);
                if (cc == null) {
                    var cc0 = configs.FirstOrDefault(t => t.Id == 1);
                    cc = cc0;
                    cc.Id = 0;
                    cc.GroupUsername = chatroom;
                    cc.SaveClusterConfig();
                    configs.Add(cc);
                }
                return cc;
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
            return null!;
           
        }
        /// <summary>
        /// 带有缓存机制的
        /// </summary>
        /// <param name="groupUsername"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        internal static ClusterSign GetClusterSign(string groupUsername,string username)
        {
            try {
                var cs = signs.FirstOrDefault(t => t.GroupUsername == groupUsername && t.Username == username);
                if (cs != null)
                    return cs;
                var sign = Db.Queryable<ClusterSign>().Where(t => t.GroupUsername == groupUsername && t.Username == username).First();
                if (sign != null) {
                    signs.Add(sign);
                    var dt = DateTime.Now.AddHours(-1);
                    signs.RemoveAll(t => t.LastSentTime < dt);
                }
                return sign!;
            } catch (Exception ex) {
            }
            return null!;
        }

        /// <summary>
        /// 今日签到
        /// </summary>
        /// <param name="ExternalId"></param>
        /// <param name="Top"></param>
        /// <param name="Time"></param>
        /// <returns></returns>
        internal static List<ClusterSign>    GetSignByLastSignTime(string groupUsername,int top,DateTime today)
        {
            try {
                return Db.Queryable<ClusterSign>().Where(t => t.SignCount > 0 && t.GroupUsername == groupUsername && t.LastSignTime > today.Date).Take(top).OrderBy(t => t.LastSignTime,OrderByType.Desc).ToList();
            } catch (Exception) {
            }
            return null!;
        }
        /// <summary>
        /// 发言月榜
        /// </summary>
        /// <param name="ExternalId"></param>
        /// <param name="Top"></param>
        /// <param name="today"></param>
        /// <returns></returns>
        internal static List<ClusterSign> GetSignByMonthSentCount(string groupUsername,int top,DateTime dateTime)
        {
            try {
                return Db.Queryable<ClusterSign>().Where(t => t.SignCount > 0 && t.GroupUsername == groupUsername && t.LastSentTime > dateTime.Date).Take(top).OrderBy(t => t.MonthSentCount,OrderByType.Desc).ToList();
            } catch (Exception) {
            }
            return null!;
        }
        /// <summary>
        /// 签到月榜
        /// </summary>
        /// <param name="ExternalId"></param>
        /// <param name="Top"></param>
        /// <param name="Time"></param>
        /// <returns></returns>
        internal static List<ClusterSign> GetSignByMonthSignCount(string groupUsername,int top,DateTime dateTime)
        {
            try {
                return Db.Queryable<ClusterSign>().Where(t => t.SignCount > 0 && t.GroupUsername == groupUsername && t.LastSentTime > dateTime.Date).Take(top).OrderBy(t => t.MonthSignCount,OrderByType.Desc).ToList();
            } catch (Exception) {
            }
            return null!;
        }
        /// <summary>
        /// 发言总榜
        /// </summary>
        /// <param name="ExternalId"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        internal static List<ClusterSign> GetSignBySentCount(string groupUsername,int top)
        {
            try {
                return Db.Queryable<ClusterSign>().Where(t => t.GroupUsername == groupUsername).Take(top).OrderBy(t => t.SentCount,OrderByType.Desc).ToList();
            } catch (Exception) {
            }
            return null!;
        }
        /// <summary>
        /// 签到总榜
        /// </summary>
        /// <param name="ExternalId"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        internal static List<ClusterSign> GetSignBySignCount(string groupUsername,int top)
        {
            try {
                return Db.Queryable<ClusterSign>().Where(t => t.GroupUsername == groupUsername && t.SignCount > 0).Take(top).OrderBy(t => t.SignCount,OrderByType.Desc).ToList();
            } catch (Exception) {
                
            }
            return null!;
        }
        /// <summary>
        /// 查询指定时间（月、日）以后的签到人数
        /// </summary>
        /// <param name="ExternalId"></param>
        /// <param name="LastSignTime"></param>
        /// <returns></returns>
        internal static int GetSignCount(string groupUsername,DateTime today)
        {
            return Db.Queryable<ClusterSign>().Where(t => t.GroupUsername == groupUsername && t.LastSignTime.HasValue && t.LastSignTime.Value > today).Count();
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="config"></param>
        internal static void SaveClusterConfig(this ClusterConfig config)
        {
            try {
            var r =Db.Storageable(config).ExecuteCommand();
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// 写入数据库
        /// </summary>
        /// <param name="sign"></param>
        internal static void SaveClusterSign(this ClusterSign sign)
        {
            try {
                Db.Storageable(sign).ExecuteCommand();
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }
    }
}