using SqlSugar;

namespace Byboy.SignPlugin.DbUtils
{
    public class DbUtil
    {
         public static SqlSugarScope Db = new(new ConnectionConfig() {
            ConnectionString = "Data Source=Db/Byboy.SignPlugin.db;",//连接符字串
            DbType = DbType.Sqlite,//数据库类型
            InitKeyType = InitKeyType.Attribute,
            IsAutoCloseConnection = true //不设成true要手动close
        });
    }
}