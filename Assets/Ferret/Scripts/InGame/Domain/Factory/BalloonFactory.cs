using System.Collections.Generic;
using Ferret.InGame.Presentation.Controller;
using UnityEngine;

namespace Ferret.InGame.Domain.Factory
{
    public sealed class BalloonFactory
    {
        private readonly List<BalloonController> _list;

        public BalloonFactory()
        {
            _list = new List<BalloonController>();
        }

        public BalloonController Rent(BalloonController balloon)
        {
            var instance = _list.Find(x => x.type == balloon.type);
            if (instance == null)
            {
                instance = Object.Instantiate(balloon);
                instance.Init(() => Return(instance));
            }
            else
            {
                _list.Remove(instance);
                instance.gameObject.SetActive(true);
            }

            return instance;
        }

        private void Return(BalloonController balloon)
        {
            balloon.gameObject.SetActive(false);
            _list.Add(balloon);
        }
    }
}