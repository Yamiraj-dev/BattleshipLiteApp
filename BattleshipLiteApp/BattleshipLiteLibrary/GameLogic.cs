using BattleshipLiteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLiteLibrary
{
    public static class GameLogic
    {
        public static void GridInitialise(PlayerInfoModel model)
        {
            List<string> letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };

            List<int> numbers = new List<int>()
            {
                1,
                2,
                3,
                4,
                5
            };

            foreach (string letter in  letters)
            {
                foreach (int number in numbers)
                {
                    AddGridSpot(model, letter, number);
                }
            }
        }

        private static void AddGridSpot(PlayerInfoModel model, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel()
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };

            model.ShotGrid.Add(spot);
        }

        public static bool PlaceShip(PlayerInfoModel model, string location)
        {
            throw new NotImplementedException();
        }

        public static bool PlayerStillActive(PlayerInfoModel player2)
        {
            throw new NotImplementedException();
        }

        public static int GetShotCount(PlayerInfoModel winner)
        {
            throw new NotImplementedException();
        }

        public static (string row, int column) SpotIdentifier(string shot)
        {
            throw new NotImplementedException();
        }

        public static bool ValidateShot(PlayerInfoModel activePlayer, string row, int column)
        {
            throw new NotImplementedException();
        }

        public static bool ShotResultIdentifier(PlayerInfoModel opponent, string row, int column)
        {
            throw new NotImplementedException();
        }

        public static void MarkShotResults(PlayerInfoModel activePlayer, string row, int column, bool isHit)
        {
            throw new NotImplementedException();
        }
    }
}
