using System.Collections.Generic;
using Ferret.InGame.Presentation.Controller;
using UnityEngine;

namespace Ferret.InGame.Domain.Factory
{
    public sealed class EnemyFactory
    {
        private readonly List<EnemyController> _list;

        public EnemyFactory()
        {
            _list = new List<EnemyController>();
        }

        public EnemyController Rent(EnemyController enemy)
        {
            var instance = _list.Find(x => x.type == enemy.type);
            if (instance == null)
            {
                instance = Object.Instantiate(enemy);
                instance.Init(() => Return(instance));
            }
            else
            {
                _list.Remove(instance);
                instance.gameObject.SetActive(true);
            }

            return instance;
        }

        private void Return(EnemyController enemy)
        {
            enemy.gameObject.SetActive(false);
            _list.Add(enemy);
        }
    }
}