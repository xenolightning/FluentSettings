using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FluentSettings
{
    public interface ISettingRepository
    {

        SettingConfiguration<T> For<T>(string name = null);

        void Add<T>(SettingConfiguration<T> configuration);

    }
}
