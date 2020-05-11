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
            Container c = new Container();
            
            c.RegisterTransient(typeof(Test));
            c.RegisterSingleton(typeof(TestDep))
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