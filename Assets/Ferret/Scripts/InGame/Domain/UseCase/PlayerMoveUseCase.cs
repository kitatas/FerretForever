using UnityEngine;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class PlayerMoveUseCase
    {
        private const float _jumpRate = 0.05f;
        private const float _jumpPower = 800.0f;
        private readonly Vector2 _jumpVector = _jumpPower * Vector2.up;

        private const float _torquePower = 300.0f;
        private const float _blowPower = 1000.0f;
        private readonly Vector2 _blowVector = new Vector2(-1.0f, 1.0f) * _blowPower;

        private readonly Rigidbody2D _rigidbody;

        public PlayerMoveUseCase(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Jump()
        {
            var rate = Random.Range(1.0f - _jumpRate, 1.0f + _jumpRate);
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

        public void Blow()
        {
            SetConstraint(RigidbodyConstraints2D.None);
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(_blowVector);
            _rigidbody.AddTorque(_torquePower);
        }
    }
}