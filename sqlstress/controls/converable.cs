using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace sqlstress
{
    public interface IStringConverable
    {
        //string ToString();
        Object FromString(string objstr);
    }

    public class StringConvertor<T> : ExpandableObjectConverter where T : IStringConverable
    {
        /// <summary>
        /// 返回该转换器是否可以使用指定的上下文将该对象转换为指定的类型
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(T))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// 返回该转换器是否可以使用指定的上下文将给定类型的对象转换为此转换器的类型
        /// </summary>
        /// <param name="context">提供有关组件的上下文，如其容器和属性描述符</param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// 使用指定的上下文和区域性信息将给定的值对象转换为指定的类型
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture,
            object value, Type destinationType)
        {
            if (destinationType == typeof(string) &&
                value is T)
            {
                return value.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }


        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                T obj = default(T);
                return obj.FromString((string)value);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }

    public class XMLConvertor<T> : ExpandableObjectConverter
    {
        /// <summary>
        /// 返回该转换器是否可以使用指定的上下文将该对象转换为指定的类型
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(T))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// 返回该转换器是否可以使用指定的上下文将给定类型的对象转换为此转换器的类型
        /// </summary>
        /// <param name="context">提供有关组件的上下文，如其容器和属性描述符</param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// 使用指定的上下文和区域性信息将给定的值对象转换为指定的类型
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture,
            object value, Type destinationType)
        {
            if (destinationType == typeof(string) &&
                value is T)
            {
                //return Utils.XmlSerializerObject.ObjToXml(value, typeof(T));
                return value == null ? "" : value.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }


        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                return Utils.XmlSerializerObject.ObjFromXml<T>((string)value);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
        
    public interface IDisplay
    {
        /// <summary>
        /// 得到显示字符串
        /// </summary>
        /// <returns></returns>
        string GetPropValueDisplay();
    }
    
    public class XMLDisplayConvertor<T> : XMLConvertor<T> where T : IDisplay
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture,
            object value, Type destinationType)
        {
            if (destinationType == typeof(string) &&
                value is T)
            {
                return ((IDisplay)value).GetPropValueDisplay(); ;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
