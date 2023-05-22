using BattleshipLiteLibrary;
using BattleshipLiteLibrary.Models;

namespace BattleshipLite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();
            PlayerInfoModel player1 = CreatePlayer("Player 1");
            PlayerInfoModel player2 = CreatePlayer("Player 2");

            Console.ReadLine();
        }

        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship Lite");
            Console.WriteLine("created by Yamiraj");
            Console.WriteLine();
        }

        private static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            PlayerInfoModel output = new PlayerInfoModel();

            Console.WriteLine($"Player information for {playerTitle}");

            // Ask player name
            output.PlayersName = AskPlayerName();
            // Load up shot grid
            GameLogic.GridInitialise(output);

            // Ask player for 5 ship placements
            PlaceShips(output);

            // Clear
            Console.Clear();

            return output;
        }

        private static string AskPlayerName()
        {
            Console.Write("What is your player name: ");
            string output = Console.ReadLine();

            return output;
        }

        private static void PlaceShips(PlayerInfoModel model)
        {
            do
            {
                Console.Write($"Where do you want to place your ship{model.ShipLocations.Count + 1}: ");
                string location = Console.ReadLine();

                // validate location is valid
                bool isValidLocation = GameLogic.PlaceShip(model,location);

                if (isValidLocation == false)
                {
                    Console.WriteLine("This is not a valid location. Please try again.");
                }

            } while (model.ShipLocations.Count < 5);
        }
    }
}