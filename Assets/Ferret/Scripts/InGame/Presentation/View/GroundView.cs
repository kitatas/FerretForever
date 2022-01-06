using EFUK;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class GroundView : MonoBehaviour
    {
        private float _moveSpeed = 10.0f;
        private float _startPositionX = 12.0f;
        private float _endPositionX = -20.0f;

        public void Init()
        {

        }

        public void Tick(float deltaTime)
        {
            transform.TranslateX(_moveSpeed * deltaTime * -1);

            if (transform.position.x <= _endPositionX)
            {
                transform.TranslateX(_startPositionX - _endPositionX);

                // TODO: main state
                {
                    // init gimmick
                }
            }
        }
    }
}