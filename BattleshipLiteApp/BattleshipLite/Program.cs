using BattleshipLiteLibrary;
using BattleshipLiteLibrary.Models;
using System.Threading.Tasks.Dataflow;

namespace BattleshipLite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();
            PlayerInfoModel activePlayer = CreatePlayer("Player 1");
            PlayerInfoModel opponent = CreatePlayer("Player 2");

            PlayerInfoModel winner = null;
            do
            {
                // Display grid of active player where they fired
                DisplayShotGrid(activePlayer);

                // Ask active player for a shot

                // Determine if shot was valid

                // Determine shot results
                RecordPlayerShot(activePlayer, opponent);

                // Determine if game continues
                bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

                // If over, set active player as winner

                // Else go to opponent
                if (doesGameContinue == true)
                {

                    // Swap using a temp variable
                    /*PlayerInfoModel temp = opponent;
                    opponent = activePlayer;
                    activePlayer = temp;*/

                    // New way of implementing this as of C# 7.0 (Tuple)
                    // Both are swapped at the same time, negating any concern about one value being overwritten
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
                else
                {
                    winner = activePlayer;
                }

            } while (winner == null);

            IdentifyWinner(winner);

            Console.ReadLine();
        }

        private static void IdentifyWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratulations to {winner.PlayersName}");
            Console.WriteLine($"{winner.PlayersName} took {GameLogic.GetShotCount(winner)} shots.");
            Spacer();
        }

        private static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
        {
            // Ask for a shot (Example: C4)
            // Split apart input to determine row and column
            // Determine if valid shot
            // Loop back if shot was not valid

            bool isValidShot = false;
            string row = "";
            int column = 0;

            do
            {
                string shot = AskForShot(activePlayer);
                try
                {
                    (row, column) = GameLogic.SpotIdentifier(shot);
                    isValidShot = GameLogic.ValidateShot(activePlayer, row, column);
                }
                catch (Exception ex)
                {

                    // Console.WriteLine("Error: " + ex.Message);
                    isValidShot = false;
                }

                if (isValidShot == false)
                {
                    Console.WriteLine("Invalid shot location. Please try again.");
                    Spacer();                   
                }

            } while (isValidShot == false);

            // Determine shot results
            bool isHit = GameLogic.ShotResultIdentifier(opponent, row, column);

            // Store data
            GameLogic.MarkShotResults(activePlayer, row, column, isHit);

            DisplayShotResults(row, column, isHit);
        }

       

        private static void DisplayShotResults(string row, int column, bool isHit)
        {
            if (isHit)
            {
                Console.WriteLine($"{row}{column} is a Hit!");
                Spacer();
            }
            else
            {
                Console.WriteLine($"{row}{column} is a Miss.");
                Spacer();
            }

            Console.WriteLine();
        }

        private static string AskForShot(PlayerInfoModel player)
        {
            Console.Write($"{player.PlayersName}, please enter your shot on the grid: ");
            string output = Console.ReadLine();

            return output;
        }

        private static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            foreach (var gridSpot in activePlayer.ShotGrid) 
            {
                if (gridSpot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                }

                if (gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridSpot.SpotLetter}{gridSpot.SpotNumber} "); // Fixed grid, forgot spacing and ommitted space between letter and number.
                }
                else if (gridSpot.Status == GridSpotStatus.Hit) 
                {
                    Console.Write(" X  ");
                }
                else if (gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O  ");
                }
                else
                {
                    Console.Write(" ?  "); //throwing a question mark instead of an exception
                }   
            }

            // Create 2 spaces after Grid is displayed.
            Console.WriteLine();
            Console.WriteLine();

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
                Console.Write($"Where do you want to place your ship ({model.ShipLocations.Count + 1}): ");
                string location = Console.ReadLine();

                // validate location is valid
                bool isValidLocation = false;

                try
                {
                    isValidLocation = GameLogic.PlaceShip(model, location);
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Error " + ex.Message);
                }

                if (isValidLocation == false)
                {
                    Console.WriteLine("This is not a valid location. Please try again.");
                }

            } while (model.ShipLocations.Count < 5);
        }

        private static void Spacer()
        {
            Console.WriteLine("-------------------");
        }
    }
}