using Ferret.Common.Presentation.Controller;
using Ferret.InGame.Presentation.Controller;
using UniRx.Toolkit;
using UnityEngine;

namespace Ferret.InGame.Domain.Factory
{
    public sealed class PlayerFactory : ObjectPool<PlayerController>
    {
        private PlayerController _player;
        private readonly ISeController _seController;

        public PlayerFactory(ISeController seController)
        {
            _seController = seController;
        }

        public void Set(PlayerController player)
        {
            _player = player;
        }

        protected override PlayerController CreateInstance()
        {
            var player = Object.Instantiate(_player);
            player.Init(() => Return(player), _seController);
            return player;
        }
    }
}