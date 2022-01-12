using System.Collections.Generic;
using Ferret.InGame.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Domain.Factory
{
    public sealed class EffectFactory
    {
        private readonly List<EffectView> _list;

        public EffectFactory()
        {
            _list = new List<EffectView>();
        }

        public EffectView Rent(EffectView effect)
        {
            var instance = _list.Find(x => x.type == effect.type);
            if (instance == null)
            {
                instance = Object.Instantiate(effect);
                instance.Init(() => Return(instance));
            }
            else
            {
                _list.Remove(instance);
                instance.gameObject.SetActive(true);
            }

            return instance;
        }

        private void Return(EffectView effect)
        {
            effect.gameObject.SetActive(false);
            _list.Add(effect);
        }
    }
}