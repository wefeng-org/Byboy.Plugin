using SqlSugar;

namespace Out.YunFaDan.Model
{
    /// <summary>
    /// 基础字段
    /// </summary>
    public class TbBaseField
    {
        /// <summary>
        /// 自增主键 
        ///</summary>
        [SugarColumn(ColumnName = "id",IsPrimaryKey = true,IsIdentity = true)]
        public long Id { get; set; }
        /// <summary>
        /// 创建时间 
        ///</summary>
        [SugarColumn(ColumnName = "create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间 
        ///</summary>
        [SugarColumn(ColumnName = "update_time")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 备注 
        ///</summary>
        [SugarColumn(ColumnName = "remark",IsNullable = true)]
        public string? Remark { get; set; }
    }
}