using System.Collections.Generic;

namespace FluentSettings.Storage
{
    public interface ISettingStorage
    {

        void Save(IEnumerable<ISettingConfiguration> settings);

        IEnumerable<ISettingConfiguration> Load();

    }
}