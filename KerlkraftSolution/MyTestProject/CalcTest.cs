using KerlkraftwerkGame;
using System.Reflection;

namespace MyTestProject
{
    [TestClass]
    public class CalcTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Calc c = new Calc();

            int result = c.Diff(2, 1);

            Assert.AreEqual(1, result);
        }
    }
}