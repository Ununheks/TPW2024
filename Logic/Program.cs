
namespace Logic
{
    class Program
    {
        static void Main(string[] args)
        {
            LogicService logicService = new LogicService();
            logicService.Start(10, 10, 400, 300);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
