using UnityEngine;

namespace Ferret.Common.Domain.UseCase
{
    public interface IBgmUseCase
    {
        AudioClip GetBgm(BgmType type);
    }
}