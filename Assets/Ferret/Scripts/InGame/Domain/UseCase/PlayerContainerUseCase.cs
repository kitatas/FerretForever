using Ferret.InGame.Data.Container;
using Ferret.InGame.Presentation.Controller;
using UnityEngine;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class PlayerContainerUseCase
    {
        private readonly PlayerContainer _playerContainer;

        public PlayerContainerUseCase(PlayerContainer playerContainer)
        {
            _playerContainer = playerContainer;
        }

        public void JumpAll()
        {
            _playerContainer.ControlAll(x => x.Jump());
        }

        public bool IsNone()
        {
            return _playerContainer.count == 0;
        }

        public int GetVictimCount()
        {
            return Mathf.Min(_playerContainer.count, InGameConfig.MAX_VICTIM_COUNT);
        }

        public PlayerController GetVictim()
        {
            return _playerContainer.Dequeue();
        }
    }
}