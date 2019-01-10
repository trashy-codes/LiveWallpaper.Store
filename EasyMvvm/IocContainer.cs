using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMvvm
{
    public class IocContainer
    {
        public IocContainer Singleton<T>()
        {
            return this;
        }

        public IocContainer PerRequest<T>()
        {
            return this;
        }
    }
}
