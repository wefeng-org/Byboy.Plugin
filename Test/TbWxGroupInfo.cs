using SqlSugar;

namespace Out.YunFaDan.Model
{
    /// <summary>
    /// 微信群信息表
    /// </summary>
    [SugarTable("tbwxgroupinfo")]
    public class TbWxGroupInfo : TbBaseField
    {
        /// <summary>
        /// 微信id
        /// </summary>
        [SugarColumn(ColumnName = "username",IsNullable = true)]
        public string? Username { get; set; }
        /// <summary>
        /// 群id
        /// </summary>
        [SugarColumn(ColumnName = "group_username",IsNullable = true)]
        public string? GroupUsername { get; set; }
        /// <summary>
        /// 群名称
        /// </summary>
        [SugarColumn(ColumnName = "group_name",IsNullable = true)]
        public string? GroupName { get; set; }
        /// <summary>
        /// 群头像
        /// </summary>
        [SugarColumn(ColumnName = "group_headimg",IsNullable = true)]
        public string? GroupHeadimg { get; set; }
        /// <summary>
        /// 群人数
        /// </summary>
        [SugarColumn(ColumnName = "group_num",IsNullable = true)]
        public int? GroupNum { get; set; }
        /// <summary>
        /// 加群时间
        /// </summary>
        [SugarColumn(ColumnName = "add_group_time",IsNullable = true)]
        public DateTime? AddGroupTime { get; set; }
        /// <summary>
        /// 群是否正常
        /// </summary>
        [SugarColumn(ColumnName = "is_normal",IsNullable = true)]
        public bool? IsNormal { get; set; }
        
    }
}
