using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSettings
{
    public interface ISettingsEngine
    {

        SettingConfiguration<T> Configure<T>(T target);

    }
}
