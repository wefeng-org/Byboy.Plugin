using SqlSugar;
using System.Security.Principal;

namespace Byboy.CocopilotGroup.DbUtils
{
    public class TBGroupDetail
    {
        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        ///  群名
        /// </summary>
        public string GroupName { get; set; } = "";
        /// <summary>
        /// 群号
        /// </summary>
        public string GroupUsername { get; set; } = "";
        /// <summary>
        /// 群人数
        /// </summary>
        public int GroupCount { get; set; }
    }
}
