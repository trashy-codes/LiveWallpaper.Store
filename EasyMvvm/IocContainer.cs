using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMvvm
{
    public class IocContainer
    {
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
