using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSettings
{
    public class SettingsEngine : ISettingsEngine
    {
        public SettingConfiguration<T> Configure<T>(T target)
        {
            return new SettingConfiguration<T>(target);
        }
    }
}
