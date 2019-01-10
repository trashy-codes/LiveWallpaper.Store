using System;
using Xunit;

namespace EasyMvvm.Test
{
    public class IocContainerTest
    {
        [Fact]
        public void Test1()
        {
            IocContainer container = new IocContainer();
            container.Singleton<object>();

            var instanceA = container.Get<object>();
            var instanceB = container.Get<object>();

            Assert.Same(instanceA, instanceB);
        }
    }
}
