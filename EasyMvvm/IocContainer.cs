using System;
using System.Collections.Generic;
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
    }
}
