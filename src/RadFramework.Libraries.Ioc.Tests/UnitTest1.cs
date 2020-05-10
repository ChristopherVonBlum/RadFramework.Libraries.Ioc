using NUnit.Framework;
using RadFramework.Libraries.Ioc.Container;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            SimpleContainer c = new SimpleContainer();
            
            c.Register(typeof(Test));
            c.Register(typeof(TestDep))
                ;

            var res = c.Resolve<Test>();
            Assert.Pass();
        }
    }

    public class Test
    {
        private readonly TestDep dep;

        public Test(TestDep dep)
        {
            this.dep = dep;
        }
    }

    public class TestDep
    {
        public TestDep()
        {
            
        }
    }
}