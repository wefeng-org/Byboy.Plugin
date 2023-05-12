using SqlSugar;

namespace MG.TimerAddGroup.DbUtils
{
    /// <summary>
    /// 添加群记录
    /// </summary>
    [SugarTable("AddGroup")]
    public class AddGroup
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 群Code
        /// </summary>
        [SugarColumn(Length = 1000)]
        public string GroupCode { get; set; }
        
        /// <summary>
        /// 群号
        /// </summary>
        [SugarColumn(Length = 50,IsNullable = true)]
        public string? GroupUsername { get; set; }
        /// <summary>
        /// 群名称
        /// </summary>
        [SugarColumn(Length = 50)]
        public string GroupName { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [SugarColumn(Length = 50)]
        public string AddUsername { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 是否已添加
        /// </summary>
        public bool IsAdd { get; set; } =false;
        /// <summary>
        /// 同意时间-可空
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? AgreeTime { get; set; }
        /// <summary>
        /// 加群返回的内容-text类型
        /// </summary>
        [SugarColumn(ColumnDataType ="text" ,IsNullable = true)]
        public string? AddGroupText { get; set; }
        [SugarColumn(ColumnDataType = "text",IsNullable = true)]
        public string? GroupA8keyUrl { get; set; }


    }
    /// <summary>
    /// 加群之后的操作
    /// </summary>
    [SugarTable("AddGroupAfter")]
    public class AddGroupAfter
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 群号
        /// </summary>
        [SugarColumn(Length = 50)]
        public string GroupUsername { get; set; }
        /// <summary>
        /// 是否已设置不提醒
        /// </summary>
        public bool IsNoRemind { get; set; } = false;
        /// <summary>
        /// 是否添加到通信录
        /// </summary>
        public bool IsCont { get; set; }
    }
}
