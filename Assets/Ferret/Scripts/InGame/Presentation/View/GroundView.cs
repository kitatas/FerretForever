using System;
using System.Collections.Generic;
using EFUK;
using Ferret.InGame.Presentation.Controller;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class GroundView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer main = default;
        [SerializeField] private SpriteRenderer child = default;

        private Action<GroundView> _setUp;
        private List<IPoolObject> _poolList;

        private float _moveSpeed = 10.0f;
        private float _startPositionX = 12.0f;
        private float _endPositionX = -20.0f;

        public void Init(Action<GroundView> setUp)
        {
            _setUp = setUp;
            _poolList = new List<IPoolObject>();
        }

        public void SetUp()
        {
            Activate(true);
            foreach (var poolObject in _poolList)
            {
                if (poolObject.self.transform.parent == transform)
                {
                    poolObject.Release();
                }
            }
            _poolList.Clear();
        }

        public void SavePool(IPoolObject poolObject)
        {
            _poolList.Add(poolObject);
            gameObject.SetChild(poolObject.self);
        }

        public void Tick(GameState state, float deltaTime)
        {
            transform.TranslateX(_moveSpeed * deltaTime * -1);

            if (transform.position.x <= _endPositionX)
            {
                transform.TranslateX(_startPositionX - _endPositionX);

                if (state == GameState.Main)
                {
                    _setUp?.Invoke(this);
                }
            }
        }

        public void Activate(bool value)
        {
            main.enabled = value;
            child.enabled = value;
        }
    }
}