using Plugin;
using SqlSugar;

namespace MG.TimerAddGroup.DbUtils
{
    public static class PgSqlDbUtil
    {
        public static SqlSugarScope Db = new(new ConnectionConfig() {
            ConnectionString = "User ID=postgres;Password=mg@123;Host=101.37.116.158;Port=5432;Database=yfd_test;",//连接符字串
            DbType = DbType.PostgreSQL,//数据库类型
            InitKeyType = InitKeyType.Attribute,
            IsAutoCloseConnection = true //不设成true要手动close
        },
           db => {
           db.Aop.OnLogExecuting = (sql,pars) => {
               //Eve.OnLog(null!,sql);
           };
               db.Aop.DataExecuting = (oldValue,entityInfo) => {
                   if (entityInfo.OperationType == DataFilterType.InsertByObject) {
                       if (entityInfo.PropertyName == "UpdateTime" || entityInfo.PropertyName == "CtrateTime") {

                       }
                       entityInfo.SetValue(DateTime.Now);//修改CreateTime字段
                      
                   }
                   if (entityInfo.PropertyName == "UpdateTime" && entityInfo.OperationType == DataFilterType.UpdateByObject) {
                       entityInfo.SetValue(DateTime.Now);//修改UpdateTime字段
                   }

               };

          });
    }
}