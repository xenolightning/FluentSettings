using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSettings.Storage;

namespace FluentSettings
{
    public sealed class SettingRepository : ISettingRepository
    {

        public static ISettingRepository Default
        {
            get;
            private set;
        }

        static SettingRepository()
        {
            //TODO
            //Default = new SettingRepository();
        }

        private readonly HashSet<ISettingConfiguration> _settings;

        public SettingRepository(ISettingStorage storage)
        {
            _settings = new HashSet<ISettingConfiguration>();
        }

        public SettingConfiguration<T> For<T>(string name = null)
        {
            return new SettingConfiguration<T>(this).Named(name);
        }

        public void Add<T>(SettingConfiguration<T> configuration)
        {
            if (_settings.Any(x => x.Name == configuration.Name && x.ObjectType == configuration.ObjectType))
                throw new Exception(); //TODO


            _settings.Add(configuration);
        }

        public T Restore<T>(T instance, string name = null)
        {
            throw new NotImplementedException();
        }

    }
}
