using ClassLib;
using ClassLib.Factory_Method;
using ClassLib.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new StartMenu());
            Console.WriteLine("Welcome to the 'Robots'!\r\n");
            Console.WriteLine(game.Turn("help") + "\r\n");

            string input = Console.ReadLine().ToLower();

            while (input != "exit")
            {
                Console.WriteLine("\r\n" + game.Turn(input) + "\r\n");
                input = Console.ReadLine().ToLower();
            }

            Console.WriteLine("\r\n" + game.Turn("exit"));
            System.Threading.Thread.Sleep(3000);
            Environment.Exit(0);
        }
    }
}
