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
    }
}
