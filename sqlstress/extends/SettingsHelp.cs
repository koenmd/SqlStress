using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;
using Utils;

namespace sqlstress
{
    public class UsualSetting
    {
        public string DisplayName = "Default";
        public DbEngineSetting Setting = new DbEngineSetting();
    }

    [Serializable]
    [TypeConverter(typeof(XMLConvertor<EnginSettingsHelper>))]
    public class EnginSettingsHelper : List<UsualSetting>, ICollection<UsualSetting>, IXmlSerializable
    {
        [Browsable(false)]
        public StressScheme Scheme { get; set; } = null;
        public UsualSetting Current { get { return _current; } set { SetCurrent(value); } } private UsualSetting _current = null;
        [Browsable(false)]
        public string CurrentName { get { return Current != null ? Current.DisplayName : ""; } }
        [Browsable(false)]
        public DbEngineSetting CurrentSetting { get { return Current != null ? Current.Setting : null; } }

        public static EnginSettingsHelper LoadEnginSettingsHelper()
        {
            var result = new EnginSettingsHelper();
            result.Load();
            return result;
        }
        public EnginSettingsHelper()
        {
            //Load();
        }

        private void SetCurrent(UsualSetting value)
        {
            if (value != _current)
            {
                _current = value;
                if (Scheme != null && value != null)
                {
                    Scheme.dbsettings = value.Setting;
                }
            }
        }

        public UsualSetting GetSettingByName(string displayname)
        {
            foreach (UsualSetting item in this)
            {
                if (item.DisplayName == displayname)
                {
                    return item;
                }
            }
            return null;
        }

        public void Load()
        {
            try
            {
                EnginSettingsHelper helper = Utils.XmlSerializerObject.ObjFromXmlFile<EnginSettingsHelper>(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.usualconnfile));
                this.Clear();
                this.AddRange(helper);
            }
            catch (Exception)
            {
            }            
        }

        public void Save()
        {
            Utils.XmlSerializerObject.ObjToXMLFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.usualconnfile), this, typeof(EnginSettingsHelper), null);
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer itemReader = new XmlSerializer(typeof(UsualSetting));

            reader.Read();
            if (reader.MoveToContent() == XmlNodeType.Element)
            {
                if (reader.LocalName != "SettingCollection")   reader.ReadToDescendant("SettingCollection");
                reader.ReadStartElement("SettingCollection");
                while (reader.NodeType != XmlNodeType.None && reader.NodeType != XmlNodeType.EndElement)
                {
                    if (reader.MoveToContent() != XmlNodeType.Element) break;
                    reader.ReadStartElement("item");
                    UsualSetting newsetting = (UsualSetting)itemReader.Deserialize(reader);
                    Add(newsetting);
                    reader.ReadEndElement();
                    reader.ReadEndElement();
                    /*
                    if (reader.MoveToContent() != XmlNodeType.Element) break;
                    try
                    {
                        reader.MoveToContent();
                        UsualSetting newsetting = new UsualSetting();
                        newsetting.DisplayName = reader.ReadElementString("displayname");
                        newsetting.Setting.ReadXml(reader);
                        Add(newsetting);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    reader.Read();
                    */
                }
                reader.ReadEndElement();                
            }
            reader.Read();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer itemWriter = new XmlSerializer(typeof(UsualSetting));

            writer.WriteStartElement("SettingCollection");
            foreach (UsualSetting setting in this)
            {
                writer.WriteStartElement("item");
                //writer.WriteElementString("displayname", setting.DisplayName);
                //setting.Setting.WriteXml(writer);
                itemWriter.Serialize(writer, setting);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public override string ToString()
        {
            return this.CurrentName;
        }
    }
}
