using Plugin;
using Plugin.DbEntitys;

namespace Byboy.SignPlugin.DbUtils
{
    public class SignConfig : BaseConfig
    {
        /// <summary>
        /// 只能管理员修改积分等数据
        /// </summary>
        public bool OnlyAdminEdit { get; set; }

        public override string ConfigName => "Byboy.SignPlugin";

        public SignConfig()
        {
            this.OnlyAdminEdit = false;
        }
    }
}
