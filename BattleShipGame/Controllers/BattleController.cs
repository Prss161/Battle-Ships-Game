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
        private List<Ships> ships = new List<Ships>();
        private int CoordinateBoardX = 10;
        private int CoordinateBoardY = 10;
        private int[,] board;
        
        public BattleController()
        {

            Ships Carrier = new Ships("Carrier", 5, 0, 0, false, false, false, false);
            Ships BattleShip = new Ships("BattleShip", 4, 0, 0, false, false, false, false);
            Ships Destroyer = new Ships("Destroyer", 3, 0, 0, false, false, false, false);
            Ships Submarine = new Ships("Submarine", 3, 0, 0, false, false, false, false);
            Ships Patrol_boat = new Ships("Patrol_boat", 2, 0, 0,  false, false, false, false);

            ships.Add(Carrier);
            ships.Add(BattleShip);
            ships.Add(Destroyer);
            ships.Add(Submarine);
            ships.Add(Patrol_boat);

            board = new int[CoordinateBoardX, CoordinateBoardY];
        }
    
        [HttpGet("CreateBoard")]
        public IActionResult CreateBoard()
        {
            Random random = new Random();

            foreach (var ship in ships)
            {
                PlaceShipOnBoard(ship, random);
                CalculateDirection(ship, random);
            }
            var shipObjects = new List<object>();
            foreach (var ship in ships)
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
        private void PlaceShipOnBoard(Ships ship, Random random)
        {
            while (true)
            {
                int maxX = CoordinateBoardX;
                int maxY = CoordinateBoardY;

                int x = random.Next(0, maxX);
                int y = random.Next(0, maxY);

                bool isValidPlacement = true;

                int boundX, boundY;
                if (ship.DirectionNorth)
                {
                    boundX = x - ship.Size + 1;
                    boundY = y;
                }
                else if (ship.DirectionSouth)
                {
                    boundX = x + ship.Size - 1;
                    boundY = y;
                }
                else if (ship.DirectionEast)
                {
                    boundX = x;
                    boundY = y + ship.Size - 1;
                }
                else
                {
                    boundX = x;
                    boundY = y - ship.Size + 1;
                }

                if (boundX < 0 || boundX >= CoordinateBoardX || boundY < 0 || boundY >= CoordinateBoardY)
                {
                    isValidPlacement = false;
                }
                else
                {
                    for (int i = 0; i < ship.Size; i++)
                    {
                        int newX = x;
                        int newY = y;

                        if (ship.DirectionNorth)
                        {
                            newX -= i;
                        }
                        else if (ship.DirectionSouth)
                        {
                            newX += i;
                        }
                        else if (ship.DirectionEast)
                        {
                            newY += i;
                        }
                        else
                        {
                            newY -= i;
                        }

                        if (newX < 0 || newX >= CoordinateBoardX || newY < 0 || newY >= CoordinateBoardY || board[newX, newY] != 0)
                        {
                            isValidPlacement = false;
                            break;
                        }
                    }
                }

                if (isValidPlacement)
                {
                    for (int i = 0; i < ship.Size; i++)
                    {
                        int newX = x;
                        int newY = y;

                        if (ship.DirectionNorth)
                        {
                            newX -= i;
                        }
                        else if (ship.DirectionSouth)
                        {
                            newX += i;
                        }
                        else if (ship.DirectionEast)
                        {
                            newY += i;
                        }
                        else
                        {
                            newY -= i;
                        }

                        board[newX, newY] = 1;
                    }
                    ship.LocationX = x;
                    ship.LocationY = y;
                    break;
                }
            }
        }

        private void CalculateDirection(Ships ship, Random random)
        {
            int direction = random.Next(4);

            while (true)
            {
                switch (direction)
                {
                    case 0: // North
                        if (IsValidDirection(ship, -1, 0))
                        {
                            ship.DirectionNorth = true;
                            return;
                        }
                        break;
                    case 1: // South
                        if (IsValidDirection(ship, 1, 0))
                        {
                            ship.DirectionSouth = true;
                            return;
                        }
                        break;
                    case 2: // East
                        if (IsValidDirection(ship, 0, 1))
                        {
                            ship.DirectionEast = true;
                            return;
                        }
                        break;
                    case 3: // West
                        if (IsValidDirection(ship, 0, -1))
                        {
                            ship.DirectionWest = true;
                            return;
                        }
                        break;
                }

                // If the direction is not valid, select a new random direction.
                direction = random.Next(4);
            }
        }

        private bool IsValidDirection(Ships ship, int deltaX, int deltaY)
        {
            int newX = ship.LocationX + deltaX;
            int newY = ship.LocationY + deltaY;

            if (newX < 0 || newX >= CoordinateBoardX || newY < 0 || newY >= CoordinateBoardY)
            {
                return false;
            }

            for (int i = 0; i < ship.Size; i++)
            {
                if (newX < 0 || newX >= CoordinateBoardX || newY < 0 || newY >= CoordinateBoardY || board[newX, newY] != 0)
                {
                    return false;
                }
                newX += deltaX;
                newY += deltaY;
            }

            return true;
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