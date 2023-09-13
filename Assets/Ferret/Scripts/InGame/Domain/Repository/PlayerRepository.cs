using Ferret.InGame.Data.DataStore;
using UniEx;

namespace Ferret.InGame.Domain.Repository
{
    public sealed class PlayerRepository
    {
        private readonly PlayerTable _playerTable;

        public PlayerRepository(PlayerTable playerTable)
        {
            _playerTable = playerTable;
        }

        public PlayerData Get()
        {
            return _playerTable.list.GetRandom();
        }
    }
}