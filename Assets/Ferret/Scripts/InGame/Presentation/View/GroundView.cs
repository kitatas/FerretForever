using EFUK;
using Ferret.InGame.Presentation.Controller;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class GroundView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer main = default;
        [SerializeField] private SpriteRenderer child = default;

        private GimmickController _gimmickController;

        private float _moveSpeed = 10.0f;
        private float _startPositionX = 12.0f;
        private float _endPositionX = -20.0f;

        public void Init(GimmickController gimmickController)
        {
            _gimmickController = gimmickController;
        }

        public void Tick(float deltaTime)
        {
            transform.TranslateX(_moveSpeed * deltaTime * -1);

            if (transform.position.x <= _endPositionX)
            {
                transform.TranslateX(_startPositionX - _endPositionX);

                // TODO: main state
                {
                    _gimmickController.SetUp(this);
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