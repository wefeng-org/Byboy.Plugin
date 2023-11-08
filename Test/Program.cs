// See https://aka.ms/new-console-template for more information
using MG.TimerAddGroup.DbUtils;
using Newtonsoft.Json;
using SuperWx;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("请输入dat所在路径");


//var path = Console.ReadLine();
//var files = Directory.GetFiles(path,"*.dat");
//foreach (var item in files) {
//    var t = File.ReadAllBytesAsync(item).Result;
//    string json = AesDecrypt(t);
//    //将json转成userlogin对象
//    var user = JsonConvert.DeserializeObject<WXUserLogin>(json);
//    var tbLonglinkDevice = new TbLonglinkDevice {
//        DeviceJson = user,
//        OrId = user.OriginalId,
//        Ip = "172.20.229.189",
//        OutIp = "101.37.116.158",
//        Port = 7100,
//    };
//    PgSqlDbUtil.LonglinkDb.Insertable(tbLonglinkDevice).ExecuteCommand();
//}



//var list = PgSqlDbUtil.Db.Queryable<TbGroupInfo>().Where(t => t.OldUsername != null && (t.GroupCountStatus != -2 ) && (t.GroupCountCallTime!.Value.AddDays(1) < DateTime.Now)).OrderBy(t => t.GroupCountCallTime).ToList();
//var t = list.GroupBy(t => t.OldUsername,t => t,(k,v) => new { Username = k,Data = v }).ToList();


//var addGroupList = DbUtil.Db.Queryable<AddGroup>().Where(t => !string.IsNullOrEmpty(t.GroupUsername)).ToList();
//var t1 = addGroupList.Select(t => new TbGroupInfo() { GroupId = t.GroupUsername,GroupName = t.GroupName,CreateTime = DateTime.Now,UpdateTime = DateTime.Now,OldUsername = t.AddUsername}).ToList();
//PgSqlDbUtil.Db.Storageable(t1).WhereColumns(t => t.GroupName).ExecuteCommand();

//var t2 = PgSqlDbUtil.Db.Queryable<TbWxGroupInfo>().Where(t => t.IsNormal == true).ToList();
//var t3 = t2.Select(t => new TbGroupInfo() { GroupId = t.GroupUsername,GroupName = t.GroupName,GroupCount = t.GroupNum,GroupAddStatus = 0,GroupCountCallTime = DateTime.Now,GroupCountStatus = 0,GroupCountTime = DateTime.Now,GroupCountSuccessTime = DateTime.Now,GroupAddResult = "0",GroupAddUsername = t.Username }).ToList();
//PgSqlDbUtil.Db.Storageable(t3).WhereColumns(t => t.GroupName).ExecuteCommand();
static string AesDecrypt(byte[] t)
{
    var aes = Aes.Create();
    var key = new byte[] { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16 };
    aes.Key = key;
    var json = Encoding.UTF8.GetString(aes.DecryptCbc(t,key));
    return json;
}