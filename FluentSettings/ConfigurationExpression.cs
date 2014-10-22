using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentSettings
{
    public sealed class ConfigurationExpression<T>
    {

        private readonly HashSet<ISettingEntry> _entries;

        internal IEnumerable<ISettingEntry> Entries
        {
            get { return _entries; }
        }

        public ConfigurationExpression()
        {
            _entries = new HashSet<ISettingEntry>();
        }

        public ISettingEntry Add(PropertyInfo propertyInfo)
        {
            Type type = typeof(T);

            if (type != propertyInfo.ReflectedType &&
                !type.IsSubclassOf(propertyInfo.ReflectedType))
                throw new ArgumentException(string.Format("Property '{0}' is not from type {1}.", propertyInfo.Name, type));

            //Have to use reflection to create the setting entry. Not ideal.
            Type settingEntryType = typeof (SettingEntry<>).MakeGenericType(new Type[] {propertyInfo.PropertyType});

            var entry = (ISettingEntry) Activator.CreateInstance(settingEntryType);
            _entries.Add(entry);
            return entry;
        }

        public SettingEntry<TProperty> Add<TProperty>(PropertyInfo propertyInfo)
        {
            Type type = typeof(T);

            if (propertyInfo.PropertyType != typeof(TProperty))
                throw new ArgumentException(string.Format("PropertyInfo type {0} does not match the expected type {1}.", propertyInfo.PropertyType, typeof(TProperty)));

            //Explicitly not calling the other add method here, because I want to avoid
            //using reflection for the object creation when not required.

            if (type != propertyInfo.ReflectedType &&
                !type.IsSubclassOf(propertyInfo.ReflectedType))
                throw new ArgumentException(string.Format("Property '{0}' is not from type {1}.", propertyInfo.Name, type));

            var entry = new SettingEntry<TProperty>(propertyInfo);
            _entries.Add(entry);
            return entry;
        }

        public SettingEntry<TProperty> Add<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            return Add<TProperty>(GetPropertyInfo(propertyExpression));
        }

        private PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;

            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo;
        }

    }
}