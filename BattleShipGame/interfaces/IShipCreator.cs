using BattleShipGame.Models;

namespace BattleShipGame.interfaces
{
    public interface IShipCreator
    {
        public void AddShipToList(List<Ships> playerShips);
    }
}