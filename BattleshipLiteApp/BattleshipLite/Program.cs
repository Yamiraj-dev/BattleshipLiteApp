using BattleshipLiteLibrary;
using BattleshipLiteLibrary.Models;

namespace BattleshipLite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();

            Console.ReadLine();
        }

        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship Lite");
            Console.WriteLine("created by Yamiraj");
            Console.WriteLine();
        }

        private static PlayerInfoModel CreatePlayer()
        {
            PlayerInfoModel output = new PlayerInfoModel();

            // Ask player name
            output.PlayersName = AskPlayerName();
            // Load up shot grid
            GameLogic.GridInitialise(output);

            // Ask player for 5 ship placements
            // Clear

        }

        private static string AskPlayerName()
        {
            Console.WriteLine("What is your player name: ");
            string output = Console.ReadLine();

            return output;
        }
    }
}