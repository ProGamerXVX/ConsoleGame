using System;
using System.Threading;

namespace RolePlayGame
{
    internal class Program
    {
        private static void Start()
        {
            Console.SetBufferSize(120, 30);
            Console.SetCursorPosition(Console.WindowWidth / 2-4, Console.WindowHeight / 2);
            string str = "RPG Game";
            Console.Title = "V0.1";
            for (int i = 0; i < str.Length; i++)
            {
                Thread.Sleep(100);
                Console.Write(str[i]);
            }
            Thread.Sleep(1000);
            Console.SetCursorPosition((Console.WindowWidth / 2) - 20, (Console.WindowHeight / 2) + 2);
            Console.Write("Please press any key to start this game.");
            _ = Console.ReadKey();
        }

        private static void Main()
        {
            Start();
            Menu._Menu();
        }
    }
}
