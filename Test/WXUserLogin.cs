using System;

namespace SuperWx
{
    public class WXUserLogin
    {
        /// <summary>
        /// 原始信息id
        /// </summary>
        public string OriginalId { get; set; } = "";




        /// <summary>
        /// 是否已登录
        /// </summary>
        public bool IsLogin { get; set; } = false;

        /// <summary>
        /// 用户密码-可能是服务器返回的一堆东西
        /// </summary>
        public string Password { get; set; }
        public bool DbIsLogin { get; set; }

        /// <summary>
        /// 链接腾讯服务器的ip
        /// </summary>
        public string LinkServerIp { get; set; }

        /// <summary>
        /// 上一次更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; } = DateTime.Now;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// cookie
        /// </summary>
        public byte[] Cookie { get; set; } = new byte[0];

        public byte[] SyncMsgKey { get; set; }
        public byte[] SyncSnsKey { get; set; }
        /// <summary>
        /// 通信用到的sessionkey
        /// </summary>
        public byte[] SessionKey { get; set; }

        /// <summary>
        /// 设备token
        /// </summary>
        public string DeviceToken { get; set; } = "";

        /// <summary>
        /// rsa版本
        /// </summary>
        public int RsaVersion { get; set; } = 10008;




        /// <summary>
        /// devicetocken下次获取时间
        /// </summary>
        public DateTime DeviceTockenDate { get; set; }

        /// <summary>
        /// 用户的数字id
        /// </summary>
        public uint Uin { get; set; }

        /// <summary>
        /// 用户的wxid
        /// </summary>
        public string Username { get; set; } = "";
        public string NickName { get; set; }
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 二次登陆用的key
        /// </summary>
        public byte[] AutoAuthKey { get; set; } = new byte[0];

        /// <summary>
        /// 短链用到的pskAccess
        /// </summary>
        public byte[] PskKey { get; set; } = new byte[0];

        /// <summary>
        /// 短连接ip
        /// </summary>
        public string[] ShortIP { get; set; } = new string[0];

        /// <summary>
        /// 短连接host
        /// </summary>
        public string[] ShortHost { get; set; } = new string[0];

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; } = "IPAD";

        /// <summary>
        /// 长连接域名
        /// </summary>
        public string[] LongHost { get; set; } = new string[0];
        /// <summary>
        /// 长连接ip
        /// </summary>
        public string[] LongIP { get; set; } = new string[0];

        /// <summary>
        /// 程序安装的时间偏移
        /// </summary>
        public int InstallTimeOffset { get; set; } = 10001;

        /// <summary>
        /// 版本号偏移值
        /// </summary>
        public int VersionBaseband { get; set; }
        /// <summary>
        /// pid
        /// </summary>
        public string Pid { get; set; } = "";


        /// <summary>
        /// soft设置
        /// </summary>
        public byte[] SoftConfig { get; set; } = new byte[0];
        /// <summary>
        /// soft数据
        /// </summary>
        public byte[] SoftData { get; set; } = new byte[0];




        /// <summary>
        /// 共享秘钥   登录成功后赋值
        /// </summary>
        public byte[] ShareKey { get; set; }



        /// <summary>
        /// 客户端sessionkey-13算法专用
        /// </summary>
        public byte[] ClientSession { get; set; }

        /// <summary>
        /// 服务器sessionkey-13算法专用
        /// </summary>
        public byte[] ServiceSession { get; set; }





        private string _DeviceId { get; set; } = "";
        /// <summary>
        /// 微信用到的设备id
        /// </summary>
        public string DeviceId {
            get;
            set;
        } = "";

        private string _Imei { get; set; }
        /// <summary>
        /// IMEI
        /// </summary>
        public string Imei {
            get;
        } = "";


        private uint _Version;

        public uint Version {
            get {
                if (_Version == 0) {
                    _Version = 0;//TODO 这里合理吗?
                }
                return _Version;
            }
            set {
                _Version = value;
            }
        }

        public byte[] FavSyncKey { get; set; } = new byte[0];

        /// <summary>
        /// 设备唯一码
        /// </summary>
        public byte[] BootHash { get; set; } = new byte[0];
        /// <summary>
        /// 微信号
        /// </summary>
        public string Alias { get; set; }

        public DateTime LoginTime { get; set; } = DateTime.Now;
    }
}