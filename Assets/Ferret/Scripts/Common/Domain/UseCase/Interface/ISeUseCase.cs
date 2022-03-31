using UnityEngine;

namespace Ferret.Common.Domain.UseCase
{
    public interface ISeUseCase
    {
        AudioClip GetSe(SeType type);
    }
}