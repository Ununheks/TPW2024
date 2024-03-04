using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TPW2024
{
    [TestClass]
    public class TestSimpleClass
    {
        [TestMethod]
        public void TestHelloMethod()
        {
            SimpleClass obj = new SimpleClass();
            string result = obj.Hello();
            Assert.AreEqual("Hello World", result);
        }

        [TestMethod]
        public void TestAddTwoInts()
        {
            SimpleClass obj = new SimpleClass();
            int result = obj.AddTwoInts(2,5);
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void TestSayTrue()
        {
            SimpleClass obj = new SimpleClass();
            bool result = obj.SayTrue();
            Assert.IsTrue(result);
        }
    }
}
