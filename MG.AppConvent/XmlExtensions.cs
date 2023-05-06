using System.Xml;

namespace MG.AppConvent
{
    /// <summary>
    /// XML扩展
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// 选择XmlElement
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static XmlElement GetXmlElement(this string xml,string node)
        {
            var document = new XmlDocument { InnerXml = xml };
            if (xml.Contains("<msg>")) {
                return (XmlElement)document.SelectSingleNode($"msg/{node}");
            } else {
                return (XmlElement)document.SelectSingleNode(node);
            }

        }
    }
}
