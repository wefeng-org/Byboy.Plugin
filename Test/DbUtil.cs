using SqlSugar;

namespace MG.TimerAddGroup.DbUtils
{
    public static class DbUtil
    {
        public static SqlSugarScope Db = new(new ConnectionConfig() {
            ConnectionString = "Data Source=D:\\workspase\\wxssc\\WeFeng.Console\\bin\\Debug\\net6.0\\Db\\MG.TimerAddGroup.db;",//连接符字串
            DbType = DbType.Sqlite,//数据库类型
            InitKeyType = InitKeyType.Attribute,
            IsAutoCloseConnection = true //不设成true要手动close
        },
           db => {
           db.Aop.OnLogExecuting = (sql,pars) => {
               //Eve.OnLog(null!,sql);
           };

          });
    }
}