using Ferret.InGame.Presentation.Controller;
using UniRx.Toolkit;
using UnityEngine;

namespace Ferret.InGame.Domain.Factory
{
    public sealed class PlayerFactory : ObjectPool<PlayerController>
    {
        private PlayerController _player;

        public void Set(PlayerController player)
        {
            _player = player;
        }

        protected override PlayerController CreateInstance()
        {
            var player = Object.Instantiate(_player);
            player.Init();
            return player;
        }
    }
}