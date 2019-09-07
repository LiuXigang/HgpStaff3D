using System;
using System.Reflection;

namespace HgpStaff3D.CommonExtension
{
    public static class CommExtensions
    {
        public static string EnumDesc(this Enum enumValue)
        {
            var value = enumValue?.ToString() ?? "";
            try
            {
                FieldInfo field = enumValue.GetType().GetField(value);
                //获取描述属性
                object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                //当描述属性没有时，直接返回名称
                if (objs.Length == 0)
                    return value;
                System.ComponentModel.DescriptionAttribute descriptionAttribute = (System.ComponentModel.DescriptionAttribute)objs[0];
                return descriptionAttribute.Description;
            }
            catch (Exception)
            {
                return value;
            }

        }
    }
}
