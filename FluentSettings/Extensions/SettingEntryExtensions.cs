using System;

namespace FluentSettings
{
    public static class SettingEntryExtensions
    {

        public static ISettingEntry WithDefaultAccess(this ISettingEntry entry)
        {
            //Set to none first
            entry.Access = Access.None;

            //Then try and determine what we can do
            if (entry.Property.CanRead)
                entry.Access |= Access.Read;

            if (entry.Property.CanWrite)
                entry.Access |= Access.Write;

            if (entry.Access == Access.None)
                throw new AccessViolationException(String.Format("Cannot read or write from property {0}", entry.Property.Name));

            return entry;
        }

        public static SettingEntry<T> WithDefaultAccess<T>(this SettingEntry<T> entry)
        {
            WithDefaultAccess(entry as ISettingEntry);
            return entry;
        }

        public static ISettingEntry ReadOnly(this ISettingEntry entry)
        {
            if (!entry.Property.CanRead)
                throw new AccessViolationException(String.Format("Cannot read from property {0}", entry.Property.Name));

            entry.Access = Access.Read;
            return entry;
        }

        public static SettingEntry<T> ReadOnly<T>(this SettingEntry<T> entry)
        {
            ReadOnly(entry as ISettingEntry);
            return entry;
        }

        public static ISettingEntry WriteOnly(this ISettingEntry entry)
        {
            if (!entry.Property.CanWrite)
                throw new AccessViolationException(String.Format("Cannot write to property {0}", entry.Property.Name));

            entry.Access = Access.Write;
            return entry;
        }

        public static SettingEntry<T> WriteOnly<T>(this SettingEntry<T> entry)
        {
            WriteOnly(entry as ISettingEntry);
            return entry;
        }

        public static ISettingEntry ReadWrite(this ISettingEntry entry)
        {
            if (!entry.Property.CanWrite)
                throw new AccessViolationException(String.Format("Cannot write to property {0}", entry.Property.Name));

            if (!entry.Property.CanRead)
                throw new AccessViolationException(String.Format("Cannot read from property {0}", entry.Property.Name));

            entry.Access = Access.ReadWrite;
            return entry;
        }

        public static SettingEntry<T> ReadWrite<T>(this SettingEntry<T> entry)
        {
            ReadWrite(entry as ISettingEntry);
            return entry;
        }

        public static SettingEntry<T> DefaultIs<T>(this SettingEntry<T> entry, T defaultValue)
        {
            entry.DefaultValue = defaultValue;
            return entry;
        }

    }
}
