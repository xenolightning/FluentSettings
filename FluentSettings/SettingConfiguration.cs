using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentSettings
{
    public class SettingConfiguration<T>
    {

        private HashSet<string> _properties;

        public IEnumerable<string> Properties
        {
            get
            {
                return _properties;
            }
        }

        public WeakReference TargetReference
        {
            get;
            private set;
        }

        public SettingConfiguration(T target)
        {
            _properties = new HashSet<string>();
            TargetReference = new WeakReference(target);
        }

        public SettingConfiguration<T> AddProperty(string name)
        {
            var pi = GetProperty(typeof(T), name);
            if (pi != null)
                _properties.Add(name);

            return this;
        }

        public SettingConfiguration<T> AddProperty(Expression<Func<T, object>> property)
        {
            var body = property.Body as UnaryExpression;
            if (body == null || !(body.Operand is MemberExpression))
                throw new ArgumentException("Invalid expression [" + property + "]", "property");

            var name = (body.Operand as MemberExpression).Member.Name;

            return AddProperty(name);
        }

        private static PropertyInfo GetProperty(Type t, string name)
        {
            var pi = t.GetProperty(name,
                BindingFlags.GetProperty
                | BindingFlags.SetProperty
                | BindingFlags.Instance
                | BindingFlags.Static
                | BindingFlags.SetField
                | BindingFlags.GetField);

            return pi;
        }

    }
}