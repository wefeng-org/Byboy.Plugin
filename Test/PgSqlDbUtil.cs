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
                   Console.WriteLine(sql);
               };
               db.Aop.DataExecuting = (oldValue,entityInfo) => {
                   if (entityInfo.OperationType == DataFilterType.InsertByObject || entityInfo.OperationType == DataFilterType.UpdateByObject) {
                       if (entityInfo.PropertyName == "UpdateTime" || entityInfo.PropertyName == "CreateTime") {
                           entityInfo.SetValue(DateTime.Now);
                       }
                   }
               };

           });

        public static SqlSugarScope LonglinkDb = new(new ConnectionConfig() {
            ConnectionString = "User ID=postgres;Password=mg@123;Host=101.37.116.158;Port=5432;Database=wxlonglink;",//连接符字串
            DbType = DbType.PostgreSQL,//数据库类型
            InitKeyType = InitKeyType.Attribute,
            IsAutoCloseConnection = true //不设成true要手动close
        },
           db => {
               db.Aop.OnLogExecuting = (sql,pars) => {
                   Console.WriteLine(sql);
               };
               db.Aop.DataExecuting = (oldValue,entityInfo) => {
                   if (entityInfo.OperationType == DataFilterType.InsertByObject || entityInfo.OperationType == DataFilterType.UpdateByObject) {
                       if (entityInfo.PropertyName == "UpdateTime" || entityInfo.PropertyName == "CreateTime") {
                           entityInfo.SetValue(DateTime.Now);
                       }
                   }
               };

           });
    }
}