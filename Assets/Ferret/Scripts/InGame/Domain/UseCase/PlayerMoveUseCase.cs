using UnityEngine;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class PlayerMoveUseCase
    {
        private const float _jumpRate = 0.05f;
        private const float _jumpPower = 800f;
        private readonly Vector2 _jumpVector = _jumpPower * Vector2.up;
        private readonly Rigidbody2D _rigidbody;

        public PlayerMoveUseCase(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Jump()
        {
            var rate = Random.Range(1f - _jumpRate, 1f + _jumpRate);
            _rigidbody.AddForce(rate * _jumpVector);
        }

        public void SetSimulate(bool value)
        {
            _rigidbody.simulated = value;
        }

        public void SetConstraint(RigidbodyConstraints2D constraints)
        {
            _rigidbody.constraints = constraints;
        }
    }
}