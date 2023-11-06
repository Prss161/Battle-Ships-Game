using BattleShipGame.interfaces;
using BattleShipGame.Models;

namespace BattleShipGame.implementations 
{
    public class ShipPlacer : IShipPlacer
    {
        private int CoordinateBoardX = 10;
        private int CoordinateBoardY = 10;
        public void SetDirectionProperties(Ships ship, int direction)
        {
            ship.DirectionNorth = direction == 0;
            ship.DirectionSouth = direction == 1;
            ship.DirectionEast = direction == 2;
            ship.DirectionWest = direction == 3;
        }
        public void PlaceShipOnBoard(Ships ship, Random random, bool[,] squaresOccupied)
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
    }
}