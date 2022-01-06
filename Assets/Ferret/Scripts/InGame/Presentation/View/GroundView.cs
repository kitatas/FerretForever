using EFUK;
using Ferret.InGame.Presentation.Controller;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public sealed class GroundView : MonoBehaviour
    {
        private SpriteRenderer _sprite;
        private Collider2D _collider;
        private GimmickController _gimmickController;

        private float _moveSpeed = 10.0f;
        private float _startPositionX = 12.0f;
        private float _endPositionX = -20.0f;

        public void Init(GimmickController gimmickController)
        {
            _sprite = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
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
            _sprite.enabled = value;
            _collider.enabled = value;
        }

        public void ActivateChildren(bool value)
        {
            gameObject.SetActiveChildren(value);
        }
    }
}