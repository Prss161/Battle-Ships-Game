using Microsoft.AspNetCore.Mvc;
using BattleShipGame.Models;
using BattleShipGame.implementations;

namespace BattleShipGame.Controllers
{
    [ApiController]
    [Route("api/battle_controller")]
    public class BattleController : ControllerBase
    {
        private ShipCreator shipCreator = new ShipCreator();
        private ShipPlacer shipPlacer;
        private List<Ships> PlayerOneShips = new List<Ships>();
        private List<Ships> PlayerTwoShips = new List<Ships>();
        private int CoordinateBoardX = 10;
        private int CoordinateBoardY = 10;
        private int[,] board;

        public BattleController()
        {
            board = new int[CoordinateBoardX, CoordinateBoardY];

            shipCreator.AddShipToList(PlayerOneShips);
            shipCreator.AddShipToList(PlayerTwoShips);

            shipPlacer = new ShipPlacer();
        }

        [HttpGet("player_one")]
        public IActionResult PlayerOne()
        {
            Random random = new Random();
            bool[,] squaresOccupied = new bool[CoordinateBoardX, CoordinateBoardY];

            foreach (var ship in PlayerOneShips)
            {
                shipPlacer.PlaceShipOnBoard(ship, random, squaresOccupied);
            }

            var shipObjects = new List<object>();
            foreach (var ship in PlayerOneShips)
            {
                var shipObject = new
                {
                    name = ship.Name,
                    size = ship.Size,
                    locationX = ship.LocationX,
                    locationY = ship.LocationY,
                    direction = GetDirectionString(ship)
                };
                shipObjects.Add(shipObject);
            }

            return Ok(shipObjects);
        }

        [HttpGet("player_two")]
        public IActionResult PlayerTwo()
        {
            Random random = new Random();
            bool[,] squaresOccupied = new bool[CoordinateBoardX, CoordinateBoardY];

            foreach (var ship in PlayerTwoShips)
            {
                shipPlacer.PlaceShipOnBoard(ship, random, squaresOccupied);
            }

            var shipObjects = new List<object>();
            foreach (var ship in PlayerTwoShips)
            {
                var shipObject = new
                {
                    name = ship.Name,
                    size = ship.Size,
                    locationX = ship.LocationX,
                    locationY = ship.LocationY,
                    direction = GetDirectionString(ship)
                };
                shipObjects.Add(shipObject);
            }

            return Ok(shipObjects);
        }
// Here i need post condition to check if ship is destroyed or if its game over

        private string GetDirectionString(Ships ship)
        {
            if (ship.DirectionNorth)
                return "North";
            if (ship.DirectionSouth)
                return "South";
            if (ship.DirectionEast)
                return "East";
            if (ship.DirectionWest)
                return "West";
            return "Unknown";
        }
    }
}