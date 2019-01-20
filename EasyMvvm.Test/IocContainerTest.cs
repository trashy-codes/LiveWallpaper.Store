using System;
using Xunit;

namespace EasyMvvm.Test
{
    class TestObjA
    {
        public string Data { get; set; } = "A";
    }

    class TestObjB
    {
        public TestObjB(TestObjA a)
        {
            A = a;
        }

        public TestObjA A { get; set; }
    }

    public class IocContainerTest
    {

        [Fact]
        public void Singleton()
        {
            IocContainer container = new IocContainer();
            container.Singleton<object>();

            var instanceA = container.Get<object>();
            var instanceB = container.Get<object>();

            Assert.Same(instanceA, instanceB);
        }


        [Fact]
        public void Singleton2()
        {
            IocContainer container = new IocContainer();
            container.Singleton<TestObjA>();
            container.Singleton<TestObjB>();

            var instanceA = container.Get<TestObjB>();
            var instanceB = container.Get<TestObjB>();

            Assert.Same(instanceA, instanceB);
            Assert.Same(instanceA.A.Data, "A");
        }

        [Fact]
        public void PerRequest()
        {
            IocContainer container = new IocContainer();
            container.PerRequest<TestObjA>();
            container.PerRequest<TestObjB>();

            var instanceA = container.Get<TestObjB>();
            var instanceB = container.Get<TestObjB>();

            Assert.NotSame(instanceA, instanceB);
            Assert.Same(instanceA.A.Data, "A");
            Assert.Same(instanceB.A.Data, "A");
        }

        [Fact]
        public void Instance()
        {
            IocContainer container = new IocContainer();
            TestObjA a = new TestObjA();
            container.Instance(a);

            var instanceA = container.Get<TestObjA>();

            Assert.Same(a, instanceA);
        }
    }
}
