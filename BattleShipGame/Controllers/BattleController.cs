using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BattleShipGame.Models;
using System.Runtime.CompilerServices;

namespace BattleShipGame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BattleController : ControllerBase
    {
        private List<Ships> PlayerOneShips = new List<Ships>();
        private List<Ships> PlayerTwoShips = new List<Ships>();
        private int CoordinateBoardX = 10;
        private int CoordinateBoardY = 10;
        private int[,] board;

        public BattleController()
        {
            Ships Carrier = new Ships("Carrier", 5, 0, 0, 0, 0, false, false, false, false);
            Ships BattleShip = new Ships("BattleShip", 4, 0, 0, 0, 0, false, false, false, false);
            Ships Destroyer = new Ships("Destroyer", 3, 0, 0, 0, 0, false, false, false, false);
            Ships Submarine = new Ships("Submarine", 3, 0, 0, 0, 0, false, false, false, false);
            Ships Patrol_boat = new Ships("Patrol_boat", 2, 0, 0, 0, 0, false, false, false, false);

            PlayerOneShips.Add(Carrier);
            PlayerOneShips.Add(BattleShip);
            PlayerOneShips.Add(Destroyer);
            PlayerOneShips.Add(Submarine);
            PlayerOneShips.Add(Patrol_boat);

            Ships Carrier2 = new Ships("Carrier", 5, 0, 0, 0, 0, false, false, false, false);
            Ships BattleShip2 = new Ships("BattleShip", 4, 0, 0, 0, 0, false, false, false, false);
            Ships Destroyer2 = new Ships("Destroyer", 3, 0, 0, 0, 0, false, false, false, false);
            Ships Submarine2 = new Ships("Submarine", 3, 0, 0, 0, 0, false, false, false, false);
            Ships Patrol_boat2 = new Ships("Patrol_boat", 2, 0, 0, 0, 0, false, false, false, false);

            PlayerTwoShips.Add(Carrier);
            PlayerTwoShips.Add(BattleShip);
            PlayerTwoShips.Add(Destroyer);
            PlayerTwoShips.Add(Submarine);
            PlayerTwoShips.Add(Patrol_boat);            

            board = new int[CoordinateBoardX, CoordinateBoardY];
        }

        [HttpGet("PlayerOne")]
        public IActionResult PlayerOne()
        {
            Random random = new Random();
            bool[,] squaresOccupied = new bool[CoordinateBoardX, CoordinateBoardY];

            foreach (var ship in PlayerOneShips)
            {
                PlaceShipOnBoard(ship, random, squaresOccupied);
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

        [HttpGet("PlayerTwo")]
        public IActionResult PlayerTwo()
        {
            Random random = new Random();
            bool[,] squaresOccupied = new bool[CoordinateBoardX, CoordinateBoardY];

            foreach (var ship in PlayerTwoShips)
            {
                PlaceShipOnBoard(ship, random, squaresOccupied);
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

        private void PlaceShipOnBoard(Ships ship, Random random, bool[,] squaresOccupied)
        {
            while (true)
            {
                int x = random.Next(0, CoordinateBoardX);
                int y = random.Next(0, CoordinateBoardY);
                int direction = random.Next(4); // 0 for North, 1 for South, 2 for East, 3 for West

                bool isValidPlacement = true;

                if (direction == 0 && (y + ship.Size) < CoordinateBoardY)
                {
                    // Check if the ship's path is clear
                    for (int i = 0; i < ship.Size; i++)
                    {
                        if (squaresOccupied[x, y + i])
                        {
                            isValidPlacement = false;
                            break;
                        }
                    }

                    if (isValidPlacement)
                    {
                        // Mark squares as occupied and set ship properties
                        for (int i = 0; i < ship.Size; i++)
                        {
                            squaresOccupied[x, y + i] = true;
                        }
                        ship.LocationX = x;
                        ship.LocationY = y;
                        ship.CalculatedX = x;
                        ship.CalculatedY = y + ship.Size - 1;
                        SetDirectionProperties(ship, direction);
                        break;
                    }
                }
                else if (direction == 1 && (y - ship.Size) >= 0)
                {
                    for (int i = 0; i < ship.Size; i++)
                    {
                        if (squaresOccupied[x, y - i])
                        {
                            isValidPlacement = false;
                            break;
                        }
                    }

                    if (isValidPlacement)
                    {
                        for (int i = 0; i < ship.Size; i++)
                        {
                            squaresOccupied[x, y - i] = true;
                        }
                        ship.LocationX = x;
                        ship.LocationY = y;
                        ship.CalculatedX = x;
                        ship.CalculatedY = y - ship.Size + 1;
                        SetDirectionProperties(ship, direction);
                        break;
                    }
                }
                else if (direction == 2 && (x + ship.Size) < CoordinateBoardX)
                {
                    for (int i = 0; i < ship.Size; i++)
                    {
                        if (squaresOccupied[x + i, y])
                        {
                            isValidPlacement = false;
                            break;
                        }
                    }

                    if (isValidPlacement)
                    {
                        for (int i = 0; i < ship.Size; i++)
                        {
                            squaresOccupied[x + i, y] = true;
                        }
                        ship.LocationX = x;
                        ship.LocationY = y;
                        ship.CalculatedX = x + ship.Size - 1;
                        ship.CalculatedY = y;
                        SetDirectionProperties(ship, direction);
                        break;
                    }
                }
                else if (direction == 3 && (x - ship.Size) >= 0)
                {
                    for (int i = 0; i < ship.Size; i++)
                    {
                        if (squaresOccupied[x - i, y])
                        {
                            isValidPlacement = false;
                            break;
                        }
                    }

                    if (isValidPlacement)
                    {
                        for (int i = 0; i < ship.Size; i++)
                        {
                            squaresOccupied[x - i, y] = true;
                        }
                        ship.LocationX = x;
                        ship.LocationY = y;
                        ship.CalculatedX = x - ship.Size + 1;
                        ship.CalculatedY = y;
                        SetDirectionProperties(ship, direction);
                        break;
                    }
                }
            }
        }
        private void SetDirectionProperties(Ships ship, int direction)
        {
            ship.DirectionNorth = direction == 0;
            ship.DirectionSouth = direction == 1;
            ship.DirectionEast = direction == 2;
            ship.DirectionWest = direction == 3;
        }

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