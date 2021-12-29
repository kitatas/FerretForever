using System;
using EFUK;
using Ferret.InGame.Domain.UseCase;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ferret.InGame.Presentation.Controller
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerController : MonoBehaviour
    {
        private PlayerMoveUseCase _playerMoveUseCase;

        public void Init()
        {
            _playerMoveUseCase = new PlayerMoveUseCase(GetComponent<Rigidbody2D>());
        }

        public void Jump()
        {
            // TODO: 接地状態
            {
                var delayTime = Random.Range(0f, 0.05f);
                this.Delay(delayTime, _playerMoveUseCase.Jump);
            }
        }
    }
}