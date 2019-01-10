using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyMvvm
{
    public class IocCacheData
    {
        public string Key { get; set; }
        public Type TargetType { get; set; }
        public object Instance { get; set; }
    }

    public class IocContainer
    {
        private Dictionary<string, IocCacheData> _cache = new Dictionary<string, IocCacheData>();

        public IocContainer Singleton<T>(string key = null)
        {
            if (string.IsNullOrEmpty(key))
            {

            }
            _cache[key] = new IocCacheData()
            {
                TargetType = typeof(T)
            };
            return this;
        }

        public IocContainer PerRequest<T>(string key = null)
        {
            return this;
        }

        public T Get<T>(string key = null) where T : object
        {
            if (string.IsNullOrEmpty(key))
                key = nameof(T);
            var result = (T)Get(key);
            return result;
        }

        public object Get(string key)
        {
            return null;
        }

        private object[] DetermineConstructorArgs(Type implementation)
        {
            var args = new List<object>();
            var constructor = SelectEligibleConstructor(implementation);

            if (constructor != null)
            {
                //args.AddRange(constructor.GetParameters().Select(info => GetInstance(info.ParameterType, null)));
            }

            return args.ToArray();
        }

        private static ConstructorInfo SelectEligibleConstructor(Type type)
        {
            return (from c in type.GetTypeInfo().DeclaredConstructors.Where(c => c.IsPublic)
                    orderby c.GetParameters().Length descending
                    select c).FirstOrDefault();
        }

        protected virtual object ActivateInstance(Type type, object[] args)
        {
            var instance = args.Length > 0 ? System.Activator.CreateInstance(type, args) : System.Activator.CreateInstance(type);
            return instance;
        }
    }
}
