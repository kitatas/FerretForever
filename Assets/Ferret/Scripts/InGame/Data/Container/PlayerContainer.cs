using System;
using System.Collections.Generic;
using Ferret.InGame.Presentation.Controller;

namespace Ferret.InGame.Data.Container
{
    public sealed class PlayerContainer
    {
        private readonly List<PlayerController> _players;

        public PlayerContainer()
        {
            _players = new List<PlayerController>();
        }

        public void Add(PlayerController player)
        {
            _players.Add(player);
        }

        public void Remove(PlayerController player)
        {
            _players.Remove(player);
        }

        public PlayerController Dequeue()
        {
            var player = _players[0];
            _players.RemoveAt(0);
            return player;
        }

        public int count => _players.Count;

        public void ControlAll(Action<PlayerController> action)
        {
            foreach (var player in _players)
            {
                action?.Invoke(player);
            }
        }
    }
}