using Microsoft.AspNetCore.Mvc;
using BattleShipGame.Models;
using BattleShipGame.interfaces;
using BattleShipGame.enums;

namespace BattleShipGame.Controllers
{
    [ApiController]
    [Route("api/battle_controller")]
    public class BattleController : ControllerBase
    {   
        private IShipPlacer shipPlacer;
        private IShipCreator shipCreator;
        private List<Ships> PlayerOneShips = new List<Ships>();
        private List<Ships> PlayerTwoShips = new List<Ships>();
        private Board board;
        public BattleController(IShipPlacer shipPlacer, IShipCreator shipCreator)
        {
            board = new Board(10, 10);

            this.shipPlacer = shipPlacer;
            this.shipCreator = shipCreator;
            
            shipCreator.AddShipToList(PlayerOneShips);
            shipCreator.AddShipToList(PlayerTwoShips);
        }

        [HttpGet("player_one")]
        public IActionResult PlayerOne()
        {
            Random random = new Random();
            bool[,] squaresOccupied = new bool[board.CoordinateBoardX, board.CoordinateBoardY];

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
            bool[,] squaresOccupied = new bool[board.CoordinateBoardX, board.CoordinateBoardY];

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
        private string GetDirectionString(Ships ship)
        {
            switch (ship.Direction)
            {
                case ShipDirection.North:
                    return "North";
                case ShipDirection.South:
                    return "South";
                case ShipDirection.East:
                    return "East";
                case ShipDirection.West:
                    return "West";
                default:
                    return "Unknown";
            }
        }
    }
}