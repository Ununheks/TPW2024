
namespace Logic
{
    class Program
    {
        static void Main(string[] args)
        {
            LogicService logicService = new LogicService();
            logicService.CreateTable(400, 300);
            logicService.SpawnBalls(10, 10);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
