using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyMvvm
{
    public class IocCacheData
    {
        public IocCacheData(bool singleton)
        {
            Singleton = singleton;
        }
        public string Key { get; set; }
        public Type TargetType { get; set; }
        public object Instance { get; set; }
        public bool Singleton { private set; get; }
    }

    public class IocContainer
    {
        private Guid _id = Guid.NewGuid();
        private Dictionary<string, IocCacheData> _cache = new Dictionary<string, IocCacheData>();

        public IocContainer Singleton<T>(string key = null)
        {
            Type targetType = typeof(T);

            if (string.IsNullOrEmpty(key))
            {
                key = GetDefaultName(targetType);
            }
            _cache[key] = new IocCacheData(true)
            {
                TargetType = targetType,
                Key = key
            };
            return this;
        }

        public IocContainer PerRequest<T>(string key = null)
        {
            return this;
        }

        public T Get<T>(string key = null) where T : object
        {
            T result = default(T);
            if (string.IsNullOrEmpty(key))
                result = (T)Get(typeof(T));
            else
                result = (T)Get(key);
            return result;
        }

        public object Get(Type type)
        {
            var key = GetDefaultName(type);
            var result = Get(key);
            return result;
        }

        public object Get(string key)
        {
            if (!_cache.Keys.Contains(key))
                return null;

            var data = _cache[key];

            if (data.Singleton && data.Instance != null)
                return data.Instance;

            var args = GetConstructorArgs(data.TargetType);
            var result = ActivateInstance(data.TargetType, args);

            //释放旧对象
            if (data.Instance is IDisposable)
                (data as IDisposable).Dispose();

            data.Instance = result;

            return result;
        }

        #region private

        //没有主动设置key，就用默认key
        private string GetDefaultName(Type type)
        {
            //加guid是为了防止，设置同名key覆盖掉默认的对象
            string name = type.Name + _id.ToString();
            return name;
        }

        #endregion

        #region reflector

        private object[] GetConstructorArgs(Type type)
        {
            var args = new List<object>();
            var constructor = GetConstructor(type);

            if (constructor != null)
            {
                args.AddRange(constructor.GetParameters().Select(info => Get(info.ParameterType)));
            }

            return args.ToArray();
        }

        private static ConstructorInfo GetConstructor(Type type)
        {
            return (from c in type.GetTypeInfo().DeclaredConstructors.Where(c => c.IsPublic)
                    orderby c.GetParameters().Length descending
                    select c).FirstOrDefault();
        }

        private static object ActivateInstance(Type type, object[] args)
        {
            var instance = args.Length > 0 ? System.Activator.CreateInstance(type, args) : System.Activator.CreateInstance(type);
            return instance;
        }

        #endregion
    }
}
