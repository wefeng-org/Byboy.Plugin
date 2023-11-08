using SqlSugar;

namespace Byboy.CocopilotGroup.DbUtils
{
    public class TBCar
    {
        [SugarColumn(IsPrimaryKey = true,IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 车队名称
        /// </summary>
        public string CatName { get; set; }

        /// <summary>
        /// 车队id
        /// </summary>
        public string CatId { get; set; }

        /// <summary>
        /// 车队成员
        /// </summary>
        [SugarColumn(IsJson = true)]
        public List<CatUuid> Uuid { get; set; }

        /// <summary>
        /// 是否开车
        /// </summary>
        public bool IsOpen { get; set; }
    }
    public class CatUuid
    {
        public string Username { get; set; }
        public string Uuid { get; set; }
    }
}
