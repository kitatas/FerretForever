using System;
using EFUK;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class GroundView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer main = default;
        [SerializeField] private SpriteRenderer child = default;

        private Action<GroundView> _setUp;

        private float _moveSpeed = 10.0f;
        private float _startPositionX = 12.0f;
        private float _endPositionX = -20.0f;

        public void Init(Action<GroundView> setUp)
        {
            _setUp = setUp;
        }

        public void Tick(float deltaTime)
        {
            transform.TranslateX(_moveSpeed * deltaTime * -1);

            if (transform.position.x <= _endPositionX)
            {
                transform.TranslateX(_startPositionX - _endPositionX);

                // TODO: main state
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