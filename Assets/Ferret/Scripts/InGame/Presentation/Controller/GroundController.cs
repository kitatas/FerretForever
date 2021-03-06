using System;
using System.Collections.Generic;
using Ferret.InGame.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class GroundController : MonoBehaviour
    {
        [SerializeField] private List<GroundView> groundViews = default;

        public void Init(Action<GroundView> setUp)
        {
            foreach (var ground in groundViews)
            {
                ground.Init(x => setUp?.Invoke(x));
            }
        }

        public void Tick(GameState state, float deltaTime)
        {
            foreach (var ground in groundViews)
            {
                ground.Tick(state, deltaTime);
            }
        }
    }
}