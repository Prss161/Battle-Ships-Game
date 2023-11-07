using BattleShipGame.enums;
using BattleShipGame.interfaces;
using BattleShipGame.Models;

namespace BattleShipGame.implementations 
{
    public class ShipPlacer : IShipPlacer
    {
        private int CoordinateBoardX = 10;
        private int CoordinateBoardY = 10;
        public void SetDirectionProperties(Ships ship, ShipDirection shipDirection)
        {
            ship.Direction = shipDirection;
        }
        public void PlaceShipOnBoard(Ships ship, Random random, bool[,] squaresOccupied)
        {
            while (true)
            {
                int x = random.Next(0, CoordinateBoardX);
                int y = random.Next(0, CoordinateBoardY);
                ShipDirection direction = (ShipDirection)random.Next(4);

                bool isValidPlacement = true;

                if (direction == ShipDirection.North && (y + ship.Size) < CoordinateBoardY)
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
                else if (direction == ShipDirection.South && (y - ship.Size) >= 0)
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
                else if (direction == ShipDirection.East && (x + ship.Size) < CoordinateBoardX)
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
                else if (direction == ShipDirection.West && (x - ship.Size) >= 0)
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
    }
}
