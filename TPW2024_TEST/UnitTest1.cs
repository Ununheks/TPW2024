namespace TPW2024_TEST
{
    [TestClass]
    public class TestSimpleClass
    {
        [TestMethod]
        public void TestHelloMethod()
        {
            TPW2024.SimpleClass obj = new TPW2024.SimpleClass();
            string result = obj.Hello();
            Assert.AreEqual("Hello World", result);
        }

        [TestMethod]
        public void TestAddTwoInts()
        {
            TPW2024.SimpleClass obj = new TPW2024.SimpleClass();
            int result = obj.AddTwoInts(2, 5);
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void TestSayTrue()
        {
            TPW2024.SimpleClass obj = new TPW2024.SimpleClass();
            bool result = obj.SayTrue();
            Assert.IsTrue(result);
        }
    }
}