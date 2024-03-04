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
    }
}
