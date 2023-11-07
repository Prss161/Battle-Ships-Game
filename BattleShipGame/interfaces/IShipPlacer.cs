using BattleShipGame.enums;
using BattleShipGame.Models;

namespace BattleShipGame.interfaces
{
    public interface IShipPlacer
    {
        void PlaceShipOnBoard(Ships ship, Random random, bool[,] squaresOccupied);

        void SetDirectionProperties(Ships ship, ShipDirection shipDirection);
    }
   
}