using System.Collections.Generic;
using Ferret.InGame.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class GroundController : MonoBehaviour
    {
        [SerializeField] private GimmickController gimmickController = default;
        [SerializeField] private List<GroundView> groundViews = default;

        public void Init()
        {
            gimmickController.Init();

            foreach (var ground in groundViews)
            {
                ground.Init(gimmickController);
            }
        }

        public void Tick(float deltaTime)
        {
            foreach (var ground in groundViews)
            {
                ground.Tick(deltaTime);
            }
        }
    }
}