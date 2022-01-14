using System;
using Ferret.InGame.Presentation.Controller;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class EffectView : MonoBehaviour, IPoolObject
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
        }

        public void Release()
        {
            _release?.Invoke();
            transform.SetParent(null);
        }
    }
}