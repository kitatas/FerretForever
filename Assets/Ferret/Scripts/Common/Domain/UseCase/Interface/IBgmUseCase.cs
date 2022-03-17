using UnityEngine;

namespace Ferret.Common.Domain.UseCase.Interface
{
    public interface IBgmUseCase
    {
        AudioClip GetBgm(BgmType type);
    }
}