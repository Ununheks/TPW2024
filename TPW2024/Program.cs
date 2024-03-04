using System;
namespace TPW2024
{
     class Program
     {
         static void Main(string[] args)
         {
             SimpleClass obj = new SimpleClass();
             string result = obj.Hello();
             Console.WriteLine(result);
         }
     }
}