namespace SendSnsTools
{
    public class AddSns
    {
        public int userId { get; set; }
        public int sendType { get; set; }
        public int dealStatus { get; set; }
        public string itemId { get; set; }
        public int itemType { get; set; }
        public string goodsSign { get; set; }
        public string itemUrl { get; set; }
        public string couponUrl { get; set; }
        public string itemTitle { get; set; }
        public string itemImage { get; set; }
        public string couponId { get; set; }
        public string zsDuoId { get; set; }
        public string content { get; set; }
        public string[] commentList { get; set; }
        public string[] picList { get; set; }
        public DateTime sendTime { get; set; }
        public string remark { get; set; }
    }


    public class Response
    {
        public int code { get; set; }
        public string msg { get; set; }
    }


}
