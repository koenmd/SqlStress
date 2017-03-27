using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Threading;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Drawing;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Utils
{
    public sealed class Debuglog
    {
        public static List<string> m_logs = new List<string>();
        //private static int iCapacity = 1000;

        public static void Log(string log)
        {
            System.Diagnostics.Debug.WriteLine(log);
            return;

            /*
            if (m_logs.Count >= iCapacity)
            {
                try
                {
                    while (m_logs.Count >= iCapacity)
                    {
                        m_logs.RemoveAt(0);
                    }                    
                }
                catch (System.Exception)
                {
                    //do nothing	
                }                
            }
            m_logs.Add(log);
             * */
        }

        public static void Debug(string log)
        {
            Log(string.Format("[DEBUG-{0}]", Thread.CurrentThread.ManagedThreadId) + log);
        }

        public static void Trace(Exception ex)
        {
            Log(string.Format("[TRACE-{0}]{1}\r\n{2}\r\n{3}", Thread.CurrentThread.ManagedThreadId, ex.Message, ex.Source, ex.StackTrace));
        }
    }

    /// <summary>
    /// Xml序列化与反序列化
    /// </summary>
    public class XmlUtil
    {
        #region 反序列化
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }
        #endregion

        #region 序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }

        #endregion
    }

    public class XmlHelper
    {
        public XmlHelper()
        {
        }
        public enum XmlType
        {
            File,
            String
        };
        /// <summary>
        /// 创建XML文档
        /// </summary>
        /// <param name="name">根节点名称</param>
        /// <param name="type">根节点的一个属性值</param>
        /// <returns></returns>
        /// .net中调用方法：写入文件中,则：
        ///          document = XmlOperate.CreateXmlDocument("sex", "sexy");
        ///          document.Save("c:/bookstore.xml");         
        public static XmlDocument CreateXmlDocument(string name, string type)
        {
            XmlDocument doc = null;
            XmlElement rootEle = null;
            try
            {
                doc = new XmlDocument();
                doc.LoadXml("<" + name + "/>");
                rootEle = doc.DocumentElement;
                rootEle.SetAttribute("type", type);
            }
            catch (Exception er)
            {
                throw er;
            }
            return doc;
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Read(path, "/Node", "")
         * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
         ************************************************/
        public static string Read(string path, string node, string attribute)
        {
            string value = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch { }
            return value;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "Element", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Element", "Attribute", "Value")
         * XmlHelper.Insert(path, "/Node", "", "Attribute", "Value")
         ************************************************/
        public static void Insert(string path, string node, string element, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    xn.AppendChild(xe);
                }
                doc.Save(path);
            }
            catch { }
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Attribute", "Value")
         ************************************************/
        public static void Update(string path, string node, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                doc.Save(path);
            }
            catch { }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Delete(path, "/Node", "")
         * XmlHelper.Delete(path, "/Node", "Attribute")
         ************************************************/
        public static void Delete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
                doc.Save(path);
            }
            catch { }
        }
        /*
        #region 读取XML资源到DataSet中
        /// <summary>
        /// 读取XML资源到DataSet中
        /// </summary>
        /// <param name="source">XML资源，文件为路径，否则为XML字符串</param>
        /// <param name="xmlType">XML资源类型</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string source, XmlType xmlType)
        {
            DataSet ds = new DataSet();
            if (xmlType == XmlType.File)
            {
                ds.ReadXml(source);
            }
            else
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(source);
                XmlNodeReader xnr = new XmlNodeReader(xd);
                ds.ReadXml(xnr);
            }
            return ds;
        }
        #endregion
         * */
        #region 操作xml文件中指定节点的数据
        /// <summary>
        /// 获得xml文件中指定节点的节点数据
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static string GetNodeInfoByNodeName(string path, string nodeName)
        {
            string XmlString = "";
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            System.Xml.XmlElement root = xml.DocumentElement;
            System.Xml.XmlNode node = root.SelectSingleNode("//" + nodeName);
            if (node != null)
            {
                XmlString = node.InnerText;
            }
            return XmlString;
        }
        #endregion
        /*
        #region 获取一个字符串xml文档中的ds
        /// <summary>
        /// 获取一个字符串xml文档中的ds
        /// </summary>
        /// <param name="xml_string">含有xml信息的字符串</param>
        public static void get_XmlValue_ds(string xml_string, ref DataSet ds)
        {
            System.Xml.XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml_string);
            XmlNodeReader xnr = new XmlNodeReader(xd);
            ds.ReadXml(xnr);
            xnr.Close();
            int a = ds.Tables.Count;
        }
        #endregion
        
        #region 读取XML资源到DataTable中
        /// <summary>
        /// 读取XML资源到DataTable中
        /// </summary>
        /// <param name="source">XML资源，文件为路径，否则为XML字符串</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="tableName">表名称</param>
        /// <returns>DataTable</returns>
        public static DataTable GetTable(string source, XmlType xmlType, string tableName)
        {
            DataSet ds = new DataSet();
            if (xmlType == XmlType.File)
            {
                ds.ReadXml(source);
            }
            else
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(source);
                XmlNodeReader xnr = new XmlNodeReader(xd);
                ds.ReadXml(xnr);
            }
            return ds.Tables[tableName];
        }
        #endregion
        #region 读取XML资源中指定的DataTable的指定行指定列的值
        /// <summary>
        /// 读取XML资源中指定的DataTable的指定行指定列的值
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="tableName">表名</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colName">列名</param>
        /// <returns>值，不存在时返回Null</returns>
        public static object GetTableCell(string source, XmlType xmlType, string tableName, int rowIndex, string colName)
        {
            DataSet ds = new DataSet();
            if (xmlType == XmlType.File)
            {
                ds.ReadXml(source);
            }
            else
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(source);
                XmlNodeReader xnr = new XmlNodeReader(xd);
                ds.ReadXml(xnr);
            }
            return ds.Tables[tableName].Rows[rowIndex][colName];
        }
        #endregion
        #region 读取XML资源中指定的DataTable的指定行指定列的值
        /// <summary>
        /// 读取XML资源中指定的DataTable的指定行指定列的值
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="tableName">表名</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列号</param>
        /// <returns>值，不存在时返回Null</returns>
        public static object GetTableCell(string source, XmlType xmlType, string tableName, int rowIndex, int colIndex)
        {
            DataSet ds = new DataSet();
            if (xmlType == XmlType.File)
            {
                ds.ReadXml(source);
            }
            else
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(source);
                XmlNodeReader xnr = new XmlNodeReader(xd);
                ds.ReadXml(xnr);
            }
            return ds.Tables[tableName].Rows[rowIndex][colIndex];
        }
        #endregion
        #region 将DataTable写入XML文件中
        /// <summary>
        /// 将DataTable写入XML文件中
        /// </summary>
        /// <param name="dt">含有数据的DataTable</param>
        /// <param name="filePath">文件路径</param>
        public static void SaveTableToFile(DataTable dt, string filePath)
        {
            DataSet ds = new DataSet("Config");
            ds.Tables.Add(dt.Copy());
            ds.WriteXml(filePath);
        }
        #endregion
        #region 将DataTable以指定的根结点名称写入文件
        /// <summary>
        /// 将DataTable以指定的根结点名称写入文件
        /// </summary>
        /// <param name="dt">含有数据的DataTable</param>
        /// <param name="rootName">根结点名称</param>
        /// <param name="filePath">文件路径</param>
        public static void SaveTableToFile(DataTable dt, string rootName, string filePath)
        {
            DataSet ds = new DataSet(rootName);
            ds.Tables.Add(dt.Copy());
            ds.WriteXml(filePath);
        }
        #endregion
        #region 使用DataSet方式更新XML文件节点
        /// <summary>
        /// 使用DataSet方式更新XML文件节点
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="tableName">表名称</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colName">列名</param>
        /// <param name="content">更新值</param>
        /// <returns>更新是否成功</returns>
        public static bool UpdateTableCell(string filePath, string tableName, int rowIndex, string colName, string content)
        {
            bool flag = false;
            DataSet ds = new DataSet();
            ds.ReadXml(filePath);
            DataTable dt = ds.Tables[tableName];
            if (dt.Rows[rowIndex][colName] != null)
            {
                dt.Rows[rowIndex][colName] = content;
                ds.WriteXml(filePath);
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }
        #endregion
        #region 使用DataSet方式更新XML文件节点
        /// <summary>
        /// 使用DataSet方式更新XML文件节点
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="tableName">表名称</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列号</param>
        /// <param name="content">更新值</param>
        /// <returns>更新是否成功</returns>
        /// 
        public static bool UpdateTableCell(string filePath, string tableName, int rowIndex, int colIndex, string content)
        {
            bool flag = false;
            DataSet ds = new DataSet();
            ds.ReadXml(filePath);
            DataTable dt = ds.Tables[tableName];
            if (dt.Rows[rowIndex][colIndex] != null)
            {
                dt.Rows[rowIndex][colIndex] = content;
                ds.WriteXml(filePath);
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }         
        #endregion
         * * */
        #region 读取XML资源中的指定节点内容
        /// <summary>
        /// 读取XML资源中的指定节点内容
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>节点内容</returns>
        public static object GetNodeValue(string source, XmlType xmlType, string nodeName)
        {
            XmlDocument xd = new XmlDocument();
            if (xmlType == XmlType.File)
            {
                xd.Load(source);
            }
            else
            {
                xd.LoadXml(source);
            }
            XmlElement xe = xd.DocumentElement;
            XmlNode xn = xe.SelectSingleNode("//" + nodeName);
            if (xn != null)
            {
                return xn.InnerText;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 读取XML资源中的指定节点内容
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>节点内容</returns>
        public static object GetNodeValue(string source, string nodeName)
        {
            if (source == null || nodeName == null || source == "" || nodeName == "" || source.Length < nodeName.Length * 2)
            {
                return null;
            }
            else
            {
                int start = source.IndexOf("<" + nodeName + ">") + nodeName.Length + 2;
                int end = source.IndexOf("</" + nodeName + ">");
                if (start == -1 || end == -1)
                {
                    return null;
                }
                else if (start >= end)
                {
                    return null;
                }
                else
                {
                    return source.Substring(start, end - start);
                }
            }
        }
        #endregion
        #region 更新XML文件中的指定节点内容
        /// <summary>
        /// 更新XML文件中的指定节点内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeValue">更新内容</param>
        /// <returns>更新是否成功</returns>
        public static bool UpdateNode(string filePath, string nodeName, string nodeValue)
        {
            bool flag = false;
            XmlDocument xd = new XmlDocument();
            xd.Load(filePath);
            XmlElement xe = xd.DocumentElement;
            XmlNode xn = xe.SelectSingleNode("//" + nodeName);
            if (xn != null)
            {
                xn.InnerText = nodeValue;
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }
        #endregion
        /// <summary>
        /// 读取xml文件，并将文件序列化为类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T ReadXML<T>(string path)
        {
            XmlSerializer reader = new XmlSerializer(typeof(T));
            StreamReader file = new StreamReader(@path);
            return (T)reader.Deserialize(file);
        }
        /// <summary>
        /// 将对象写入XML文件
        /// </summary>
        /// <typeparam name="T">C#对象名</typeparam>
        /// <param name="item">对象实例</param>
        /// <param name="path">路径</param>
        /// <param name="jjdbh">标号</param>
        /// <param name="ends">结束符号（整个xml的路径类似如下：C:\xmltest\201111send.xml，其中path=C:\xmltest,jjdbh=201111,ends=send）</param>
        /// <returns></returns>
        public static string WriteXML<T>(T item, string path, string jjdbh, string ends)
        {
            if (string.IsNullOrEmpty(ends))
            {
                //默认为发送
                ends = "send";
            }
            int i = 0;//控制写入文件的次数，
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            object[] obj = new object[] { path, "\\", jjdbh, ends, ".xml" };
            string xmlPath = String.Concat(obj);
            while (true)
            {
                try
                {
                    //用filestream方式创建文件不会出现“文件正在占用中，用File.create”则不行
                    FileStream fs;
                    fs = File.Create(xmlPath);
                    fs.Close();
                    TextWriter writer = new StreamWriter(xmlPath, false, Encoding.UTF8);
                    XmlSerializerNamespaces xml = new XmlSerializerNamespaces();
                    xml.Add(string.Empty, string.Empty);
                    serializer.Serialize(writer, item, xml);
                    writer.Flush();
                    writer.Close();
                    break;
                }
                catch (Exception)
                {
                    if (i < 5)
                    {
                        i++;
                        continue;
                    }
                    else
                    { break; }
                }
            }
            return SerializeToXmlStr<T>(item, true);
        }
        /// <summary>
        /// 静态扩展
        /// </summary>
        /// <typeparam name="T">需要序列化的对象类型，必须声明[Serializable]特征</typeparam>
        /// <param name="obj">需要序列化的对象</param>
        /// <param name="omitXmlDeclaration">true:省略XML声明;否则为false.默认false，即编写 XML 声明。</param>
        /// <returns></returns>
        public static string SerializeToXmlStr<T>(T obj, bool omitXmlDeclaration)
        {
            return XmlSerialize<T>(obj, omitXmlDeclaration);
        }
        //#region XML序列化反序列化相关的静态方法
        /// <summary>
        /// 使用XmlSerializer序列化对象
        /// </summary>
        /// <typeparam name="T">需要序列化的对象类型，必须声明[Serializable]特征</typeparam>
        /// <param name="obj">需要序列化的对象</param>
        /// <param name="omitXmlDeclaration">true:省略XML声明;否则为false.默认false，即编写 XML 声明。</param>
        /// <returns>序列化后的字符串</returns>
        public static string XmlSerialize<T>(T obj, bool omitXmlDeclaration)
        {
            /* This property only applies to XmlWriter instances that output text content to a stream; otherwise, this setting is ignored.
            可能很多朋友遇见过 不能转换成Xml不能反序列化成为UTF8XML声明的情况，就是这个原因。
            */
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.OmitXmlDeclaration = omitXmlDeclaration;
            xmlSettings.Encoding = new System.Text.UTF8Encoding(false);
            MemoryStream stream = new MemoryStream();//var writer = new StringWriter();
            XmlWriter xmlwriter = XmlWriter.Create(stream/*writer*/, xmlSettings); //这里如果直接写成：Encoding = Encoding.UTF8 会在生成的xml中加入BOM(Byte-order Mark) 信息(Unicode 字节顺序标记) ， 所以new System.Text.UTF8Encoding(false)是最佳方式，省得再做替换的麻烦
            XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
            xmlns.Add(String.Empty, String.Empty); //在XML序列化时去除默认命名空间xmlns:xsd和xmlns:xsi
            XmlSerializer ser = new XmlSerializer(typeof(T));
            ser.Serialize(xmlwriter, obj, xmlns);
            return Encoding.UTF8.GetString(stream.ToArray());//writer.ToString();
        }
        /// <summary>
        /// 使用XmlSerializer序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="obj">需要序列化的对象</param>
        /// <param name="omitXmlDeclaration">true:省略XML声明;否则为false.默认false，即编写 XML 声明。</param>
        /// <param name="removeDefaultNamespace">是否移除默认名称空间(如果对象定义时指定了:XmlRoot(Namespace = "http://www.xxx.com/xsd")则需要传false值进来)</param>
        /// <returns>序列化后的字符串</returns>
        public static void XmlSerialize<T>(string path, T obj, bool omitXmlDeclaration, bool removeDefaultNamespace)
        {
            XmlWriterSettings xmlSetings = new XmlWriterSettings();
            xmlSetings.OmitXmlDeclaration = omitXmlDeclaration;
            using (XmlWriter xmlwriter = XmlWriter.Create(path, xmlSetings))
            {
                XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
                if (removeDefaultNamespace)
                    xmlns.Add(String.Empty, String.Empty); //在XML序列化时去除默认命名空间xmlns:xsd和xmlns:xsi
                XmlSerializer ser = new XmlSerializer(typeof(T));
                ser.Serialize(xmlwriter, obj, xmlns);
            }
        }
        private static byte[] ShareReadFile(string filePath)
        {
            byte[] bytes;
            //避免"正由另一进程使用,因此该进程无法访问此文件"造成异常 共享锁 flieShare必须为ReadWrite，但是如果文件不存在的话，还是会出现异常，所以这里不能吃掉任何异常，但是需要考虑到这些问题 
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                bytes = new byte[fs.Length];
                int numBytesToRead = (int)fs.Length;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    int n = fs.Read(bytes, numBytesRead, numBytesToRead);
                    if (n == 0)
                        break;
                    numBytesRead += n;
                    numBytesToRead -= n;
                }
            }
            return bytes;
        }
        /// <summary>
        /// 从文件读取并反序列化为对象 （解决: 多线程或多进程下读写并发问题）
        /// </summary>
        /// <typeparam name="T">返回的对象类型</typeparam>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static T XmlFileDeserialize<T>(string path)
        {
            byte[] bytes = ShareReadFile(path);
            if (bytes.Length < 1)//当文件正在被写入数据时，可能读出为0
                for (int i = 0; i < 5; i++)
                { //5次机会
                    bytes = ShareReadFile(path); // 采用这样诡异的做法避免独占文件和文件正在被写入时读出来的数据为0字节的问题。
                    if (bytes.Length > 0) break;
                    System.Threading.Thread.Sleep(50); //悲观情况下总共最多消耗1/4秒，读取文件
                }
            XmlDocument doc = new XmlDocument();
            doc.Load(new MemoryStream(bytes));
            if (doc.DocumentElement != null)
                return (T)new XmlSerializer(typeof(T)).Deserialize(new XmlNodeReader(doc.DocumentElement));
            return default(T);
            /*
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.CloseInput = true;
            using (XmlReader xmlReader = XmlReader.Create(path, xmlReaderSettings))
            {
                T obj = (T)new XmlSerializer(typeof(T)).Deserialize(xmlReader);
                return obj;
            }
             * */
        }
        /// <summary>
        /// 使用XmlSerializer反序列化对象
        /// </summary>
        /// <param name="xmlOfObject">需要反序列化的xml字符串</param>
        /// <returns>反序列化后的对象</returns>
        public static T XmlDeserialize<T>(string xmlOfObject) where T : class
        {
            XmlReader xmlReader = XmlReader.Create(new StringReader(xmlOfObject), new XmlReaderSettings());
            return (T)new XmlSerializer(typeof(T)).Deserialize(xmlReader);
        }
        //#endregion
    }


    internal sealed class Logger
    {
        #region Member Variables

        /// <summary>
        /// 用于Trace的组织输出的类别名称
        /// </summary>
        //private const string trace_sql = "\r\n***********************TRACE_SQL {0}*****************************\r\nTRACE_SQL";

        /// <summary>
        /// 用于Trace的组织输出的类别名称
        /// </summary>
        private const string trace_exception = "\r\n***********************TRACE_EXCEPTION {0}***********************";

        /// <summary>
        /// 当前日志的日期
        /// </summary>
        private static DateTime CurrentLogFileDate = DateTime.Now;

        /// <summary>
        /// 日志对象
        /// </summary>
        private static TextWriterTraceListener twtl;

        /// <summary>
        /// 日志根目录
        /// </summary>
        //private const string log_root_directory = @"D:\log";
        //private static string log_root_directory = string.Format(@"{0}\log", Directory.GetCurrentDirectory());

        /// <summary>
        /// 日志子目录
        /// </summary>
        //private static string log_subdir;


        /// <summary>
        /// "      {0} = {1}"
        /// </summary>
        private const string FORMAT_TRACE_PARAM = "      {0} = {1}";

        /// <summary>
        /// 1   仅控制台输出
        /// 2   仅日志输出
        /// 3   控制台+日志输出
        /// </summary>
        private static readonly int flag = 2;         //可以修改成从配置文件读取

        #endregion

        public delegate void delegateLogEvent(int type, string message, bool raise);
        public static delegateLogEvent onLog = null;

        #region Constructor

        static Logger()
        {
            System.Diagnostics.Trace.AutoFlush = true;

            switch (flag)
            {
                case 1:
                    System.Diagnostics.Trace.Listeners.Add(new ConsoleTraceListener());
                    break;
                case 2:
                    System.Diagnostics.Trace.Listeners.Add(TWTL);
                    break;
                case 3:
                    System.Diagnostics.Trace.Listeners.Add(new ConsoleTraceListener());
                    System.Diagnostics.Trace.Listeners.Add(TWTL);
                    break;
            }
        }

        #endregion

        #region Method

        #region trace

        /// <summary>
        /// 异步错误日志
        /// </summary>
        /// <param name="value"></param>
        public static void Trace(Exception ex, bool raise = false, int logtype = 1)
        {
            if (onLog != null)
            {
                try
                {
                    string text1 = ex.Message;
                    text1 += ex.StackTrace;
                    text1 += text1.Substring(text1.Length - 2) == "\r\n" ? "" : "\r\n";
                    onLog(logtype, "[TRACE] " + text1, raise);
                }
                catch (System.Exception)
                {
                    //throw exa;
                }
            }

            try
            {
                new AsyncLogException(BeginTraceError).BeginInvoke(ex, null, null);
            }
            catch (System.Exception)
            {
            }
        }

        public static void Debug(string text)
        {
            if (onLog != null)
            {
                try
                {
                    string text1 = text;
                    text1 += text1.Substring(text1.Length - 2) == "\r\n" ? "" : "\r\n";
                    onLog(0, "[DEBUG] " + text1, false);
                }
                catch (System.Exception)
                {
                }
            }

            try
            {
                System.Diagnostics.Trace.WriteLine(string.Format("\r\n[DEBUG]{0}:{1}", DateTime.Now,
                    text.Substring(text.Length - 2) == "\r\n" ? text : text + "\r\n"));
            }
            catch (System.Exception)
            {
            }
        }


        /*
        /// <summary>
        /// 异步SQL日志
        /// </summary>
        /// <param name="cmd"></param>
        public static void Trace(SqlCommand cmd)
        {
            new AsyncLogSqlCommand(BeginTraceSqlCommand).BeginInvoke(cmd, null, null);
        }

        /// <summary>
        /// 异步SQL日志
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        public static void Trace(string sql, params SqlParameter[] parameter)
        {
            new AsyncLogSql(BeginTraceSql).BeginInvoke(sql, parameter, null, null);
        }
        */
        #endregion

        #region delegate

        private delegate void AsyncLogException(Exception ex);
        /*
        private delegate void AsyncLogSqlCommand(SqlCommand cmd);
        private delegate void AsyncLogSql(string sql, params SqlParameter[] parameter);
        */
        private static void BeginTraceError(Exception ex)
        {
            try
            {
                if (null != ex)
                {
                    //检测日志日期
                    StrategyLog();

                    //输出日志头
                    System.Diagnostics.Trace.WriteLine(string.Format(trace_exception, DateTime.Now));
                    while (null != ex)
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format("{0} {1}\r\n{2} Source:{3}", ex.GetType().Name, ex.Message, ex.StackTrace, ex.Source));
                        ex = ex.InnerException;
                    }
                }
            }
            catch (System.Exception e)
            {
                //donothing
                System.Diagnostics.Trace.WriteLine(e.Message);
            }
        }
        /*
        private static void BeginTraceSqlCommand(SqlCommand cmd)
        {
            if (null != cmd)
            {
                SqlParameter[] parameter = new SqlParameter[cmd.Parameters.Count];
                cmd.Parameters.CopyTo(parameter, 0);
                BeginTraceSql(cmd.CommandText, parameter);
            }
        }

        private static void BeginTraceSql(string sql, params SqlParameter[] parameter)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                //检测日志日期
                StrategyLog();

                System.Diagnostics.Trace.WriteLine(sql, string.Format(trace_sql, DateTime.Now));
                if (parameter != null)
                {
                    foreach (SqlParameter param in parameter)
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format(FORMAT_TRACE_PARAM, param.ParameterName, param.Value));
                    }
                }
            }
        }
        */
        #endregion

        #region helper

        /// <summary>
        /// 根据日志策略生成日志
        /// </summary>
        private static void StrategyLog()
        {
            //判断日志日期
            if (DateTime.Compare(DateTime.Now.Date, CurrentLogFileDate.Date) != 0)
            {
                DateTime currentDate = DateTime.Now.Date;

                //生成子目录
                //BuiderDir(currentDate);
                //更新当前日志日期
                CurrentLogFileDate = currentDate;

                System.Diagnostics.Trace.Flush();

                //更改输出
                //if (twtl != null)
                //    System.Diagnostics.Trace.Listeners.Remove(twtl);

                System.Diagnostics.Trace.Listeners.Add(TWTL);
            }
        }

        /// <summary>
        /// 根据年月生成子目录
        /// </summary>
        /// <param name="currentDate"></param>
        /// 
        /*
        private static void BuiderDir(DateTime currentDate)
        {
            int year = currentDate.Year;
            int month = currentDate.Month;
            //年/月
            string subdir = string.Concat(year, '\\', month);
            string path = Path.Combine(log_root_directory, subdir);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            log_subdir = subdir;
        }
        */
        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// 日志文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetLogFullPath
        {
            get
            {
                //return string.Concat(log_root_directory, '\\', string.Concat(log_subdir, @"\log", CurrentLogFileDate.ToShortDateString(), ".txt"));
                string result = string.Format(@"{0}log\{1}_{2:D4}{3:D2}{4:D2}.log", System.AppDomain.CurrentDomain.BaseDirectory,
                    System.AppDomain.CurrentDomain.FriendlyName,
                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                if (!Directory.Exists(Path.GetDirectoryName(result)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(result));
                }

                return result;
            }
        }

        /// <summary>
        /// 跟踪输出日志文件
        /// </summary>
        private static TextWriterTraceListener TWTL
        {
            get
            {
                if (twtl == null)
                {
                    /*
                    if (string.IsNullOrEmpty(log_subdir))
                        BuiderDir(DateTime.Now);
                    else
                    {
                        string logPath = GetLogFullPath;
                        if (!Directory.Exists(Path.GetDirectoryName(logPath)))
                            BuiderDir(DateTime.Now);
                    }
                     * */
                    twtl = new TextWriterTraceListener(GetLogFullPath);
                }
                return twtl;
            }
        }

        #endregion

    }

    internal abstract class XmlSerializerObject
    {
        public virtual string ToXml(string xmlRootName = null)
        {
            StringBuilder sbXML = new StringBuilder();
            object sourceObj = this;
            Type type = this.GetType();
            using (StringWriter writer = new StringWriter(sbXML))
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?
                    new System.Xml.Serialization.XmlSerializer(type) :
                    new System.Xml.Serialization.XmlSerializer(type, new XmlRootAttribute(xmlRootName));
                xmlSerializer.Serialize(writer, sourceObj);
            }
            return sbXML.ToString();
        }

        public virtual void FromXml(string sXML)
        {
            Type type = this.GetType();
            object result = type.Assembly.CreateInstance(type.FullName);

            using (StringReader reader = new StringReader(sXML))
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
                result = xmlSerializer.Deserialize(reader);
            }

            this.Copy((XmlSerializerObject)result);
        }

        public virtual void ToXMLFile(string FileFullName)
        {
            StreamWriter sw = new StreamWriter(FileFullName);
            sw.Write(ToXml());
            sw.Flush();
            sw.Close();
        }

        public virtual void FromXmlFile(string FileFullName)
        {
            StreamReader sr = new StreamReader(FileFullName);
            FromXml(sr.ReadToEnd());
        }

        public virtual void Copy(object source)
        {
            throw new NotImplementedException();
        }

        public static string ObjToXml(object sourceObj, Type type, string xmlRootName = "")
        {
            StringBuilder sbXML = new StringBuilder();
            if (sourceObj != null)
            {
                type = type != null ? type : sourceObj.GetType();

                using (StringWriter writer = new StringWriter(sbXML))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?
                        new System.Xml.Serialization.XmlSerializer(type) :
                        new System.Xml.Serialization.XmlSerializer(type, new XmlRootAttribute(xmlRootName));
                    xmlSerializer.Serialize(writer, sourceObj);
                }
            }
            return sbXML.ToString();
        }

        public enum XmlSerializeOptions
        {
            OMITHEAD = 1,
            ENCODEUTF8 = 2,
            EMPTYNS = 4,
        }

        public static string ObjToXml(object sourceObj, Type type, XmlSerializeOptions[] options, string xmlRootName = "")
        {
            string xml = "";
            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms);
                if (sourceObj != null)
                {
                    type = type != null ? type : sourceObj.GetType();

                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.IndentChars = "\t";
                    settings.NewLineChars = Environment.NewLine;
                    if (Array.IndexOf(options, XmlSerializeOptions.ENCODEUTF8) >= 0) settings.Encoding = new UTF8Encoding(false);
                    //settings.Encoding = Encoding.UTF8; //
                    if (Array.IndexOf(options, XmlSerializeOptions.OMITHEAD) >= 0) settings.OmitXmlDeclaration = true;
                    //settings.OmitXmlDeclaration = true; // 不生成声明头

                    using (XmlWriter writer = XmlWriter.Create(sw, settings))
                    {
                        System.Xml.Serialization.XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?
                            new System.Xml.Serialization.XmlSerializer(type) :
                            new System.Xml.Serialization.XmlSerializer(type, new XmlRootAttribute(xmlRootName));
                        if (Array.IndexOf(options, XmlSerializeOptions.EMPTYNS) >= 0)
                        {
                            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                            namespaces.Add(string.Empty, string.Empty);
                            xmlSerializer.Serialize(writer, sourceObj, namespaces);
                        }
                        else
                        {
                            xmlSerializer.Serialize(writer, sourceObj);
                        }
                        writer.Flush();
                        writer.Close();
                    }
                }

                using (StreamReader sr = new StreamReader(ms))
                {
                    ms.Position = 0;
                    xml = sr.ReadToEnd();
                    sr.Close();
                }
            }
            return xml;
        }

        public static void ObjToXml(object sourceObj, Type type, XmlWriter parent)
        {
            if (sourceObj != null)
            {
                type = type != null ? type : sourceObj.GetType();

                System.Xml.Serialization.XmlSerializer xmlSerializer =
                    new System.Xml.Serialization.XmlSerializer(type);

                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                xmlSerializer.Serialize(parent, sourceObj, namespaces);
            }
        }

        public static object ObjFromXml(string sXML, Type type)
        {
            object result = type.Assembly.CreateInstance(type.FullName);
            using (StringReader reader = new StringReader(sXML))
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
                result = xmlSerializer.Deserialize(reader);
            }

            return result;
        }

        public static object ObjFromXml(Type type, XmlReader parent)
        {
            StringBuilder sXMLb = new StringBuilder();
            object result = type.Assembly.CreateInstance(type.FullName);
            using (XmlWriter xw = XmlWriter.Create(sXMLb))
            {
                xw.WriteRaw(parent.ReadInnerXml());
                xw.Flush();
                xw.Close();
            }
            using (StringReader reader = new StringReader(sXMLb.ToString()))
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
                result = xmlSerializer.Deserialize(reader);
            }
            return result;
        }
        public static T ObjFromXml<T>(string sXML) //where T : new()
        {
            //T result = new T();
            T result = default(T);
            using (StringReader reader = new StringReader(sXML))
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                result = (T)xmlSerializer.Deserialize(reader);
            }

            return result;
        }

        public static void ObjToXMLFile(string FileFullName, object sourceObj, Type type, XmlSerializeOptions[] options, string xmlRootName = "")
        {
            StreamWriter sw = new StreamWriter(FileFullName, false);
            if (options != null && options.Length > 0)
                sw.Write(ObjToXml(sourceObj, type, options, xmlRootName));
            else
                sw.Write(ObjToXml(sourceObj, type, xmlRootName));
            sw.Flush();
            sw.Close();
        }

        public static T ObjFromXmlFile<T>(string FileFullName)
        {
            StreamReader sr = new StreamReader(FileFullName, false);
            string sXML = sr.ReadToEnd();
            sr.Close();
            return (T)ObjFromXml(sXML, typeof(T));
        }

        public static TTO ObjTOObj<TFROM, TTO>(TFROM fromobj)
        {
            string sXML = ObjToXml(fromobj, typeof(TFROM));
            return ObjFromXml<TTO>(sXML);
        }
    }

    [Serializable]
    public class SaDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public SaDictionary() : base() { }
        public void WriteXml(XmlWriter write)       // Serializer
        {
            XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

            foreach (KeyValuePair<TKey, TValue> kv in this)
            {
                write.WriteStartElement("Dictionary");

                if ((typeof(TKey).IsValueType || (typeof(TKey) == typeof(string)))
                    || (typeof(TValue).IsValueType || (typeof(TValue) == typeof(string))))
                {
                    write.WriteAttributeString("key", kv.Key.ToString());
                    write.WriteAttributeString("value", kv.Value.ToString());
                }
                else
                {
                    write.WriteStartElement("key");
                    KeySerializer.Serialize(write, kv.Key);
                    write.WriteEndElement();
                    write.WriteStartElement("value");
                    ValueSerializer.Serialize(write, kv.Value);
                    write.WriteEndElement();
                }
                write.WriteEndElement();
            }
        }

        public void ReadXml(XmlReader reader)       // Deserializer
        {
            reader.Read();
            XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

            while (reader.NodeType != XmlNodeType.None && reader.NodeType != XmlNodeType.EndElement)
            {
                //reader.ReadStartElement("Dictionary");                                                

                if ((typeof(TKey).IsValueType || (typeof(TKey) == typeof(string)))
                    || (typeof(TValue).IsValueType || (typeof(TValue) == typeof(string))))
                {
                    if (reader.MoveToContent() != XmlNodeType.Element) break;
                    TKey k = (TKey)Convert.ChangeType(reader["key"], typeof(TKey));
                    TValue v = (TValue)Convert.ChangeType(reader["value"], typeof(TValue));
                    this[k] = v;
                    reader.ReadToNextSibling("Dictionary");
                }
                else
                {
                    reader.ReadStartElement("Dictionary");
                    reader.ReadStartElement("key");
                    TKey tk = (TKey)KeySerializer.Deserialize(reader);
                    reader.ReadEndElement();
                    reader.ReadStartElement("value");
                    TValue vl = (TValue)ValueSerializer.Deserialize(reader);
                    reader.ReadEndElement();
                    reader.ReadEndElement();
                    //this.Add(tk, vl);
                    this[tk] = vl;
                    reader.ReadEndElement();
                }
            }
            reader.Read();
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }
    }

    internal static class UtilCls
    {
        public static void FillWith<T>(this T[] source, T Value, int StartIndex = 0)// where T : new()
        {
            for (int i = StartIndex; i < source.Length; i++)
            {
                source[i] = Value;
            }
        }

        public static T IsNull<T>(T source, T def)
        {
            if (source == null)
            {
                return def;
            }
            return source;
        }

    }


    public class RegistryHelper
    {
        /// <summary>
        /// root键转换，64/32位系统
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public RegistryKey RootKey(RegistryHive root)
        {
            RegistryKey localMachineRegistry
                = RegistryKey.OpenBaseKey(root,
                                          Environment.Is64BitOperatingSystem
                                              ? RegistryView.Registry64
                                              : RegistryView.Registry32);

            return localMachineRegistry;
        }
        /// <summary>
        /// 读取指定名称的注册表的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetRegistryData(RegistryKey root, string subkey, string name)
        {
            string registData = "";
            RegistryKey myKey = root.OpenSubKey(subkey, true);
            if (myKey != null)
            {
                registData = myKey.GetValue(name).ToString();
            }

            return registData;
        }

        /// <summary>
        /// 向注册表中写数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tovalue"></param> 
        public void SetRegistryData(RegistryKey root, string subkey, string name, string value)
        {
            RegistryKey aimdir = root.CreateSubKey(subkey);
            aimdir.SetValue(name, value);
        }

        /// <summary>
        /// 删除注册表中指定的注册表项
        /// </summary>
        /// <param name="name"></param>
        public void DeleteRegist(RegistryKey root, string subkey, string name)
        {
            string[] subkeyNames;
            RegistryKey myKey = root.OpenSubKey(subkey, true);
            subkeyNames = myKey.GetSubKeyNames();
            foreach (string aimKey in subkeyNames)
            {
                if (aimKey == name)
                    myKey.DeleteSubKeyTree(name);
            }
        }

        /// <summary>
        /// 判断指定注册表项是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsRegistryExist(RegistryKey root, string subkey, string name)
        {
            bool _exit = false;
            string[] subkeyNames;
            RegistryKey myKey = root.OpenSubKey(subkey, true);
            if (myKey == null)
            {
                return false;
            }

            subkeyNames = myKey.GetValueNames();
            foreach (string keyName in subkeyNames)
            {
                if (keyName == name)
                {
                    _exit = true;
                    return _exit;
                }
            }

            return _exit;
        }
    }

    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public SerializableDictionary() { }
        public void WriteXml(XmlWriter write)       // Serializer
        {
            XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

            foreach (KeyValuePair<TKey, TValue> kv in this)
            {
                write.WriteStartElement("SerializableDictionary");
                write.WriteStartElement("key");
                KeySerializer.Serialize(write, kv.Key);
                write.WriteEndElement();
                write.WriteStartElement("value");
                ValueSerializer.Serialize(write, kv.Value);
                write.WriteEndElement();
                write.WriteEndElement();
            }
        }
        public void ReadXml(XmlReader reader)       // Deserializer
        {
            reader.Read();
            XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("SerializableDictionary");
                reader.ReadStartElement("key");
                TKey tk = (TKey)KeySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                TValue vl = (TValue)ValueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadEndElement();
                this.Add(tk, vl);
                reader.MoveToContent();
            }
            reader.ReadEndElement();

        }
        public XmlSchema GetSchema()
        {
            return null;
        }
    }

    public class ToolBox
    {
        public static string GetCurrentDirectory()
        {
            string _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            _CodeBase = _CodeBase.Substring(8, _CodeBase.Length - 8);    // 8是 file:// 的长度
            return Path.GetDirectoryName(_CodeBase);
            /*
            string[] arrSection = _CodeBase.Split(new char[] { '/' });
            string _FolderPath = "";
            for (int i = 0; i < arrSection.Length - 1; i++)
            {
                _FolderPath += arrSection[i] + "/";
            }

            return _FolderPath;
             * */
        }

        public static string ComputeFileSHA1(string FileName)
        {
            try
            {
                byte[] hr;
                using (SHA1Managed Hash = new SHA1Managed()) // 创建Hash算法对象
                {
                    using (FileStream fs = new FileStream(FileName, FileMode.Open)) // 创建文件流对象
                    {
                        hr = Hash.ComputeHash(fs); // 计算 
                    }
                }
                return BitConverter.ToString(hr).Replace("-", ""); // 转化为十六进制字符串
            }
            catch (IOException)
            {
                return "Error:访问文件时出现异常";
            }
        }

        //public delegate void DelegateThreadProc();

        public static void RunInPool(WaitCallback proc, int MaxThreads = 20)
        {
            ThreadPool.SetMaxThreads(MaxThreads, MaxThreads);
            ThreadPool.QueueUserWorkItem(proc);
        }

        public static void StringToFile(string sFilename, string sText)
        {
            using (FileStream fs = new FileStream(sFilename, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sText);
                    sw.Flush();
                }
            }
        }

        public static string FileToString(string sFilename)
        {
            using (FileStream fs = new FileStream(sFilename, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public delegate void DelegateThreadProc();

        public static void RunThread(DelegateThreadProc proc)
        {
            ThreadStart ts = new ThreadStart(proc);
            Thread newThread = new Thread(ts);
            newThread.Start();
        }

        public static ICollection<string> EnumPath(string path, string filter)
        {
            List<string> _files = new List<string>();

            DirectoryInfo di = new DirectoryInfo(path);

            FileInfo[] fs = di.GetFiles();
            foreach (FileInfo fi in fs)
            {
                if (filter != "")
                {
                    if (filter.ToLower().IndexOf(Path.GetExtension(fi.FullName).ToLower()) < 0)
                    {
                        continue;
                    }
                }
                _files.Add(fi.FullName);
            }

            DirectoryInfo[] ds = di.GetDirectories();
            foreach (DirectoryInfo d in ds)
            {
                if (filter == ".")
                {
                    _files.Add(d.FullName);
                }
                _files.AddRange(EnumPath(d.FullName, filter));
            }

            return _files;
        }

        public static void ProcessOpenFile(string filename, string process = "")
        {
            Process p = new Process();
            p.StartInfo.FileName = process == "" ? filename : process;
            p.StartInfo.Arguments = process == "" ? "" : filename;
            //p.StartInfo.Verb = "Print";  
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            p.Start();
        }

        public static void CopyObject<T>(T origin, T target)
        {
            /*
            System.Reflection.PropertyInfo[] properties = (target.GetType()).GetProperties();
            System.Reflection.FieldInfo[] fields = (origin.GetType()).GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                for (int j = 0; j < properties.Length; j++)
                {
                    if (fields[i].Name == properties[j].Name && properties[j].CanWrite)
                    {
                        properties[j].SetValue(target, fields[i].GetValue(origin), null);
                    }
                }
            }
            */
            System.Reflection.PropertyInfo[] properties = (typeof(T)).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].CanWrite && properties[i].CanRead)
                {
                    try
                    {
                        properties[i].SetValue(target, properties[i].GetValue(origin, null), null);
                    }
                    catch (Exception) {}                    
                }
            }
            System.Reflection.FieldInfo[] fields = (typeof(T)).GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                try
                {
                    fields[i].SetValue(target, fields[i].GetValue(origin));
                }
                catch (Exception) { }                
            }
        }

    }
}
