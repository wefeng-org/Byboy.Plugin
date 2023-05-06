using MG.WeCode.Entitys;
using Plugin;
using SuperWx.algorithm.AB;
using System.Xml;

namespace MG.AppConvent
{
    public class Main : IPlugin
    {
        public override string Author => "美逛科技";
        public override string Description => "他会转换一个app消息";
        public override string Name => "App消息转换";
        public override string Version => "v1.0";
        public TaskData TaskData { get; set; }

        public override void Initialize()
        {
            ReceiveGroupMessage += Main_ReceiveGroupMessage;
        }


        private async Task Main_ReceiveGroupMessage(SuperWx.WXUserLogin sender,Plugin.EveEntitys.ReceiveGroupMessageArgs e)
        {
            if (e.GroupUsername == "34702671242@chatroom") {

                if (e.Content.MsgType == Plugin.EveEntitys.MessageType.APPMSG) {
                    //await SendAppMsg(sender.OriginalId,e.Sender.Username,"",1,33);
                    TaskData = new TaskData {
                        AppMsg = e.Content.Xml,
                        Username = e.Username
                    };
                    await SendTextMsg(sender.OriginalId,"34702671242@chatroom",$"@{e.Sender.Nickname},请输入标题",e.Username);
                    e.Cancel = true;
                    return;
                } else if (e.Content.MsgType == Plugin.EveEntitys.MessageType.NORMALIM) {
                    if (TaskData == null) {
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(TaskData.Title)) {
                        TaskData.Title = e.Content.Text;
                        await SendTextMsg(sender.OriginalId,"34702671242@chatroom",$"@{e.Sender.Nickname},请输入图片",e.Username);
                        e.Cancel = true;
                        return;
                    } else {
                        TaskData.Icon = e.Content.Text;
                        await SendTextMsg(sender.OriginalId,"34702671242@chatroom",$"@{e.Sender.Nickname},正在整合,请稍等...",e.Username);

                        var xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(TaskData.AppMsg);
                        //var t = Xml(xmlDoc);
                        UpdateXmlNodeValue(xmlDoc,KeyToXPath("msg.appmsg.title"),TaskData.Title);
                        UpdateXmlNodeValue(xmlDoc,KeyToXPath("msg.appmsg.appattach.cdnthumburl"),TaskData.Image.FileId);
                        UpdateXmlNodeValue(xmlDoc,KeyToXPath("msg.appmsg.appattach.cdnthumbmd5"),TaskData.Image.Md5);
                        UpdateXmlNodeValue(xmlDoc,KeyToXPath("msg.appmsg.appattach.cdnthumbaeskey"),TaskData.Image.AesKey);
                        UpdateXmlNodeValue(xmlDoc,KeyToXPath("msg.appmsg.appattach.aeskey"),TaskData.Image.AesKey);
                        UpdateXmlNodeValue(xmlDoc,KeyToXPath("msg.appmsg.sourcedisplayname"),TaskData.Icon);
                        var str = xmlDoc.InnerXml;
                        OnLog(str);
                        await Task.Delay(5 * 1000);
                        await SendAppMsg(sender.OriginalId,"34702671242@chatroom",str,1,33);
                        await Task.Delay(1 * 1000);
                        await SendTextMsg(sender.OriginalId,"34702671242@chatroom",$"@{e.Sender.Nickname},整合完成,需要开始请发小程序",e.Username);
                        TaskData = null!;
                        e.Cancel = true;
                        return;
                    }
                } else if (e.Content.MsgType == Plugin.EveEntitys.MessageType.IMAGE) {
                    if (TaskData == null || TaskData.Image != null) {
                        return;
                    }
                    var xmldoc = e.Content.Xml.GetXmlElement("img");
                    if (xmldoc != null) {
                        var t = await DownloadImage(sender.OriginalId,xmldoc.GetAttribute("cdnthumburl"),xmldoc.GetAttribute("aeskey"));
                        UploadAppImageResp UploadAppImage = null!;
                        if (t != null) {
                            UploadAppImage = await CdnUpdateImageOriginImage(sender.OriginalId,"34702671242@chatroom",t,520,416);
                        }
                        if (UploadAppImage == null) {
                            await SendTextMsg(sender.OriginalId,"34702671242@chatroom",$"@{e.Sender.Nickname},图片上传失败",e.Username);

                            return;
                        }

                        Img img = new() {
                            AesKey = UploadAppImage.DataAes,
                            FileId = UploadAppImage.DataUrl,
                            Md5 = xmldoc.GetAttribute("md5"),
                        };
                        TaskData.Image = img;


                        e.Cancel = true;
                        return;
                    }
                }
            }
        }
        public static string KeyToXPath(string key)
        {
            return "/" + key.Replace(".","/");
        }
        private static Dictionary<string,string> Xml(XmlDocument xmlDoc)
        {

            var resultList = new Dictionary<string,string>();
            TraverseNodes(xmlDoc.DocumentElement,resultList,xmlDoc.DocumentElement.Name);
            return resultList;
        }

        public static void TraverseNodes(XmlNode node,Dictionary<string,string> dict,string path = "")
        {
            if (node.HasChildNodes) {
                foreach (XmlNode childNode in node.ChildNodes) {
                    if (childNode.Name == "#text" || childNode.Name == "#cdata-section") {
                        TraverseNodes(childNode,dict,$"{path}");
                    } else {
                        TraverseNodes(childNode,dict,$"{path}.{childNode.Name}");
                    }
                }
            } else {
                dict.Add(path,node.InnerText);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="nodePath"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private static string UpdateXmlNodeValue(XmlDocument xmlDoc,string nodePath,string newValue)
        {
            XmlNode targetNode = xmlDoc.SelectSingleNode(nodePath);
            if (targetNode != null) {
                targetNode.InnerText = newValue;
            }

            return xmlDoc.OuterXml;
        }
    }
    public class TaskData
    {
        /// <summary>
        /// 发送着
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// app消息
        /// </summary>
        public string AppMsg { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public Img Image { get; set; }
        /// <summary>
        /// 小图片
        /// </summary>
        public string Icon { get; set; }
    }

}
