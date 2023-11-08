
using SqlSugar;

namespace Out.YunFaDan.Model
{
    /// <summary>
    /// 微信群信息
    /// </summary>
    [SugarTable("tbgroup_info")]
    public class TbGroupInfo : TbBaseField
    {
        /// <summary>
        /// 微信群id
        /// </summary>
        [SugarColumn(ColumnName = "group_id", IsNullable = true)]
        public string? GroupId { get; set; }
        /// <summary>
        /// 微信群名称
        /// </summary>
        [SugarColumn(ColumnName = "group_name")]
        public string GroupName { get; set; } = "";
        /// <summary>
        /// 微信群头像
        /// </summary>
        [SugarColumn(ColumnName = "group_head")]
        public string GroupHead { get; set; }= "";
        /// <summary>
        /// 微信群人数
        /// </summary>
        [SugarColumn(ColumnName = "group_count", IsNullable = true)]
        public int? GroupCount { get; set; }
        /// <summary>
        /// 微信群人数更新时间
        /// </summary>
        [SugarColumn(ColumnName = "group_count_time", IsNullable = true)]
        public DateTime? GroupCountTime { get; set; }
        /// <summary>
        /// 微信群人数获取状态
        /// </summary>
        [SugarColumn(ColumnName = "group_count_status", IsNullable = true)]
        public int? GroupCountStatus { get; set; }
        /// <summary>
        /// 微信群人数调用时间
        /// </summary>
        [SugarColumn(ColumnName = "group_count_call_time", IsNullable = true)]
        public DateTime? GroupCountCallTime { get; set; }
        /// <summary>
        /// 微信群人数调用成功时间
        /// </summary>
        [SugarColumn(ColumnName = "group_count_success_time", IsNullable = true)]
        public DateTime? GroupCountSuccessTime { get; set; }
        /// <summary>
        /// 微信原始二维码
        /// </summary>
        [SugarColumn(ColumnName = "group_native_qrcode", IsNullable = true)]
        public string? GroupNativeQrcode { get; set; }
        /// <summary>
        /// 微信原始二维码更新时间
        /// </summary>
        [SugarColumn(ColumnName = "group_native_qrcode_time", IsNullable = true)]
        public DateTime? GroupNativeQrcodeTime { get; set; }
        /// <summary>
        /// 微信群二维码
        /// </summary>
        [SugarColumn(ColumnName = "group_qrcode", IsNullable = true)]
        public string? GroupQrcode { get; set; }
        /// <summary>
        /// 微信群二维码更新时间
        /// </summary>
        [SugarColumn(ColumnName = "group_qrcode_time", IsNullable = true)]
        public DateTime? GroupQrcodeTime { get; set; }
        /// <summary>
        /// 微信群二维码状态
        /// </summary>
        [SugarColumn(ColumnName = "group_qrcode_status", IsNullable = true)]
        public int? GroupQrcodeStatus { get; set; }
        /// <summary>
        /// 微信群加群结果
        /// </summary>
        [SugarColumn(ColumnName = "group_add_result", IsNullable = true)]
        public string? GroupAddResult { get; set; }
        /// <summary>
        /// 微信群加群状态
        /// </summary>
        [SugarColumn(ColumnName = "group_add_status", IsNullable = true)]
        public int? GroupAddStatus { get; set; }
        /// <summary>
        /// 微信群加群的微信Username
        /// </summary>
        [SugarColumn(ColumnName = "group_add_username", IsNullable = true)]
        public string? GroupAddUsername { get; set; }
        /// <summary>
        /// 就
        /// </summary>
        [SugarColumn(ColumnName = "old_username",IsNullable = true)]

        public string OldUsername { get; set; } = null!;
    }
}
