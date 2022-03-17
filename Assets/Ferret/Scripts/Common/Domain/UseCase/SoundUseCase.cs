using System;
using System.Linq;
using EFUK;
using Ferret.Common.Domain.Repository;
using Ferret.Common.Domain.UseCase.Interface;
using UnityEngine;

namespace Ferret.Common.Domain.UseCase
{
    public sealed class SoundUseCase : IBgmUseCase, ISeUseCase
    {
        private readonly SoundRepository _soundRepository;

        public SoundUseCase(SoundRepository soundRepository)
        {
            _soundRepository = soundRepository;
        }

        public AudioClip GetBgm(BgmType type)
        {
            return _soundRepository.FindBgm(type).audioClip;
        }

        public AudioClip GetSe(SeType type)
        {
            switch (type)
            {
                case SeType.Button:
                case SeType.Jump:
                case SeType.Crash:
                case SeType.Explode:
                case SeType.Build:
                case SeType.Fall:
                case SeType.Ground:
                    return _soundRepository.FindSe(type).audioClip;
                case SeType.Scream:
                    return _soundRepository.FindAllSe(type).GetRandomParam().audioClip;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}