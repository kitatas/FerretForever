using UnityEngine;

namespace Ferret.Common.Domain.UseCase.Interface
{
    public interface ISeUseCase
    {
        AudioClip GetSe(SeType type);
    }
}