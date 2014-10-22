using System;
using System.Collections.Generic;

namespace FluentSettings
{
    public interface ISettingConfiguration
    {
        string Name
        {
            get;
        }

        IEnumerable<ISettingEntry> Settings
        {
            get;
        } 

        Type ObjectType
        {
            get;
        }

    }
}