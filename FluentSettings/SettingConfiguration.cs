using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentSettings
{
    public sealed class SettingConfiguration<T> : ISettingConfiguration
    {
        private readonly ISettingRepository _repository;
        private readonly HashSet<ISettingEntry> _settings;

        public string Name
        {
            get;
            private set;
        }

        public IEnumerable<ISettingEntry> Settings
        {
            get { return _settings; }
        }

        public Type ObjectType
        {
            get { return typeof(T); }
        }

        public SettingConfiguration(ISettingRepository repository)
        {
            _repository = repository;
            _settings = new HashSet<ISettingEntry>();
        }

        public SettingConfiguration<T> Named(string name)
        {
            Name = name;
            return this;
        }

        public SettingConfiguration<T> UsingDefaults()
        {

            return Configure(x =>
            {
                //Register the type with the default configuration options
                foreach (var pi in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    x.Add(pi).WithDefaultAccess();
                }
            });
        }

        public SettingConfiguration<T> Configure(Action<ConfigurationExpression<T>> configuration)
        {
            //Register with the configuration
            var cfg = new ConfigurationExpression<T>();

            configuration(cfg);
            Configure(cfg);

            return this;
        }

        private void Configure(ConfigurationExpression<T> configurationExpression)
        {
            foreach (var e in configurationExpression.Entries)
            {
                _settings.Add(e);
            }
        }

        public void Commit()
        {
            _repository.Add(this);
        }

    }
}