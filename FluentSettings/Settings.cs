using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSettings
{
    public static class Settings
    {

        private static ISettingsEngine _engine;
        public static ISettingsEngine Engine
        {
            get
            {
                if (_engine == null)
                    _engine = new SettingsEngine();

                return _engine;
            }
        }

    }
}
