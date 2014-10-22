using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FluentSettings
{
    [Flags]
    public enum Access
    {
        None = 0x00000000,
        Read = 0x00000001,
        Write = 0x00000010,
        ReadWrite = Read | Write
    }

    public interface ISettingEntry
    {
        PropertyInfo Property
        {
            get;
            set;
        }

        Access Access
        {
            get;
            set;
        }

        object DefaultValue
        {
            get;
            set;
        }
    }

    public sealed class SettingEntry<TProperty> : ISettingEntry
    {
        public SettingEntry(PropertyInfo propertyInfo)
        {
            Property = propertyInfo;
        }

        public PropertyInfo Property
        {
            get;
            set;
        }

        public Access Access
        {
            get;
            set;
        }

        public TProperty DefaultValue
        {
            get;
            set;
        }

        object ISettingEntry.DefaultValue
        {
            get { return DefaultValue; }
            set
            {
                //Can do direct assign
                if (value != null && value.GetType() == typeof (TProperty))
                {
                    DefaultValue = (TProperty) value;
                    return;
                }

                //Otherwise set it to the default
                DefaultValue = default(TProperty);
            }
        }

    }

    
}
