using SqlSugar;

namespace Byboy.SignPlugin.DbUtils
{
    /// <summary>
    /// 签到表
    /// </summary>
    public class Sign
    {
        // 表内共有个字段,分别是
        // id,用户,群,签到时间,连续签到天数
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 群
        /// </summary>
        public string GroupUsername { get; set; }
        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime SignTime { get; set; }
        /// <summary>
        /// 连续签到天数
        /// </summary>
        public int SignDays { get; set; }
    }
}
