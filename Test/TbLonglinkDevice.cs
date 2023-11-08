
using SqlSugar;
using SuperWx;
using System.Security.Principal;
/// <summary>
/// 
/// </summary>

/// <summary>
/// 设备信息
///</summary>
[SugarTable("tblonglink_device")]
public class TbLonglinkDevice
{
    /// <summary>
    /// 自增主键 
    ///</summary>
    [SugarColumn(ColumnName = "id",IsPrimaryKey = true,IsIdentity = true)]
    public long Id { get; set; }
    /// <summary>
    /// 设备Id 
    ///</summary>
    [SugarColumn(ColumnName = "device_id")]
    public string OrId { get; set; }
    /// <summary>
    /// 设备Json信息 
    ///</summary>
    [SugarColumn(ColumnName = "device_json",ColumnDataType = "json",IsJson = true)]
    public WXUserLogin? DeviceJson { get; set; }
    /// <summary>
    /// 长链ip
    ///</summary>
    [SugarColumn(ColumnName = "ip")]
    public string Ip { get; set; }
    /// <summary>
    /// 长链外网ip
    ///</summary>
    [SugarColumn(ColumnName = "out_ip")]
    public string OutIp { get; set; }
    /// <summary>
    /// 长链端口
    ///</summary>
    [SugarColumn(ColumnName = "port")]
    public int Port { get; set; }

    /// <summary>
    /// 创建时间 
    ///</summary>
    [SugarColumn(ColumnName = "create_time")]
    public DateTime? CreateTime { get; set; }
    /// <summary>
    /// 更新时间 
    ///</summary>
    [SugarColumn(ColumnName = "update_time")]
    public DateTime? UpdateTime { get; set; }
    /// <summary>
    /// 备注 
    ///</summary>
    [SugarColumn(ColumnName = "remark",IsNullable = true,ColumnDataType = "text")]
    public string? Remark { get; set; }
}