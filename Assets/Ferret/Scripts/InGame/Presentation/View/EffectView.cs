using System;
using EFUK;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class EffectView : MonoBehaviour
    {
        [SerializeField] private EffectType effectType = default;
        [SerializeField] private ParticleSystem particle = default;

        private Action _release;
        private ParticleSystem.MainModule _mainModule;

        public EffectType type => effectType;

        public void Init(Action release)
        {
            _release = release;
            _mainModule = particle.main;
        }

        public void Play(Vector3 position, EffectColor color)
        {
            transform.position = position;
            _mainModule.startColor = color.ConvertColor();

            this.Delay(2.0f, () => _release?.Invoke());
        }
    }
}